using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CadEditor
{
    public partial class BoxBackForm : Form
    {
        public BoxBackForm()
        {
            InitializeComponent();
        }

        private void preparePanel()
        {
            //GUI
            mapObjects.SuspendLayout();
            for (int i = 0; i < 16; i++)
            {
                Panel fp = new Panel();
                fp.Size = new Size(mapObjects.Width - 25, 32);
                //
                Label lb = new Label();
                lb.Location = new Point(0, 0);
                lb.Size = new Size(24, 32);
                lb.Tag = i;
                lb.Text = String.Format("{0:X}",i);
                fp.Controls.Add(lb);
                //
                PictureBox pb = new PictureBox();
                pb.Location = new Point(24, 0);
                pb.Size = new Size(32, 32);
                pb.Tag = i;
                fp.Controls.Add(pb);
                //
                ComboBox cbColor = new ComboBox();
                cbColor.Size = new Size(120, 21);
                cbColor.Location = new Point(60, 0);
                cbColor.Tag = pb;
                for (int p = 0; p < 256; p++)
                  cbColor.Items.Add(p.ToString());
                cbColor.DropDownStyle = ComboBoxStyle.DropDownList;
                cbColor.SelectedIndexChanged += cbColor_SelectedIndexChanged;
                fp.Controls.Add(cbColor);
                //
                mapObjects.Controls.Add(fp);
            }
            mapObjects.ResumeLayout();
        }

        void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (parentForm == null)
                return;
            ComboBox cb = (ComboBox)sender;
            if (cb.SelectedIndex == -1)
                return;
            PictureBox pb = (PictureBox)cb.Tag;
            int index = (int)pb.Tag;
            pb.Image = parentForm.makeObjImage(cb.SelectedIndex);
            curActiveBack[index] = (byte)cb.SelectedIndex;
            dirty = true;
        }

        private void BoxBackForm_Load(object sender, EventArgs e)
        {
            preparePanel();
            setBack();
            for (int i = 0; i < 16; i++)
            {
                var p = (Panel) mapObjects.Controls[i];
                var cb = (ComboBox)p.Controls[2];
                cb.SelectedIndex = curActiveBack[i];
            }
        }

        private void setBack()
        {
            int backAddr = Globals.getBackTileAddr(parentForm.getActiveLevel());
            for (int i = 0; i < 16; i++)
                curActiveBack[i] = Globals.romdata[backAddr + i];
        }

        private bool saveToFile()
        {
            int backAddr = Globals.getBackTileAddr(parentForm.getActiveLevel());
            for (int i = 0; i < 16; i++)
                Globals.romdata[backAddr + i] = curActiveBack[i];
            dirty = !Globals.flushToFile();
            return !dirty;
        }

        private BlockEditCad parentForm = null;
        private byte[] curActiveBack = new byte[16];
        private bool dirty = false;

        public void setParentForm(BlockEditCad parentForm)
        {
            this.parentForm = parentForm;
        }

        private void BoxBackForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dirty)
            {
                DialogResult dr = MessageBox.Show("Backs was changed. Do you want to save current Backs?", "Save", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                    saveToFile();
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            saveToFile();
        }
    }
}
