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
            this.KeyPreview = true;
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
                curScale = curButtonScale =  ConfigScript.isBuildScreenFromSmallBlocks() ? 1 : 2;
                fileLoaded = true;

                //Change size without event, it will call later
                pnBlocks.SizeChanged -= pnBlocks_SizeChanged;
                splitContainer1.Width = this.Width - 21;
                splitContainer1.Height = this.Height - 81;
                pnBlocks.SizeChanged += pnBlocks_SizeChanged;
                //
                resetControls();
            }

            subeditorsDict = new Dictionary<ToolStripButton, Func<Form>> { 
                 { bttBigBlocks,    ()=>{ var f = new BigBlockEdit();  f.setFormMain(this); return f;} },
                 { bttBlocks,       makeBlocksEditor },
                 { bttEnemies,      ()=>{ var f = new EnemyEditor();  f.setFormMain(this); return f;}  },
                 { bttConfig,       ()=>{ var f = new FormConfig();  f.setFormMain(this); f.onApply += reloadCallback; return f;}    },
            };

            ConfigScript.plugins.ForEach((p) => p.addToolButton(this));
            ConfigScript.plugins.ForEach((p) => p.addSubeditorButton(this));
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

            layers[0].screens = Utils.setScreens(curActiveLevelForScreen);
            if (ConfigScript.getLayersCount() > 1)
                layers[1].screens = Utils.setScreens2();
        }

        private void changeBlocksSize(Image[] bigImages)
        {
            layers[0].blockWidth = bigImages[0].Width;
            layers[0].blockHeight = bigImages[0].Height;
            layers[1].blockWidth = layers[0].blockWidth;
            layers[1].blockHeight = layers[0].blockHeight;
        }

        private void resetControls()
        {
            curActiveLevelForScreen = 0;
            resetScreens();

            UtilsGui.setCbItemsCount(cbVideoNo, ConfigScript.videoOffset.recCount);
            UtilsGui.setCbItemsCount(cbBigBlockNo, ConfigScript.bigBlocksOffsets[0].recCount);
            UtilsGui.setCbItemsCount(cbBlockNo, ConfigScript.blocksOffset.recCount);
            UtilsGui.setCbItemsCount(cbPaletteNo, ConfigScript.palOffset.recCount);
            UtilsGui.setCbItemsCount(cbLevelNo, ConfigScript.getLevelsCount());
            UtilsGui.setCbIndexWithoutUpdateLevel(cbVideoNo, cbLevel_SelectedIndexChanged);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbBigBlockNo, cbLevel_SelectedIndexChanged);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbBlockNo, cbLevel_SelectedIndexChanged);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbPaletteNo, cbLevel_SelectedIndexChanged);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbLevelNo, cbLevelNo_SelectedIndexChanged);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbViewType, cbLevel_SelectedIndexChanged);

            cbGroup.Items.Clear();
            foreach (var g in ConfigScript.getGroups())
            {
                cbGroup.Items.Add(g.name);
            }
            dirty = false; updateSaveVisibility();
            showNeiScreens = true;
            showAxis = true;
            showBrush = true;
            useStructs = false;
            curActiveLayer = 0;

            reloadGameType();
            changeLevelIndex(true);

            bttBigBlocks.Enabled = ConfigScript.isBigBlockEditorEnabled;
            bttBlocks.Enabled = ConfigScript.isBlockEditorEnabled;
            bttEnemies.Enabled = ConfigScript.isEnemyEditorEnabled;

            bttShowLayer1.Visible = ConfigScript.getLayersCount() > 1;
            bttShowLayer2.Visible = ConfigScript.getLayersCount() > 1;
            bttLayer.Visible = ConfigScript.getLayersCount() > 1;

            pnGroups.Visible = ConfigScript.getGroups().Length > 0;

            resetMapScreenSize();
        }

        void resetMapScreenSize()
        {
            if (ConfigScript.getScreenVertical())
                mapScreen.Size = new Size((int)(ConfigScript.getScreenHeight(curActiveLevelForScreen) * layers[0].blockWidth * curScale), (int)((ConfigScript.getScreenWidth(curActiveLevelForScreen) + 2) * layers[0].blockHeight * curScale));
            else
                mapScreen.Size = new Size((int)((ConfigScript.getScreenWidth(curActiveLevelForScreen) + 2) * layers[0].blockWidth * curScale), (int)(ConfigScript.getScreenHeight(curActiveLevelForScreen) * layers[0].blockHeight * curScale));
        }

        public void reloadLevel(bool reloadScreens = true, bool rebuildBlocks = false)
        {
            setBlocks(rebuildBlocks);
            if (reloadScreens)
                resetScreens();
            mapScreen.Invalidate();
        }

        private Image[] makeSegaBigBlocks()
        {
            byte[] mapping = ConfigScript.getSegaMapping(curActiveBigBlockNo);
            byte[] videoTiles = ConfigScript.getVideoChunk(curActiveVideoNo);
            byte[] pal = ConfigScript.getPal(curActivePalleteNo);
            int count = ConfigScript.getBigBlocksCount(ConfigScript.getbigBlocksHierarchyCount()-1);
            return ConfigScript.videoSega.makeBigBlocks(mapping, videoTiles, pal, count, curViewType, showAxis);
        }

        private void setBlocks(bool needRebuildBlocks)
        {
            //if using pictures
            if (ConfigScript.usePicturesInstedBlocks)
            {
                if (needRebuildBlocks)
                {
                    //get block size from image
                    layers[0].blockWidth = ConfigScript.getBlocksPicturesWidth();
                    layers[0].blockHeight = 32;
                    layers[1].blockWidth = ConfigScript.getBlocksPicturesWidth();
                    layers[1].blockHeight = 32;
                    bigBlocks = UtilsGDI.setBlocksForPictures(curButtonScale, layers[0].blockWidth, layers[0].blockHeight, curViewType, showAxis);
                }
                updateBlocksImages();
                return;
            }

            MapViewType smallObjectsType =
                curViewType == MapViewType.SmallObjNumbers ? MapViewType.ObjNumbers :
                  curViewType == MapViewType.ObjType ? MapViewType.ObjType : MapViewType.Tiles;

            float smallBlockScaleFactor = curButtonScale;
            int bigTileIndex = curActiveBlockNo;
            if (needRebuildBlocks)
            {
                if (ConfigScript.isUseSegaGraphics())
                {
                    bigBlocks = makeSegaBigBlocks();
                }
                else
                {
                    bigBlocks = ConfigScript.videoNes.makeBigBlocks(curActiveVideoNo, curActiveBigBlockNo, bigTileIndex, curActivePalleteNo, smallObjectsType, curViewType, showAxis, ConfigScript.getbigBlocksHierarchyCount() - 1);
                }
                changeBlocksSize(bigBlocks);
            }
            curActiveBlock = 0;
            updateBlocksImages();
        }

        private void updateBlocksImages()
        {
            UtilsGui.resizeBlocksScreen(bigBlocks, blocksScreen, layers[0].blockWidth, layers[0].blockHeight, curScale);
            blocksScreen.Invalidate();
        }

        private void buttonBlockClick(Object button, EventArgs e)
        {
            int index = ((Button)button).ImageIndex;
            activeBlock.Image = bigBlocks[index];
            curActiveBlock = index;
            lbActiveBlock.Text = String.Format("Label: ({0:X})", index);

            blocksScreen.Invalidate();
        }

        private void renderNeighbornLine(Graphics g, int screenNo, int line, int X)
        {
            int WIDTH = ConfigScript.getScreenWidth(curActiveLevelForScreen);
            int HEIGHT = ConfigScript.getScreenHeight(curActiveLevelForScreen);
            int TILE_SIZE_X = (int)(layers[0].blockWidth * curScale);
            int TILE_SIZE_Y = (int)(layers[0].blockHeight * curScale);
            int SIZE = WIDTH * HEIGHT;
            int[] indexesPrev = layers[0].screens[screenNo];
            for (int i = 0; i < SIZE; i++)
            {
                if (i % WIDTH == line)
                {
                    int index = indexesPrev[i];
                    int bigBlockNo = ConfigScript.getBigTileNoFromScreen(indexesPrev, i);
                    if (bigBlockNo < bigBlocks.Length)
                        g.DrawImage(bigBlocks[bigBlockNo], new Rectangle(X, i / WIDTH * TILE_SIZE_Y, TILE_SIZE_X, TILE_SIZE_Y));
                }
            }
        }

        private void drawActiveTileStruct(Graphics g, Rectangle visibleRect)
        {
            int TILE_SIZE_X = (int)(layers[0].blockWidth * curScale);
            int TILE_SIZE_Y = (int)(layers[0].blockHeight * curScale);
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
                            if ((index!=-1) && (index < bigBlocks.Length))
                                g.DrawImage(bigBlocks[index], tileRect);
                        }
                    }
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (!fileLoaded)
                return;
            int[] indexes = layers[0].screens[curActiveScreen];
            int[] indexes2 = (ConfigScript.getLayersCount() > 1) ? layers[1].screens[curActiveScreen] : null;
            var g = e.Graphics;

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            int WIDTH = ConfigScript.getScreenWidth(curActiveLevelForScreen);
            int HEIGHT = ConfigScript.getScreenHeight(curActiveLevelForScreen);
            int TILE_SIZE_X = (int)(layers[0].blockWidth * curScale);
            int TILE_SIZE_Y = (int)(layers[0].blockHeight * curScale);
            int SIZE = WIDTH * HEIGHT;
            var visibleRect = UtilsGui.getVisibleRectangle(pnView, mapScreen);
            MapEditor.Render(e.Graphics, bigBlocks, visibleRect, layers, curActiveScreen, curScale, true, ConfigScript.getScreenVertical() ? TILE_SIZE_Y : TILE_SIZE_X, 0, WIDTH, HEIGHT);

            if (!ConfigScript.getScreenVertical() && showNeiScreens && (curActiveScreen > 0) && layers[0].showLayer)
            {
                renderNeighbornLine(g, curActiveScreen - 1, (WIDTH - 1), 0);
            }
            if (!ConfigScript.getScreenVertical() && showNeiScreens && (curActiveScreen < ConfigScript.screensOffset[curActiveLevelForScreen].recCount - 1) && layers[0].showLayer)
            {
                renderNeighbornLine(g, curActiveScreen + 1, 0 , (WIDTH + 1) * TILE_SIZE_X);
            }

            //show brush
            bool altPressed = Control.ModifierKeys == Keys.Alt;
            if (showBrush && curActiveBlock != -1 && (curDx != OUTSIDE || curDy != OUTSIDE) && !altPressed)
            {
                if (!useStructs)
                {
                    var tx = ConfigScript.getScreenVertical() ? curDy * TILE_SIZE_X : (curDx + 1) * TILE_SIZE_X;
                    var ty = ConfigScript.getScreenVertical() ? (curDx + 1) * TILE_SIZE_Y : curDy * TILE_SIZE_Y;
                    var tileRect = new Rectangle(tx, ty, TILE_SIZE_X, TILE_SIZE_Y);
                    g.DrawImage(bigBlocks[curActiveBlock], tileRect);
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
        //private int blockWidth = 32;
        //private int blockHeight = 32;

        bool useStructs;
        TileStructure curTileStruct;

        MapViewType curViewType = MapViewType.ObjType;
        private bool dirty;
        private bool showNeiScreens;
        private bool showAxis;
        private bool showBrush;
        private BlockLayer[] layers = new BlockLayer[2] { new BlockLayer(), new BlockLayer() };

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
        private Image[] bigBlocks = new Image[0];

        private void mapScreen_MouseClick(object sender, MouseEventArgs ea)
        {
            var ee = ea.Location;
            if (ee.X < 0) { ee.X += 32768 * 2; }
            if (ee.Y < 0) { ee.Y += 32768 * 2; }
            int WIDTH = ConfigScript.getScreenWidth(curActiveLevelForScreen);
            int HEIGHT = ConfigScript.getScreenHeight(curActiveLevelForScreen);
            int dx, dy;
            if (ConfigScript.getScreenVertical())
            {
                dy = ee.X / (int)(layers[0].blockWidth * curScale);
                dx = ee.Y / (int)(layers[0].blockHeight * curScale) - 1;
            }
            else
            {
                dx = ee.X / (int)(layers[0].blockWidth * curScale) - 1;
                dy = ee.Y / (int)(layers[0].blockHeight * curScale);
            }

            if (ea.Button == MouseButtons.Right)
            {
                if (dx == WIDTH || dx == -1)
                    return;
                int index = dy * WIDTH + dx;
                curActiveBlock = ConfigScript.getBigTileNoFromScreen(layers[0].screens[curActiveScreen], index);
                if (curActiveBlock != -1)
                {
                    activeBlock.Image = bigBlocks[curActiveBlock];
                    lbActiveBlock.Text = String.Format("Label: {0:X}", curActiveBlock);
                }
                blocksScreen.Invalidate();
                return;
            }
        }

        private void mapScreen_MouseMove(object sender, MouseEventArgs ea)
        {
            var ee = ea.Location;
            if (ee.X < 0) { ee.X += 32768 * 2; }
            if (ee.Y < 0) { ee.Y += 32768 * 2; }

            if (selectionRect)
            {
                selectionMouseX = ee.X;
                selectionMouseY = ee.Y;
                mapScreen.Invalidate();
                return;
            }
            int WIDTH = ConfigScript.getScreenWidth(curActiveLevelForScreen);
            int HEIGHT = ConfigScript.getScreenHeight(curActiveLevelForScreen);
            int dx, dy;
            if (ConfigScript.getScreenVertical())
            {
                dy = ee.X / (int)(layers[0].blockWidth * curScale);
                dx = ee.Y / (int)(layers[0].blockHeight * curScale) - 1;
            }
            else
            {
                dx = ee.X / (int)(layers[0].blockWidth * curScale) - 1;
                dy = ee.Y / (int)(layers[0].blockHeight * curScale);
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
                var activeScreens = layers[curActiveLayer].screens;
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
                        if (index < activeScreens[curActiveScreen].Length)
                        {
                            ConfigScript.setBigTileToScreen(activeScreens[curActiveScreen], index, curActiveBlock);
                        }
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
            int TILE_SIZE_X = (int)(layers[0].blockWidth * curScale);
            int TILE_SIZE_Y = (int)(layers[0].blockHeight * curScale);
            var activeScreens = layers[curActiveLayer].screens;
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
            saveScreens(ConfigScript.screensOffset[curActiveLevelForScreen], layers[0].screens);
            if (ConfigScript.getLayersCount() > 1)
                saveScreens(ConfigScript.screensOffset2, layers[1].screens);
            dirty = !Globals.flushToFile(); updateSaveVisibility();
            return !dirty;
        }



        private void cbLevel_SelectedIndexChanged(object sender, EventArgs ev)
        {
            if (!UtilsGui.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
            {
                updateSaveVisibility();
                return;
            }
            updateSaveVisibility();
            bool senderIsScale = sender == bttScale;
            changeLevelIndex(!senderIsScale);
            if (senderIsScale)
            {
                if (ConfigScript.getScreenVertical())
                    mapScreen.Size = new Size((int)(ConfigScript.getScreenHeight(curActiveLevelForScreen) * layers[0].blockWidth * curScale), (int)((ConfigScript.getScreenWidth(curActiveLevelForScreen) + 2) * layers[0].blockHeight * curScale));
                else
                    mapScreen.Size = new Size((int)((ConfigScript.getScreenWidth(curActiveLevelForScreen) + 2) * layers[0].blockWidth * curScale), (int)(ConfigScript.getScreenHeight(curActiveLevelForScreen) * layers[0].blockHeight * curScale));
                updateBlocksImages();
            }
        }

        private void changeLevelIndex(bool reloadBlocks = false)
        {
            curActiveVideoNo = cbVideoNo.SelectedIndex;
            curActiveBigBlockNo = cbBigBlockNo.SelectedIndex;
            curActiveBlockNo = cbBlockNo.SelectedIndex;
            curActivePalleteNo = cbPaletteNo.SelectedIndex;
            curViewType = (MapViewType)cbViewType.SelectedIndex;
            reloadLevel(true, reloadBlocks);
        }

        private void returnCbLevelIndex()
        {
            cbLevelNo.SelectedIndexChanged -= cbLevelNo_SelectedIndexChanged;
            cbLevelNo.SelectedIndex = curActiveLevelForScreen;
            cbLevelNo.SelectedIndexChanged += cbLevelNo_SelectedIndexChanged;
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!UtilsGui.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
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
            if (!UtilsGui.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
            {
                updateSaveVisibility();
                return false;
            }
            updateSaveVisibility();
            var f = new OpenFile();
            if (f.ShowDialog() == DialogResult.OK)
            {
                Globals.loadData(OpenFile.FileName, OpenFile.DumpName, OpenFile.ConfigName);
                curScale = curButtonScale = ConfigScript.isBuildScreenFromSmallBlocks() ? 1 : 2;
                fileLoaded = true;
                resetControls();
                setWindowText();
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
            if (openFile())
            {
                reloadGameType();
                changeLevelIndex();
            }
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
            reloadLevel(false, true);
        }

        private void bttShowBrush_CheckedChanged(object sender, EventArgs e)
        {
            showBrush = bttShowBrush.Checked;
        }

        private FormClosedEventHandler subeditorClosed(ToolStripItem enabledAfterCloseButton)
        {
            return delegate(object sender, FormClosedEventArgs e) 
            { 
                enabledAfterCloseButton.Enabled = true;
                reloadLevel(true, true);
            };
        }

        public void subeditorOpen(Form f, ToolStripItem b, bool showDialog = false)
        {
            if (UtilsGui.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
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

        public BlockLayer[] Layers
        {
            get { return layers; }
        }

        public Image[] BigBlocks
        {
            get { return bigBlocks; }
        }

        public Image[] getBigBlockImages()
        {
            return bigBlocks;
        }

        //warnging! danger direct function. do not use it
        public void SetScreens(int[][] screens)
        {
            this.layers[0].screens = screens;
        }

        private void bttScale_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            float[] scaleFactors = { 0.25f, 0.5f, 1.0f, 2.0f, 3.0f, 4.0f };
            curScale = curButtonScale = scaleFactors [bttScale.DropDownItems.IndexOf(e.ClickedItem)];
            cbLevel_SelectedIndexChanged(bttScale, new EventArgs());
        }

        private void mapScreen_MouseDown(object sender, MouseEventArgs ea)
        {
            var ee = ea.Location;
            if (ee.X < 0) { ee.X += 32768 * 2; }
            if (ee.Y < 0) { ee.Y += 32768 * 2; }
            if (ea.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (Control.ModifierKeys == Keys.Alt)
                {
                    convertMouseToDxDy(ee, out selectionBeginX, out selectionBeginY);
                    selectionBeginMouseX = ee.X;
                    selectionBeginMouseY = ee.Y;
                    selectionRect = true;
                }
                else
                {
                    curClicked = true;
                    mapScreen_MouseMove(sender, ea);
                }
            }
        }

        private void mapScreen_MouseUp(object sender, MouseEventArgs ea)
        {
            var ee = ea.Location;
            if (ee.X < 0) { ee.X += 32768 * 2; }
            if (ee.Y < 0) { ee.Y += 32768 * 2; }
            if (selectionRect)
            {
                convertMouseToDxDy(ee, out selectionEndX, out selectionEndY);
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
                int deltaX = selectionEndX - selectionBeginX + 1;
                int deltaY = selectionEndY - selectionBeginY + 1;
                int [][] tiles = new int[deltaY][];
                for (int arrs = 0; arrs < tiles.Length; arrs++)
                    tiles[arrs] = new int[deltaX];
                var curScreen = layers[0].screens[curActiveScreen]; //screens2?
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

        private void convertMouseToDxDy(Point e, out int dx, out int dy)
        {
            if (ConfigScript.getScreenVertical())
            {
                dy = e.X / (int)(layers[0].blockWidth * curScale);
                dx = e.Y / (int)(layers[0].blockHeight * curScale) - 1;
            }
            else
            {
                dx = e.X / (int)(layers[0].blockWidth * curScale) - 1;
                dy = e.Y / (int)(layers[0].blockHeight * curScale);
            }
        }

        private void bttShowLayer1_CheckedChanged(object sender, EventArgs e)
        {
            layers[0].showLayer = bttShowLayer1.Checked;
            mapScreen.Invalidate();
        }

        private void bttShowLayer2_CheckedChanged(object sender, EventArgs e)
        {
            layers[1].showLayer = bttShowLayer2.Checked;
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
            pnBlocks.Visible = !useStructs;
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
            if (UtilsGui.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
            {
                reloadLevel(true, true);
                mapScreen.Invalidate();
            }
        }

        private void cbLevelNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!UtilsGui.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
            {
                updateSaveVisibility();
                return;
            }
            curActiveLevelForScreen = cbLevelNo.SelectedIndex;
            resetScreens();
            resetMapScreenSize();
            mapScreen.Invalidate();
        }

        private void cbAdvanced_CheckedChanged(object sender, EventArgs e)
        {
            pnAdvancedParams.Visible = cbAdvanced.Checked;
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            /*
            //useful for config debugging
            if (e.KeyCode == Keys.Q)
            {
                ConfigScript.palBytesAddr += 16;
                lbPalBytesAddr.Text = String.Format("PalBytesAddr:{0:X}", ConfigScript.palBytesAddr);
                reloadLevel(true, true);
                mapScreen.Invalidate();
            }
            else if (e.KeyCode == Keys.W)
            {
                ConfigScript.palBytesAddr -= 16;
                lbPalBytesAddr.Text = String.Format("PalBytesAddr:{0:X}", ConfigScript.palBytesAddr);
                reloadLevel(true, true);
                mapScreen.Invalidate();
            }*/
        }

        private void blocksScreen_Paint(object sender, PaintEventArgs e)
        {
            if (!fileLoaded)
                return;
            var g = e.Graphics;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            var visibleRect = UtilsGui.getVisibleRectangle(pnBlocks, blocksScreen);
            MapEditor.RenderAllBlocks(e.Graphics, blocksScreen, bigBlocks, layers[0].blockWidth, layers[0].blockHeight, visibleRect, curScale, curActiveBlock);
        }

        private void blocksScreen_MouseDown(object sender, MouseEventArgs e)
        {
            var p = blocksScreen.PointToClient(Cursor.Position);
            int x = p.X, y = p.Y;
            int TILE_SIZE_X = (int)(layers[0].blockWidth * CurScale);
            int TILE_SIZE_Y = (int)(layers[0].blockHeight * CurScale);
            int tx = x / TILE_SIZE_X, ty = y / TILE_SIZE_Y;
            int maxtX = blocksScreen.Width / TILE_SIZE_X;
            int index = ty * maxtX + tx;
            if ((tx < 0) || (tx >= maxtX) || (index < 0) || (index >= bigBlocks.Length))
            {
                return;
            }

            activeBlock.Image = bigBlocks[index];
            curActiveBlock = index;
            lbActiveBlock.Text = String.Format("Label: ({0:X})", index);
            blocksScreen.Invalidate();
        }

        private void pnBlocks_SizeChanged(object sender, EventArgs e)
        {
            updateBlocksImages();
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
            UtilsGui.setCbIndexWithoutUpdateLevel(cbVideoNo, cbLevel_SelectedIndexChanged, g.videoNo);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbBigBlockNo, cbLevel_SelectedIndexChanged, g.bigBlockNo);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbBlockNo, cbLevel_SelectedIndexChanged, g.blockNo);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbPaletteNo, cbLevel_SelectedIndexChanged, g.palNo);
            cbLevel_SelectedIndexChanged(cbVideoNo, new EventArgs());
            if (g.firstScreen < 0 || g.firstScreen <= cbScreenNo.Items.Count)
              cbScreenNo.SelectedIndex = g.firstScreen - 1;
        }

        private void tbbShowInfo_Click(object sender, EventArgs e)
        {
            new About().ShowDialog();
        }

        private void setWindowText()
        {
            Text = String.Format("CAD Editor v4.3 - {0}", OpenFile.FileName);
        }
    }
}
