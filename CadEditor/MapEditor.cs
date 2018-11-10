using System;
using System.Drawing;
using System.Windows.Forms;

namespace CadEditor
{
    public class MapEditor
    {
        public static void render(Graphics g, Screen[] screens, int scrNo, RenderParams renderParams)
        {
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            var curScreen = screens[scrNo];
            for (int layerIndex = 0; layerIndex < curScreen.layers.Length; layerIndex++)
            {
                var layer = screens[scrNo].layers[layerIndex];
                renderLayer(g, layer, renderParams);
            }

            renderLayer(g, curScreen.physicsLayer, new RenderParams(renderParams) { renderBlockFunc = renderPhysicsBlock });

            if (renderParams.showBorder)
            {
                int tileSizeX = (int)(renderParams.bigBlocks[0].Width * renderParams.curScale);
                int tileSizeY = (int)(renderParams.bigBlocks[0].Height * renderParams.curScale);
                g.DrawRectangle(new Pen(Color.Green, 4.0f), new Rectangle(tileSizeX, 0, tileSizeX * renderParams.width, tileSizeY * renderParams.height));
            }

            //Additional rendering  //float to int!
            if (renderParams.additionalRenderEnabled)
            {
                ConfigScript.renderToMainScreen(g, (int) renderParams.curScale, scrNo);
            }
        }

        private static void renderLayer(Graphics g, BlockLayer layer, RenderParams renderParams)
        {
            bool needRenderLayer = layer != null && layer.showLayer;
            if (!needRenderLayer)
            {
                return;
            }

            int tileSizeX = renderParams.getTileSizeX();
            int tileSizeY = renderParams.getTileSizeY();

            int size = renderParams.getLayerSize();
            for (int i = 0; i < size; i++)
            {
                int bigBlockNo = ConfigScript.getBigTileNoFromScreen(layer.data, i);
                Rectangle tileRect = new Rectangle((i % renderParams.width) * tileSizeX + renderParams.leftMargin, i / renderParams.width * tileSizeY + renderParams.topMargin, tileSizeX, tileSizeY);
                renderParams.renderBlock(g,bigBlockNo, tileRect);
            }
        }

        private static void renderPhysicsBlock(Graphics g, int bigBlockNo, Rectangle tileRect, RenderParams renderParams)
        {
            g.DrawRectangle(new Pen(Color.Red, 2.0f), tileRect);
            g.FillRectangle(new SolidBrush(Color.FromArgb(128, 255, 255, 255)), tileRect);
            g.DrawString(String.Format("{0:X2}", bigBlockNo), new Font("Arial", 8), Brushes.Red, tileRect.X + 8, tileRect.Y);
        }

        private static void renderBlockOnPanel(Graphics g, int bigBlockNo, Rectangle tileRect, RenderParams renderParams)
        {
            if (bigBlockNo > -1 && bigBlockNo < renderParams.bigBlocks.Length)
                g.DrawImage(renderParams.bigBlocks[bigBlockNo], tileRect);
            else
                g.FillRectangle(Brushes.White, tileRect);

            if (renderParams.showBlocksAxis)
            {
                g.DrawRectangle(new Pen(Color.FromArgb(255, 255, 255, 255)), tileRect);
            }
        }

        public static void renderAllBlocks(Graphics g, PictureBox parentControl, int activeBlock, RenderParams renderParams)
        {
            renderParams.renderBlockFunc = renderBlockOnPanel; //render block on panel

            int tileSizeX = renderParams.getTileSizeX();
            int tileSizeY = renderParams.getTileSizeY();
            int width = parentControl.Width / tileSizeX;
            if (width == 0)
            {
                return;
            }

            for (int bigBlockNo = 0; bigBlockNo < renderParams.bigBlocks.Length; bigBlockNo++)
            {
                var tileRect = new Rectangle((bigBlockNo % width) * tileSizeX, bigBlockNo / width * tileSizeY, tileSizeX, tileSizeY);
                if (renderParams.needRenderTileRect(tileRect))
                {
                    renderParams.renderBlock(g, bigBlockNo, tileRect);

                    //additinal border render for active block
                    if (bigBlockNo == activeBlock)
                    {
                        g.DrawRectangle(new Pen(Brushes.Red, 3.0f), tileRect);
                    }
                }
            }
        }

        public static Image screenToImage(Screen[] screens, int scrNo, RenderParams renderParams)
        {
            int tileSizeX = (int)(renderParams.bigBlocks[0].Width * renderParams.curScale);
            int tileSizeY = (int)(renderParams.bigBlocks[0].Height * renderParams.curScale);

            Image result = new Bitmap(renderParams.width * tileSizeX, renderParams.height * tileSizeY);

            using (var g = Graphics.FromImage(result))
            {
                render(g, screens, scrNo, renderParams);
            }
            return result;
        }

        public class RenderParams
        {
            public RenderParams()
            {
                renderBlockFunc = renderBlockDefault;
            }

            public RenderParams(RenderParams other)
            {
                bigBlocks = other.bigBlocks;
                visibleRect = other.visibleRect;
                curScale = other.curScale;
                showBorder = other.showBorder;
                showBlocksAxis = other.showBlocksAxis;
                leftMargin = other.leftMargin;
                topMargin = other.topMargin;
                width = other.width;
                height = other.height;
                additionalRenderEnabled = other.additionalRenderEnabled;
                renderBlockFunc = other.renderBlockFunc;
            }

            public Image[] bigBlocks { get; set; }
            public Rectangle? visibleRect { get; set; }
            public float curScale { get; set; }
            public bool showBorder { get; set; }
            public bool showBlocksAxis { get; set; }
            public int leftMargin { get; set; }
            public int topMargin { get; set; }
            public int width { get; set; }
            public int height { get; set; }
            public bool additionalRenderEnabled { get; set; }

            public delegate void RenderBlockFunc(Graphics g, int bigBlockNo, Rectangle tileRect, RenderParams renderParams);

            public RenderBlockFunc renderBlockFunc { get; set; }

            public int getTileSizeX()
            {
                if (bigBlocks == null || bigBlocks?.Length < 1)
                {
                    return -1;
                }

                return (int) (bigBlocks[0].Width * curScale);
            }

            public int getTileSizeY()
            {
                if (bigBlocks == null || bigBlocks?.Length < 1)
                {
                    return -1;
                }

                return (int) (bigBlocks[0].Height * curScale);
            }

            public int getLayerSize()
            {
                return width * height;
            }

            public bool needRenderTileRect(Rectangle tileRect)
            {
                return visibleRect == null || 
                       visibleRect.Value.Contains(tileRect) ||
                       visibleRect.Value.IntersectsWith(tileRect);
            }

            private void renderBlockDefault(Graphics g, int bigBlockNo, Rectangle tileRect, RenderParams renderParams)
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

            public void renderBlock(Graphics g, int bigBlockNo, Rectangle tileRect)
            {
                if (needRenderTileRect(tileRect))
                {
                    renderBlockFunc(g, bigBlockNo, tileRect, this);
                }
            }
        }
    }
}
