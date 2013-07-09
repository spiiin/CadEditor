﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CadEditor
{
    public partial class EditVideo : Form
    {
        public EditVideo()
        {
            InitializeComponent();
        }

        private void EditVideo_Load(object sender, EventArgs e)
        {
            curActiveVideo = 0;
            curSubPal = 0;
            Utils.setCbItemsCount(cbVideoNo, ConfigScript.videoOffset.recCount);
            Utils.setCbItemsCount(cbPalleteNo, ConfigScript.palOffset.recCount);
            Utils.setCbIndexWithoutUpdateLevel(cbVideoNo, cbVideoNo_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbSubPal, cbVideoNo_SelectedIndexChanged);
            //Utils.setCbIndexWithoutUpdateLevel(cbPalleteNo, cbPalleteNo_SelectedIndexChanged);
            cbPalleteNo.SelectedIndex = 0;
        }

        private void setPal()
        {
            var palImage = new Bitmap(128,128);
            using (Graphics g = Graphics.FromImage(palImage))
            {
                for (int i = 0; i < 16; i++)
                {
                    g.FillRectangle(new SolidBrush(Video.NesColors[curPal[i]]), i % 4 * 32, (i / 4) * 32, 32, 32);
                }
            }
            pbPal.Image = palImage;
        }

        private void reloadVideo()
        {
            setPal();
            byte videoPageId = (byte)(curActiveVideo + 0x90);
            Bitmap imageStrip = Video.makeImageStrip(ConfigScript.getVideoChunk(videoPageId), curPal, curSubPal, 4);
            Bitmap resultVideo = new Bitmap(512, 512);
            using (Graphics g = Graphics.FromImage(resultVideo))
            {
                for (int i = 0; i < 256; i++)
                {
                    g.DrawImage(imageStrip, new Rectangle(i%16 * 32, (i/16) *32, 32, 32), new Rectangle(i * 32, 0, 32, 32) , GraphicsUnit.Pixel);
                }
            }
            pbVideo.Image = resultVideo;
        }

        private int curActiveVideo = 0;
        private byte[] curPal = new byte[16];
        private int curSubPal;

        private void cbVideoNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            curActiveVideo = cbVideoNo.SelectedIndex;
            curSubPal = cbSubPal.SelectedIndex;
            reloadVideo();
        }

        private void pbPal_MouseClick(object sender, MouseEventArgs e)
        {
            var f = new EditColor();
            f.ShowDialog();
            if (EditColor.ColorIndex != -1)
            {
                int index = e.X / 32 + (e.Y / 32) * 4;
                curPal[index] = (byte)EditColor.ColorIndex;
                reloadVideo();
            }
        }

        private void cbPalleteNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cbPalleteNo.SelectedIndex;
            if (index == -1)
                return;
            int palAddress = Globals.getPalAddr((byte)index);
            for (int i = 0; i < 16; i++)
                curPal[i] = Globals.romdata[palAddress + i];
            reloadVideo();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
          int index = cbPalleteNo.SelectedIndex;
          if (index == -1)
              return;
          int palAddress = Globals.getPalAddr((byte)index);
          for (int i = 0; i < 16; i++)
              Globals.romdata[palAddress + i] = curPal[i];
          Globals.flushToFile();
        }
    }
}
