-- Script for corrupting ROMs

require("auxlib");

local save1Label, save2Label, save1Tb, save2Tb

START_CORRUPTING = false
STOP_CORRUPTING = false
MAKE_SCREESHOTS = false

START_ADDR = 0x10
END_ADDR   = rom.readbyte(4) * 0x4000 + 0x10
CUR_ADDR = START_ADDR
STEP = 1
CDL_FILE = "diff.cdl"
WRITE_VALUE = 0x33

CHECK_NAMETABLE = { true, false, false, false } -- need to check nametables

FRAME_FOR_SCREEN = 0
cdlData = ""

--------------------------------------------------------
function atSave(i)
    --print("Save to slot: "..tostring(i))
    local curSave = savestate.create()
    savestate.save(curSave)
    savestate.load(savestate.create(2))
    frame1 = emu.framecount()
    savestate.load(curSave) -- need to reload for right value of framecount() for savestate
    savestate.load(savestate.create(3))
    frame2 = emu.framecount()
    savestate.load(curSave)
    print("Save 1-2 frames:"..tostring(frame1).."-"..tostring(frame2))
    save1Tb.value = tostring(frame1)
    save2Tb.value = tostring(frame2)
    if i == 1 then
        cdlog.docdlogger()
        cdlog.resetcdlog()
        cdlog.startcdlogging()
    elseif i == 2 then
        cdlog.pausecdlogging()
        cdlog.savecdlogfileas(CDL_FILE)
    end
end

savestate.registersave(atSave)
--------------------------------------------------------
function readCdlMask(CUR_ADDR)
    return bit.band(string.byte(cdlData:sub(CUR_ADDR+0x10,CUR_ADDR+0x10)), 0x22)
end

function stop()
    STOP_CORRUPTING = true
    --emu.loadrom("RELOAD_ROM")
    emu.speedmode("normal")
end

function start()
    print("Start corrupter")
    CUR_ADDR = START_ADDR
    
    FRAME_FOR_SCREEN = tonumber(save2Tb.value)
    
    cdlFile = assert(io.open(CDL_FILE, "rb"))
    cdlData = cdlFile:read("*all")
    shas = {}
    CYCLES = 0
    emu.speedmode("maximum");

    lastValue = rom.readbyte(START_ADDR)
    savestate.load(savestate.create(2))
    needReturn = true
        
    while (true) do
        if emu.framecount() > FRAME_FOR_SCREEN then
            if needReturn then
                for i = 1,4 do
                    if CHECK_NAMETABLE[i] then
                        local ppuNametable = ppu.readbyterange(0x2000 + 0x400*(i-1), 0x400)
                        local hash  = gethash(ppuNametable, string.len(ppuNametable));
                        if (not shas[hash]) then
                          print("Found new screen at addr "..string.format("%05X", CUR_ADDR).." hash "..tostring(hash));
                          local fname = string.format("snaps/%05X", CUR_ADDR)..".png";
                          gui.savescreenshotas(fname);
                          emu.frameadvance();
                          shas[hash] = true;
                          break
                        end;
                    end;
                end;
                rom.writebyte(CUR_ADDR, lastValue);
                needReturn = false;
            end;
            CUR_ADDR = CUR_ADDR + STEP;
            CYCLES = CYCLES + 1;
           
            if (CYCLES % 100) == 0 then
                print(string.format("%05X", CUR_ADDR));
            end;
            
            if (CUR_ADDR > END_ADDR) then
                gui.text(20,20, string.format("WORK COMPLETE!"));
                emu.pause();
            end;
            
            if STOP_CORRUPTING then
                print("Stopped")
                STOP_CORRUPTING = false
                break
            end;
            
            local cdlCharMask = readCdlMask(CUR_ADDR)
            
            if cdlCharMask ~= 0 then
                lastValue = rom.readbyte(CUR_ADDR);
                rom.writebyte(CUR_ADDR, WRITE_VALUE);
                needReturn = true;
                savestate.load(savestate.create(2))
            end;
            
            --fast check of cdl data
            while cdlCharMask == 0 and CUR_ADDR < END_ADDR do
                CUR_ADDR = CUR_ADDR + STEP
                cdlCharMask = readCdlMask(CUR_ADDR)
            end
        end;
        emu.frameadvance();
    end;
end

--------------------------------------------------------
function makeScreenshotsEveryFrame()
    endFrame = tonumber(save2Tb.value)
    savestate.load(savestate.create(2))
    while emu.framecount() <= endFrame do
        print("Screenshot at frame:"..string.format("%05d", emu.framecount()));
        local fname = string.format("snaps/frame_%05d", emu.framecount())..".png";
        gui.savescreenshotas(fname);
        emu.frameadvance();
    end
end

function createWindow()
	runButton = iup.button{title="Run(E)"};
	runButton.action = function(self) 
			START_CORRUPTING = true
	end
  
  stopButton = iup.button{title="Stop"};
	stopButton.action = function(self)
      stop()
	end
  
  makeScreenBetween = iup.button { title = "Make screenshots at every frame"}
  makeScreenBetween.action = function(self)
      MAKE_SCREESHOTS = true
  end
  
  nameTable0toolge = iup.toggle{title="Check nametable 0", VALUE="ON"}
  nameTable0toolge.action = function(self)
      CHECK_NAMETABLE[1] = self.value == "ON"
  end
  
  nameTable1toolge = iup.toggle{title="Check nametable 1", VALUE="OFF"}
  nameTable1toolge.action = function(self)
      CHECK_NAMETABLE[2] = self.value == "ON"
  end
  
  nameTable2toolge = iup.toggle{title="Check nametable 2", VALUE="OFF"}
  nameTable2toolge.action = function(self)
      CHECK_NAMETABLE[3] = self.value == "ON"
  end
  
  nameTable3toolge = iup.toggle{title="Check nametable 3", VALUE="OFF"}
  nameTable3toolge.action = function(self)
      CHECK_NAMETABLE[4] = self.value == "ON"
  end
  
  save1Label = iup.label{title="Save 1 frame(start):"}
  save2Label = iup.label{title="Save 2 frame(stop):"}
  save1Tb    = iup.text{value="0", readonly="yes", active="no"}
	save2Tb    = iup.text{value="0", readonly="no"}
	dialogs = dialogs + 1;
	handles[dialogs] = iup.dialog{
			title="Autocorrupter v5",
		  iup.vbox{
        iup.vbox{
          iup.frame{
            title="",
            iup.vbox{
              iup.frame{
                title = "Frames",
                iup.vbox{
                  iup.hbox { save1Label, save1Tb },
                  iup.hbox { save2Label, save2Tb },
                  makeScreenBetween
                }
              },
              iup.frame{
                title = "Check conditions",
                iup.vbox{
                  nameTable0toolge,
                  nameTable1toolge,
                  nameTable2toolge,
                  nameTable3toolge
                }
              },
              iup.hbox { runButton, stopButton }
            }
          }
        }
		  }, -- /vbox
      gap="5",
      alignment="ARIGHT",
      margin="5x5"
		};
	
	handles[dialogs]:show();

end

createWindow() --create gui
atSave(9) --for update frame values

while true do
    local t = input.get()
    if t["E"] then
        START_CORRUPTING = true
    end
    
    if START_CORRUPTING then
        START_CORRUPTING = false
        start()
    end
    
    if MAKE_SCREESHOTS then
        MAKE_SCREESHOTS = false
        makeScreenshotsEveryFrame()
    end
    FCEU.frameadvance();
end;

gui.popup("Script closed");