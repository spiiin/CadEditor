using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CadEditor
{
    public partial class SaveScreensCount : Form
    {
        public SaveScreensCount()
        {
            InitializeComponent();
        }

        private void SaveScreensCount_Load(object sender, EventArgs e)
        {
            int scrCount = ConfigScript.screensOffset.recCount;
            Utils.setCbItemsCount(cbFirst, scrCount);
            Utils.setCbItemsCount(cbCount, scrCount);
            cbFirst.SelectedIndex = 0;
            cbCount.SelectedIndex = scrCount - 1;
            Result = false;
            btOpen.Visible = ExportMode;
            btImport.Visible = !ExportMode;
            lbCount.Visible = ExportMode;
            cbCount.Visible = ExportMode;
            tbFileName.Text = Filename;
        }

        private void tbFileName_Click(object sender, EventArgs e)
        {
            if (ofOpenDialog.ShowDialog() == DialogResult.OK)
            {
                tbFileName.Text = ofOpenDialog.FileName;
            }
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            First = Utils.parseInt(cbFirst.Text);
            Count = Utils.parseInt(cbCount.Text);
            Filename = tbFileName.Text;
            Result = true;
            Close();
        }

        public static int Count;
        public static int First;
        public static string Filename;
        public static bool Result;
        public static bool ExportMode = true;
    }
}
