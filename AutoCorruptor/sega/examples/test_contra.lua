-- ContraHC 
-- layerA : 0xFF7A00 + 29-30, +129-130 - 2 lines at 2nd screen
startAddr = 0xFF7A00
val = 0x0
savestate.load(1);

for mem = startAddr+0x29,startAddr+0x30 ,1 do
  memory.writebyte(mem, val);
  val = val + 1;
end;

for mem = startAddr+0x129,startAddr+0x130 ,1 do
  memory.writebyte(mem, val);
  val = val + 1;
end;
--memory.writebyte(0xFF5028, 78);