using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CadEditor
{
    //---------------------------------------------------------------------------------------------
    static public class VideoSega
    {
        public static Image[] makeBigBlocksSega(byte[] mapping, byte[] tiles, byte[] palette, int count, float zoom, MapViewType curViewType = MapViewType.Tiles, bool showAxis = false)
        {
            var result = new Image[count];
            ushort[] m = Mapper.LoadMapping(mapping);
            Color[] cpal = GetPalette(palette);
            for (ushort i = 0; i < count; i++)
            {
                result[i] = GetZoomBlock(m, tiles, cpal, i, zoom*2.0f);
                if (curViewType == MapViewType.ObjNumbers)
                  result[i] = addObjNumber(result[i], i);
                if (showAxis)
                    result[i] = addAxisRectangle(result[i]);
            }
            return result;
        }

        public static Color[] GetPalette(byte[] pal)
        {
            Color[] retn = new Color[0x40];
            int offset = 0;
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 16; x++, offset += 2)
                {
                    ushort W = pal.ReadUInt16BE(offset);
                    byte r = (byte)((W & 0xE) * 0x10);
                    byte g = (byte)(((W / 0x10) & 0xE) * 0x10);
                    byte b = (byte)(((W / 0x100) & 0xE) * 0x10);
                    retn[y * 16 + x] = Color.FromArgb(r, g, b);

                }
            retn[0x00] =
            retn[0x10] =
            retn[0x20] =
            retn[0x30] = Color.FromArgb(0xFF, 0xDC, 0xDC);
            return retn;
        }

        //
        public static Bitmap GetTileFromArray(byte[] Tiles, ref int Position, Color[] Palette, byte PalIndex)
        {
            Bitmap retn = new Bitmap(8, 8, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);

            for (int h = 0; h < 8; h++)
                for (int w = 0; w < 4; w++)
                {
                    byte B = Tiles[Position++];

                    retn.SetPixel(w * 2, h, Palette[PalIndex * 0x10 + (byte)((B & 0xF0) >> 4)]);
                    retn.SetPixel(w * 2 + 1, h, Palette[PalIndex * 0x10 + (byte)(B & 0xF)]);
                }
            return retn;
        }

        public static Bitmap GetTileFrom2ColorArray(byte[] Tiles, ref int Position)
        {
            Bitmap retn = new Bitmap(8, 8, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);

            for (int h = 0; h < 8; h++)
                for (int w = 0; w < 4; w++)
                {
                    retn.SetPixel(w * 2, h, (Tiles[Position] & 0xF0) == 0 ? Color.Black : Color.White);
                    retn.SetPixel(w * 2 + 1, h, (Tiles[Position] & 0x0F) == 0 ? Color.Black : Color.White);
                    Position++;
                }
            return retn;
        }

        public static byte[] GetArrayFrom2ColorTile(Bitmap tile)
        {
            byte[] retn = new byte[0x20];

            for (int h = 0, i = 0; h < 8; h++)
                for (int w = 0; w < 4; w++, i++)
                {
                    retn[i] = (byte)(tile.GetPixel(w * 2, h).ToArgb() == Color.Black.ToArgb() ? 0 : 0xF0);
                    retn[i] |= (byte)(tile.GetPixel(w * 2 + 1, h).ToArgb() == Color.Black.ToArgb() ? 0 : 0x0F);
                }

            return retn;
        }

        public static byte[] GetArrayFrom2ColorBlock(Bitmap block)
        {
            byte[] retn = new byte[block.Width * block.Height / 2];

            for (int y = 0, pos = 0; y < block.Height / 8; y++)
                for (int x = 0; x < block.Width / 8; x++, pos += 0x20)
                {
                    Bitmap tile = new Bitmap(8, 8, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
                    using (Graphics g = Graphics.FromImage(tile)) g.DrawImage(block, new Rectangle(0, 0, 8, 8), new Rectangle(x * 8, y * 8, 8, 8), GraphicsUnit.Pixel);
                    Array.Copy(GetArrayFrom2ColorTile(tile), 0, retn, pos, 0x20);
                }

            return retn;
        }

        public static Bitmap GetZoomBlockFrom2ColorArray(byte[] Tiles, int Width, float Zoom)
        {
            int Height = Tiles.Length / (0x20 * Width);
            Bitmap block = new Bitmap(Width * 8, Height * 8);
            Bitmap retn = new Bitmap((int)(Width * Zoom * 8), (int)(Height * Zoom * 8));

            for (int y = 0, pos = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                {
                    Bitmap tile = GetTileFrom2ColorArray(Tiles, ref pos);
                    using (Graphics g = Graphics.FromImage(block)) g.DrawImage(tile, new Rectangle(x * 8, y * 8, 8, 8));
                }

            Graphics gr = Graphics.FromImage(retn);
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            gr.DrawImage(block, new Rectangle(0, 0, retn.Width, retn.Height));
            gr.Dispose();

            return retn;
        }

        private static Bitmap GetTile(byte[] Tiles, ushort Word, Color[] Palette, byte PalIndex, bool HF, bool VF)
        {
            int pos = Mapper.TilePos(Word);
            Bitmap retn = GetTileFromArray(Tiles, ref pos, Palette, PalIndex);

            if (HF) retn.RotateFlip(RotateFlipType.RotateNoneFlipX);
            if (VF) retn.RotateFlip(RotateFlipType.RotateNoneFlipY);

            return retn;
        }

        public static Bitmap GetZoomTile(byte[] tiles, ushort Word, Color[] palette, byte palIndex, bool HF, bool VF, float zoom)
        {
            Bitmap tile = GetTile(tiles, Word, palette, palIndex, HF, VF);
            Bitmap retn = new Bitmap((int)(8 * zoom), (int)(8 * zoom), System.Drawing.Imaging.PixelFormat.Format16bppRgb555);

            Graphics gr = Graphics.FromImage(retn);
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            if (zoom > 1.0f)
                gr.DrawImage(tile, new Rectangle(0, 0, retn.Width + 2, retn.Height + 2));
            else
                gr.DrawImage(tile, new Rectangle(0, 0, retn.Width, retn.Height));
            gr.Dispose();
            return retn;
        }

        private static Bitmap GetBlock(ushort[] mapping, byte[] tiles, Color[] palette, byte Index)
        {
            Bitmap block = new Bitmap(32, 32, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
            for (int y = 0; y < 4; y++)
                for (int x = 0; x < 4; x++)
                {
                    ushort Word = mapping[Index * 0x10 + y * 4 + x];
                    byte palIndex = Mapper.PalIdx(Word);
                    bool HF = Mapper.HF(Word);
                    bool VF = Mapper.VF(Word);

                    Bitmap tile = GetTile(tiles, Word, palette, palIndex, HF, VF);

                    using (Graphics g = Graphics.FromImage(block)) g.DrawImage(tile, new Rectangle(x * 8, y * 8, 8, 8));
                }
            return block;
        }

        public static Bitmap GetZoomBlock(ushort[] mapping, byte[] tiles, Color[] palette, int Index, float zoom)
        {
            Bitmap block = new Bitmap(16, 16, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
            Bitmap retn = new Bitmap((int)(16 * zoom), (int)(16 * zoom), System.Drawing.Imaging.PixelFormat.Format16bppRgb555);

            for (int y = 0; y < 2; y++)
                for (int x = 0; x < 2; x++)
                {
                    ushort Word = mapping[Index * 4 + y * 2 + x];
                    byte palIndex = Mapper.PalIdx(Word);
                    bool HF = Mapper.HF(Word);
                    bool VF = Mapper.VF(Word);

                    Bitmap tile = GetZoomTile(tiles, Word, palette, palIndex, HF, VF, zoom);
                    using (Graphics g = Graphics.FromImage(block)) g.DrawImage(tile, new Rectangle(x * 8, y * 8, 8, 8));
                }

            Graphics gr = Graphics.FromImage(retn);
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            if (zoom > 1.0f)
                gr.DrawImage(block, new Rectangle(0, 0, retn.Width + 2, retn.Height + 2));
            else
                gr.DrawImage(block, new Rectangle(0, 0, retn.Width, retn.Height));
            gr.Dispose();
            return retn;
        }

        public static Image addObjNumber(Image source, int no)
        {
            using (Graphics g = Graphics.FromImage(source))
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(192, 255, 255, 255)), new Rectangle(0, 0, source.Width, source.Height));
                g.DrawString(String.Format("{0:X}", no), new Font("Arial", 16), Brushes.Red, new Point(0, 0));
            }
            return source;
        }

        public static Image addAxisRectangle(Image source)
        {
            using (Graphics g = Graphics.FromImage(source))
                g.DrawRectangle(new Pen(Color.FromArgb(255, 255, 255, 255)), new Rectangle(0, 0, source.Width, source.Height));
            return source;
        }

        /*public static Bitmap GetZoomBlock1x1(ushort[] mapping, byte[] tiles, Color[] pallete, int Index, float zoom)
        {
            Bitmap block = new Bitmap(8, 8, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
            Bitmap retn = new Bitmap((int)(8 * zoom), (int)(8 * zoom), System.Drawing.Imaging.PixelFormat.Format16bppRgb555);

            ushort Word = mapping[Index];
            byte palIndex = Mapper.PalIdx(Word);
            bool HF = Mapper.HF(Word);
            bool VF = Mapper.VF(Word);

            Bitmap tile = GetZoomTile(tiles, Word, pallete, palIndex, HF, VF, zoom);

            using (Graphics g = Graphics.FromImage(block)) g.DrawImage(tile, new Rectangle (8, 8, 8, 8));

            Graphics gr = Graphics.FromImage(retn);
            gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            gr.DrawImage(block, new Rectangle(0, 0, retn.Width, retn.Height));
            gr.Dispose();
            return retn;
        }*/
    }
    //---------------------------------------------------------------------------------------------

    //---------------------------------------------------------------------------------------------
    public static class Mapper
    {
        public static ushort TileIdx(ushort Word)
        {
            return (ushort)(Word & 0x7FF);
        }

        public static byte PalIdx(ushort Word)
        {
            return (byte)((Word & 0x6000) >> 13);
        }

        public static bool HF(ushort Word)
        {
            return ((Word & 0x800) >> 11) == 1;
        }

        public static bool VF(ushort Word)
        {
            return ((Word & 0x1000) >> 12) == 1;
        }

        public static bool P(ushort Word)
        {
            return ((Word & 0x8000) >> 15) == 1;
        }

        public static ushort ApplyTileIdx(ushort Word, ushort tileIdx)
        {
            return (ushort)((Word & ~0x07FF) | tileIdx);
        }

        public static ushort ApplyPalIdx(ushort Word, byte palIdx)
        {
            return (ushort)((Word & ~0x6000) | (palIdx << 13));
        }

        public static ushort ApplyHF(ushort Word, int hf)
        {
            return (ushort)((Word & ~0x0800) | (hf << 11));
        }

        public static ushort ApplyVF(ushort Word, int vf)
        {
            return (ushort)((Word & ~0x1000) | (vf << 12));
        }

        public static ushort ApplyP(ushort Word, int p)
        {
            return (ushort)((Word & ~0x8000) | (p << 15));
        }

        public static ushort TilePos(ushort Word)
        {
            ushort idx = TileIdx(Word);
            ushort tilesPos = (ushort)(idx * 0x20);
            if (idx >= 0x1F0 && idx <= 0x1FF) tilesPos += 0x200;
            return tilesPos;
        }

        public static ushort EncodeTileInfo(ushort idx, bool hf, bool vf, byte PalIndex)
        {
            int retn = ((PalIndex & 3) << 13) | ((vf ? 1 : 0) << 12) | ((hf ? 1 : 0) << 11) | (idx & 0x7FF);
            return (ushort)retn;
        }

        public static void ApplyMapping(ref byte[] LevelMapping, ushort[] ChangedMapping)
        {
            int len = ChangedMapping.Length;
            for (int i = 0, j =  0; i < len; i++, j += 2)
            {
                LevelMapping[j] = (byte)((ChangedMapping[i] & 0xFF00) >> 8);
                LevelMapping[j + 1] = (byte)(ChangedMapping[i] & 0xFF);
            }
        }

        public static ushort[] LoadMapping(byte[] LevelMapping)
        {
            int len = LevelMapping.Length/2;
            ushort[] retn = new ushort[len];
            for (int i = 0, j = 0 ; i < len; i++, j += 2)
                retn[i] = (ushort)(((LevelMapping[j] << 8) | LevelMapping[j + 1]) & 0xFFFF);
            return retn;
        }
    }

    public static class Helpers
    {
        public static UInt16 ReadUInt16BE(this byte[] array, int index)
        {
            return (ushort)((array[index] << 8) | array[index + 1]);
        }
    }
}
