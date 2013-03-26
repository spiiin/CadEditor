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
            dirty = false;
            gameType = GameType.Generic;

            Utils.setCbItemsCount(cbVideoNo, Globals.videoOffset.recCount);
            Utils.setCbItemsCount(cbPaletteNo, Globals.palOffset.recCount);
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
            cbGame.SelectedIndex = 0;

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

            if (gameType == GameType.CAD)
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
            for (int i = 0; i < BIG_BLOCKS_COUNT * 4; i++)
                bigBlockIndexes[i] = Globals.romdata[addr + i];
        }

        const int BIG_BLOCKS_COUNT = 256;
        const int SMALL_BLOCKS_COUNT = 256;
        private byte[] bigBlockIndexes = new byte[BIG_BLOCKS_COUNT * 4];

        private void mapScreen_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 0; i < BIG_BLOCKS_COUNT; i++)
            {
                int xb = i%16;
                int yb = i/16;
                g.DrawImage(smallBlocks.Images[bigBlockIndexes[i*4]], new Rectangle(xb*32, yb *32, 16, 16));
                g.DrawImage(smallBlocks.Images[bigBlockIndexes[i * 4+1]], new Rectangle(xb * 32 +16, yb * 32, 15, 16));
                g.DrawImage(smallBlocks.Images[bigBlockIndexes[i * 4+2]], new Rectangle(xb * 32, yb * 32+16, 16, 15));
                g.DrawImage(smallBlocks.Images[bigBlockIndexes[i * 4+3]], new Rectangle(xb * 32+16, yb * 32+16, 15, 15));
            }
        }

        private void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            dirty = true;
            int bx = e.X / 32;
            int by = e.Y / 32;
            int dx = (e.X % 32) / 16;
            int dy = (e.Y % 32) / 16;
            int ind = (by * 16 + bx) * 4 + (dy * 2 + dx);
            bigBlockIndexes[ind] = (byte)curActiveBlock;
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

        private bool dirty;
        private GameType gameType;

        private void cbLevelPair_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLevel.SelectedIndex == -1 || cbTileset.SelectedIndex == -1 || cbDoor.SelectedIndex == -1 ||
                cbVideoNo.SelectedIndex == -1 || cbPaletteNo.SelectedIndex == -1 || cbGame.SelectedIndex == -1)
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
            gameType = cbGame.SelectedIndex == 0 ? GameType.Generic : GameType.CAD;
            pnGeneric.Visible = gameType == GameType.Generic;
            pnEditCad.Visible = gameType == GameType.CAD;
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
            for (int i = 0; i < BIG_BLOCKS_COUNT * 4; i++)
                Globals.romdata[addr + i] = bigBlockIndexes[i];

            string romFname = OpenFile.FileName;
            try
            {
                using (FileStream f = File.OpenWrite(romFname))
                {
                    f.Write(Globals.romdata, 0, Globals.FILE_SIZE);
                    f.Seek(0, SeekOrigin.Begin);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            dirty = false;
            return true;
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
