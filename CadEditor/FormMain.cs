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
                curScale =  ConfigScript.isBuildScreenFromSmallBlocks() ? 1 : 2;
                fileLoaded = true;

                resetControls();
            }

            subeditorsDict = new Dictionary<ToolStripButton, Func<Form>> { 
                 { bttBigBlocks,    ()=>{ var f = new BigBlockEdit();  f.setFormMain(this); return f;} },
                 { bttBlocks,       makeBlocksEditor },
                 { bttEnemies,      ()=>{ var f = new EnemyEditor();  f.setFormMain(this); return f;}  },
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
            screens = ConfigScript.loadScreens();
            int count = screens.Length;

            int oldScreenNo = cbScreenNo.SelectedIndex;
            cbScreenNo.Items.Clear();
            for (int i = 0; i < count; i++)
                cbScreenNo.Items.Add(String.Format("{0:X}", i + 1));

            if (oldScreenNo == -1)
                cbScreenNo.SelectedIndex = 0;
            else if (oldScreenNo < cbScreenNo.Items.Count)
                cbScreenNo.SelectedIndex = oldScreenNo;


            //todo: layer 2
            /*if (ConfigScript.getLayersCount() > 1)
                screens = Utils.setScreens2();*/
        }

        private void changeBlocksSize(Image[] bigImages)
        {
        }

        private void resetControls()
        {
            resetScreens();

            UtilsGui.setCbItemsCount(cbVideoNo, ConfigScript.videoOffset.recCount);
            UtilsGui.setCbItemsCount(cbBigBlockNo, ConfigScript.bigBlocksOffsets[0].recCount);
            UtilsGui.setCbItemsCount(cbBlockNo, ConfigScript.blocksOffset.recCount);
            UtilsGui.setCbItemsCount(cbPaletteNo, ConfigScript.palOffset.recCount);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbVideoNo, cbLevel_SelectedIndexChanged);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbBigBlockNo, cbLevel_SelectedIndexChanged);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbBlockNo, cbLevel_SelectedIndexChanged);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbPaletteNo, cbLevel_SelectedIndexChanged);
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
            if (bigBlocks.Length > 0)
            {
                var screen = getActiveScreen();
                if (ConfigScript.getScreenVertical())
                    mapScreen.Size = new Size((int)(screen.height * bigBlocks[0].Width * curScale), (int)((screen.width + 2) * bigBlocks[0].Height * curScale));
                else
                    mapScreen.Size = new Size((int)((screen.width + 2) * bigBlocks[0].Width * curScale), (int)(screen.height * bigBlocks[0].Height * curScale));
            }
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
            return ConfigScript.videoSega.makeBigBlocks(mapping, videoTiles, pal, count, curViewType);
        }

        private void setBlocks(bool needRebuildBlocks)
        {
            var screen = getActiveScreen();
            //if using pictures
            if (ConfigScript.usePicturesInstedBlocks)
            {
                if (needRebuildBlocks)
                {
                    //get block size from image
                    int w = ConfigScript.getBlocksPicturesWidth();
                    int h = 32;
                    bigBlocks = UtilsGDI.setBlocksForPictures(curScale, w, h, curViewType);
                }
                updateBlocksImages();
                return;
            }

            MapViewType smallObjectsType =
                curViewType == MapViewType.SmallObjNumbers ? MapViewType.ObjNumbers :
                  curViewType == MapViewType.ObjType ? MapViewType.ObjType : MapViewType.Tiles;

            if (needRebuildBlocks)
            {
                if (ConfigScript.isUseSegaGraphics())
                {
                    bigBlocks = makeSegaBigBlocks();
                }
                else
                {
                    bigBlocks = ConfigScript.videoNes.makeBigBlocks(curActiveVideoNo, curActiveBigBlockNo, curActiveBlockNo, curActivePalleteNo, smallObjectsType, curViewType, ConfigScript.getbigBlocksHierarchyCount() - 1);
                }
            }
            changeBlocksSize(bigBlocks);
            curActiveBlock = 0;
            updateBlocksImages();
        }

        private void updateBlocksImages()
        {
            if (bigBlocks.Length > 0)
            {
                UtilsGui.resizeBlocksScreen(bigBlocks, blocksScreen, bigBlocks[0].Width, bigBlocks[0].Height, curScale);
                blocksScreen.Invalidate();
            }
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
            Screen prevScreen = screens[screenNo];
            int WIDTH = prevScreen.width;
            int HEIGHT = prevScreen.height;
            int TILE_SIZE_X = (int)(bigBlocks[0].Width * curScale);
            int TILE_SIZE_Y = (int)(bigBlocks[0].Height * curScale);
            int SIZE = WIDTH * HEIGHT;
            int[] indexesPrev = prevScreen.layers[0].data;
            for (int i = 0; i < SIZE; i++)
            {
                if (i % WIDTH == line)
                {
                    int bigBlockNo = ConfigScript.getBigTileNoFromScreen(indexesPrev, i);
                    if (bigBlockNo < bigBlocks.Length)
                        g.DrawImage(bigBlocks[bigBlockNo], new Rectangle(X, i / WIDTH * TILE_SIZE_Y, TILE_SIZE_X, TILE_SIZE_Y));
                }
            }
        }

        private void drawActiveTileStruct(Graphics g, Rectangle visibleRect)
        {
            var screen = getActiveScreen();
            int TILE_SIZE_X = (int)(bigBlocks[0].Width * curScale);
            int TILE_SIZE_Y = (int)(bigBlocks[0].Height * curScale);
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
            var g = e.Graphics;

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            var screen = getActiveScreen();

            int WIDTH = screen.width;
            int HEIGHT = screen.height;
            int TILE_SIZE_X = (int)(bigBlocks[0].Width * curScale);
            int TILE_SIZE_Y = (int)(bigBlocks[0].Height * curScale);
            var visibleRect = UtilsGui.getVisibleRectangle(pnView, mapScreen);
            //ConfigScript.getScreenVertical() ? TILE_SIZE_Y : TILE_SIZE_X
            MapEditor.Render(e.Graphics, bigBlocks, visibleRect, screens, curActiveScreen, curScale, true, showAxis, ConfigScript.getScreenVertical() ? 0 : TILE_SIZE_X, ConfigScript.getScreenVertical() ? TILE_SIZE_X : 0, WIDTH, HEIGHT);

            if (!ConfigScript.getScreenVertical() && showNeiScreens && (curActiveScreen > 0) && screen.layers[0].showLayer)
            {
                renderNeighbornLine(g, curActiveScreen - 1, (WIDTH - 1), 0);
            }
            if (!ConfigScript.getScreenVertical() && showNeiScreens && (curActiveScreen < ConfigScript.screensOffset[0].recCount - 1) && screen.layers[0].showLayer)
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

        //generic
        private int curActiveVideoNo = 0;
        private int curActiveBigBlockNo = 0;
        private int curActiveBlockNo = 0;
        private int curActivePalleteNo = 0;

        private float curScale = 2;

        bool useStructs;
        TileStructure curTileStruct;

        MapViewType curViewType = MapViewType.ObjType;
        private bool dirty;
        private bool showNeiScreens;
        private bool showAxis;
        private bool showBrush;
        private Screen[] screens;

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

            var screen = getActiveScreen();

            int WIDTH = screen.width;
            int dx, dy;
            if (ConfigScript.getScreenVertical())
            {
                dy = ee.X / (int)(bigBlocks[0].Width * curScale);
                dx = ee.Y / (int)(bigBlocks[0].Height * curScale) - 1;
            }
            else
            {
                dx = ee.X / (int)(bigBlocks[0].Width * curScale) - 1;
                dy = ee.Y / (int)(bigBlocks[0].Height * curScale);
            }

            if (ea.Button == MouseButtons.Right)
            {
                if (dx == WIDTH || dx == -1)
                    return;
                int index = dy * WIDTH + dx;
                curActiveBlock = ConfigScript.getBigTileNoFromScreen(screens[curActiveScreen].layers[0].data, index);
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
            var screen = getActiveScreen();
            int WIDTH = screen.width;
            int dx, dy;
            if (ConfigScript.getScreenVertical())
            {
                dy = ee.X / (int)(bigBlocks[0].Width * curScale);
                dx = ee.Y / (int)(bigBlocks[0].Height * curScale) - 1;
            }
            else
            {
                dx = ee.X / (int)(bigBlocks[0].Width * curScale) - 1;
                dy = ee.Y / (int)(bigBlocks[0].Height * curScale);
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
                var activeScreens = screens;
                if (dx == WIDTH)
                {
                    if (curActiveScreen < ConfigScript.screensOffset[0].recCount - 1)
                    {
                        int index = dy * WIDTH;
                        ConfigScript.setBigTileToScreen(activeScreens[curActiveScreen + 1].layers[curActiveLayer].data, index, curActiveBlock);
                        dirty = true; updateSaveVisibility();
                    }
                }
                else if (dx == -1)
                {
                    if (curActiveScreen > 0)
                    {
                        int index = dy * WIDTH + (WIDTH - 1);
                        ConfigScript.setBigTileToScreen(activeScreens[curActiveScreen - 1].layers[0].data, index, curActiveBlock);
                        dirty = true; updateSaveVisibility();
                    }
                }
                else
                {
                    if (!useStructs)
                    {
                        int index = dy * WIDTH + dx;
                        if (index < activeScreens[curActiveScreen].layers[0].data.Length)
                        {
                            ConfigScript.setBigTileToScreen(activeScreens[curActiveScreen].layers[0].data, index, curActiveBlock);
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
            var screen = getActiveScreen();
            int WIDTH = screen.width;
            int HEIGHT = screen.height; ;
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
                        ConfigScript.setBigTileToScreen(screen.layers[0].data, index, no);
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

        private void saveScreens(Screen[] screensData)
        {
            ConfigScript.saveScreens(screensData);
        }

        private bool saveToFile()
        {
            saveScreens(screens);
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
            var screen = getActiveScreen();
            if (senderIsScale)
            {
                if (ConfigScript.getScreenVertical())
                    mapScreen.Size = new Size((int)(screen.height * bigBlocks[0].Width * curScale), (int)((screen.width + 2) * bigBlocks[0].Height * curScale));
                else
                    mapScreen.Size = new Size((int)((screen.width + 2) * bigBlocks[0].Width * curScale), (int)(screen.height * bigBlocks[0].Height * curScale));
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
            resetMapScreenSize();
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
                curScale = ConfigScript.isBuildScreenFromSmallBlocks() ? 1 : 2;
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
            mapScreen.Invalidate();
            blocksScreen.Invalidate();
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

        public int ScreenNo
        {
            get { return curActiveScreen; }
        }

        public float CurScale
        {
            get { return curScale; }
        }

        public Screen[] Screens
        {
            get { return screens; }
        }

        public Image[] BigBlocks
        {
            get { return bigBlocks; }
        }

        public Image[] getBigBlockImages()
        {
            return bigBlocks;
        }

        //warning! danger direct function. do not use it
        public void SetScreens(Screen[] screens)
        {
            this.screens = screens;
        }

        private void bttScale_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            float[] scaleFactors = { 0.25f, 0.5f, 1.0f, 2.0f, 3.0f, 4.0f };
            curScale = scaleFactors [bttScale.DropDownItems.IndexOf(e.ClickedItem)];
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
                var curScreen = screens[curActiveScreen];
                for (int i = 0; i < deltaX; i++)
                {
                    for (int j = 0; j < deltaY; j++)
                    {
                        int index = (selectionBeginY + j)*curScreen.width + (selectionBeginX + i);
                        tiles[j][i] = curScreen.layers[curActiveLayer].data[index];
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
            var screen = getActiveScreen();
            if (ConfigScript.getScreenVertical())
            {
                dy = e.X / (int)(bigBlocks[0].Width * curScale);
                dx = e.Y / (int)(bigBlocks[0].Height * curScale) - 1;
            }
            else
            {
                dx = e.X / (int)(bigBlocks[0].Width * curScale) - 1;
                dy = e.Y / (int)(bigBlocks[0].Height * curScale);
            }
        }

        private void bttShowLayer1_CheckedChanged(object sender, EventArgs e)
        {
            var screen = getActiveScreen(); //TODO: for all screens
            screen.layers[0].showLayer = bttShowLayer1.Checked;
            mapScreen.Invalidate();
        }

        private void bttShowLayer2_CheckedChanged(object sender, EventArgs e)
        {
            var screen = getActiveScreen(); //TODO: for all screens
            screen.layers[1].showLayer = bttShowLayer2.Checked;
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
                Globals.loadData(OpenFile.FileName, OpenFile.DumpName, OpenFile.ConfigName);
                resetControls();
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
            var screen = getActiveScreen();
            MapEditor.RenderAllBlocks(e.Graphics, blocksScreen, bigBlocks, bigBlocks[0].Width, bigBlocks[0].Height, visibleRect, curScale, curActiveBlock, showAxis);
        }

        private void blocksScreen_MouseDown(object sender, MouseEventArgs e)
        {
            var screen = getActiveScreen();
            var p = blocksScreen.PointToClient(Cursor.Position);
            int x = p.X, y = p.Y;
            int TILE_SIZE_X = (int)(bigBlocks[0].Width * CurScale);
            int TILE_SIZE_Y = (int)(bigBlocks[0].Height * CurScale);
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

        private void FormMain_Resize(object sender, EventArgs e)
        {
            splitContainer1.Width = this.Width - 21;
            splitContainer1.Height = this.Height - 81;
        }

        private void FormMain_LocationChanged(object sender, EventArgs e)
        {
            OnResize(e);
        }

        private void bttRunScript_Click(object sender, EventArgs e)
        {
            var f = new FormScript();
            f.setFormMain(this);
            f.Show();
        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            pnBlocks.Width = splitContainer1.Panel1.Width - pnElements.Width - 10;
            pnBlocks.Height = splitContainer1.Panel1.Height - 10;
            blocksScreen.Width = pnBlocks.Width;
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
            Text = String.Format("CAD Editor v5.0 - {0}", OpenFile.FileName);
        }

        private Screen getActiveScreen()
        {
            return screens[curActiveScreen];
        }
    }
}
