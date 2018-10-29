using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
//using System.Windows.Forms;

using CadEditor;

namespace PluginVideoNes
{
    public class Video : IVideoPluginNes
    {
        public string getName()
        {
            return "Nes Video Plugin";
        }
        static Video()
        {
            nesColors[0] = Color.FromArgb(124, 124, 124);
            nesColors[1] = Color.FromArgb(0, 0, 252);
            nesColors[2] = Color.FromArgb(0, 0, 188);
            nesColors[3] = Color.FromArgb(68, 40, 188);
            nesColors[4] = Color.FromArgb(148, 0, 132);
            nesColors[5] = Color.FromArgb(168, 0, 32);
            nesColors[6] = Color.FromArgb(168, 16, 0);
            nesColors[7] = Color.FromArgb(136, 20, 0);
            nesColors[8] = Color.FromArgb(80, 48, 0);
            nesColors[9] = Color.FromArgb(0, 120, 0);
            nesColors[0xA] = Color.FromArgb(0, 104, 0);
            nesColors[0xB] = Color.FromArgb(0, 88, 0);
            nesColors[0xC] = Color.FromArgb(0, 64, 88);
            nesColors[0xD] = Color.FromArgb(0, 0, 0);
            nesColors[0xE] = Color.FromArgb(0, 0, 0);
            nesColors[0xF] = Color.FromArgb(0, 0, 0);

            nesColors[0x10] = Color.FromArgb(188, 188, 188);
            nesColors[0x11] = Color.FromArgb(0, 120, 248);
            nesColors[0x12] = Color.FromArgb(0, 88, 248);
            nesColors[0x13] = Color.FromArgb(104, 68, 252);
            nesColors[0x14] = Color.FromArgb(216, 0, 204);
            nesColors[0x15] = Color.FromArgb(228, 0, 88);
            nesColors[0x16] = Color.FromArgb(248, 56, 0);
            nesColors[0x17] = Color.FromArgb(228, 92, 16);
            nesColors[0x18] = Color.FromArgb(172, 124, 0);
            nesColors[0x19] = Color.FromArgb(0, 184, 0);
            nesColors[0x1A] = Color.FromArgb(0, 168, 0);
            nesColors[0x1B] = Color.FromArgb(0, 168, 68);
            nesColors[0x1C] = Color.FromArgb(0, 136, 136);
            nesColors[0x1D] = Color.FromArgb(0, 0, 0);
            nesColors[0x1E] = Color.FromArgb(0, 0, 0);
            nesColors[0x1F] = Color.FromArgb(0, 0, 0);

            nesColors[0x20] = Color.FromArgb(248, 248, 248);
            nesColors[0x21] = Color.FromArgb(60, 188, 252);
            nesColors[0x22] = Color.FromArgb(104, 136, 252);
            nesColors[0x23] = Color.FromArgb(152, 120, 248);
            nesColors[0x24] = Color.FromArgb(248, 120, 248);
            nesColors[0x25] = Color.FromArgb(248, 88, 152);
            nesColors[0x26] = Color.FromArgb(248, 120, 88);
            nesColors[0x27] = Color.FromArgb(252, 160, 68);
            nesColors[0x28] = Color.FromArgb(248, 184, 0);
            nesColors[0x29] = Color.FromArgb(184, 248, 24);
            nesColors[0x2A] = Color.FromArgb(88, 216, 84);
            nesColors[0x2B] = Color.FromArgb(88, 248, 152);
            nesColors[0x2C] = Color.FromArgb(0, 232, 216);
            nesColors[0x2D] = Color.FromArgb(120, 120, 120);
            nesColors[0x2E] = Color.FromArgb(0, 0, 0);
            nesColors[0x2F] = Color.FromArgb(0, 0, 0);

            nesColors[0x30] = Color.FromArgb(252, 252, 252);
            nesColors[0x31] = Color.FromArgb(164, 228, 252);
            nesColors[0x32] = Color.FromArgb(184, 184, 248);
            nesColors[0x33] = Color.FromArgb(216, 184, 248);
            nesColors[0x34] = Color.FromArgb(248, 184, 248);
            nesColors[0x35] = Color.FromArgb(248, 164, 192);
            nesColors[0x36] = Color.FromArgb(240, 208, 176);
            nesColors[0x37] = Color.FromArgb(252, 224, 168);
            nesColors[0x38] = Color.FromArgb(248, 216, 120);
            nesColors[0x39] = Color.FromArgb(216, 248, 120);
            nesColors[0x3A] = Color.FromArgb(184, 248, 184);
            nesColors[0x3B] = Color.FromArgb(184, 248, 216);
            nesColors[0x3C] = Color.FromArgb(0, 252, 252);
            nesColors[0x3D] = Color.FromArgb(248, 216, 248);
            nesColors[0x3E] = Color.FromArgb(0, 0, 0);
            nesColors[0x3F] = Color.FromArgb(0, 0, 0);

            cadObjectTypeColors[0x0] = Color.FromArgb(196, 0, 255, 0);
            cadObjectTypeColors[0x1] = Color.FromArgb(196, 0, 255, 0);
            cadObjectTypeColors[0x2] = Color.FromArgb(196, 0, 196, 0);
            cadObjectTypeColors[0x3] = Color.FromArgb(196, 255, 0, 0);
            cadObjectTypeColors[0x4] = Color.FromArgb(196, 255, 0, 0);
            cadObjectTypeColors[0x5] = Color.FromArgb(196, 255, 0, 0);
            cadObjectTypeColors[0x6] = Color.FromArgb(196, 0, 255, 0);
            cadObjectTypeColors[0x7] = Color.FromArgb(196, 255, 255, 0);
            cadObjectTypeColors[0x8] = Color.FromArgb(196, 255, 0, 0);
            cadObjectTypeColors[0x9] = Color.FromArgb(196, 255, 0, 0);
            cadObjectTypeColors[0xA] = Color.FromArgb(196, 255, 0, 0);
            cadObjectTypeColors[0xB] = Color.FromArgb(196, 0, 0, 0);
            cadObjectTypeColors[0xC] = Color.FromArgb(196, 255, 0, 0);
            cadObjectTypeColors[0xD] = Color.FromArgb(196, 255, 0, 0);
            cadObjectTypeColors[0xE] = Color.FromArgb(196, 0, 255, 255);
            cadObjectTypeColors[0xF] = Color.FromArgb(196, 0, 255, 255);
        }

        public void updateColorsFromConfig()
        {
            if (ConfigScript.nesColors != null)
                nesColors = ConfigScript.nesColors;
        }

        public Bitmap makeImage(int index, byte[] videoChunk, byte[] pallete, int subPalIndex, bool withAlpha = false)
        {
            Bitmap res = new Bitmap(8, 8);
            using (Graphics g = Graphics.FromImage(res))
            {
                int i = index;
                int beginIndex = 16 * i;
                for (int line = 0; line < 8; line++)
                {
                    for (int pixel = 0; pixel < 8; pixel++)
                    {
                        bool bitLo = Utils.getBit(videoChunk[beginIndex + line], 8 - pixel);
                        bool bitHi = Utils.getBit(videoChunk[beginIndex + line + 8], 8 - pixel);
                        int palIndex = mixBits(bitHi, bitLo);
                        int fullPalIndex = subPalIndex * 4 + palIndex;
                        bool isBackColor = fullPalIndex % 4 == 0;
                        int colorNo = pallete[isBackColor ? 0 : fullPalIndex];
                        Color c = (withAlpha && isBackColor) ? Color.FromArgb(0) : nesColors[colorNo];
                        res.SetPixel(pixel, line, c);
                    }
                }
            }
            return res;
        }

        public Bitmap makeImageStrip(byte[] videoChunk, byte[] pallete, int subPalIndex, bool withAlpha = false)
        {
            Bitmap res = new Bitmap(8 * chunkCount, 8);
            using (Graphics g = Graphics.FromImage(res))
            {
                for (int i = 0; i < chunkCount; i++)
                {
                    Bitmap onePic = makeImage(i, videoChunk, pallete, subPalIndex, withAlpha);
                    g.DrawImage(onePic, new Rectangle(i * 8, 0, 8, 8));
                }
            }
            return res;
        }

        public Bitmap makeImageRectangle(byte[] videoChunk, byte[] pallete, int subPalIndex, bool withAlpha = false)
        {
            var images = Enumerable.Range(0, 256).Select(i => makeImage(i, videoChunk, pallete, subPalIndex, withAlpha));
            return UtilsGDI.GlueImages(images.ToArray(), 16, 16);
        }

        public Bitmap[] makeObjects(ObjRec[] objects, Bitmap[][] objStrips, MapViewType drawType, int constantSubpal = -1)
        {
            var ans = new Bitmap[objects.Length];
            for (int index = 0; index < objects.Length; index++)
            {
                ans[index] = makeObject(index, objects, objStrips, drawType, constantSubpal);
            }
            return ans;
        }

        public Bitmap makeObject(int index, ObjRec[] objects, Bitmap[][] objStrips, MapViewType drawType, int constantSubpal = -1)
        {
            var obj = objects[index];
            int scaleInt16 = 16;
            var images = new Image[obj.getSize()];
            for (int i = 0; i < obj.getSize(); i++)
            {
                int x = i % obj.w;
                int y = i / obj.w;
                int pali = (y >> 1) * (obj.w >> 1) + (x >> 1);
                var objStrip = constantSubpal == -1 ? objStrips[obj.getSubpallete(pali)] : objStrips[constantSubpal];
                images[i] = objStrip[obj.indexes[i]];
            }

            var mblock = UtilsGDI.GlueImages(images, obj.w, obj.h);
            using (Graphics g2 = Graphics.FromImage(mblock))
            {
                if (drawType == MapViewType.ObjType)
                {
                    int objType = obj.getType();
                    var col = (objType < cadObjectTypeColors.Length) ? cadObjectTypeColors[objType] : cadObjectTypeColors[0];
                    g2.FillRectangle(new SolidBrush(col), new Rectangle(0, 0, scaleInt16, scaleInt16));
                    g2.DrawString(String.Format("{0:X}", obj.getType()), new Font("Arial", 6), Brushes.White, new Point(0, 0));
                }
                else if (drawType == MapViewType.ObjNumbers)
                {
                    g2.FillRectangle(new SolidBrush(Color.FromArgb(192, 255, 255, 255)), new Rectangle(0, 0, scaleInt16, scaleInt16));
                    g2.DrawString(String.Format("{0:X}", index), new Font("Arial", 6), Brushes.Red, new Point(0, 0));
                }
            }
            return mblock;
        }

        public Bitmap[] makeObjects(int videoPageId, int tilesId, int palId, MapViewType drawType, int constantSubpal = -1)
        {
            byte[] videoChunk = ConfigScript.getVideoChunk(videoPageId);
            ObjRec[] objects = ConfigScript.getBlocks(tilesId);

            byte[] palette = ConfigScript.getPal(palId);
            var range256 = Enumerable.Range(0, 256);
            var objStrip1 = range256.Select(i => makeImage(i, videoChunk, palette, 0)).ToArray();
            var objStrip2 = range256.Select(i => makeImage(i, videoChunk, palette, 1)).ToArray();
            var objStrip3 = range256.Select(i => makeImage(i, videoChunk, palette, 2)).ToArray();
            var objStrip4 = range256.Select(i => makeImage(i, videoChunk, palette, 3)).ToArray();
            var objStrips = new[] { objStrip1, objStrip2, objStrip3, objStrip4 };

            var bitmaps = makeObjects(objects, objStrips, drawType, constantSubpal);
            return bitmaps;
        }

        public Bitmap makeObjectsStrip(int videoPageId, int tilesId, int palId, MapViewType drawType, int constantSubpal = -1)
        {
            var bitmaps = makeObjects(videoPageId, tilesId, palId, drawType, constantSubpal);
            return UtilsGDI.GlueImages(bitmaps, bitmaps.Length, 1);
        }

         public Image[] makeBigBlocks(int videoNo, int bigBlockNo, int blockNo, int palleteNo, MapViewType smallObjectsViewType = MapViewType.Tiles,
           MapViewType curViewType = MapViewType.Tiles, int hierarchyLevel = 0)
        {
            BigBlock[] bigBlockIndexes = ConfigScript.getBigBlocksRecursive(hierarchyLevel, bigBlockNo);
            return makeBigBlocks(videoNo, bigBlockNo, blockNo, bigBlockIndexes, palleteNo, smallObjectsViewType, curViewType, hierarchyLevel);
        }

        public Image[] makeBigBlocks(int videoNo, int bigBlockNo, int blockNo, BigBlock[] bigBlockIndexes, int palleteNo, MapViewType smallObjectsViewType = MapViewType.Tiles,
            MapViewType curViewType = MapViewType.Tiles, int hierarchyLevel = 0)
        {
            int blockCount = ConfigScript.getBigBlocksCount(hierarchyLevel, bigBlockNo);
            var bigBlocks = new Image[blockCount];

            Image[] smallBlocksPack;
            if (hierarchyLevel == 0)
            {
                smallBlocksPack = makeObjects(videoNo, blockNo, palleteNo, smallObjectsViewType);
            }
            else
            {
                var bigBlockIndexesPrev = ConfigScript.getBigBlocksRecursive(hierarchyLevel - 1, bigBlockNo);
                smallBlocksPack = makeBigBlocks(videoNo, bigBlockNo, blockNo, bigBlockIndexesPrev, palleteNo, smallObjectsViewType, curViewType, hierarchyLevel - 1);
            }

            //tt version hardcode
            Image[][] smallBlocksAll = null;

            bool smallBlockHasSubpals = bigBlockIndexes==null ? true : bigBlockIndexes[0].smallBlocksWithPal();
            if (!smallBlockHasSubpals)
            {
                smallBlocksAll = new Image[4][];
                for (int i = 0; i < 4; i++)
                {
                    smallBlocksAll[i] = makeObjects(videoNo, bigBlockNo, palleteNo, smallObjectsViewType, i);
                }
            }
            else
            {
                smallBlocksAll = new Image[4][] { smallBlocksPack, smallBlocksPack, smallBlocksPack, smallBlocksPack } ;
            }

            for (int btileId = 0; btileId < blockCount; btileId++)
            {
                Image b;
                if (ConfigScript.isBuildScreenFromSmallBlocks())
                {
                    var sb = smallBlocksPack[btileId];
                    //scale for small blocks
                    b = UtilsGDI.ResizeBitmap(sb, (int)(sb.Width * 2), (int)(sb.Height * 2));
                }
                else
                {
                    b = bigBlockIndexes[btileId].makeBigBlock(smallBlocksAll);
                }
                if (curViewType == MapViewType.ObjNumbers)
                    b = VideoHelper.addObjNumber(b, btileId);
                bigBlocks[btileId] = b;
            }
            return bigBlocks;
        }

        //make capcom screen image
        public Bitmap makeScreen(int scrNo, int levelNo, int videoNo, int bigBlockNo, int blockNo, int palleteNo, bool withBorders = true)
        {
            if (scrNo < 0)
            {
                return VideoHelper.emptyScreen(256, 256);
                //return VideoHelper.emptyScreen((int)(ConfigScript.getScreenWidthDefault(levelNo) * 32), (int)(ConfigScript.getScreenWidthDefault(levelNo) * 32));
            }
            var bigBlocks = makeBigBlocks(videoNo, bigBlockNo, blockNo, palleteNo, MapViewType.Tiles, MapViewType.Tiles);
            //var bigBlocks = makeBigBlocks(videoNo, bigBlockNo, blockNo, palleteNo, MapViewType.ObjType,MapViewType.Tiles, withBorders);
            var screens = ConfigScript.loadScreens(); //Need to send it to function parameters to avoid reloads for every screen
            Screen screen = screens[scrNo];
            int scrW = screen.width;
            int scrH = screen.height;
            //capcom hardcode

            return new Bitmap(MapEditor.screenToImage(bigBlocks, screens, scrNo, 2.0f, false, 0, 0, scrW, scrH));
        }

        public Color[] defaultNesColors
        {
            get { return nesColors; }
            set { nesColors = value; }
        }

        private static int mixBits(bool hi, bool lo)
        {
            return (hi ? 1 : 0) << 1 | (lo ? 1 : 0);
        }

        public static int nesColorsCount = 64;
        public static int chunkCount = 256;
        public static Color[] nesColors = new Color[nesColorsCount];

        const int CadObjtypesCount = 16;
        public static Color[] cadObjectTypeColors = new Color[CadObjtypesCount];
    }
}
