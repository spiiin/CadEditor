"""Ida script that find all RNC archives in file and create arrays for it.
Script make some checks before create array archive for avoid false positive
(There are "RNC" strings can be found in game code, that check if it is really RNC array)
Script also rename arrays as rnc_ADDRESS.
"""
from idaapi import *
import struct 
sizeBuf = GetManyBytes(0x1A0, 8) #get ROM size from file header
romStart, romEnd = struct.unpack(">II", sizeBuf) 
buf = GetManyBytes(romStart, romEnd+1)

i = 0 
while True: 
    i = buf.find("RNC", i+1)
    if i == -1:
        break
    rncVersion, = struct.unpack(">B", buf[i+3])
    if rncVersion > 2:
        continue
    #todo check special version for RNC0, that not packed and not stored sizeUnpacked
    size, = struct.unpack(">I", buf[i+8 : i+12]) 
    sizeUnpacked, = struct.unpack(">I", buf[i+4 : i+8])
    if size > sizeUnpacked:
        continue
    #todo add checking packed size CRC
    print "Found RNC at address", hex(i), size, sizeUnpacked
    arrSizeInWords = (size + 0x12) / 2
    MakeUnknown(i, arrSizeInWords*2, 0)
    MakeWord(i)
    MakeArray(i, arrSizeInWords)
    MakeNameEx(i, "rnc_"+ hex(i)[2:], 0)