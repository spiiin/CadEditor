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

            Utils.setCbItemsCount(cbVideoNo, Globals.videoOffset.recCount);
            Utils.setCbItemsCount(cbPaletteNo, Globals.palOffset.recCount);
            Utils.setCbItemsCount(cbPart, Globals.getBigBlocksCount() / 256);
            cbTileset.Items.Clear();
            for (int i = 0; i < Globals.bigBlocksOffset.recCount; i++)
            {
                var str = String.Format("Tileset{0} ({1:X})", i, 0x3000 + i * 0x4000);
                cbTileset.Items.Add(str);
            }
            cbTileset.SelectedIndex = 0;
            cbLevel.SelectedIndex = 0;
            cbDoor.SelectedIndex = 0;
            cbVideoNo.SelectedIndex = 0;
            cbTileset.SelectedIndex = 0;
            cbPaletteNo.SelectedIndex = 0;
            cbPart.SelectedIndex = 0;

            blocksPanel.Controls.Clear();
            blocksPanel.SuspendLayout();
            for (int i = 0; i < SMALL_BLOCKS_COUNT; i++)
            {
                var but = new Button();
                but.Size = new Size(32, 32);
                but.ImageList = smallBlocks;
                but.ImageIndex = i;
                but.Click += new EventHandler(buttonObjClick);
                blocksPanel.Controls.Add(but);
            }
            blocksPanel.ResumeLayout();
            reloadLevel();
        }

        private void reloadLevel()
        {
            curActiveBlock = 0;
            setSmallBlocks();
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

            var im = Video.makeObjectsStrip((byte)backId, (byte)curTileset, (byte)palId, 1, false);
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
            var addr = Globals.getBigTilesAddr((byte)curTileset);
            bigBlockIndexes = new byte[Globals.getBigBlocksCount()*4];
            for (int i = 0; i < Globals.getBigBlocksCount() * 4; i++)
                bigBlockIndexes[i] = Globals.romdata[addr + i];
        }

        const int SMALL_BLOCKS_COUNT = 256;
        private byte[] bigBlockIndexes;

        private void mapScreen_Paint(object sender, PaintEventArgs e)
        {
            int addIndexes = curPart * 256;
            Graphics g = e.Graphics;
            for (int i = 0; i < 256; i++)
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
            dirty = true;
            int bx = e.X / 32;
            int by = e.Y / 32;
            int dx = (e.X % 32) / 16;
            int dy = (e.Y % 32) / 16;
            int ind = (by * 16 + bx) * 4 + (dy * 2 + dx);
            bigBlockIndexes[addIndexes+ind] = (byte)curActiveBlock;
            mapScreen.Invalidate();
        }

        private void buttonObjClick(Object button, EventArgs e)
        {
            int index = ((Button)button).ImageIndex;
            pbActive.Image = smallBlocks.Images[index];
            curActiveBlock = index;
        }

        private int curActiveBlock;
        private int curTileset;

        //chip and dale
        private int curLevel;
        private int curDoor;

        //generic
        private int curVideo;
        private int curPallete;
        private int curPart;

        private bool dirty;

        private void cbLevelPair_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLevel.SelectedIndex == -1 || cbTileset.SelectedIndex == -1 || cbDoor.SelectedIndex == -1 ||
                cbVideoNo.SelectedIndex == -1 || cbPaletteNo.SelectedIndex == -1 || cbPart.SelectedIndex == -1)
            {
                return;
            }
            if (dirty && sender == cbTileset)
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
                }
            }
            curTileset = cbTileset.SelectedIndex;

            curLevel = cbLevel.SelectedIndex;
            curDoor = cbDoor.SelectedIndex - 1;

            curVideo = cbVideoNo.SelectedIndex + 0x90;
            curPallete = cbPaletteNo.SelectedIndex;
            curPart = cbPart.SelectedIndex;

            pnGeneric.Visible = Globals.gameType != GameType.CAD;
            pnEditCad.Visible = Globals.gameType == GameType.CAD;
            Utils.setCbItemsCount(cbPart, Globals.getBigBlocksCount() / 256);
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
            int addr = Globals.getBigTilesAddr((byte)curTileset);
            for (int i = 0; i < Globals.getBigBlocksCount() * 4; i++)
                Globals.romdata[addr + i] = bigBlockIndexes[i];
            dirty = !Globals.flushToFile();
            return !dirty;
        }

        private void BigBlockEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dirty)
            {
                DialogResult dr = MessageBox.Show("Tiles was changed. Do you want to save current tileset?", "Save", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                    saveToFile();
            }
        }
    }
}
