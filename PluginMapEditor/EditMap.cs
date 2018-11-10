using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PluginMapEditor;

namespace CadEditor
{
    public partial class EditMap : Form
    {
        public EditMap()
        {
            InitializeComponent();
        }

        int TILE_SIZE = 16;
        bool showSecondNametable;

        private void reloadAllData()
        {
            mapDatas = MapConfig.loadMap(curActiveMapNo);
            setPal();
            int videoPageId = curActiveVideo;
            videos = new Image[4][];
            var chunk = ConfigScript.getVideoChunk(videoPageId);
            for (int i = 0; i < 4; i++)
            {
                var images = new Image[256];
                for (int t = 0; t < 256; t++)
                {
                    images[t] = UtilsGDI.ResizeBitmap(ConfigScript.videoNes.makeImage(t, chunk, curPal, i), 16, 16);
                }
                videos[i] = images;
            }

            blocksScreen.Invalidate();

            mapScreen.Size = new Size(mapDatas[0].width * 16, mapDatas[0].height * 16);
            mapScreen.Invalidate();

            mapScreen2.Visible = showSecondNametable;
            mapScreen2.Size = mapScreen.Size;
            mapScreen2.Location = new Point(mapScreen.Location.X + mapScreen.Width, mapScreen2.Location.Y);
            mapScreen2.Invalidate();
        }

        private void EditMap_Load(object sender, EventArgs e)
        {
            UtilsGui.setCbItemsCount(cbScreenNo, MapConfig.mapsInfo.Length);
            cbScreenNo.SelectedIndex = 0;
            //reloadAllData();

            cbShowSecondNametable.Checked =  mapDatas.Length > 1;
            cbShowSecondNametable.Visible = mapDatas.Length > 1;

            cbSubpalette.DrawItem += cbSubpalette_DrawItemEvent;
        }

        protected void cbSubpalette_DrawItemEvent(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                return;
            }
            e.DrawBackground();
            e.Graphics.DrawImage(subpalSprites.Images[e.Index], e.Bounds.Width - 63, e.Bounds.Y, 64, 16);
            string text = cbSubpalette.Items[e.Index].ToString();
            e.Graphics.DrawString(text, cbSubpalette.Font,
                Brushes.Black,
                new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            e.DrawFocusRectangle();
        }

        private void setPal()
        {
            if (MapConfig.sharedPal)
            {
                curPal = ConfigScript.getPal(0);
            }
            else
            {
                int palAddr = MapConfig.mapsInfo[curActiveMapNo].palAddr;
                curPal = new byte[16];
                for (int i = 0; i < 16; i++)
                    curPal[i] = Globals.romdata[palAddr + i];
                curPal[0] = curPal[4] = curPal[8] = curPal[12] = 0x0F;
            }

            //set images for subpalletes
            subpalSprites.Images.Clear();
            for (int i = 0; i < 4; i++)
            {
                var sb = new Bitmap(16 * 4, 16);
                using (Graphics g = Graphics.FromImage(sb))
                {
                    g.FillRectangle(new SolidBrush(ConfigScript.videoNes.defaultNesColors[curPal[i * 4]]), 0, 0, 16, 16);
                    g.FillRectangle(new SolidBrush(ConfigScript.videoNes.defaultNesColors[curPal[i * 4 + 1]]), 16, 0, 16, 16);
                    g.FillRectangle(new SolidBrush(ConfigScript.videoNes.defaultNesColors[curPal[i * 4 + 2]]), 32, 0, 16, 16);
                    g.FillRectangle(new SolidBrush(ConfigScript.videoNes.defaultNesColors[curPal[i * 4 + 3]]), 48, 0, 16, 16);
                }
                subpalSprites.Images.Add(sb);
            }
        }

        private void saveMap()
        {
            byte[] x;
            int nn = MapConfig.saveMap(curActiveMapNo, mapDatas, out x);

            if (MapConfig.readOnly)
            {
                return;
            }
            //
            try
            {
                if (sfSaveDialog.ShowDialog() == DialogResult.OK)
                {
                    var fname = sfSaveDialog.FileName;
                    using (FileStream f = File.OpenWrite(fname))
                        f.Write(x, 0, nn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        byte[] curPal;
        int curActiveMapNo;
        int curActiveVideo = 10;
        int curActiveBlock;
        Image[][] videos;
        MapData[] mapDatas;
        bool showAxis = true;
        private int curActiveSubpal;

        private void renderMapScreen(Graphics g, MapData mapData)
        {
            var visibleRect = UtilsGui.getVisibleRectangle(mapPanel, mapScreen);
            for (int i = 0; i < mapData.width * mapData.height; i++)
            {
                int x = i % mapData.width;
                int y = i / mapData.width;
                int colorByte = mapData.attrData[x / 4 + mapData.width / 4 * (y / 4)];
                int subPal = (colorByte >> (x % 4 / 2 * 2 + y % 4 / 2 * 4)) & 0x03;
                var tileRect = new Rectangle(new Point(x * 16, y * 16), new Size(16, 16));
                if (visibleRect.Contains(tileRect) || visibleRect.IntersectsWith(tileRect))
                {
                    g.DrawImage(videos[subPal][mapData.mapData[i]], tileRect);
                }
            }

            //add axis
            if (showAxis)
            {
                for (int x = 0; x < mapData.width; x++)
                    g.DrawLine(new Pen(Color.White, 1.0f), new Point(x * 32, 0), new Point(x * 32, 32 * mapData.height));
                for (int y = 0; y < mapData.height; y++)
                    g.DrawLine(new Pen(Color.White, 1.0f), new Point(0, y * 32), new Point(32 * mapData.width, y * 32));
            }
        }

        private void mapScreen_Paint(object sender, PaintEventArgs e)
        {
            renderMapScreen(e.Graphics, mapDatas[0]);
        }

        private void mapScreen2_Paint(object sender, PaintEventArgs e)
        {
            if (mapDatas.Length > 1)
            {
                renderMapScreen(e.Graphics, mapDatas[1]);
            }
        }

        private void clickOnMapScreen(MouseEventArgs e, MapData mapData)
        {
            int x = e.X / 16;
            int y = e.Y / 16;
            if ((x < 0) || (x >= mapData.width) || (y < 0) || (y >= mapData.height))
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                if (!MapConfig.readOnly)
                {
                    mapData.mapData[y * mapData.width + x] = curActiveBlock;
                }
            }
            else
            {
                //bit magic!!!
                int attrIndex = x / 4 + mapData.width / 4 * (y / 4);
                int colorByte = mapData.attrData[attrIndex];
                int startBitIndex = x % 4 / 2 * 2 + y % 4 / 2 * 4;  //get start bit index
                int subPal = (colorByte >> startBitIndex) & 0x03;   //get 2 bits for subpal
                subPal = (subPal + 1) & 0x3;                        //round increment it
                colorByte &= ~(3 << startBitIndex);                 //clear 2 bits in color byte
                colorByte |= (subPal << startBitIndex);             //set 2 bits according subpal
                mapData.attrData[attrIndex] = colorByte;
            }
            
        }

        private void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            clickOnMapScreen(e, mapDatas[0]);
            mapScreen.Invalidate();
        }

        private void mapScreen2_MouseClick(object sender, MouseEventArgs e)
        {
            if (mapDatas.Length > 1)
            {
                clickOnMapScreen(e, mapDatas[1]);
                mapScreen2.Invalidate();
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            saveMap();
        }

        private void cbShowAxis_CheckedChanged(object sender, EventArgs e)
        {
            showAxis = cbShowAxis.Checked;
            mapScreen.Invalidate();
            mapScreen2.Invalidate();
            blocksScreen.Invalidate();
        }

        private void cbVideoNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            MapInfo si = MapConfig.mapsInfo[cbScreenNo.SelectedIndex];
            curActiveMapNo = cbScreenNo.SelectedIndex;
            curActiveVideo = si.videoNo;
            reloadAllData();
        }

        private void blocksScreen_MouseDown(object sender, MouseEventArgs e)
        {
            int tilesCount = videos[0].Length;
            var p = blocksScreen.PointToClient(Cursor.Position);
            int x = p.X, y = p.Y;
            int tileSizeX = TILE_SIZE;
            int tileSizeY = TILE_SIZE;
            int tx = x / tileSizeX, ty = y / tileSizeY;
            int maxtX = blocksScreen.Width / tileSizeX;
            int index = ty * maxtX + tx;
            if ((tx < 0) || (tx >= maxtX) || (index < 0) || (index >= tilesCount))
            {
                return;
            }

            curActiveBlock = index;
            lbActiveBlock.Text = String.Format("({0:X})", curActiveBlock);
            blocksScreen.Invalidate();
        }

        private void blocksScreen_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            MapEditor.renderAllBlocks(e.Graphics, blocksScreen, curActiveBlock, videos[curActiveSubpal].Length, new MapEditor.RenderParams
            {
                bigBlocks = videos[curActiveSubpal],
                visibleRect = UtilsGui.getVisibleRectangle(this, blocksScreen),
                curScale =1.0f,
                showBlocksAxis = showAxis,
                renderBlockFunc = MapEditor.renderBlocksOnPanelFunc
            });
        }

        private void cbSubpalette_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSubpalette.SelectedIndex != -1)
            {
                curActiveSubpal = cbSubpalette.SelectedIndex;
                blocksScreen.Invalidate();
            }
        }

        private void cbShowSecondNametable_CheckedChanged(object sender, EventArgs e)
        {
            showSecondNametable = cbShowSecondNametable.Checked;
            mapScreen2.Visible = showSecondNametable;
        }
    }


}
