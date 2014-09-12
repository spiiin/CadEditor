using System;
using System.Drawing;
using System.Windows.Forms;

namespace CadEditor
{
    class MapEditor
    {
        /*public MapEditor(PictureBox _mapScreen, PictureBox _activeBlock, Panel _pnView, ImageList _bigBlocks)
        {
            mapScreen = _mapScreen;
            activeBlock = _activeBlock;
            pnView = _pnView;
            bigBlocks = _bigBlocks;
        }*/

        /*public void Update()
        {
            mapScreen.Invalidate();
        }*/

        public static void Render(Graphics g, ImageList bigBlocks, Rectangle? visibleRect, int[] screen, int[] screen2, float CurScale, bool ShowLayer1, bool ShowLayer2, bool ShowBorder, int LeftMargin, int WIDTH, int HEIGHT, bool verticalScreen)
        {
            int TILE_SIZE_X = (int)(blockWidth * CurScale);
            int TILE_SIZE_Y = (int)(blockHeight * CurScale);
            int SIZE = WIDTH * HEIGHT;
            for (int i = 0; i < SIZE; i++)
            {
                int bigBlockNo = Globals.getBigTileNoFromScreen(screen, i);
                Rectangle tileRect;
                if (verticalScreen)
                    tileRect = new Rectangle(i / WIDTH * TILE_SIZE_X, (i % WIDTH) * TILE_SIZE_Y + LeftMargin, TILE_SIZE_X, TILE_SIZE_Y);
                else
                    tileRect = new Rectangle((i % WIDTH) * TILE_SIZE_X + LeftMargin, i / WIDTH * TILE_SIZE_Y, TILE_SIZE_X, TILE_SIZE_Y);

                if (visibleRect == null || visibleRect.Value.Contains(tileRect) || visibleRect.Value.IntersectsWith(tileRect))
                {
                    if (bigBlockNo != -1 && bigBlockNo < bigBlocks.Images.Count & ShowLayer1)
                        g.DrawImage(bigBlocks.Images[bigBlockNo], tileRect);
                    else
                        g.FillRectangle(Brushes.White, tileRect);
                    if (screen2 != null && ShowLayer2)
                    {
                        int bigBlockNo2 = Globals.getBigTileNoFromScreen(screen2, i);
                        if (bigBlockNo2 != -1 && bigBlockNo2 < bigBlocks.Images.Count)
                            g.DrawImage(bigBlocks.Images[bigBlockNo2], tileRect);
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

        public static Image ScreenToImage(ImageList bigBlocks, int[] screen, int[] screen2, float CurScale, bool ShowLayer1, bool ShowLayer2, bool ShowBorder, int LeftMargin, int WIDTH, int HEIGHT, bool verticalScreen)
        {
            int TILE_SIZE_X = (int)(blockWidth * CurScale);
            int TILE_SIZE_Y = (int)(blockHeight * CurScale);
            int SIZE = WIDTH * HEIGHT;

            int[] indexes2 = null;
            if (ConfigScript.getLayersCount() > 1)
                indexes2 = screen2;

            Image result;
            if (verticalScreen)
                result = new Bitmap(HEIGHT * TILE_SIZE_Y, WIDTH * TILE_SIZE_X);
            else
                result = new Bitmap(WIDTH * TILE_SIZE_X, HEIGHT * TILE_SIZE_Y);

            using (var g = Graphics.FromImage(result))
            {
                Render(g, bigBlocks, null, screen, screen2, CurScale, ShowLayer1, ShowLayer2, ShowBorder, LeftMargin, WIDTH, HEIGHT, verticalScreen);
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

        private static int blockWidth = 32;
        private static int blockHeight = 32;
    }
}
