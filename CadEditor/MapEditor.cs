using System;
using System.Drawing;
using System.Windows.Forms;

namespace CadEditor
{
    public class MapEditor
    {
        public static void render(Graphics g, Image[] bigBlocks, Rectangle? visibleRect,Screen[] screens, int scrNo, float curScale, bool showBorder, bool showBlocksAxis, int leftMargin, int topMargin, int width, int height, bool additionalRenderEnabled)
        {
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            int size = width * height;

            for (int layerIndex = 0; layerIndex < screens[scrNo].layers.Length; layerIndex++)
            {
                var layer = screens[scrNo].layers[layerIndex];
                bool needRenderLayer = layer != null && layer.showLayer;
                if (!needRenderLayer)
                {
                    continue;
                }

                int tileSizeX = (int)(bigBlocks[0].Width* curScale);
                int tileSizeY = (int)(bigBlocks[0].Height* curScale);

                for (int i = 0; i < size; i++)
                {
                    int bigBlockNo = ConfigScript.getBigTileNoFromScreen(layer.data, i);
                    Rectangle tileRect = new Rectangle((i % width) * tileSizeX + leftMargin, i / width * tileSizeY + topMargin, tileSizeX, tileSizeY);

                    if (visibleRect == null || visibleRect.Value.Contains(tileRect) || visibleRect.Value.IntersectsWith(tileRect))
                    {
                        if (bigBlockNo > -1 && bigBlockNo < bigBlocks.Length)
                        {
                            g.DrawImage(bigBlocks[bigBlockNo], tileRect);
                            if (showBlocksAxis)
                            {
                                g.DrawRectangle(new Pen(Color.FromArgb(255, 255, 255, 255)), tileRect);
                            }
                        }
                        //else
                        //    g.FillRectangle(Brushes.White, tileRect);
                    }
                }
            }

            if (showBorder)
            {
                int tileSizeX = (int)(bigBlocks[0].Width * curScale);
                int tileSizeY = (int)(bigBlocks[0].Height * curScale);
                g.DrawRectangle(new Pen(Color.Green, 4.0f), new Rectangle(tileSizeX, 0, tileSizeX * width, tileSizeY * height));
            }

            //Additional rendering  //float to int!
            if (additionalRenderEnabled)
            {
                ConfigScript.renderToMainScreen(g, (int) curScale, scrNo);
            }
        }

        public static void renderAllBlocks(Graphics g, PictureBox parentControl, Image[] bigBlocks, int blockWidth, int blockHeight, Rectangle? visibleRect, float curScale, int activeBlock, bool showBlocksAxis)
        {
            int tileSizeX = (int)(blockWidth * curScale);
            int tileSizeY = (int)(blockHeight * curScale);
            int width = parentControl.Width / tileSizeX;
            if (width == 0)
            {
                return;
            }

            for (int i = 0; i < bigBlocks.Length; i++)
            {
                int bigBlockNo = i;
                Rectangle tileRect = new Rectangle((i % width) * tileSizeX, i / width * tileSizeY, tileSizeX, tileSizeY);

                if (visibleRect == null || visibleRect.Value.Contains(tileRect) || visibleRect.Value.IntersectsWith(tileRect))
                {
                    if (bigBlockNo > -1 && bigBlockNo < bigBlocks.Length)
                        g.DrawImage(bigBlocks[bigBlockNo], tileRect);
                    else
                        g.FillRectangle(Brushes.White, tileRect);

                    if (showBlocksAxis)
                    {
                        g.DrawRectangle(new Pen(Color.FromArgb(255, 255, 255, 255)), tileRect);
                    }

                    if (i == activeBlock)
                    {
                        g.DrawRectangle(new Pen(Brushes.Red, 3.0f), tileRect);
                    }
                }
            }
        }

        public static Image screenToImage(Image[] bigBlocks, Screen[] screens, int scrNo, float curScale, bool showBorder, int leftMargin, int topMargin, int width, int height)
        {
            int tileSizeX = (int)(bigBlocks[0].Width * curScale);
            int tileSizeY = (int)(bigBlocks[0].Height * curScale);

            Image result = new Bitmap(width * tileSizeX, height * tileSizeY);

            using (var g = Graphics.FromImage(result))
            {
                render(g, bigBlocks, null, screens, scrNo, curScale, showBorder, false, leftMargin, topMargin, width, height, true);
            }
            return result;
        }

        /*private static PictureBox mapScreen;
        private static PictureBox activeBlock;
        private static Panel pnView;
        private static ImageList bigBlocks;*/
    }
}
