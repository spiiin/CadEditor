--Script for showing hidden objects
--Rom: Duck Tales 2 NES (All versions)
--Author: spiiin 

--require math

local startCrystalActivityAddr = 0x400

function update()
    local camX = memory.readword(0x17)
    local scrY = memory.readbyte(0x9A)
    local OBJ_COUNT = 16
    for o = 0, OBJ_COUNT do
        local t = memory.readbyte(0x400+o)
        local objX = memory.readword(0x4B0+o, 0x4C0+o)
        local objY = memory.readbyte(0x4E0+o) - 8
        local objScrY = memory.readbyte(0x4F0+o)
        local dx = objX-camX
        local SCR_WIDTH = 256
        local BOX_SIZE = 6
        if t ~= 0 then
            if (dx > 0) and (dx < SCR_WIDTH) and (objScrY == scrY) then
              gui.rect(dx, objY, dx+BOX_SIZE, objY+BOX_SIZE, "#FF0000")
              gui.rect(dx+1, objY+1, dx+BOX_SIZE-1, objY+BOX_SIZE-1, "#FFFFFF")
            end;
        end;
    end;
    
    gui.text(1, 12, string.format("camX:%2d",camX))
    --gui.text(1, 20, string.format("objX:%2d",objX))
    --gui.text(1, 28, string.format("scrY:%2d",scrY))
end

while(true) do
    update()
    FCEU.frameadvance()
end