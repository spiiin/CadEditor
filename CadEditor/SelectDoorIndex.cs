using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CadEditor
{
    public partial class SelectDoorIndex : Form
    {
        public SelectDoorIndex()
        {
            InitializeComponent();
        }

        private void cbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedIndex = -1;
            pnScreens.Controls.Clear();
            int levelNo = cbLevel.SelectedIndex;
            if (levelNo == -1)
                return;
            LevelData ld = Globals.levelData[levelNo];
            int h = ld.getHeight();
            int w = ld.getWidth();
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    var b = new Button();
                    b.Size = new Size(28, 20);
                    b.Location = new Point(i * 29, j * 21);
                    int coord = j*w + i;
                    b.Text = String.Format("{0:X}",coord);
                    b.Tag = coord;
                    b.Click += new EventHandler(b_Click);
                    pnScreens.Controls.Add(b);
                }
            }
        }

        void b_Click(object sender, EventArgs e)
        {
            var b = (Button)sender;
            int tag = (int)b.Tag;
            for (int i = 0; i < pnScreens.Controls.Count; i++)
            {
                var everyButton = (Button)pnScreens.Controls[i];
                everyButton.ForeColor = Color.Black;
            }
            b.ForeColor = Color.Red;
            selectedIndex = tag;
        }

        private void SelectDoorIndex_Load(object sender, EventArgs e)
        {
            Globals.reloadLevelParamsData();
            cbLevel.SelectedIndex = 0;
        }

        static int selectedIndex;

        public int getSelectedIndex()
        {
            return selectedIndex;
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
