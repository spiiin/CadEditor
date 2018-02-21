--Script make screenshots every frame (for helping make gifs)
--  files will be stored at %MESEN_DIR%\LuaScriptData\mesen_makeScreensEveryFrame\
--Emulator: Mesen 0.9.4
--Rom: any
--Author: spiiin

KEY_TO_START = "Q"  --key to start and stop screnshoting

frameToScreen = 0
startShoting = false;

function save(fname, data)
    file = io.open(fname, "wb");
    file:write(data);
    file:close();
end

function onEndFrame()
    local keyPressed = emu.isKeyPressed(KEY_TO_START)      --check if key pressed
    
    if startShoting then
        if frameToScreen >= 0 then
          local screenShotData = emu.takeScreenshot();
          local fname = emu.getScriptDataFolder().."/screen"..string.format("%05d", frameToScreen)..".png";
          save(fname, screenShotData);
          emu.log("Screenshot save to file: "..fname);
        end;
        frameToScreen = frameToScreen + 1;
        if keyPressed then
            --minimum 10 frames to shot - so user have time to release button
            startShoting = frameToScreen < 10;
            frameToScreen = -10;
            emu.log("Stop screenshoting ");
        end
    else
        --wait 10 frames between last stopping of screenshoting
        if frameToScreen < 0 then
           frameToScreen = frameToScreen + 1;
        else
           startShoting = keyPressed;
        end
    end
end

emu.addEventCallback(onEndFrame, emu.eventType.endFrame);
