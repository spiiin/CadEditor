using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

using CadEditor;

namespace PluginExportScreens
{
    public partial class ExportTMX : Form
    {
        public ExportTMX()
        {
            InitializeComponent();
        }

        private int calcScrNo(LevelLayerData layout, int noInLayout)
        {
            return layout.layer[noInLayout] - 1;
        }

        private byte[] packLayerData(int[] layerData)
        {
            var ms = new MemoryStream();
            using (var writer = new BinaryWriter(ms))
            {
                foreach (int v in layerData)
                {
                    writer.Write(v);
                }
            }

            byte[] data = ms.ToArray();
            var msDeflated = new MemoryStream();
            using (var ds = new GZipStream(msDeflated, CompressionMode.Compress))
            {
                ds.Write(data, 0, data.Length);
            }

            byte[] deflactedData = msDeflated.ToArray();
            return deflactedData;
        }

        private string toZippedBase64String(int[] data)
        {
            return Convert.ToBase64String(packLayerData(data));
        }

        public Image prepareImage(string imName)
        {
            //add scale?
            var bigBlocksImages = formMain.bigBlocks;
            int blockWidth = bigBlocksImages[0].Width;
            int blockHeight = bigBlocksImages[0].Height;
            int imWidthInBlocks = 16;
            int imHeightInBlocks = (int)(Math.Ceiling(bigBlocksImages.Length * 1.0 / imWidthInBlocks));
            var bigBlockImage = UtilsGDI.GlueImages(bigBlocksImages, imWidthInBlocks, imHeightInBlocks);
            bigBlockImage.Save(imName);
            return bigBlockImage;
        }

        private int[] prepaerLayerData()
        {
            int layoutNo = cbLayout.SelectedIndex;
            var layout = ConfigScript.getLayout(layoutNo);

            int scrNo = calcScrNo(layout, 0);
            int width = formMain.screens[scrNo].width;
            int height = formMain.screens[scrNo].height;

            int layerWidth = layout.width * width;
            int layerHeight = layout.height * height;
            int[] layerData = new int[layerWidth * layerHeight];

            for (int sy = 0; sy < layout.height; sy++)
            {
                for (int sx = 0; sx < layout.width; sx++)
                {
                    int scrIndex = sy * layout.width + sx;
                    scrNo = calcScrNo(layout, scrIndex);
                    if (scrNo >= 0 && scrNo < formMain.screens.Length)
                    {
                        var curScreen = formMain.screens[scrNo];
                        var curScreenData = curScreen.layers[0].data;
                        for (int y = 0; y < height; y++)
                        {
                            for (int x = 0; x < width; x++)
                            {
                                int index = y * width + x;
                                int tileNo = ConfigScript.getBigTileNoFromScreen(curScreenData, index);
                                int lx = sx * width + x;
                                int ly = sy * height + y;

                                layerData[ly * layerWidth + lx] = tileNo + 1; //Tiled indexes start from 1, not 0
                            }
                        }
                    }
                }
            }

            return layerData;
        }

        private string tmxTemplate(int mapWidth, int mapHeight, int tileWidth, int tileHeight, string imageName, int imageWidth, int imageHeight, string base64mapDataString)
        {
            return $@"<?xml version='1.0' encoding='UTF-8'?>
<map width='{mapWidth}' height='{mapHeight}' orientation='orthogonal' tilewidth='{tileHeight}' tileheight='{tileWidth}' version='1.0'><tileset firstgid='1' name='Tiles' tilewidth='{tileWidth}' tileheight='{tileHeight}'><image width='{imageWidth}' height='{imageHeight}' source='{imageName}'/></tileset><layer height='{mapHeight}' name='Layer1' width='{mapWidth}'><data compression='gzip' encoding='base64'>{base64mapDataString}</data></layer></map>";
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (sfSave.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                var imName = Path.ChangeExtension(sfSave.FileName, "png");

                var bigBlockImage = prepareImage(imName);
                var base64LayerData = toZippedBase64String(prepaerLayerData());

                int layoutNo = cbLayout.SelectedIndex;
                var layout = ConfigScript.getLayout(layoutNo);

                int scrNo = calcScrNo(layout, 0);
                int width = formMain.screens[scrNo].width;
                int height = formMain.screens[scrNo].height;
                var bigBlocksImages = formMain.bigBlocks;
                int blockWidth = bigBlocksImages[0].Width;
                int blockHeight = bigBlocksImages[0].Height;

                int layerWidth = layout.width * width;
                int layerHeight = layout.height * height;

                using (var f = File.CreateText(sfSave.FileName))
                {
                    f.Write(tmxTemplate(layerWidth, layerHeight, blockWidth, blockHeight, imName, bigBlockImage.Width, bigBlockImage.Height, base64LayerData));
                }

                MessageBox.Show("Export done!");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private FormMain formMain;

        public void setFormMain(FormMain f)
        {
            formMain = f;
        }

        private void ExportTMX_Load(object sender, EventArgs e)
        {
            cbLayout.Items.Clear();
            foreach (var lr in ConfigScript.getLevelRecs())
                cbLayout.Items.Add(String.Format("Layout {0}:0x{1:X} ({2}x{3})", lr.name, lr.layoutAddr, lr.width, lr.height));
            cbLayout.SelectedIndex = 0;
        }

        private void ExportTMX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btExport_Click(btExport, new EventArgs());
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
