#!/usr/bin/env python3
#Script for search duplicate files in repository
#Useful for find CHR and PAL dump copies, made for settings files

import os
import sys
import hashlib

EXCLUDE_NAMES = [".git", "Release"]

def hashfile(path, blocksize = 65536):
    afile = open(path, 'rb')
    hasher = hashlib.md5()
    buf = afile.read(blocksize)
    while len(buf) > 0:
        hasher.update(buf)
        buf = afile.read(blocksize)
    afile.close()
    return hasher.hexdigest()

def findDup(parentFolder):
    # Dups in format {hash:[names]}
    dups = {}
    for dirName, subdirs, fileList in os.walk(parentFolder):
        if any(dirName.find(excludeName)!=-1 for excludeName in EXCLUDE_NAMES):
            continue
        print('Scanning %s...' % dirName)
        for filename in fileList:
            path = os.path.join(dirName, filename)
            # Calculate hash
            file_hash = hashfile(path)
            # Add or append the file path
            if file_hash in dups:
                dups[file_hash].append(path)
            else:
                dups[file_hash] = [path]
    return dups
    
for hash,files in findDup("..").items():
    if len(files) > 1:
      print(hash,":")
      for file in files:
          print("    ", file)
     
