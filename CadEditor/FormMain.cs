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
           

            cbScreenNo.Items.Clear();
            for (int i = 0; i < SCREEN_COUNT; i++)
                cbScreenNo.Items.Add(String.Format("{0:X}", i+1));
            cbScreenNo.SelectedIndex = 0;
            setScreens();

            cbLevel.SelectedIndex = 0;
            cbViewType.SelectedIndexChanged -= cbLevel_SelectedIndexChanged;
            cbViewType.SelectedIndex = 0;
            cbViewType.SelectedIndexChanged += cbLevel_SelectedIndexChanged;
            cbDoor.SelectedIndexChanged -= cbLevel_SelectedIndexChanged;
            cbDoor.SelectedIndex = 0;
            cbDoor.SelectedIndexChanged += cbLevel_SelectedIndexChanged;
            dirty = false;
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
            int bigTileIndex = Globals.levelData[curActiveLevel].bigBlockId;
            int bigBlocksAddr  = Globals.getBigTilesAddr((byte) bigTileIndex);
            for (int i = 0; i < BIG_BLOCKS_COUNT*4; i++)
                bigBlockIndexes[i] = Globals.romdata[bigBlocksAddr + i];
        }

        private void setBlocks()
        {
            var lr = Globals.levelRecs[curActiveLevel];
            smallBlocks.Images.Clear();
            bigBlocks.Images.Clear();
            blocksPanel.Controls.Clear();
            int backId, palId;
            int blockId = Globals.levelData[curActiveLevel].bigBlockId;
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
            curActiveBlock = 0;
        }

        private void setScreens()
        {
            screens = new byte[SCREEN_COUNT][];
            for (int i = 0; i < SCREEN_COUNT; i++)
              screens[i] = Video.getScreen(i);
        }

        private void updateMap()
        {
            mapScreen.Invalidate();
        }

        private void buttonBlockClick(Object button, EventArgs e)
        {
            int index = ((Button)button).ImageIndex;
            activeBlock.Image = bigBlocks.Images[index];
            curActiveBlock = index;
        }



        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            byte[] indexes = screens[curActiveScreen];
            var g = e.Graphics;
            for (int i = 0; i < SCREEN_SIZE; i++)
            {
                int index = indexes[i];
                g.DrawImage(bigBlocks.Images[index], new Rectangle((i % 8 + 1) * 64, i / 8 * 64, 64, 64));
            }
            if (curActiveScreen > 0)
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
            if (curActiveScreen < SCREEN_COUNT - 1)
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

        const int SCREEN_SIZE = 64;
        const int OBJECTS_COUNT = 96;
        const int BIG_BLOCKS_COUNT = 256;
        const int SCREEN_COUNT = 300;
        private int curActiveBlock = 0;
        private int curActiveScreen = 0;
        private int curActiveLevel = 0;
        private int curActiveDoor = 0;
        MapViewType curViewType = MapViewType.ObjType;
        private bool dirty;
        private byte[][] screens = null;
        
        private byte[] bigBlockIndexes = new byte[BIG_BLOCKS_COUNT * 4];

        private void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int dx = e.X / 64 - 1;
            int dy = e.Y / 64;
            if (dx == 8)
            {
                if (curActiveScreen < SCREEN_COUNT - 1)
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
            var romFname = "Chip 'n Dale Rescue Rangers (U) [!].nes";
            LevelRec lr = Globals.levelRecs[curActiveLevel];
            //write back tiles
            for (int i = 0; i < SCREEN_COUNT; i++)
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
            curActiveLevel = cbLevel.SelectedIndex;
            curActiveDoor = cbDoor.SelectedIndex - 1;
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
            //TODO: refactor this block to separate method
            if (dirty)
            {
                DialogResult dr = MessageBox.Show("Level was changed. Do you want to save current level?", "Save", MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Cancel)
                {
                    returnCbLevelIndex();
                    return;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!saveToFile())
                    {
                        returnCbLevelIndex();
                        return;
                    }
                }
                else
                {
                    dirty = false;
                }
            }
            var b = new BigBlockEdit();
            b.ShowDialog();
            reloadLevel();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dirty)
            {
                DialogResult dr = MessageBox.Show("Level was changed. Do you want to save current level?", "Save", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                    saveToFile();
            }
        }

        private void btEditObjs_Click(object sender, EventArgs e)
        {
            //TODO: refactor this block to separate method
            if (dirty)
            {
                DialogResult dr = MessageBox.Show("Level was changed. Do you want to save current level?", "Save", MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Cancel)
                {
                    returnCbLevelIndex();
                    return;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!saveToFile())
                    {
                        returnCbLevelIndex();
                        return;
                    }
                }
                else
                {
                    dirty = false;
                }
            }
            var b = new BlockEdit();
            b.ShowDialog();
            reloadLevel();
        }


        private void btForm_Click(object sender, EventArgs e)
        {
            //TODO: refactor this block to separate method
            if (dirty)
            {
                DialogResult dr = MessageBox.Show("Level was changed. Do you want to save current level?", "Save", MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Cancel)
                {
                    returnCbLevelIndex();
                    return;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!saveToFile())
                    {
                        returnCbLevelIndex();
                        return;
                    }
                }
                else
                {
                    dirty = false;
                }
            }
            var f = new EditLayout();
            f.ShowDialog();
            reloadLevel();
        }

        private void cbScreenNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbScreenNo.SelectedIndex == -1)
                return;
            curActiveScreen = cbScreenNo.SelectedIndex;
            mapScreen.Invalidate();
        }

        private void editEnemy_Click(object sender, EventArgs e)
        {
            //TODO: refactor this block to separate method
            if (dirty)
            {
                DialogResult dr = MessageBox.Show("Level was changed. Do you want to save current level?", "Save", MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Cancel)
                {
                    returnCbLevelIndex();
                    return;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!saveToFile())
                    {
                        returnCbLevelIndex();
                        return;
                    }
                }
                else
                {
                    dirty = false;
                }
            }
            var f = new EnemyEditor();
            f.ShowDialog();
            reloadLevel();
        }
    }

    struct ObjectRec
    {
        public ObjectRec(byte type, byte sx, byte sy, byte x, byte y)
        {
            this.type = type;
            this.sx = sx;
            this.sy = sy;
            this.x = x;
            this.y = y;
        }
        public byte type;
        public byte x;
        public byte y;
        public byte sx;
        public byte sy;

        public override String ToString()
        {
            String formatStr = (type > 15) ? "{0:X} : ({1:X}:{2:X})" : "0{0:X} : ({1:X}:{2:X})";
            return String.Format(formatStr, type, sx << 8 | x, sy << 8 | y);
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
