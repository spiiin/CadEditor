using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Globalization;

using System.Drawing;

namespace CadEditor
{
    public static class GlobalsCad
    {
        public static void reloadLevelParamsData()
        {
            LevelData.LoadOffsetsFromConfig();
            DoorData.LoadOffsetsFromConfig();

            levelData.Clear();
            doorsData.Clear();
            for (int i = 0; i < LEVELS_COUNT; i++)
                levelData.Add(LevelData.readFromFile(Globals.romdata, i));
            for (int i = 0; i < DOORS_COUNT; i++)
                doorsData.Add(DoorData.readFromFile(Globals.romdata, i));
        }
        //cad specific
        public static int LEVELS_COUNT = 11;
        public static int DOORS_COUNT = 25;

        public static IList<LevelData> levelData = new List<LevelData>(LEVELS_COUNT);
        public static IList<DoorData> doorsData = new List<DoorData>(DOORS_COUNT);

        public static OffsetRec boxesBackOffset;
        public static int LevelRecBaseOffset;
        public static int LevelRecDirOffset;
        public static int LayoutPtrAdd;
        public static int ScrollPtrAdd;
        public static int DirPtrAdd;
        public static int DoorRecBaseOffset;
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
            GlobalsCad.reloadLevelParamsData();
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
        public static int DoorRecBaseOffset;

        public static void LoadOffsetsFromConfig()
        {
            LevelRecBaseOffset = GlobalsCad.LevelRecBaseOffset;
            LevelRecDirOffset = GlobalsCad.LevelRecDirOffset;
            LayoutPtrAdd = GlobalsCad.LayoutPtrAdd;
            ScrollPtrAdd = GlobalsCad.ScrollPtrAdd;
            DirPtrAdd = GlobalsCad.DirPtrAdd;
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
            GlobalsCad.reloadLevelParamsData();
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
            DoorRecBaseOffset = GlobalsCad.DoorRecBaseOffset;
        }
    }
}