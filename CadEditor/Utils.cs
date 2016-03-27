using System;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Drawing;
using System.Linq;

namespace CadEditor
{
    public static class Utils
    {
        public static void setCbItemsCount(ComboBox cb, int count, int first = 0, bool inHex = false)
        {
            cb.Items.Clear();
            if (!inHex)
            {
                for (int i = 0; i < count; i++)
                    cb.Items.Add(first + i);
            }
            else
            {
                for (int i = 0; i < count; i++)
                    cb.Items.Add(String.Format("{0:X}", first+i));
            }
        }

        public static void setCbIndexWithoutUpdateLevel(ComboBox cb, EventHandler ev, int index = 0)
        {
            cb.SelectedIndexChanged -= ev;
            cb.SelectedIndex = index;
            cb.SelectedIndexChanged += ev;
        }

        public static void setCbCheckedWithoutUpdateLevel(CheckBox cb, EventHandler ev, bool index = false)
        {
            cb.CheckedChanged -= ev;
            cb.Checked = index;
            cb.CheckedChanged += ev;
        }

        public static void prepareBlocksPanel(FlowLayoutPanel blocksPanel, Size buttonSize, ImageList buttonsImages, EventHandler buttonBlockClick, int startIndex, int count)
        {
            blocksPanel.Controls.Clear();
            blocksPanel.SuspendLayout();
            for (int i = startIndex; i < startIndex+count; i++)
            {
                var but = new Button();
                but.FlatStyle = FlatStyle.Flat;
                but.Size = buttonSize;
                but.ImageList = buttonsImages;
                but.ImageIndex = i;
                but.Click += buttonBlockClick;
                but.Margin = new Padding(0);
                but.Padding = new Padding(0);
                blocksPanel.Controls.Add(but);
            }
            blocksPanel.ResumeLayout();
        }

        public static void reloadBlocksPanel(FlowLayoutPanel blocksPanel, ImageList buttonsImages, int startIndex, int count)
        {
            for (int i = startIndex, controlIndex = 0; i < startIndex+count; i++, controlIndex++)
            {
                var but = (Button)blocksPanel.Controls[controlIndex];
                but.ImageList = buttonsImages;
                but.ImageIndex = i;
            }
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
                if (returnCbLevelIndex != null)
                  returnCbLevelIndex();
                return false;
            }
            else if (dr == DialogResult.Yes)
            {
                if (!saveToFile())
                {
                    if (returnCbLevelIndex != null)
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

        public static int parseInt(string value, int defaultVal = 0)
        {
            int ans = defaultVal;
            //try hex parsing
            if ((value.Length > 2) && (value[0] == '0') && ((value[1] == 'x') || (value[1] == 'X')))
            {
                var newStr = value.Substring(2);
                if (int.TryParse(newStr, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out ans))
                  return ans;
                else 
                  return defaultVal;
            }
            if (int.TryParse(value, out ans))
                return ans;
            else
                return defaultVal;

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

        public static byte[] getPalleteLinear(int palIndex)
        {
            int pal_size = ConfigScript.isUseSegaGraphics() ? Globals.SEGA_PAL_LEN : Globals.PAL_LEN;
            var palette = new byte[pal_size];
            int addr = ConfigScript.palOffset.beginAddr + palIndex * ConfigScript.palOffset.recSize;
            if (!ConfigScript.isUseSegaGraphics())
            {
                for (int i = 0; i < pal_size; i++)
                    palette[i] = (byte)(Globals.romdata[addr + i] & 0x3F);
            }
            else
            {
                for (int i = 0; i < pal_size; i++)
                    palette[i] = (byte)(Globals.romdata[addr + i]);
            }
            return palette;
        }

        public static void setPalleteLinear(int palIndex, byte[] pallete)
        {
            int addr = ConfigScript.palOffset.beginAddr + palIndex * ConfigScript.palOffset.recSize;
            for (int i = 0; i < 16; i++)
                Globals.romdata[addr + i] = pallete[i];
        }

        public static LevelLayerData getLayoutLinear(int curActiveLayout)
        {
            int layoutAddr = ConfigScript.getLayoutAddr(curActiveLayout);
            int width =  ConfigScript.getLevelWidth(curActiveLayout);
            int height = ConfigScript.getLevelHeight(curActiveLayout);
            byte[] layer = new byte[width * height];
            for (int i = 0; i < width * height; i++)
                layer[i] = Globals.romdata[layoutAddr + i];
            return new LevelLayerData(width, height, layer, null, null);
        }

        public static int getBigTileNoFromScreen(int[] screenData, int index)
        {
            if (index == -1)
                return -1;
            return screenData[index];
        }

        public static void setBigTileToScreen(int[] screenData, int index, int value)
        {
            screenData[index] = value;
        }

        //strip ints to bytes
        public static byte[] linearizeBigBlocks(BigBlock[] bigBlocks)
        {
            if ((bigBlocks == null)  || (bigBlocks.Length == 0))
            {
                return new byte[0];
            }
            byte[] result = new byte[bigBlocks.Length * bigBlocks[0].getSize()];
            for (int i = 0; i < bigBlocks.Length; i++)
            {
                int size = bigBlocks[i].getSize();
                var byteIndexes = bigBlocks[i].indexes.Select(old => (byte)old).ToArray();
                Array.Copy(byteIndexes, 0, result, i*size, size);
            }
            return result;
        }

        public static BigBlock[] unlinearizeBigBlocks(byte[] data, int w, int h)
        {
            if ((data == null)  || (data.Length == 0))
            {
                return new BigBlock[0];
            }
            int size = w*h;
            BigBlock[] result = new BigBlock[data.Length / size];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = new BigBlock(w, h);
                Array.Copy(data, i*size, result[i].indexes, 0, size);
            }
            return result;
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

        //capcom version
        public static ObjRec[] readBlocksFromAlignedArrays(byte[] romdata, int addr, int count)
        {
            var objects = new ObjRec[count];
            for (int i = 0; i < count; i++)
            {
                byte c1, c2, c3, c4, typeColor;
                c1 = romdata[addr + i];
                c2 = romdata[addr + count*1 + i];
                c3 = romdata[addr + count*2 + i];
                c4 = romdata[addr + count*3 + i];
                typeColor = romdata[addr + count * 4 + i];
                objects[i] = new ObjRec(c1, c2, c3, c4, typeColor);
            }
            return objects;
        }
        
        /*public static ObjRec[] readBlocksFromUnalignedArrays(byte[] romdata, int addr1, int addr2, int addr3, int addr4, int addr5, int count)
        {
            var objects = new ObjRec[count];
            for (int i = 0; i < count; i++)
            {
                byte c1, c2, c3, c4, typeColor;
                c1 = romdata[addr1 + i];
                c2 = romdata[addr2 + i];
                c3 = romdata[addr3 + i];
                c4 = romdata[addr4 + i];
                typeColor = romdata[addr5 + i]; ;
            }
            return objects;
        }*/

        public static void writeBlocksToAlignedArrays(ObjRec[] objects, byte[] romdata, int addr, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var obj = objects[i];
                romdata[addr + i] = obj.c1;
                romdata[addr + count * 1 + i] = obj.c2;
                romdata[addr + count * 2 + i] = obj.c3;
                romdata[addr + count * 3 + i] = obj.c4;
                romdata[addr + count * 4 + i] = obj.typeColor;
            }
        }

        /*public static void writeBlocksToUnalignedArrays(ObjRec[] objects, byte[] romdata, int addr1, int addr2, int addr3, int addr4, int addr5, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var obj = objects[i];
                romdata[addr1 + i] = obj.c1;
                romdata[addr2 + i] = obj.c2;
                romdata[addr3 + i] = obj.c3;
                romdata[addr4 + i] = obj.c4;
                romdata[addr5 + i] = obj.typeColor;
            }
        }*/

        public static byte[] readLinearBigBlockData(int bigTileIndex)
        {
            int tileSize = ConfigScript.isBlockSize4x4() ? 16 : 4;
            int wordSize = ConfigScript.isUseSegaGraphics() ? 2 : 1;
            int size = ConfigScript.getBigBlocksCount() * tileSize * wordSize;
            byte[] bigBlockIndexes = new byte[size];
            var bigBlocksAddr = ConfigScript.getBigTilesAddr(bigTileIndex);
            for (int i = 0; i < size; i++)
                bigBlockIndexes[i] = Globals.romdata[bigBlocksAddr + i];
            return bigBlockIndexes;
        }

        public static BigBlock[] getBigBlocksCapcomDefault(int bigTileIndex)
        {
            var data = readLinearBigBlockData(bigTileIndex);
            return Utils.unlinearizeBigBlocks(data, 2, 2);
        }

        public static void writeLinearBigBlockData(int bigTileIndex, byte[] bigBlockIndexes)
        {
            int tileSize = ConfigScript.isBlockSize4x4() ? 16 : 4;
            int wordSize = ConfigScript.isUseSegaGraphics() ? 2 : 1;
            int size = ConfigScript.getBigBlocksCount() * tileSize * wordSize;
            int addr = ConfigScript.getBigTilesAddr(bigTileIndex);
            for (int i = 0; i < size; i++)
                Globals.romdata[addr + i] = bigBlockIndexes[i];
        }

        public static void setBigBlocksCapcomDefault(int bigTileIndex, BigBlock[] bigBlockIndexes)
        {
            var data = Utils.linearizeBigBlocks(bigBlockIndexes);
            writeLinearBigBlockData(bigTileIndex, data);
        }

        public static byte[] readDataFromAlignedArrays(byte[] romdata, int addr, int count)
        {
            byte[] data = new byte[count * 4];
            for (int i = 0; i < count; i++)
            {
                data[i * 4 + 0] = Globals.romdata[addr + count * 0 + i];
                data[i * 4 + 1] = Globals.romdata[addr + count * 1 + i];
                data[i * 4 + 2] = Globals.romdata[addr + count * 2 + i];
                data[i * 4 + 3] = Globals.romdata[addr + count * 3 + i];
                //data[i *4 + 4] = Globals.romdata[addr + count*4 + i]; // for tt
            }
            return data;
        }

        public static void writeDataToAlignedArrays(byte[] data, byte[] romdata, int addr, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Globals.romdata[addr + count * 0 + i] = data[i * 4 + 0];
                Globals.romdata[addr + count * 1 + i] = data[i * 4 + 1];
                Globals.romdata[addr + count * 2 + i] = data[i * 4 + 2];
                Globals.romdata[addr + count * 3 + i] = data[i * 4 + 3];
                //Globals.romdata[addr + count*4 + i] = data[i *4 + 4]; // for tt
            }
        }

        public static byte[] readDataFromUnalignedArrays(byte[] romdata, int addr1, int addr2, int addr3, int addr4, int count)
        {
            byte[] data = new byte[count * 4];
            for (int i = 0; i < count; i++)
            {
                data[i * 4 + 0] = Globals.romdata[addr1 + i];
                data[i * 4 + 1] = Globals.romdata[addr2 + i];
                data[i * 4 + 2] = Globals.romdata[addr3 + i];
                data[i * 4 + 3] = Globals.romdata[addr4 + i];
            }
            return data;
        }

        public static void writeDataToUnalignedArrays(byte[] data, byte[] romdata, int addr1, int addr2, int addr3, int addr4, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Globals.romdata[addr1 + i] = data[i * 4 + 0];
                Globals.romdata[addr2 + i] = data[i * 4 + 1];
                Globals.romdata[addr3 + i] = data[i * 4 + 2];
                Globals.romdata[addr4 + i] = data[i * 4 + 3];
            }
        }

        public static T[] mergeArrays<T>(T[] a, T[] b)
        {
            var c = new T[a.Length + b.Length];
            a.CopyTo(c, 0);
            b.CopyTo(c, a.Length);
            return c;
        }

        //
        public static int readWord(byte[] data, int addr)
        {
            return (short)(data[addr] << 8 | data[addr + 1]);
        }

        public static int readWordUnsigned(byte[] data, int addr)
        {
            return (data[addr] << 8 | data[addr + 1]);
        }

        public static int readWordLE(byte[] data, int addr)
        {
            return (short)(data[addr+1] << 8 | data[addr]);
        }

        public static int readWordUnsignedLE(byte[] data, int addr)
        {
            return data[addr + 1] << 8 | data[addr];
        }

        public static int readInt(byte[] data, int addr)
        {
            return data[addr] << 24 | data[addr + 1] << 16 | data[addr + 2] << 8 | data[addr + 3];
        }

        public static int readIntLE(byte[] data, int addr)
        {
            return data[addr + 3] << 24 | data[addr + 2] << 16 | data[addr + 1] << 8 | data[addr];
        }

        public static void writeWord(byte[] data, int addr, int word)
        {
            data[addr]     = (byte)(word >> 8);
            data[addr + 1] = (byte)(word & 0xFF);
        }

        public static void writeWordLE(byte[] data, int addr, int word)
        {
            data[addr+1] = (byte)(word >> 8);
            data[addr] = (byte)(word & 0xFF);
        }

        public static void writeInt(byte[] data, int addr, int word)
        {
            data[addr + 0] = (byte)(word >> 24);
            data[addr + 1] = (byte)((word&0x00FF0000) >> 16);
            data[addr + 2] = (byte)((word&0x0000FF00) >> 8 );
            data[addr + 3] = (byte)(word & 0xFF);
        }

        public static Image ResizeBitmap(Image sourceBMP, int width, int height)
        {
            Image result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
        }

        public static Rectangle getVisibleRectangle(Control panel, Control insideControl)
        {
            Rectangle rect = panel.RectangleToScreen(panel.ClientRectangle);
            while (panel != null)
            {
                rect = Rectangle.Intersect(rect, panel.RectangleToScreen(panel.ClientRectangle));
                panel = panel.Parent;
            }
            rect = insideControl.RectangleToClient(rect);
            return rect;
        }

        public static void setBlocks(ImageList bigBlocks, float curButtonScale = 2, int blockWidth = 32, int blockHeight = 32, MapViewType curDrawType = MapViewType.Tiles, bool showAxis = true)
        {
            MapViewType curViewType = curDrawType;

            bigBlocks.Images.Clear();
            //smallBlocks.Images.Clear();
            bigBlocks.ImageSize = new Size((int)(curButtonScale * blockWidth), (int)(curButtonScale * blockHeight));

            //if using pictures
            if (ConfigScript.usePicturesInstedBlocks)
            {
                if (ConfigScript.blocksPicturesFilename != "")
                {
                    var imSrc = Image.FromFile(ConfigScript.blocksPicturesFilename);
                    var imResized = Utils.ResizeBitmap(imSrc, (int)(curButtonScale * blockWidth * ConfigScript.getBigBlocksCount()), (int)(curButtonScale * blockHeight));
                    bigBlocks.Images.AddStrip(imResized);
                }
                if (ConfigScript.blocksPicturesFilenames != null)
                {
                    for (int i = 0; i < ConfigScript.blocksPicturesFilenames.Length; i++)
                    {
                        var fname = ConfigScript.blocksPicturesFilenames[i];
                        var imSrc = Image.FromFile(fname);
                        //WARNING!!! blockWidth param scale not supported for blocksPicturesFilenames
                        //var imResized = Utils.ResizeBitmap(imSrc, curButtonScale * blockWidth * ConfigScript.getBigBlocksCount(), curButtonScale * blockHeight);
                        var imResized = imSrc;
                        bigBlocks.Images.AddStrip(imResized);
                    }
                }
                for (int i = bigBlocks.Images.Count; i < 256; i++)
                    bigBlocks.Images.Add(VideoHelper.emptyScreen((int)(blockWidth * curButtonScale), (int)(blockHeight * curButtonScale)));
                if (showAxis)
                {
                    for (int i = 0; i < 256; i++)
                    {
                        var im1 = bigBlocks.Images[i];
                        using (var g = Graphics.FromImage(im1))
                            g.DrawRectangle(new Pen(Color.FromArgb(255, 255, 255, 255)), new Rectangle(0, 0, (int)(blockWidth * curButtonScale), (int)(blockHeight * curButtonScale)));
                        bigBlocks.Images[i] = im1;
                    }
                }

                if (curViewType == MapViewType.ObjNumbers)
                {
                    int _bbRectPosX = (int)((blockWidth / 2) * curButtonScale);
                    int _bbRectSizeX = (int)((blockWidth / 2) * curButtonScale);
                    int _bbRectPosY = (int)((blockHeight / 2) * curButtonScale);
                    int _bbRectSizeY = (int)((blockHeight / 2) * curButtonScale);
                    for (int i = 0; i < 256; i++)
                    {
                        var im1 = bigBlocks.Images[i];
                        using (var g = Graphics.FromImage(im1))
                        {
                            g.FillRectangle(new SolidBrush(Color.FromArgb(192, 255, 255, 255)), new Rectangle(0, 0, _bbRectSizeX * 2, _bbRectSizeY * 2));
                            g.DrawString(String.Format("{0:X}", i), new Font("Arial", 16), Brushes.Red, new Point(0, 0));
                        }
                        bigBlocks.Images[i] = im1;
                    }

                }
            }
        }

        //for capcom mmc3 mappers, only for certain banks
        public static int getRomAddr(int bank, int addr)
        {
            if (bank == 0x04)
                return 0x8000 + addr + 0x10;
            else if (bank == 0x05)
                return 0xC000 + addr + 0x10;
            return addr;
        }

        public static int makeAddrPtr(byte hi, byte lo)
        {
            return (hi << 8) | lo;
        }

        public static int getSignedFromByte(byte b)
        {
            if (b < 128)
                return b;
            else
                return -256 + b;
        }

        public static int[][] setScreens(int levelNo)
        {
            int[][]screens = new int[ConfigScript.screensOffset[levelNo].recCount][];
            for (int i = 0; i < ConfigScript.screensOffset[levelNo].recCount; i++)
                screens[i] = Globals.getScreen(ConfigScript.screensOffset[levelNo], i);
            return screens;
        }

        public static int[][] setScreens2()
        {
            int[][] screens = new int[ConfigScript.screensOffset2.recCount][];
            for (int i = 0; i < ConfigScript.screensOffset2.recCount; i++)
                screens[i] = Globals.getScreen(ConfigScript.screensOffset2, i);
            return screens;
        }

        public static byte[] readVideoBankFromFile(string filename, int videoPageId)
        {
            try
            {
                using (FileStream f = File.OpenRead(filename))
                {

                    byte[] videodata = new byte[(int)f.Length];
                    f.Read(videodata, 0, (int)f.Length);
                    byte[] ans = new byte[0x1000];
                    //read only first 16 banks
                    int offset = (videoPageId & 0x0F) * 0x1000;
                    for (int i = 0; i < ans.Length; i++)
                        ans[i] = videodata[offset + i];
                    return ans;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public static byte[] readBinFile(string filename)
        {
            try
            {
                using (FileStream f = File.OpenRead(filename))
                {
                    byte[] d = new byte[(int)f.Length];
                    f.Read(d, 0, (int)f.Length);
                    return d;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        public static byte[] readVideoBankFrom16Pointers(int[] ptrs)
        {
            //local version for cad & dwd
            byte[] videoChunk = new byte[Globals.VIDEO_PAGE_SIZE];
            for (int i = 0; i < ptrs.Length; i++)
            {
                var ptr = ptrs[i];
                for (int pi = 0; pi < 256; pi++)
                  videoChunk[i*256 + pi] = Globals.romdata[ptr + pi];
            }
            return videoChunk;
        }

        public static void loadEnemyPictures(ref ImageList objectSprites, ref Image[] objectSpritesBig)
        {
            const int OBJECTS_COUNT = 256; //limit for now
            var objSpritesDir = ConfigScript.getObjTypesPicturesDir();
            var objSpritesDirGeneric = "obj_sprites";
            var templ = objSpritesDir + "\\object{0}.png";
            var templGeneric = objSpritesDirGeneric + "\\object{0}.png";
            var templBig = objSpritesDir + "\\object{0}b.png";
            var templGenericBig = objSpritesDirGeneric + "\\object{0}b.png";
            objectSprites.Images.Clear();
            objectSpritesBig = new Image[256];
            for (int i = 0; i < OBJECTS_COUNT; i++)
            {
                var fname = String.Format(templ, i);
                var fnameGeneric = String.Format(templGeneric, i);
                //".." hack for WinXP compatibility
                if (File.Exists(fname))
                {
                    objectSprites.Images.Add(Image.FromFile(fname));
                }
                else if (File.Exists("..\\" + fname))
                {
                    objectSprites.Images.Add(Image.FromFile("..\\" + fname));
                }
                else if (File.Exists(fnameGeneric))
                {
                    objectSprites.Images.Add(Image.FromFile(fnameGeneric));
                }
                else if (File.Exists("..\\" + fnameGeneric))
                {
                    objectSprites.Images.Add(Image.FromFile("..\\" + fnameGeneric));
                }

                //
                var fnameBig = String.Format(templBig, i);
                var fnameGenericBig = String.Format(templGenericBig, i);
                if (File.Exists(fnameBig))
                {
                    objectSpritesBig[i] = Image.FromFile(fnameBig);
                }
                else if (File.Exists("..\\" + fnameBig))
                {
                    objectSpritesBig[i] = Image.FromFile("..\\" + fnameBig);
                }
                else if (File.Exists(fnameGenericBig))
                {
                    objectSpritesBig[i] = Image.FromFile(fnameGenericBig);
                }
                else if (File.Exists("..\\" + fnameGenericBig))
                {
                    objectSpritesBig[i] = Image.FromFile("..\\" + fnameGenericBig);
                }
                else
                {
                    objectSpritesBig[i] = objectSprites.Images[i];
                }
            }
        }

        public static  void defaultDrawObject(Graphics g, ObjectRec curObject, int listNo, bool isSelected, float curScale, ImageList objectSprites)
        {
            int x = curObject.x, y = curObject.y;
            var myFont = new Font(FontFamily.GenericSansSerif, 6.0f);
            if (curObject.type < objectSprites.Images.Count)
            {
                g.DrawImage(objectSprites.Images[curObject.type], new Point((int)(x * curScale) - 8, (int)(y * curScale) - 8));
            }
            else
            {
                g.FillRectangle(Brushes.Black, new Rectangle((int)(x * curScale) - 8, (int)(y * curScale) - 8, 16, 16));
                g.DrawString(curObject.type.ToString("X3"), myFont, Brushes.White, new Point((int)(x * curScale) - 8, (int)(y * curScale) - 8));
            }
            if (isSelected)
                g.DrawRectangle(new Pen(Brushes.Red, 2.0f), new Rectangle((int)(x * curScale) - 8, (int)(y * curScale) - 8, 16, 16));       
        }

        public static void defaultDrawObjectBig(Graphics g, ObjectRec curObject, int listNo, bool isSelected, float curScale, Image[] objectSpritesBig)
        {
            int x = curObject.x, y = curObject.y;
            var myFont = new Font(FontFamily.GenericSansSerif, 6.0f);
            int xsize = objectSpritesBig[curObject.type].Size.Width;
            int ysize = objectSpritesBig[curObject.type].Size.Height;
            if (curObject.type < objectSpritesBig.Length)
                g.DrawImage(objectSpritesBig[curObject.type], new Rectangle((int)(x * curScale) - xsize / 2, (int)(y * curScale) - ysize / 2, xsize, ysize));
            if (isSelected)
                g.DrawRectangle(new Pen(Brushes.Red, 2.0f), new Rectangle((int)(x * curScale) - xsize / 2, (int)(y * curScale) - ysize / 2, xsize, ysize));
        }
    }
}
