def findPointer(romName, romAddr):
    print "--- search", hex(romAddr), "---"
    addr = (romAddr - 0x10) & 0xFFFF
    hiByte = (addr & 0xFF00) >> 8
    loByte = addr & 0x00FF
    
    with open(romName, "rb") as f:
        d = f.read()
        
    prefixes = [0x80, 0xA0, 0xC0, 0xE0]
    for prefix in prefixes:
        preHiByte = (hiByte & 0x0F) | prefix
        strToFind = chr(loByte) + chr(preHiByte)
        startIndex = d.find(strToFind)
        while startIndex!=-1:
            print hex(startIndex), hex(preHiByte << 8 | loByte)
            startIndex = d.find(strToFind, startIndex+1)
   
ROM_NAME = r"d:\DEV\MYGIT\CadEditor\Tom & Jerry (and Tuffy) (J).nes"
ROM_ADDRS = [0x84AF, 0x8153]
for romAddr in ROM_ADDRS: 
    findPointer(ROM_NAME, romAddr)