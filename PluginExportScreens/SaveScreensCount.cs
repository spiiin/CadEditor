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
            var screens = ConfigScript.loadScreens();
            int scrCount = screens.Length;
            UtilsGui.setCbItemsCount(cbFirst, scrCount);
            UtilsGui.setCbItemsCount(cbCount, scrCount);
            cbFirst.SelectedIndex = 0;
            cbCount.SelectedIndex = scrCount - 1;
            result = false;
            btOpen.Visible = exportMode;
            btImport.Visible = !exportMode;
            lbCount.Visible = exportMode;
            cbCount.Visible = exportMode;
            tbFileName.Text = filename;
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
            first = Utils.parseInt(cbFirst.Text);
            count = Utils.parseInt(cbCount.Text);
            filename = tbFileName.Text;
            result = true;
            Close();
        }

        public static int count;
        public static int first;
        public static string filename;
        public static bool result;
        public static bool exportMode = true;
    }
}
