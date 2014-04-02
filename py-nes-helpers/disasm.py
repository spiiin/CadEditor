from nes_opcodes import *

class ParseException(Exception):
  pass

def disasm(bytecode_):
  bytecode = bytecode_[:]
  while len(bytecode)>0:
    instr = bytecode[0]
    instrFormat, instrLen = nes_opcodes[instr]
    if instrLen > len(bytecode):
      raise ParseException("Invalid instruction %X : %s"%(instr,instrFormat))
    string = ""
    if instrLen == 1:
      string = instrFormat
    elif instrLen == 2:
      string = instrFormat%bytecode[1]
    else:
      string = instrFormat%(bytecode[2], bytecode[1])
    bytecode = bytecode[instrLen:]
    yield (string,instrLen)
    
    
def makeLuaReplacer(ind):
  def getNextByte():
    #BYTECODE FOR "LUA ind"
    yield 0x3A
    yield ind
    yield 0x60
    while True:
      yield 0xEA
  nexter = getNextByte()
  def luaReplacer(bytecode, addr, instrLen):
    for ind in xrange(instrLen):
      bytecode[addr+ind] = nexter.next()
  return luaReplacer
  
#extrace code of function from bytecode and replace old bytes from bytecode with values from replaceFunc
def extractFromCode(bytecode, startAddress = 0x0000, shift = 0x8000,  replaceFunc = None, maxIter = 0x8000):
  RTS_OPCODE = 0x60
  JMP_OPCODE = 0x4C
  curAddress = startAddress
  iter = 0
  answer = []
  while iter < maxIter:
    curInstr = bytecode[curAddress]
    instrFormat, instrLen = disasm(bytecode[curAddress:curAddress+3]).next()
    answer.append((curAddress+shift, instrFormat))
    oldCurAddress = curAddress
    curAddress += instrLen
    iter+=1
    if curInstr == RTS_OPCODE:
      break
    elif curInstr == JMP_OPCODE:
      curAddress = (bytecode[curAddress-1]<<8 | bytecode[curAddress-2]) - shift
    if replaceFunc!= None:
      replaceFunc(bytecode, oldCurAddress, instrLen)  
  return answer
  
###rom = loadRom(path)
###bank0 = rom.getPrgBank(7)
###dataNormal = extractFromCode(bank0, 0xD9A, shift = 0xC000, replaceFunc=makeLuaReplacer(1)) #(normal)
###dataLUA    = extractFromCode(bank0, 0xD9A, shift = 0xC000, replaceFunc=makeLuaReplacer(1)) #(after first call dataLUA must include (LUA 1, RTS) instructions 

def printDisasm(d):
  for x in d:
    print ("%04X"%x[0]), x[1]
