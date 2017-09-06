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

        private void reloadAllData()
        {
            mapData = MapConfig.loadMap(curActiveMapNo);
            setPal();
            int videoPageId = curActiveVideo;
            videos = new ImageList[4];
            var chunk = ConfigScript.getVideoChunk(videoPageId);
            for (int i = 0; i < 4; i++)
            {
                videos[i] = new ImageList();
                videos[i].ImageSize = new Size(16, 16);
                var images = new Image[256];
                for (int t = 0; t < 256; t++)
                {
                    images[t] = UtilsGDI.ResizeBitmap(ConfigScript.videoNes.makeImage(t, chunk, curPal, i), 16, 16);
                }
                videos[i].Images.AddRange(images);
            }

            prepareBlocksPanel();
            mapScreen.Size = new Size(mapData.width * 16, mapData.height * 16);
            mapScreen.Invalidate();
        }

        private void EditMap_Load(object sender, EventArgs e)
        {
            UtilsGui.setCbItemsCount(cbScreenNo, MapConfig.mapsInfo.Length);
            cbScreenNo.SelectedIndex = 0;
            //reloadAllData();
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
        }

        private void saveMap()
        {
            byte[] x;
            int nn = MapConfig.saveMap(curActiveMapNo, mapData, out x);

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
                        f.Write(x, 0, (int)nn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void prepareBlocksPanel()
        {
            blocksPanel.Controls.Clear();
            blocksPanel.SuspendLayout();
            for (int i = 0; i < 256; i++)
            {
                var but = new Button();
                but.Size = new Size(16+5,16+5);
                but.Tag = i;
                but.Image = videos[0].Images[i];
                but.Click += new EventHandler(buttonBlockClick);
                blocksPanel.Controls.Add(but);
            }
            blocksPanel.ResumeLayout();
        }

        private void buttonBlockClick(Object button, EventArgs e)
        {
            int index = (int)((Button)button).Tag;
            curActiveBlock = index;
        }

        byte[] curPal;
        int curActiveMapNo = 0;
        int curActiveVideo = 10;
        int curActiveBlock = 0;
        ImageList[] videos;
        MapData mapData;
        bool showAxis = true;

        private void mapScreen_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var visibleRect = UtilsGui.getVisibleRectangle(mapPanel, mapScreen);
            for (int i = 0; i < mapData.width * mapData.height; i++)
            {
                int x = i % mapData.width;
                int y = i / mapData.width;
                int colorByte = mapData.attrData[x / 4 + mapData.width/4* (y / 4)];
                int subPal = (colorByte >> (x%4/2*2 + y%4/2*4))& 0x03;
                var tileRect = new Rectangle(new Point(x * 16, y * 16), new Size(16,16));
                if (visibleRect == null || visibleRect.Contains(tileRect) || visibleRect.IntersectsWith(tileRect))
                {
                    g.DrawImage(videos[subPal].Images[mapData.mapData[i]], tileRect);
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

        private void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X / 16;
            int y = e.Y / 16;
            if ((x < 0) || (x >= mapData.width) || (y < 0) || (y >= mapData.height))
            {
                return;
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (!MapConfig.readOnly)
                {
                    mapData.mapData[y * mapData.width + x] = curActiveBlock;
                }
            }
            else
            {
                //bit magic!!!
                int attrIndex = x / 4 + mapData.width/4 * (y / 4);
                int colorByte = mapData.attrData[attrIndex];
                int startBitIndex = x % 4 / 2 * 2 + y % 4 / 2 * 4;  //get start bit index
                int subPal = (colorByte >> startBitIndex) & 0x03;   //get 2 bits for subpal
                subPal = (subPal + 1) & 0x3;                        //round increment it
                colorByte &= ~(3 << startBitIndex);                 //clear 2 bits in color byte
                colorByte |= (subPal << startBitIndex);             //set 2 bits according subpal
                mapData.attrData[attrIndex] = colorByte;
            }
            mapScreen.Invalidate();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            saveMap();
        }

        private void cbShowAxis_CheckedChanged(object sender, EventArgs e)
        {
            showAxis = cbShowAxis.Checked;
            mapScreen.Invalidate();
        }

        private void cbVideoNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            MapInfo si = MapConfig.mapsInfo[cbScreenNo.SelectedIndex];
            curActiveMapNo = cbScreenNo.SelectedIndex;
            curActiveVideo = si.videoNo;
            reloadAllData();
        }
    }


}
