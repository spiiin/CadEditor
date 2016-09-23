using System;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Collections.Generic;

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

        public static Image ResizeBitmap(Image sourceBMP, int width, int height)
        {
            Image result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
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
                    int imBlockWidth = blockWidth * 2; //default scale
                    int imBlockHeight = blockHeight * 2;
                    int imCountX = imSrc.Width / imBlockWidth;
                    int imCountY = imSrc.Height / imBlockHeight;
                    for (int y = 0; y < imCountY; y++)
                    {
                        for (int x = 0; x < imCountX; x++)
                        {
                            var imBlock = CropImage(imSrc, new Rectangle(x * imBlockWidth, y * imBlockHeight, imBlockWidth, imBlockHeight));
                            var imResized = ResizeBitmap(imSrc, (int)(curButtonScale * blockWidth), (int)(curButtonScale * blockHeight));
                            bigBlocks.Images.Add(imBlock);
                        }
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
    }
}
