using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Linq;

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
            ofOpenDialog.Filter = "";
            if (ofOpenDialog.ShowDialog() == DialogResult.OK)
            {
                tbFileName.Text = ofOpenDialog.FileName;
            }
        }

        private void tbConfigName_Click(object sender, EventArgs e)
        {
            ofOpenDialog.Filter = "Config files|*.cs";
            if (ofOpenDialog.ShowDialog() == DialogResult.OK)
            {
                cbConfigName.Text = ofOpenDialog.FileName;
                updateCbConfigInDirectory(cbConfigName.Text);
                var showDumpField = ConfigScript.PreloadShowDumpField(cbConfigName.Text);
                tbDumpName.Enabled = showDumpField;
                lbDumpName.Enabled = showDumpField;
            }
        }

        private void updateCbConfigInDirectory(string text)
        {
            try
            {
                var files = Directory.EnumerateFiles(Path.GetDirectoryName(text), "Settings_*.cs");
                cbConfigName.DropDownWidth = 600;
                cbConfigName.Items.Clear();
                cbConfigName.Items.AddRange(files.ToArray());
            }
            catch (Exception)
            {
                ;
            }
        }

        private void tbDumpName_Click(object sender, EventArgs e)
        {
            ofOpenDialog.Filter = "";
            if (ofOpenDialog.ShowDialog() == DialogResult.OK)
            {
                tbDumpName.Text = ofOpenDialog.FileName;
            }
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            FileName = tbFileName.Text;
            ConfigName = cbConfigName.Text;
            DumpName = ConfigScript.PreloadShowDumpField(ConfigName) ? tbDumpName.Text : "";
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
            if (Properties.Settings.Default["FileName"].ToString() != "")
                tbFileName.Text = Properties.Settings.Default["FileName"].ToString();
            if (Properties.Settings.Default["DumpName"].ToString() != "")
                tbDumpName.Text = Properties.Settings.Default["DumpName"].ToString();
            if (Properties.Settings.Default["ConfigName"].ToString() != "")
            {
                cbConfigName.Text = Properties.Settings.Default["ConfigName"].ToString();
            }

            var showDumpField = ConfigScript.PreloadShowDumpField(cbConfigName.Text);
            tbDumpName.Enabled = showDumpField;
            lbDumpName.Enabled = showDumpField;

            if (FileName == "" && ConfigScript.romName != "")
                tbFileName.Text = ConfigScript.romName;
            if (DumpName == "" && ConfigScript.dumpName != "")
                tbDumpName.Text = ConfigScript.dumpName;
            if (ConfigName == "" && ConfigScript.cfgName != "")
            {
                cbConfigName.Text = ConfigScript.cfgName;
            }

            ofOpenDialog.InitialDirectory = Environment.CurrentDirectory;
            if (FileName != "")
                tbFileName.Text = FileName;
            if (ConfigName != "")
            {
                cbConfigName.Text = ConfigName;
            }
            if (DumpName != "")
                tbDumpName.Text = DumpName;

            updateCbConfigInDirectory(cbConfigName.Text);
        }
    }
}
