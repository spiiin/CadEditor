NAME_TABLE_ADDR = 0x2000
NAME_TABLE_PAGE = 0
NAME_TABLE_SIZE = 960
NAME_TABLE_FULL_SIZE = 0x400
NAME_TABLE_ROW = 32

ATTR_TABLE_ADDR = 0x23C0
ATTR_TABLE_SIZE   = 64

function selectNamepageForSearch(no)
    NAME_TABLE_PAGE = no
end

function hex(arg)
  return string.format("%02x", arg)
end

function romToString(rom)
    local chars = {}
    for i,b in ipairs(rom) do
        chars[i] = string.char(b)
    end
    return table.concat(chars)
end

local escapeRe
do
  local matches =
  {
    ["^"] = "%^";
    ["$"] = "%$";
    ["("] = "%(";
    [")"] = "%)";
    ["%"] = "%%";
    ["."] = "%.";
    ["["] = "%[";
    ["]"] = "%]";
    ["*"] = "%*";
    ["+"] = "%+";
    ["-"] = "%-";
    ["?"] = "%?";
    ["\0"] = "%z";
  }

  escapeRe = function(s)
    return (s:gsub(".", matches))
  end
end

function getNextItem2x2(firstIndex)
    local rowCount = NAME_TABLE_ROW / 2
    local skipBytes = NAME_TABLE_ROW * 2
    local i = firstIndex
    local baseI = i
    while baseI < NAME_TABLE_SIZE do
        i = baseI
        for x = 1, rowCount do
            coroutine.yield(i)
            i = i + 2
            if i >= NAME_TABLE_SIZE then
               return
            end
        end
        baseI = baseI + skipBytes
    end
end

function getNextItem4x4(firstIndex)
    local rowCount = NAME_TABLE_ROW / 4
    local skipBytes = NAME_TABLE_ROW * 4
    local i = firstIndex
    local baseI = i
    while baseI < NAME_TABLE_SIZE do
        i = baseI
        for x = 1, rowCount do
            coroutine.yield(i)
            i = i + 4
            if i >= NAME_TABLE_SIZE then
               return
            end
        end
        baseI = baseI + skipBytes
    end
end

function applyToAllElements(t, blockIncrementIterator)
    local newt = {}
    for i,e in ipairs(t) do
        newt[i] = coroutine.create(function() return blockIncrementIterator(e) end)
    end
    return newt
end

function produceNext(t)
    return function()
        ans = {}
        for i, e in ipairs(t) do
            result, val = coroutine.resume(e)
            if result == false or val == nil then
                return nil
            end
            ans[i] = val
        end
        return ans
    end
end


function getAllIndexes(startBlock, blockIncrementIterator)
    local its = applyToAllElements(startBlock, blockIncrementIterator)
    local all = {}
    for bb in produceNext(its) do
        table.insert(all, bb)
    end
    return all
end

function printIndexes(all)
    for i, bb in ipairs(all) do
       for j, b in ipairs(bb) do
            print(b ," ")
        end 
        print("\n")
    end
    print("Total blocks:", #all)
end

function mapToPpu(all, ppu)
    local newBlocks = {}
    for i, bb in ipairs(all) do
       local newBlock = {}
       for j, b in ipairs(bb) do
            local ppuAddr = NAME_TABLE_ADDR+NAME_TABLE_FULL_SIZE*NAME_TABLE_PAGE+b
            newBlock[j] = ppu:byte(ppuAddr+1)
       end 
       newBlocks[i] = newBlock
    end
    return newBlocks
end

function loadFromFile(fname)
    file = io.open(fname, "rb")
    local data = file:read("*all")
    file:close()
    return data
end

function buildReWithStride(block, stride)
    local e = escapeRe
    local blockStr = romToString(block)
    local anySymbol = "."
    local repStr = anySymbol:rep(stride)
    return e(blockStr:sub(1,1))..repStr..e(blockStr:sub(2,2))..repStr..e(blockStr:sub(3,3))..repStr..e(blockStr:sub(4,4))
end

function isZeroFFBlocks(block)
    for i, b in ipairs(block) do
        --print(b)
        if b ~= 0x00 and b ~= 0xFF then
            return false
        end
    end
    return true
end

function convertBlockStride(stride)
    return function(block)
        return buildReWithStride(block, stride)
    end
end

function convertBlockLinear()
    return function(block)
        local e = escapeRe
        local blockStr = romToString(block)
        return e(blockStr:sub(1,1))..e(blockStr:sub(2,2))..e(blockStr:sub(3,3))..e(blockStr:sub(4,4))
    end
end

function findBlocksInRom(rom, blocks, convertBlockFunc, blockStride)
    local foundTable = {}
    for i=1, #rom do
        foundTable[i]=-1
    end
    
    local alreadyCheckedBlocks = {}
    
    for blockIndex, block in ipairs(blocks) do
        if not isZeroFFBlocks(block) then
            local blockStr = convertBlockFunc(block)
            if not alreadyCheckedBlocks[blockStr] then
                alreadyCheckedBlocks[blockStr] = true
                local blockFound = 0
                while blockFound ~= nil do
                    blockFound = rom:find(blockStr, blockFound+1)
                    if blockFound ~= nil then
                        foundTable[blockFound] = blockIndex
                    end
                end
            end
        end
    end
    return calcLongestStrip(foundTable, blockStride)
end

function totalLen(curIndexes)
    local count = 0
    for _ in pairs(curIndexes) do count = count + 1 end
    return count
end

function calcLongestStrip(foundTable, blockBeginStride)
    local longestStrip = {}
    local foundTableLen = #foundTable
    local maxDistance = 64
    for x = 1, foundTableLen do
        local curIndexes = {}
        if foundTable[x] ~= -1 then
            for lenIndex = 1, maxDistance do
                local ind = x + lenIndex * blockBeginStride
                if ind >= foundTableLen then
                    break
                end
                if foundTable[ind] ~= -1 then
                    curIndexes[foundTable[ind]] = true
                end
            end
            local curIndexesLen = totalLen(curIndexes)
            if totalLen(curIndexes) > 3 then
                --print (totalLen(curIndexes))
                local newStripData = { [1] = x, [2] = curIndexesLen, [3] = curIndexes }
                if checkAlreadyInLongestStrip(longestStrip, newStripData, blockBeginStride) then
                    table.insert(longestStrip, newStripData)
                end
            end
        end
    end
    table.sort(longestStrip, function(a,b) return a[2]>b[2] end)
    return longestStrip
end

function checkAlreadyInLongestStrip(longestStrip, newVal, blockLen)
    longestStripLen = #longestStrip
    if longestStripLen == 0 then
        return true
    end
    local lastVal = longestStrip[longestStripLen]
    addr1, len1 = lastVal[1], lastVal[2]
    addr2, len2 = newVal[1], newVal[2]
    return addr1 + len1*blockLen < addr2 + len2*blockLen
end

--printIndexes(getAllIndexes({32,33, 64,65}, getNextItem2x2))
--printIndexes(getAllIndexes({0,1,32,33}, getNextItem2x2))
--printIndexes(getAllIndexes({0,1,2,3, 32,33,34,35, 64,65,66,67, 96,97,98,99}, getNextItem4x4))

function readAllRom()
    local size = rom.readbyte(4) * 0x4000 + 0x10
    data = {}
    for i = 1, size do
        data[i] = rom.readbyte(i)
    end
    return data
end

function printResults(foundTable)
    if #foundTable == 0 then
        print("  Not found")
        return
    end
    
    for i = 1,5 do
        local el = foundTable[i]
        if el ~= nil then
        print("    Address: ", hex(el[1]), " Count: ", el[2])
        end
    end
end

function runStride(rom, ppu, minStride, maxStride)
    --local rom = loadFromFile("rom.nes")
    --local ppu = loadFromFile("ppu.bin")
    
    local blocks = mapToPpu(getAllIndexes({0,1,32,33}, getNextItem2x2), ppu)
    for stride = minStride, maxStride do
        print("Stride: ", stride)
        local foundTable = findBlocksInRom(rom, blocks, convertBlockStride(stride), 4)
        printResults(foundTable)
        FCEU.frameadvance() --for not freeze emulator window
    end
end

function runLinear(rom, ppu)
    print("Search linear 2x2 horizontal blocks")
    printResults(findBlocksInRom(rom, mapToPpu(getAllIndexes({0,1,32,33}, getNextItem2x2), ppu), convertBlockLinear(), 4))
    print("Search linear 2x2 horizontal blocks, with attrib bytes")
    printResults(findBlocksInRom(rom, mapToPpu(getAllIndexes({0,1,32,33}, getNextItem2x2), ppu), convertBlockLinear(), 5))
    print("Search linear 2x2 vertical blocks")
    printResults(findBlocksInRom(rom, mapToPpu(getAllIndexes({0,32,1,33}, getNextItem2x2), ppu), convertBlockLinear(), 4))
    print("Search linear 2x2 vertical blocks, with attrib bytes")
    printResults(findBlocksInRom(rom, mapToPpu(getAllIndexes({0,32,1,33}, getNextItem2x2), ppu), convertBlockLinear(), 5))
    print("Search linear 4x4 horizontal blocks")
    printResults(findBlocksInRom(rom, mapToPpu(getAllIndexes({0,1,2,3, 32,33,34,35, 64,65,66,67, 96,97,98,99}, getNextItem4x4), ppu), convertBlockLinear(), 16))
    print("Search linear 4x4 vertical blocks")
    printResults(findBlocksInRom(rom, mapToPpu(getAllIndexes({0,32,64,96, 1,33,65,97, 2,34,66,98, 3,35,67,99}, getNextItem4x4), ppu), convertBlockLinear(), 16))
    --print("Search linear 4x4 mirrored Y blocks")
    --printResults(findBlocksInRom(rom, mapToPpu(getAllIndexes({96,97,98,99, 64,65,66,67, 32,33,34,35, 0,1,2,3}, getNextItem4x4), ppu), convertBlockLinear()))
end

local rom = romToString(readAllRom())
local ppu = ppu.readbyterange(0x0, 0x4000)
runLinear(rom, ppu)
runStride(rom, ppu, 255, 255)
print("Complete!")
print("Press 1 to restart search in ppu name table 0")
print("Press 2 to start search in ppu name table 1")
print("Press 3 to run stride search (take 1-2 minutes)")

while true do
    local t = input.get()
    if t["1"] then
        print("-----------------------------")
        print("Linear search in name table 0")
        selectNamepageForSearch(0)
        runLinear(rom, ppu)
        runStride(rom, ppu, 255, 255)
    end
    
    if t["2"] then
        print("-----------------------------")
        print("Linear search in name table 1")
        selectNamepageForSearch(1)
        runLinear(rom, ppu)
        runStride(rom, ppu, 255, 255)
    end
    
    if t["3"] then
        print("-----------------------------")
        print("Stride search")
        runStride(rom, ppu, 64, 255)
    end
    
    FCEU.frameadvance();
end;
