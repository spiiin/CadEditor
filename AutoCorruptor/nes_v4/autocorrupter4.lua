START_ADDR = 0x10
END_ADDR   = rom.readbyte(4) * 0x4000 + 0x10
CUR_ADDR = START_ADDR
STEP = 1
CDL_FILE = "diff.cdl"
WRITE_VALUE = 0x33

FRAME_FOR_SCREEN = 0
cdlData = ""
--------------------------------------------------------
function atSave(i)
    print(i)
    local curSave = savestate.create()
    savestate.save(curSave)
    savestate.load(savestate.create(2))
    frame1 = emu.framecount()
    savestate.load(curSave) -- need to reload for right value of framecount() for savestate
    savestate.load(savestate.create(3))
    frame2 = emu.framecount()
    savestate.load(curSave)
    print("save:"..tostring(frame1).."-"..tostring(frame2))
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

function start()
    savestate.load(savestate.create(3))
    FRAME_FOR_SCREEN = emu.framecount()
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
                local ppuNametable = ppu.readbyterange(0x2000, 0x400)
                local hash  = gethash(ppuNametable, string.len(ppuNametable));
                if (not shas[hash]) then
                  print("Found new screen at addr "..tostring(CUR_ADDR).." hash "..tostring(hash));
                  local fname = string.format("snaps/%05X", CUR_ADDR)..".png";
                  gui.savescreenshotas(fname);
                  emu.frameadvance();
                  shas[hash] = true;
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
while (true) do
    local t = input.get()
    if t["E"] then
        break
    end
    emu.frameadvance();
end
print "!!!"
start()