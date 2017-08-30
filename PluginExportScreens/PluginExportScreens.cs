using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using CadEditor;
using Newtonsoft.Json;

namespace PluginExportScreens
{
    public class PluginExportScreens : IPlugin
    {
        public string getName()
        {
            return "Export screens";
        }
        public void addSubeditorButton(FormMain formMain)
        {
        }

        public void addToolButton(FormMain formMain)
        {
            this.formMain = formMain;
            var rm = new ResourceManager("PluginExportScreens.Icon", this.GetType().Assembly);

            var iconImport = (System.Drawing.Bitmap)rm.GetObject("icon_import");
            var item = new ToolStripButton("Import", iconImport, btImport_Click);
            item.DisplayStyle = ToolStripItemDisplayStyle.Image;
            formMain.addToolButton(item);

            var iconExportPic = (System.Drawing.Bitmap)rm.GetObject("icon_export");
            item = new ToolStripButton("Export pic", iconExportPic, bttExportPic_Click);
            item.DisplayStyle = ToolStripItemDisplayStyle.Image;
            formMain.addToolButton(item);

            var iconExportJson = (System.Drawing.Bitmap)rm.GetObject("icon_export");
            item = new ToolStripButton("Export json", iconExportJson, bttExportJson_Click);
            item.DisplayStyle = ToolStripItemDisplayStyle.Image;
            formMain.addToolButton(item);

            var iconExport = (System.Drawing.Bitmap)rm.GetObject("icon_export");
            item = new ToolStripButton("Export", iconExport, btExport_Click);
            item.DisplayStyle = ToolStripItemDisplayStyle.Image;
            formMain.addToolButton(item);
        }

        public void loadFromConfig(object asm, object data)
        {
        }


        private void bttExportPic_Click(object sender, EventArgs e)
        {
            int[][] screens, screens2;
            loadScreens(out screens, out screens2);

            SaveScreensCount.ExportMode = true;
            SaveScreensCount.Filename = "exportedScreens.png";
            var f = new SaveScreensCount();
            f.Text = "Export picture";

            formMain.subeditorOpen(f, (ToolStripButton)sender, true);

            if (SaveScreensCount.Result)
            {
                if (SaveScreensCount.Count <= 0)
                {
                    MessageBox.Show("Screens count value must be greater than 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int saveLastIndex = SaveScreensCount.First + SaveScreensCount.Count;
                if (saveLastIndex > screens.Length)
                {
                    MessageBox.Show(string.Format("First screen + Screens Count value ({0}) must be less than Total Screen Count in the game ({1}", saveLastIndex, screens.Length), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int first = SaveScreensCount.First;
                int WIDTH = ConfigScript.getScreenWidth(formMain.LevelNoForScreens);
                int HEIGHT = ConfigScript.getScreenHeight(formMain.LevelNoForScreens);
                float curScale = formMain.CurScale;
                int TILE_SIZE_X = (int)(formMain.Layer1.blockWidth * curScale);
                int TILE_SIZE_Y = (int)(formMain.Layer1.blockHeight * curScale);
                var probeIm = MapEditor.ScreenToImage(formMain.BigBlocks, new BlockLayer[] { formMain.Layer1, formMain.Layer2 }, formMain.ScreenNo, curScale, false, 0, 0, WIDTH, HEIGHT);
                int screenCount = SaveScreensCount.Count;
                var resultImage = new Bitmap(probeIm.Width * screenCount, probeIm.Height);
                using (var g = Graphics.FromImage(resultImage))
                {
                    for (int i = 0; i < screenCount; i++)
                    {
                        var im = MapEditor.ScreenToImage(formMain.BigBlocks, new BlockLayer[] { formMain.Layer1, formMain.Layer2 }, first + i, curScale, false, 0, 0, WIDTH, HEIGHT);
                        g.DrawImage(im, new Point(i * im.Width, 0));
                    }
                }
                resultImage.Save(SaveScreensCount.Filename);
            }
        }

        private void bttExportJson_Click(object sender, EventArgs e)
        {
            int[][] screens, screens2;
            loadScreens(out screens, out screens2);

            SaveScreensCount.ExportMode = true;
            SaveScreensCount.Filename = "exportedScreens.json";
            var f = new SaveScreensCount();
            f.Text = "Export json";

            formMain.subeditorOpen(f, (ToolStripButton)sender, true);

            if (SaveScreensCount.Result)
            {
                if (SaveScreensCount.Count <= 0)
                {
                    MessageBox.Show("Screens count value must be greater than 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int saveLastIndex = SaveScreensCount.First + SaveScreensCount.Count;
                if (saveLastIndex > screens.Length)
                {
                    MessageBox.Show(string.Format("First screen + Screens Count value ({0}) must be less than Total Screen Count in the game ({1}", saveLastIndex, screens.Length), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int first = SaveScreensCount.First;
                int[] indexes = screens[first];
                int[] indexes2 = null;
                if (ConfigScript.getLayersCount() > 1)
                    indexes2 = screens2[formMain.ScreenNo];
                int WIDTH = ConfigScript.getScreenWidth(formMain.LevelNoForScreens);
                int HEIGHT = ConfigScript.getScreenHeight(formMain.LevelNoForScreens);
                int screenCount = SaveScreensCount.Count;
                var screenParams = new { Width = WIDTH, Height = HEIGHT, Screens = new int[screenCount][] };
                using (TextWriter tw = new StreamWriter(SaveScreensCount.Filename))
                {
                    for (int i = 0; i < screenCount; i++)
                    {
                        indexes = screens[formMain.ScreenNo + i];
                        screenParams.Screens[i] = indexes;
                        /*if (ConfigScript.getLayersCount() > 1)
                            indexes2 = screens2[formMain.ScreenNo + i];*/
                    }
                    tw.WriteLine(JsonConvert.SerializeObject(screenParams));
                }
            }
        }

        private void btImport_Click(object sender, EventArgs e)
        {
            int[][] screens, screens2;
            loadScreens(out screens, out screens2);
            SaveScreensCount.ExportMode = false;
            SaveScreensCount.Filename = "exportedScreens.bin";
            var f = new SaveScreensCount();
            f.Text = "Import";
            formMain.subeditorOpen(f, (ToolStripButton)sender, true);
            if (SaveScreensCount.Result)
            {
                int saveLastIndex = SaveScreensCount.First;
                if (saveLastIndex > screens.Length)
                {
                    MessageBox.Show(string.Format("First screen ({0}) must be less than Total Screen Count in the game ({1}", saveLastIndex, screens.Length), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!File.Exists(SaveScreensCount.Filename))
                {
                    MessageBox.Show(string.Format("File ({0}) not exists", SaveScreensCount.Filename), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int screenSize = ConfigScript.screensOffset[formMain.LevelNoForScreens].recSize;
                int first = SaveScreensCount.First;
                var data = Utils.loadDataFromFile(SaveScreensCount.Filename);
                int screenCount = data.Length / screenSize;
                for (int i = 0; i < screenCount; i++)
                {
                    Array.Copy(data, i * screenSize, screens[first + i], 0, screenSize);
                }
            }
            formMain.SetScreens(screens);
            formMain.setDirty();
            formMain.reloadLevel(false);
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            int[][] screens, screens2;
            loadScreens(out screens, out screens2);
            SaveScreensCount.ExportMode = true;
            SaveScreensCount.Filename = "exportedScreens.bin";
            var f = new SaveScreensCount();
            f.Text = "Export";
            formMain.subeditorOpen(f, (ToolStripButton)sender, true);
            if (SaveScreensCount.Result)
            {
                if (SaveScreensCount.Count <= 0)
                {
                    MessageBox.Show("Screens count value must be greater than 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int saveLastIndex = SaveScreensCount.First + SaveScreensCount.Count;
                if (saveLastIndex > screens.Length)
                {
                    MessageBox.Show(string.Format("First screen + Screens Count value ({0}) must be less than Total Screen Count in the game ({1}", saveLastIndex, screens.Length), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int screenSize = ConfigScript.screensOffset[formMain.LevelNoForScreens].recSize;
                int screenCount = SaveScreensCount.Count;
                int first = SaveScreensCount.First;
                var data = new byte[screenSize * screenCount];

                for (int i = 0; i < screenCount; i++)
                {
                    byte[] byteScreen = new byte[screens[i + first].Length];
                    //all ints will be truncated to byte. it's ok for NES games, but may not for other platforms
                    byteScreen = Array.ConvertAll(screens[i + first], (int x)=>(byte)x);
                    Array.Copy(byteScreen, 0, data, screenSize * i, screenSize);
                }
                Utils.saveDataToFile(SaveScreensCount.Filename, data);
            }
        }

        private void loadScreens(out int[][] screens, out int[][] screens2)
        {
            screens = Utils.setScreens(formMain.LevelNoForScreens);
            screens2 = null;
            if (ConfigScript.getLayersCount() > 1)
                screens2 = Utils.setScreens2();
        }

        FormMain formMain;
    }
}
