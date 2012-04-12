using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CadEditor
{
    static class Video
    {
        static Video()
        {
            NesColors[0] = Color.FromArgb( 124,124,124);
            NesColors[1] = Color.FromArgb(0,0,252);
            NesColors[2] = Color.FromArgb(0,0,188);
            NesColors[3] = Color.FromArgb(68,40,188);
            NesColors[4] = Color.FromArgb(148,0,132);
            NesColors[5] = Color.FromArgb(168,0,32);
            NesColors[6] = Color.FromArgb(168,16,0);
            NesColors[7] = Color.FromArgb(136,20,0);
            NesColors[8] = Color.FromArgb(80,48,0);
            NesColors[9] = Color.FromArgb(0,120,0);
            NesColors[0xA] = Color.FromArgb(0,104,0);
            NesColors[0xB] = Color.FromArgb(0,88,0);
            NesColors[0xC] = Color.FromArgb(0,64,88);
            NesColors[0xD] = Color.FromArgb(0, 0, 0);
            NesColors[0xE] = Color.FromArgb(0, 0, 0);
            NesColors[0xF] = Color.FromArgb(0, 0, 0);

            NesColors[0x10] = Color.FromArgb(188,188,188);
            NesColors[0x11] = Color.FromArgb(0,120,248);
            NesColors[0x12] = Color.FromArgb(0, 88, 248);
            NesColors[0x13] = Color.FromArgb(104, 68, 252);
            NesColors[0x14] = Color.FromArgb(216, 0, 204);
            NesColors[0x15] = Color.FromArgb(228, 0, 88);
            NesColors[0x16] = Color.FromArgb(248, 56, 0);
            NesColors[0x17] = Color.FromArgb(228, 92, 16);
            NesColors[0x18] = Color.FromArgb(172, 124, 0);
            NesColors[0x19] = Color.FromArgb(0, 184, 0);
            NesColors[0x1A] = Color.FromArgb(0, 168, 0);
            NesColors[0x1B] = Color.FromArgb(0, 168, 68);
            NesColors[0x1C] = Color.FromArgb(0, 136, 136);
            NesColors[0x1D] = Color.FromArgb(0, 0, 0);
            NesColors[0x1E] = Color.FromArgb(0, 0, 0);
            NesColors[0x1F] = Color.FromArgb(0, 0, 0);

            NesColors[0x20] = Color.FromArgb(248,248,248);
            NesColors[0x21] = Color.FromArgb(60, 188, 252);
            NesColors[0x22] = Color.FromArgb(104, 136, 252);
            NesColors[0x23] = Color.FromArgb(152, 120, 248);
            NesColors[0x24] = Color.FromArgb(248, 120, 248);
            NesColors[0x25] = Color.FromArgb(248, 88, 152);
            NesColors[0x26] = Color.FromArgb(248, 120, 88);
            NesColors[0x27] = Color.FromArgb(252, 160, 68);
            NesColors[0x28] = Color.FromArgb(248, 184, 0);
            NesColors[0x29] = Color.FromArgb(184, 248, 24);
            NesColors[0x2A] = Color.FromArgb(88, 216, 84);
            NesColors[0x2B] = Color.FromArgb(88,248,152);
            NesColors[0x2C] = Color.FromArgb(0, 232, 216);
            NesColors[0x2D] = Color.FromArgb(120, 120, 120);
            NesColors[0x2E] = Color.FromArgb(0, 0, 0);
            NesColors[0x2F] = Color.FromArgb(0, 0, 0);

            NesColors[0x30] = Color.FromArgb(252,252,252);
            NesColors[0x31] = Color.FromArgb(164, 228, 252);
            NesColors[0x32] = Color.FromArgb(184, 184, 248);
            NesColors[0x33] = Color.FromArgb(216,184,248);
            NesColors[0x34] = Color.FromArgb(248, 184, 248);
            NesColors[0x35] = Color.FromArgb(248, 164, 192);
            NesColors[0x36] = Color.FromArgb(240,208,176);
            NesColors[0x37] = Color.FromArgb(252, 224, 168);
            NesColors[0x38] = Color.FromArgb(248, 216, 120);
            NesColors[0x39] = Color.FromArgb(216, 248, 120);
            NesColors[0x3A] = Color.FromArgb(184,248,184);
            NesColors[0x3B] = Color.FromArgb(184, 248, 216);
            NesColors[0x3C] = Color.FromArgb(0, 252, 252);
            NesColors[0x3D] = Color.FromArgb(248, 216, 248);
            NesColors[0x3E] = Color.FromArgb(0, 0, 0);
            NesColors[0x3F] = Color.FromArgb(0, 0, 0);

            CadObjectTypeColors[0x0] = Color.FromArgb(196, 0, 255, 0);
            CadObjectTypeColors[0x1] = Color.FromArgb(196, 0, 255, 0);
            CadObjectTypeColors[0x2] = Color.FromArgb(196, 0, 196, 0);
            CadObjectTypeColors[0x3] = Color.FromArgb(196, 255, 0,0);
            CadObjectTypeColors[0x4] = Color.FromArgb(196, 255, 0, 0);
            CadObjectTypeColors[0x5] = Color.FromArgb(196, 255, 0, 0);
            CadObjectTypeColors[0x6] = Color.FromArgb(196, 0, 255, 0);
            CadObjectTypeColors[0x7] = Color.FromArgb(196, 255, 255, 0);
            CadObjectTypeColors[0x8] = Color.FromArgb(196, 255, 0, 0);
            CadObjectTypeColors[0x9] = Color.FromArgb(196, 255, 0, 0);
            CadObjectTypeColors[0xA] = Color.FromArgb(196, 255, 0, 0);
            CadObjectTypeColors[0xB] = Color.FromArgb(196, 0, 0, 0);
            CadObjectTypeColors[0xC] = Color.FromArgb(196, 255, 0, 0);
            CadObjectTypeColors[0xD] = Color.FromArgb(196, 255, 0, 0);
            CadObjectTypeColors[0xE] = Color.FromArgb(196, 0, 255, 255);
            CadObjectTypeColors[0xF] = Color.FromArgb(196, 0, 255, 255);
        }

        public static Bitmap makeImageStrip(byte[] videoChunk, byte[] pallete, int subPalIndex, int scale)
        {
            Bitmap res = new Bitmap(16 * CHUNK_COUNT, 16);
            using (Graphics g = Graphics.FromImage(res))
            {
                for (int i = 0; i < CHUNK_COUNT; i++)
                {
                    Bitmap onePic = new Bitmap(8, 8);
                    int beginIndex = 16 * i;
                    for (int line = 0; line < 8; line++)
                    {
                        for (int pixel = 0; pixel < 8; pixel++)
                        {
                            bool bitLo = getBit(videoChunk[beginIndex + line], 8-pixel);
                            bool bitHi = getBit(videoChunk[beginIndex + line+8], 8-pixel);
                            int palIndex = mixBits(bitHi, bitLo);
                            Color c = NesColors[pallete[subPalIndex * 4 +palIndex]];
                            onePic.SetPixel(pixel, line, c);
                        }
                    }
                    g.DrawImage(onePic, new Rectangle(i * 8 * scale, 0, 8*scale, 8*scale));
                }
            }
            return res;
        }

        //chip and dale specific
        public static Bitmap makeObjectsStrip(byte videoPageId, byte tilesId, byte palId, int scale, bool drawType)
        {
            byte[] videoChunk = new byte[Globals.VIDEO_PAGE_SIZE];
            int videoAddr = Globals.getVideoPageAddr(videoPageId);
            for (int i = 0; i < Globals.VIDEO_PAGE_SIZE; i++)
                videoChunk[i] = Globals.romdata[videoAddr + i];

            ObjRec[] objects = new ObjRec[256];
            int addr = Globals.getTilesAddr(tilesId);
            for (int i = 0; i < Globals.OBJECTS_COUNT; i++)
            {
                byte c1 = Globals.romdata[addr + i];
                byte c2 = Globals.romdata[addr + 0x100 + i];
                byte c3 = Globals.romdata[addr + 0x200 + i];
                byte c4 = Globals.romdata[addr + 0x300 + i];
                byte typeColor = Globals.romdata[addr + 0x400 + i];
                objects[i] = new ObjRec(c1, c2, c3, c4, typeColor);
            }

            int addrPal = Globals.getPalAddr(palId);
            byte[] palette = new byte[Globals.PAL_LEN];
            for (int i = 0; i < Globals.PAL_LEN; i++)
                palette[i] = Globals.romdata[addrPal + i];

            var objStrip1 = makeImageStrip(videoChunk, palette, 0, scale);
            var objStrip2 = makeImageStrip(videoChunk, palette, 1, scale);
            var objStrip3 = makeImageStrip(videoChunk, palette, 2, scale);
            var objStrip4 = makeImageStrip(videoChunk, palette, 3, scale);
            var objStrips = new[] { objStrip1, objStrip2, objStrip3, objStrip4 };
            Bitmap res = new Bitmap(16 * Globals.OBJECTS_COUNT * scale, 16 * scale);
            using (Graphics g = Graphics.FromImage(res))
            {
                for (int i = 0; i < 256; i++)
                {
                    var mblock = new Bitmap(16 * scale, 16 * scale);
                    var co = objects[i];
                    var curStrip = objStrips[co.getSubpallete()];
                    using (Graphics g2 = Graphics.FromImage(mblock))
                    {
                        g2.DrawImage(curStrip, new Rectangle(0, 0, 8 * scale, 8 * scale), new Rectangle(co.c1 * 8 * scale, 0, 8 * scale, 8 * scale), GraphicsUnit.Pixel);
                        g2.DrawImage(curStrip, new Rectangle(8 * scale, 0, 8 * scale, 8 * scale), new Rectangle(co.c2 * 8 * scale, 0, 8 * scale, 8 * scale), GraphicsUnit.Pixel);
                        g2.DrawImage(curStrip, new Rectangle(0, 8 * scale, 8 * scale, 8 * scale), new Rectangle(co.c3 * 8 * scale, 0, 8 * scale, 8 * scale), GraphicsUnit.Pixel);
                        g2.DrawImage(curStrip, new Rectangle(8 * scale, 8 * scale, 8 * scale, 8 * scale), new Rectangle(co.c4 * 8 * scale, 0, 8 * scale, 8 * scale), GraphicsUnit.Pixel);
                        if (drawType)
                        {
                            g2.FillRectangle(new SolidBrush(CadObjectTypeColors[co.getType()]), new Rectangle(0, 0, 16 * scale, 16 * scale));
                            g2.DrawString(String.Format("{0:X}", co.getType()), new Font("Arial", 6), Brushes.White, new Point(0, 0));
                        }
                    }
                    g.DrawImage(mblock, new Rectangle(i*16*scale, 0, 16*scale, 16*scale));
                }
            }
            return res;

        }

        public static byte[] getScreen(int screenIndex)
        {
            var result = new byte[SCREEN_SIZE];
            int beginAddr = 0x10 + screenIndex * 0x40;
            for (int i = 0; i < SCREEN_SIZE; i++)
                result[i] = Globals.romdata[beginAddr + i];
            return result;
        }

        private static bool getBit(byte b, int bit)
        {
            return (b & (1 << bit - 1)) != 0;
        }

        private static int mixBits(bool hi, bool lo)
        {
            return (hi?1:0) << 1 |(lo?1:0);
        }

        public static int NES_COLORS_COUNT = 64;
        public static int CHUNK_COUNT = 256;
        public static Color[] NesColors = new Color[NES_COLORS_COUNT];

        const int CAD_OBJTYPES_COUNT = 16;
        public static Color[] CadObjectTypeColors = new Color[CAD_OBJTYPES_COUNT];

        const int SCREEN_SIZE = 64;
    }
}
