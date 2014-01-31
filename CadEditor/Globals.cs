using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

using System.Drawing;

namespace CadEditor
{
    public static class Globals
    {
        static Globals()
        {
        }

        public static void loadData(string Filename, string Dumpfile, string ConfigFilename)
        {
            try
            {
                using (FileStream f = File.OpenRead(Filename))
                {
                    int size = OpenFile.FileSize;
                    romdata = new byte[size];
                    f.Read(romdata, 0, size);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                if (Dumpfile != "")
                {
                    using (FileStream f = File.OpenRead(Dumpfile))
                    {
                        int size = OpenFile.DumpSize;
                        dumpdata = new byte[size];
                        f.Read(dumpdata, 0, size);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            try
            {
                ConfigScript.LoadFromFile(ConfigFilename);
                LevelData.LoadOffsetsFromConfig();
                DoorData.LoadOffsetsFromConfig();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            reloadLevelParamsData();
        }

        public static bool flushToFile()
        {
            if (OpenFile.DumpName != "")
            {
                try
                {
                    using (FileStream f = File.OpenWrite(OpenFile.DumpName))
                    {
                        f.Write(Globals.dumpdata, 0, OpenFile.DumpSize);
                        f.Seek(0, SeekOrigin.Begin);

                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            try
            {
                using (FileStream f = File.OpenWrite(OpenFile.FileName))
                {
                    f.Write(Globals.romdata, 0, OpenFile.FileSize);
                    f.Seek(0, SeekOrigin.Begin);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
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

        public static int getTilesAddr(int id)
        {
            return ConfigScript.blocksOffset.beginAddr + ConfigScript.blocksOffset.recSize * id;
        }

        public static int getBigTilesAddr(int id)
        {
            return ConfigScript.bigBlocksOffset.beginAddr + ConfigScript.bigBlocksOffset.recSize * id;
        }

        public static int getBackTileAddr(int levelNo)
        {
            return ConfigScript.boxesBackOffset.beginAddr + levelNo * ConfigScript.boxesBackOffset.recSize;
        }

        public static int getLevelWidth(int levelNo)
        {
            if (gameType != GameType.CAD)
                return ConfigScript.getLevelRec(levelNo).width;
            return levelData[levelNo].getWidth();
        }

        public static int getLevelHeight(int levelNo)
        {
            if (gameType != GameType.CAD)
                return ConfigScript.getLevelRec(levelNo).height;
            return levelData[levelNo].getHeight();
        }

        public static byte[] getScreen(int screenIndex)
        {
            var result = new byte[Math.Max(64, ConfigScript.screensOffset.recSize)];
            var arrayWithData = Globals.dumpdata != null ? Globals.dumpdata : Globals.romdata;
            int dataStride = ConfigScript.getScreenDataStride();
            int beginAddr = ConfigScript.screensOffset.beginAddr + screenIndex * ConfigScript.screensOffset.recSize * dataStride;
            for (int i = 0; i < ConfigScript.screensOffset.recSize; i++)
                result[i] = arrayWithData[beginAddr + i*dataStride];
            for (int i = ConfigScript.screensOffset.recSize; i < 64; i++)
                result[i] = 0; //need this?
            return result;
        }

        public static int getBigTileNoFromScreen(byte[] screenData, int index)
        {
            if (gameType == GameType.DT)
            {
                int noY = index % 8;
                int noX = index / 8;
                byte lineByte = screenData[0x40 + noX];
                int addValue = (lineByte & (1 << (7 - noY))) != 0 ? 256 : 0;
                return addValue + screenData[index];
            }
            return screenData[index];
        }

        public static void setBigTileToScreen(byte[] screenData, int index, int value)
        {
            if (gameType == GameType.DT)
            {
                bool hiPart = value > 0xFF;
                int noY = index % 8;
                int noX = index / 8;
                int lineByte = screenData[0x40 + noX];
                int mask = 1 << (7 - noY);
                if (hiPart)
                    lineByte |= mask;
                else
                    lineByte &= ~mask;
                screenData[index] = (byte)value;
                screenData[0x40 + noX] = (byte)lineByte;
            }
            else
            {
                screenData[index] = (byte)value;
            }
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
                if (doorIndex > 0 && doorIndex != DOORS_COUNT /*hack for last door with no exit*/)
                    doorsIndexes.Add(curIndex1);
            }
            List<int> startIndexes = new List<int>();
            //first stage. marking layout
            for (int doorInd = 0; doorInd < doorsIndexes.Count; doorInd++)
            {
                int doorNo = curLevelLayerData.scroll[doorsIndexes[doorInd]] & 0x1F;
                int curIndex = doorInd == 0 ? Globals.levelData[levelNo].startLoc : Globals.doorsData[doorNo - 1].startLoc;
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

        //for CAD editor only
        public static Image[] makeScreensCad(int levelNo, bool stopOnDoors)
        {
            //!!!duplicate in EditLayout.cs
            Image[] scrImages = new Image[ConfigScript.screensOffset.recCount];
            for (int i = 0; i < ConfigScript.screensOffset.recCount; i++)
                scrImages[i] = Video.emptyScreen(512, 512);

            var screenList = Globals.buildScreenRecs(levelNo, stopOnDoors);

            var sortedScreenList = new List<ScreenRec>(screenList);
            sortedScreenList.Sort((r1, r2) => { return r1.door > r2.door ? 1 : r1.door < r2.door ? -1 : 0; });
            int lastDoorNo = -1;

            byte blockId = (byte)Globals.levelData[levelNo].bigBlockId;
            byte backId = 0, palId = 0;
            for (int i = 0; i < sortedScreenList.Count; i++)
            {
                if (lastDoorNo != sortedScreenList[i].door)
                {
                    lastDoorNo = sortedScreenList[i].door;
                    if (lastDoorNo == 0)
                    {
                        backId = (byte)Globals.levelData[levelNo].backId;
                        palId = (byte)Globals.levelData[levelNo].palId;
                    }
                    else
                    {
                        backId = (byte)Globals.doorsData[lastDoorNo - 1].backId;
                        palId = (byte)Globals.doorsData[lastDoorNo - 1].palId;
                    }
                }
                int scrNo = sortedScreenList[i].no;
                int addEH = (levelNo == 5 || levelNo == 8) ? 256 : 0; //hack
                int realScrNo = scrNo - 1 + addEH;
                scrImages[scrNo] = Video.makeScreen(realScrNo, backId, blockId, blockId, palId);
            }
            return scrImages;
        }

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

        public static int getLayoutAddr(int index)
        {
            return ConfigScript.getLevelRec(index).layoutAddr;
        }

        public static int getScrollAddr(int index)
        {
            return getLayoutAddr(index) + 508; //dwd specific
        }

        /*private static int getTeleport(int doorNo)
        {
            return doorNo == DOORS_COUNT ? -1 : doorsData[doorNo - 1].startLoc;
        }*/

        public static byte[] getTTSmallBlocksColorBytes(int bigTileIndex)
        {
            int btc = ConfigScript.getBigBlocksCount();
            var colorBytes = new byte[btc];
            int addr = Globals.getBigTilesAddr(bigTileIndex);
            for (int i = 0; i < ConfigScript.getBigBlocksCount(); i++)
                colorBytes[i] = Globals.romdata[addr + btc * 4 + i];
            return colorBytes;
        }

        public static IList<LevelData> levelData = new List<LevelData>(LEVELS_COUNT);
        public static IList<DoorData> doorsData = new List<DoorData>(DOORS_COUNT);

        //cad specific
        public static int LEVELS_COUNT = 11;
        public static int DOORS_COUNT = 25;

        public static byte[] romdata;
        public static byte[] dumpdata;
        public static int CHUNKS_COUNT = 256;
        public static int VIDEO_PAGE_SIZE = 4096;
        public static int PAL_LEN = 16;
        public static int MAX_SCREEN_LIST_LEN = 64;

        public static GameType gameType = GameType.Generic;
    }

    public struct OffsetRec
    {
        public OffsetRec(int beginAddr, int recCount, int recSize)
        {
            this.beginAddr = beginAddr;
            this.recCount = recCount;
            this.recSize = recSize;
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

        public int getSubpalleteForDt2(int parByteNo)
        {
            return (typeColor >> parByteNo*2) & 3;
        }

        public void setSubpalleteForDt2(int parByteNo, int color)
        {
            typeColor = (byte)((typeColor & (~(3 << parByteNo * 2))) | (color << parByteNo * 2)); 
        }

        public int getTypeForDt2(int no)
        {
            return no < 0xA0 ? 0 : 5;
        }

        public int getType()
        {
            return (typeColor & 0xF0) >> 4;
        }
    }

    public struct LevelRec
    {
        public LevelRec(int objectBeginAddr, int objCount)
        {
            this.objCount = objCount;
            this.objectsBeginAddr = objectBeginAddr;
            this.width = 0;
            this.height = 0;
            this.layoutAddr = 0;
        }
        public LevelRec(int objectsBeginAddr, int objCount, int width = 0, int height = 0, int layoutAddr = 0)
        {
            this.objCount = objCount;
            this.objectsBeginAddr = objectsBeginAddr;
            this.width = width;
            this.height = height;
            this.layoutAddr = layoutAddr;
        }
        public int objCount;
        public int objectsBeginAddr;
        public int width;
        public int height;
        public int layoutAddr;
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

    public struct LevelLayerData
    {
        public LevelLayerData(int width, int height, byte[] layer, byte[] scroll, byte[] dirs)
        {
            this.width = width;
            this.height = height;
            this.layer = layer;
            this.scroll = scroll;
            this.dirs = dirs;
        }

        public LevelLayerData(int width, int height, byte[] layer)
        {
            this.width = width;
            this.height = height;
            this.layer = layer;
            this.scroll = null;
            this.dirs = null;
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

    public struct LevelData
    {
        static public LevelData readFromFile(byte[] romdata, int no)
        {
            LevelData res = new LevelData();
            res.objId = romdata[LevelRecBaseOffset        + 60  + no];
            res.backId = romdata[LevelRecBaseOffset       + 75  + no];
            res.palId = romdata[LevelRecBaseOffset        + 90  + no];
            res.palId2 = romdata[LevelRecBaseOffset       + 120 + no];
            res.palBlink = romdata[LevelRecBaseOffset     + 135 + no];
            res.width = romdata[LevelRecBaseOffset        + 45  + no];
            res.height = romdata[LevelRecBaseOffset       + 30  + no];
            res.startLoc = romdata[LevelRecBaseOffset     + 15  + no];
            byte layoutByte1 = romdata[LevelRecBaseOffset + 150 + no];
            byte layoutByte2 = romdata[LevelRecBaseOffset + 165 + no];
            byte scrollByte1 = romdata[LevelRecBaseOffset + 180 + no];
            byte scrollByte2 = romdata[LevelRecBaseOffset + 195 + no];
            res.bigBlockId = romdata[LevelRecBaseOffset   + 0   + no];
            res.musicNo = romdata[LevelRecBaseOffset      + 105 + no];
            byte dirsByte1 = romdata[LevelRecDirOffset    + 0   + no];
            byte dirsByte2 = romdata[LevelRecDirOffset    + 15  + no];

            res.scrollAddr = makeAddrPtr(scrollByte2, scrollByte1);
            res.layoutAddr = makeAddrPtr(layoutByte2, layoutByte1);
            res.dirsAddr = makeAddrPtr(dirsByte2, dirsByte1);
            return res;
        }

        public bool saveToFile(byte[] romdata, int no)
        {
            romdata[LevelRecBaseOffset + 60 + no] = (byte)objId;
            romdata[LevelRecBaseOffset + 75  + no] = (byte)backId;
            romdata[LevelRecBaseOffset + 90  + no] = (byte)palId;
            romdata[LevelRecBaseOffset + 120 + no] = (byte)palId2;
            romdata[LevelRecBaseOffset + 135  + no] = (byte)palBlink;
            romdata[LevelRecBaseOffset + 45  + no] = (byte)width;
            romdata[LevelRecBaseOffset + 30  + no] = (byte)height;
            romdata[LevelRecBaseOffset + 15  + no] = (byte)startLoc;
            romdata[LevelRecBaseOffset + 150 + no] = getLoByte(layoutAddr);
            romdata[LevelRecBaseOffset + 165 + no] = getHiByte(layoutAddr);
            romdata[LevelRecBaseOffset + 180 + no] = getLoByte(scrollAddr);
            romdata[LevelRecBaseOffset + 195 + no] = getHiByte(scrollAddr);
            romdata[LevelRecBaseOffset + 0   + no] = (byte)bigBlockId;
            romdata[LevelRecBaseOffset + 105 + no] = (byte)musicNo;
            romdata[LevelRecDirOffset  + 0   + no] = getLoByte(dirsAddr);
            romdata[LevelRecDirOffset  + 15  + no] = getHiByte(dirsAddr);
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
            return scrollAddr + ScrollPtrAdd;
        }

        public int getActualLayoutAddr()
        {
            return layoutAddr + LayoutPtrAdd;
        }

        public int getActualDirsAddr()
        {
            return dirsAddr + DirPtrAdd;
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

        public static int LevelRecBaseOffset;
        public static int LevelRecDirOffset;
        public static int LayoutPtrAdd;
        public static int ScrollPtrAdd;
        public static int DirPtrAdd;

        public static void LoadOffsetsFromConfig()
        {
            LevelRecBaseOffset = ConfigScript.LevelRecBaseOffset;
            LevelRecDirOffset = ConfigScript.LevelRecDirOffset;
            LayoutPtrAdd = ConfigScript.LayoutPtrAdd;
            ScrollPtrAdd = ConfigScript.ScrollPtrAdd;
            DirPtrAdd = ConfigScript.DirPtrAdd;
        }
    }

    public struct DoorData
    {
        static public DoorData readFromFile(byte[] romdata, int no)
        {
            DoorData res = new DoorData();
            res.objId = romdata[DoorRecBaseOffset    + 72  + no];
            res.backId = romdata[DoorRecBaseOffset   + 96  + no];
            res.palId = romdata[DoorRecBaseOffset    + 120 + no];
            res.palId2 = romdata[DoorRecBaseOffset   + 144 + no];
            res.palBlink = romdata[DoorRecBaseOffset + 168 + no];
            res.startLoc = romdata[DoorRecBaseOffset + 0   + no];
            res.scrX = romdata[DoorRecBaseOffset     + 48  + no];
            res.scrY = romdata[DoorRecBaseOffset     + 24  + no];
            res.playerX = romdata[DoorRecBaseOffset  + 193 + no];
            res.playerY = romdata[DoorRecBaseOffset  + 217 + no];
            return res;
        }

        public bool saveToFile(byte[] romdata, int no)
        {
            romdata[DoorRecBaseOffset + 72 + no] = (byte)objId;
            romdata[DoorRecBaseOffset + 96 + no] = (byte)backId;
            romdata[DoorRecBaseOffset + 120 + no] = (byte)palId;
            romdata[DoorRecBaseOffset + 144 + no] = (byte)palId2;
            romdata[DoorRecBaseOffset + 168 + no] = (byte)palBlink;
            romdata[DoorRecBaseOffset + 0   + no] = (byte)startLoc;
            romdata[DoorRecBaseOffset + 48  + no] = (byte)scrX;
            romdata[DoorRecBaseOffset + 24  + no] = (byte)scrY;
            romdata[DoorRecBaseOffset + 193 + no] = (byte)playerX;
            romdata[DoorRecBaseOffset + 217 + no] = (byte)playerY;
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

        public static int DoorRecBaseOffset;

        public static void LoadOffsetsFromConfig()
        {
            DoorRecBaseOffset = ConfigScript.DoorRecBaseOffset;
        }
    }

    public struct ObjectRec
    {
        public ObjectRec(int type, int sx, int sy, int x, int y, Dictionary<String, int> additionalData)
            :this(type, sx, sy, x,y)
        {
            this.additionalData = additionalData;
        }
         
        public ObjectRec(int type, int sx, int sy, int x, int y)
        {
            this.type = type;
            this.sx = sx;
            this.sy = sy;
            this.x = x;
            this.y = y;
            this.additionalData = null;
        }
        public int type;
        public int x;
        public int y;
        public int sx;
        public int sy;
        public Dictionary<String, int> additionalData;

        public override String ToString()
        {
            String formatStr = (type > 15) ? "{0:X} : ({1:X}:{2:X})" : "0{0:X} : ({1:X}:{2:X})";
            return String.Format(formatStr, type, sx << 8 | x, sy << 8 | y);
        }
    }

    public enum GameType
    {
        Generic,
        CAD,
        DT,
        DT2,
        TT,
        LM,
        _3E,
    };
}