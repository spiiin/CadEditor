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
            byte videoPageId = (byte)(curActiveVideo + 0x90);
            videos = new ImageList[4];
            for (int i = 0; i < 4; i++)
            {
                Bitmap imageStrip = ConfigScript.videoNes.makeImageStrip(ConfigScript.getVideoChunk(videoPageId), curPal, i, 2);
                videos[i] = new ImageList();
                videos[i].ImageSize = new Size(16, 16);
                videos[i].Images.AddStrip(imageStrip);
            }

            prepareBlocksPanel();
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
                but.ImageList = videos[0];
                but.ImageIndex = i;
                but.Click += new EventHandler(buttonBlockClick);
                blocksPanel.Controls.Add(but);
            }
            blocksPanel.ResumeLayout();
        }

        private void buttonBlockClick(Object button, EventArgs e)
        {
            int index = ((Button)button).ImageIndex;
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
            for (int i = 0; i < 32*30; i++)
            {
                int x = i % 32;
                int y = i / 32;
                int colorByte = mapData.attrData[x / 4 + 8* (y / 4)];
                int subPal = (colorByte >> (x%4/2*2 + y%4/2*4))& 0x03;
                g.DrawImage(videos[subPal].Images[mapData.mapData[i]], new Point(x * 16, y * 16));
            }

            //add axis
            if (showAxis)
            {
                for (int x = 0; x < 32; x++)
                    g.DrawLine(new Pen(Color.White, 1.0f), new Point(x * 32, 0), new Point(x * 32, 32 * 30));
                for (int y = 0; y < 30; y++)
                    g.DrawLine(new Pen(Color.White, 1.0f), new Point(0, y * 32), new Point(32 * 32, y * 32));
            }
        }

        private void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X / 16;
            int y = e.Y / 16;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (!MapConfig.readOnly)
                {
                    mapData.mapData[y * 32 + x] = (byte)curActiveBlock;
                }
            }
            else
            {
                //bit magic!!!
                int attrIndex = x / 4 + 8 * (y / 4);
                int colorByte = mapData.attrData[attrIndex];
                int startBitIndex = x % 4 / 2 * 2 + y % 4 / 2 * 4;  //get start bit index
                int subPal = (colorByte >> startBitIndex) & 0x03;   //get 2 bits for subpal
                subPal = (subPal + 1) & 0x3;                        //round increment it
                colorByte &= ~(3 << startBitIndex);                 //clear 2 bits in color byte
                colorByte |= (subPal << startBitIndex);             //set 2 bits according subpal
                mapData.attrData[attrIndex] = (byte)colorByte;
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
