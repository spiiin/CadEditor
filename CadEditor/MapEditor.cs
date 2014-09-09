using System;
using System.Drawing;
using System.Windows.Forms;

namespace CadEditor
{
    class MapEditor
    {
        public MapEditor(PictureBox _mapScreen, PictureBox _activeBlock, Panel _pnView, ImageList _bigBlocks)
        {
            mapScreen = _mapScreen;
            activeBlock = _activeBlock;
            pnView = _pnView;
            bigBlocks = _bigBlocks;
        }

        public void Update()
        {
            mapScreen.Invalidate();
        }

        public void Render(Graphics g, int[] screen, int[] screen2, int CurActiveLevelForScreen, float CurScale, bool ShowLayer1, bool ShowLayer2, int LeftMargin)
        {
            int WIDTH = ConfigScript.getScreenWidth(CurActiveLevelForScreen);
            int HEIGHT = ConfigScript.getScreenHeight(CurActiveLevelForScreen);
            int TILE_SIZE_X = (int)(blockWidth * CurScale);
            int TILE_SIZE_Y = (int)(blockHeight * CurScale);
            int SIZE = WIDTH * HEIGHT;
            //if (!fileLoaded)
            //    return;
            int[] indexes2 = null;
            if (ConfigScript.getLayersCount() > 1)
                indexes2 = screen2;
            var visibleRect = Utils.getVisibleRectangle(pnView, mapScreen);
            for (int i = 0; i < SIZE; i++)
            {
                int index = screen[i];
                int bigBlockNo = Globals.getBigTileNoFromScreen(screen, i);
                Rectangle tileRect;
                if (ConfigScript.getScreenVertical())
                    tileRect = new Rectangle(i / WIDTH * TILE_SIZE_X, (i % WIDTH) * TILE_SIZE_Y + LeftMargin, TILE_SIZE_X, TILE_SIZE_Y);
                else
                    tileRect = new Rectangle((i % WIDTH) * TILE_SIZE_X + LeftMargin, i / WIDTH * TILE_SIZE_Y, TILE_SIZE_X, TILE_SIZE_Y);

                if ((visibleRect.Contains(tileRect)) || (visibleRect.IntersectsWith(tileRect)))
                {
                    if (bigBlockNo < bigBlocks.Images.Count & ShowLayer1)
                        g.DrawImage(bigBlocks.Images[bigBlockNo], tileRect);
                    if (indexes2 != null && ShowLayer2)
                    {
                        int bigBlockNo2 = Globals.getBigTileNoFromScreen(indexes2, i);
                        if (bigBlockNo2 < bigBlocks.Images.Count)
                            g.DrawImage(bigBlocks.Images[bigBlockNo2], tileRect);
                    }
                }
            }
            /*if (!ConfigScript.getScreenVertical() && ShowNeiScreens && (curActiveScreen > 0) && ShowLayer1)
            {
                renderNeighbornLine(g, curActiveScreen - 1, (WIDTH - 1), 0);
            }
            if (!ConfigScript.getScreenVertical() && ShowNeiScreens && (curActiveScreen < ConfigScript.screensOffset[CurActiveLevelForScreen].recCount - 1) && ShowLayer1)
            {
                renderNeighbornLine(g, curActiveScreen + 1, 0, (WIDTH + 1) * TILE_SIZE_X);
            }*/

            if (ConfigScript.getScreenVertical())
                g.DrawRectangle(new Pen(Color.Green, 4.0f), new Rectangle(0, TILE_SIZE_Y, TILE_SIZE_X * HEIGHT, TILE_SIZE_Y * WIDTH));
            else
                g.DrawRectangle(new Pen(Color.Green, 4.0f), new Rectangle(TILE_SIZE_X, 0, TILE_SIZE_X * WIDTH, TILE_SIZE_Y * HEIGHT));

            //Additional rendering  //float to int!
            ConfigScript.renderToMainScreen(g, (int)CurScale);

            /*if (showBrush && curActiveBlock != -1 && (curDx != OUTSIDE || curDy != OUTSIDE))
            {
                if (!useStructs)
                {
                    if (!ConfigScript.getScreenVertical())
                        g.DrawImage(bigBlocks.Images[curActiveBlock], (curDx + 1) * TILE_SIZE_X, curDy * TILE_SIZE_Y);
                    else
                        g.DrawImage(bigBlocks.Images[curActiveBlock], curDy * TILE_SIZE_X, (curDx + 1) * TILE_SIZE_Y);
                }
                else
                {
                    drawActiveTileStruct(g, visibleRect);
                }
            }*/
        }

        //need to be fixed to work as RENDER, delete all copypaste
        public Image ScreenToImage(int[] screen, int[] screen2, int CurActiveLevelForScreen, float CurScale, bool ShowLayer1, bool ShowLayer2, int LeftMargin)
        {
            int EXPORT_SCALE = 2;
            int WIDTH = ConfigScript.getScreenWidth(CurActiveLevelForScreen);
            int HEIGHT = ConfigScript.getScreenHeight(CurActiveLevelForScreen);
            int TILE_SIZE_X = (int)(blockWidth * EXPORT_SCALE);
            int TILE_SIZE_Y = (int)(blockHeight * EXPORT_SCALE);
            int SIZE = WIDTH * HEIGHT;

            int[] indexes2 = null;
            if (ConfigScript.getLayersCount() > 1)
                indexes2 = screen2;

            Image result;
            if ((ConfigScript.getScreenVertical()))
                result = new Bitmap(HEIGHT * TILE_SIZE_Y, WIDTH * TILE_SIZE_X);
            else
                result = new Bitmap(WIDTH * TILE_SIZE_X, HEIGHT * TILE_SIZE_Y);

            using (var g = Graphics.FromImage(result))
            {
                for (int i = 0; i < SIZE; i++)
                {
                    int index = screen[i];
                    int bigBlockNo = Globals.getBigTileNoFromScreen(screen, i);
                    Rectangle tileRect;
                    if (ConfigScript.getScreenVertical())
                        tileRect = new Rectangle(i / WIDTH * TILE_SIZE_X, (i % WIDTH) * TILE_SIZE_Y, TILE_SIZE_X, TILE_SIZE_Y);
                    else
                        tileRect = new Rectangle((i % WIDTH) * TILE_SIZE_X, i / WIDTH * TILE_SIZE_Y, TILE_SIZE_X, TILE_SIZE_Y);

                    if (bigBlockNo < bigBlocks.Images.Count)
                        g.DrawImage(bigBlocks.Images[bigBlockNo], tileRect);
                    if (indexes2 != null)
                    {
                        int bigBlockNo2 = Globals.getBigTileNoFromScreen(indexes2, i);
                        if (bigBlockNo2 < bigBlocks.Images.Count)
                            g.DrawImage(bigBlocks.Images[bigBlockNo2], tileRect);
                    }
                }
            }
            return result;
        }

        public void setScreenData(int[] _screen, int layer)
        {
            if (layer == 0)
            {
                screen = _screen;
            }
            else
            {
                screen2 = _screen;
            }
        }

        void resetMapScreenSize(int CurActiveLevelForScreen, float CurScale)
        {
            if (ConfigScript.getScreenVertical())
                mapScreen.Size = new Size((int)(ConfigScript.getScreenHeight(CurActiveLevelForScreen) * blockWidth * CurScale), (int)((ConfigScript.getScreenWidth(CurActiveLevelForScreen) + 2) * blockHeight * CurScale));
            else
                mapScreen.Size = new Size((int)((ConfigScript.getScreenWidth(CurActiveLevelForScreen) + 2) * blockWidth * CurScale), (int)(ConfigScript.getScreenHeight(CurActiveLevelForScreen) * blockHeight * CurScale));
        }

        private PictureBox mapScreen;
        private PictureBox activeBlock;
        private Panel pnView;
        private ImageList bigBlocks;

        private int blockWidth = 32;
        private int blockHeight = 32;

        int[] screen;
        int[] screen2;
    }
}
