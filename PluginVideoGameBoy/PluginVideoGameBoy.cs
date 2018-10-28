using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CadEditor;
using System.Drawing;
using System.Drawing.Imaging;

namespace PluginVideoGameBoy
{
    public class PluginVideoGb : IVideoPluginGb
    {
        public string getName()
        {
            return "Game Boy Video Plugin";
        }

        public Image[] makeBigBlocks(byte[] ppuData, byte[] tileData, byte[] pallette, int count,
            MapViewType curViewType = MapViewType.Tiles)
        {
            var result = new Image[count];
            Color[] pal = getPalette(pallette);

            var tiles = new Image[256];
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = getTile(ppuData, pal, i);
            }

            /*var im = getTilesRectangle(ppuData, pal);
            im.Save("chr.png");*/

            for (int i = 0; i < count; i++)
            {
                //linear 2x2 tiles only for now
                int startIndex = i * 4;
                var tileImages = new[]
                {
                    tiles[tileData[startIndex]],
                    tiles[tileData[startIndex + 1]],
                    tiles[tileData[startIndex + 2]],
                    tiles[tileData[startIndex + 3]],
                };
                var block = UtilsGDI.GlueImages(tileImages, 2, 2);
                result[i] = block;

                if (curViewType == MapViewType.ObjNumbers)
                    result[i] = VideoHelper.addObjNumber(result[i], i);
            }
            return result;
        }

        public Color[] getPalette(byte[] pal)
        {
            var colors = new[]
            {
                Color.FromArgb(5, 37, 5),
                Color.FromArgb(29, 85, 29),
                Color.FromArgb(136, 169, 5),
                Color.FromArgb(164, 197, 5),
            };
            return new[] {colors[pal[0]], colors[pal[1]], colors[pal[2]], colors[pal[3]]};
        }

        public Bitmap getTile(byte[] ppuData, Color[] palette, int no)
        {
            Bitmap retn = new Bitmap(8, 8, PixelFormat.Format24bppRgb);
            BitmapData data = retn.LockBits(new Rectangle(0, 0, retn.Width, retn.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int stride = data.Stride;
            unsafe
            {
                int curIndex = no * 16;
                byte* ptr = (byte*)data.Scan0;
                for (int h = 0; h < 8; h++)
                {
                    var b1 = ppuData[curIndex++];
                    var b2 = ppuData[curIndex++];
                    var colorIndexes = new int[]
                    {
                        (((b1 >> 7) & 0x1) << 1) | ((b2 >> 7) & 0x1),
                        (((b1 >> 6) & 0x1) << 1) | ((b2 >> 6) & 0x1),
                        (((b1 >> 5) & 0x1) << 1) | ((b2 >> 5) & 0x1),
                        (((b1 >> 4) & 0x1) << 1) | ((b2 >> 4) & 0x1),
                        (((b1 >> 3) & 0x1) << 1) | ((b2 >> 3) & 0x1),
                        (((b1 >> 2) & 0x1) << 1) | ((b2 >> 2) & 0x1),
                        (((b1 >> 1) & 0x1) << 1) | ((b2 >> 1) & 0x1),
                        (((b1 >> 0) & 0x1) << 1) | ((b2 >> 0) & 0x1),
                    };
                    for (int w = 0; w < 8; w++)
                    {
                        var color = palette[colorIndexes[w]];
                        ptr[(w * 3 + 0) + h * stride] = color.B;
                        ptr[(w * 3 + 1) + h * stride] = color.G;
                        ptr[(w * 3 + 2) + h * stride] = color.R;
                    }
                }
            }
            retn.UnlockBits(data);
            return retn;
        }

        public Bitmap getTilesRectangle(byte[] ppuData, Color[] palette)
        {
            var tiles = new Image[256];
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = getTile(ppuData, palette, i);
            }
            return UtilsGDI.GlueImages(tiles, 16, 16);
        }
    }
}
