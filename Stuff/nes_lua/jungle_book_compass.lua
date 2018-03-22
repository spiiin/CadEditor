--Script for showing path to the nearest crystal
--Rom: Jungle Book NES (All versions)
--Author: spiiin

local COMPASS_COUNT = 5

local startCrystalActivityAddr = 0x6B4
local levelObjAddrs = {
    0x167D5,
    0x18815,
    0x173F6, --FFCount=1
    0x14B7A, --FFCount=1
    0x1550E, --FFCount=1
    0x194D3,
    0x128D6,
    0x13642, --show 1 fake crystal
    0x17E04,
    0x10912, --show 1 fake crystal near the boss
}
local levelFFCount = {0,0,1,1,1,0,0,0,0,0} --hack used to correct determinate objects count per level
local mowgliAddrX      = 0x572
local mowgliAddrY      = 0x571
local mowgliAtScrAddrX = 0x662
local mowgliAtScrAddrY = 0x674
local levelNoAddr = 0x35D

local crystals = nil --store info about current level crystals states (position and index to monitor state (if player already got it)

--I don't want to find real items count variable, so I just calc objects while meet 0xFF,
--sometimes object list contain 0xFF, so I must use hack FFCount for manual skip it for some levels
function calcItems(addr, maxFFCount)
    local count = -1
    local FFCount = 0
    repeat
        local val = rom.readbyte(addr)
        count = count + 1
        addr = addr + 1
        if val == 0xFF then
            FFCount = FFCount + 1
        end
    until FFCount > maxFFCount -->1 for level 3/4/5, 0 for other levels
    return count + 1
end

function getCrystals(addr, objectsCount)
    local crystals = {}
    for i = 0, objectsCount do
        local v = rom.readbyte(addr + objectsCount*0 + i);
        local d = rom.readbyte(addr + objectsCount*1 + i);
        --if v == 0x40 and d == 0x0F then
        if d == 0x0F then
            local x = rom.readbyte(addr - objectsCount*2 + i);
            local y = rom.readbyte(addr - objectsCount*1 + i);
            table.insert(crystals, {x,y,i})
            --print("crystal coord", x, y, "index", i)
        end
    end
    return crystals
end

function drawCompass(minDx, minDy)
    local sx = memory.readbyte(mowgliAtScrAddrX) + 20
    local sy = memory.readbyte(mowgliAtScrAddrY)
    local scale = 4
    local endx = sx - minDx*4
    local endy = sy - minDy*4
    gui.line(sx, sy, endx, endy, "#FFFFFF")
    local ends = 2
    gui.box(endx - ends, endy - ends, endx + ends, endy + ends, "#00FF00")
end

function updateCompass(crystals)
    if crystals == nil then
        return
    end
    local posX = memory.readbyte(mowgliAddrX)
    local posY = memory.readbyte(mowgliAddrY) - 2
    gui.text(1, 12, string.format("PosX:%2d",posX))
    gui.text(1, 20, string.format("PosY:%2d",posY))
    
    --find min dist
    
    --local MAXVAL = 10e6
    --local minDist = 10e6
    --local minDx, minDy = MAXVAL, MAXVAL
    
    distTable = {}
    
    for i,c in ipairs(crystals) do
        local x,y,i = c[1], c[2], c[3]
        --check activity
        local isActiveByte = memory.readbyte(startCrystalActivityAddr + i)
        local isActive = bit.band(isActiveByte, 0x40) ~= 0x40
        if isActive then
            local dx, dy = posX-x, posY-y
            local distSq = dx*dx + dy*dy
            table.insert(distTable, {distSq, dx, dy})
        end
    end
    
    table.sort(distTable, function(lhs, rhs) return lhs[1] < rhs[1] end)
    for currentCompass = 1, COMPASS_COUNT do
        if #distTable < currentCompass then
            return
        end
        local currentDistRec = distTable[currentCompass]
        local minDist, minDx, minDy = currentDistRec[1], currentDistRec[2], currentDistRec[3]
        --print(distTable[1], minDist, minDx, minDy)
        local dist = math.sqrt(minDist)
        gui.text(1, 28, string.format("Min distantion:%d",dist))
        drawCompass(minDx, minDy)
    end
end


function changeLevelNo()
    local levelNo = memory.readbyte(levelNoAddr)
    print("Change level no", levelNo)
    local levelObjAddr = levelObjAddrs[levelNo+1]
    if levelObjAddr == nil then
        crystals = nil
        return
    end
    local maxFFCount = levelFFCount[levelNo+1]
    if maxFFCount == nil then
        crystals = nil
        return
    end
    local objectsCount = calcItems(levelObjAddr, maxFFCount)
    print("Objects count", objectsCount)
    crystals = getCrystals(levelObjAddr, objectsCount)
    print("Crystals count", #crystals)
    for k,v in ipairs(crystals) do
      local x,y,i = v[1], v[2], v[3]
      print("crystal coord", x, y, "index", i)
    end
end

memory.registerwrite(levelNoAddr, changeLevelNo)

while(true) do
    updateCompass(crystals)
    FCEU.frameadvance()
end