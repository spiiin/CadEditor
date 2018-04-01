--Script for search ROM bytes related to PPU picture changes
--Emulator: Mesen 0.9.5
--Rom: any
--Author: spiiin

PPU_PAGE = 0x2000;
PPU_PAGE_SIZE = 0x400;
START_PPU_ADDR = 0x100; --start PPU render zone
END_PPU_ADDR   = 0x200; --end PPU render zone

startFrame = 0;
endFrame = 0;

sortedAddresses = nil;
currentCorruptIndex = 0;
foundAddressesCount = 0;
screenshotDone = true;

hashes = {};

callbackStart = nil;
callbackEnd = nil;
callbackScreenshot = nil;

SAVESTATE_NO = 999;
CORRUPT_VALUE = 0x33;
WAIT_TO_SCREENSHOT = 15; --we must wait, while game enabled rendering to take screen

table.filter = function(t, filterIter)
    local out = {}
    for k, v in pairs(t) do
        if filterIter(v, k, t) then
            out[k] = v;
        end
    end
    return out
end
  
function getSortedAddresses(tbl)
  local keys = {};
  for key in pairs(tbl) do
      table.insert(keys, key);
  end

  table.sort(keys, function(a, b) return a < b end);

  return keys;
end
  
function hex(arg)
  return string.format("%02x", arg)
end

function getCurrentFrame()
  return emu.getState()["ppu"]["frameCount"];
end;

function save(fname, data)
    file = io.open(fname, "wb");
    file:write(data);
    file:close();
end;
  
function getPpuPage()
    local t = {};
    for i=0, PPU_PAGE_SIZE do
      t[i+1] = emu.read(PPU_PAGE+i, emu.memType.ppuDebug);
    end
    return string.char(table.unpack(t));
end

function onWritePPUZoneStart()
    emu.removeMemoryCallback(callbackStart, emu.memCallbackType.ppuWrite, PPU_PAGE+START_PPU_ADDR, PPU_PAGE+START_PPU_ADDR+1);
    startFrame = getCurrentFrame();
    emu.saveSavestateAsync(SAVESTATE_NO);
    emu.log("Enter to PPU zone. Frame: "..tostring(startFrame));
    emu.resetAccessCounters();
end
  
function onWritePPUZoneEnd()
    local accessCounters = emu.getAccessCounters(emu.counterMemType.prgRom, emu.counterOpType.read);
    accessCounters = table.filter(accessCounters, function(val, key, index) return val > 0 end);
    sortedAddresses = getSortedAddresses(accessCounters);
    foundAddressesCount = #sortedAddresses;
    if foundAddressesCount > 0 then
        --remove callbacks
        emu.removeMemoryCallback(callbackEnd, emu.memCallbackType.ppuWrite, PPU_PAGE+END_PPU_ADDR, PPU_PAGE+END_PPU_ADDR+1);
        endFrame = getCurrentFrame();
        emu.log("Exit from PPU zone. Frame: "..tostring(endFrame));
        --print results
        emu.log("Found "..tostring(foundAddressesCount).." addresses");
        local rangeBegin, rangeEnd = sortedAddresses[1], sortedAddresses[1];
        for i = 2, foundAddressesCount do
            local addr = sortedAddresses[i];
            if rangeEnd+1 == addr then
               rangeEnd = addr; 
            else
               --if rangeBegin != 0 then
               emu.log("addresses: "..hex(rangeBegin).."-"..hex(rangeEnd).." reads count: "..tostring(accessCounters[rangeEnd]));
               rangeBegin = addr;
               rangeEnd = addr;
            end;
            --emu.log("addresses: "..hex(addr).." reads count: "..tostring(accessCounters[addr]));
        end;
        emu.log("addresses: "..hex(rangeBegin).."-"..hex(rangeEnd).." reads count: "..tostring(accessCounters[rangeEnd]));
     end;
    startMakingScreenshots(sortedAddresses);
end;
  
function startMakingScreenshots()
  emu.log("Start making screenshots");
  currentCorruptIndex = 1;
  screenshotDone = true;
  callbackScreenshot = emu.addEventCallback(waitAndMakeScreenshot, emu.eventType.endFrame);
end;

function waitAndMakeScreenshot()
    if screenshotDone then
        if currentCorruptIndex >= foundAddressesCount then
           emu.removeEventCallback(callbackScreenshot, emu.eventType.endFrame); 
           emu.log("Work complete!");
        else
            local addr = sortedAddresses[currentCorruptIndex];
            emu.log("Current address: "..hex(addr));
            emu.revertPrgChrChanges();
            emu.write(addr, CORRUPT_VALUE, emu.memType.prgRom);
            emu.loadSavestateAsync(SAVESTATE_NO);
            screenshotDone = false;
        end
    else
      if getCurrentFrame() > (endFrame+WAIT_TO_SCREENSHOT) then
           screenshotDone = true;
           local ppuData = getPpuPage();
           local screenData = emu.takeScreenshot();
           if not hashes[ppuData] then
               hashes[ppuData] = true;
               local addr = sortedAddresses[currentCorruptIndex];
               local fname = emu.getScriptDataFolder().."/"..hex(addr)..".png";
               save(fname, screenData);
               emu.log("Make new screenshot");
           else
               emu.log("Skip, ppu not changed");
           end;
           --next address
           currentCorruptIndex = currentCorruptIndex + 1;
      end;
    end;
end;

callbackStart = emu.addMemoryCallback(onWritePPUZoneStart, emu.memCallbackType.ppuWrite, PPU_PAGE+START_PPU_ADDR, PPU_PAGE+START_PPU_ADDR+1);
callbackEnd = emu.addMemoryCallback(onWritePPUZoneEnd, emu.memCallbackType.ppuWrite, PPU_PAGE+END_PPU_ADDR, PPU_PAGE+END_PPU_ADDR+1);

