#script for compare two cdl files produces by FCEUX code-data log and show difference between logged data by ranges
name1 = "file1.cdl"
name2 = "file2.cdl"
with open(name1, "rb") as f:
    d1 = f.read()
with open(name2, "rb") as f:
    d2 = f.read()
d1 = [ord(x) for x in d1]
d2 = [ord(x) for x in d2]
firstAddr, lastAddr = -1, -1
for addr, (v1, v2) in enumerate(zip(d1,d2)):
    bitMask1 = v1 & 0x22
    bitMask2 = v2 & 0x22
    if bitMask1 != bitMask2:
        if firstAddr == -1:
            firstAddr = lastAddr = addr
        else:
            lastAddr = addr
    else:
        if firstAddr !=-1:
            print hex(firstAddr+16),"-",hex(lastAddr+16), "(",(lastAddr - firstAddr + 1)," bytes)"
        firstAddr = -1
        lastAddr = -1