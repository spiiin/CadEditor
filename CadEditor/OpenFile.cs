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

        private void tbDumpName_Click(object sender, EventArgs e)
        {
            if (ofOpenDialog.ShowDialog() == DialogResult.OK)
            {
                tbDumpName.Text = ofOpenDialog.FileName;
            }
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            FileName = tbFileName.Text;
            DumpName = ConfigScript.showDumpFileField ? tbDumpName.Text : "";
            ConfigName = tbConfigName.Text;
            DialogResult = DialogResult.OK;
            Close();

            Properties.Settings.Default["FileName"] = FileName;
            Properties.Settings.Default["DumpName"] = DumpName;
            Properties.Settings.Default["ConfigName"] = ConfigName;
            Properties.Settings.Default.Save();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public static string FileName = "";
        public static string DumpName = "";
        public static string ConfigName="";

        private void OpenFile_Load(object sender, EventArgs e)
        {
            tbDumpName.Enabled = ConfigScript.showDumpFileField;
            lbDumpName.Enabled = ConfigScript.showDumpFileField;

            if (Properties.Settings.Default["FileName"].ToString() != "")
                tbFileName.Text = Properties.Settings.Default["FileName"].ToString();
            if (Properties.Settings.Default["DumpName"].ToString() != "")
                tbDumpName.Text = Properties.Settings.Default["DumpName"].ToString();
            if (Properties.Settings.Default["ConfigName"].ToString() != "")
                tbConfigName.Text = Properties.Settings.Default["ConfigName"].ToString();

            if (FileName == "" && ConfigScript.romName != "")
                tbFileName.Text = ConfigScript.romName;
            if (DumpName == "" && ConfigScript.dumpName != "")
                tbDumpName.Text = ConfigScript.dumpName;
            if (ConfigName == "" && ConfigScript.cfgName != "")
                tbConfigName.Text = ConfigScript.cfgName;

            ofOpenDialog.InitialDirectory = Environment.CurrentDirectory;
            if (FileName != "")
                tbFileName.Text = FileName;
            if (ConfigName != "")
                tbConfigName.Text = ConfigName;
            if (DumpName != "")
                tbDumpName.Text = DumpName;
        }
    }
}
