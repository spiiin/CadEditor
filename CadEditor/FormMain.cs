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
                 { bttBigBlocks,    makeBigBlocksEditor },
                 { bttBlocks,       makeBlocksEditor },
                 { bttEnemies,      ()=>{ var f = new EnemyEditor();  f.setFormMain(this); return f;}  },
                 { bttConfig,       ()=>{ var f = new FormConfig();  f.setFormMain(this); f.onApply += reloadCallback; return f;}    },
            };

            ConfigScript.plugins.ForEach((p) => p.addToolButton(this));
            ConfigScript.plugins.ForEach((p) => p.addSubeditorButton(this));
        }

        private Form makeBigBlocksEditor()
        {
            BigBlockEdit f = new BigBlockEdit();
            f.setFormMain(this);
            return f;
        }

        private Form makeBlocksEditor()
        {
            if (!ConfigScript.isUseSegaGraphics())
            {
                var f = new BlockEdit();
                f.setFormMain(this);
                return f;
            }
            else
            {
                var f = new SegaBlockEdit();
                return f;
            }
        }

        private void resetScreens()
        {
            int oldScreenNo = cbScreenNo.SelectedIndex;
            cbScreenNo.Items.Clear();
            for (int i = 0; i < ConfigScript.screensOffset[curActiveLevelForScreen].recCount; i++)
                cbScreenNo.Items.Add(String.Format("{0:X}", i + 1));

            if (oldScreenNo == -1)
                cbScreenNo.SelectedIndex = 0;
            else if (oldScreenNo < cbScreenNo.Items.Count)
                cbScreenNo.SelectedIndex = oldScreenNo;

            screens = Utils.setScreens(curActiveLevelForScreen);
            if (ConfigScript.getLayersCount() > 1)
                screens2 = Utils.setScreens2();
        }

        private void resetControls()
        {
            curActiveLevelForScreen = 0;
            Utils.setCbItemsCount(cbPanelNo, (ConfigScript.getBigBlocksCount()+1023) / 1024);
            cbPanelNo.SelectedIndex = 0;
            resetScreens();

            blockWidth = ConfigScript.getBlocksPicturesWidth();
            blockHeight = 32;

            Utils.setCbItemsCount(cbVideoNo, ConfigScript.videoOffset.recCount);
            Utils.setCbItemsCount(cbBigBlockNo, ConfigScript.bigBlocksOffset.recCount);
            Utils.setCbItemsCount(cbBlockNo, ConfigScript.blocksOffset.recCount);
            Utils.setCbItemsCount(cbPaletteNo, ConfigScript.palOffset.recCount);
            Utils.setCbItemsCount(cbLevelNo, ConfigScript.getLevelsCount());
            Utils.setCbIndexWithoutUpdateLevel(cbVideoNo, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbBigBlockNo, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbBlockNo, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbPaletteNo, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbLevelNo, cbLevelNo_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbViewType, cbLevel_SelectedIndexChanged);

            cbGroup.Items.Clear();
            foreach (var g in ConfigScript.getGroups())
            {
                cbGroup.Items.Add(g.name);
            }
            /*if (cbGroup.Items.Count > 0)
              Utils.setCbIndexWithoutUpdateLevel(cbGroup, cbGroup_SelectedIndexChanged);*/
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

            bttBigBlocks.Enabled = ConfigScript.isBigBlockEditorEnabled;
            bttBlocks.Enabled = ConfigScript.isBlockEditorEnabled;
            bttEnemies.Enabled = ConfigScript.isEnemyEditorEnabled;

            bttShowLayer1.Visible = ConfigScript.getLayersCount() > 1;
            bttShowLayer2.Visible = ConfigScript.getLayersCount() > 1;
            bttLayer.Visible = ConfigScript.getLayersCount() > 1;

            resetMapScreenSize();
        }

        void resetMapScreenSize()
        {
            if (ConfigScript.getScreenVertical())
                mapScreen.Size = new Size((int)(ConfigScript.getScreenHeight(curActiveLevelForScreen) * blockWidth * curScale), (int)((ConfigScript.getScreenWidth(curActiveLevelForScreen) + 2) * blockHeight * curScale));
            else
                mapScreen.Size = new Size((int)((ConfigScript.getScreenWidth(curActiveLevelForScreen) + 2) * blockWidth * curScale), (int)(ConfigScript.getScreenHeight(curActiveLevelForScreen) * blockHeight * curScale));
        }

        public void reloadLevel(bool reloadScreens = true, bool reloadBlockPanel = false)
        {
            setBigBlocksIndexes();
            setBlocks(reloadBlockPanel);
            if (reloadScreens)
                resetScreens();
            updateMap();
        }

        private void setBigBlocksIndexes()
        {
          int bigTileIndex = curActiveBlockNo;
        }

        private Image[] makeSegaBigBlocks()
        {
            byte[] mapping = ConfigScript.getSegaMapping(curActiveBigBlockNo);
            byte[] videoTiles = ConfigScript.getVideoChunk(curActiveVideoNo);
            byte[] pal = ConfigScript.getPal(curActivePalleteNo);
            int count = ConfigScript.getBigBlocksCount();
            return ConfigScript.videoSega.makeBigBlocks(mapping, videoTiles, pal, count, curScale, curViewType, showAxis);
        }

        private void setBlocks(bool needToRefillBlockPanel)
        {
            bigBlocks.Images.Clear();
            //smallBlocks.Images.Clear();
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
            backId = curActiveVideoNo; ;
            blockId = curActiveBigBlockNo;
            palId = curActivePalleteNo;

            MapViewType smallObjectsType = 
                curViewType == MapViewType.SmallObjNumbers ? MapViewType.ObjNumbers :
                  curViewType == MapViewType.ObjType ? MapViewType.ObjType : MapViewType.Tiles;

            float smallBlockScaleFactor = curButtonScale;
            int bigTileIndex = curActiveBlockNo;
            Image[] bigImages;
            if (ConfigScript.isUseSegaGraphics())
                bigImages = makeSegaBigBlocks();
            else
                bigImages = ConfigScript.videoNes.makeBigBlocks(backId, curActiveLevelForScreen, blockId, bigTileIndex, palId, smallObjectsType, smallBlockScaleFactor, blockWidth, blockHeight, curButtonScale, curViewType, showAxis);
            bigBlocks.Images.AddRange(bigImages);

            //tt add
            for (int i = ConfigScript.getBigBlocksCount(); i < 256; i++)
            {
                bigBlocks.Images.Add(VideoHelper.emptyScreen((int)(blockWidth*curButtonScale),(int)(blockHeight*curButtonScale)));
            }
            curActiveBlock = 0;

            if (needToRefillBlockPanel)
                prepareBlocksPanel();
            else
                reloadBlocksPanel();
        }

        private void prepareBlocksPanel()
        {
            int subparts = (ConfigScript.getBigBlocksCount()+1023) / 1024;
            FlowLayoutPanel[] blocksPanels = { blocksPanel, blockPanel2, blockPanel3, blockPanel4 };
            if (ConfigScript.getBigBlocksCount() < 1024)
            {
                Utils.prepareBlocksPanel(blocksPanels[0], new Size((int)(blockWidth * curButtonScale + 1), (int)(blockHeight * curButtonScale + 1)), bigBlocks, buttonBlockClick, 0, ConfigScript.getBigBlocksCount());
            }
            else
            {
                for (int i = 0; i < subparts; i++)
                {
                    int count = (i * 1024 > ConfigScript.getBigBlocksCount()) ? (i * 1024) % ConfigScript.getBigBlocksCount() : 1024;
                    Utils.prepareBlocksPanel(blocksPanels[i], new Size((int)(blockWidth * curButtonScale + 1), (int)(blockHeight * curButtonScale + 1)), bigBlocks, buttonBlockClick, i * 1024, count);
                }
            }
        }

        private void reloadBlocksPanel()
        {
             int subparts = (ConfigScript.getBigBlocksCount() + 1023) / 1024;
             FlowLayoutPanel[] blocksPanels = { blocksPanel, blockPanel2, blockPanel3, blockPanel4 };
             if (ConfigScript.getBigBlocksCount() < 1024)
             {
                 Utils.reloadBlocksPanel(blocksPanels[0], bigBlocks, 0, ConfigScript.getBigBlocksCount());
             }
             else
             {
                 for (int i = 0; i < subparts; i++)
                 {
                     int count = (i * 1024 > ConfigScript.getBigBlocksCount()) ? (i * 1024) % ConfigScript.getBigBlocksCount() : 1024;
                     Utils.reloadBlocksPanel(blocksPanels[i], bigBlocks, i * 1024, count);
                 }
             }
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
            int WIDTH = ConfigScript.getScreenWidth(curActiveLevelForScreen);
            int HEIGHT = ConfigScript.getScreenHeight(curActiveLevelForScreen);
            int TILE_SIZE_X = (int)(blockWidth * curScale);
            int TILE_SIZE_Y = (int)(blockHeight * curScale);
            int SIZE = WIDTH * HEIGHT;
            int[] indexesPrev = screens[screenNo];
            for (int i = 0; i < SIZE; i++)
            {
                if (i % WIDTH == line)
                {
                    int index = indexesPrev[i];
                    int bigBlockNo = ConfigScript.getBigTileNoFromScreen(indexesPrev, i);
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
            if (!fileLoaded)
                return;
            int[] indexes = screens[curActiveScreen];
            int[] indexes2 = null;
            var g = e.Graphics;
            if (ConfigScript.getLayersCount() > 1)
                indexes2 = screens2[curActiveScreen];

            int WIDTH = ConfigScript.getScreenWidth(curActiveLevelForScreen);
            int HEIGHT = ConfigScript.getScreenHeight(curActiveLevelForScreen);
            int TILE_SIZE_X = (int)(blockWidth * curScale);
            int TILE_SIZE_Y = (int)(blockHeight * curScale);
            int SIZE = WIDTH * HEIGHT;
            var visibleRect = Utils.getVisibleRectangle(pnView, mapScreen);
            MapEditor.Render(e.Graphics, bigBlocks, blockWidth, blockHeight, visibleRect, indexes, indexes2, curScale, showLayer1, showLayer2, true, ConfigScript.getScreenVertical() ? TILE_SIZE_Y : TILE_SIZE_X, WIDTH, HEIGHT, ConfigScript.getScreenVertical());

            if (!ConfigScript.getScreenVertical() && showNeiScreens && (curActiveScreen > 0) && showLayer1)
            {
                renderNeighbornLine(g, curActiveScreen - 1, (WIDTH - 1), 0);
            }
            if (!ConfigScript.getScreenVertical() && showNeiScreens && (curActiveScreen < ConfigScript.screensOffset[curActiveLevelForScreen].recCount - 1) && showLayer1)
            {
                renderNeighbornLine(g, curActiveScreen + 1, 0 , (WIDTH + 1) * TILE_SIZE_X);
            }

            //show brush
            bool altPressed = Control.ModifierKeys == Keys.Alt;
            if (showBrush && curActiveBlock != -1 && (curDx != OUTSIDE || curDy != OUTSIDE) && !altPressed)
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
            if (altPressed && selectionRect)
            {
                int x = Math.Min(selectionMouseX, selectionBeginMouseX);
                int y = Math.Min(selectionMouseY, selectionBeginMouseY);
                int w = Math.Abs(selectionMouseX - selectionBeginMouseX);
                int h = Math.Abs(selectionMouseY - selectionBeginMouseY);
                g.DrawRectangle(new Pen(Brushes.Black, 2.0f), new Rectangle(x, y, w, h));
            }
        }

        //editor globals
        private int curActiveBlock = 0;
        private int curActiveScreen = 0;
        private int curActiveLevelForScreen = 0;

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

        public static bool fileLoaded = false;

        const int OUTSIDE = -10;
        int curDx = OUTSIDE;
        int curDy = OUTSIDE;
        bool curClicked = false;
        int curActiveLayer = 0;

        //select rect if alt pressed
        private int selectionBeginX, selectionBeginY, selectionEndX, selectionEndY;
        private int selectionBeginMouseX, selectionBeginMouseY, selectionMouseX, selectionMouseY;
        private bool selectionRect = false;

        private Dictionary<ToolStripButton, Func<Form>> subeditorsDict;

        private void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int WIDTH = ConfigScript.getScreenWidth(curActiveLevelForScreen);
            int HEIGHT = ConfigScript.getScreenHeight(curActiveLevelForScreen);
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
                curActiveBlock = ConfigScript.getBigTileNoFromScreen(screens[curActiveScreen], index);
                activeBlock.Image = bigBlocks.Images[curActiveBlock];
                lbActiveBlock.Text = String.Format("Label: {0:X}", curActiveBlock);
                return;
            }
        }

        private void mapScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectionRect)
            {
                selectionMouseX = e.X;
                selectionMouseY = e.Y;
                mapScreen.Invalidate();
                return;
            }
            int WIDTH = ConfigScript.getScreenWidth(curActiveLevelForScreen);
            int HEIGHT = ConfigScript.getScreenHeight(curActiveLevelForScreen);
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
                    if (curActiveScreen < ConfigScript.screensOffset[curActiveLevelForScreen].recCount - 1)
                    {
                        int index = dy * WIDTH;
                        ConfigScript.setBigTileToScreen(activeScreens[curActiveScreen + 1], index, curActiveBlock);
                        dirty = true; updateSaveVisibility();
                    }
                }
                else if (dx == -1)
                {
                    if (curActiveScreen > 0)
                    {
                        int index = dy * WIDTH + (WIDTH - 1);
                        ConfigScript.setBigTileToScreen(activeScreens[curActiveScreen - 1], index, curActiveBlock);
                        dirty = true; updateSaveVisibility();
                    }
                }
                else
                {
                    if (!useStructs)
                    {
                        int index = dy * WIDTH + dx;
                        ConfigScript.setBigTileToScreen(activeScreens[curActiveScreen], index, curActiveBlock);
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
            int WIDTH = ConfigScript.getScreenWidth(curActiveLevelForScreen);
            int HEIGHT = ConfigScript.getScreenHeight(curActiveLevelForScreen);
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
                        ConfigScript.setBigTileToScreen(activeScreens[curActiveScreen], index, no);
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
                        arrayToSave[addr + x * dataStride] = (byte)ConfigScript.backConvertScreenTile(screensData[i][x]);
                }
                else if (wordLen == 2)
                {
                    if (littleEndian)
                    {
                        for (int x = 0; x < screensRec.recSize; x++)
                            Utils.writeWordLE(arrayToSave, addr + x * (dataStride * wordLen), ConfigScript.backConvertScreenTile(screensData[i][x]));
                    }
                    else
                    {
                        for (int x = 0; x < screensRec.recSize; x++)
                            Utils.writeWord(arrayToSave, addr + x * (dataStride * wordLen), ConfigScript.backConvertScreenTile(screensData[i][x]));
                    }
                }
            }
        }

        private bool saveToFile()
        {
            saveScreens(ConfigScript.screensOffset[curActiveLevelForScreen], screens);
            if (ConfigScript.getLayersCount() > 1)
                saveScreens(ConfigScript.screensOffset2, screens2);
            dirty = !Globals.flushToFile(); updateSaveVisibility();
            return !dirty;
        }



        private void cbLevel_SelectedIndexChanged(object sender, EventArgs ev)
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
                    mapScreen.Size = new Size((int)(ConfigScript.getScreenHeight(curActiveLevelForScreen) * blockWidth * curScale), (int)((ConfigScript.getScreenWidth(curActiveLevelForScreen) + 2) * blockHeight * curScale));
                else
                    mapScreen.Size = new Size((int)((ConfigScript.getScreenWidth(curActiveLevelForScreen) + 2) * blockWidth * curScale), (int)(ConfigScript.getScreenHeight(curActiveLevelForScreen) * blockHeight * curScale));
            }
        }

        private void changeLevelIndex(bool reloadObjectsPanel = false)
        {
            curActiveVideoNo = cbVideoNo.SelectedIndex + 0x90;
            curActiveBigBlockNo = cbBigBlockNo.SelectedIndex;
            curActiveBlockNo = cbBlockNo.SelectedIndex;
            curActivePalleteNo = cbPaletteNo.SelectedIndex;
            curViewType = (MapViewType)cbViewType.SelectedIndex;
            reloadLevel(true, reloadObjectsPanel);
        }

        private void returnCbLevelIndex()
        {
            cbLevelNo.SelectedIndexChanged -= cbLevelNo_SelectedIndexChanged;
            cbLevelNo.SelectedIndex = curActiveLevelForScreen;
            cbLevelNo.SelectedIndexChanged += cbLevelNo_SelectedIndexChanged;
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
            pnGeneric.Visible = true;
        }

        private void btOpen_Click(object sender, EventArgs e)
        {
            openFile();
            reloadGameType();
            changeLevelIndex(true);
        }

        public void setDirty()
        {
            dirty = true;
            updateSaveVisibility();
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
            //reloadLevel(false);
        }

        private FormClosedEventHandler subeditorClosed(ToolStripButton enabledAfterCloseButton)
        {
            return delegate(object sender, FormClosedEventArgs e) 
            { 
                enabledAfterCloseButton.Enabled = true;
                reloadLevel();
            };
        }

        public void subeditorOpen(Form f, ToolStripButton b, bool showDialog = false)
        {
            if (Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
            {
                updateSaveVisibility();
                b.Enabled = false;
                f.FormClosed += subeditorClosed(b);
                if (showDialog)
                {
                  f.ShowDialog();
                }
                else
                {
                  f.Show();
                }
            }
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

        public bool ShowAxis
        {
            get { return showAxis; }
        }

        public int LevelNoForScreens
        {
            get { return curActiveLevelForScreen; }
        }

        public int ScreenNo
        {
            get { return curActiveScreen; }
        }

        public float CurScale
        {
            get { return curScale; }
        }

        public bool ShowLayer1
        {
            get { return showLayer1; }
        }

        public bool ShowLayer2
        {
            get { return showLayer2; }
        }

        public int BlockWidth
        {
            get { return blockWidth; }
        }

        public int BlockHeight
        {
            get { return blockHeight; }
        }

        public ImageList BigBlocks
        {
            get { return bigBlocks; }
        }

        public ImageList getBigBlockImageList()
        {
            return bigBlocks;
        }

        //warnging! danger direct function. do not use it
        public void SetScreens(int[][] screens)
        {
            this.screens = screens;
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
                if (Control.ModifierKeys == Keys.Alt)
                {
                    convertMouseToDxDy(e, out selectionBeginX, out selectionBeginY);
                    selectionBeginMouseX = e.X;
                    selectionBeginMouseY = e.Y;
                    selectionRect = true;
                }
                else
                {
                    curClicked = true;
                    mapScreen_MouseMove(sender, e);
                }
            }
        }

        private void mapScreen_MouseUp(object sender, MouseEventArgs e)
        {
            if (selectionRect)
            {
                convertMouseToDxDy(e, out selectionEndX, out selectionEndY);
                if (selectionEndX < selectionBeginX)
                {
                    selectionBeginX ^= selectionEndX;
                    selectionEndX ^= selectionBeginX;
                    selectionBeginX ^= selectionEndX;
                }
                if (selectionEndY < selectionBeginY)
                {
                    selectionBeginY ^= selectionEndY;
                    selectionEndY ^= selectionBeginY;
                    selectionBeginY ^= selectionEndY;
                }
                /*var f = new FormStructures();
                f.setFormMain(this);
                f.Show();*/
                int deltaX = selectionEndX - selectionBeginX + 1;
                int deltaY = selectionEndY - selectionBeginY + 1;
                int [][] tiles = new int[deltaY][];
                for (int arrs = 0; arrs < tiles.Length; arrs++)
                    tiles[arrs] = new int[deltaX];
                var curScreen = screens[curActiveScreen]; //screens2?
                for (int i = 0; i < deltaX; i++)
                {
                    for (int j = 0; j < deltaY; j++)
                    {
                        int index = (selectionBeginY + j)*ConfigScript.getScreenWidth(curActiveLevelForScreen) + (selectionBeginX + i);
                        tiles[j][i] = curScreen[index];
                    }
                }
                FormStructures.addTileStruct(tiles);
                cbUseStructs.Checked = true;
                if (useStructs)
                    updateBlocksPanelVisible();
            }
            selectionRect = false;
            curClicked = false;
        }

        private void convertMouseToDxDy(MouseEventArgs e, out int dx, out int dy)
        {
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

        public void reloadCallback()
        {
            bttReload_Click(null, new EventArgs());
        }

        private void bttReload_Click(object sender, EventArgs e)
        {
            if (Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
            {
                reloadLevel(true, true);
                mapScreen.Invalidate();
            }
        }

        private void cbLevelNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
            {
                updateSaveVisibility();
                return;
            }
            curActiveLevelForScreen = cbLevelNo.SelectedIndex;
            resetScreens();
            resetMapScreenSize();
            mapScreen.Invalidate();
        }

        private void cbPanelNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            FlowLayoutPanel[] blocksPanels = { blocksPanel, blockPanel2, blockPanel3, blockPanel4 };
            int index = cbPanelNo.SelectedIndex;
            for (int i = 0; i < blocksPanels.Length; i++)
                blocksPanels[i].Visible = i == index;
        }

        public void addSubeditorButton(ToolStripItem item)
        {
          toolStrip1.Items.Insert(toolStrip1.Items.IndexOf(bttEnemies)+1, item);
        }

        public void addToolButton(ToolStripItem item)
        {
            toolStrip1.Items.Insert(toolStrip1.Items.IndexOf(toolStripSeparator1) + 1, item);
        }

        private void tbbShowPluginInfo_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Loaded Plugins:\n\n");
            foreach (var p in ConfigScript.plugins)
            {
                sb.Append(p.getName() + "\n");
            }
            if (ConfigScript.videoNes != null)
                sb.Append(ConfigScript.videoNes.getName() + "\n");
            if (ConfigScript.videoSega != null)
                sb.Append(ConfigScript.videoSega.getName() + "\n");
            MessageBox.Show(sb.ToString());
        }

        private void cbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbGroup.SelectedIndex < 0)
                return;
            GroupRec g = ConfigScript.getGroup(cbGroup.SelectedIndex);
            Utils.setCbIndexWithoutUpdateLevel(cbVideoNo, cbLevel_SelectedIndexChanged, g.videoNo);
            Utils.setCbIndexWithoutUpdateLevel(cbBigBlockNo, cbLevel_SelectedIndexChanged, g.bigBlockNo);
            Utils.setCbIndexWithoutUpdateLevel(cbBlockNo, cbLevel_SelectedIndexChanged, g.blockNo);
            Utils.setCbIndexWithoutUpdateLevel(cbPaletteNo, cbLevel_SelectedIndexChanged, g.palNo);
            cbLevel_SelectedIndexChanged(cbVideoNo, new EventArgs());
            if (g.firstScreen < 0 || g.firstScreen <= cbScreenNo.Items.Count)
              cbScreenNo.SelectedIndex = g.firstScreen - 1;
        }

        private void tbbShowInfo_Click(object sender, EventArgs e)
        {
            var sb = new StringBuilder();
            sb.Append("CadEditor\n\n");
            sb.Append("Level editor for NES, SEGA, GBA games\n");
            sb.Append("(You need original ROMs for editing it)\n\n");
            sb.Append("Author: spiiin (sanya.boyko@gmail.com)\n\n");
            sb.Append("Project support: R122299008919\n");
            sb.Append("(It's really important for project development)\n");
            MessageBox.Show(sb.ToString());
        }
    }
}
