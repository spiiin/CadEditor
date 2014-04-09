using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CadEditor
{
    public partial class BigBlockEdit : Form
    {
        public BigBlockEdit()
        {
            InitializeComponent();
        }

        private void BigBlockEdit_Load(object sender, EventArgs e)
        {
            curTileset = 0;
            curLevel = 0;
            curDoor = -1;
            curVideo = 0x90;
            curPallete = 0;
            curPart = 0;
            dirty = false;
            updateSaveVisibility();
            curViewType = MapViewType.Tiles;

            Utils.setCbItemsCount(cbVideoNo, ConfigScript.videoOffset.recCount);
            Utils.setCbItemsCount(cbSmallBlock, ConfigScript.blocksOffset.recCount);
            Utils.setCbItemsCount(cbPaletteNo, ConfigScript.palOffset.recCount);
            Utils.setCbItemsCount(cbPart, Math.Max(ConfigScript.getBigBlocksCount() / 256, 1));
            cbTileset.Items.Clear();
            for (int i = 0; i < ConfigScript.bigBlocksOffset.recCount; i++)
            {
                var str = String.Format("Tileset{0}", i);
                cbTileset.Items.Add(str);
            }
            cbLevel.SelectedIndex = 0;
            cbDoor.SelectedIndex = 0;
            if (Globals.gameType != GameType.CAD)
            {
                cbTileset.SelectedIndex = formMain.CurActiveBigBlockNo;
                cbVideoNo.SelectedIndex = formMain.CurActiveVideoNo - 0x90;
                cbSmallBlock.SelectedIndex = formMain.CurActiveBlockNo;
                cbPaletteNo.SelectedIndex = formMain.CurActivePalleteNo;
                cbPart.SelectedIndex = 0;
                cbViewType.SelectedIndex = (int)formMain.CurActiveViewType;
            }
            else
            {
                cbTileset.SelectedIndex = 0;
                cbVideoNo.SelectedIndex = 0;
                cbSmallBlock.SelectedIndex = 0;
                cbPaletteNo.SelectedIndex = 0;
                cbPart.SelectedIndex = 0;
                cbViewType.SelectedIndex = 0;
            }

            blocksPanel.Controls.Clear();
            blocksPanel.SuspendLayout();
            for (int i = 0; i < SMALL_BLOCKS_COUNT; i++)
            {
                var but = new Button();
                but.Size = new Size(32, 32);
                but.ImageList = smallBlocks;
                but.ImageIndex = i;
                but.Margin = new Padding(0);
                but.Padding = new Padding(0);
                but.Click += new EventHandler(buttonObjClick);
                blocksPanel.Controls.Add(but);
            }
            blocksPanel.ResumeLayout();
            prepareAxisLabels();
            reloadLevel();

            readOnly = false; //must be read from config
            tbbSave.Enabled = !readOnly;
            tbbImport.Enabled = !readOnly;

            pnGeneric.Visible = Globals.gameType != GameType.CAD;
            pnEditCad.Visible = Globals.gameType == GameType.CAD;
        }

        private void prepareAxisLabels()
        {
            int x = mapScreen.Location.X;
            int y = mapScreen.Location.Y;
            for (int i = 0; i < 16; i++)
            {
                var l = new Label();
                l.Size = new System.Drawing.Size(12, 12);
                l.Location = new Point(x-16, y+10 + i*32);
                l.Text = String.Format("{0:X}", i);
                this.Controls.Add(l);

                var l2 = new Label();
                l2.Size = new System.Drawing.Size(12, 12);
                l2.Location = new Point(x+8 + i*32, y-16);
                l2.Text = String.Format("{0:X}", i);
                this.Controls.Add(l2);
            }
        }

        private void reloadLevel(bool reloadBigBlocks = true)
        {
            curActiveBlock = 0;
            setSmallBlocks();
            if (reloadBigBlocks)
              setBigBlocksIndexes();
            mapScreen.Invalidate();
        }

        private void setSmallBlocks()
        {
            int backId, palId;

            if (Globals.gameType == GameType.CAD)
            {
                var ld = Globals.levelData[curLevel];
                if (curDoor < 0)
                {
                    backId = ld.backId;
                    palId = ld.palId;
                }
                else
                {
                    DoorData dd = Globals.doorsData[curDoor];
                    backId = dd.backId;
                    palId = dd.palId;
                }
            }
            else
            {
                backId = curVideo;
                palId = curPallete;
            }

            var im = Video.makeObjectsStrip((byte)backId, (byte)curTileset, (byte)palId, 1, curViewType);
            smallBlocks.Images.Clear();
            smallBlocks.Images.AddStrip(im);
            /*for (int i = 0; i < SMALL_BLOCKS_COUNT ; i++)
            {
                var but = (Button)blocksPanel.Controls[i];
                but.ImageList = smallBlocks;
                but.ImageIndex = i;
            }*/
            blocksPanel.Invalidate(true);
        }

        private void setBigBlocksIndexes()
        {
            bigBlockIndexes = ConfigScript.getBigBlocks(Globals.gameType == GameType.CAD ? curTileset : curSmallBlockNo);
        }

        const int SMALL_BLOCKS_COUNT = 256;
        private byte[] bigBlockIndexes;

        private void mapScreen_Paint(object sender, PaintEventArgs e)
        {
            int addIndexes = curPart * 256;
            Graphics g = e.Graphics;
            int btc = Math.Min(ConfigScript.getBigBlocksCount(), 256);
            for (int i = 0; i < btc; i++)
            {
                int xb = i%16;
                int yb = i/16;
                g.DrawImage(smallBlocks.Images[bigBlockIndexes[addIndexes+i * 4]], new Rectangle(xb * 32, yb * 32, 16, 16));
                g.DrawImage(smallBlocks.Images[bigBlockIndexes[addIndexes+i * 4 + 1]], new Rectangle(xb * 32 + 16, yb * 32, 15, 16));
                g.DrawImage(smallBlocks.Images[bigBlockIndexes[addIndexes+i * 4 + 2]], new Rectangle(xb * 32, yb * 32 + 16, 16, 15));
                g.DrawImage(smallBlocks.Images[bigBlockIndexes[addIndexes+i * 4 + 3]], new Rectangle(xb * 32 + 16, yb * 32 + 16, 15, 15));
            }
        }

        private void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int addIndexes = curPart * 256;
            dirty = true; updateSaveVisibility();
            int bx = e.X / 32;
            int by = e.Y / 32;
            int dx = (e.X % 32) / 16;
            int dy = (e.Y % 32) / 16;
            int ind = (by * 16 + bx) * 4 + (dy * 2 + dx);
            int actualIndex = addIndexes + ind;
            if (e.Button == MouseButtons.Left)
            {
                if (actualIndex < bigBlockIndexes.Length)
                    bigBlockIndexes[actualIndex] = (byte)curActiveBlock;
                mapScreen.Invalidate();
            }
            else
            {
                if (actualIndex < bigBlockIndexes.Length)
                    curActiveBlock = bigBlockIndexes[actualIndex];
                pbActive.Image = smallBlocks.Images[curActiveBlock];
            }
        }

        private void buttonObjClick(Object button, EventArgs e)
        {
            int index = ((Button)button).ImageIndex;
            pbActive.Image = smallBlocks.Images[index];
            curActiveBlock = index;
        }

        private int curActiveBlock;
        private int curTileset;
        private int curSmallBlockNo;

        //chip and dale
        private int curLevel;
        private int curDoor;

        //generic
        private int curVideo;
        private int curPallete;
        private int curPart;

        private MapViewType curViewType;

        private bool dirty;
        private bool readOnly;

        private FormMain formMain;

        private void updateSaveVisibility()
        {
            tbbSave.Enabled = dirty;
        }

        private void cbLevelPair_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLevel.SelectedIndex == -1 || cbTileset.SelectedIndex == -1 || cbDoor.SelectedIndex == -1 ||
                cbVideoNo.SelectedIndex == -1 || cbPaletteNo.SelectedIndex == -1 || cbPart.SelectedIndex == -1 ||
                cbViewType.SelectedIndex == -1 || cbSmallBlock.SelectedIndex == -1)
            {
                return;
            }
            if (!readOnly && dirty && sender == cbTileset)
            {
                DialogResult dr = MessageBox.Show("Tiles was changed. Do you want to save current tileset?", "Save", MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Cancel)
                {
                    returnCbLevelIndexes();
                    return;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!saveToFile())
                    {
                        returnCbLevelIndexes();
                        return;
                    }
                }
                else
                {
                    dirty = false;
                    updateSaveVisibility();
                }
            }
            curTileset = cbTileset.SelectedIndex;
            curSmallBlockNo = cbSmallBlock.SelectedIndex;
            curViewType = (MapViewType)cbViewType.SelectedIndex;

            curLevel = cbLevel.SelectedIndex;
            curDoor = cbDoor.SelectedIndex - 1;

            curVideo = cbVideoNo.SelectedIndex + 0x90;
            curPallete = cbPaletteNo.SelectedIndex;
            curPart = cbPart.SelectedIndex;
            Utils.setCbItemsCount(cbPart, Math.Max(ConfigScript.getBigBlocksCount() / 256, 1));
            Utils.setCbIndexWithoutUpdateLevel(cbPart, cbLevelPair_SelectedIndexChanged, curPart);
            reloadLevel();
        }

        private void returnCbLevelIndexes()
        {
            cbTileset.SelectedIndexChanged -= cbLevelPair_SelectedIndexChanged;
            cbTileset.SelectedIndex = curTileset;
            cbTileset.SelectedIndexChanged += cbLevelPair_SelectedIndexChanged;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            saveToFile();
        }

        private bool saveToFile()
        {
            ConfigScript.setBigBlocks(curTileset, bigBlockIndexes);
            dirty = !Globals.flushToFile();
            updateSaveVisibility();
            return !dirty;
        }

        private void BigBlockEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!readOnly && dirty)
            {
                DialogResult dr = MessageBox.Show("Tiles was changed. Do you want to save current tileset?", "Save", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                    saveToFile();
            }
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to clear all blocks?", "Clear", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            for (int i = 0; i < ConfigScript.getBigBlocksCount() * 4; i++)
                bigBlockIndexes[i] = 0;
            dirty = true;
            updateSaveVisibility();
            mapScreen.Invalidate();
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            //duck tales 2 has other format
            var f = new SelectFile();
            f.Filename = "exportedBigBlocks.bin";
            f.ShowExportParams = true;
            f.ShowDialog();
            if (!f.Result)
                return;
            var fn = f.Filename;
            if (f.getExportType() == ExportType.Binary)
            {
                Utils.saveDataToFile(fn, bigBlockIndexes);
            }
            else
            {
                Bitmap result = new Bitmap(64*256, 64); //need some hack for duck tales 1
                using (Graphics g = Graphics.FromImage(result))
                {
                    for (int i = 0; i < ConfigScript.getBigBlocksCount(); i++)
                    {
                        Bitmap b;
                        switch (Globals.gameType)
                        {
                            //todo: write code to export blocks for TinyToon
                            /*case GameType.TT:
                                b = Video.makeBigBlockTT(i, 64, 64, bigBlockIndexes, smallBlocksAll, smallBlocksColorBytes);
                                break;*/
                            case GameType._3E:
                                b = Video.makeBigBlock3E(i, 64, 64, bigBlockIndexes, smallBlocks);
                                break;
                            default:
                                b = Video.makeBigBlock(i, 64, 64, bigBlockIndexes, smallBlocks);
                                break;
                        }
                        g.DrawImage(b, new Point(64 * i, 0));
                    }
                }
                result.Save(fn);
            }
        }

        private void btImport_Click(object sender, EventArgs e)
        {
            var f = new SelectFile();
            f.Filename = "exportedBigBlocks.bin";
            f.ShowDialog();
            if (!f.Result)
                return;
            var fn = f.Filename;
            var data = Utils.loadDataFromFile(fn);
            //duck tales 2 has other format
            bigBlockIndexes = data;
            reloadLevel(false);
            dirty = true;
            updateSaveVisibility();
        }

        public void setFormMain(FormMain f)
        {
            formMain = f;
        }
    }
}
