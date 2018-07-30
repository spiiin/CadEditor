using System;
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
            UtilsGui.setCbItemsCount(cbVideoNo, ConfigScript.videoOffset.recCount);
            UtilsGui.setCbItemsCount(cbPalleteNo, ConfigScript.palOffset.recCount);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbVideoNo, cbVideoNo_SelectedIndexChanged);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbSubPal, cbVideoNo_SelectedIndexChanged);
            //UtilsGui.setCbIndexWithoutUpdateLevel(cbPalleteNo, cbPalleteNo_SelectedIndexChanged);
            cbPalleteNo.SelectedIndex = 0;
        }

        private void setPal()
        {
            var palImage = new Bitmap(128,128);
            using (Graphics g = Graphics.FromImage(palImage))
            {
                for (int i = 0; i < 16; i++)
                {
                    g.FillRectangle(new SolidBrush(ConfigScript.videoNes.defaultNesColors[curPal[i]]), i % 4 * 32, (i / 4) * 32, 32, 32);
                    if (showNo)
                        g.DrawString(String.Format("{0:X2}", curPal[i]), new Font("Arial", 6), Brushes.White, new Rectangle(i % 4 * 32, (i / 4) * 32, 32, 32));
                }
            }
            pbPal.Image = palImage;
        }

        private void reloadVideo()
        {
            setPal();
            pbVideo.Image = ConfigScript.videoNes.makeImageRectangle(ConfigScript.getVideoChunk(curActiveVideo), curPal, curSubPal);
        }

        private int curActiveVideo;
        private byte[] curPal = new byte[16];
        private int curSubPal;
        private bool showNo;

        private void cbVideoNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            curActiveVideo = cbVideoNo.SelectedIndex;
            curSubPal = cbSubPal.SelectedIndex;
            reloadVideo();
        }

        private void pbPal_MouseClick(object sender, MouseEventArgs e)
        {
            var f = new EditColor {showNo = showNo};
            f.ShowDialog();
            if (EditColor.colorIndex != -1)
            {
                int index = e.X / 32 + (e.Y / 32) * 4;
                curPal[index] = (byte)EditColor.colorIndex;
                reloadVideo();
            }
        }

        private void cbPalleteNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cbPalleteNo.SelectedIndex;
            if (index == -1)
                return;
            curPal = ConfigScript.getPal(index);
            reloadVideo();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
          int index = cbPalleteNo.SelectedIndex;
          if (index == -1)
              return;
          ConfigScript.setPal(index, curPal);
          Globals.flushToFile();
        }

        private void cbShowNo_CheckedChanged(object sender, EventArgs e)
        {
            showNo = cbShowNo.Checked;
            setPal();
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            var f = new SelectFile {filename = "exportedConfigScript.videoNes.bin"};
            f.ShowDialog();
            if (!f.result)
                return;
            var data = ConfigScript.getVideoChunk(curActiveVideo);
            Utils.saveDataToFile(f.filename, data);
        }

        private void btImport_Click(object sender, EventArgs e)
        {
            var f = new SelectFile {filename = "exportedConfigScript.videoNes.bin"};
            f.ShowDialog();
            if (!f.result)
                return;
            var fn = f.filename;
            var data = Utils.loadDataFromFile(fn);
            if (data == null)
                return;
            ConfigScript.setVideoChunk(curActiveVideo, data);

            //dirty = true;
            reloadVideo();
        }
    }
}
