def extractWord(fullData):
	return (fullData[0]*256 + fullData[1], fullData[2:])
  
def extractTwoBytes(fullData):
	return (fullData[0], fullData[1], fullData[2:])
  
def extractCol(data, colLen):
	ans = []
	while len(ans) < colLen and len(data)>0:
		b1, b2, data = extractTwoBytes(data)
		if b1 == 0xFF:
			w, data = extractWord(data)
			ans.extend(itertools.repeat(w, b2+1))
		else:
			ans.append(b1*256+b2)
	return ans
  
def extractData(d):
	colCount, d = extractWord(d)
	colLen, d = extractWord(d)
	colAddrs = []
	for n in xrange(colCount):
		colAddr, d = extractWord(d)
		colAddrs.append(colAddr)
	ans = []
	for n in xrange(colCount):
		readColAddr = colAddrs[n]
		colData = d[colAddrs[n]-colCount*2-4:]
		col = extractCol(colData, colLen)
		ans.extend(col[:colLen])
	return ans
  
# >>> fn = r"SegaPitfall_1.bin"
# >>> f = open(fn, "rb")
# >>> dall = f.read()
# >>> f.close()
# >>> d = map(ord, dall)

# >>> fn = r"SegaPitfall_1_extract.bin"
# >>> f = open(fn, "wb")
# >>> def remakeBytes(d):
	# ans = []
	# for x in d:
		# ans.append(x / 256)
		# ans.append(x % 256)
	# return ans

# >>> db = remakeBytes(ans)
# >>> f = open(fn, "wb")
# >>> dbs = map(chr, db)
# >>> f.write("".join(dbs))
# >>> f.close()