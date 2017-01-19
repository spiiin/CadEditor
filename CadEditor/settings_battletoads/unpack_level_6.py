from os.path import abspath
START_ADDR = 0x38848
FNAME = "../Battletoads (U) [!].nes"
OUT_FNAME = "level6.bin"
SIZE = 16*96

with open(abspath(FNAME), "rb") as f:
  data = f.read()
  
ans = []
curAddr = START_ADDR
while len(ans) < SIZE:
  v = ord(data[curAddr])
  if v > 0xB0:
    print hex(curAddr), hex(v)
    curAddr += 1
    continue
  if v < 0x80:
    ans.append(data[curAddr])
    curAddr += 1
  else:
    repearVal = chr(v - 0x80)
    repeatCount = ord(data[curAddr+1])
    ans.extend([repearVal]*repeatCount)
    curAddr += 2
    
with open(abspath(OUT_FNAME), "wb") as f:
  f.write("".join(ans))

  