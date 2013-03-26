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
            for (int i = 0; i < BIG_BLOCKS_COUNT; i++)
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
            int bigTileIndex = gameType == GameType.Generic ? curActiveBlockNo : Globals.levelData[curActiveLevel].bigBlockId;
            int bigBlocksAddr  = Globals.getBigTilesAddr((byte) bigTileIndex);
            for (int i = 0; i < BIG_BLOCKS_COUNT*4; i++)
                bigBlockIndexes[i] = Globals.romdata[bigBlocksAddr + i];
        }

        private void setBlocks()
        {
            int backId, blockId, palId;

            if (GameType.Generic == gameType)
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

            var im = Video.makeObjectsStrip((byte)backId, (byte)blockId, (byte)palId, 1, curViewType != MapViewType.Tiles);
            smallBlocks.Images.AddStrip(im);

            for (int i = 0; i < BIG_BLOCKS_COUNT; i++)
            {
                var b = new Bitmap(64, 64);
                using (Graphics g = Graphics.FromImage(b))
                {
                    g.DrawImage(smallBlocks.Images[bigBlockIndexes[i*4]], new Rectangle(0, 0, 32, 32));
                    g.DrawImage(smallBlocks.Images[bigBlockIndexes[i * 4 + 1]], new Rectangle(31, 0, 32, 32));
                    g.DrawImage(smallBlocks.Images[bigBlockIndexes[i * 4 + 2]], new Rectangle(0, 31, 32, 32));
                    g.DrawImage(smallBlocks.Images[bigBlockIndexes[i * 4 + 3]], new Rectangle(31, 31, 32, 32));
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
                g.DrawImage(bigBlocks.Images[index], new Rectangle((i % 8 + 1) * 64, i / 8 * 64, 64, 64));
            }
            if (showNeiScreens && (curActiveScreen > 0))
            {
                byte[] indexesPrev = screens[curActiveScreen - 1];
                for (int i = 0; i < SCREEN_SIZE; i++)
                {
                    if (i % 8 == 7)
                    {
                        int index = indexesPrev[i];
                        g.DrawImage(bigBlocks.Images[index], new Rectangle(0, i / 8 * 64, 64, 64));
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
                        g.DrawImage(bigBlocks.Images[index], new Rectangle(9*64, i / 8 * 64, 64, 64));
                    }
                }
            }
            g.DrawRectangle(new Pen(Color.Green, 4.0f), new Rectangle(64, 0, 512, 512));
        }

        //consts
        const int SCREEN_SIZE = 64;
        const int OBJECTS_COUNT = 96;
        const int BIG_BLOCKS_COUNT = 256;

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
        
        private byte[] bigBlockIndexes = new byte[BIG_BLOCKS_COUNT * 4];

        public static bool fileLoaded = false;

        private GameType gameType = GameType.Generic;

        private void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int dx = e.X / 64 - 1;
            int dy = e.Y / 64;
            if (dx == 8)
            {
                if (curActiveScreen < Globals.screensOffset.recCount - 1)
                {
                     int index = dy * 8;
                     screens[curActiveScreen + 1][index] = (byte)curActiveBlock;
                     dirty = true;
                }
            }
            else if (dx == -1)
            {
                if (curActiveScreen > 0)
                {
                    int index = dy * 8 + 7;
                    screens[curActiveScreen - 1][index] = (byte)curActiveBlock;
                    dirty = true;
                }
            }
            else
            {
                int index = dy * 8 + dx;
                screens[curActiveScreen][index] = (byte)curActiveBlock;
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
                int addr = 0x10 + i * 0x40;
                for (int x = 0; x < SCREEN_SIZE; x++)
                    Globals.romdata[addr + x] = screens[i][x];
            }
            //write to file
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



        private void cbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gameType == GameType.CAD)
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
            curViewType = cbViewType.SelectedIndex == 1 ? MapViewType.ObjType : MapViewType.Tiles;
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
            if (!Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
                return false;
 
            var f = new OpenFile();
            if (f.ShowDialog() == DialogResult.OK)
            {
                Globals.loadData(OpenFile.FileName, OpenFile.ConfigName);
                fileLoaded = true;
                gameType = GameType.Generic;
            }
            if (!fileLoaded)
                return false;
            return true;
            
        }

        public void reloadGameType(bool reloadVideo)
        {
            bool generic = gameType == GameType.Generic;
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
            gameType = cbGame.SelectedIndex == 0 ? GameType.Generic : GameType.CAD;
            reloadGameType(true);
        }
    }

    enum MapDrawMode 
    {
        Screens,
        Scrolls,
        Doors
    };

    enum MapViewType
    {
        Tiles,
        ObjType
    };
}
