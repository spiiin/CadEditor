DIRECT_ROM_NAME = "rom/tmnt2"
ADDR_TO_CORRUPT = 0xD70D

def makeCorruptionSingle(inp):
  f = open(inp+".nes", "rb")
  d = f.read()
  f.close()
  it = 0
  for x in xrange(256):
    cd = d[:ADDR_TO_CORRUPT] + chr(x) + d[ADDR_TO_CORRUPT+1:] #corrupt single byte at screen
    foname= inp+"%04d"%it+".nes"
    print foname
    fo = open(foname, "wb")
    fo.write(cd)
    fo.close()
    it+=1
    
makeCorruptionSingle(DIRECT_ROM_NAME)