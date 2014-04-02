class RomData:
	PRG_BANK_SIZE = 16384
	CHR_BANK_SIZE = 65536
	def __init__(self, data, mapper):
		self.data = data
		self.mapper = mapper
	def getPrgBank(self,i):
		beginIndex = 0x10 + RomData.PRG_BANK_SIZE*i
		return self.data[beginIndex:beginIndex+RomData.PRG_BANK_SIZE]
	def setPrgBank(self, i, bank):
		assert (len(bank) == RomData.PRG_BANK_SIZE)
		beginIndex = 0x10 + RomData.PRG_BANK_SIZE*i
		self.data[beginIndex:beginIndex+RomData.PRG_BANK_SIZE] = bank
	def save(self, fname):
		with open(fname, "wb") as f:
			f.write(self.data)
      
      
def loadRom(fname):
  data = []
  with open(fname,"rb") as f:
    data = f.read()
  data = map(ord, data)
  return RomData(data,1)
  
rom = loadRom(path)
bank0 = rom.getPrgBank(0)