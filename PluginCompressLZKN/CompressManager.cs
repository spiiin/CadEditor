using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CadEditor;

namespace PluginCompressLZKN
{
    public partial class CompressManager : Form
    {
        public CompressManager()
        {
            InitializeComponent();
        }

        private void CompressManager_Load(object sender, EventArgs e)
        {
            cbAddress.Items.Clear();
            cbAddress.Items.AddRange(CompressConfig.compressParams.Select(x => x.address.ToString()).ToArray());
            UtilsGui.setCbIndexWithoutUpdateLevel(cbAddress, cbAddress_SelectedIndexChanged);
        }

        private void cbAddress_SelectedIndexChanged(object sender, EventArgs e)
        {
            int curIndex = cbAddress.SelectedIndex;
            if (curIndex < 0) { return; }
            lbMaxLength.Text = CompressConfig.compressParams[curIndex].maxSize.ToString();
        }

        private void cbInsert_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btCompress_Click(object sender, EventArgs e)
        {
            tbLog.Text = "";
            tbLog.AppendText("---------------------------------------------------------------\n");
            tbLog.AppendText("Job start\n");
            tbLog.AppendText(String.Format("Current file name: {0}\n", OpenFile.FileName));
            tbLog.AppendText(String.Format("Current dump name: {0}\n", OpenFile.DumpName));
            tbLog.AppendText("Error! Compressor not found\n");
            tbLog.AppendText("Job end\n");
            tbLog.AppendText("---------------------------------------------------------------\n");
            bool needToInsert = cbInsert.Checked;
        }
    }
}
