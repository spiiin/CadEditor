--Script change screen buffer
--Emulator: Mesen 0.9.5
--Rom: any
--Author: spiiin

function getCurrentFrame()
    return emu.getState()["ppu"]["frameCount"]
end

function makeRed()
  local buffer = emu.getScreenBuffer()
  for i, p in pairs(buffer) do 
      buffer[i] = buffer[i] & 0xFFFF0000
  end
  emu.setScreenBuffer(buffer)
end


function onlyOddLines()
  local width = 256
  local buffer = emu.getScreenBuffer()
  for i, p in pairs(buffer) do
      local x = i % width
      local y = i // width
      if y % 2 == 0 then
          buffer[i] = 0x00000000
      end
  end
  emu.setScreenBuffer(buffer)
end

function blink()
   local buffer = emu.getScreenBuffer()
   local frame = getCurrentFrame()
   local blinkPeriod = 24*5
   local blinkTime = 10
   if frame % blinkPeriod < blinkTime then
       for i, p in pairs(buffer) do 
          buffer[i] = 0x0000FF00
       end
   end
   emu.setScreenBuffer(buffer)
end

function waves()
  local frame = getCurrentFrame()
  local maxWave = 40
  local waveSpeed = 4
  local anim = (frame // waveSpeed % maxWave)
  local animTable = {
    -10,-9,-8,-7,-6,-5,-4,-3,-2,-1,
      0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
     10, 9, 8, 7, 6, 5, 4, 3, 2, 1,
      0,-1,-2,-3,-4,-5,-6,-7,-8,-9,
  }
  anim = animTable[anim+1]
  local width = 256
  local buffer = emu.getScreenBuffer()
  local buffer2 = emu.getScreenBuffer()
  for i, p in pairs(buffer) do
      local x = i % width
      local y = i // width
      if y % 2 == 0 then
          buffer[i] = buffer2[i+anim]
      else
          buffer[i] = buffer2[i-anim]
      end
  end
  emu.setScreenBuffer(buffer)
end

currentShader = nil

function checkKeys()
    local press0 = emu.isKeyPressed("0")
    local press1 = emu.isKeyPressed("1")
    local press2 = emu.isKeyPressed("2")
    local press3 = emu.isKeyPressed("3")
    local press4 = emu.isKeyPressed("4")
    local needToRemoveOldShader = press0 or press1 or press2 or press3 or press4
    --remove old shader
    if currentShader and needToRemoveOldShader then
       emu.removeEventCallback(currentShader, emu.eventType.endFrame); 
    end
    
    if press0 then
        emu.displayMessage("Info", "No shader selected")
    elseif press1 then
        emu.displayMessage("Info", "Shader Red selected")
        currentShader = emu.addEventCallback(makeRed, emu.eventType.endFrame);
    elseif press2 then
        emu.displayMessage("Info", "Shader OddLines selected")
        currentShader = emu.addEventCallback(onlyOddLines, emu.eventType.endFrame);
    elseif press3 then
        emu.displayMessage("Info", "Shader Blink selected")
        currentShader = emu.addEventCallback(blink, emu.eventType.endFrame);
    elseif press4 then
        emu.displayMessage("Info", "Shader Waves selected")
        currentShader = emu.addEventCallback(waves, emu.eventType.endFrame);
    end 
end

emu.addEventCallback(checkKeys, emu.eventType.inputPolled);
local infoStr = "Press keys from 0 to 4 to change shaders"
emu.displayMessage("Info", infoStr)
emu.log(infoStr)