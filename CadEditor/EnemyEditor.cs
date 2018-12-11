using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace CadEditor
{
    public partial class EnemyEditor : Form
    {
        public EnemyEditor()
        {
            InitializeComponent();
        }

        private int curActiveBlock;

        private int curActiveLayout;
        private int curVideoNo;
        private int curBigBlockNo;
        private int curBlockNo;
        private int curPaletteNo;
        private float curScale = 1.0f;

        private int curActiveObjectListIndex;

        private bool bindToAxis;
        private bool useBigPictures;

        private ToolType curTool = ToolType.Create;

        private LevelLayerData curLevelLayerData;
        private List<ObjectList> objectLists = new List<ObjectList>();
        private bool dirty;
        private bool readOnly = false;

        private FormMain formMain;

        Screen[] screens;

        private Image[] objectSpritesBig;
        private Image[] bigBlocks;

        bool objectDragged;

        private void reloadLevelLayerData()
        {
            curActiveLayout = cbLayoutNo.SelectedIndex;
            curVideoNo = cbVideoNo.SelectedIndex;
            curBigBlockNo = cbBigBlockNo.SelectedIndex;
            curBlockNo = cbBlockNo.SelectedIndex;
            curPaletteNo = cbPaletteNo.SelectedIndex;
            curLevelLayerData = ConfigScript.getLayout(curActiveLayout);
        }

        private void setDirty()
        {
            dirty = true;
            bttSave.Enabled = ConfigScript.setObjectsFunc != null;
        }

        private void reloadBigBlocks()
        {
            if (!ConfigScript.usePicturesInstedBlocks)
            {
                if (ConfigScript.isUseSegaGraphics())
                {
                    bigBlocks = Globals.makeSegaBigBlocks(curVideoNo, curBigBlockNo, curPaletteNo, MapViewType.Tiles);
                }
                else if (ConfigScript.isUseGbGraphics())
                {
                    bigBlocks = Globals.makeGbBigBlocks(curVideoNo, curBigBlockNo, curPaletteNo, MapViewType.Tiles);
                }
                else
                {
                    bigBlocks = ConfigScript.videoNes.makeBigBlocks(curVideoNo, curBigBlockNo, curBlockNo, curPaletteNo, MapViewType.Tiles, MapViewType.Tiles, ConfigScript.getbigBlocksHierarchyCount() - 1);
                }
            }
        }

        private int calcScrNo(int noInLayout)
        {
            return curLevelLayerData.layer[noInLayout] - 1;
        }

        private void reloadLevel(bool reloadObjects)
        {
            reloadLevelLayerData();
            reloadBigBlocks();
            if (reloadObjects)
              setObjects();
            curActiveBlock = 0;
            btDelete.Enabled = false;
        }

        private void reloadScreens()
        {
            screens = ConfigScript.loadScreens();
        }

        private void cbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLayoutNo.SelectedIndex == -1)
                return;
            bool realReload = sender == cbLayoutNo;
            bool reloadObjects = realReload;
            if (!readOnly && realReload)
            {
                if (!UtilsGui.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
                    return;
            }
           
            curActiveLayout = cbLayoutNo.SelectedIndex;
            curScale = cbScale.SelectedIndex + 1; //TODO: normal scale factors;
            cbLayoutNo.Items.Clear();
            foreach (var lr in ConfigScript.getLevelRecs())
                cbLayoutNo.Items.Add(String.Format("{0} : 0x{1:X}  ({2}x{3})", lr.name, lr.layoutAddr, lr.width, lr.height));
            UtilsGui.setCbIndexWithoutUpdateLevel(cbLayoutNo, cbLevel_SelectedIndexChanged, curActiveLayout);

            if (sender == cbLayoutNo)
            {
                var g = getLevelRecForGameType().group;
                if (g != null)
                {
                    UtilsGui.setCbIndexWithoutUpdateLevel(cbVideoNo, cbLevel_SelectedIndexChanged, g.videoNo);
                    UtilsGui.setCbIndexWithoutUpdateLevel(cbBigBlockNo, cbLevel_SelectedIndexChanged, g.bigBlockNo);
                    UtilsGui.setCbIndexWithoutUpdateLevel(cbBlockNo, cbLevel_SelectedIndexChanged, g.blockNo);
                    UtilsGui.setCbIndexWithoutUpdateLevel(cbPaletteNo, cbLevel_SelectedIndexChanged, g.palNo);
                    reloadLevelLayerData();

                    cbGroup.SelectedIndex = -1;
                }

                reloadScreens();
            }
            reloadLevel(reloadObjects);
            resizeMapScreen();
            mapScreen.Invalidate();
        }

        private void EnemyEditor_Load(object sender, EventArgs e)
        {
            KeyPreview = true;

            if (ConfigScript.usePicturesInstedBlocks)
            {
                //TODO: set big blocks sizes from picture
                bigBlocks = UtilsGDI.setBlocksForPictures();
            }

            reloadPictures();
            fillObjPanel();

            UtilsGui.setCbItemsCount(cbVideoNo, ConfigScript.videoOffset.recCount);
            UtilsGui.setCbItemsCount(cbBigBlockNo, ConfigScript.bigBlocksOffsets[0].recCount);
            UtilsGui.setCbItemsCount(cbBlockNo, ConfigScript.blocksOffset.recCount);
            UtilsGui.setCbItemsCount(cbPaletteNo, ConfigScript.palOffset.recCount);
            UtilsGui.setCbItemsCount(cbScale, 2, 1);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbVideoNo, cbLevel_SelectedIndexChanged, formMain.curActiveVideoNo);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbBlockNo, cbLevel_SelectedIndexChanged, formMain.curActiveBlockNo);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbBigBlockNo, cbLevel_SelectedIndexChanged, formMain.curActiveBigBlockNo);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbPaletteNo, cbLevel_SelectedIndexChanged, formMain.curActivePalleteNo);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbTool, cbTool_SelectedIndexChanged);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbScale, cbLevel_SelectedIndexChanged, 1);
            cbLayoutNo.Items.Clear();
            foreach (var lr in ConfigScript.getLevelRecs())
            {
                cbLayoutNo.Items.Add(String.Format("{0}:0x{1:X} ({2}x{3})", lr.name, lr.layoutAddr, lr.width, lr.height));
            }
            UtilsGui.setCbIndexWithoutUpdateLevel(cbLayoutNo, cbLevel_SelectedIndexChanged);

            bttSave.Enabled = ConfigScript.setObjectsFunc != null;

            btSort.Visible = ConfigScript.sortObjectsFunc != null;

            UtilsGui.setCbItemsCount(cbBigObjectNo, 256, 0, true);
            cbLevel_SelectedIndexChanged(cbLayoutNo, new EventArgs());

            resizeMapScreen();

            cbGroup.Items.Clear();
            foreach (var g in ConfigScript.getGroups())
            {
                cbGroup.Items.Add(g.name);
            }
        }

        private Screen getActiveScreen()
        {
            for (int i = 0; i < curLevelLayerData.layer.Length; i++)
            {
                int scrNo = curLevelLayerData.layer[i] - 1; //Calculate size of all layout for first non zero screen in it
                if ((scrNo >= 0) && (scrNo < screens.Length))
                {
                    return screens[scrNo];
                }
            }
            return null;
        }

        private void resizeMapScreen()
        {
            reloadScreens();
            var screen = getActiveScreen();
            int blockWidth = bigBlocks[0].Width;
            int blockHeight = bigBlocks[0].Height;

            int scrWidth, scrHeight;
            if (ConfigScript.getScreenVertical())
            {
                scrWidth = (int)(screen.width * blockHeight * curScale);
                scrHeight = (int)(screen.height * blockWidth * curScale);
            }
            else
            {
                scrWidth = (int)(screen.width * blockWidth * curScale);
                scrHeight = (int)(screen.height * blockHeight * curScale);
            }

            int screensInWidth = curLevelLayerData.width;
            int screensInHeight = curLevelLayerData.height;
            if (ConfigScript.getScreenVertical())
            {
                mapScreen.Size = new Size(scrHeight * screensInWidth, scrWidth * screensInHeight);
            }
            else
            {
                mapScreen.Size = new Size(scrWidth * screensInWidth, scrHeight * screensInHeight);
            }
                
        }

        private void fillObjPanel()
        {
            objPanel.Controls.Clear();
            for (int i = 0; i < objectSprites.Images.Count; i++)
            {
                var but = new Button();
                but.FlatStyle = FlatStyle.Flat;
                but.Size = new Size(32, 32);
                but.ImageList = objectSprites;
                but.ImageIndex = i;
                but.Click += buttonObjClick;
                objPanel.Controls.Add(but);
            }
        }

        private void buttonObjClick(Object button, EventArgs e)
        {
            int index = ((Button)button).ImageIndex;
            lbActive.Text = String.Format("({0:X2})", index);
            curActiveBlock = index;
        }

        private void deleteSelected()
        {
            var activeObjectList = objectLists[curActiveObjectListIndex];
            var toRemove = new List<ObjectRec>();
            for (int i = 0; i < dgvObjects.SelectedRows.Count; i++)
            {
                int index = dgvObjects.SelectedRows[i].Index;
                if (index == -1)
                    continue;
                toRemove.Add(activeObjectList.objects[index]);
            }

            dgvObjects.DataSource = null;
            for (int i = 0; i < toRemove.Count; i++)
                activeObjectList.objects.Remove(toRemove[i]);

            fillObjectsDataGrid();

            btDelete.Enabled = false;
            mapScreen.Invalidate();
            setDirty();
        }

        private LevelRec getLevelRecForGameType()
        {
            return ConfigScript.getLevelRec(curActiveLayout);
        }

        private void setObjects()
        {
            objectLists = ConfigScript.getObjects(curActiveLayout);
            cbObjectList.Items.Clear();
            for (int i = 0; i < objectLists.Count; i++)
                cbObjectList.Items.Add(objectLists[i].name);
            fillObjectsDataGrid();
        }

        private void fillObjectsDataGrid()
        {
            var activeObjectList = objectLists[curActiveObjectListIndex];
            dgvObjects.DataSource = null;
            dgvObjects.Columns.Clear();
            dgvObjects.AutoGenerateColumns = true;
            dgvObjects.DataSource = activeObjectList.objects;

            if (dgvObjects.Columns.Count < ObjectRec.fieldCount)
            {
                //add icon column
                DataGridViewImageColumn iconColumn = new DataGridViewImageColumn();
                iconColumn.Name = "Icon";
                iconColumn.HeaderText = "Icon";
                dgvObjects.Columns.Insert(0, iconColumn);

                //add values from dictionary
                if (activeObjectList.objects.Count > 0)
                {
                    var defObject = activeObjectList.objects[0];
                    var dict = defObject.additionalData;
                    if (dict != null)
                    {
                        foreach (var k in dict.Keys)
                        {
                            var dictColumn = new DataGridViewTextBoxColumn();
                            dictColumn.Name = k;
                            dictColumn.HeaderText = k;
                            dgvObjects.Columns.Add(dictColumn);
                        }
                    }
                }
            }

            for (int i = 0; i < dgvObjects.Columns.Count; i++)
            {
                dgvObjects.Columns[i].Width = 35;
            }

            dgvObjects.Invalidate();
            lbObjectsCount.Text = String.Format("objects count: {0}/{1}", dgvObjects.RowCount, getLevelRecForGameType().objCount);
        }

        private Point mouseCoordToSxSyCoord(Point mouseCoord)
        {
            var screen = getActiveScreen();
            int blockWidth = bigBlocks[0].Width;
            int blockHeight = bigBlocks[0].Height;
            int scrWidth = (int)(screen.width * blockWidth * curScale);
            int scrHeight = (int)(screen.height * blockHeight * curScale);
            int scrX = mouseCoord.X / (ConfigScript.screenVertical ? scrHeight : scrWidth);
            int scrY = mouseCoord.Y / (ConfigScript.screenVertical ? scrWidth : scrHeight);
            return new Point(scrX, scrY);
        }

        private Point mouseCoordToCoordInsideScreen(Point mouseCoord)
        {
            var screen = getActiveScreen();
            int blockWidth = bigBlocks[0].Width;
            int blockHeight = bigBlocks[0].Height;
            int scrWidth = (int)(screen.width * blockWidth * curScale);
            int scrHeight = (int)(screen.height * blockHeight * curScale);
            int oX = mouseCoord.X % (ConfigScript.screenVertical ? scrHeight : scrWidth);
            int oY = mouseCoord.Y % (ConfigScript.screenVertical ? scrWidth : scrHeight);
            return new Point(oX, oY);
        }

        private void paintBack(Graphics g)
        {
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            var screen = getActiveScreen();
            int blockWidth = bigBlocks[0].Width;
            int blockHeight = bigBlocks[0].Height;

            int width = screen.width;
            int height = screen.height;
            int scrWidth = (int)(width * blockWidth * curScale);
            int scrHeight = (int)(height * blockHeight * curScale);

            int tileSizeX = (int)(bigBlocks[0].Width * curScale);
            int tileSizeY = (int)(bigBlocks[0].Height * curScale);

            for (int x = 0; x < curLevelLayerData.width; x++)
            {
                for (int y = 0; y < curLevelLayerData.height; y++)
                {
                    int noInLayout = y * curLevelLayerData.width + x;
                    int scrNo = calcScrNo(noInLayout);
                    if (scrNo < screens.Length && scrNo >= 0)
                    {

                        var visibleRect = UtilsGui.getVisibleRectangle(pnView, mapScreen);

                        int leftMargin = scrWidth * x;
                        int topMargin = scrHeight * y;
                          
                        MapEditor.render(g, screens, scrNo, new MapEditor.RenderParams
                        {
                            bigBlocks = bigBlocks,
                            visibleRect = visibleRect,
                            curScale = curScale,
                            showBlocksAxis = showAxis,
                            width = width,
                            height = height,
                            additionalRenderEnabled = formMain.additionalRenderEnabled,
                            leftMargin = leftMargin,
                            topMargin = topMargin
                        });

                        if (showScreenAxis)
                        {
                            g.DrawRectangle(new Pen(Brushes.Black,2.0f), new Rectangle(leftMargin, topMargin, tileSizeX * width, tileSizeY * height));
                        }
                    }
                    else
                    {
                        //g.FillRectangle(Brushes.Black, new Rectangle(0, 0, 512, 512));
                    }
                }
            }
        }

        private void mapScreen_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            paintBack(g);

            var screen = getActiveScreen();
            int blockWidth = bigBlocks[0].Width;
            int blockHeight = bigBlocks[0].Height;
            int scrWidth = (int)(screen.width * blockWidth * curScale);
            int scrHeight = (int)(screen.height * blockHeight * curScale);

            for (int objListIndex = 0; objListIndex < objectLists.Count; objListIndex++)
            {
                var activeObjectList = objectLists[objListIndex];

                var selectedRows = dgvObjects.SelectedRows;
                for (int i = 0; i < activeObjectList.objects.Count; i++)
                {
                    var curObject = activeObjectList.objects[i];
                    int leftMargin = (ConfigScript.getScreenVertical() ? scrHeight : scrWidth) * curObject.sx;
                    int topMargin = (ConfigScript.getScreenVertical() ? scrWidth : scrHeight) * curObject.sy;

                    bool inactive = objListIndex != curActiveObjectListIndex;
                    bool selected = !inactive && selectedRows.Cast<DataGridViewRow>().Any(r=>(r.Index == i));
                    if (!useBigPictures)
                        ConfigScript.drawObject(g, curObject, curActiveObjectListIndex, selected, curScale, objectSprites, inactive, leftMargin, topMargin);
                    else
                        ConfigScript.drawObjectBig(g, curObject, curActiveObjectListIndex, selected, curScale, objectSpritesBig, inactive, leftMargin, topMargin);
                }
            }
        }

        private bool saveToFile()
        {
            //TODO: return free space checking
            if (objectLists.Count == 1)
            {
                var lr = getLevelRecForGameType();
                int objCount = lr.objCount;
                if (objectLists[0].objects.Count > objCount)
                {
                    MessageBox.Show(String.Format("Too many objects in level ({0}). Maximum: {1}", objectLists[0].objects.Count, lr.objCount));
                    return false;
                }
            }
            try
            {
                ConfigScript.setObjects(curActiveLayout, objectLists);
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "Save error");
                return false;
            }

            dirty = !Globals.flushToFile();
            bttSave.Enabled = false;

            return !dirty;
        }

        private bool saveToJsonFile(string fname)
        {
            try
            {
                using (TextWriter f = new StreamWriter(fname))
                {
                    foreach (var obj in objectLists)
                    {
                        string json = JsonConvert.SerializeObject(obj);
                        f.WriteLine(json);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save error");
                return false;
            }

            return true;
        }

        private bool loadFromJsonFile(string fname)
        {
            objectLists.Clear();
            try
            {
                using (TextReader f = new StreamReader(fname))
                {
                    string line;
                    while ((line = f.ReadLine()) != null)
                    {
                        var obj = JsonConvert.DeserializeObject<ObjectList>(line);
                        objectLists.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load error");
                return false;
            }

            fillObjectsDataGrid();

            return true;
        }

        private void EnemyEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!readOnly && dirty)
            {
                DialogResult dr = MessageBox.Show("Level was changed. Do you want to save current level?", "Save", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                    saveToFile();
            }
        }

        private void returnCbLevelIndex()
        {
            UtilsGui.setCbIndexWithoutUpdateLevel(cbLayoutNo, cbLevel_SelectedIndexChanged, curActiveLayout);
            //save width/height
        }

        private void btSortUp_Click(object sender, EventArgs e)
        {
            bool canMoveUp = true;
            int repeatCount = ModifierKeys == Keys.Shift ? 10 : 1;
            var activeObjectList = objectLists[curActiveObjectListIndex];
            for (int count = 0; count < repeatCount && canMoveUp; count++)
            {
                var selInds = new List<int>();
                for (int i = 0; i < dgvObjects.SelectedRows.Count; i++)
                {
                    int ind = dgvObjects.SelectedRows[i].Index;
                    selInds.Add(ind);
                }

                selInds.Sort();
                foreach(var ind in selInds)
                {
                    var xchg = activeObjectList.objects[ind];
                    activeObjectList.objects[ind] = activeObjectList.objects[ind - 1];
                    activeObjectList.objects[ind - 1] = xchg;
                }
                fillObjectsDataGrid();

                //correct selection
                for (int i = 0; i < selInds.Count; i++)
                {
                    dgvObjects.Rows[selInds[i]].Selected = false;
                }
                for (int i = 0; i < selInds.Count; i++)
                {
                    dgvObjects.Rows[selInds[i] - 1].Selected = true;
                }
                canMoveUp = dgvObjects.SelectedRows.Cast<DataGridViewRow>().All(r => r.Index > 0);
            }
            setDirty();
            dgvObjects.Invalidate();
            //dgvObjects.FirstDisplayedScrollingRowIndex = dgvObjects.Rows[dgvObjects.SelectedRows[0].Index].Index;
        }

        private void btSortDown_Click(object sender, EventArgs e)
        {
            bool canMoveDown = true;
            int repeatCount = ModifierKeys == Keys.Shift ? 10 : 1;
            var activeObjectList = objectLists[curActiveObjectListIndex];
            for (int count = 0; count < repeatCount && canMoveDown; count++)
            {
                var selInds = new List<int>();
                for (int i = 0; i < dgvObjects.SelectedRows.Count; i++)
                {
                    int ind = dgvObjects.SelectedRows[i].Index;
                    selInds.Add(ind);
                }

                selInds.Sort((x, y) => x < y ? 1 : x > y ? -1 : 0);
                foreach (var ind in selInds)
                {
                    var xchg = activeObjectList.objects[ind];
                    activeObjectList.objects[ind] = activeObjectList.objects[ind + 1];
                    activeObjectList.objects[ind + 1] = xchg;
                }

                fillObjectsDataGrid();

                //correct selection
                for (int i = 0; i < selInds.Count; i++)
                {
                    dgvObjects.Rows[selInds[i]].Selected = false;
                }
                for (int i = 0; i < selInds.Count; i++)
                {
                    dgvObjects.Rows[selInds[i] + 1].Selected = true;
                }
                canMoveDown = dgvObjects.SelectedRows.Cast<DataGridViewRow>().All(r => r.Index < dgvObjects.RowCount - 1);
            }
            setDirty();
            dgvObjects.Invalidate();
            //dgvObjects.FirstDisplayedScrollingRowIndex = dgvObjects.Rows[dgvObjects.SelectedRows[0].Index].Index;
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            deleteSelected();
        }

        private void reloadPictures()
        {
            Utils.loadEnemyPictures(ref objectSprites, out objectSpritesBig);
        }

        private void cbTool_SelectedIndexChanged(object sender, EventArgs e)
        {
            curTool = (ToolType)cbTool.SelectedIndex;
        }

        private void btSort_Click(object sender, EventArgs e)
        {
            var activeObjectList = objectLists[curActiveObjectListIndex];
            ConfigScript.sortObjects(curActiveLayout, curActiveObjectListIndex, activeObjectList.objects);
            fillObjectsDataGrid();
        }

        private void mapScreen_MouseDown(object sender, MouseEventArgs e)
        {
            var mouseCoord = new Point(e.X, e.Y);
            Point coord = mouseCoordToSxSyCoord(mouseCoord);
            Point ocoord = mouseCoordToCoordInsideScreen(mouseCoord);
            int sx = coord.X, sy = coord.Y;
            int x = (int)(ocoord.X / curScale);
            int y = (int)(ocoord.Y / curScale);

            if (e.Button == MouseButtons.Right)
            {
                cbTool.SelectedIndex = (int)(ToolType.Select);
            }

            if (curTool == ToolType.Select)
            {
                if (ModifierKeys != Keys.Shift && ModifierKeys != Keys.Control)
                {
                    for (int i = 0; i < dgvObjects.SelectedRows.Count; i++)
                    {
                        dgvObjects.SelectedRows[i].Selected = false;
                    }
                }

                var activeObjectList = objectLists[curActiveObjectListIndex]; //TODO: all
                for (int i = 0; i < activeObjectList.objects.Count; i++)
                {
                    if (!useBigPictures)
                    {
                        var obj = activeObjectList.objects[i];
                        if ((obj.sx == sx) && (obj.sy == sy) && (Math.Abs(obj.x - x) < 8) && (Math.Abs(obj.y - y) < 8))
                        {
                            dgvObjects.Rows[i].Selected = !dgvObjects.Rows[i].Selected;
                        }
                    }
                    else
                    {
                        var obj = activeObjectList.objects[i];
                        if ((obj.sx == sx) && (obj.sy == sy))
                        {
                            if (isMouseInside(obj, x, y, objectSpritesBig))
                            {
                                dgvObjects.Rows[i].Selected = !dgvObjects.Rows[i].Selected;
                            }
                        }
                    }
                }
            }
            objectDragged = true;
            oldX = x;
            oldY = y;
        }

        private bool isMouseInside(ObjectRec obj, int x, int y, Image[] sprites)
        {
            var bigObject = sprites[obj.type];
            return (x > (obj.x - bigObject.Width/2)) && (x < obj.x + bigObject.Width/2) && (y > (obj.y - bigObject.Height/2)) && (y < obj.y + bigObject.Height/2);
        }

        private int oldX;
        private int oldY;
        private bool showAxis;
        private bool showScreenAxis;

        private void mapScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (!objectDragged)
                return;
            var mouseCoord = new Point(e.X, e.Y);
            Point coord = mouseCoordToSxSyCoord(mouseCoord);
            Point ocoord = mouseCoordToCoordInsideScreen(mouseCoord);
            int sx = coord.X, sy = coord.Y;
            int x = (int)(ocoord.X / curScale);
            int y = (int)(ocoord.Y / curScale);

            int dx = x - oldX;
            int dy = y - oldY;

            var screen = getActiveScreen();
            int coordXCount = screen.width * 32;
            int coordYCount = screen.height * 32;
            int minCoordX = 0;
            int minCoordY = 0;
            if (!ConfigScript.getScreenVertical())
            {
                if (x >= coordXCount || y >= coordYCount || x < minCoordX || y < minCoordY)
                    return;
            }
            else
            {
                if (y >= coordXCount || x >= coordYCount || y < minCoordX || x < minCoordY)
                    return;
            }

            if ((sx >= screen.width || (sy >= screen.height)))
            {
                return;
            }

            if (curTool == ToolType.Select)
            {
                var activeObjectList = objectLists[curActiveObjectListIndex]; //TODO: all
                for (int i = 0; i < dgvObjects.SelectedRows.Count; i++)
                {
                    int selIndex = dgvObjects.SelectedRows[i].Index;
                    var obj = activeObjectList.objects[selIndex];

                    obj.sx = sx;
                    obj.sy = sy;

                    if (bindToAxis)
                    {
                        obj.x = (x / 8) * 8;
                        obj.y = (y / 8) * 8;
                    }
                    else
                    {
                        obj.x += dx;
                        obj.y += dy;
                    }
                    activeObjectList.objects[selIndex] = obj;
                }
            }
            oldX = x;
            oldY = y;
            setDirty();
            mapScreen.Invalidate();
        }

        private void mapScreen_MouseUp(object sender, MouseEventArgs e)
        {
            dragEnd();
        }

        private void mapScreen_MouseLeave(object sender, EventArgs e)
        {
            dragEnd();
        }

        private void dragEnd()
        {
            if (curTool == ToolType.Select)
            {
                objectDragged = false;
                dgvObjects_SelectionChanged(dgvObjects, new EventArgs());
            }
            dgvObjects.Invalidate();
        }

        private void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            var mouseCoord = new Point(e.X, e.Y);
            Point coord = mouseCoordToSxSyCoord(mouseCoord);
            Point ocoord = mouseCoordToCoordInsideScreen(mouseCoord);
            int type = curActiveBlock;
            int sx = coord.X, sy = coord.Y;
            int x = (int)(ocoord.X / curScale);
            int y = (int)(ocoord.Y / curScale);

            if (curTool == ToolType.Create)
            {
                var screen = getActiveScreen();
                int coordXCount = screen.width * 32;
                int coordYCount = screen.height * 32;
                int minCoordX = 0;
                int minCoordY = 0;

                if (!ConfigScript.getScreenVertical())
                {
                    if (x >= coordXCount || y >= coordYCount || x < minCoordX || y < minCoordY)
                        return;
                }
                else
                {
                    if (y >= coordXCount || x >= coordYCount || y < minCoordX || x < minCoordY)
                        return;
                }
                setDirty();
                if (bindToAxis)
                {
                    x = (x / 8) * 8;
                    y = (y / 8) * 8;
                }
                var dictionary = ConfigScript.getObjectDictionary(curActiveObjectListIndex, type);
                var obj = new ObjectRec(type, sx, sy, x, y, dictionary);

                int insertPos = dgvObjects.SelectedRows.Count > 0 ? dgvObjects.SelectedRows[0].Index + 1 : dgvObjects.RowCount;
                var activeObjectList = objectLists[curActiveObjectListIndex];
                activeObjectList.objects.Insert(insertPos, obj);

                dgvObjects.DataSource = null;
                fillObjectsDataGrid();
            }
            else if (curTool == ToolType.Delete)
            {
                var activeObjectList = objectLists[curActiveObjectListIndex]; //TODO: all
                for (int i = activeObjectList.objects.Count - 1; i >= 0; i--)
                {
                    var obj = activeObjectList.objects[i];
                    if ((obj.sx == sx) && (obj.sy == sy) && (Math.Abs(obj.x - x) < 8) && (Math.Abs(obj.y - y) < 8))
                    {
                        if (MessageBox.Show("Do you really want to delete object?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes)
                            return;
                        setDirty();
                        dgvObjects.DataSource = null;
                        activeObjectList.objects.RemoveAt(i);
                        fillObjectsDataGrid();
                        break;
                    }
                }
            }
            mapScreen.Invalidate();
        }

        public void setFormMain(FormMain f)
        {
            formMain = f;
        }

        private void cbUseBigPictures_CheckedChanged(object sender, EventArgs e)
        {
            useBigPictures = cbUseBigPictures.Checked;
            updatePanelsVisible();
            mapScreen.Invalidate();
        }

        private void updatePanelsVisible()
        {
            objPanel.Visible = !useBigPictures;
            pnBigObjects.Visible = useBigPictures;
            cbBigObjectNo.SelectedIndex = 0;
            curActiveBlock = 0;
            pbBigObject.Image = objectSpritesBig[curActiveBlock];
            lbActive.Text = String.Format("({0:X2})", curActiveBlock);
        }

        private void cbBigObjectNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            curActiveBlock = cbBigObjectNo.SelectedIndex;
            pbBigObject.Image = objectSpritesBig[curActiveBlock];
            lbActive.Text = String.Format("({0:X2})", curActiveBlock);
        }

        private void cbObjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selIndex = cbObjectList.SelectedIndex;
            if ((selIndex == -1) || (selIndex == curActiveObjectListIndex))
            {
                return;
            }
            curActiveObjectListIndex = cbObjectList.SelectedIndex;
            fillObjectsDataGrid();
            mapScreen.Invalidate();
        }

        private void dgvObjects_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                var row = dgvObjects.Rows[e.RowIndex];
                int cell0 = Convert.ToInt32(row.Cells[1].Value);

                if (row.Cells.Count >= ObjectRec.fieldCount)
                {
                    //update icon
                    if ((cell0 >= 0) && (cell0 < objectSprites.Images.Count))
                    {
                        row.Cells[0].Value = objectSprites.Images[cell0];
                    }

                    //update dictionary
                    try
                    {
                        var activeObjectList = objectLists[curActiveObjectListIndex];
                        var dict = activeObjectList.objects[e.RowIndex].additionalData;
                        if (dict != null)
                        {
                            foreach (var k in dict.Keys)
                            {
                                row.Cells[k].Value = dict[k];
                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }

            if (e.Value != null)
            {
                long value;
                if (long.TryParse(e.Value.ToString(), out value))
                {
                    e.Value = "0x" + value.ToString("X");
                    e.FormattingApplied = true;
                }
            }
        }

        private void dgvObjects_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (e != null && e.Value != null && e.DesiredType.Equals(typeof(int)))
            {
                var s = e.Value.ToString();
                if (s.StartsWith("0x") || s.StartsWith("0X"))
                {
                    try
                    {
                        int hex = Convert.ToInt32(s, 16);
                        e.Value = hex;
                        e.ParsingApplied = true;
                    }
                    catch
                    {
                        // Not a Valid Hexadecimal
                    }
                }
            }

            if (e != null && e.Value != null)
            {
                try
                {
                    //check dict fields
                    if (e.ColumnIndex >= ObjectRec.fieldCount)
                    {
                        var activeObjectList = objectLists[curActiveObjectListIndex];
                        var dict = activeObjectList.objects[e.RowIndex].additionalData;
                        if (dict != null)
                        {
                            var v = e.Value.ToString();
                            bool isHex = (v.StartsWith("0x") || v.StartsWith("0X"));
                            int intValue = isHex ? Convert.ToInt32(v, 16) : Convert.ToInt32(v, 10);
                            dict[dgvObjects.Columns[e.ColumnIndex].Name] = intValue;
                        }
                    }
                }
                catch
                {
                    //error at parse dictionary
                }
            }
        }

        private void dgvObjects_SelectionChanged(object sender, EventArgs e)
        {
            bool selectedZero = dgvObjects.SelectedRows.Count == 0;
            btDelete.Enabled = !selectedZero;
            btSortDown.Enabled = false;
            btSortUp.Enabled = false;

            if (!selectedZero)
            {
                btSortDown.Enabled = dgvObjects.SelectedRows.Cast<DataGridViewRow>().All(r => r.Index < dgvObjects.RowCount - 1);
                btSortUp.Enabled = dgvObjects.SelectedRows.Cast<DataGridViewRow>().All(r => r.Index > 0);
            }

            mapScreen.Invalidate();
        }

        private void dgvObjects_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                deleteSelected();
            }
        }

        private void tbbSave_Click(object sender, EventArgs e)
        {
            saveToFile();
        }

        private void bttExport_Click(object sender, EventArgs e)
        {
            if (sfJson.ShowDialog() == DialogResult.OK)
            {
                saveToJsonFile(sfJson.FileName);
            }
        }

        private void bttImport_Click(object sender, EventArgs e)
        {
            if (ofJson.ShowDialog() == DialogResult.OK)
            {
                loadFromJsonFile(ofJson.FileName);
            }
        }

        private void bttAlign8_Click(object sender, EventArgs e)
        {
            bindToAxis = bttAlign8.Checked;
        }

        private void EnemyEditor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                deleteSelected();
            }
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
        }

        private void bttAxis_Click(object sender, EventArgs e)
        {
            showAxis = bttAxis.Checked;
            showScreenAxis = bttAxisScreens.Checked;
            mapScreen.Invalidate();
        }
    }
}
