using System;
using System.Windows.Forms;
using System.Globalization;

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
            byte[] videoChunk = new byte[Globals.VIDEO_PAGE_SIZE];
            int videoAddr = ConfigScript.getVideoPageAddr(videoPageId);
            for (int i = 0; i < Globals.VIDEO_PAGE_SIZE; i++)
                videoChunk[i] = Globals.romdata[videoAddr + i];
            return videoChunk;
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
    }
}
