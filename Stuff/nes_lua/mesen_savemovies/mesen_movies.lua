local json = require(".json")

movieTable = {}
local movieFileName = emu.getScriptDataFolder().."/movie.json"
playing = false

function getCurrentFrame()
    return emu.getState()["ppu"]["frameCount"]
end

function onInput()
    local frame = getCurrentFrame()
    --if frame <= 1 then
    --    return
    --end

    if not playing then
        local input = emu.getInput(0)
        movieTable[frame] = input
    else
        --emu.log("Frame: ".. tostring(frame))
        local input = movieTable[frame]
        if input == nil then
            playing = false
            emu.log("Movie ended")
        else
            emu.setInput(0, input)
        end
    end
end
      
function onEndFrame()
    if emu.isKeyPressed("1") then
        emu.log("Save movie")
        saveMovie()
    end
    
    if emu.isKeyPressed("2") then
        emu.log("Load movie")
        loadMovie()
    end
    
    if emu.isKeyPressed("3") then
        emu.log("Replay movie")
        replayMovie()
    end
      
    if emu.isKeyPressed("4") then
        emu.log("Stop playing")
        stopMovie()
    end
end
  
function saveMovie()
    local d = json.encode(movieTable)
    local file = io.open(movieFileName, "wb")
    file:write(d)
    file:close()
end

function loadMovie()
    local file = io.open(movieFileName, "rb")
    if file ~= nil then
        local d = file:read("*all")
        file:close()
        movieTable = json.decode(d)
        emu.log("Loaded ".. tostring(movieTable).." frames")
    else
        movieTable = {}
    end
end
    
function replayMovie()
    playing = true
    emu.reset()
end
  
function stopMovie()
    playing = false
end

loadMovie()
playing = false
emu.addEventCallback(onInput, emu.eventType.inputPolled)
emu.addEventCallback(onEndFrame, emu.eventType.endFrame);

emu.log("Press 1 to save movie")
emu.log("Press 2 to load movie")
emu.log("Press 3 to replay movie")
emu.log("Press 4 to stop movie")