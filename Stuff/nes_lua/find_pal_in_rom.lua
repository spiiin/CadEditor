--Script for find current loaded background pallete in ROM file
--Rom: any
--Author: spiiin

ROM_LEN = rom.readbyte(4) * 0x4000 + 0x10
pal = ppu.readbyterange(0x3F00, 0x16)

for curAddr = 0x10, ROM_LEN-0x10 do
  gotoNextIter = false
  for curOffset = 1, 16 do
    b = rom.readbyte(curAddr + curOffset)
    if b ~= pal:sub(curOffset,curOffset):byte() then
      gotoNextIter = true
      break
    end
  end
  if not gotoNextIter then
    print ("Pal address:", (string.format("%05X",curAddr+1)))
  end
end