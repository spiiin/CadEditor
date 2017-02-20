--Script for find current loaded background pallete in ROM file
--Rom: any
--Author: spiiin

ROM_LEN = rom.readbyte(4) * 0x4000 + 0x10
romdata = rom.readbyterange(0x10, ROM_LEN)
paldata = ppu.readbyterange(0x3F00, 0x16)

v = 0
repeat
    v = romdata:find(paldata, v+1)
    print("Pal address:", v and string.format("%05X",v) or "-----")
until not v