-- Script for showing all addresses of rnc array in console and make dumps of uncompressed arrays in dumps_dir in emulator folder
-- Rom: Jungle Book, The (U) [!].gen
-- Require binio module in emulator folder

require "binio"

local caddr, daddr, log
  os.execute("mkdir dumps_dir")
  logpath = "dumps_dir\\data.log"
  dumppath = "dumps_dir\\dump_%06X.bin"

local START_ADDR_1 = 0x1C6DEE
local END_ADDR_1 = 0x1C6F4A

local START_ADDR_2 = 0x1C7036
local END_ADDR_2 = 0x1C70CC

memory.registerexec(START_ADDR_1, function()
  caddr = memory.getregister("a0")
  daddr = memory.getregister("a1")
end)

memory.registerexec(START_ADDR_2, function()
  caddr = memory.getregister("a0")
  daddr = memory.getregister("a1")
end)

memory.registerexec(END_ADDR_1, function()
local endaddr, csize, dsize, w

  csize = memory.getregister("a3") - caddr
  if (csize % 2) ~= 0 then
  csize = csize + 1
  end
  dsize = memory.getregister("a1") - daddr

  BinIO.Open(string.format(dumppath, caddr), "wb")
  w = memory.readbyterange(daddr, dsize)

  BinIO.Write(w, dsize)
  BinIO.Close()

  log = io.open (logpath, "a");
  local pr = string.format("%06X (%05d / %05d)\n", caddr,csize,dsize)
  
  log:write(pr)
  log:close()
  print(pr)
end)

memory.registerexec(END_ADDR_2, function()
local endaddr, csize, dsize, w

  csize = memory.getregister("a3") - caddr
  if (csize % 2) ~= 0 then
  csize = csize + 1
  end
  dsize = memory.getregister("a1") - daddr

  BinIO.Open(string.format(dumppath, caddr), "wb")
  w = memory.readbyterange(daddr, dsize)

  BinIO.Write(w, dsize)
  BinIO.Close()

  log = io.open (logpath, "a");
  local pr = string.format("%06X (%05d / %05d)\n", caddr,csize,dsize)
  
  log:write(pr)
  log:close()
  print(pr)
end)