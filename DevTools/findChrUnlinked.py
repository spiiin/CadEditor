#!/usr/bin/env python3
#Script for search unlinked dumps in repository
#Useful for find CHR and PAL dump copies, made for settings files

import os
import sys
import hashlib

chrExtensions = ("chr", "ppu", "pal")

def findUnlinked(parentFolder):
    notFoundChr = []
    for dirName, subdirs, fileList in os.walk(parentFolder):
        print('Scanning %s...' % dirName)
        chrList = [f for f in fileList if any((f.find(ext)!=-1 for ext in chrExtensions))]
        csList = [f for f in fileList if f.find("Settings")!=-1]
        for chr in chrList:
            for cs in csList:
                with open(os.path.join(dirName,cs), "rt") as f:
                    lines = f.read()
                    if lines.find('"%s"'%chr) != -1:
                        break
            else:
                fullChrName = os.path.join(dirName, chr)
                print(fullChrName)
                notFoundChr.append(fullChrName)
    return notFoundChr
    
for chr in findUnlinked("../CadEditor/settings_nes"):
    print(chr)
     
