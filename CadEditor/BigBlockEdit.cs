using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
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
            curHierarchyLevel = 0;
            curTileset = 0;
            curVideo = 0x90;
            curPallete = 0;
            curPart = 0;
            dirty = false;
            updateSaveVisibility();
            curViewType = MapViewType.Tiles;

            initControls();
            reloadLevel();
            reloadBlocksPanel();

            readOnly = false; //must be read from config
            tbbSave.Enabled = !readOnly;
            tbbImport.Enabled = !readOnly;
        }

        protected void reloadBlocksPanel()
        {
            blocksPanel.Controls.Clear();
            blocksPanel.SuspendLayout();
            int sbc = smallBlocks.Images.Count;
            for (int i = 0; i < sbc; i++)
            {
                var but = new Button();
                but.FlatStyle = FlatStyle.Flat;
                but.Size = smallBlocks.Images[i].Size;
                but.ImageList = smallBlocks;
                but.ImageIndex = i;
                but.Margin = new Padding(0);
                but.Padding = new Padding(0);
                but.Click += new EventHandler(buttonObjClick);
                blocksPanel.Controls.Add(but);
            }
            blocksPanel.ResumeLayout();
        }

        protected virtual void initControls()
        {
            UtilsGui.setCbItemsCount(cbHierarchyLevel, ConfigScript.getbigBlocksHierarchyCount());
            UtilsGui.setCbItemsCount(cbVideoNo, ConfigScript.videoOffset.recCount);
            UtilsGui.setCbItemsCount(cbSmallBlock, ConfigScript.blocksOffset.recCount);
            UtilsGui.setCbItemsCount(cbPaletteNo, ConfigScript.palOffset.recCount);
            UtilsGui.setCbItemsCount(cbPart, Math.Max(ConfigScript.getBigBlocksCount(curHierarchyLevel) / 256, 1));
            cbTileset.Items.Clear();
            for (int i = 0; i < ConfigScript.bigBlocksOffsets[curTileset].recCount; i++)
            {
                var str = String.Format("Tileset{0}", i);
                cbTileset.Items.Add(str);
            }

            //generic version
            cbHierarchyLevel.SelectedIndex = 0;
            cbTileset.SelectedIndex = formMain.CurActiveBigBlockNo;
            cbVideoNo.SelectedIndex = formMain.CurActiveVideoNo - 0x90;
            cbSmallBlock.SelectedIndex = formMain.CurActiveBlockNo;
            cbPaletteNo.SelectedIndex = formMain.CurActivePalleteNo;
            cbPart.SelectedIndex = 0;
            cbViewType.SelectedIndex = Math.Min((int)formMain.CurActiveViewType, cbViewType.Items.Count - 1);
        }

        protected void reloadLevel(bool reloadBigBlocks = true)
        {
            curActiveBlock = 0;
            if (reloadBigBlocks)
              setBigBlocksIndexes();
            setSmallBlocks();
            reloadBlocksPanel();
            mapScreen.Invalidate();
        }

        protected virtual void setSmallBlocks()
        {
            int backId, palId;
            backId = curVideo;
            palId = curPallete;

            if (curHierarchyLevel == 0)
            {
                if (hasSmallBlocksPals())
                {
                    var im = ConfigScript.videoNes.makeObjectsStrip((byte)backId, (byte)curTileset, (byte)palId, 1, curViewType);
                    smallBlocks.ImageSize = new Size(16, 16);
                    smallBlocks.Images.Clear();
                    smallBlocks.Images.AddStrip(im);
                }
                else
                {
                    fillSmallBlockImageLists();
                }
            }
            else
            {
                var ims = ConfigScript.videoNes.makeBigBlocks(backId, curTileset, ConfigScript.getBigBlocksRecursive(curHierarchyLevel-1, curSmallBlockNo), palId, curViewType, 1, 2.0f, MapViewType.Tiles, false, curHierarchyLevel-1);
                smallBlocks.ImageSize = ims[0].Size;
                smallBlocks.Images.Clear();
                smallBlocks.Images.AddRange(ims);
            }
            blocksPanel.Invalidate(true);

            //prerender big blocks
            bigBlocksImages = ConfigScript.videoNes.makeBigBlocks(backId, curTileset, bigBlockIndexes, palId, curViewType, 1, 2.0f, MapViewType.Tiles, false, curHierarchyLevel);
            //
            int btc = Math.Min(ConfigScript.getBigBlocksCount(curHierarchyLevel), 256);
            int bblocksInRow = 16;
            int bblocksInCol = (btc / bblocksInRow) + 1;
            //
            mapScreen.Size = new Size(bigBlocksImages[0].Width* bblocksInRow, bigBlocksImages[0].Height*bblocksInCol);
        }

        private void fillSmallBlockImageLists()
        {
            int backId, palId;
            backId = curVideo;
            palId = curPallete;

            var smils = new ImageList[]{ smallBlocks, smallBlocks2, smallBlocks3, smallBlocks4 };
            for (int i = 0; i < 4; i++)
            {
                var smil = smils[i];
                var im = ConfigScript.videoNes.makeObjectsStrip((byte)backId, (byte)curTileset, (byte)palId, 1, curViewType, i);
                smil.ImageSize = new Size(16, 16);
                smil.Images.Clear();
                smil.Images.AddStrip(im);
            }
        }

        protected virtual void setBigBlocksIndexes()
        {
            bigBlockIndexes = ConfigScript.getBigBlocksRecursive(curHierarchyLevel, curSmallBlockNo);
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
                Utils.saveDataToFile(fn, Utils.linearizeBigBlocks(bigBlockIndexes));
            }
            else
            {
                //todo:add BigBlockWithPal version
                Bitmap result = new Bitmap((int)(32 * formMain.CurScale * 256),(int)(32 * formMain.CurScale)); //need some hack for duck tales 1
                var smb = smallBlocks.Images.Cast<Image>().ToArray();
                Image[][] smallBlocksPack = new Image[4][] {smb, smb, smb, smb }; 
                using (Graphics g = Graphics.FromImage(result))
                {
                    for (int i = 0; i < ConfigScript.getBigBlocksCount(curHierarchyLevel); i++)
                    {
                        Bitmap b;
                        b = bigBlockIndexes[i].makeBigBlock(smallBlocksPack);
                        g.DrawImage(b, new Point((int)(32 * formMain.CurScale * i), 0));
                    }
                }
                result.Save(fn);
            }
        }

        protected int SMALL_BLOCKS_COUNT = 256;
        protected BigBlock[] bigBlockIndexes;

        //hardcode
        private int getBlockWidth()
        {
            return smallBlocks.Images[0].Size.Width;
        }

        private int getBlockHeight()
        {
            return smallBlocks.Images[0].Size.Height;
        }

        protected void mapScreen_Paint(object sender, PaintEventArgs e)
        {
            int addIndexes = curPart * 256;
            Graphics g = e.Graphics;
            int btc = Math.Min(ConfigScript.getBigBlocksCount(curHierarchyLevel), 256);
            int bblocksInRow = 16;
            int bblocksInCol = (btc / bblocksInRow) + 1;

            var testBBlock = bigBlockIndexes[0];
            int bWidth = getBlockWidth();
            int bHeight = getBlockHeight();
            int bbWidth  =  bWidth  * testBBlock.width;
            int bbHeight =  bHeight * testBBlock.height;

            var pen = new Pen(Brushes.Magenta);

            for (int i = 0; i < btc; i++)
            {
                int xb = i % bblocksInRow;
                int yb = i / bblocksInRow;
                var rr = new Rectangle(xb * bbWidth, yb * bbHeight, bbWidth, bbHeight);
                g.DrawImage(bigBlocksImages[addIndexes + i], rr);
                g.DrawRectangle(pen, rr);
            }
        }

        private bool hasSmallBlocksPals()
        {
            return bigBlockIndexes[0].smallBlocksWithPal();
        }

        protected void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int addIndexes = curPart * 256;
            dirty = true; updateSaveVisibility();

            int btc = Math.Min(ConfigScript.getBigBlocksCount(curHierarchyLevel), 256);
            int bblocksInRow = 16;
            int bblocksInCol = (btc / bblocksInRow) + 1;

            var testBBlock = bigBlockIndexes[0];
            int bWidth = getBlockWidth();
            int bHeight = getBlockHeight();
            int bbWidth  =  bWidth  * testBBlock.width;
            int bbHeight =  bHeight * testBBlock.height;

            int bx = e.X / bbWidth;
            int by = e.Y / bbHeight;
            int dx = (e.X % bbWidth) / bWidth;
            int dy = (e.Y % bbHeight) / bHeight;
            int bigBlockIndex = by * bblocksInRow + bx;
            int insideIndex   = dy * testBBlock.width + dx;
            //prevent out in bounds
            if (bigBlockIndex >= btc)
            {
                return;
            }
            int actualIndex = addIndexes + bigBlockIndex;
            if (e.Button == MouseButtons.Left)
            {
                if (actualIndex < bigBlockIndexes.Length)
                    bigBlockIndexes[actualIndex].indexes[insideIndex] = curActiveBlock;
            }
            else
            {
                //first action - change pal byte if it applicable
                if (!hasSmallBlocksPals())
                {
                    if (actualIndex < bigBlockIndexes.Length)
                    {
                        var bbPal = bigBlockIndexes[actualIndex] as BigBlockWithPal;
                        if (bbPal == null)
                        {
                            return;
                        }
                        //
                        int palByte = bbPal.palBytes[insideIndex];
                        if (++palByte > 3)
                        {
                            palByte = 0;
                        }
                        bbPal.palBytes[insideIndex] = palByte;
                        //
                    }
                }
                //second action - change cur active block to selected
                if (actualIndex < bigBlockIndexes.Length)
                    curActiveBlock = bigBlockIndexes[actualIndex].indexes[insideIndex];
                pbActive.Image = smallBlocks.Images[curActiveBlock];
                lbActive.Text = String.Format("({0:X})", curActiveBlock);
            }

            //fix current big blocks image
            var imss = new Image[4][] {
                smallBlocks.Images.Cast<Image>().ToArray(),
                smallBlocks2.Images.Cast<Image>().ToArray(),
                smallBlocks3.Images.Cast<Image>().ToArray(),
                smallBlocks4.Images.Cast<Image>().ToArray()
            };
            bigBlocksImages[actualIndex] = bigBlockIndexes[actualIndex].makeBigBlock(imss);
            mapScreen.Invalidate();
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
        protected int curHierarchyLevel;

        //generic
        protected int curVideo;
        protected int curPallete;
        protected int curPart;

        protected MapViewType curViewType;

        protected bool dirty;
        protected bool readOnly;

        protected FormMain formMain;

        Image[] bigBlocksImages; //prerendered for faster rendering;

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
                cbSmallBlock.SelectedIndex == -1 ||
                cbHierarchyLevel.SelectedIndex == -1
                )
            {
                return;
            }
            if (!readOnly && dirty && (sender == cbTileset || sender == cbHierarchyLevel))
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
            curHierarchyLevel = cbHierarchyLevel.SelectedIndex;
            curTileset = cbTileset.SelectedIndex;
            curSmallBlockNo = cbSmallBlock.SelectedIndex;
            curViewType = (MapViewType)cbViewType.SelectedIndex;

            curVideo = cbVideoNo.SelectedIndex + 0x90;
            curPallete = cbPaletteNo.SelectedIndex;
            curPart = cbPart.SelectedIndex;
            UtilsGui.setCbItemsCount(cbPart, Math.Max(ConfigScript.getBigBlocksCount(curHierarchyLevel) / 256, 1));
            UtilsGui.setCbIndexWithoutUpdateLevel(cbPart, cbLevelPair_SelectedIndexChanged, curPart);
            reloadLevel();
        }

        private void returnCbLevelIndexes()
        {
            cbTileset.SelectedIndexChanged -= cbLevelPair_SelectedIndexChanged;
            cbTileset.SelectedIndex = curTileset;
            cbTileset.SelectedIndexChanged += cbLevelPair_SelectedIndexChanged;

            cbHierarchyLevel.SelectedIndexChanged -= cbLevelPair_SelectedIndexChanged;
            cbHierarchyLevel.SelectedIndex = curTileset;
            cbHierarchyLevel.SelectedIndexChanged += cbLevelPair_SelectedIndexChanged;
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            saveToFile();
        }

        protected bool saveToFile()
        {
            ConfigScript.setBigBlocksHierarchy(curHierarchyLevel, curSmallBlockNo, bigBlockIndexes);
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
            for (int i = 0; i < bigBlockIndexes.Length; i++)
            {
                var bb = bigBlockIndexes[i];
                for (int j = 0; j < bb.indexes.Length; j++)
                {
                    bb.indexes[j] = 0;
                }
            }
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
            bigBlockIndexes = Utils.unlinearizeBigBlocks<BigBlock>(data, 2,2);
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

            int btc = Math.Min(ConfigScript.getBigBlocksCount(curHierarchyLevel), 256);
            int bblocksInRow = 16;
            int bblocksInCol = (btc / bblocksInRow) + 1;

            var testBBlock = bigBlockIndexes[0];
            int bWidth = getBlockWidth();
            int bHeight = getBlockHeight();
            int bbWidth  =  bWidth  * testBBlock.width;
            int bbHeight =  bHeight * testBBlock.height;

            int bx = e.X / bbWidth;
            int by = e.Y / bbHeight;
            int dx = (e.X % bbWidth) / bWidth;
            int dy = (e.Y % bbHeight) / bHeight;
            int ind = ((by * bblocksInRow + bx) * testBBlock.getSize() + (dy * testBBlock.width + dx)) / testBBlock.getSize();
            if (ind > 255)
            {
                lbBigBlockNo.Text = "()";
            }
            else
            {
                int actualIndex = addIndexesText + ind;
                lbBigBlockNo.Text = String.Format("({0:X})", actualIndex);
            }
        }

        protected void mapScreen_MouseLeave(object sender, EventArgs e)
        {
            lbBigBlockNo.Text = "()";
        }
    }
}
