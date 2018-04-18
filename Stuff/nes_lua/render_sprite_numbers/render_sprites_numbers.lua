--Script for render tile numbers on sprites,
-- using breakpoint on write at OAM (Object Attribute Memory), as only available method to read sprite info from lua
--Rom: any
--Author: spiiin

require("gd")

mode = 0
MAX_MODE = 4
activeIndex = nil

keyTimer = 0
MAX_KEY_TIME = 12

dmaAddr = 0x4014
local gds = {}

for i = 0, 255 do
  gds[i] = gd.createFromPng("images/"..string.format("%02X", i)..".png"):gdStr()
end

function renderTilesNumbers()
    dmaVal = memory.readbyte(dmaAddr)
    --print(dmaVal)
    local values = memory.readbyterange(dmaVal*256, 256)
    for i = 1, 64 do
      spriteStartIndex = (i-1)*4+1
      tileNo = string.byte(values:sub(spriteStartIndex+1,spriteStartIndex+1))
      tileX = string.byte(values:sub(spriteStartIndex+3,spriteStartIndex+3))
      tileY = string.byte(values:sub(spriteStartIndex+0,spriteStartIndex+0))
      tileAttr = string.byte(values:sub(spriteStartIndex+2,spriteStartIndex+2))
      --gui.drawbox(tileX, tileY, tileX+8, tileY+8, "#ffffff");
      --gui.text(tileX, tileY, string.format('%02X',tileNo), "#00ff00", "#ffffff")
      if gDumpToConsole then
        if tileY < 0xF0 then
          print("i: "..i..", tileX: "..string.format('%02X',tileX)..", tileY: "..string.format('%02X',tileY)..", tileNo: "..string.format('%02X',tileNo)..", tileAttr: "..string.format('%02X',tileAttr))
        end
      end
      
      if mode == 0 then
          activeIndex = tileNo
      elseif mode == 1 then
          activeIndex = tileAttr
      elseif mode == 2 then
          activeIndex = tileX
      else
          activeIndex = tileY
      end
      gui.gdoverlay(tileX, tileY, gds[activeIndex])
    end
    
    if gDumpToConsole then 
      gDumpToConsole = false
    end
end

memory.registerwrite(dmaAddr, renderTilesNumbers)

function printMode()
  if mode == 0 then
      print("Current mode: tileNo")
  elseif mode == 1 then
      print("Current mode: tileAttr")
  elseif mode == 2 then
      print("Current mode: tile X")
  else
      print("Current mode: tile Y")
  end
end

printMode()
while true do
    local t = input.get()
    keyTimer = keyTimer - 1
    if t["E"] then
        if keyTimer < 0 then
          mode = mode + 1
          if mode >= MAX_MODE then mode = 0 end
          printMode()
          keyTimer = MAX_KEY_TIME
        end
    end
    
    if t["Q"] then
      if keyTimer < 0 then
        gDumpToConsole = true
        keyTimer = MAX_KEY_TIME
      end
    end
    FCEU.frameadvance();
end;