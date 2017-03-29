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
       public static  byte[] loadMapDwd(int romAddr)
        {
            byte[] mapData = new byte[1024];
            while (Globals.romdata[romAddr] != 0xFF)
            {
                int videoAddr = Utils.readWord(Globals.romdata, romAddr) - 0x2000;
                romAddr += 2;
                int count = Globals.romdata[romAddr++];
                for (int i = 0; i < count; i++)
                    mapData[videoAddr++] = Globals.romdata[romAddr++];
            }
            return mapData;
        }

        public static byte[] loadMapCad(int romAddr)
        {
            byte[] mapData = new byte[1024];
            while (Globals.romdata[romAddr] != 0xFF)
            {
                uint videoAddr = (uint)Utils.readWord(Globals.romdata, romAddr) - 0x2000;
                if (videoAddr >= mapData.Length)
                {
                    break;
                }
                romAddr += 2;
                int count = Globals.romdata[romAddr++]+1;
                for (int i = 0; i < count; i++)
                {
                    mapData[videoAddr++] = Globals.romdata[romAddr++];
                }

            }
            return mapData;
        }

        static void foundLongestZeros(byte[] data, int startIndex, int endIndex, out int firstZeroIndex, out int lastZeroIndex)
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

        static void recursiveS(byte[] d, int first, int last, MemoryStream outBuf, int countAdd)
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
                        outBuf.WriteByte(d[first + ind]);
                    first += 255;
                }
            }
        }

        public static int saveMapDwd(byte[] mapData, out byte[] packedData)
        {
            packedData = new byte[(256 + 3) * 4];
            var s = new MemoryStream(packedData);
            recursiveS(mapData, 0, mapData.Length, s, 0);
            s.WriteByte(0xFF); //write stop byte
            long nn = s.Position;
            return (int)nn;
        }

        public static int saveMapCad(byte[] mapData, out byte[] packedData)
        {
            packedData = new byte[(256 + 3) * 4];
            var s = new MemoryStream(packedData);
            recursiveS(mapData, 0, mapData.Length, s, 1);
            s.WriteByte(0xFF); //write stop byte
            long nn = s.Position;
            return (int)nn;
        }
        //--------------------------------------------

        public static byte[] loadMapDt2(int romAddr)
        {
            byte[] mapData = new byte[1024];
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
                        mapData[readCount++] = (byte)chunkToRepeat;
                    }
                }
                else
                {
                    mapData[readCount++] = (byte)sym;
                }

            }
            return mapData;
        }

        public static int saveMapDt2(byte[] mapData, out byte[] packedData)
        {
            packedData = new byte[1024];
            var s = new MemoryStream(packedData);
            byte repeatCode = 0xDC;
            s.WriteByte(repeatCode);
            int repeatCounter = 1;
            for (int i = 0; i < mapData.Length - 1; i++)
            {
                byte sym = mapData[i];
                if ((sym == mapData[i + 1]) && (i < mapData.Length - 2))
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
            s.WriteByte(mapData[mapData.Length - 1]);
            //write archive end code
            s.WriteByte(repeatCode);
            s.WriteByte(0);

            return (int)s.Position;
        }

        public static void applyBlock2x2ToMap(byte[] mapData, ObjRec block, int x, int y)
        {
            const int MAP_WIDTH = 32;
            mapData[(y * 2 + 0) * MAP_WIDTH + x * 2 + 0] = (byte)block.indexes[0];
            mapData[(y * 2 + 0) * MAP_WIDTH + x * 2 + 1] = (byte)block.indexes[1];
            mapData[(y * 2 + 1) * MAP_WIDTH + x * 2 + 0] = (byte)block.indexes[2];
            mapData[(y * 2 + 1) * MAP_WIDTH + x * 2 + 1] = (byte)block.indexes[3];
        }

        public static int getAttribAddr()
        {
            return MapConfig.attribAddr;
        }

        public static byte[] loadMapContraSpirits(int romAddr)
        {
            byte[] mapData = new byte[1024];
            var blocks = ConfigScript.getBlocks(0);
            int scrSize = ConfigScript.getScreenWidth(0) * ConfigScript.getScreenHeight(0);
            //fill tiles region
            const int SCREEN_WIDTH = 16;
            for (int i = 0; i < scrSize; i++)
            {
                int blockIndex = Utils.readWordLE(Globals.romdata, romAddr + i * 2);
                applyBlock2x2ToMap(mapData, blocks[blockIndex], i % SCREEN_WIDTH, i / SCREEN_WIDTH);                
            }
            //fill attribs region
            int attribAddr = getAttribAddr();
            for (int i = 0; i < 64; i++)
            {
                mapData[960 + i] = (byte)Globals.romdata[attribAddr + i];
            }
            return mapData;
        }

        public static int saveMapContraSpirits(byte[] mapData, out byte[] packedData)
        {
            packedData = new byte[0];
            return 0;
        }
    }
}
