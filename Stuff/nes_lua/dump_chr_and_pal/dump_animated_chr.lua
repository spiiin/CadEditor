--Script for dump changed ppu CHR banks(for dumping chr-ram or animated chr-rom)
--Rom: any
--Author: spiiin

BANK_NO = 0  --dump first of second bank
DUMP_COUNT = 0

function save(fname, data)
    file = io.open(fname, "wb")
    file:write(data)
    file:close()
end

irqAddr = memory.readwordunsigned(0xFFFE)
shas = {}

function dumpData()
    local chrBank = ppu.readbyterange(0x1000*BANK_NO, 0x1000);
    local hash  = gethash(chrBank, string.len(chrBank));

    if (not shas[hash]) then
       save(string.format("chr_%03d.bin", DUMP_COUNT), chrBank);
       shas[hash] = true;
       DUMP_COUNT = DUMP_COUNT + 1;
           print(string.format("Dump %03d complete!", DUMP_COUNT));
    end
end


memory.registerexecute(irqAddr, dumpData)
while (true) do
  FCEU.frameadvance();
  dumpData()
end