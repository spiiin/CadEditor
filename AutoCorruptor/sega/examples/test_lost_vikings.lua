startAddr = 0xFF2B60 -- begin
lineOffset = 60*10
val = 0xC0;
savestate.load(1);

function fillLine(lineNo)
  for mem = startAddr + lineOffset + lineNo*60, startAddr + lineOffset + lineNo*60 + 32 - 1, 2 do
    memory.writebyte(mem, 3);
    memory.writebyte(mem+1, val);
    val = val + 1;
  end;
end

fillLine(1);
fillLine(2);
fillLine(3);
fillLine(4);
