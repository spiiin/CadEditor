using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CadEditor
{
    public partial class OpenFile : Form
    {
        public OpenFile()
        {
            InitializeComponent();
        }

        private void tbFileName_Click(object sender, EventArgs e)
        {
            if (ofOpenDialog.ShowDialog() == DialogResult.OK)
            {
                tbFileName.Text = ofOpenDialog.FileName;
            }
        }

        private void tbConfigName_Click(object sender, EventArgs e)
        {
            if (ofOpenDialog.ShowDialog() == DialogResult.OK)
            {
                tbConfigName.Text = ofOpenDialog.FileName;
            }
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            FileName = tbFileName.Text;
            FileSize= (int)new FileInfo(FileName).Length;
            ConfigName = tbConfigName.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public static string FileName="";
        public static string ConfigName="";
        public static int FileSize = 0;

        private void OpenFile_Load(object sender, EventArgs e)
        {
            ofOpenDialog.InitialDirectory = Environment.CurrentDirectory;
            if (FileName != "")
                tbFileName.Text = FileName;
            if (ConfigName != "")
                tbConfigName.Text = ConfigName;
        }
    }
}
