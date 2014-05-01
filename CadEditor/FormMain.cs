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
                Globals.loadData(OpenFile.FileName, OpenFile.DumpName, OpenFile.ConfigName);
                fileLoaded = true;
                resetControls();
            }

            subeditorsDict = new Dictionary<ToolStripButton, Func<Form>> { 
                 { bttBigBlocks,    ()=>{ var f = new BigBlockEdit(); f.setFormMain(this); return f;} },
                 { bttBlocks,       ()=>{ var f = new BlockEdit();    f.setFormMain(this); return f;} },
                 { bttLayout,       ()=>{ return new EditLayout();}   },
                 { bttEnemies,      ()=>{ var f = new EnemyEditor();  f.setFormMain(this); return f;}  },
                 { bttVideo,        ()=>{ return new EditVideo();}    },
                 { bttMap,          ()=>{ return new EditMap();}    },
            };
        }

        private void resetControls()
        {
            cbScreenNo.Items.Clear();
            for (int i = 0; i < ConfigScript.screensOffset.recCount; i++)
                cbScreenNo.Items.Add(String.Format("{0:X}", i + 1));
            cbScreenNo.SelectedIndex = 0;
            screens = Utils.setScreens();
            if (ConfigScript.getLayersCount() > 1)
              screens2 = Utils.setScreens2();

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
            dirty = false; updateSaveVisibility();
            showNeiScreens = true;
            showAxis = true;
            showBrush = true;
            showLayer1 = true;
            showLayer2 = true;
            useStructs = false;
            curActiveLayer = 0;
            prepareBlocksPanel();

            reloadGameType();
            changeLevelIndex();

            bool showImportExport = Globals.gameType != GameType.DT;
            bttImport.Visible = showImportExport;
            bttExport.Visible = showImportExport;

            bttBigBlocks.Enabled = ConfigScript.isBigBlockEditorEnabled;
            bttBlocks.Enabled = ConfigScript.isBlockEditorEnabled;
            bttLayout.Enabled = ConfigScript.isLayoutEditorEnabled;
            bttEnemies.Enabled = ConfigScript.isEnemyEditorEnabled;
            bttVideo.Enabled = ConfigScript.isVideoEditorEnabled;
            bttMap.Enabled = ConfigScript.isMapEditorEnabled;

            bttShowLayer1.Enabled = ConfigScript.getLayersCount() > 1;
            bttShowLayer2.Enabled = ConfigScript.getLayersCount() > 1;
            bttLayer.Enabled = ConfigScript.getLayersCount() > 1;

            if (ConfigScript.getScreenVertical())
                mapScreen.Size = new Size((int)(ConfigScript.getScreenHeight() * blockWidth * curScale), (int)((ConfigScript.getScreenWidth() + 2) * blockHeight * curScale));
            else
                mapScreen.Size = new Size((int)((ConfigScript.getScreenWidth() + 2) * blockWidth * curScale), (int)(ConfigScript.getScreenHeight() * blockHeight * curScale));
        }

        private void reloadLevel(bool reloadScreens = true, bool reloadBlockPanel = false)
        {
            setBigBlocksIndexes();
            setBlocks(reloadBlockPanel);
            if (reloadScreens)
            {
                screens = Utils.setScreens();
                if (ConfigScript.getLayersCount() > 1)
                    screens2 = Utils.setScreens2();
            }
            updateMap();
        }

        private void setBigBlocksIndexes()
        {
          int bigTileIndex = (Globals.gameType != GameType.CAD) ? curActiveBlockNo : Globals.levelData[curActiveLevel].bigBlockId;
          bigBlockIndexes = ConfigScript.getBigBlocks(bigTileIndex);
        }

        private void setBlocks(bool needToRefillBlockPanel)
        {
            bigBlocks.Images.Clear();
            smallBlocks.Images.Clear();
            bigBlocks.ImageSize = new Size((int)(curButtonScale * blockWidth), (int)(curButtonScale * blockHeight));

            //if using pictures
            if (ConfigScript.usePicturesInstedBlocks)
            {
                Utils.setBlocks(bigBlocks, curButtonScale, blockWidth, blockHeight, curViewType, showAxis);
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

            float smallBlockScaleFactor = curButtonScale;
            int bigTileIndex = (Globals.gameType != GameType.CAD) ? curActiveBlockNo : Globals.levelData[curActiveLevel].bigBlockId;
            Image[] bigImages = Video.makeBigBlocks(backId, blockId, bigTileIndex, palId, smallObjectsType, smallBlockScaleFactor, blockWidth, blockHeight, curButtonScale, curViewType, showAxis);
            bigBlocks.Images.AddRange(bigImages);

            //tt add
            for (int i = ConfigScript.getBigBlocksCount(); i < 256; i++)
            {
                bigBlocks.Images.Add(Video.emptyScreen((int)(blockWidth*curButtonScale),(int)(blockHeight*curButtonScale)));
            }
            curActiveBlock = 0;

            if (needToRefillBlockPanel)
                prepareBlocksPanel();
            else
                reloadBlocksPanel();
        }

        private void prepareBlocksPanel()
        {
            Utils.prepareBlocksPanel(blocksPanel, new Size((int)(blockWidth * curButtonScale + 1), (int)(blockHeight * curButtonScale + 1)), bigBlocks, buttonBlockClick);
        }

        private void reloadBlocksPanel()
        {
            Utils.reloadBlocksPanel(blocksPanel, bigBlocks);
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
            lbActiveBlock.Text = String.Format("Label: ({0:X})", index);
        }

        private void renderNeighbornLine(Graphics g, int screenNo, int line, int X)
        {
            int WIDTH = ConfigScript.getScreenWidth();
            int HEIGHT = ConfigScript.getScreenHeight();
            int TILE_SIZE_X = (int)(blockWidth * curScale);
            int TILE_SIZE_Y = (int)(blockHeight * curScale);
            int SIZE = WIDTH * HEIGHT;
            int[] indexesPrev = screens[screenNo];
            for (int i = 0; i < SIZE; i++)
            {
                if (i % WIDTH == line)
                {
                    int index = indexesPrev[i];
                    int bigBlockNo = Globals.getBigTileNoFromScreen(indexesPrev, i);
                    if (bigBlockNo < bigBlocks.Images.Count)
                        g.DrawImage(bigBlocks.Images[bigBlockNo], new Rectangle(X, i / WIDTH * TILE_SIZE_Y, TILE_SIZE_X, TILE_SIZE_Y));
                }
            }
        }

        private void drawActiveTileStruct(Graphics g, Rectangle visibleRect)
        {
            int TILE_SIZE_X = (int)(blockWidth * curScale);
            int TILE_SIZE_Y = (int)(blockHeight * curScale);
            if (curTileStruct != null)
            {
                int WIDTH1 = curTileStruct.Width;
                int HEIGHT1 = curTileStruct.Height;
                for (int x = 0; x < WIDTH1; x++)
                {
                    for (int y = 0; y < HEIGHT1; y++)
                    {
                        int index = curTileStruct[x, y];
                        Rectangle tileRect;
                        if (ConfigScript.getScreenVertical())
                            tileRect = new Rectangle(curDy * TILE_SIZE_X + y * TILE_SIZE_X, (curDx + 1) * TILE_SIZE_Y + x * TILE_SIZE_Y, TILE_SIZE_X, TILE_SIZE_Y);
                        else
                            tileRect = new Rectangle((curDx + 1) * TILE_SIZE_X + x * TILE_SIZE_X, curDy * TILE_SIZE_Y + y * TILE_SIZE_Y, TILE_SIZE_X, TILE_SIZE_Y);

                        if ((visibleRect.Contains(tileRect)) || (visibleRect.IntersectsWith(tileRect)))
                        {
                            if ((index!=-1) && (index < bigBlocks.Images.Count))
                                g.DrawImage(bigBlocks.Images[index], tileRect);
                        }
                    }
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            int WIDTH = ConfigScript.getScreenWidth();
            int HEIGHT = ConfigScript.getScreenHeight();
            int TILE_SIZE_X = (int)(blockWidth * curScale);
            int TILE_SIZE_Y = (int)(blockHeight * curScale);
            int SIZE = WIDTH * HEIGHT;
            if (!fileLoaded)
                return;
            int[] indexes = screens[curActiveScreen];
            int[] indexes2 = null;
            if (ConfigScript.getLayersCount() > 1)
                indexes2 = screens2[curActiveScreen];
            var visibleRect = Utils.getVisibleRectangle(pnView, mapScreen);
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
                {
                    if (bigBlockNo < bigBlocks.Images.Count & showLayer1)
                      g.DrawImage(bigBlocks.Images[bigBlockNo], tileRect);
                    if (indexes2 != null && showLayer2)
                    {
                        int bigBlockNo2 = Globals.getBigTileNoFromScreen(indexes2, i);
                        if (bigBlockNo2 < bigBlocks.Images.Count)
                            g.DrawImage(bigBlocks.Images[bigBlockNo2], tileRect);
                    }
                }
            }
            if (!ConfigScript.getScreenVertical() && showNeiScreens && (curActiveScreen > 0) && showLayer1)
            {
                renderNeighbornLine(g, curActiveScreen - 1, (WIDTH - 1), 0);
            }
            if (!ConfigScript.getScreenVertical() && showNeiScreens && (curActiveScreen < ConfigScript.screensOffset.recCount - 1) && showLayer1)
            {
                renderNeighbornLine(g, curActiveScreen + 1, 0 , (WIDTH + 1) * TILE_SIZE_X);
            }

            if (ConfigScript.getScreenVertical())
              g.DrawRectangle(new Pen(Color.Green, 4.0f), new Rectangle(0, TILE_SIZE_Y, TILE_SIZE_X * HEIGHT, TILE_SIZE_Y * WIDTH));
            else
              g.DrawRectangle(new Pen(Color.Green, 4.0f), new Rectangle(TILE_SIZE_X, 0, TILE_SIZE_X * WIDTH, TILE_SIZE_Y * HEIGHT));

            //Additional rendering  //float to int!
            ConfigScript.renderToMainScreen(g, (int)curScale);

            if (showBrush && curActiveBlock != -1 && (curDx != OUTSIDE || curDy != OUTSIDE))
            {
                if (!useStructs)
                {
                    if (!ConfigScript.getScreenVertical())
                        g.DrawImage(bigBlocks.Images[curActiveBlock], (curDx + 1) * TILE_SIZE_X, curDy * TILE_SIZE_Y);
                    else
                        g.DrawImage(bigBlocks.Images[curActiveBlock], curDy * TILE_SIZE_X, (curDx + 1) * TILE_SIZE_Y);
                }
                else
                {
                    drawActiveTileStruct(g, visibleRect);
                }
            }
        }

        private Image screenToImage(int scrNo)
        {
            int EXPORT_SCALE = 2;
            int WIDTH = ConfigScript.getScreenWidth();
            int HEIGHT = ConfigScript.getScreenHeight();
            int TILE_SIZE_X = (int)(blockWidth * EXPORT_SCALE);
            int TILE_SIZE_Y = (int)(blockHeight * EXPORT_SCALE);
            int SIZE = WIDTH * HEIGHT;

            int[] indexes = screens[scrNo];
            int[] indexes2 = null;
            if (ConfigScript.getLayersCount() > 1)
                indexes2 = screens2[scrNo];

            Image result;
            if ( (ConfigScript.getScreenVertical()))
                result = new Bitmap(HEIGHT * TILE_SIZE_Y, WIDTH * TILE_SIZE_X);
            else
                result = new Bitmap(WIDTH * TILE_SIZE_X, HEIGHT * TILE_SIZE_Y);

            using (var g = Graphics.FromImage(result))
            {
                for (int i = 0; i < SIZE; i++)
                {
                    int index = indexes[i];
                    int bigBlockNo = Globals.getBigTileNoFromScreen(indexes, i);
                    Rectangle tileRect;
                    if (ConfigScript.getScreenVertical())
                        tileRect = new Rectangle(i / WIDTH * TILE_SIZE_X, (i % WIDTH) * TILE_SIZE_Y, TILE_SIZE_X, TILE_SIZE_Y);
                    else
                        tileRect = new Rectangle((i % WIDTH) * TILE_SIZE_X, i / WIDTH * TILE_SIZE_Y, TILE_SIZE_X, TILE_SIZE_Y);

                    if (bigBlockNo < bigBlocks.Images.Count & showLayer1)
                        g.DrawImage(bigBlocks.Images[bigBlockNo], tileRect);
                    if (indexes2 != null && showLayer2)
                    {
                        int bigBlockNo2 = Globals.getBigTileNoFromScreen(indexes2, i);
                        if (bigBlockNo2 < bigBlocks.Images.Count)
                            g.DrawImage(bigBlocks.Images[bigBlockNo2], tileRect);
                    }
                }
            }
            return result;
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

        private float curScale = 2;
        private float curButtonScale = 2;
        private int blockWidth = 32;
        private int blockHeight = 32;

        bool useStructs;
        TileStructure curTileStruct;

        MapViewType curViewType = MapViewType.ObjType;
        private bool dirty;
        private bool showNeiScreens;
        private bool showAxis;
        private bool showBrush;
        private bool showLayer1;
        private bool showLayer2;
        private int[][] screens;
        private int[][] screens2;

        private byte[] bigBlockIndexes;

        public static bool fileLoaded = false;

        const int OUTSIDE = -10;
        int curDx = OUTSIDE;
        int curDy = OUTSIDE;
        bool curClicked = false;
        int curActiveLayer = 0;

        private Dictionary<ToolStripButton, Func<Form>> subeditorsDict;

        private void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int WIDTH = ConfigScript.getScreenWidth();
            int HEIGHT = ConfigScript.getScreenHeight();
            int dx, dy;
            if (ConfigScript.getScreenVertical())
            {
                dy = e.X / (int)(blockWidth * curScale);
                dx = e.Y / (int)(blockHeight * curScale) - 1;
            }
            else
            {
                dx = e.X / (int)(blockWidth * curScale) - 1;
                dy = e.Y / (int)(blockHeight * curScale);
            }

            if (e.Button == MouseButtons.Right)
            {
                if (dx == WIDTH || dx == -1)
                    return;
                int index = dy * WIDTH + dx;
                curActiveBlock = Globals.getBigTileNoFromScreen(screens[curActiveScreen], index);
                activeBlock.Image = bigBlocks.Images[curActiveBlock];
                lbActiveBlock.Text = String.Format("Label: {0:X}", curActiveBlock);
                return;
            }
        }

        private void mapScreen_MouseMove(object sender, MouseEventArgs e)
        {
            int WIDTH = ConfigScript.getScreenWidth();
            int HEIGHT = ConfigScript.getScreenHeight();
            int dx, dy;
            if (ConfigScript.getScreenVertical())
            {
                dy = e.X / (int)(blockWidth * curScale);
                dx = e.Y / (int)(blockHeight * curScale) - 1;
            }
            else
            {
                dx = e.X / (int)(blockWidth * curScale) - 1;
                dy = e.Y / (int)(blockHeight * curScale);
            }
            lbCoords.Text = String.Format("Coords:({0},{1})", dx, dy);

            bool curDeltaChanged = curDx != dx || curDy != dy;
            if (curDeltaChanged)
            {
                curDx = dx;
                curDy = dy;
            }
            if (curClicked)
            {
                var activeScreens = curActiveLayer == 0 ? screens : screens2;
                if (dx == WIDTH)
                {
                    if (curActiveScreen < ConfigScript.screensOffset.recCount - 1)
                    {
                        int index = dy * WIDTH;
                        Globals.setBigTileToScreen(activeScreens[curActiveScreen + 1], index, curActiveBlock);
                        dirty = true; updateSaveVisibility();
                    }
                }
                else if (dx == -1)
                {
                    if (curActiveScreen > 0)
                    {
                        int index = dy * WIDTH + (WIDTH - 1);
                        Globals.setBigTileToScreen(activeScreens[curActiveScreen - 1], index, curActiveBlock);
                        dirty = true; updateSaveVisibility();
                    }
                }
                else
                {
                    if (!useStructs)
                    {
                        int index = dy * WIDTH + dx;
                        Globals.setBigTileToScreen(activeScreens[curActiveScreen], index, curActiveBlock);
                        dirty = true; updateSaveVisibility();
                    }
                    else
                    {
                        appendCurTileStruct(dx, dy);
                        dirty = true; updateSaveVisibility();
                    }
                }
            }
            mapScreen.Invalidate();
        }

        private void appendCurTileStruct(int dx, int dy)
        {
            int WIDTH = ConfigScript.getScreenWidth();
            int HEIGHT = ConfigScript.getScreenHeight();
            int TILE_SIZE_X = (int)(blockWidth * curScale);
            int TILE_SIZE_Y = (int)(blockHeight * curScale);
            var activeScreens = curActiveLayer == 0 ? screens : screens2;
            if (curTileStruct!=null)
            {
                int WIDTH1 = curTileStruct.Width;
                int HEIGHT1 = curTileStruct.Height;
                for (int x = 0; x < WIDTH1; x++)
                {
                    for (int y = 0; y < HEIGHT1; y++)
                    {
                        if ((dy + y) >= HEIGHT || (dx + x) >= WIDTH)
                            continue;
                        int index = (dy+y) * WIDTH + (dx+x);
                        int no = curTileStruct[x, y];
                        if (no == -1)
                            continue;
                        Globals.setBigTileToScreen(activeScreens[curActiveScreen], index, no);
                    }
                }
            }
        }

        private void mapScreen_MouseLeave(object sender, EventArgs e)
        {
            lbCoords.Text = "Coords:()";
            curDx = OUTSIDE;
            curDy = OUTSIDE;
            curClicked = false;
            mapScreen.Invalidate();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            saveToFile();
        }

        private void saveScreens(OffsetRec screensRec, int[][] screensData)
        {
            var arrayToSave = Globals.dumpdata != null ? Globals.dumpdata : Globals.romdata;
            int wordLen = ConfigScript.getWordLen();
            bool littleEndian = ConfigScript.isLittleEndian();
            //write back tiles
            int dataStride = ConfigScript.getScreenDataStride();
            for (int i = 0; i < screensRec.recCount; i++)
            {
                int addr = screensRec.beginAddr + i * screensRec.recSize * (dataStride * wordLen);
                if (wordLen == 1)
                {
                    for (int x = 0; x < screensRec.recSize; x++)
                        arrayToSave[addr + x * dataStride] = (byte)screensData[i][x];
                }
                else if (wordLen == 2)
                {
                    if (littleEndian)
                    {
                        for (int x = 0; x < screensRec.recSize; x++)
                            Utils.writeWordLE(arrayToSave, addr + x * (dataStride * wordLen), screensData[i][x]);
                    }
                    else
                    {
                        for (int x = 0; x < screensRec.recSize; x++)
                            Utils.writeWord(arrayToSave, addr + x * (dataStride * wordLen), screensData[i][x]);
                    }
                }
            }
        }

        private bool saveToFile()
        {
            saveScreens(ConfigScript.screensOffset, screens);
            if (ConfigScript.getLayersCount() > 1)
                saveScreens(ConfigScript.screensOffset2, screens2);
            dirty = !Globals.flushToFile(); updateSaveVisibility();
            return !dirty;
        }



        private void cbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
            {
                updateSaveVisibility();
                return;
            }
            updateSaveVisibility();
            bool senderIsScale = sender == bttScale;
            changeLevelIndex(senderIsScale);
            if (senderIsScale)
            {
                if (ConfigScript.getScreenVertical())
                    mapScreen.Size = new Size((int)(ConfigScript.getScreenHeight() * blockWidth * curScale), (int)((ConfigScript.getScreenWidth() + 2) * blockHeight * curScale));
                else
                    mapScreen.Size = new Size((int)((ConfigScript.getScreenWidth() + 2) * blockWidth * curScale), (int)(ConfigScript.getScreenHeight() * blockHeight * curScale));
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
            {
                updateSaveVisibility();
                e.Cancel = true;
            }
        }

        private void btSubeditor_Click(object sender, EventArgs e)
        {
            var button = (ToolStripButton)sender;
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
            showNeiScreens = bttShowNei.Checked;
            mapScreen.Invalidate();
        }

        private bool openFile()
        {
            Globals.gameType = GameType.Generic;
            if (!Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
            {
                updateSaveVisibility();
                return false;
            }
            updateSaveVisibility();
            var f = new OpenFile();
            if (f.ShowDialog() == DialogResult.OK)
            {
                Globals.loadData(OpenFile.FileName, OpenFile.DumpName, OpenFile.ConfigName);
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
            SaveScreensCount.Filename = "exportedScreens.bin";
            var f = new SaveScreensCount();
            f.Text = "Export";
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
            SaveScreensCount.Filename = "exportedScreens.bin";
            var f = new SaveScreensCount();
            f.Text = "Import";
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
            updateSaveVisibility();
            reloadLevel(false);
        }

        private void updateSaveVisibility()
        {
            bttSave.Enabled = dirty;
        }

        private void cbShowAxis_CheckedChanged(object sender, EventArgs e)
        {
            showAxis = bttAxis.Checked;
            reloadLevel(false);
            //mapScreen.Invalidate();
        }

        private void bttShowBrush_CheckedChanged(object sender, EventArgs e)
        {
            showBrush = bttShowBrush.Checked;
            reloadLevel(false);
        }

        private void btHex_Click(object sender, EventArgs e)
        {
            if (Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
            {
                updateSaveVisibility();
                var f = new EditHexEditor();
                f.setHighlightZone(ConfigScript.screensOffset.beginAddr + ConfigScript.screensOffset.recSize * curActiveScreen, ConfigScript.screensOffset.recSize);
                f.ShowDialog();
                reloadLevel();
            }
            
        }

        private FormClosedEventHandler subeditorClosed(ToolStripButton enabledAfterCloseButton)
        {
            return delegate(object sender, FormClosedEventArgs e) 
            { 
                enabledAfterCloseButton.Enabled = true;
                reloadLevel();
            };
        }

        private void subeditorOpen(Form f, ToolStripButton b)
        {
            if (Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
            {
                updateSaveVisibility();
                b.Enabled = false;
                f.Show();
                f.FormClosed += subeditorClosed(b);
            }
        }

        public int CurActiveLevelCad
        {
           get { return curActiveLevel; }
        }

        public int CurActiveDoorCad
        {
             get { return curActiveDoor; }
        }

        public int CurActiveVideoNo
        {
            get { return curActiveVideoNo; }
        }

        public int CurActiveBlockNo
        {
            get { return curActiveBlockNo; }
        }

        public int CurActiveBigBlockNo
        {
            get { return curActiveBigBlockNo; }
        }

        public int CurActivePalleteNo
        {
            get { return curActivePalleteNo; }
        }

        public MapViewType CurActiveViewType
        {
            get { return curViewType; } 
        }

        public ImageList getBigBlockImageList()
        {
            return bigBlocks;
        }

        private void bttScale_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            float[] scaleFactors = { 0.25f, 0.5f, 1.0f, 2.0f, 3.0f, 4.0f };
            curScale = curButtonScale = scaleFactors [bttScale.DropDownItems.IndexOf(e.ClickedItem)];
            cbLevel_SelectedIndexChanged(bttScale, new EventArgs());
        }

        private void mapScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                curClicked = true;
                mapScreen_MouseMove(sender, e);
            }
        }

        private void mapScreen_MouseUp(object sender, MouseEventArgs e)
        {
            curClicked = false;
        }

        private void bttShowLayer1_CheckedChanged(object sender, EventArgs e)
        {
            showLayer1 = bttShowLayer1.Checked;
            mapScreen.Invalidate();
        }

        private void bttShowLayer2_CheckedChanged(object sender, EventArgs e)
        {
            showLayer2 = bttShowLayer2.Checked;
            mapScreen.Invalidate();
        }

        private void bttLayer_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            curActiveLayer = bttLayer.DropDownItems.IndexOf(e.ClickedItem);
        }

        private void bttStructures_Click(object sender, EventArgs e)
        {
            var f = new FormStructures();
            f.setFormMain(this);
            f.Show();
            updateBlocksPanelVisible();
        }

        private void updateBlocksPanelVisible()
        {
            blocksPanel.Visible = !useStructs;
            lbStructures.Visible = useStructs;
            if (useStructs)
            {
                lbStructures.Items.Clear();
                var tss = FormStructures.getTileStructures();
                foreach (var ts in tss)
                    lbStructures.Items.Add(ts.Name);
            }
        }

        private void cbUseStructs_CheckedChanged(object sender, EventArgs e)
        {
            useStructs = cbUseStructs.Checked;
            updateBlocksPanelVisible();
        }

        private void lbStructures_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lbStructures.SelectedIndex;
            var tss = FormStructures.getTileStructures();
            if (index == -1 || index >= tss.Count)
                return;
            curTileStruct = tss[index];
        }

        private void bttExportPic_Click(object sender, EventArgs e)
        {
            SaveScreensCount.ExportMode = true;
            SaveScreensCount.Filename = "exportedScreens.png";
            var f = new SaveScreensCount();
            f.Text = "Export picture";
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

                int first = SaveScreensCount.First;
                var probeIm = screenToImage(curActiveScreen);
                int screenCount = SaveScreensCount.Count;
                var resultImage = new Bitmap(probeIm.Width * screenCount, probeIm.Height);
                using (var g = Graphics.FromImage(resultImage))
                {
                    for (int i = 0; i < screenCount; i++)
                    {
                        var im = screenToImage(first + i);
                        g.DrawImage(im, new Point(i * im.Width, 0));
                    }
                }
                resultImage.Save(SaveScreensCount.Filename);
            }
        }
    }
}
