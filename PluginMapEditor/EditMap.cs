using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

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
            mapData = new byte[1024];
            setPal();
            byte videoPageId = (byte)(curActiveVideo + 0x90);
            videos = new ImageList[4];
            for (int i = 0; i < 4; i++)
            {
                Bitmap imageStrip = Video.makeImageStrip(ConfigScript.getVideoChunk(videoPageId), curPal, i, 2);
                videos[i] = new ImageList();
                videos[i].ImageSize = new Size(16, 16);
                videos[i].Images.AddStrip(imageStrip);
            }

            prepareBlocksPanel();
            reloadMap();
        }

        private void EditMap_Load(object sender, EventArgs e)
        {
            Utils.setCbItemsCount(cbScreenNo, screensInfo.Length);
            cbScreenNo.SelectedIndex = 0;
            //reloadAllData();
        }

        private void setPal()
        {
            curPal = new byte[16];
            for (int i = 0; i < 16; i++)
                curPal[i] = Globals.romdata[curActivePalAddr + i];
            curPal[0] = curPal[4] = curPal[8] = curPal[12] = 0x0F;
        }

        private void reloadMap()
        {
            int romAddr = curActiveDataAddr;
            while (Globals.romdata[romAddr] != 0xFF)
            {
                int videoAddr = Utils.readWord(Globals.romdata, romAddr) - 0x2000;
                romAddr += 2;
                int count = Globals.romdata[romAddr++];
                for (int i = 0; i < count; i++)
                    mapData[videoAddr++] = Globals.romdata[romAddr++];
            }
            mapScreen.Invalidate();
        }

        void foundLongestZeros(byte[] data, int startIndex, int endIndex, out int firstZeroIndex, out int lastZeroIndex)
        {
            int longestZeroLen = 0;
            int curZeroLen = 0;
            int curFirstZeroIndex = -1;
            for (int i = startIndex; i < endIndex; i++)
            {
                if (data[i] == 0)
                {
                    if (++curZeroLen > longestZeroLen)
                    {
                        longestZeroLen = curZeroLen;
                        curFirstZeroIndex = i - longestZeroLen + 1;
                    }
                }
                else
                {
                    curZeroLen = 0;
                }
            }
            firstZeroIndex = curFirstZeroIndex;
            lastZeroIndex = firstZeroIndex + longestZeroLen - 1;
        }

        void recursiveS(byte[] d, int first, int last, MemoryStream outBuf)
        {
            int f, e;
            foundLongestZeros(d, first, last, out f, out e);
            if (e - f >= 5)
            {
                recursiveS(d, first, f, outBuf);
                recursiveS(d, e + 1, last, outBuf);
            }
            else
            {
                while (last > first)
                {
                    int addr = first + 0x2000;
                    outBuf.WriteByte((byte)(addr >> 8));
                    outBuf.WriteByte((byte)(addr & 0xFF));
                    outBuf.WriteByte((byte)Math.Min(last - first, 255));
                    for (int ind = 0; ind < 255 && (first+ind) < last; ind++)
                        outBuf.WriteByte(d[first+ind]);
                    first += 255;
                }
            }
        }

        private void saveMap()
        {
            byte[] x = new byte[(256+3)*4];
            var s = new MemoryStream(x);
            recursiveS(mapData, 0, mapData.Length, s);
            s.WriteByte(0xFF); //write stop byte
            long nn = s.Position;

            //
            try
            {
                if (ofOpenDialog.ShowDialog() == DialogResult.OK)
                {
                    var fname = ofOpenDialog.FileName;
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
        int curActiveDataAddr = 0;
        int curActivePalAddr = 0;
        int curActiveVideo = 10;
        int curActiveBlock = 0;
        ImageList[] videos;
        byte[] mapData;
        bool showAxis = true;

        private void mapScreen_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            for (int i = 0; i < 32*30; i++)
            {
                int x = i % 32;
                int y = i / 32;
                int colorByte = mapData[0x3C0 + x / 4 + 8* (y / 4)];
                int subPal = (colorByte >> (x%4/2*2 + y%4/2*4))& 0x03;
                g.DrawImage(videos[subPal].Images[mapData[i]], new Point(x * 16, y * 16));
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
                mapData[y * 32 + x] = (byte)curActiveBlock;
            }
            else
            {
                //bit magic!!!
                int colorByte = mapData[0x3C0 + x / 4 + 8 * (y / 4)];
                int startBitIndex = x % 4 / 2 * 2 + y % 4 / 2 * 4;  //get start bit index
                int subPal = (colorByte >> startBitIndex) & 0x03;   //get 2 bits for subpal
                subPal = (subPal + 1) & 0x3;                        //round increment it
                colorByte &= ~(3 << startBitIndex);                 //clear 2 bits in color byte
                colorByte |= (subPal << startBitIndex);             //set 2 bits according subpal
                mapData[0x3C0 + x / 4 + 8 * (y / 4)] = (byte)colorByte;
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
            ScreenInfo si = screensInfo[cbScreenNo.SelectedIndex];
            curActiveVideo = si.videoNo;
            curActiveDataAddr = si.dataAddr;
            curActivePalAddr = si.palAddr;
            reloadAllData();
        }

        struct ScreenInfo
        {
            public int dataAddr;
            public int palAddr;
            public int videoNo;
        }

        ScreenInfo[] screensInfo = new ScreenInfo[]
        { 
            new ScreenInfo(){ dataAddr = 0x80B1, palAddr = 0x1C43D, videoNo = 9 },
            new ScreenInfo(){ dataAddr = 0x83FC, palAddr = 0x8E6E , videoNo = 10 } 
        };
    }
}
