using System;
using System.Drawing;
using System.Windows.Forms;

namespace CadEditor
{
    public class MapEditor
    {
        public static void Render(Graphics g, Image[] bigBlocks, int blockWidth, int blockHeight, Rectangle? visibleRect, BlockLayer layer1, BlockLayer layer2, int scrNo, float CurScale, bool ShowBorder, int LeftMargin, int TopMargin, int WIDTH, int HEIGHT)
        {
            bool verticalScreen = ConfigScript.getScreenVertical();
            int TILE_SIZE_X = (int)(blockWidth * CurScale);
            int TILE_SIZE_Y = (int)(blockHeight * CurScale);
            int SIZE = WIDTH * HEIGHT;
            for (int i = 0; i < SIZE; i++)
            {
                int bigBlockNo = ConfigScript.getBigTileNoFromScreen(layer1.screens[scrNo], i);
                Rectangle tileRect;
                if (verticalScreen)
                    tileRect = new Rectangle(i / WIDTH * TILE_SIZE_X + TopMargin, (i % WIDTH) * TILE_SIZE_Y + LeftMargin, TILE_SIZE_X, TILE_SIZE_Y);
                else
                    tileRect = new Rectangle((i % WIDTH) * TILE_SIZE_X + LeftMargin, i / WIDTH * TILE_SIZE_Y + TopMargin, TILE_SIZE_X, TILE_SIZE_Y);

                if (visibleRect == null || visibleRect.Value.Contains(tileRect) || visibleRect.Value.IntersectsWith(tileRect))
                {
                    if (bigBlockNo > -1 && bigBlockNo < bigBlocks.Length && layer1.showLayer)
                        g.DrawImage(bigBlocks[bigBlockNo], tileRect);
                    else
                        g.FillRectangle(Brushes.White, tileRect);
                    if (layer2 != null && layer2.screens != null && layer2.screens[scrNo] != null && layer2.showLayer)
                    {
                        int bigBlockNo2 = ConfigScript.getBigTileNoFromScreen(layer2.screens[scrNo], i);
                        if (bigBlockNo2 != -1 && bigBlockNo2 < bigBlocks.Length)
                            g.DrawImage(bigBlocks[bigBlockNo2], tileRect);
                        else
                            g.FillRectangle(Brushes.White, tileRect);
                    }
                }
            }

            if (ShowBorder)
            {
                if (verticalScreen)
                    g.DrawRectangle(new Pen(Color.Green, 4.0f), new Rectangle(0, TILE_SIZE_Y, TILE_SIZE_X * HEIGHT, TILE_SIZE_Y * WIDTH));
                else
                    g.DrawRectangle(new Pen(Color.Green, 4.0f), new Rectangle(TILE_SIZE_X, 0, TILE_SIZE_X * WIDTH, TILE_SIZE_Y * HEIGHT));
            }

            //Additional rendering  //float to int!
            ConfigScript.renderToMainScreen(g, (int)CurScale);
        }

        public static void RenderAllBlocks(Graphics g, PictureBox parentControl, Image[] bigBlocks, int blockWidth, int blockHeight, Rectangle? visibleRect, float CurScale, int activeBlock)
        {
            int TILE_SIZE_X = (int)(blockWidth * CurScale);
            int TILE_SIZE_Y = (int)(blockHeight * CurScale);
            int WIDTH = parentControl.Width / TILE_SIZE_X;
            if (WIDTH == 0)
            {
                return;
            }

            for (int i = 0; i < bigBlocks.Length; i++)
            {
                int bigBlockNo = i;
                Rectangle tileRect = new Rectangle((i % WIDTH) * TILE_SIZE_X, i / WIDTH * TILE_SIZE_Y, TILE_SIZE_X, TILE_SIZE_Y);

                if (visibleRect == null || visibleRect.Value.Contains(tileRect) || visibleRect.Value.IntersectsWith(tileRect))
                {
                    if (bigBlockNo > -1 && bigBlockNo < bigBlocks.Length)
                        g.DrawImage(bigBlocks[bigBlockNo], tileRect);
                    else
                        g.FillRectangle(Brushes.White, tileRect);

                    if (i == activeBlock)
                    {
                        g.DrawRectangle(new Pen(Brushes.Red, 2.0f), tileRect);
                    }
                }
            }
        }

        public static Image ScreenToImage(Image[] bigBlocks, int blockWidth, int blockHeight, BlockLayer layer1, BlockLayer layer2, int scrNo, float CurScale, bool ShowBorder, int LeftMargin, int TopMargin, int WIDTH, int HEIGHT)
        {
            bool verticalScreen = ConfigScript.getScreenVertical();
            int TILE_SIZE_X = (int)(blockWidth * CurScale);
            int TILE_SIZE_Y = (int)(blockHeight * CurScale);
            int SIZE = WIDTH * HEIGHT;

            Image result;
            if (verticalScreen)
                result = new Bitmap(HEIGHT * TILE_SIZE_Y, WIDTH * TILE_SIZE_X);
            else
                result = new Bitmap(WIDTH * TILE_SIZE_X, HEIGHT * TILE_SIZE_Y);

            using (var g = Graphics.FromImage(result))
            {
                Render(g, bigBlocks, blockWidth, blockHeight, null, layer1, layer2, scrNo, CurScale, ShowBorder, LeftMargin, TopMargin, WIDTH, HEIGHT);
            }
            return result;
        }

        /*void resetMapScreenSize(PictureBox mapScreen, int CurActiveLevelForScreen, float CurScale)
        {
            if (ConfigScript.getScreenVertical())
                mapScreen.Size = new Size((int)(ConfigScript.getScreenHeight(CurActiveLevelForScreen) * blockWidth * CurScale), (int)((ConfigScript.getScreenWidth(CurActiveLevelForScreen) + 2) * blockHeight * CurScale));
            else
                mapScreen.Size = new Size((int)((ConfigScript.getScreenWidth(CurActiveLevelForScreen) + 2) * blockWidth * CurScale), (int)(ConfigScript.getScreenHeight(CurActiveLevelForScreen) * blockHeight * CurScale));
        }*/

        /*private static PictureBox mapScreen;
        private static PictureBox activeBlock;
        private static Panel pnView;
        private static ImageList bigBlocks;*/
    }
}
