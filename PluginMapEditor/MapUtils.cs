using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CadEditor;
using System.IO;

namespace PluginMapEditor
{
    public static class MapUtils
    {
       public static MapData loadMapDwd(int mapNo)
        {
            int romAddr = MapConfig.mapsInfo[mapNo].dataAddr;
            int[] mapData = new int[960];
            int[] attrData = new int[64];
            while (Globals.romdata[romAddr] != 0xFF)
            {
                int videoAddr = Utils.readWord(Globals.romdata, romAddr) - 0x2000;
                romAddr += 2;
                int count = Globals.romdata[romAddr++];
                for (int i = 0; i < count; i++)
                {
                    if (videoAddr < mapData.Length)
                    {
                        mapData[videoAddr++] = Globals.romdata[romAddr++];
                    }
                    else
                    {
                        attrData[-960 + videoAddr++] = Globals.romdata[romAddr++];
                    }
                }
            }
            return new MapData(mapData, attrData, 32);
        }

        public static MapData loadMapCad(int mapNo)
        {
            int romAddr = MapConfig.mapsInfo[mapNo].dataAddr;
            int[] mapData = new int[960];
            int[] attrData = new int[64];
            while (Globals.romdata[romAddr] != 0xFF)
            {
                uint videoAddr = (uint)Utils.readWord(Globals.romdata, romAddr) - 0x2000;
                if (videoAddr > mapData.Length + attrData.Length)
                {
                    break;
                }
                romAddr += 2;
                int count = Globals.romdata[romAddr++] + 1;
                for (int i = 0; i < count; i++)
                {
                    if (videoAddr < mapData.Length)
                    {
                        mapData[videoAddr++] = Globals.romdata[romAddr++];
                    }
                    else
                    {
                        attrData[-960 + videoAddr++] = Globals.romdata[romAddr++];
                    }
                }
            }
            return new MapData(mapData, attrData, 32);
        }

        static void foundLongestZeros(int[] data, int startIndex, int endIndex, out int firstZeroIndex, out int lastZeroIndex)
        {
            int longestZeroLen = 0;
            int curZeroLen = 0;
            int curFirstZeroIndex = -1;
            for (int i = startIndex; i < endIndex; i++)
            {
                if (data[i] == 0)
                {
                    if (++curZeroLen > longestZeroLen)
                    {
                        longestZeroLen = curZeroLen;
                        curFirstZeroIndex = i - longestZeroLen + 1;
                    }
                }
                else
                {
                    curZeroLen = 0;
                }
            }
            firstZeroIndex = curFirstZeroIndex;
            lastZeroIndex = firstZeroIndex + longestZeroLen - 1;
        }

        static void recursiveS(int[] d, int first, int last, MemoryStream outBuf, int countAdd)
        {
            int f, e;
            foundLongestZeros(d, first, last, out f, out e);
            if (e - f >= 5)
            {
                recursiveS(d, first, f, outBuf, countAdd);
                recursiveS(d, e + 1, last, outBuf, countAdd);
            }
            else
            {
                while (last > first)
                {
                    int addr = first + 0x2000;
                    outBuf.WriteByte((byte)(addr >> 8));
                    outBuf.WriteByte((byte)(addr & 0xFF));
                    outBuf.WriteByte((byte)Math.Min(last - first + countAdd, 255));
                    for (int ind = 0; ind < 255 && (first + ind) < last; ind++)
                        outBuf.WriteByte((byte)d[first + ind]);
                    first += 255;
                }
            }
        }

        public static int saveMapDwd(int mapNo, MapData mapData, out byte[] packedData)
        {
            packedData = new byte[(256 + 3) * 4];
            var s = new MemoryStream(packedData);
            var full = mapData.getFullArray();
            recursiveS(full, 0, full.Length, s, 0);
            s.WriteByte(0xFF); //write stop byte
            long nn = s.Position;
            return (int)nn;
        }

        public static int saveMapCad(int mapNo, MapData mapData, out byte[] packedData)
        {
            packedData = new byte[(256 + 3) * 4];
            var s = new MemoryStream(packedData);
            var full = mapData.getFullArray();
            recursiveS(full, 0, full.Length, s, 1);
            s.WriteByte(0xFF); //write stop byte
            long nn = s.Position;
            return (int)nn;
        }
        //--------------------------------------------

        public static MapData loadMapDt2(int mapNo)
        {
            int romAddr = MapConfig.mapsInfo[mapNo].dataAddr;
            int[] mapData = new int[960];
            int[] attrData = new int[64];
            int readCount = 0;
            int repeatSymbol = Globals.romdata[romAddr++];
            while (readCount < 0x400)
            {
                int sym = Globals.romdata[romAddr++];
                if (sym == repeatSymbol)
                {
                    int repeatsCount = Globals.romdata[romAddr++];
                    int chunkToRepeat = Globals.romdata[romAddr++];
                    for (int i = 0; i < repeatsCount; i++)
                    {
                        if (readCount < mapData.Length)
                        {
                            mapData[readCount++] = chunkToRepeat;
                        }
                        else
                        {
                            attrData[-960 + readCount++] = chunkToRepeat;
                        }
                    }
                }
                else
                {
                    if (readCount < mapData.Length)
                    {
                        mapData[readCount++] = (byte)sym;
                    }
                    else
                    {
                        attrData[-960 + readCount++] = (byte)sym;
                    }
                }

            }
            return new MapData(mapData, attrData, 32);
        }

        public static int saveMapDt2(int mapNo, MapData mapData, out byte[] packedData)
        {
            packedData = new byte[1024];
            var s = new MemoryStream(packedData);
            byte repeatCode = 0xDC;
            s.WriteByte(repeatCode);
            int repeatCounter = 1;
            var full = mapData.getFullArray();
            for (int i = 0; i < full.Length - 1; i++)
            {
                byte sym = (byte)full[i];
                if ((sym == full[i + 1]) && (i < full.Length - 2))
                {
                    repeatCounter++; //need check if repeatCounter < repeatCode
                }
                else
                {
                    if (repeatCounter < 3)
                    {
                        for (int r = 0; r < repeatCounter; r++)
                        {
                            s.WriteByte(sym);
                        }
                    }
                    else
                    {
                        s.WriteByte(repeatCode);
                        s.WriteByte((byte)repeatCounter);
                        s.WriteByte(sym);
                    }
                    repeatCounter = 1;
                }
            }
            //write last byte
            s.WriteByte((byte)full[full.Length - 1]);
            //write archive end code
            s.WriteByte(repeatCode);
            s.WriteByte(0);

            return (int)s.Position;
        }

        public static void applyBlock2x2ToMap(int[] mapData, ObjRec block, int x, int y)
        {
            const int MAP_WIDTH = 32;
            mapData[(y * 2 + 0) * MAP_WIDTH + x * 2 + 0] =block.indexes[0];
            mapData[(y * 2 + 0) * MAP_WIDTH + x * 2 + 1] =block.indexes[1];
            mapData[(y * 2 + 1) * MAP_WIDTH + x * 2 + 0] =block.indexes[2];
            mapData[(y * 2 + 1) * MAP_WIDTH + x * 2 + 1] =block.indexes[3];
        }

        public static void applyBlock4x2ToMap(int[] mapData, ObjRec block, int x, int y)
        {
            const int MAP_WIDTH = 32;
            mapData[(y * 2 + 0) * MAP_WIDTH + x * 4 + 0] = block.indexes[0];
            mapData[(y * 2 + 0) * MAP_WIDTH + x * 4 + 1] = block.indexes[1];
            mapData[(y * 2 + 0) * MAP_WIDTH + x * 4 + 2] = block.indexes[2];
            mapData[(y * 2 + 0) * MAP_WIDTH + x * 4 + 3] = block.indexes[3];

            mapData[(y * 2 + 1) * MAP_WIDTH + x * 4 + 0] = block.indexes[4];
            mapData[(y * 2 + 1) * MAP_WIDTH + x * 4 + 1] = block.indexes[5];
            mapData[(y * 2 + 1) * MAP_WIDTH + x * 4 + 2] = block.indexes[6];
            mapData[(y * 2 + 1) * MAP_WIDTH + x * 4 + 3] = block.indexes[7];
        }

        public static void applyBlock4x1ToMap(int[] mapData, ObjRec block, int x, int y)
        {
            const int MAP_WIDTH = 32;
            mapData[(y * 1 + 0) * MAP_WIDTH + x * 4 + 0] = block.indexes[0];
            mapData[(y * 1 + 0) * MAP_WIDTH + x * 4 + 1] = block.indexes[1];
            mapData[(y * 1 + 0) * MAP_WIDTH + x * 4 + 2] = block.indexes[2];
            mapData[(y * 1 + 0) * MAP_WIDTH + x * 4 + 3] = block.indexes[3];
        }


        private static void fillAttribs(int[] attrData, byte[] romdata, int attribAddr)
        {
            for (int i = 0; i < 64; i++)
            {
                attrData[i] = Globals.romdata[attribAddr + i];
            }
        }

        private static void fillAttribsNinjaCrusaders(int[] attrData, byte[] romdata, int attribAddr)
        {
            int HEIGHT = 6;
            int WIDTH = 8;
            for (int i = 0; i < HEIGHT*WIDTH; i++)
            {
                int x = i / HEIGHT;
                int y = i % HEIGHT;
                int ind = x * HEIGHT + y;
                int tind = y * WIDTH + x;
                attrData[tind] = Globals.romdata[attribAddr + ind];
            }
        }

        public static int saveAttribsNinjaCrusaders(int mapNo, MapData mapData, out byte[] packedData)
        {
            packedData = new byte[0];
            int attribAddr = MapConfig.mapsInfo[mapNo].attribsAddr;

            int HEIGHT = 6;
            int WIDTH = 8;
            for (int i = 0; i < HEIGHT * WIDTH; i++)
            {
                int x = i / HEIGHT;
                int y = i % HEIGHT;
                int ind = x * HEIGHT + y;
                int tind = y * WIDTH + x;
                Globals.romdata[attribAddr + ind] = (byte)mapData.attrData[tind];
            }
            Globals.flushToFile();
            return 0;
        }

        public static MapData loadMapContraSpirits(int mapNo)
        {
            int romAddr = MapConfig.mapsInfo[mapNo].dataAddr;
            int attribAddr = MapConfig.mapsInfo[mapNo].attribsAddr;
            int[] mapData = new int[960];
            int[] attrData = new int[64];
            var blocks = ConfigScript.getBlocks(0);
            int scrSize = ConfigScript.getScreenWidth(0) * ConfigScript.getScreenHeight(0);

            //fill tiles region
            const int SCREEN_WIDTH = 16;
            for (int i = 0; i < scrSize; i++)
            {
                int blockIndex = Utils.readWordLE(Globals.romdata, romAddr + i * 2);
                applyBlock2x2ToMap(mapData, blocks[blockIndex], i % SCREEN_WIDTH, i / SCREEN_WIDTH);                
            }

            fillAttribs(attrData, Globals.romdata, attribAddr);
            return new MapData(mapData, attrData, 32);
        }

        public static MapData loadMapBatman(int mapNo)
        {
            int romAddr = MapConfig.mapsInfo[mapNo].dataAddr;
            int attribAddr = MapConfig.mapsInfo[mapNo].attribsAddr;
            int[] mapData = new int[960];
            int[] attrData = new int[64];
            var blocks = ConfigScript.getBlocks(0);
            int scrSize = ConfigScript.getScreenWidth(0) * ConfigScript.getScreenHeight(0);

            //fill tiles region
            const int SCREEN_WIDTH = 8;
            for (int i = 0; i < scrSize; i++)
            {
                int blockIndex = Globals.romdata[romAddr + i];
                applyBlock4x2ToMap(mapData, blocks[blockIndex], i % SCREEN_WIDTH, i / SCREEN_WIDTH);
            }

            fillAttribs(attrData, Globals.romdata, attribAddr);
            return new MapData(mapData, attrData, 32);
        }

        public static MapData loadMapNinjaCrusaders(int mapNo)
        {
            int romAddr = MapConfig.mapsInfo[mapNo].dataAddr;
            int attribAddr = MapConfig.mapsInfo[mapNo].attribsAddr;
            int[] mapData = new int[960];
            int[] attrData = new int[64];
            var blocks = ConfigScript.getBlocks(0);
            int scrSize = ConfigScript.getScreenWidth(0) * ConfigScript.getScreenHeight(0);

            //fill tiles region
            const int SCREEN_HEIGHT = 24;
            for (int i = 0; i < scrSize; i++)
            {
                int blockIndex = Globals.romdata[romAddr + i];
                //vertical -> invert x and y parameters
                applyBlock4x1ToMap(mapData, blocks[blockIndex], i / SCREEN_HEIGHT, i % SCREEN_HEIGHT);
            }

            fillAttribsNinjaCrusaders(attrData, Globals.romdata, attribAddr);
            return new MapData(mapData, attrData, 32);
        }

        public static int saveAttribs(int mapNo, MapData mapData, out byte[] packedData)
        {
            packedData = new byte[0];
            int attribAddr = MapConfig.mapsInfo[mapNo].attribsAddr;
            for (int i = 0; i < mapData.attrData.Length; i++)
            {
                 Globals.romdata[attribAddr + i] = (byte)mapData.attrData[i];
            }
            Globals.flushToFile();
            return 0;
        }
    }
}
