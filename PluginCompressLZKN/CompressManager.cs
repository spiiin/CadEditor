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
            cbAddress.Items.AddRange(CompressConfig.compressParams.Select(x => String.Format("{0} ({1})", x.address.ToString("X"), x.fname) ).ToArray());
            cbAddress.SelectedIndex = 0;
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

                int selectedAddressIndex = cbAddress.SelectedIndex;
                var inputFilename = CompressConfig.compressParams[selectedAddressIndex].fname;
                var fullInputFilename = inputFilename == null ? OpenFile.DumpName : (ConfigScript.ConfigDirectory + inputFilename);
                tbLog.AppendText(String.Format("Input file name: {0}\n", fullInputFilename));

                var compressedFileName = fullInputFilename + ".lzkn1";
                tbLog.AppendText(String.Format("Try to compress input file with lzkn1 compressor\n"));

                var inputData = File.ReadAllBytes(fullInputFilename);
                byte[] compressedBytes = new byte[inputData.Length];
                int compressedSize = LZKN1.compress(inputData, compressedBytes, inputData.Length);
                tbLog.AppendText(String.Format("Compression complete. Compressed size: {0} bytes\n", compressedSize));

                byte[] realCompressedBytes = new byte[compressedSize];
                Array.Copy(compressedBytes, realCompressedBytes, compressedSize);

                int maxSize = CompressConfig.compressParams[selectedAddressIndex].maxSize;
                if (compressedSize > maxSize)
                {
                    throw new Exception(String.Format(@"Maximum archive size allowed to inserting to ROM at this address is: {0} bytes.
Final archive is to large: {1} bytes.
Try to make archive smaller or disable size checking in settings file", maxSize, compressedSize));
                }

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
                    int insertingAddress = CompressConfig.compressParams[selectedAddressIndex].address;
                    tbLog.AppendText(String.Format("Inserting archive in ROM at address: {0}\n", insertingAddress.ToString("X")));
                    Array.Copy(realCompressedBytes, 0, Globals.romdata, insertingAddress, compressedSize);
                    tbLog.AppendText("Inserting archive in ROM complete\n");

                    if (cbFillZero.Checked)
                    {
                        int zerosSize = maxSize - compressedSize;
                        tbLog.AppendText(String.Format("Filling free space in ROM with zeros: {0} bytes\n", zerosSize));
                        var fillArray = new byte[zerosSize];
                        Array.Copy(fillArray, 0, Globals.romdata, insertingAddress + compressedSize, zerosSize);
                    }
                    Globals.flushToFile();
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
    }
}
