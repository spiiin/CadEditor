using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CadEditor
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!openFile())
            {
                Close();
                return;
            }

            cbScreenNo.Items.Clear();
            for (int i = 0; i < Globals.screensOffset.recCount; i++)
                cbScreenNo.Items.Add(String.Format("{0:X}", i+1));
            cbScreenNo.SelectedIndex = 0;
            setScreens();

            Utils.setCbItemsCount(cbVideoNo, Globals.videoOffset.recCount);
            Utils.setCbItemsCount(cbBigBlockNo, Globals.bigBlocksOffset.recCount);
            Utils.setCbItemsCount(cbBlockNo, Globals.blocksOffset.recCount);
            Utils.setCbItemsCount(cbPaletteNo, Globals.palOffset.recCount);
            Utils.setCbIndexWithoutUpdateLevel(cbVideoNo, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbBigBlockNo, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbBlockNo, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbPaletteNo, cbLevel_SelectedIndexChanged);

            Utils.setCbIndexWithoutUpdateLevel(cbLevel, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbDoor, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbViewType, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbGame, cbGame_SelectedIndexChanged);

            dirty = false;
            showNeiScreens = true;
            blocksPanel.Controls.Clear();
            blocksPanel.SuspendLayout();
            for (int i = 0; i < Globals.getBigBlocksCount(); i++)
            {
                var but = new Button();
                but.Size = new Size(64, 64);
                but.ImageList = bigBlocks;
                but.ImageIndex = i;
                but.Click += new EventHandler(buttonBlockClick);
                blocksPanel.Controls.Add(but);

            }
            blocksPanel.ResumeLayout();

            cbGame.SelectedIndex = 0;
            reloadGameType(false);
            cbLevel_SelectedIndexChanged(null, new EventArgs());
        }

        private void reloadLevel()
        {
            setBigBlocksIndexes();
            setBlocks();
            setScreens();
            updateMap();
        }

        private void setBigBlocksIndexes()
        {
            int bigTileIndex = (Globals.gameType != GameType.CAD) ? curActiveBlockNo : Globals.levelData[curActiveLevel].bigBlockId;
            int bigBlocksAddr  = Globals.getBigTilesAddr((byte) bigTileIndex);
            bigBlockIndexes = new byte[Globals.getBigBlocksCount() * 4];
            for (int i = 0; i < Globals.getBigBlocksCount() * 4; i++)
            {
                bigBlockIndexes[i] = Globals.romdata[bigBlocksAddr + i];
            }
            //duck tales version for level 1
            /*for (int i = 0; i < Globals.getBigBlocksCount(); i++)
            {
                bigBlockIndexes[i * 4  ] = Globals.romdata[0x10D4A + i];
                bigBlockIndexes[i * 4+1] = Globals.romdata[0x10E19 + i];
                bigBlockIndexes[i * 4+2] = Globals.romdata[0x10EE8 + i];
                bigBlockIndexes[i * 4+3] = Globals.romdata[0x10FB7 + i];
            }*/
        }

        private void setBlocks()
        {
            int backId, blockId, palId;

            if (GameType.CAD != Globals.gameType)
            {
                backId = curActiveVideoNo; ;
                blockId = curActiveBigBlockNo;
                palId = curActivePalleteNo;
            }
            else
            {
                var lr = Globals.levelRecsCad[curActiveLevel];
                blockId = Globals.levelData[curActiveLevel].bigBlockId;
                if (curActiveDoor < 0)
                {
                    backId = Globals.levelData[curActiveLevel].backId;
                    palId = Globals.levelData[curActiveLevel].palId;
                }
                else
                {
                    backId = Globals.doorsData[curActiveDoor].backId;
                    palId = Globals.doorsData[curActiveDoor].palId;
                }
            }

            smallBlocks.Images.Clear();
            bigBlocks.Images.Clear();

            MapViewType smallObjectsType = curViewType == MapViewType.ObjType ? MapViewType.ObjType : MapViewType.Tiles;
            var im = Video.makeObjectsStrip((byte)backId, (byte)blockId, (byte)palId, 1, smallObjectsType);
            smallBlocks.Images.AddStrip(im);

            for (int i = 0; i < Globals.getBigBlocksCount(); i++)
            {
                var b = new Bitmap(64, 64);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.DrawImage(smallBlocks.Images[bigBlockIndexes[i*4]], new Rectangle(0, 0, 32, 32));
                    g.DrawImage(smallBlocks.Images[bigBlockIndexes[i * 4 + 1]], new Rectangle(31, 0, 32, 32));
                    g.DrawImage(smallBlocks.Images[bigBlockIndexes[i * 4 + 2]], new Rectangle(0, 31, 32, 32));
                    g.DrawImage(smallBlocks.Images[bigBlockIndexes[i * 4 + 3]], new Rectangle(31, 31, 32, 32));
                    if (curViewType == MapViewType.ObjNumbers)
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(192, 255, 255, 255)), new Rectangle(0, 0, 64, 64));
                        g.DrawString(String.Format("{0:X}", i), new Font("Arial", 16), Brushes.Red, new Point(0, 0));
                    }
                }
                bigBlocks.Images.Add(b);
            }
            curActiveBlock = 0;
        }

        private void setScreens()
        {
            screens = new byte[Globals.screensOffset.recCount][];
            for (int i = 0; i < Globals.screensOffset.recCount; i++)
              screens[i] = Globals.getScreen(i);
        }

        private void updateMap()
        {
            mapScreen.Invalidate();
            blocksPanel.Invalidate(true);
        }

        private void buttonBlockClick(Object button, EventArgs e)
        {
            int index = ((Button)button).ImageIndex;
            activeBlock.Image = bigBlocks.Images[index];
            curActiveBlock = index;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (!fileLoaded)
                return;
            byte[] indexes = screens[curActiveScreen];
            var g = e.Graphics;
            for (int i = 0; i < SCREEN_SIZE; i++)
            {
                int index = indexes[i];
                int bigBlockNo = Globals.getBigTileNoFromScreen(indexes, i);
                g.DrawImage(bigBlocks.Images[bigBlockNo], new Rectangle((i % 8 + 1) * 64, i / 8 * 64, 64, 64));
            }
            if (showNeiScreens && (curActiveScreen > 0))
            {
                byte[] indexesPrev = screens[curActiveScreen - 1];
                for (int i = 0; i < SCREEN_SIZE; i++)
                {
                    if (i % 8 == 7)
                    {
                        int index = indexesPrev[i];
                        int bigBlockNo = Globals.getBigTileNoFromScreen(indexesPrev, i);
                        g.DrawImage(bigBlocks.Images[bigBlockNo], new Rectangle(0, i / 8 * 64, 64, 64));
                    }
                }
            }
            if (showNeiScreens && (curActiveScreen < Globals.screensOffset.recCount - 1))
            {
                byte[] indexesNext = screens[curActiveScreen + 1];
                for (int i = 0; i < SCREEN_SIZE; i++)
                {
                    if (i % 8 == 0)
                    {
                        int index = indexesNext[i];
                        int bigBlockNo = Globals.getBigTileNoFromScreen(indexesNext, i);
                        g.DrawImage(bigBlocks.Images[bigBlockNo], new Rectangle(9 * 64, i / 8 * 64, 64, 64));
                    }
                }
            }
            g.DrawRectangle(new Pen(Color.Green, 4.0f), new Rectangle(64, 0, 512, 512));
        }

        //consts
        const int SCREEN_SIZE = 64;
        const int OBJECTS_COUNT = 96;

        //editor globals
        private int curActiveBlock = 0;
        private int curActiveScreen = 0;

        //chip'n'dale specific
        private int curActiveLevel = 0;
        private int curActiveDoor = 0;
        //generic
        private int curActiveVideoNo = 0;
        private int curActiveBigBlockNo = 0;
        private int curActiveBlockNo = 0;
        private int curActivePalleteNo = 0;


        MapViewType curViewType = MapViewType.ObjType;
        private bool dirty;
        private bool showNeiScreens;
        private byte[][] screens = null;

        private byte[] bigBlockIndexes;

        public static bool fileLoaded = false;

        private void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int dx = e.X / 64 - 1;
            int dy = e.Y / 64;
            if (dx == 8)
            {
                if (curActiveScreen < Globals.screensOffset.recCount - 1)
                {
                     int index = dy * 8;
                     Globals.setBigTileToScreen(screens[curActiveScreen + 1], index, curActiveBlock);
                     dirty = true;
                }
            }
            else if (dx == -1)
            {
                if (curActiveScreen > 0)
                {
                    int index = dy * 8 + 7;
                    Globals.setBigTileToScreen(screens[curActiveScreen - 1], index, curActiveBlock);
                    dirty = true;
                }
            }
            else
            {
                int index = dy * 8 + dx;
                Globals.setBigTileToScreen(screens[curActiveScreen], index, curActiveBlock);
                dirty = true;
            }
            mapScreen.Invalidate();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            saveToFile();
        }

        private bool saveToFile()
        {
            var romFname = OpenFile.FileName;
            //write back tiles
            for (int i = 0; i < Globals.screensOffset.recCount; i++)
            {
                int addr = Globals.screensOffset.beginAddr + i * Globals.screensOffset.recSize;
                for (int x = 0; x < Globals.screensOffset.recSize; x++)
                    Globals.romdata[addr + x] = screens[i][x];
            }
            dirty = !Globals.flushToFile();
            return !dirty;
        }



        private void cbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Globals.gameType == GameType.CAD)
            {
                curActiveLevel = cbLevel.SelectedIndex;
                curActiveDoor = cbDoor.SelectedIndex - 1;
            }
            else
            {
                curActiveVideoNo = cbVideoNo.SelectedIndex + 0x90;
                curActiveBigBlockNo = cbBigBlockNo.SelectedIndex;
                curActiveBlockNo = cbBlockNo.SelectedIndex;
                curActivePalleteNo = cbPaletteNo.SelectedIndex;
            }
            curViewType = (MapViewType)cbViewType.SelectedIndex;
            reloadLevel();
        }

        private void returnCbLevelIndex()
        {
            cbLevel.SelectedIndexChanged -= cbLevel_SelectedIndexChanged;
            cbLevel.SelectedIndex = curActiveLevel;
            cbLevel.SelectedIndexChanged += cbLevel_SelectedIndexChanged;
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            if (Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
            {
                var b = new BigBlockEdit();
                b.ShowDialog();
                reloadLevel();
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
                e.Cancel = true;
        }

        private void btEditObjs_Click(object sender, EventArgs e)
        {
            if (Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
            {
                var b = new BlockEdit();
                b.ShowDialog();
                reloadLevel();
            }
        }


        private void btEditLayout_Click(object sender, EventArgs e)
        {
            if (Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
            {
                var f = new EditLayout();
                f.ShowDialog();
                reloadLevel();
            }
        }

        private void editEnemy_Click(object sender, EventArgs e)
        {
            if (Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
            {
                var f = new EnemyEditor();
                f.ShowDialog();
                reloadLevel();
            }
        }

        private void cbScreenNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbScreenNo.SelectedIndex == -1)
                return;
            curActiveScreen = cbScreenNo.SelectedIndex;
            mapScreen.Invalidate();
        }


        private void cbShowNeighborns_CheckedChanged(object sender, EventArgs e)
        {
            showNeiScreens = cbShowNeighborns.Checked;
            mapScreen.Invalidate();
        }

        private bool openFile()
        {
            Globals.gameType = GameType.Generic;
            if (!Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
                return false;
 
            var f = new OpenFile();
            if (f.ShowDialog() == DialogResult.OK)
            {
                Globals.loadData(OpenFile.FileName, OpenFile.ConfigName);
                fileLoaded = true;
            }
            if (!fileLoaded)
                return false;
            return true;
            
        }

        public void reloadGameType(bool reloadVideo)
        {
            bool generic = Globals.gameType != GameType.CAD;
            pnGeneric.Visible = generic;
            pnCad.Visible = !generic;
            if (reloadVideo)
              cbLevel_SelectedIndexChanged(null, new EventArgs());
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            openFile();
            cbGame.SelectedIndex = 0; 
            reloadGameType(false);
            cbLevel_SelectedIndexChanged(null, new EventArgs());
        }

        private void cbGame_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.gameType = cbGame.SelectedIndex == 0 ? GameType.Generic : (cbGame.SelectedIndex == 1) ? GameType.CAD : GameType.DT; 
            reloadGameType(true);
        }

        private void btVideo_Click(object sender, EventArgs e)
        {
            if (Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
            {
                var f = new EditVideo();
                f.ShowDialog();
                reloadLevel();
            }
        }
    }
}
