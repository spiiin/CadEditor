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
            curVideo = 0x90;
            curPallete = 0;
            curPart = 0;
            dirty = false;
            updateSaveVisibility();
            curViewType = MapViewType.Tiles;

            initControls();
        
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
        }

        protected virtual void initControls()
        {
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

            //generic version
            cbTileset.SelectedIndex = formMain.CurActiveBigBlockNo;
            cbVideoNo.SelectedIndex = formMain.CurActiveVideoNo - 0x90;
            cbSmallBlock.SelectedIndex = formMain.CurActiveBlockNo;
            cbPaletteNo.SelectedIndex = formMain.CurActivePalleteNo;
            cbPart.SelectedIndex = 0;
            cbViewType.SelectedIndex = Math.Min((int)formMain.CurActiveViewType, cbViewType.Items.Count - 1);
        }

        protected void prepareAxisLabels()
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

        protected void reloadLevel(bool reloadBigBlocks = true)
        {
            curActiveBlock = 0;
            setSmallBlocks();
            if (reloadBigBlocks)
              setBigBlocksIndexes();
            mapScreen.Invalidate();
        }

        protected virtual void setSmallBlocks()
        {
            int backId, palId;
            backId = curVideo;
            palId = curPallete;

            var im = ConfigScript.videoNes.makeObjectsStrip((byte)backId, (byte)curTileset, (byte)palId, 1, curViewType);
            smallBlocks.Images.Clear();
            smallBlocks.Images.AddStrip(im);
            blocksPanel.Invalidate(true);
        }

        protected virtual void setBigBlocksIndexes()
        {
            bigBlockIndexes = ConfigScript.getBigBlocks(curSmallBlockNo);
        }

        protected virtual void exportBlocks()
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
                Bitmap result = new Bitmap((int)(32 * formMain.CurScale * 256),(int)(32 * formMain.CurScale)); //need some hack for duck tales 1
                using (Graphics g = Graphics.FromImage(result))
                {
                    for (int i = 0; i < ConfigScript.getBigBlocksCount(); i++)
                    {
                        Bitmap b;
                        /*switch (Globals.gameType)
                        {
                            //todo: write code to export blocks for TinyToon
                            //case GameTyp.TT:
                            //    b = ConfigScript.videoNes.makeBigBlockTT(i, 64, 64, bigBlockIndexes, smallBlocksAll, smallBlocksColorBytes);
                            //    break;
                            case GameTyp._3E:
                                b = ConfigScript.videoNes.makeBigBlock3E(i, 64, 64, bigBlockIndexes, smallBlocks);
                                break;
                            default:
                                b = ConfigScript.videoNes.makeBigBlock(i, 64, 64, bigBlockIndexes, smallBlocks);
                                break;
                        }*/
                        b = ConfigScript.videoNes.makeBigBlock(i, 64, 64, bigBlockIndexes, smallBlocks);
                        g.DrawImage(b, new Point((int)(32 * formMain.CurScale * i), 0));
                    }
                }
                result.Save(fn);
            }
        }

        protected int SMALL_BLOCKS_COUNT = 256;
        protected byte[] bigBlockIndexes;

        protected void mapScreen_Paint(object sender, PaintEventArgs e)
        {
            int addIndexes = curPart * 256 * 4;
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

        protected void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int addIndexes = curPart * 256 * 4;
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
                lbActive.Text = String.Format("({0:X})", curActiveBlock);
            }
        }

        protected void buttonObjClick(Object button, EventArgs e)
        {
            int index = ((Button)button).ImageIndex;
            pbActive.Image = smallBlocks.Images[index];
            lbActive.Text = String.Format("({0:X})", curActiveBlock);
            curActiveBlock = index;
        }

        protected int curActiveBlock;
        protected int curTileset;
        protected int curSmallBlockNo;

        //generic
        protected int curVideo;
        protected int curPallete;
        protected int curPart;

        protected MapViewType curViewType;

        protected bool dirty;
        protected bool readOnly;

        protected FormMain formMain;

        protected void updateSaveVisibility()
        {
            tbbSave.Enabled = dirty;
        }

        private void cbLevelPair_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (
                cbTileset.SelectedIndex == -1 ||
                cbVideoNo.SelectedIndex == -1 || 
                cbPaletteNo.SelectedIndex == -1 ||
                cbPart.SelectedIndex == -1 ||
                cbViewType.SelectedIndex == -1 || 
                cbSmallBlock.SelectedIndex == -1
                )
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

            //generic version
            curTileset = cbTileset.SelectedIndex;
            curSmallBlockNo = cbSmallBlock.SelectedIndex;
            curViewType = (MapViewType)cbViewType.SelectedIndex;

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

        protected void btSave_Click(object sender, EventArgs e)
        {
            saveToFile();
        }

        protected bool saveToFile()
        {
            ConfigScript.setBigBlocks(curSmallBlockNo, bigBlockIndexes);
            dirty = !Globals.flushToFile();
            updateSaveVisibility();
            return !dirty;
        }

        protected void BigBlockEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!readOnly && dirty)
            {
                DialogResult dr = MessageBox.Show("Tiles was changed. Do you want to save current tileset?", "Save", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                    saveToFile();
            }
        }

        protected void btClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to clear all blocks?", "Clear", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            for (int i = 0; i < ConfigScript.getBigBlocksCount() * 4; i++)
                bigBlockIndexes[i] = 0;
            dirty = true;
            updateSaveVisibility();
            mapScreen.Invalidate();
        }

        protected void btExport_Click(object sender, EventArgs e)
        {
            exportBlocks();
        }

        protected void btImport_Click(object sender, EventArgs e)
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

        protected void mapScreen_MouseMove(object sender, MouseEventArgs e)
        {
            int addIndexesText = curPart * 256;
            int bx = e.X / 32;
            int by = e.Y / 32;
            int dx = (e.X % 32) / 16;
            int dy = (e.Y % 32) / 16;
            int ind = ((by * 16 + bx) * 4 + (dy * 2 + dx)) / 4;
            if (ind > 255)
            {
                lbBigBlockNo.Text = "()";
            }
            else
            {
                int actualIndex = addIndexesText + ind;
                lbBigBlockNo.Text = String.Format("({0:X})", addIndexesText);
            }
        }

        protected void mapScreen_MouseLeave(object sender, EventArgs e)
        {
            lbBigBlockNo.Text = "()";
        }
    }
}
