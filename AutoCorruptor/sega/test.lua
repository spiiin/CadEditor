startAddr = 0xFF0000 --start addr in RAM
val = 0x0 
savestate.load(1);

for mem = startAddr+0x0, startAddr+0x1000, 1 do
  memory.writebyte(mem, val);
  val = val + 1;
end;