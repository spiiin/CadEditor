--Emulator: Bizhawk 2.2.2
--ROM     : Any GameBoy ROM
--Author  : Spiiin


--This is lua script for finding bytes, related to level data (and other VRAM changes).
--User need to prepare 2 savestates - before level loaded and after. Autocorrupter use this saves to check, if level loading code already executed or no.
--Script changes ROM per byte and make screenshot, if ROM change led to change in VRAM.
--After that, user can analyze screenshots and, possible, understand level format.
--Script can works a long time, bizhawk lua api can't check, what bytes exactly read from ROM between saves, so it need to check all bytes.

CORRUPT_VALUE = 0x33

START_ADDR = 0x150
CUR_ADDR = START_ADDR

END_FRAME = 0

PPU_START = 0x9800
PPU_END   = 0x9D0A

client.speedmode(5000)

--load 2nd save for get END_FRAME from it
savestate.loadslot(2)
END_FRAME = emu.framecount()

savestate.loadslot(1)
console.log("Start frame:" .. tostring(emu.framecount()))
console.log("End frame:" .. tostring(END_FRAME))

function getHash()
  return memory.hash_region(PPU_START, PPU_END - PPU_START)
end

--hashes of VRAMs, that's already checked
shas = {}

--start corrupting
ROM_SIZE = 0x20000 --hardcode, must be read from ROM at address 0x148
while CUR_ADDR < ROM_SIZE do
  --save previous value and write corrupted value
  prevValue = memory.readbyte(CUR_ADDR, "ROM")
  memory.writebyte(CUR_ADDR, CORRUPT_VALUE, "ROM")
  
  --load savestate 1
  savestate.loadslot(1)
  --and wait until game reach savestate 2
  while emu.framecount() < END_FRAME do
    emu.frameadvance()
  end
  
  local hash = getHash()
  --check is VRAM hash is changed
  if not shas[hash] then
      --make screenshot
      print("Found new screen at addr "..string.format("%05X", CUR_ADDR).." hash "..tostring(hash))
      local fname = string.format("snaps/%05X", CUR_ADDR)..".png";
      client.screenshot(fname)
      shas[hash] = true
  end
  
  --return uncorrupted value
  memory.writebyte(CUR_ADDR, prevValue, "ROM")
  --and continue with next byte
  CUR_ADDR = CUR_ADDR + 1
end

--return emulator default speed and print end message
client.speedmode(100)
console.log("Finish!")