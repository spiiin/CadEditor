def cutPal(dumpName):
    with open(dumpName, "rb") as f:
        d = f.read()
    with open("pal.bin", "wb") as f:
        f.write(d[0x3f00:0x3f10])
    with open("chr.bin", "wb") as f:
        f.write(d[0x0:0x1000])
   
ROM_NAME = r"mickey_5.bin"
cutPal(ROM_NAME)