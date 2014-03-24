-- layerA 
startAddr = 0xFF0200
zeroVal = 0x20
val = 0x0
savestate.load(1);

for mem = startAddr+0x00,startAddr+0x80-1 ,2 do
  memory.writebyte(mem+1, zeroVal)
  memory.writebyte(mem, val);
  val = val + 1;
end;