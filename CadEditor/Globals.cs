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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        public static int getTilesAddr(int id)
        {
            return ConfigScript.blocksOffset.beginAddr + ConfigScript.blocksOffset.recSize * id;
        }

        public static int getBigTilesAddr(int id)
        {
            return ConfigScript.bigBlocksOffset.beginAddr + ConfigScript.bigBlocksOffset.recSize * id;
        }

        public static int getLevelWidth(int levelNo)
        {
            return ConfigScript.getLevelRec(levelNo).width;
        }

        public static int getLevelHeight(int levelNo)
        {
            return ConfigScript.getLevelRec(levelNo).height;
        }

        public static int[] getScreen(OffsetRec screenOffset,  int screenIndex)
        {
            var result = new int[Math.Max(64, screenOffset.recSize)];
            var arrayWithData = Globals.dumpdata != null ? Globals.dumpdata : Globals.romdata;
            int dataStride = ConfigScript.getScreenDataStride();
            int wordLen = ConfigScript.getWordLen();
            bool littleEndian = ConfigScript.isLittleEndian();
            int beginAddr = screenOffset.beginAddr + screenIndex * screenOffset.recSize * dataStride * wordLen;
            if (wordLen == 1)
            {
                for (int i = 0; i < screenOffset.recSize; i++)
                    result[i] = ConfigScript.convertScreenTile(arrayWithData[beginAddr + i * dataStride]);
                for (int i = screenOffset.recSize; i < 64; i++)
                    result[i] = ConfigScript.convertScreenTile(0); //need this?
            }
            else if (wordLen == 2)
            {
                if (littleEndian)
                {
                    for (int i = 0; i < screenOffset.recSize; i++)
                        result[i] = ConfigScript.convertScreenTile(Utils.readWordLE(arrayWithData, beginAddr + i * (dataStride * wordLen)));
                }
                else
                {
                    for (int i = 0; i < screenOffset.recSize; i++)
                        result[i] = ConfigScript.convertScreenTile(Utils.readWord(arrayWithData, beginAddr + i * (dataStride * wordLen)));
                }
            }
            return result;
        }


        public static int getBigTileNoFromScreen(int[] screenData, int index)
        {
            if (gameType == GameType.DT)
            {
                int noY = index % 8;
                int noX = index / 8;
                int lineByte = screenData[0x40 + noX];
                int addValue = (lineByte & (1 << (7 - noY))) != 0 ? 256 : 0;
                return addValue + screenData[index];
            }
            if (index == -1)
                return -1;
            return screenData[index];
        }

        public static void setBigTileToScreen(int[] screenData, int index, int value)
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
                screenData[index] = value;
            }
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

        public static int getLayoutAddr(int index)
        {
            return ConfigScript.getLevelRec(index).layoutAddr;
        }

        public static int getScrollAddr(int index)
        {
            return getLayoutAddr(index) + ConfigScript.getScrollsOffsetFromLayout();
        }

        public static byte[] getTTSmallBlocksColorBytes(int bigTileIndex)
        {
            int btc = ConfigScript.getBigBlocksCount();
            var colorBytes = new byte[btc];
            int addr = Globals.getBigTilesAddr(bigTileIndex);
            for (int i = 0; i < ConfigScript.getBigBlocksCount(); i++)
                colorBytes[i] = Globals.romdata[addr + btc * 4 + i];
            return colorBytes;
        }

        public static byte[] romdata;
        public static byte[] dumpdata;
        public static int CHUNKS_COUNT = 256;
        public static int VIDEO_PAGE_SIZE = 4096;
        public static int PAL_LEN = 16;
        public static int SEGA_PAL_LEN = 128;
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
            this.width = 0;
            this.height = 0;
        }

        public OffsetRec(int beginAddr, int recCount, int recSize, int width = 0, int height = 0)
        {
            this.beginAddr = beginAddr;
            this.recCount = recCount;
            this.recSize = recSize;
            this.width = width;
            this.height = height;
        }

        public int beginAddr;
        public int recCount;
        public int recSize;
        public int width;
        public int height;
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
            this.name = "";
            this.levelNo = 0;
        }

        public LevelRec(int objectsBeginAddr, int objCount, int width = 0, int height = 0, int layoutAddr = 0)
        {
            this.objCount = objCount;
            this.objectsBeginAddr = objectsBeginAddr;
            this.width = width;
            this.height = height;
            this.layoutAddr = layoutAddr;
            this.name = "";
            this.levelNo = 0;
        }

        public LevelRec(int objectsBeginAddr, int objCount, int width = 0, int height = 0, int layoutAddr = 0, string name = "")
        {
            this.objCount = objCount;
            this.objectsBeginAddr = objectsBeginAddr;
            this.width = width;
            this.height = height;
            this.layoutAddr = layoutAddr;
            this.name = name;
            this.levelNo = 0;
        }

        public LevelRec(int objectsBeginAddr, int objCount, int width = 0, int height = 0, int layoutAddr = 0, string name = "", int levelNo = 0)
        {
            this.objCount = objCount;
            this.objectsBeginAddr = objectsBeginAddr;
            this.width = width;
            this.height = height;
            this.layoutAddr = layoutAddr;
            this.name = name;
            this.levelNo = levelNo;
        }
        public int objCount;
        public int objectsBeginAddr;
        public int width;
        public int height;
        public int layoutAddr;
        public int levelNo;
        public string name;
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

    public enum MapViewType
    {
        Tiles,
        ObjType,
        ObjNumbers,
        SmallObjNumbers,
    };
}