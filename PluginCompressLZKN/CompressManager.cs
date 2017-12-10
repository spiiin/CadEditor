using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CadEditor;
using System.Runtime.InteropServices;
using System.IO;

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
            cbAddress.Items.AddRange(CompressConfig.compressParams.Select(x => x.address.ToString("X")).ToArray());
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
            try
            {
                tbLog.Text = "";
                tbLog.AppendText("--------------------------------------------------------------------------------------------------\n");
                tbLog.AppendText("Job start\n");
                tbLog.AppendText(String.Format("Current file name: {0}\n", OpenFile.FileName));
                tbLog.AppendText(String.Format("Current dump name: {0}\n", OpenFile.DumpName));

                var compressedFileName = OpenFile.DumpName + ".lzkn1";
                tbLog.AppendText(String.Format("Try to compress current dumpdata with lzkn1 compressor\n"));

                byte[] compressedBytes = new byte[Globals.dumpdata.Length];
                int compressedSize = LZKN1.compress(Globals.dumpdata, compressedBytes, Globals.dumpdata.Length);
                tbLog.AppendText(String.Format("Compression complete. Compressed size: {0} bytes\n", compressedSize));

                byte[] realCompressedBytes = new byte[compressedSize];
                Array.Copy(compressedBytes, realCompressedBytes, compressedSize);

                bool needCreateArchiveFile = cbArchiveFile.Checked;
                if (needCreateArchiveFile)
                {
                    tbLog.AppendText(String.Format("Saving content to lzkn archive: {0}\n", compressedFileName));
                    File.WriteAllBytes(compressedFileName, realCompressedBytes);
                    tbLog.AppendText(String.Format("Saving content to lzkn archive complete\n"));
                }

                bool insert = cbInsert.Checked;
                if (insert)
                {
                    int selectedAddressIndex = cbAddress.SelectedIndex;
                    int insertingAddress = CompressConfig.compressParams[selectedAddressIndex].address;
                    tbLog.AppendText(String.Format("Inserting archive in ROM at address: {0}\n", insertingAddress.ToString("X")));
                    Array.Copy(realCompressedBytes, 0, Globals.romdata, insertingAddress, compressedSize);
                    //todo: fill free space with 0xFF
                    Globals.flushToFile();
                    tbLog.AppendText("Inserting archive in ROM complete\n");
                }
                else
                {
                    tbLog.AppendText("Inserting archive in ROM disabled\n");
                }
                tbLog.AppendText("Done!\n");
                tbLog.AppendText("--------------------------------------------------------------------------------------------------\n");
            }
            catch(Exception ex)
            {
                tbLog.AppendText(String.Format("Error! Description: {0}", ex.Message));
            }
        }
    }

    public static class LZKN1
    {
        [DllImport("lzkn1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int compress([In] byte[] input, [Out] byte[] output, int size);

        [DllImport("lzkn1.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int compressed_size([In] byte[] input);

        //[DllImport("lzkn2.dll", CallingConvention = CallingConvention.Cdecl)]
        //public static extern void ShowMe();
    }
}
