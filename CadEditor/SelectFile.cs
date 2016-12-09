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
    public partial class SelectFile : Form
    {
        public SelectFile()
        {
            InitializeComponent();
        }

        private void btSelect_Click(object sender, EventArgs e)
        {
            Result = true;
            Filename = tbFileName.Text;
            Close();
        }

        private void tbFileName_Click(object sender, EventArgs e)
        {
            if (ofOpenDialog.ShowDialog() == DialogResult.OK)
            {
                tbFileName.Text = ofOpenDialog.FileName;
            }
        }

        public string Filename;
        public bool ShowExportParams;
        public bool Result;

        private void SelectFile_Load(object sender, EventArgs e)
        {
            tbFileName.Text = Filename;
            ofOpenDialog.FileName = Filename;
            cbExportType.SelectedIndexChanged -= cbExportType_SelectedIndexChanged;
            cbExportType.SelectedIndex = 0;
            cbExportType.SelectedIndexChanged += cbExportType_SelectedIndexChanged;
            Result = false;

            lbExportType.Visible = cbExportType.Visible = ShowExportParams;
        }

        private void cbExportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var exts = new Dictionary<ExportType, String> { {ExportType.Binary,".bin"}, {ExportType.Picture,".png"} };
            exportType = (ExportType)cbExportType.SelectedIndex;
            Filename = System.IO.Path.ChangeExtension(Filename, exts[exportType]);
            tbFileName.Text = Filename;
            ofOpenDialog.FileName = Filename;
        }

        public ExportType getExportType()
        {
            return exportType;
        }

        ExportType exportType;
    }

    public enum ExportType
    {
        Binary,
        Picture,
    }
}
