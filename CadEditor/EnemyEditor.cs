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

        private int curActiveBlock = 0;

        private int curActiveLayout = 0;
        private int curVideoNo = 0;
        private int curBigBlockNo = 0;
        private int curBlockNo = 0;
        private int curPaletteNo = 0;
        private float curScale = 1.0f;

        private int curActiveObjectListIndex = 0;

        private bool bindToAxis = false;
        private bool useBigPictures = false;

        private ToolType curTool = ToolType.Create;

        private LevelLayerData curLevelLayerData = new LevelLayerData();
        private List<ObjectList> objectLists = new List<ObjectList>();
        private bool dirty = false;
        private bool readOnly = false;

        private FormMain formMain;

        private Image[] objectSpritesBig;
        private Image[] bigBlocks;
            
        const int MAX_SIZE = 64;

        bool objectDragged = false;

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
            bttSave.Enabled = bttSave.Enabled = ConfigScript.setObjectsFunc != null;
        }

        private int findStartPosition()
        {
            int w = curLevelLayerData.width;
            int h = curLevelLayerData.height;
            return w * (h - 1);
        }

        private void reloadBigBlocks()
        {
            if (!ConfigScript.usePicturesInstedBlocks)
            {
                if (ConfigScript.isUseSegaGraphics())
                    bigBlocks = makeSegaBigBlocks();
                else
                    bigBlocks = ConfigScript.videoNes.makeBigBlocks(curVideoNo, curBigBlockNo, curBlockNo, curPaletteNo, MapViewType.Tiles, MapViewType.Tiles, ConfigScript.getbigBlocksHierarchyCount()-1);
            }
        }

        //copy-paste
        private Image[] makeSegaBigBlocks()
        {
            byte[] mapping = ConfigScript.getSegaMapping(curBigBlockNo);
            byte[] videoTiles = ConfigScript.getVideoChunk(curVideoNo);
            byte[] pal = ConfigScript.getPal(curPaletteNo);
            int count = ConfigScript.getBigBlocksCount(ConfigScript.getbigBlocksHierarchyCount()-1);
            return ConfigScript.videoSega.makeBigBlocks(mapping, videoTiles, pal, count, MapViewType.Tiles);
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
            foreach (var lr in ConfigScript.levelRecs)
                cbLayoutNo.Items.Add(String.Format("{0}:0x{1:X} ({2}x{3})", lr.name, lr.layoutAddr, lr.width, lr.height));
            UtilsGui.setCbIndexWithoutUpdateLevel(cbLayoutNo, cbLevel_SelectedIndexChanged, curActiveLayout);

            reloadLevel(reloadObjects);
            resizeMapScreen();
            mapScreen.Invalidate();
        }

        private void EnemyEditor_Load(object sender, EventArgs e)
        {
            if (ConfigScript.usePicturesInstedBlocks)
            {
                bigBlocks = UtilsGDI.setBlocksForPictures(2, 32,32, MapViewType.Tiles);
            }

            reloadPictures();
            fillObjPanel();

            UtilsGui.setCbItemsCount(cbVideoNo, ConfigScript.videoOffset.recCount);
            UtilsGui.setCbItemsCount(cbBigBlockNo, ConfigScript.bigBlocksOffsets[0].recCount);
            UtilsGui.setCbItemsCount(cbBlockNo, ConfigScript.blocksOffset.recCount);
            UtilsGui.setCbItemsCount(cbPaletteNo, ConfigScript.palOffset.recCount);
            UtilsGui.setCbItemsCount(cbScale, 2, 1);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbVideoNo, cbLevel_SelectedIndexChanged, formMain.CurActiveVideoNo);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbBlockNo, cbLevel_SelectedIndexChanged, formMain.CurActiveBlockNo);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbBigBlockNo, cbLevel_SelectedIndexChanged, formMain.CurActiveBigBlockNo);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbPaletteNo, cbLevel_SelectedIndexChanged, formMain.CurActivePalleteNo);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbTool, cbTool_SelectedIndexChanged);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbScale, cbLevel_SelectedIndexChanged, 1);
            cbLayoutNo.Items.Clear();
            foreach (var lr in ConfigScript.levelRecs)
                cbLayoutNo.Items.Add(String.Format("{0}:0x{1:X} ({2}x{3})", lr.name, lr.layoutAddr, lr.width, lr.height));
            UtilsGui.setCbIndexWithoutUpdateLevel(cbLayoutNo, cbLevel_SelectedIndexChanged);

            bttSave.Enabled = ConfigScript.setObjectsFunc != null;

            btSort.Visible = ConfigScript.sortObjectsFunc != null;
            resizeMapScreen();

            UtilsGui.setCbItemsCount(cbBigObjectNo, 256, 0, true);
            cbLevel_SelectedIndexChanged(cbLayoutNo, new EventArgs());
        }

        private void resizeMapScreen()
        {
            int blockWidth = formMain.Layers[0].blockWidth;
            int blockHeight = formMain.Layers[0].blockHeight;
            int scrLevelNo = getLevelRecForGameType().levelNo;

            int scrWidth = (int)(ConfigScript.getScreenWidth(scrLevelNo) * blockWidth * curScale);
            int scrHeight = (int)(ConfigScript.getScreenHeight(scrLevelNo) * blockHeight * curScale);

            int screensInWidth = curLevelLayerData.width;
            int screensInHeight = curLevelLayerData.height;
            if (!ConfigScript.getScreenVertical())
                mapScreen.Size = new Size(scrWidth * screensInWidth, scrHeight * screensInHeight);
            else
                mapScreen.Size = new Size(scrHeight * screensInWidth, scrWidth * screensInHeight);
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
                but.Click += new EventHandler(buttonObjClick);
                objPanel.Controls.Add(but);
            }
        }

        private void buttonObjClick(Object button, EventArgs e)
        {
            int index = ((Button)button).ImageIndex;
            lbActive.Text = String.Format("({0:X2})", index);
            curActiveBlock = index;
        }

        private bool isUndefindedObj(ObjectRec obj)
        {
            int scrNo = coordToScreenNo(obj);
            if (scrNo == -1)
                return false;
           
           return (obj.type != 0xFF) && (curLevelLayerData.layer[scrNo] == 0);
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
            dgvObjects.AutoGenerateColumns = true;
            var activeObjectList = objectLists[curActiveObjectListIndex];
            dgvObjects.DataSource = activeObjectList.objects;

            if (dgvObjects.Columns.Count < ObjectRec.FIELD_COUNT)
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

    
        private int coordToScreenNo(ObjectRec obj)
        {
            int index = obj.sy * curLevelLayerData.width + obj.sx;
            if (index < 0 || index >= curLevelLayerData.width * curLevelLayerData.height)
                return -1;
            return index;
        }

        private String makeStringForObject(ObjectRec obj)
        {
            int screenNo = coordToScreenNo(obj);
            String ans = String.Format("{0} <{1:X}>", obj.ToString(), screenNo);
            return ans;
        }

        private Point mouseCoordToSxSyCoord(Point mouseCoord)
        {
            int blockWidth = formMain.Layers[0].blockWidth;
            int blockHeight = formMain.Layers[0].blockHeight;
            int scrLevelNo = getLevelRecForGameType().levelNo;
            int scrWidth = (int)(ConfigScript.getScreenWidth(scrLevelNo) * blockWidth * curScale);
            int scrHeight = (int)(ConfigScript.getScreenHeight(scrLevelNo) * blockHeight * curScale);
            int scrX = mouseCoord.X / (ConfigScript.screenVertical ? scrHeight : scrWidth);
            int scrY = mouseCoord.Y / (ConfigScript.screenVertical ? scrWidth : scrHeight);
            return new Point(scrX, scrY);
        }

        private Point mouseCoordToCoordInsideScreen(Point mouseCoord)
        {
            int blockWidth = formMain.Layers[0].blockWidth;
            int blockHeight = formMain.Layers[0].blockHeight;
            int scrLevelNo = getLevelRecForGameType().levelNo;
            int scrWidth = (int)(ConfigScript.getScreenWidth(scrLevelNo) * blockWidth * curScale);
            int scrHeight = (int)(ConfigScript.getScreenHeight(scrLevelNo) * blockHeight * curScale);
            int oX = mouseCoord.X % (ConfigScript.screenVertical ? scrHeight : scrWidth);
            int oY = mouseCoord.Y % (ConfigScript.screenVertical ? scrWidth : scrHeight);
            return new Point(oX, oY);
        }

        private void paintBack(Graphics g)
        {
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            int blockWidth = formMain.Layers[0].blockWidth;
            int blockHeight = formMain.Layers[0].blockHeight;
            int scrLevelNo = getLevelRecForGameType().levelNo;

            int width = ConfigScript.getScreenWidth(scrLevelNo);
            int height = ConfigScript.getScreenHeight(scrLevelNo);
            int scrWidth = (int)(width * blockWidth * curScale);
            int scrHeight = (int)(height * blockHeight * curScale);

            for (int x = 0; x < curLevelLayerData.width; x++)
            {
                for (int y = 0; y < curLevelLayerData.height; y++)
                {
                    int noInLayout = y * curLevelLayerData.width + x;
                    int scrNo = calcScrNo(noInLayout);
                    if (scrNo < formMain.Layers[0].screens.Length && scrNo >= 0)
                    {

                        var visibleRect = UtilsGui.getVisibleRectangle(pnView, mapScreen);
                        int leftMargin = scrWidth * x;
                        int topMargin = scrHeight * y;
                        MapEditor.Render(g, bigBlocks, visibleRect, formMain.Layers, scrNo, curScale, false, formMain.ShowAxis, leftMargin, topMargin, width, height);
                        //ConfigScript.renderToMainScreen(g, (int)curScale);
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

            int blockWidth = formMain.Layers[0].blockWidth;
            int blockHeight = formMain.Layers[0].blockHeight;
            int scrLevelNo = getLevelRecForGameType().levelNo;
            int scrWidth = (int)(ConfigScript.getScreenWidth(scrLevelNo) * blockWidth * curScale);
            int scrHeight = (int)(ConfigScript.getScreenHeight(scrLevelNo) * blockHeight * curScale);

            for (int objListIndex = 0; objListIndex < objectLists.Count; objListIndex++)
            {
                var activeObjectList = objectLists[objListIndex];

                var selectedRows = dgvObjects.SelectedRows;
                for (int i = 0; i < activeObjectList.objects.Count; i++)
                {
                    var curObject = activeObjectList.objects[i];
                    int leftMargin = scrWidth * curObject.sx;
                    int topMargin = scrHeight * curObject.sy;

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
            var romFname = OpenFile.FileName;
            LevelRec lr = getLevelRecForGameType();
            //TODO: return free space checking
            /*int addrBase = lr.objectsBeginAddr;
            int objCount = lr.objCount;
            if (activeObjectList.objects.Count > objCount)
            {
                MessageBox.Show(String.Format("Too many objects in level ({0}). Maximum: {1}", activeObjectList.objects.Count, lr.objCount));
                return false;
            }*/
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
            LevelRec lr = getLevelRecForGameType();
            try
            {
                using (TextWriter f = new StreamWriter(fname))
                {
                    for (int i = 0; i < objectLists.Count; i++)
                    {
                        var obj = objectLists[i];
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
            int repeatCount = (Control.ModifierKeys == Keys.Shift) ? 10 : 1;
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
            int repeatCount = (Control.ModifierKeys == Keys.Shift) ? 10 : 1;
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
            Utils.loadEnemyPictures(ref objectSprites, ref objectSpritesBig);
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
            int type = curActiveBlock;
            int sx = coord.X, sy = coord.Y;
            int x = (int)(ocoord.X / curScale);
            int y = (int)(ocoord.Y / curScale);
            if (curTool == ToolType.Select)
            {
                if (Control.ModifierKeys != Keys.Shift && Control.ModifierKeys != Keys.Control)
                {
                    for (int i = 0; i < dgvObjects.SelectedRows.Count; i++)
                    {
                        dgvObjects.SelectedRows[i].Selected = false;
                    }
                }

                var activeObjectList = objectLists[curActiveObjectListIndex]; //TODO: all
                for (int i = 0; i < activeObjectList.objects.Count; i++)
                {
                    var obj = activeObjectList.objects[i];
                    if ((obj.sx == sx) && (obj.sy == sy) && (Math.Abs(obj.x - x) < 8) && (Math.Abs(obj.y - y) < 8))
                    {
                        dgvObjects.Rows[i].Selected = !dgvObjects.Rows[i].Selected;
                    }
                }
            }
            objectDragged = true;
            oldX = x;
            oldY = y;
        }

        private int oldX;
        private int oldY;

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

            int scrLevelNo = getLevelRecForGameType().levelNo;
            int coordXCount = ConfigScript.getScreenWidth(scrLevelNo) * 32;
            int coordYCount = ConfigScript.getScreenHeight(scrLevelNo) * 32;
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
                int scrLevelNo = getLevelRecForGameType().levelNo;
                int coordXCount = ConfigScript.getScreenWidth(scrLevelNo) * 32;
                int coordYCount = ConfigScript.getScreenHeight(scrLevelNo) * 32;
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
            if (cbObjectList.SelectedIndex == -1)
                return;
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

                if (row.Cells.Count >= ObjectRec.FIELD_COUNT)
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
                long value = 0;
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
                        int hex = (int)Convert.ToInt32(s.ToString(), 16);
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
                    if (e.ColumnIndex >= ObjectRec.FIELD_COUNT)
                    {
                        var activeObjectList = objectLists[curActiveObjectListIndex];
                        var dict = activeObjectList.objects[e.RowIndex].additionalData;
                        if (dict != null)
                        {
                            var v = e.Value.ToString();
                            bool isHex = (v.StartsWith("0x") || v.StartsWith("0X"));
                            int intValue = isHex ? (int)Convert.ToInt32(v.ToString(), 16) : (int)Convert.ToInt32(v.ToString(), 10);
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
                btSortDown.Enabled = dgvObjects.SelectedRows.Cast<DataGridViewRow>().All(r => r.Index < dgvObjects.RowCount - 1); ;
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
    }
}
