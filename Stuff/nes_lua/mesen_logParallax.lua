--Script for render scanlines, where horizontal scroll changed
--Emulator: Mesen 0.9.4
--Rom: any
--Author: spiiin

PPUSCROLL = 0x2005;

colorCode = 0x4000FF00;

function onStartFrame()
  emu.log("New frame");
end

function onEndFrame()
  emu.log("End frame");
end

function onIrq()
  emu.log("Irq");
end

function hex(arg)
  return string.format("%02x", arg)
end

function onScroll(address, value)
  local state = emu.getState();
  emu.log("Scrolling change. Scanline: "..state.ppu.scanline.." Value:"..value);
  local color = colorCode + state.ppu.scanline;
  emu.drawLine(0, state.ppu.scanline, 256, state.ppu.scanline, color, 1)
end;

emu.addEventCallback(onStartFrame, emu.eventType.startFrame);
emu.addEventCallback(onEndFrame, emu.eventType.endFrame);
emu.addEventCallback(onIrq, emu.eventType.irq);
emu.addMemoryCallback(onScroll, emu.memCallbackType.cpuWrite, PPUSCROLL, PPUSCROLL+1);

--Display a startup message
emu.displayMessage("Script", "Script loaded.")