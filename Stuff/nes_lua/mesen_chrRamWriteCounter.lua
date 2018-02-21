--Script for writing to CHR-RAM count per frame
--Emulator: Mesen 0.9.4
--Rom: any with CHR-RAM
--Author: spiiin

writeToChr0 = 0;
writeToChr1 = 0;

function onStartFrame()
    writeToChr0 = 0;
    writeToChr1 = 0;
end

function onEndFrame()
    emu.log("Frame ended, chr0 writes: "..writeToChr0..", chr1 writes: "..writeToChr1);
    writeToChr0 = 0;
    writeToChr1 = 0;
end

function onChrWrite(address, value)
    if (address < 0x1000) then
      writeToChr0 = writeToChr0 + 1;
    else
      writeToChr1 = writeToChr1 + 1;
    end;
end

emu.addEventCallback(onStartFrame, emu.eventType.startFrame);
emu.addEventCallback(onEndFrame, emu.eventType.endFrame);
emu.addMemoryCallback(onChrWrite, emu.memCallbackType.ppuWrite, 0x0000, 0x1FFF);

emu.displayMessage("Script", "Script loaded.")