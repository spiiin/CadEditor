class Opcode:
	def __init__(self, name, stringFormat, hexCode, length):
		self.name = name
		self.stringFormat = stringFormat
		self.hexCode = hexCode
		self.length = length
    
def parseOpcode(lines):
	name = lines[0]
	ind = 1
	while lines[ind] != "" and ind < len(lines):
		ind+=1
	strings = lines[1:ind]
	lines = lines[ind+1:]
	return (name, strings), lines
  
def parseFormatString(fs):
	opf1 = fs[14:28]
	hex1 = fs[29:31]
	len1 = fs[33]
	return (opf1,hex1,len1)  
  
path1 = os.path.expanduser("opcodes_table.txt", "rt")
with open(path1,"rt") as f:
	lines = f.readlines()
lines = [x.strip() for x in lines]

opcodeRecs = []
while len(lines)>0:
	rec, lines = parseOpcode(lines)
	opcodeRecs.append(rec)
  
opcodes = []
for opcodeRec in opcodeRecs:
	name, strings = opcodeRec
	for string in strings:
	  format, hex, len = parseFormatString(string)
	  format = format.strip()
	  hex = int(hex,16)
	  len = int(len)
	  opcodes.append(Opcode(name,format,hex,len))
   
def appendSpecial(opcodes):   
  opcodes.append(Opcode("BMI", "BMI addr+%X", 0x30, 2))
  opcodes.append(Opcode("BVC", "BVC addr+%X", 0x50, 2))
  opcodes.append(Opcode("BVS", "BVS addr+%X", 0x70, 2))
  opcodes.append(Opcode("BCC", "BCC addr+%X", 0x90, 2))
  opcodes.append(Opcode("BCS", "BVC addr+%X", 0xB0, 2))
  opcodes.append(Opcode("BNE", "BVS addr+%X", 0xD0, 2))
  opcodes.append(Opcode("BEQ", "BCC addr+%X", 0xF0, 2))
  opcodes.append(Opcode("CLC", "CLC", 0x18, 1))
  opcodes.append(Opcode("SEC", "SEC", 0x38, 1))
  opcodes.append(Opcode("CLI", "CLI", 0x58, 1))
  opcodes.append(Opcode("SEI", "SEI", 0x78, 1))
  opcodes.append(Opcode("CLV", "CLV", 0xB8, 1))
  opcodes.append(Opcode("CLD", "CLD", 0xD8, 1))
  opcodes.append(Opcode("SED", "SED", 0xF8, 1))
  opcodes.append(Opcode("TAX", "TAX", 0xAA, 1))
  opcodes.append(Opcode("TXA", "TXA", 0x8A, 1))
  opcodes.append(Opcode("DEX", "DEX", 0xEA, 1))
  opcodes.append(Opcode("INX", "INX", 0xE8, 1))
  opcodes.append(Opcode("TAY", "TAY", 0xA8, 1))
  opcodes.append(Opcode("TYA", "TYA", 0x98, 1))
  opcodes.append(Opcode("DEY", "DEY", 0x88, 1))
  opcodes.append(Opcode("INY", "INY", 0xC8, 1))
  opcodes.append(Opcode("TXS", "TXS", 0x9A, 1))
  opcodes.append(Opcode("TSX", "TSX", 0xBA, 1))
  opcodes.append(Opcode("PHA", "PHA", 0x48, 1))
  opcodes.append(Opcode("PLA", "PLA", 0x68, 1))
  opcodes.append(Opcode("PHP", "PHP", 0x08, 1))
  opcodes.append(Opcode("PLP", "PLP", 0x28, 1))
  opcodes.append(Opcode("LUA", "LUA #%X", 0x3A, 2))
  
appendSpecial(opcodes)
  
for opcode in opcodes:
	opcode.stringFormat = opcode.stringFormat.replace("4400","%X%X").replace("44","%X").replace("5597","%X%X")
  
opcodeDict = {} 
for x in xrange(256):
	opcodeDict[x] = ("XXX", 1) 
for opcode in opcodes:
	opcodeDict[opcode.hexCode] = (opcode.stringFormat, opcode.length)
  
pyFileBegin = "nes_opcodes = [\n"
pyFileTempl = "  (%-16s,%d),       #%X\n"
pyFileEnd    = "]"
 
def makePyFile(fname):
  with open(fname, "wt") as f:
    f.write(pyFileBegin)
    for x in xrange(256):
      ops, opd = opcodeDict[x]
      f.write(pyFileTempl%(ops,opd,x))
    f.write(pyFileEnd)
    
path2 = "c:/users/spin/desktop/nes_opcodes.py"
makePyFile(path2)