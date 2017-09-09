using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CadEditor
{
    public static class VideoHelper
    {
        public static Bitmap emptyScreen(int w, int h, bool withBorder = true)
        {
            var b = new Bitmap(w, h);
            using (var g = Graphics.FromImage(b))
            {
                g.FillRectangle(Brushes.Black, new Rectangle(0, 0, w, h));
                if (withBorder)
                    g.DrawRectangle(new Pen(Color.Green, w / 32), new Rectangle(0, 0, w, h));
            }
            return b;
        }
        public static Image addObjNumber(Image source, int no)
        {
            using (Graphics g = Graphics.FromImage(source))
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(192, 255, 255, 255)), new Rectangle(0, 0, source.Width, source.Height));
                g.DrawString(String.Format("{0:X}", no), new Font("Arial", source.Width/4), Brushes.Red, new Point(0, 0));
            }
            return source;
        }

        /*public static Image addAxisRectangle(Image source)
        {
            using (Graphics g = Graphics.FromImage(source))
                g.DrawRectangle(new Pen(Color.FromArgb(255, 255, 255, 255)), new Rectangle(0, 0, source.Width, source.Height));
            return source;
        }*/
    }

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
            //if (idx >= 0x1F0 && idx <= 0x1FF) tilesPos += 0x200;
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
            for (int i = 0, j = 0; i < len; i++, j += 2)
            {
                LevelMapping[j] = (byte)((ChangedMapping[i] & 0xFF00) >> 8);
                LevelMapping[j + 1] = (byte)(ChangedMapping[i] & 0xFF);
            }
        }

        public static ushort[] LoadMapping(byte[] LevelMapping)
        {
            int len = LevelMapping.Length / 2;
            ushort[] retn = new ushort[len];
            for (int i = 0, j = 0; i < len; i++, j += 2)
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
