using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

namespace CadEditor
{
    static class Globals
    {
        static Globals()
        {
        }

        public static void loadData(string Filename, string ConfigFilename)
        {
            try
            {
                using (FileStream f = File.OpenRead(Filename))
                {
                    f.Read(romdata, 0, Globals.FILE_SIZE);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                using (XmlReader reader = XmlReader.Create(ConfigFilename, new XmlReaderSettings()))
                {
                    palOffset.readFromXml(reader, "pallete");
                    videoOffset.readFromXml(reader, "videoBack");
                    videoObjOffset.readFromXml(reader, "videoObj");
                    bigBlocksOffset.readFromXml(reader, "bigBlocks");
                    blocksOffset.readFromXml(reader, "blocks");
                    screensOffset.readFromXml(reader, "screens");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            levelRecsCad.Add(new LevelRec(0x10388, 76));
            levelRecsCad.Add(new LevelRec(0x10456, 31));
            levelRecsCad.Add(new LevelRec(0x105A1, 73));
            levelRecsCad.Add(new LevelRec(0x106D1, 57));
            levelRecsCad.Add(new LevelRec(0x10890, 97));
            levelRecsCad.Add(new LevelRec(0x10A1D, 74));
            levelRecsCad.Add(new LevelRec(0x10B0E, 41));
            levelRecsCad.Add(new LevelRec(0x10C88, 83));
            levelRecsCad.Add(new LevelRec(0x10DB3, 53));
            levelRecsCad.Add(new LevelRec(0x10EA1, 45));
            levelRecsCad.Add(new LevelRec(0x10FED, 71));

            levelRecsDwd.Add(new LevelRec(0x10315, 51));
            levelRecsDwd.Add(new LevelRec(0x10438, 60));
            levelRecsDwd.Add(new LevelRec(0x10584, 68));
            levelRecsDwd.Add(new LevelRec(0x106A0, 54));
            levelRecsDwd.Add(new LevelRec(0x10816, 80));
            levelRecsDwd.Add(new LevelRec(0x10962, 63));
            levelRecsDwd.Add(new LevelRec(0x10A89, 58));
 
            reloadLevelParamsData();
        }

        public static void reloadLevelParamsData()
        {
            levelData.Clear();
            doorsData.Clear();
            for (int i = 0; i < LEVELS_COUNT; i++)
                levelData.Add(LevelData.readFromFile(romdata, i));
            for (int i = 0; i < DOORS_COUNT; i++)
                doorsData.Add(DoorData.readFromFile(romdata, i));
        }

        public static int getVideoPageAddr(byte id)
        {
            if ((id & 0xF0) == 0x90)
                return videoOffset.beginAddr + videoOffset.recSize * (id & 0x0F);
            else if ((id & 0xF0) == 0x80)
                return videoObjOffset.beginAddr + videoObjOffset.recSize * (id & 0x0F); 
            return -1;
        }

        public static int getTilesAddr(byte id)
        {
            return blocksOffset.beginAddr + blocksOffset.recSize * id;
        }

        public static int getBigTilesAddr(byte id)
        {
            return bigBlocksOffset.beginAddr + bigBlocksOffset.recSize * id;
        }

        public static int getPalAddr(byte palId)
        {
            return palOffset.beginAddr + palId * palOffset.recSize;
        }

        public static int getBackTileAddr(int levelNo)
        {
            return 0x1E909 + levelNo * 0x10;
        }

        public static byte[] getScreen(int screenIndex)
        {
            var result = new byte[screensOffset.recSize];
            int beginAddr = screensOffset.beginAddr + screenIndex * screensOffset.recSize;
            for (int i = 0; i < screensOffset.recSize; i++)
                result[i] = Globals.romdata[beginAddr + i];
            return result;
        }

        public static List<ScreenRec> buildScreenRecs(int levelNo, bool stopOnDoor)
        {
             var lr = Globals.levelData[levelNo];
             int width = lr.getWidth();
             int height = lr.getHeight();
             byte[] layer = new byte[width * height];
             byte[] scroll = new byte[width * height];
             byte[] dirs = new byte[height];
             for (int i = 0; i < width * height; i++)
             {
                 layer[i] = Globals.romdata[lr.getActualLayoutAddr() + i];
                 scroll[i] = Globals.romdata[lr.getActualScrollAddr() + i];
             }
             for (int i = 0; i < height; i++)
             {
                 dirs[i] = Globals.romdata[lr.getActualDirsAddr() + i];
             }
             LevelLayerData curLevelLayerData = new LevelLayerData(width, height, layer, scroll, dirs);

             int[] roomInds = new int[width * height];
             for (int i = 0; i < width * height; i++)
                 roomInds[i] = -1;

            List<int> doorsIndexes = new List<int>();
            doorsIndexes.Add(0);
            //sort doors left to right
            for (int i = 0; i < width; i++)
              for (int j = height-1; j >=0 ; j--)
            {
                int curIndex1 = j*width + i;
                int doorIndex = curLevelLayerData.scroll[curIndex1] & 0x1F;
                if (doorIndex > 0 && doorIndex != 25 /*hack for last door with no exit*/)
                    doorsIndexes.Add(curIndex1);
            }
            List<int> startIndexes = new List<int>();
            //first stage. marking layout
            for (int doorInd = 0; doorInd < doorsIndexes.Count; doorInd++)
            {
                int doorNo = curLevelLayerData.scroll[doorsIndexes[doorInd]] & 0x1F;
                int curIndex = doorInd == 0 ? getStartLoc(levelNo) : Globals.doorsData[doorNo-1].startLoc;
                if (curIndex >= 0 && curIndex < width * height)
                {
                    roomInds[curIndex] = doorNo;
                    startIndexes.Add(curIndex);
                }
                int roomLen = 0;
                List<int> newWays = new List<int>();
                while (roomLen < MAX_SCREEN_LIST_LEN)
                {
                    int scrollVal = curLevelLayerData.scroll[curIndex] >> 5;
                    int nextIndex = getNextIndex(curLevelLayerData, curIndex, 0, stopOnDoor);
                    if (scrollVal == 7)
                    {
                        int newWayIndex = getNextIndex(curLevelLayerData, curIndex, 1);
                        if (newWayIndex != -1 && roomInds[newWayIndex] == -1 && curLevelLayerData.layer[newWayIndex]!=0)
                            newWays.Add(newWayIndex);
                    }
                    if (nextIndex != -1 && roomInds[nextIndex] == -1 && layer[nextIndex] !=0)
                    {
                        curIndex = nextIndex;
                    }
                    else if (newWays.Count > 0)
                    {
                        curIndex = newWays[0];
                        newWays.RemoveAt(0);
                    }
                    else
                    {
                        break;
                    }
                    roomLen++;
                    if (doorInd == 0)
                    {
                        roomInds[curIndex] = 0;
                    }
                    else
                    {
                        roomInds[curIndex] = doorNo;
                    }
                }
            }

            //sort start rooms
            doorsIndexes.Sort(makeSortDoorsForExits(curLevelLayerData));

            //second stage. get room list
            var res = new List<ScreenRec>();
            for (int doorInd = 0; doorInd < doorsIndexes.Count; doorInd++)
            {
                int doorNo = doorInd == 0 ? 0 : curLevelLayerData.scroll[doorsIndexes[doorInd]] & 0x1F;
                for (int y = height - 1; y >= 0; y--)
                {
                    int dir = curLevelLayerData.dirs[y];
                    if (dir == 0)
                    {
                        for (int x = 0; x < width; x++)
                            if (roomInds[y*width+x] == doorNo)
                            {
                                int curIndex1 = y * width + x;
                                bool upsort = getUpsort(curLevelLayerData, curIndex1);
                                if (res.FindIndex((scr)=>{ return scr.no == layer[curIndex1];})==-1)
                                  res.Add(new ScreenRec(layer[curIndex1], (byte)x, (byte)y, doorNo, false, upsort));
                            }
                    }
                    else
                    {
                        for (int x = width-1; x >=0; x--)
                            if (roomInds[y * width + x] == doorNo)
                            {
                                int curIndex1 = y * width + x;
                                bool upsort = getUpsort(curLevelLayerData, curIndex1);
                                if (res.FindIndex((scr) => { return scr.no == layer[curIndex1]; }) == -1)
                                  res.Add(new ScreenRec(layer[curIndex1], (byte)x, (byte)y, doorNo, false, upsort));
                            }
                    }
                }
            }
            return res;
        }

        private static Comparison<int> makeSortDoorsForExits(LevelLayerData currentLevelData)
        {
            return (x, y) => { return sortDoorsForExits(currentLevelData, x, y); };
        }

        private static int sortDoorsForExits(LevelLayerData curLevelLayerData, int doorIndex1, int doorIndex2)
        {
            if (doorIndex1 == 0)
                return doorIndex2 == 0 ? 0 : -1;
            else if (doorIndex2==0)
                return 1;
            int doorNo1 = doorIndex1 == 0 ? 0 : curLevelLayerData.scroll[doorIndex1] & 0x1F;
            int doorNo2 = doorIndex2 == 0 ? 0 : curLevelLayerData.scroll[doorIndex2] & 0x1F;
            var r1 = Globals.doorsData[doorNo1 - 1];
            var r2 = Globals.doorsData[doorNo2 - 1];
            return r1.scrX > r2.scrX ? 1 : r1.scrX < r2.scrX ? -1 : r1.scrY > r2.scrY ? 1 : r1.scrY < r2.scrY ? -1 : 0;
        }

        private static int compareRooms(List<ScreenRec> r1, List<ScreenRec> r2)
        {
            if (r1.Count == 0 || r2.Count == 0)
                return 0;
            return r1[0].sx > r2[0].sx ? 1 : r1[0].sx < r2[0].sx ? -1 : r1[0].sy > r2[0].sy ? 1 : r1[0].sy < r2[0].sy ? -1 : 0;
        }

        private static bool getBacksort(LevelLayerData curLevelLayerData, int curIndex)
        {
            //int curScroll = curLevelLayerData.scroll[curIndex] >> 5;
            int height = curIndex / curLevelLayerData.width;
            bool backSort = curLevelLayerData.dirs[height] != 0;
            return backSort;
        }

        private static bool getUpsort(LevelLayerData curLevelLayerData, int curIndex)
        {
            int curScroll = curLevelLayerData.scroll[curIndex] >> 5;
            bool upsort = curScroll == 0;
            return upsort;
        }

        /*public static List<ScreenRec> buildScreenRecsForObjects(int levelNo)
        {
            //preload
            var lr = Globals.levelData[levelNo];
            int width = lr.getWidth();
            int height = lr.getHeight();
            byte[] layer = new byte[width * height];
            byte[] scroll = new byte[width * height];
            byte[] dirs = new byte[height];
            for (int i = 0; i < width * height; i++)
            {
                layer[i] = Globals.romdata[lr.getActualLayoutAddr() + i];
                scroll[i] = Globals.romdata[lr.getActualScrollAddr() + i];
            }
            for (int i = 0; i < height; i++)
            {
                dirs[i] = Globals.romdata[lr.getActualDirsAddr() + i];
            }
            LevelLayerData curLevelLayerData = new LevelLayerData(width, height, layer, scroll, dirs);
            List<ScreenRec> res = new List<ScreenRec>();
            for (int y = height - 1; y >= 0; y--)
            {
                if (dirs[y] == 0)
                {
                    for (int x = 0; x < width; x++)
                        res.Add(new ScreenRec(layer[y * width + x], (byte)x, (byte)y, 0, false));
                }
                else
                {
                    for (int x = width-1; x >= 0; x--)
                        res.Add(new ScreenRec(layer[y * width + x], (byte)x, (byte)y, 0, true));
                }
            }
            
            return res;
        }*/

        private static int getNextDoor(LevelLayerData curLevelLayerData, int curIndex)
        {
            if (curIndex == -1)
                return -1;
            return curLevelLayerData.scroll[curIndex] & 0x1F;
        }

        private static int getNextIndex(LevelLayerData curLevelLayerData, int curIndex, int dir=0, bool stopOnDoor = false)
        {
            int scrollVal = curLevelLayerData.scroll[curIndex] >> 5;
            int doorVal = curLevelLayerData.scroll[curIndex] & 0x1F;
            if (doorVal != 0 && stopOnDoor)
            {
                return -1;
            }
            else
            if (scrollVal == 0 || scrollVal == 1 || scrollVal == 2)
            {
                curIndex -= curLevelLayerData.width;
                return curIndex < 0 ? -1 : curIndex;
            }
            else if (scrollVal == 6)
            {
                ++curIndex;
                if (curIndex % curLevelLayerData.width == 0)
                    return -1;
                return curIndex;
            }
            else if (scrollVal == 7)
            {
                //int dir = curLevelLayerData.getDirForIndex(curIndex);
                int dirAdd = dir == 0 ? 1 : -1;
                curIndex += dirAdd;
                if (dir == 0 && curIndex % curLevelLayerData.width == 0)
                    return -1;
                if (dir != 0 && curIndex % curLevelLayerData.width == curLevelLayerData.width - 1)
                    return -1;
                return curIndex;
            }
            else if (scrollVal == 5)
            {
                --curIndex;
                if (curIndex % curLevelLayerData.width == curLevelLayerData.width - 1)
                    return -1;
                return curIndex;
            }
            return -1;
        }

        /*private static int getTeleport(int doorNo)
        {
            return doorNo == 25 ? -1 : doorsData[doorNo - 1].startLoc;
        }*/

        //C&D specific
        private static int getStartLoc(int no)
        {
            var n = new[] { 0x23, 0x15, 0x10, 0x10, 0x18, 0x10, 0x20, 0x18, 0x20, 0x10, 0x18 };
            return n[no];
        }

        //DWD specific
        public static int[] layoutAddrs = 
        {
            0x1DFA0, 
            0x1DFE4,
            0x1E028,
            0x1E06C,
            0x1E0E4,
            0x1E11D,
            0x1E156,
        };

        public static List<LevelRec> levelRecsCad = new List<LevelRec>();
        public static List<LevelRec> levelRecsDwd = new List<LevelRec>();
        public static List<LevelData> levelData = new List<LevelData>(LEVELS_COUNT);
        public static List<DoorData> doorsData = new List<DoorData>(DOORS_COUNT);

        public static int LEVELS_COUNT = 11;
        public static int DOORS_COUNT = 25;

        public static int FILE_SIZE = 262160;
        public static byte[] romdata = new byte[FILE_SIZE];
        public static int CHUNKS_COUNT = 256;
        public static int VIDEO_PAGE_SIZE = 4096;
        public static int OBJECTS_COUNT = 256;
        public static int PAL_LEN = 16;
        public static int MAX_SCREEN_LIST_LEN = 64;

        public static OffsetRec palOffset;
        public static OffsetRec videoOffset;
        public static OffsetRec videoObjOffset;
        public static OffsetRec bigBlocksOffset;
        public static OffsetRec blocksOffset;
        public static OffsetRec screensOffset;
    }

    public struct OffsetRec
    {
        public OffsetRec(int beginAddr, int recCount, int recSize)
        {
            this.beginAddr = beginAddr;
            this.recCount = recCount;
            this.recSize = recSize;
        }

        public void readFromXml(XmlReader reader, string nodeName)
        {
            reader.ReadToFollowing(nodeName);
            reader.MoveToAttribute("begin");
            beginAddr = parseInt(reader.Value);
            reader.MoveToAttribute("count");
            recCount = parseInt(reader.Value);
            reader.MoveToAttribute("size");
            recSize = parseInt(reader.Value);
        }

        private int parseInt(string value)
        {
            int ans = 0;
            //try hex parsing
            if ((value.Length > 2) && (value[0] == '0') && ((value[1] == 'x') || (value[1] == 'X')))
            {
                var newStr = value.Substring(2);
                int.TryParse(newStr, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out ans);
                return ans;
            }
            int.TryParse(value, out ans);
            return ans;

        }

        public int beginAddr;
        public int recCount;
        public int recSize;
    }

    public struct LevelObjRec
    {
        public LevelObjRec(string name, int addrOfObjects, int addrOfPallete, int addrOfVideo)
        {
            this.name = name;
            this.addrOfObjects = addrOfObjects;
            this.addrOfPallete = addrOfPallete;
            this.addrOfVideo = addrOfVideo;
        }
        public string name;
        public int addrOfObjects;
        public int addrOfPallete;
        public int addrOfVideo;
    }

    public struct ObjRec
    {
        public ObjRec(byte c1, byte c2, byte c3, byte c4, byte typeColor)
        {
            this.c1 = c1;
            this.c2 = c2;
            this.c3 = c3;
            this.c4 = c4;
            this.typeColor = typeColor;
        }
        public byte c1, c2, c3, c4;
        public byte typeColor;

        public int getSubpallete()
        {
            return typeColor & 0x3;
        }

        public int getType()
        {
            return (typeColor & 0xF0) >> 4;
        }
    }

    public struct LevelRec
    {
        public LevelRec(int objectsBeginAddr, int objCount)
        {
            this.objCount = objCount;
            this.objectsBeginAddr = objectsBeginAddr;
        }
        public int objCount;
        public int objectsBeginAddr;
    }

    public struct ScreenRec
    {
        public ScreenRec(int no, byte sx, byte sy, int door, bool backSort = false, bool upSort = false)
        {
            this.no = no;
            this.sx = sx;
            this.sy = sy;
            this.backSort = backSort;
            this.upsort = upSort;
            this.door = door;
        }
        public int no;
        public byte sx;
        public byte sy;
        public bool backSort;
        public bool upsort;
        public int door;
    }

    struct LevelLayerData
    {
        public LevelLayerData(int width, int height, byte[] layer, byte[] scroll, byte[] dirs)
        {
            this.width = width;
            this.height = height;
            this.layer = layer;
            this.scroll = scroll;
            this.dirs = dirs;
        }

        public byte getDirForIndex(int index)
        {
            int line = index / width;
            return dirs[line];
        }
        public int width;
        public int height;
        public byte[] layer;
        public byte[] scroll;
        public byte[] dirs;
    }

    struct LevelData
    {
        static public LevelData readFromFile(byte[] romdata, int no)
        {
            LevelData res = new LevelData();
            res.objId = romdata[0x1E23D + no];
            res.backId = romdata[0x1E24C + no];
            res.palId = romdata[0x1E25B + no];
            res.palId2 = romdata[0x1E279 + no];
            res.palBlink = romdata[0x1E288 + no];
            res.width = romdata[0x1E22E + no];
            res.height = romdata[0x1E21F + no];
            res.startLoc = romdata[0x1E210 + no];
            byte layoutByte1 = romdata[0x1E297 + no];
            byte layoutByte2 = romdata[0x1E2A6 + no];
            res.layoutAddr = makeAddrPtr(layoutByte2, layoutByte1);
            byte scrollByte1 = romdata[0x1E2B5 + no];
            byte scrollByte2 = romdata[0x1E2C4 + no];
            res.scrollAddr = makeAddrPtr(scrollByte2, scrollByte1);
            res.bigBlockId = romdata[0x1E201 + no];
            res.musicNo = romdata[0x1E26A + no];
            byte dirsByte1 = romdata[0x10239 + no];
            byte dirsByte2 = romdata[0x10248 + no];
            res.dirsAddr = makeAddrPtr(dirsByte2, dirsByte1);
            return res;
        }

        public bool saveToFile(byte[] romdata, int no)
        {
            romdata[0x1E23D + no] = (byte)objId;
            romdata[0x1E24C + no] = (byte)backId;
            romdata[0x1E25B + no] = (byte)palId;
            romdata[0x1E279 + no] = (byte)palId2;
            romdata[0x1E288 + no] = (byte)palBlink;
            romdata[0x1E22E + no] = (byte)width;
            romdata[0x1E21F + no] = (byte)height;
            romdata[0x1E210 + no] = (byte)startLoc;
            romdata[0x1E297 + no] = getLoByte(layoutAddr);
            romdata[0x1E2A6 + no] = getHiByte(layoutAddr);
            romdata[0x1E2B5 + no] = getLoByte(scrollAddr);
            romdata[0x1E2C4 + no] = getHiByte(scrollAddr);
            romdata[0x1E201 + no] = (byte)bigBlockId;
            romdata[0x1E26A + no] = (byte)musicNo;
            romdata[0x10239 + no] = getLoByte(dirsAddr);
            romdata[0x10248 + no] = getHiByte(dirsAddr);
            Globals.reloadLevelParamsData();
            return true;
        }

        static private int makeAddrPtr(byte hi, byte lo)
        {
            return (hi << 8) | lo;
        }

        static private byte getLoByte(int addr)
        {
            return (byte)(addr & 0xFF);
        }

        static private byte getHiByte(int addr)
        {
            return (byte)(addr >> 8);
        }

        public int getActualScrollAddr()
        {
            return scrollAddr + 0x10010;
        }

        public int getActualLayoutAddr()
        {
            return layoutAddr + 0x10010;
        }

        public int getActualDirsAddr()
        {
            return dirsAddr + 0x8010;
        }

        public int getWidth()
        {
            return width + 1;
        }

        public int getHeight()
        {
            return height + 1;
        }

        public void setWidth(int w)
        {
            width = w-1;
        }

        public void setHeight(int h)
        {
            height = h-1;
        }

        public int backId;
        public int objId;
        public int palId;
        public int palId2;
        public int palBlink;
        public int startLoc;
        private int width;
        private int height;
        public int layoutAddr;
        public int scrollAddr;
        public int bigBlockId;
        public int musicNo;
        public int dirsAddr;
        //ptr tables 1-5 
    }

    struct DoorData
    {
        static public DoorData readFromFile(byte[] romdata, int no)
        {
            DoorData res = new DoorData();
            res.objId = romdata[0x1E6BB + no];
            res.backId = romdata[0x1E6D3 + no];
            res.palId = romdata[0x1E6EB + no];
            res.palId2 = romdata[0x1E703 + no];
            res.palBlink = romdata[0x1E71B + no];
            res.startLoc = romdata[0x1E673 + no];
            res.scrX = romdata[0x1E6A3 + no];
            res.scrY = romdata[0x1E68B + no];
            res.playerX = romdata[0x1E74C + no];
            res.playerY = romdata[0x1E734 + no];
            return res;
        }

        public bool saveToFile(byte[] romdata, int no)
        {
            romdata[0x1E6BB + no] = (byte)objId;
            romdata[0x1E6D3 + no] = (byte)backId;
            romdata[0x1E6EB + no] = (byte)palId;
            romdata[0x1E703 + no] = (byte)palId2;
            romdata[0x1E71B + no] = (byte)palBlink;
            romdata[0x1E673 + no] = (byte)startLoc;
            romdata[0x1E6A3 + no] = (byte)scrX;
            romdata[0x1E68B + no] = (byte)scrY;
            romdata[0x1E74C + no] = (byte)playerX;
            romdata[0x1E734 + no] = (byte)playerY;
            Globals.reloadLevelParamsData();
            return true;
        }
        public int backId;
        public int objId;
        public int palId;
        public int palId2;
        public int palBlink;
        public int startLoc;
        public int scrX;
        public int scrY;
        public int playerX;
        public int playerY;
    }

    public enum GameType
    {
        Generic,
        CAD
    };
}

