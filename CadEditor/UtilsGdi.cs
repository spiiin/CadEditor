using System;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace CadEditor
{
    public static class UtilsGDI
    {

        public static Image CropImage(Image source, Rectangle rect)
        {
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            using (var g = Graphics.FromImage(bmp))
                g.DrawImage(source, 0, 0, rect, GraphicsUnit.Pixel);
            return bmp;
        }

        public static Image ResizeBitmap(Image image, int width, int height)
        {
            if ((image.Width == width) && (image.Height == height))
            {
                return image;
            }
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            try
            {
                destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            }
            catch (ArgumentException ex)
            {
                //exception throw on Mono.
            }

            using (var graphics = Graphics.FromImage(destImage))
            {
                /*graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;*/

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }

        //glue image together
        public static Bitmap GlueImages(Image[] images, int width, int height)
        {
            int totalImageWidth = 0;
            int totalImageHeight = 0;
            for (int x = 0; x < width; x++)
            {
                totalImageWidth += images[x].Width;
            }
            for (int y = 0; y < height; y++)
            {
                totalImageHeight += images[y * width].Height;
            }

            var totalImage = new Bitmap(totalImageWidth, totalImageHeight);
            using (var g = Graphics.FromImage(totalImage))
            {
                int currentY = 0;
                for (int y = 0; y < height; y++)
                {
                    int currentX = 0;
                    int currentMaxY = 0;
                    for (int x = 0; x < width; x++)
                    {
                        var imIndex = y * width + x;
                        if (imIndex < images.Length)
                        {
                            var im = images[imIndex];
                            g.DrawImage(im, new Point(currentX, currentY));
                            currentX += im.Width;
                            currentMaxY = Math.Max(currentMaxY, im.Height);
                            if (x == width - 1)
                            {
                                currentY += currentMaxY;
                            }
                        }
                    }
                }
            }
            return totalImage;
        }

        public static bool CompareBitmaps(Image left, Image right)
        {
            if (object.Equals(left, right))
                return true;
            if (left == null || right == null)
                return false;
            if (!left.Size.Equals(right.Size) || !left.PixelFormat.Equals(right.PixelFormat))
                return false;

            Bitmap leftBitmap = left as Bitmap;
            Bitmap rightBitmap = right as Bitmap;
            if (leftBitmap == null || rightBitmap == null)
                return true;

            int bytes = left.Width * left.Height * (Image.GetPixelFormatSize(left.PixelFormat) / 8);

            bool result = true;
            byte[] b1bytes = new byte[bytes];
            byte[] b2bytes = new byte[bytes];

            BitmapData bmd1 = leftBitmap.LockBits(new Rectangle(0, 0, leftBitmap.Width - 1, leftBitmap.Height - 1), ImageLockMode.ReadOnly, leftBitmap.PixelFormat);
            BitmapData bmd2 = rightBitmap.LockBits(new Rectangle(0, 0, rightBitmap.Width - 1, rightBitmap.Height - 1), ImageLockMode.ReadOnly, rightBitmap.PixelFormat);

            Marshal.Copy(bmd1.Scan0, b1bytes, 0, bytes);
            Marshal.Copy(bmd2.Scan0, b2bytes, 0, bytes);

            for (int n = 0; n <= bytes - 1; n++)
            {
                if (b1bytes[n] != b2bytes[n])
                {
                    result = false;
                    break;
                }
            }

            leftBitmap.UnlockBits(bmd1);
            rightBitmap.UnlockBits(bmd2);

            return result;
        }

        private static ushort[][,] ConvertBitmapToArray(Bitmap bmp)
        {

            ushort[][,] array = new ushort[4][,];

            for (int i = 0; i < 4; i++)
                array[i] = new ushort[bmp.Width, bmp.Height];

            BitmapData bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            int nOffset = (bd.Stride - bd.Width * 4);

            unsafe
            {

                byte* p = (byte*)bd.Scan0;

                for (int y = 0; y < bd.Height; y++)
                {
                    for (int x = 0; x < bd.Width; x++)
                    {

                        array[3][x, y] = (ushort)p[3];
                        array[2][x, y] = (ushort)p[2];
                        array[1][x, y] = (ushort)p[1];
                        array[0][x, y] = (ushort)p[0];

                        p += 4;
                    }

                    p += nOffset;
                }
            }
            bmp.UnlockBits(bd);
            return array;
        }

        private static Bitmap ConvertArrayToBitmap(ushort[][,] array)
        {

            int width = array[0].GetLength(0);
            int height = array[0].GetLength(1);

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            BitmapData bd = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            int nOffset = (bd.Stride - bd.Width * 4);

            unsafe
            {

                byte* p = (byte*)bd.Scan0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {

                        p[3] = (byte)Math.Min(Math.Max(array[3][x, y], Byte.MinValue), Byte.MaxValue);
                        p[2] = (byte)Math.Min(Math.Max(array[2][x, y], Byte.MinValue), Byte.MaxValue);
                        p[1] = (byte)Math.Min(Math.Max(array[1][x, y], Byte.MinValue), Byte.MaxValue);
                        p[0] = (byte)Math.Min(Math.Max(array[0][x, y], Byte.MinValue), Byte.MaxValue);

                        p += 4;
                    }

                    p += nOffset;
                }
            }

            bmp.UnlockBits(bd);

            return bmp;
        }

        public static Image[] setBlocksForPictures(float curButtonScale = 2, int blockWidth = 32, int blockHeight = 32, MapViewType curDrawType = MapViewType.Tiles)
        {
            //only if using pictures
            if (!ConfigScript.usePicturesInstedBlocks || ConfigScript.getBlocksPicturesFilename() == "")
            {
                return null;
            }
            MapViewType curViewType = curDrawType;
            var imSrc = Image.FromFile(ConfigScript.getBlocksPicturesFilename());
            int imBlockWidth = blockWidth * 2; //default scale
            int imBlockHeight = blockHeight * 2;
            int imCountX = imSrc.Width / imBlockWidth;
            int imCountY = imSrc.Height / imBlockHeight;

            var bigBlocks = new Image[imCountX*imCountY];
            for (int i = 0; i < bigBlocks.Length; i++)
            {
                bigBlocks[i] = new Bitmap((int)(curButtonScale * blockWidth), (int)(curButtonScale * blockHeight));
            }

            for (int y = 0; y < imCountY; y++)
            {
                for (int x = 0; x < imCountX; x++)
                {
                    var imBlock = CropImage(imSrc, new Rectangle(x * imBlockWidth, y * imBlockHeight, imBlockWidth, imBlockHeight));
                    var imResized = ResizeBitmap(imBlock, (int)(curButtonScale * blockWidth), (int)(curButtonScale * blockHeight));
                    bigBlocks[y*imCountX + x] = imResized;
                }
            }

            if (curViewType == MapViewType.ObjNumbers)
            {
                int _bbRectSizeX = (int)((blockWidth / 2) * curButtonScale);
                int _bbRectSizeY = (int)((blockHeight / 2) * curButtonScale);
                for (int i = 0; i < 256; i++)
                {
                    var im1 = bigBlocks[i];
                    using (var g = Graphics.FromImage(im1))
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(192, 255, 255, 255)), new Rectangle(0, 0, _bbRectSizeX * 2, _bbRectSizeY * 2));
                        g.DrawString(String.Format("{0:X}", i), new Font("Arial", 16), Brushes.Red, new Point(0, 0));
                    }
                    bigBlocks[i] = im1;
                }
            }
            return bigBlocks;
        }
    }
}
