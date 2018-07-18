# Module for NES Duck Tales 2 videomemory compression and decompression.

import itertools

#----decompress
def bankToPhysAddr(addr, bank):
  if bank == 2:
    return addr + 0x10
  elif bank == 3:
    return addr + 0x4010
  else:
    return -1
    
def unpack(d, addr):
  result = []
  repeatByte = d[addr]
  addr+=1
  while not(d[addr]==repeatByte and d[addr+1]==0):
    if d[addr] == repeatByte:
      repeatCount = d[addr+1]
      repeatVal  = d[addr+2]
      result.extend([repeatVal]*repeatCount)
      addr+=3
    else:
      result.append(d[addr])
      addr+=1
  return result
  
def fullUnpack(d, unpackTuples):
  result = [0,]*0x1000
  for (addr, bank, dstAddr) in unpackTuples:
    res = unpack(d, bankToPhysAddr(addr, bank)-1)
    result[dstAddr:dstAddr+len(res)] = res
  return result
  
fullUnpack1 = [
  (0xBD74, 2, 0x000),
  (0xAE62, 3, 0x400),
  (0x813E, 3, 0x800),
  (0x848B, 3, 0xC00)
  ]
  
fullUnpack2 = [
  (0x8851,3, 0),
  (0xAE62,3, 0x400),
  (0xB692,2, 0x800),
  (0xB9D5,2, 0xC00)
  ]
  
fullUnpack3 = [
  (0xA109,2, 0),
  (0xAE62,3, 0x400),
  (0xAF6F,2, 0x800),
  (0xB2EC,2, 0xC00),
  (0xA7DC,3, 0x700)
  ]
  
fullUnpack4 = [
  (0xA109,2, 0),
  (0xAE62,3, 0x400),
  (0xA4F6,2, 0x800),
  (0xA858,2, 0xC00),
  (0xA72D,3, 0x700)
  ]
  
fullUnpack5 = [
  (0x9632,2, 0),
  (0xAE62,3, 0x400),
  (0x99EF,2, 0x800),
  (0x9D5E,2, 0xC00),
  ]
  
fullUnpack6 = [
  (0x9632,2, 0),
  (0xAE62,3, 0x400),
  (0x9450,2, 0x800),
  (0x9D5E,2, 0xC00),
  ]
  
#ddd = fullUnpack(d, fullUnpack1)
#f = open(OUT_NAME, "wb")
#ddd1 = "".join(map(chr,ddd))
#f.write(ddd1)
#f.close()

#----compress

def findUnused(strData):
  for x in xrange(255, 0, -1):
    if strData.find(chr(x))==-1:
      return x
  return -1

def compress(repeatSym, data):
  res = [chr(repeatSym)]
  grps = ["".join(grp) for num, grp in itertools.groupby(data)]
  for grp in grps:
    if len(grp)<3:
      res.extend(grp)
    else:
      res.append(chr(repeatSym))
      res.append(chr(len(grp)))
      res.append(grp[0])
  res.append(chr(repeatSym))
  res.append(chr(0))
  return res
  
#test
#strData = "".join(map(chr,ddd[:1024]))
#cd = compress(findUnused(strData), strData)
#upkTest = unpack(map(ord, cd), 0)
#upkTest == ddd[0:1024]

def main(romName, outName):
  f = open(romName, "rb")
  d = f.read()
  f.close()
  d = map(ord, d)
  f = open(outName, "wb")
  for fullUnpackRec in (fullUnpack1, fullUnpack2, fullUnpack3, fullUnpack4, fullUnpack5, fullUnpack6):
    ddd =  fullUnpack(d, fullUnpackRec)
    f.write("".join(map(chr, ddd)))
  f.close()
  
#-----------------------------------------------------------------------------------------------------
if __name__ == "__main__":
  main("Duck Tales 2.nes", "VideoBack_DT2.bin")


