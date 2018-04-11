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
        public delegate void FillAttribDelegate(int[] attrData, byte[] romdata, int attribAddr);

        public static MapData[] loadMapDwd(int mapNo)
        {
            int romAddr = MapConfig.mapsInfo[mapNo].dataAddr;
            int[] mapData = new int[960];
            int[] attrData = new int[64];
            int[] mapData2 = new int[960];
            int[] attrData2 = new int[64];
            while (Globals.romdata[romAddr] != 0xFF)
            {
                int videoAddr = Utils.readWord(Globals.romdata, romAddr) - 0x2000;
                romAddr += 2;
                int count = Globals.romdata[romAddr++];
                for (int i = 0; i < count; i++)
                {
                    if (videoAddr < mapData.Length)
                    {
                        //write in name table 1, data
                        mapData[videoAddr++] = Globals.romdata[romAddr++];
                    }
                    else if (videoAddr < (mapData.Length + attrData.Length))
                    {
                        //write in name table 1, attributes
                        attrData[-960 + videoAddr++] = Globals.romdata[romAddr++];
                    }
                    else if (videoAddr < mapData.Length * 2 + attrData.Length)
                    {
                        //write in name table 2, data
                        mapData2[-960 - 64 + videoAddr++] = Globals.romdata[romAddr++];
                    }
                    else
                    {
                        //write in name table 2, attributes
                        attrData2[-960 * 2 - 64 + videoAddr++] = Globals.romdata[romAddr++];
                    }
                }
            }
            return new MapData[] { new MapData(mapData, attrData, 32) , new MapData(mapData2, attrData2, 32) };
        }

        public static MapData[] loadMapCad(int mapNo)
        {
            int romAddr = MapConfig.mapsInfo[mapNo].dataAddr;
            int[] mapData = new int[960];
            int[] attrData = new int[64];
            int[] mapData2 = new int[960];
            int[] attrData2 = new int[64];
            while (Globals.romdata[romAddr] != 0x00)
            {
                uint videoAddr = (uint)Utils.readWord(Globals.romdata, romAddr) - 0x2000;
                /*if (videoAddr > (mapData.Length + attrData.Length)*2) //support writing into two pages
                {
                    break;
                }*/
                romAddr += 2;
                int count = Globals.romdata[romAddr++] + 1;
                for (int i = 0; i < count; i++)
                {
                    if (videoAddr < mapData.Length)
                    {
                        //write in name table 1, data
                        mapData[videoAddr++] = Globals.romdata[romAddr++];
                    }
                    else if (videoAddr < (mapData.Length + attrData.Length))
                    {
                        //write in name table 1, attributes
                        attrData[-960 + videoAddr++] = Globals.romdata[romAddr++];
                    }
                    else if (videoAddr < mapData.Length*2+attrData.Length)
                    {
                        //write in name table 2, data
                        mapData2[-960 - 64 + videoAddr++] = Globals.romdata[romAddr++];
                    }
                    else
                    {
                        //write in name table 2, attributes
                        attrData2[-960*2 - 64 + videoAddr++] = Globals.romdata[romAddr++];
                    }
                }
            }
            return new MapData[] { new MapData(mapData, attrData, 32), new MapData(mapData2, attrData2, 32) };
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

        public static int saveMapDwd(int mapNo, MapData[] mapData, out byte[] packedData)
        {
            packedData = new byte[(256 + 3) * 4 * 2]; //max size, for two name tables
            var s = new MemoryStream(packedData);
            var full1 = mapData[0].getFullArray();
            var full2 = mapData[1].getFullArray();

            //copy both arrays data into one array
            var full = new int[full1.Length + full2.Length];
            Array.Copy(full1, full, full1.Length);
            Array.Copy(full2, 0, full, full1.Length, full2.Length);

            recursiveS(full, 0, full.Length, s, 0);
            s.WriteByte(0xFF); //write stop byte
            long nn = s.Position;
            return (int)nn;
        }

        public static int saveMapCad(int mapNo, MapData[] mapData, out byte[] packedData)
        {
            packedData = new byte[(256 + 3) * 4 * 2 ]; //max size, for two name tables
            var s = new MemoryStream(packedData);
            var full1 = mapData[0].getFullArray();
            var full2 = mapData[1].getFullArray();

            //copy both arrays data into one array
            var full = new int[full1.Length + full2.Length];
            Array.Copy(full1, full, full1.Length);
            Array.Copy(full2, 0, full, full1.Length, full2.Length);

            recursiveS(full, 0, full.Length, s, -1);
            s.WriteByte(0x00); //write stop byte
            long nn = s.Position;
            return (int)nn;
        }
        //--------------------------------------------

        public static MapData[] loadMapDt2(int mapNo)
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
                        mapData[readCount++] = sym;
                    }
                    else
                    {
                        attrData[-960 + readCount++] = sym;
                    }
                }

            }
            return new MapData[] { new MapData(mapData, attrData, 32) };
        }

        public static int saveMapDt2(int mapNo, MapData[] mapData, out byte[] packedData)
        {
            packedData = new byte[1024];
            var s = new MemoryStream(packedData);
            byte repeatCode = 0xDC;
            s.WriteByte(repeatCode);
            int repeatCounter = 1;
            var full = mapData[0].getFullArray();
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

        //------------------------------------------------------------------------------------------------

        private static void applyBlockToMap(int[] mapData, ObjRec block, int x, int y, int mapWidth)
        {
            int width = block.w;
            int height = block.h;
            for (int w = 0; w < width; w++)
            {
                for (int h = 0; h < height; h++)
                {
                    int mdIndex = (y * height + h) * mapWidth + x * width + w;
                    int bIndex = width * h + w;
                    if ((mdIndex >= 0) && (mdIndex < mapData.Length))
                    {
                        mapData[mdIndex] = block.indexes[bIndex];
                    }
                }
            }
        }

        public static void fillAttribs(int[] attrData, byte[] romdata, int attribAddr)
        {
            for (int i = 0; i < attrData.Length; i++)
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

        public static int saveAttribsNinjaCrusaders(int mapNo, MapData[] mapData, out byte[] packedData)
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
                Globals.romdata[attribAddr + ind] = (byte)mapData[0].attrData[tind];
            }
            Globals.flushToFile();
            return 0;
        }

        public static MapData[] loadMapFromBlocks(int mapNo, int mapSizeInBytes, int attrSizeInBytes, int mapWidth, bool vertical, FillAttribDelegate fillAttribDelegate)
        {
            int romAddr = MapConfig.mapsInfo[mapNo].dataAddr;
            int attribAddr = MapConfig.mapsInfo[mapNo].attribsAddr;
            int[] mapData = new int[mapSizeInBytes];
            int[] attrData = new int[attrSizeInBytes];
            var blocks = ConfigScript.getBlocks(0);
            int scrSize = ConfigScript.getScreenWidth(0) * ConfigScript.getScreenHeight(0);

            //fill tiles region
            int SCREEN_WIDTH = ConfigScript.getScreenWidth(0);
            for (int i = 0; i < scrSize; i++)
            {
                int blockIndex = Globals.readBlockIndexFromMap(Globals.romdata, romAddr, i);
                int bx = i % SCREEN_WIDTH;
                int by = i / SCREEN_WIDTH;
                if (vertical)
                {
                    Utils.Swap(ref bx, ref by);
                }
                applyBlockToMap(mapData, blocks[blockIndex], bx, by, mapWidth);
            }

            fillAttribDelegate(attrData, Globals.romdata, attribAddr);
            return new MapData[] { new MapData(mapData, attrData, mapWidth) };
        }

        public static MapData[] loadMapContraSpirits(int mapNo)
        {
            return loadMapFromBlocks(mapNo, 960, 64, 32, false, fillAttribs);
        }

        public static MapData[] loadMapBatman(int mapNo)
        {
            return loadMapFromBlocks(mapNo, 960, 64, 32, false, fillAttribs);
        }

        public static MapData[] loadMapNinjaCrusaders(int mapNo)
        {
            return loadMapFromBlocks(mapNo, 960, 64, 32, true, fillAttribsNinjaCrusaders);
        }

        public static MapData[] loadMapAddamsFamily(int mapNo)
        {
            return loadMapFromBlocks(mapNo, 256 * 20, 64 * 5, 256, false, fillAttribs);
        }

        public static MapData[] loadMapAddamsFamilyFloor(int mapNo)
        {
            return loadMapFromBlocks(mapNo, 256 * 6, 64 * 3, 256, false, fillAttribs);
        }

        public static int saveAttribs(int mapNo, MapData[] mapData, out byte[] packedData)
        {
            packedData = new byte[0];
            int attribAddr = MapConfig.mapsInfo[mapNo].attribsAddr;
            for (int i = 0; i < mapData[0].attrData.Length; i++)
            {
                 Globals.romdata[attribAddr + i] = (byte)mapData[0].attrData[i];
            }
            Globals.flushToFile();
            return 0;
        }
    }
}
