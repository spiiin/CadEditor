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
            result = true;
            filename = tbFileName.Text;
            Close();
        }

        private void tbFileName_Click(object sender, EventArgs e)
        {
            if (ofOpenDialog.ShowDialog() == DialogResult.OK)
            {
                tbFileName.Text = ofOpenDialog.FileName;
            }
        }

        public string filename;
        public bool showExportParams;
        public bool result;

        private void SelectFile_Load(object sender, EventArgs e)
        {
            tbFileName.Text = filename;
            ofOpenDialog.FileName = filename;
            cbExportType.SelectedIndexChanged -= cbExportType_SelectedIndexChanged;
            cbExportType.SelectedIndex = 0;
            cbExportType.SelectedIndexChanged += cbExportType_SelectedIndexChanged;
            result = false;

            lbExportType.Visible = cbExportType.Visible = showExportParams;
        }

        private void cbExportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var exts = new Dictionary<ExportType, String> { {ExportType.Binary,".bin"}, {ExportType.Picture,".png"} };
            exportType = (ExportType)cbExportType.SelectedIndex;
            filename = System.IO.Path.ChangeExtension(filename, exts[exportType]);
            tbFileName.Text = filename;
            ofOpenDialog.FileName = filename;
        }

        public ExportType getExportType()
        {
            return exportType;
        }

        ExportType exportType;
    }

    public enum ExportType
    {
        Picture,
        Binary,
    }
}
