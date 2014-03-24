-- layerA 
startAddr = 0xFF0200
val = 0xC0
savestate.load(1);

for mem = startAddr+0x20,startAddr+0x20+0x40-1 ,1 do
  memory.writebyte(mem, val);
  val = val + 1;
end;