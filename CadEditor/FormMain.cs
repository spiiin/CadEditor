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
            if (OpenFile.FileName == "" || OpenFile.ConfigName == "")
            {
                if (!openFile())
                {
                    Close();
                    return;
                }
            }
            else
            {
                Globals.loadData(OpenFile.FileName, OpenFile.ConfigName);
                fileLoaded = true;
                resetControls();
            }

            subeditorsDict = new Dictionary<Button, Func<Form>> { 
                 { btEdit,           ()=>{ return new BigBlockEdit();} },
                 { btEditObjs,       ()=>{ return new BlockEdit();}    },
                 { btEditLayout,     ()=>{ return new EditLayout();}   },
                 { btEditEnemy,      ()=>{ return new EnemyEditor();}  },
                 { btVideo,          ()=>{ return new EditVideo();}    },
            };
        }

        private void resetControls()
        {
            cbScreenNo.Items.Clear();
            for (int i = 0; i < ConfigScript.screensOffset.recCount; i++)
                cbScreenNo.Items.Add(String.Format("{0:X}", i + 1));
            cbScreenNo.SelectedIndex = 0;
            setScreens();

            blockWidth = ConfigScript.getBlocksPicturesWidth();
            blockHeight = 32;

            Utils.setCbItemsCount(cbVideoNo, ConfigScript.videoOffset.recCount);
            Utils.setCbItemsCount(cbBigBlockNo, ConfigScript.bigBlocksOffset.recCount);
            Utils.setCbItemsCount(cbBlockNo, ConfigScript.blocksOffset.recCount);
            Utils.setCbItemsCount(cbPaletteNo, ConfigScript.palOffset.recCount);
            Utils.setCbIndexWithoutUpdateLevel(cbVideoNo, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbBigBlockNo, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbBlockNo, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbPaletteNo, cbLevel_SelectedIndexChanged);

            Utils.setCbIndexWithoutUpdateLevel(cbLevel, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbDoor, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbViewType, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbScale, cbLevel_SelectedIndexChanged, 1);
            dirty = false;
            showNeiScreens = true;
            showAxis = true;
            prepareBlocksPanel();

            reloadGameType();
            changeLevelIndex();

            bool showImportExport = Globals.gameType != GameType.DT;
            btImport.Visible = showImportExport;
            btExport.Visible = showImportExport;

            btEdit.Enabled = ConfigScript.isBigBlockEditorEnabled;
            btEditObjs.Enabled = ConfigScript.isBlockEditorEnabled;
            btEditLayout.Enabled = ConfigScript.isLayoutEditorEnabled;
            btEditEnemy.Enabled = ConfigScript.isEnemyEditorEnabled;
            btVideo.Enabled = ConfigScript.isVideoEditorEnabled;

            if (ConfigScript.getScreenVertical())
                mapScreen.Size = new Size(ConfigScript.getScreenHeight() * blockWidth * curScale, (ConfigScript.getScreenWidth() + 2) * blockHeight * curScale);
            else
                mapScreen.Size = new Size((ConfigScript.getScreenWidth() + 2) * blockWidth * curScale, ConfigScript.getScreenHeight() * blockHeight * curScale);
        }

        private void reloadLevel(bool reloadScreens = true, bool reloadBlockPanel = false)
        {
            setBigBlocksIndexes();
            setBlocks(reloadBlockPanel);
            if (reloadScreens)
              setScreens();
            updateMap();
        }

        private void setBigBlocksIndexes()
        {
          int bigTileIndex = (Globals.gameType != GameType.CAD) ? curActiveBlockNo : Globals.levelData[curActiveLevel].bigBlockId;
          bigBlockIndexes = ConfigScript.getBigBlocks(bigTileIndex);
        }


        private Image ResizeBitmap(Image sourceBMP, int width, int height)
        {
            Image result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
        }


        private void setBlocks(bool needToRefillBlockPanel)
        {
            bigBlocks.Images.Clear();
            smallBlocks.Images.Clear();
            bigBlocks.ImageSize = new Size(curButtonScale * blockWidth, curButtonScale * blockHeight);

            //if using pictures
            if (ConfigScript.usePicturesInstedBlocks)
            {
                var imSrc = Image.FromFile(ConfigScript.blocksPicturesFilename);
                var imResized = ResizeBitmap(imSrc, curButtonScale * blockWidth * ConfigScript.getBigBlocksCount(), curButtonScale * blockHeight);
                bigBlocks.Images.AddStrip(imResized);
                for (int i = bigBlocks.Images.Count; i < 256; i++)
                    bigBlocks.Images.Add(Video.emptyScreen(blockWidth*curButtonScale, blockHeight*curButtonScale));
                if (showAxis)
                {
                    for (int i = 0; i < 256; i++)
                    {
                        var im1 = bigBlocks.Images[i];
                        using (var g = Graphics.FromImage(im1))
                            g.DrawRectangle(new Pen(Color.FromArgb(255, 255, 255, 255)), new Rectangle(0, 0, blockWidth*curButtonScale, blockHeight*curButtonScale));
                        bigBlocks.Images[i] = im1;
                    }
                }

                if (curViewType == MapViewType.ObjNumbers)
                {
                    int _bbRectPosX = (blockWidth / 2) * curButtonScale;
                    int _bbRectSizeX = (blockWidth / 2) * curButtonScale;
                    int _bbRectPosY = (blockHeight / 2) * curButtonScale;
                    int _bbRectSizeY = (blockHeight / 2) * curButtonScale;
                    for (int i = 0; i < 256; i++)
                    {
                        var im1 = bigBlocks.Images[i];
                        using (var g = Graphics.FromImage(im1))
                        {
                            g.FillRectangle(new SolidBrush(Color.FromArgb(192, 255, 255, 255)), new Rectangle(0, 0, _bbRectSizeX * 2, _bbRectSizeY * 2));
                            g.DrawString(String.Format("{0:X}", i), new Font("Arial", 16), Brushes.Red, new Point(0, 0));
                        }
                        bigBlocks.Images[i] = im1;
                    }
                    
                }

                if (needToRefillBlockPanel)
                    prepareBlocksPanel();
                else
                    reloadBlocksPanel();
                return;
            }

            //read blocks from file
            int backId, blockId, palId;

            if (GameType.CAD != Globals.gameType)
            {
                backId = curActiveVideoNo; ;
                blockId = curActiveBigBlockNo;
                palId = curActivePalleteNo;
            }
            else
            {
                var lr = ConfigScript.getLevelRec(curActiveLevel);
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

            MapViewType smallObjectsType = curViewType == MapViewType.ObjType ? MapViewType.ObjType : MapViewType.Tiles;

            int smallBlockScaleFactor = curButtonScale;
            var im = Video.makeObjectsStrip((byte)backId, (byte)blockId, (byte)palId, smallBlockScaleFactor, smallObjectsType);
            smallBlocks.ImageSize = new System.Drawing.Size(16*smallBlockScaleFactor, 16*smallBlockScaleFactor);
            smallBlocks.Images.AddStrip(im);

            //tt version hardcode
            ImageList[] smallBlocksAll = null;
            byte[] smallBlocksColorBytes = null;
            if (GameType.TT == Globals.gameType)
            {
                smallBlocksAll = new[] { smallBlocks1, smallBlocks2, smallBlocks3, smallBlocks4 };
                for (int i = 0; i < 4; i++)
                {
                    smallBlocksAll[i].Images.Clear();
                    smallBlocksAll[i].ImageSize = new System.Drawing.Size(16 * smallBlockScaleFactor, 16 * smallBlockScaleFactor);
                    smallBlocksAll[i].Images.AddStrip(Video.makeObjectsStrip((byte)backId, (byte)blockId, (byte)palId, smallBlockScaleFactor, smallObjectsType, i));
                }
                smallBlocksColorBytes = Globals.getTTSmallBlocksColorBytes(blockId);
            }

            int bbRectPosX = (blockWidth/2)*curButtonScale;
            int bbRectSizeX = (blockWidth / 2) * curButtonScale;
            int bbRectPosY = (blockHeight /2) * curButtonScale;
            int bbRectSizeY = (blockHeight/2) * curButtonScale; 
            for (int i = 0; i < ConfigScript.getBigBlocksCount(); i++)
            {
                var b = new Bitmap(blockWidth*curButtonScale, blockHeight*curButtonScale);
                using (Graphics g = Graphics.FromImage(b))
                {
                    if (Globals.gameType == GameType.TT)
                    {
                        int scb = smallBlocksColorBytes[i];
                        g.DrawImage(smallBlocksAll[scb >> 0 & 0x3].Images[bigBlockIndexes[i * 4]], new Rectangle(0, 0, bbRectSizeX, bbRectSizeY));
                        g.DrawImage(smallBlocksAll[scb >> 2 & 0x3].Images[bigBlockIndexes[i * 4 + 1]], new Rectangle(bbRectPosX, 0, bbRectSizeX, bbRectSizeY));
                        g.DrawImage(smallBlocksAll[scb >> 4 & 0x3].Images[bigBlockIndexes[i * 4 + 2]], new Rectangle(0, bbRectPosY, bbRectSizeX, bbRectSizeY));
                        g.DrawImage(smallBlocksAll[scb >> 6 & 0x3].Images[bigBlockIndexes[i * 4 + 3]], new Rectangle(bbRectPosX, bbRectPosY, bbRectSizeX, bbRectSizeY));   

                    }
                    else if (Globals.gameType == GameType._3E)
                    {
                        g.DrawImage(smallBlocks.Images[bigBlockIndexes[i * 4 + 0]], new Rectangle(0, 0, bbRectSizeX, bbRectSizeY));
                        g.DrawImage(smallBlocks.Images[bigBlockIndexes[i * 4 + 2]], new Rectangle(bbRectPosX, 0, bbRectSizeX, bbRectSizeY));
                        g.DrawImage(smallBlocks.Images[bigBlockIndexes[i * 4 + 1]], new Rectangle(0, bbRectPosY, bbRectSizeX, bbRectSizeY));
                        g.DrawImage(smallBlocks.Images[bigBlockIndexes[i * 4 + 3]], new Rectangle(bbRectPosX, bbRectPosY, bbRectSizeX, bbRectSizeY));
                    }
                    else
                    {
                        g.DrawImage(smallBlocks.Images[bigBlockIndexes[i * 4]], new Rectangle(0, 0, bbRectSizeX, bbRectSizeY));
                        g.DrawImage(smallBlocks.Images[bigBlockIndexes[i * 4 + 1]], new Rectangle(bbRectPosX, 0, bbRectSizeX, bbRectSizeY));
                        g.DrawImage(smallBlocks.Images[bigBlockIndexes[i * 4 + 2]], new Rectangle(0, bbRectPosY, bbRectSizeX, bbRectSizeY));
                        g.DrawImage(smallBlocks.Images[bigBlockIndexes[i * 4 + 3]], new Rectangle(bbRectPosX, bbRectPosY, bbRectSizeX, bbRectSizeY)); 
                    }

                    if (curViewType == MapViewType.ObjNumbers)
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(192, 255, 255, 255)), new Rectangle(0, 0, bbRectSizeX * 2, bbRectSizeY*2));
                        g.DrawString(String.Format("{0:X}", i), new Font("Arial", 16), Brushes.Red, new Point(0, 0));
                    }

                    if (showAxis)
                        g.DrawRectangle(new Pen(Color.FromArgb(255, 255, 255, 255)), new Rectangle(0, 0, bbRectSizeX*2, bbRectSizeY*2));
                }
                bigBlocks.Images.Add(b);
            }
            //tt add
            for (int i = ConfigScript.getBigBlocksCount(); i < 256; i++)
            {
                bigBlocks.Images.Add(Video.emptyScreen(blockWidth*curButtonScale,blockHeight*curButtonScale));
            }
            curActiveBlock = 0;

            if (needToRefillBlockPanel)
                prepareBlocksPanel();
            else
                reloadBlocksPanel();
        }

        private void prepareBlocksPanel()
        {
            blocksPanel.Controls.Clear();
            blocksPanel.SuspendLayout();
            for (int i = 0; i < ConfigScript.getBigBlocksCount(); i++)
            {
                var but = new Button();
                but.Size = new Size(blockWidth*curButtonScale, blockHeight*curButtonScale);
                but.ImageList = bigBlocks;
                but.ImageIndex = i;
                but.Click += new EventHandler(buttonBlockClick);
                blocksPanel.Controls.Add(but);
            }
            blocksPanel.ResumeLayout();
        }

        private void reloadBlocksPanel()
        {
            for (int i = 0; i < ConfigScript.getBigBlocksCount(); i++)
            {
                var but = (Button)blocksPanel.Controls[i];
                but.ImageList = bigBlocks;
                but.ImageIndex = i;
            }
        }

        private void setScreens()
        {
            screens = new byte[ConfigScript.screensOffset.recCount][];
            for (int i = 0; i < ConfigScript.screensOffset.recCount; i++)
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

        private Rectangle getVisibleRectangle(Control c)
        {
            Rectangle rect = c.RectangleToScreen(c.ClientRectangle);
            while (c != null)
            {
                rect = Rectangle.Intersect(rect, c.RectangleToScreen(c.ClientRectangle));
                c = c.Parent;
            }
            rect = mapScreen.RectangleToClient(rect);
            return rect;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {

            int WIDTH = ConfigScript.getScreenWidth();
            int HEIGHT = ConfigScript.getScreenHeight();
            int TILE_SIZE_X = blockWidth * curScale;
            int TILE_SIZE_Y = blockHeight * curScale;
            int SIZE = WIDTH * HEIGHT;
            if (!fileLoaded)
                return;
            byte[] indexes = screens[curActiveScreen];
            var visibleRect = getVisibleRectangle(pnView);
            var g = e.Graphics;
            for (int i = 0; i < SIZE; i++)
            {
                int index = indexes[i];
                int bigBlockNo = Globals.getBigTileNoFromScreen(indexes, i);
                Rectangle tileRect;
                if (ConfigScript.getScreenVertical())
                  tileRect = new Rectangle(i / WIDTH * TILE_SIZE_X, (i % WIDTH + 1) * TILE_SIZE_Y, TILE_SIZE_X, TILE_SIZE_Y);
                else
                  tileRect  = new Rectangle((i % WIDTH + 1) * TILE_SIZE_X, i / WIDTH * TILE_SIZE_Y, TILE_SIZE_X, TILE_SIZE_Y);

                if ((visibleRect.Contains(tileRect)) || (visibleRect.IntersectsWith(tileRect)))
                  g.DrawImage(bigBlocks.Images[bigBlockNo], tileRect);
            }
            if (showNeiScreens && (curActiveScreen > 0))
            {
                byte[] indexesPrev = screens[curActiveScreen - 1];
                for (int i = 0; i < SIZE; i++)
                {
                    if (i % WIDTH == WIDTH-1)
                    {
                        int index = indexesPrev[i];
                        int bigBlockNo = Globals.getBigTileNoFromScreen(indexesPrev, i);
                        g.DrawImage(bigBlocks.Images[bigBlockNo], new Rectangle(0, i / WIDTH * TILE_SIZE_Y, TILE_SIZE_X, TILE_SIZE_Y));
                    }
                }
            }
            if (showNeiScreens && (curActiveScreen < ConfigScript.screensOffset.recCount - 1))
            {
                byte[] indexesNext = screens[curActiveScreen + 1];
                for (int i = 0; i < SIZE; i++)
                {
                    if (i % WIDTH == 0)
                    {
                        int index = indexesNext[i];
                        int bigBlockNo = Globals.getBigTileNoFromScreen(indexesNext, i);
                        g.DrawImage(bigBlocks.Images[bigBlockNo], new Rectangle((WIDTH + 1) * TILE_SIZE_X, i / WIDTH * TILE_SIZE_Y, TILE_SIZE_X, TILE_SIZE_Y));
                    }
                }
            }
            if (ConfigScript.getScreenVertical())
              g.DrawRectangle(new Pen(Color.Green, 4.0f), new Rectangle(0, TILE_SIZE_Y, TILE_SIZE_X * HEIGHT, TILE_SIZE_Y * WIDTH));
            else
              g.DrawRectangle(new Pen(Color.Green, 4.0f), new Rectangle(TILE_SIZE_X, 0, TILE_SIZE_X * WIDTH, TILE_SIZE_Y * HEIGHT));
        }

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

        private int curScale = 2;
        private int curButtonScale = 2;
        private int blockWidth = 32;
        private int blockHeight = 32;

        MapViewType curViewType = MapViewType.ObjType;
        private bool dirty;
        private bool showNeiScreens;
        private bool showAxis;
        private byte[][] screens = null;

        private byte[] bigBlockIndexes;

        public static bool fileLoaded = false;

        private Dictionary<Button, Func<Form>> subeditorsDict;

        private void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int WIDTH  = ConfigScript.getScreenWidth();
            int HEIGHT = ConfigScript.getScreenHeight();
            int dx, dy;
            if (ConfigScript.getScreenVertical())
            {
                dy = e.X / (blockWidth * curScale);
                dx = e.Y / (blockHeight * curScale) - 1;
            }
            else
            {
                dx = e.X / (blockWidth * curScale) - 1;
                dy = e.Y / (blockHeight * curScale);
            }

            if (dx == WIDTH)
            {
                if (curActiveScreen < ConfigScript.screensOffset.recCount - 1)
                {
                    int index = dy * WIDTH;
                    Globals.setBigTileToScreen(screens[curActiveScreen + 1], index, curActiveBlock);
                    dirty = true;
                }
            }
            else if (dx == -1)
            {
                if (curActiveScreen > 0)
                {
                    int index = dy * WIDTH + (WIDTH-1);
                    Globals.setBigTileToScreen(screens[curActiveScreen - 1], index, curActiveBlock);
                    dirty = true;
                }
            }
            else
            {
                int index = dy * WIDTH + dx;
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
            for (int i = 0; i < ConfigScript.screensOffset.recCount; i++)
            {
                int addr = ConfigScript.screensOffset.beginAddr + i * ConfigScript.screensOffset.recSize;
                for (int x = 0; x < ConfigScript.screensOffset.recSize; x++)
                    Globals.romdata[addr + x] = screens[i][x];
            }
            dirty = !Globals.flushToFile();
            return !dirty;
        }



        private void cbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
                return;
            bool senderIsScale = sender == cbScale;
            changeLevelIndex(senderIsScale);
            if (senderIsScale)
            {
                if (ConfigScript.getScreenVertical())
                    mapScreen.Size = new Size(ConfigScript.getScreenHeight() * blockWidth * curScale, (ConfigScript.getScreenWidth() + 2) * blockHeight * curScale);
                else
                    mapScreen.Size = new Size((ConfigScript.getScreenWidth() + 2) * blockWidth * curScale, ConfigScript.getScreenHeight() * blockHeight * curScale);
            }
        }

        private void changeLevelIndex(bool reloadObjectsPanel = false)
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
            curScale = curButtonScale = cbScale.SelectedIndex + 1;
            reloadLevel(true, reloadObjectsPanel);
        }

        private void returnCbLevelIndex()
        {
            cbLevel.SelectedIndexChanged -= cbLevel_SelectedIndexChanged;
            cbLevel.SelectedIndex = curActiveLevel;
            cbLevel.SelectedIndexChanged += cbLevel_SelectedIndexChanged;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
                e.Cancel = true;
        }

        private void btSubeditor_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            subeditorOpen(subeditorsDict[button](), button);
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
                resetControls();
            }
            if (!fileLoaded)
                return false;
            return true;
            
        }

        public void reloadGameType()
        {
            bool generic = Globals.gameType != GameType.CAD;
            pnGeneric.Visible = generic;
            pnCad.Visible = !generic;
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            openFile();
            reloadGameType();
            changeLevelIndex(true);
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            SaveScreensCount.ExportMode = true;
            var f = new SaveScreensCount();
            f.ShowDialog();
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

                int screenSize = ConfigScript.screensOffset.recSize;
                int screenCount = SaveScreensCount.Count;
                int first = SaveScreensCount.First;
                var data = new byte[screenSize * screenCount];
                for (int i = 0; i < screenCount; i++)
                {
                    Array.Copy(screens[i + first], 0, data, screenSize*i, screenSize);
                }
                Utils.saveDataToFile(SaveScreensCount.Filename, data);
            }
        }

        private void btImport_Click(object sender, EventArgs e)
        {
            SaveScreensCount.ExportMode = false;
            var f = new SaveScreensCount();
            f.ShowDialog();
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

                int screenSize = ConfigScript.screensOffset.recSize;
                int first = SaveScreensCount.First;
                var data = Utils.loadDataFromFile(SaveScreensCount.Filename);
                int screenCount = data.Length / screenSize;
                for (int i = 0; i < screenCount; i++)
                {
                    Array.Copy(data, i * screenSize, screens[first + i], 0, screenSize);
                }
                Utils.saveDataToFile(SaveScreensCount.Filename, data);
            }
            dirty = true;
            reloadLevel(false);
        }

        private void cbShowAxis_CheckedChanged(object sender, EventArgs e)
        {
            showAxis = cbShowAxis.Checked;
            reloadLevel(false);
            //mapScreen.Invalidate();
        }

        private void btHex_Click(object sender, EventArgs e)
        {
            if (Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
            {
                var f = new EditHexEditor();
                f.setHighlightZone(ConfigScript.screensOffset.beginAddr + ConfigScript.screensOffset.recSize * curActiveScreen, ConfigScript.screensOffset.recSize);
                f.ShowDialog();
                reloadLevel();
            }
            
        }

        private FormClosedEventHandler subeditorClosed(Button enabledAfterCloseButton)
        {
            return delegate(object sender, FormClosedEventArgs e) 
            { 
                enabledAfterCloseButton.Enabled = true;
                reloadLevel();
            };

        }

        private void subeditorOpen(Form f, Button b)
        {
            if (Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
            {
                b.Enabled = false;
                f.Show();
                f.FormClosed += subeditorClosed(b);
            }
        }
    }
}
