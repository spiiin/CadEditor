using System;
using System.Windows.Forms;
using System.Globalization;
using System.IO;

namespace CadEditor
{
    public static class Utils
    {
        public static void setCbItemsCount(ComboBox cb, int count, int first = 0)
        {
            cb.Items.Clear();
            for (int i = 0; i < count; i++)
                cb.Items.Add(first + i);
        }

        public static void setCbIndexWithoutUpdateLevel(ComboBox cb, EventHandler ev, int index = 0)
        {
            cb.SelectedIndexChanged -= ev;
            cb.SelectedIndex = index;
            cb.SelectedIndexChanged += ev;
        }

        public delegate bool SaveFunction();
        public delegate void ReturnComboBoxIndexFunction();
        public static bool askToSave(ref bool dirty, SaveFunction saveToFile, ReturnComboBoxIndexFunction returnCbLevelIndex)
        {
            if (!dirty)
                return true;
            DialogResult dr = MessageBox.Show("Level was changed. Do you want to save current level?", "Save", MessageBoxButtons.YesNoCancel);
            if (dr == DialogResult.Cancel)
            {
                returnCbLevelIndex();
                return false;
            }
            else if (dr == DialogResult.Yes)
            {
                if (!saveToFile())
                {
                    returnCbLevelIndex();
                    return false;
                }
                return true;
            }
            else
            {
                dirty = false;
                return true;
            }
        }

        public static int parseInt(string value)
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

        public static int getChrAddress(int id)
        {
            if ((id & 0xF0) == 0x90)
                return ConfigScript.videoOffset.beginAddr + ConfigScript.videoOffset.recSize * (id & 0x0F);
            else if ((id & 0xF0) == 0x80)
                return ConfigScript.videoObjOffset.beginAddr + ConfigScript.videoObjOffset.recSize * (id & 0x0F);
            return -1;
        }

        public static byte[] getVideoChunk(int videoPageId)
        {
            //local version for cad & dwd
            byte[] videoChunk = new byte[Globals.VIDEO_PAGE_SIZE];
            int videoAddr = ConfigScript.getVideoPageAddr(videoPageId);
            for (int i = 0; i < Globals.VIDEO_PAGE_SIZE; i++)
                videoChunk[i] = Globals.romdata[videoAddr + i];
            return videoChunk;
        }

        public static void setVideoChunk(int videoPageId, byte[] videoChunk)
        {
            //local version for cad & dwd
            int videoAddr = ConfigScript.getVideoPageAddr(videoPageId);
            for (int i = 0; i < Globals.VIDEO_PAGE_SIZE; i++)
                Globals.romdata[videoAddr + i] = videoChunk[i];
        }

        public static byte[] fillBigBlocks(int bigTileIndex)
        {
            byte[] bigBlockIndexes = new byte[ConfigScript.getBigBlocksCount() * 4];
            if (GameType.DT2 != Globals.gameType)
            {
                var bigBlocksAddr = Globals.getBigTilesAddr(bigTileIndex);
                for (int i = 0; i < ConfigScript.getBigBlocksCount() * 4; i++)
                    bigBlockIndexes[i] = Globals.romdata[bigBlocksAddr + i];
                return bigBlockIndexes;
            }

            //dt2 version with consts:
            if (bigTileIndex == 0)
            {
                for (int i = 0; i < ConfigScript.getBigBlocksCount(); i++)
                {
                    bigBlockIndexes[i * 4 + 0] = Globals.romdata[0x10D4A + i];
                    bigBlockIndexes[i * 4 + 1] = Globals.romdata[0x10E19 + i];
                    bigBlockIndexes[i * 4 + 2] = Globals.romdata[0x10EE8 + i];
                    bigBlockIndexes[i * 4 + 3] = Globals.romdata[0x10FB7 + i];
                }
            }
            else if (bigTileIndex == 1)
            {
                for (int i = 0; i < ConfigScript.getBigBlocksCount(); i++)
                {
                    bigBlockIndexes[i * 4 + 0] = Globals.romdata[0x11086 + i];
                    bigBlockIndexes[i * 4 + 1] = Globals.romdata[0x11149 + i];
                    bigBlockIndexes[i * 4 + 2] = Globals.romdata[0x1120C + i];
                    bigBlockIndexes[i * 4 + 3] = Globals.romdata[0x112CF + i];
                }
            }
            else if (bigTileIndex == 2)
            {
                for (int i = 0; i < ConfigScript.getBigBlocksCount(); i++)
                {
                    bigBlockIndexes[i * 4 + 0] = Globals.romdata[0x11392 + i];
                    bigBlockIndexes[i * 4 + 1] = Globals.romdata[0x1143B + i];
                    bigBlockIndexes[i * 4 + 2] = Globals.romdata[0x114E4 + i];
                    bigBlockIndexes[i * 4 + 3] = Globals.romdata[0x1158D + i];
                }
            }
            else if (bigTileIndex == 3)
            {
                for (int i = 0; i < ConfigScript.getBigBlocksCount(); i++)
                {
                    bigBlockIndexes[i * 4 + 0] = Globals.romdata[0x11636 + i];
                    bigBlockIndexes[i * 4 + 1] = Globals.romdata[0x116E6 + i];
                    bigBlockIndexes[i * 4 + 2] = Globals.romdata[0x11796 + i];
                    bigBlockIndexes[i * 4 + 3] = Globals.romdata[0x11846 + i];
                }
            }
            else if (bigTileIndex == 4)
            {
                for (int i = 0; i < ConfigScript.getBigBlocksCount(); i++)
                {
                    bigBlockIndexes[i * 4 + 0] = Globals.romdata[0x118F6 + i];
                    bigBlockIndexes[i * 4 + 1] = Globals.romdata[0x119C7 + i];
                    bigBlockIndexes[i * 4 + 2] = Globals.romdata[0x11A98 + i];
                    bigBlockIndexes[i * 4 + 3] = Globals.romdata[0x11B69 + i];
                }
            }
            return bigBlockIndexes;
        }

        public static void saveBigBlocks(int bigTileIndex, byte[] bigBlockIndexes)
        {
            int addr = Globals.getBigTilesAddr(bigTileIndex);
            for (int i = 0; i < ConfigScript.getBigBlocksCount() * 4; i++)
                Globals.romdata[addr + i] = bigBlockIndexes[i];
        }

        public static bool getBit(byte b, int bit)
        {
            return (b & (1 << bit - 1)) != 0;
        }

        private static byte[] BitReverseTable =
        {
            0x00, 0x80, 0x40, 0xc0, 0x20, 0xa0, 0x60, 0xe0,
            0x10, 0x90, 0x50, 0xd0, 0x30, 0xb0, 0x70, 0xf0,
            0x08, 0x88, 0x48, 0xc8, 0x28, 0xa8, 0x68, 0xe8,
            0x18, 0x98, 0x58, 0xd8, 0x38, 0xb8, 0x78, 0xf8,
            0x04, 0x84, 0x44, 0xc4, 0x24, 0xa4, 0x64, 0xe4,
            0x14, 0x94, 0x54, 0xd4, 0x34, 0xb4, 0x74, 0xf4,
            0x0c, 0x8c, 0x4c, 0xcc, 0x2c, 0xac, 0x6c, 0xec,
            0x1c, 0x9c, 0x5c, 0xdc, 0x3c, 0xbc, 0x7c, 0xfc,
            0x02, 0x82, 0x42, 0xc2, 0x22, 0xa2, 0x62, 0xe2,
            0x12, 0x92, 0x52, 0xd2, 0x32, 0xb2, 0x72, 0xf2,
            0x0a, 0x8a, 0x4a, 0xca, 0x2a, 0xaa, 0x6a, 0xea,
            0x1a, 0x9a, 0x5a, 0xda, 0x3a, 0xba, 0x7a, 0xfa,
            0x06, 0x86, 0x46, 0xc6, 0x26, 0xa6, 0x66, 0xe6,
            0x16, 0x96, 0x56, 0xd6, 0x36, 0xb6, 0x76, 0xf6,
            0x0e, 0x8e, 0x4e, 0xce, 0x2e, 0xae, 0x6e, 0xee,
            0x1e, 0x9e, 0x5e, 0xde, 0x3e, 0xbe, 0x7e, 0xfe,
            0x01, 0x81, 0x41, 0xc1, 0x21, 0xa1, 0x61, 0xe1,
            0x11, 0x91, 0x51, 0xd1, 0x31, 0xb1, 0x71, 0xf1,
            0x09, 0x89, 0x49, 0xc9, 0x29, 0xa9, 0x69, 0xe9,
            0x19, 0x99, 0x59, 0xd9, 0x39, 0xb9, 0x79, 0xf9,
            0x05, 0x85, 0x45, 0xc5, 0x25, 0xa5, 0x65, 0xe5,
            0x15, 0x95, 0x55, 0xd5, 0x35, 0xb5, 0x75, 0xf5,
            0x0d, 0x8d, 0x4d, 0xcd, 0x2d, 0xad, 0x6d, 0xed,
            0x1d, 0x9d, 0x5d, 0xdd, 0x3d, 0xbd, 0x7d, 0xfd,
            0x03, 0x83, 0x43, 0xc3, 0x23, 0xa3, 0x63, 0xe3,
            0x13, 0x93, 0x53, 0xd3, 0x33, 0xb3, 0x73, 0xf3,
            0x0b, 0x8b, 0x4b, 0xcb, 0x2b, 0xab, 0x6b, 0xeb,
            0x1b, 0x9b, 0x5b, 0xdb, 0x3b, 0xbb, 0x7b, 0xfb,
            0x07, 0x87, 0x47, 0xc7, 0x27, 0xa7, 0x67, 0xe7,
            0x17, 0x97, 0x57, 0xd7, 0x37, 0xb7, 0x77, 0xf7,
            0x0f, 0x8f, 0x4f, 0xcf, 0x2f, 0xaf, 0x6f, 0xef,
            0x1f, 0x9f, 0x5f, 0xdf, 0x3f, 0xbf, 0x7f, 0xff
        };

        public static byte ReverseBits(byte toReverse)
        {
            return BitReverseTable[toReverse];
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public static bool saveDataToFile(string fn, byte[] data)
        {
            try
            {
                using (FileStream f = File.Open(fn, FileMode.Create))
                {
                    f.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        public static byte[] loadDataFromFile(string fn)
        {
            byte[] data = null;
            try
            {
                using (FileStream f = File.OpenRead(fn))
                {
                    int size = (int)new FileInfo(fn).Length;
                    data = new byte[size];
                    f.Read(data, 0, size);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return data;
        }
    }
}
