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
        private int curActiveScreen = 0;

        private int curActiveLayout = 0;
        private int curVideoNo = 0x90;
        private int curBigBlockNo = 0;
        private int curBlockNo = 0;
        private int curPaletteNo = 0;
        private int curWidth = 1;
        private int curHeight = 1;
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
        ComboBox[] cbDatas;
        Label[] lbDatas;

        private Image[] objectSpritesBig;

        const int MAX_SIZE = 64;

        bool objectDragged = false;

        //render back
        private int[][] screens = null;

        private void reloadLevelLayerData(bool resetScreenPos)
        {
            curWidth = ConfigScript.getLevelWidth(curActiveLayout);
            curHeight = ConfigScript.getLevelHeight(curActiveLayout);
            curActiveLayout = cbLayoutNo.SelectedIndex;
            curVideoNo = cbVideoNo.SelectedIndex + 0x90;
            curBigBlockNo = cbBigBlockNo.SelectedIndex;
            curBlockNo = cbBlockNo.SelectedIndex;
            curPaletteNo = cbPaletteNo.SelectedIndex;
            curActiveScreen = cbScreenNo.SelectedIndex;
            curLevelLayerData = (ConfigScript.getLayoutFunc != null) ? ConfigScript.getLayout(curActiveLayout) : Utils.getLayoutLinear(curActiveLayout);

            if (resetScreenPos)
            {
                cbScreenNo.Items.Clear();
                for (int i = 0; i < curLevelLayerData.width * curLevelLayerData.height; i++)
                    cbScreenNo.Items.Add(String.Format("{0:X}", i));
                cbScreenNo.SelectedIndex = findStartPosition();
            }
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
                bigBlocks.Images.Clear();
                Image[] bigImages;
                if (ConfigScript.isUseSegaGraphics())
                  bigImages = makeSegaBigBlocks();
                else
                  bigImages = ConfigScript.videoNes.makeBigBlocks(curVideoNo, curBigBlockNo, curBlockNo, curPaletteNo, MapViewType.Tiles, curScale, curScale, MapViewType.Tiles, formMain.ShowAxis);
                bigBlocks.Images.AddRange(bigImages);
            }
        }

        //copy-paste
        private Image[] makeSegaBigBlocks()
        {
            byte[] mapping = ConfigScript.getSegaMapping(curBigBlockNo);
            byte[] videoTiles = ConfigScript.getVideoChunk(curVideoNo);
            byte[] pal = ConfigScript.getPal(curPaletteNo);
            int count = ConfigScript.getBigBlocksCount();
            return ConfigScript.videoSega.makeBigBlocks(mapping, videoTiles, pal, count, curScale, MapViewType.Tiles, formMain.ShowAxis);
        }

        private int calcScrNo()
        {
            int scrNo = curLevelLayerData.layer[curActiveScreen];
            if (cbPlus256.Checked)
                scrNo += 256;
            return scrNo - 1;
        }

        private void reloadLevel(bool reloadObjects)
        {
            reloadLevelLayerData(reloadObjects);
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

            //reload screens
            screens = Utils.setScreens(getLevelRecForGameType().levelNo);
            resetObjCheckBoxes();

            reloadLevel(reloadObjects);
            resizeMapScreen();
            mapScreen.Invalidate();
        }

        private void EnemyEditor_Load(object sender, EventArgs e)
        {
            screens = Utils.setScreens(getLevelRecForGameType().levelNo);
            if (ConfigScript.usePicturesInstedBlocks)
            {
                UtilsGDI.setBlocks(bigBlocks, 2, 32,32, MapViewType.Tiles, formMain.ShowAxis);
            }

            cbDatas = new ComboBox[] { cbD0, cbD1, cbD2, cbD3, cbD4, cbD5 };
            lbDatas = new Label[]    { lbD0, lbD1, lbD2, lbD3, lbD4, lbD5 };

            reloadPictures();
            fillObjPanel();

            resetObjCheckBoxes();

            UtilsGui.setCbItemsCount(cbVideoNo, ConfigScript.videoOffset.recCount);
            UtilsGui.setCbItemsCount(cbBigBlockNo, ConfigScript.bigBlocksOffset.recCount);
            UtilsGui.setCbItemsCount(cbBlockNo, ConfigScript.blocksOffset.recCount);
            UtilsGui.setCbItemsCount(cbPaletteNo, ConfigScript.palOffset.recCount);
            UtilsGui.setCbItemsCount(cbScale, 2, 1);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbVideoNo, cbLevel_SelectedIndexChanged, formMain.CurActiveVideoNo - 0x90);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbBlockNo, cbLevel_SelectedIndexChanged, formMain.CurActiveBlockNo);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbBigBlockNo, cbLevel_SelectedIndexChanged, formMain.CurActiveBigBlockNo);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbPaletteNo, cbLevel_SelectedIndexChanged, formMain.CurActivePalleteNo);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbTool, cbTool_SelectedIndexChanged);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbScale, cbLevel_SelectedIndexChanged, 1);
            cbLayoutNo.Items.Clear();
            foreach (var lr in ConfigScript.levelRecs)
                cbLayoutNo.Items.Add(String.Format("{0}:0x{1:X} ({2}x{3})", lr.name, lr.layoutAddr, lr.width, lr.height));
            UtilsGui.setCbIndexWithoutUpdateLevel(cbLayoutNo, cbLevel_SelectedIndexChanged);

            readOnly = ConfigScript.setObjectsFunc == null;
            btSave.Enabled = !readOnly;
            lbReadOnly.Visible = readOnly;

            btSort.Visible = ConfigScript.sortObjectsFunc != null;
            resizeMapScreen();

            UtilsGui.setCbItemsCount(cbBigObjectNo, 256, 0, true);
            cbLevel_SelectedIndexChanged(cbLayoutNo, new EventArgs());
        }

        private void resetObjCheckBoxes()
        {
            int coordXCount = ConfigScript.getScreenWidth(getLevelRecForGameType().levelNo) * 32;
            int coordYCount = ConfigScript.getScreenHeight(getLevelRecForGameType().levelNo) * 32;
            int objType = (ConfigScript.getMaxObjType() != -1) ? ConfigScript.getMaxObjType() : 256;
            int minCoordX = 0;
            int minCoordY = 0;
            int minObjType = ConfigScript.getMinObjType();
            if (!ConfigScript.getScreenVertical())
            {
                UtilsGui.setCbItemsCount(cbCoordX, coordXCount - minCoordX, minCoordX, true);
                UtilsGui.setCbItemsCount(cbCoordY, coordYCount - minCoordY, minCoordY, true);
            }
            else
            {
                UtilsGui.setCbItemsCount(cbCoordY, coordXCount - minCoordX, minCoordX, true);
                UtilsGui.setCbItemsCount(cbCoordX, coordYCount - minCoordY, minCoordY, true);
            }
            UtilsGui.setCbItemsCount(cbObjType, objType - minObjType, minObjType, true);
        }

        private void resizeMapScreen()
        {
            int blockWidth = ConfigScript.getBlocksPicturesWidth();
            int scrLevelNo = getLevelRecForGameType().levelNo;
            if (ConfigScript.getScreenVertical())
                mapScreen.Size = new Size(ConfigScript.getScreenHeight(scrLevelNo) * blockWidth * 2, (ConfigScript.getScreenWidth(scrLevelNo) + 2) * 64);
            else
                mapScreen.Size = new Size((ConfigScript.getScreenWidth(scrLevelNo) + 2) * blockWidth * 2, ConfigScript.getScreenHeight(scrLevelNo) * 64);
            //mapScreen.Size = back3.Size;
        }

        private void cbScreenNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            curActiveScreen = cbScreenNo.SelectedIndex;
            int w = curLevelLayerData.width;
            int h = curLevelLayerData.height;
            btLeft.Enabled = curActiveScreen % w != 0;
            btRight.Enabled = curActiveScreen % w != w - 1;
            btUp.Enabled = curActiveScreen >= w;
            btDown.Enabled = curActiveScreen < w * (h - 1);
            lbScrNo.Text = String.Format("({0:X})", calcScrNo());
            mapScreen.Invalidate();
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
            activeBlock.Image = objectSprites.Images[index];
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
            for (int i = 0; i < lvObjects.SelectedIndices.Count; i++)
            {
                int index = lvObjects.SelectedIndices[i];
                if (index == -1)
                    continue;
                toRemove.Add(activeObjectList.objects[index]);
            }
            for (int i = 0; i < toRemove.Count; i++)
                activeObjectList.objects.Remove(toRemove[i]);
            fillObjectsListBox();

            btDelete.Enabled = false;
            mapScreen.Invalidate();
            dirty = true;
        }

        private void lbObjects_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                deleteSelected();
            }
        }

        private LevelRec getLevelRecForGameType()
        {
            return ConfigScript.getLevelRec(getActiveLayoutNo());
        }

        private void setObjects()
        {
            objectLists = ConfigScript.getObjects(getActiveLayoutNo());
            updateAddDataVisible(0);
            cbObjectList.Items.Clear();
            for (int i = 0; i < objectLists.Count; i++)
                cbObjectList.Items.Add(objectLists[i].name);
            fillObjectsListBox();
        }

        public void updateAddDataVisible(int index)
        {
            var activeObjectList = objectLists[curActiveObjectListIndex];
            pnAddData.Visible = activeObjectList.objects.Count > index && activeObjectList.objects[index].additionalData != null;
            if (pnAddData.Visible)
            {
                int addDataCount = 0;
                foreach (var addData in activeObjectList.objects[index].additionalData)
                {
                    var key = addData.Key;
                    lbDatas[addDataCount].Text = key;
                    cbDatas[addDataCount].Tag = key;
                    cbDatas[addDataCount].SelectedIndexChanged -= cbCoordX_SelectedIndexChanged;
                    UtilsGui.setCbItemsCount(cbDatas[addDataCount], 256, 0, true);
                    cbDatas[addDataCount].SelectedIndexChanged += cbCoordX_SelectedIndexChanged;
                    cbDatas[addDataCount].Visible = true;
                    lbDatas[addDataCount].Visible = true;
                    if (++addDataCount == cbDatas.Length)
                        break;
                }
                for (int i = addDataCount; i < cbDatas.Length; i++ )
                {
                    cbDatas[i].Visible = false;
                    lbDatas[i].Visible = false;
                }
            }
        }

        private void fillObjectsListBox()
        {
            var activeObjectList = objectLists[curActiveObjectListIndex];
            lvObjects.Items.Clear();
            for (int i = 0; i < activeObjectList.objects.Count; i++)
                lvObjects.Items.Add(new ListViewItem(makeStringForObject(activeObjectList.objects[i]), activeObjectList.objects[i].type));
            cbCoordX.Enabled = false;
            cbCoordY.Enabled = false;
            cbObjType.Enabled = false;
            lbObjectsCount.Text = String.Format("objects count: {0}/{1}", lvObjects.Items.Count, getLevelRecForGameType().objCount);
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

        private void btClearObjs_Click(object sender, EventArgs e)
        {
            var activeObjectList = objectLists[curActiveObjectListIndex];
            if (MessageBox.Show("Do you really want to delete all objects at screen?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            List<ObjectRec> toRemove = new List<ObjectRec>();
            for (int i = 0; i < activeObjectList.objects.Count; i++)
            {
                int screenNo = coordToScreenNo(activeObjectList.objects[i]);
                if (screenNo == curActiveScreen)
                    toRemove.Add(activeObjectList.objects[i]);
            }
            for (int i = 0; i < toRemove.Count; i++)
                activeObjectList.objects.Remove(toRemove[i]);

            fillObjectsListBox();
            mapScreen.Invalidate();
            dirty = true;
        }

        private void btLeft_Click(object sender, EventArgs e)
        {
            cbScreenNo.SelectedIndex = --curActiveScreen; ;
        }

        private void btRight_Click(object sender, EventArgs e)
        {
            cbScreenNo.SelectedIndex = ++curActiveScreen;
        }

        private void btUp_Click(object sender, EventArgs e)
        {
            curActiveScreen -= curLevelLayerData.width;
            cbScreenNo.SelectedIndex = curActiveScreen;
        }

        private void btDown_Click(object sender, EventArgs e)
        {
            curActiveScreen += curLevelLayerData.width;
            cbScreenNo.SelectedIndex = curActiveScreen;
        }

        private Point screenNoToCoord()
        {
            int width = curLevelLayerData.width;
            int height = curLevelLayerData.height;
            return new Point(curActiveScreen % width, curActiveScreen / width);
        }

        //Image back3 = Image.FromFile("back_tunnel_3.png");

        private void paintBack(Graphics g)
        {
            //temp hack for compatibility. for cad-games scrNo -= 1 !!!
            int scrNo = ConfigScript.usePicturesInstedBlocks ? curLevelLayerData.layer[curActiveScreen] : calcScrNo(); 
            if (scrNo < screens.Length && scrNo >= 0)
            {
                int[] indexes = screens[scrNo];
                int scrLevelNo = getLevelRecForGameType().levelNo;
                int width = ConfigScript.getScreenWidth(scrLevelNo);
                int height = ConfigScript.getScreenHeight(scrLevelNo);
                var visibleRect = UtilsGui.getVisibleRectangle(pnView, mapScreen);
                MapEditor.Render(g, bigBlocks, formMain.BlockWidth, formMain.BlockHeight, visibleRect, indexes, null, curScale, true, false, false, 0, width, height, ConfigScript.getScreenVertical());
                ConfigScript.renderToMainScreen(g, (int)curScale);
            }
            else
            {
                g.FillRectangle(Brushes.Black, new Rectangle(0, 0, 512, 512));
            }

            //mapScreen.Image = back3;
        }

        private void mapScreen_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            paintBack(g);
            for (int objListIndex = 0; objListIndex < objectLists.Count; objListIndex++)
            {
                var activeObjectList = objectLists[objListIndex];
                //if (ConfigScript.usePicturesInstedBlocks)
                var selectedInds = lvObjects.SelectedIndices;
                for (int i = 0; i < activeObjectList.objects.Count; i++)
                {
                    var curObject = activeObjectList.objects[i];
                    int screenIndex = coordToScreenNo(curObject);
                    if (screenIndex == curActiveScreen)
                    {
                        bool inactive = objListIndex != curActiveObjectListIndex;
                        bool selected = !inactive && selectedInds.Contains(i);
                        if (!useBigPictures)
                            ConfigScript.drawObject(g, curObject, curActiveObjectListIndex, selected, curScale, objectSprites, inactive);
                        else
                            ConfigScript.drawObjectBig(g, curObject, curActiveObjectListIndex, selected, curScale, objectSpritesBig, inactive);
                    }
                }
            }
        }

        private void cbCoordX_SelectedIndexChanged(object sender, EventArgs e)
        {
            var activeObjectList = objectLists[curActiveObjectListIndex];
            if (lvObjects.SelectedItems.Count != 1)
                return;
            int index = lvObjects.SelectedItems[0].Index;
            var obj = activeObjectList.objects[index];
            int minCoordX = 0;
            int minCoordY = 0;
            int minObjType = ConfigScript.getMinObjType();
            obj.x = cbCoordX.SelectedIndex + minCoordX;
            obj.y = cbCoordY.SelectedIndex + minCoordY;
            obj.type = cbObjType.SelectedIndex + minObjType;
            if (obj.additionalData != null)
            {
                foreach (var cb in cbDatas)
                {
                    if (cb.Visible)  //enable
                    {
                        var key = (string)cb.Tag;
                        activeObjectList.objects[index].additionalData[key] = cb.SelectedIndex;
                    }
                }
            }
            activeObjectList.objects[index] = obj;
            lvObjects.SelectedItems[0].ImageIndex = obj.type;
            lvObjects.SelectedItems[0].Text = makeStringForObject(obj);
            mapScreen.Invalidate();
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
                ConfigScript.setObjects(getActiveLayoutNo(), objectLists);
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "Save error");
                return false;
            }

            dirty = !Globals.flushToFile();
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

            //dirty = !Globals.flushToFile();
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
            return true;
        }

        private void lvObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool selectedZero = lvObjects.SelectedItems.Count == 0;
            bool selectedOne =  lvObjects.SelectedItems.Count == 1;
            bool selectedMany = lvObjects.SelectedItems.Count > 1;
            btDelete.Enabled = !selectedZero;
            cbCoordX.Enabled = selectedOne;
            cbCoordY.Enabled = selectedOne;
            cbObjType.Enabled = selectedOne;
            btSortDown.Enabled = false;
            btSortUp.Enabled = false;

            var activeObjectList = objectLists[curActiveObjectListIndex];

            if (selectedOne)
            {
                int index = lvObjects.SelectedItems[0].Index;
                int minCoordX = 0;
                int minCoordY = 0;
                int minObjType = ConfigScript.getMinObjType();
                try
                { 
                    UtilsGui.setCbIndexWithoutUpdateLevel(cbCoordX, cbCoordX_SelectedIndexChanged, activeObjectList.objects[index].x - minCoordX);
                    UtilsGui.setCbIndexWithoutUpdateLevel(cbCoordY, cbCoordX_SelectedIndexChanged, activeObjectList.objects[index].y - minCoordY);
                    UtilsGui.setCbIndexWithoutUpdateLevel(cbObjType, cbCoordX_SelectedIndexChanged, activeObjectList.objects[index].type - minObjType);
                    if (activeObjectList.objects[index].additionalData != null)
                    {
                        updateAddDataVisible(index);
                        int addDataCount = 0;
                        foreach (var addData in activeObjectList.objects[index].additionalData)
                        {
                   
                            UtilsGui.setCbIndexWithoutUpdateLevel(cbDatas[addDataCount], cbCoordX_SelectedIndexChanged, addData.Value);
                            cbDatas[addDataCount].Enabled = true;
                            if (++addDataCount == cbDatas.Length)
                                break;
                        }
                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    cbCoordX.Enabled = false;
                    cbCoordY.Enabled = false;
                    cbObjType.Enabled = false;
                    if (activeObjectList.objects[index].additionalData != null)
                    {
                        foreach (var cb in cbDatas)
                          cb.Enabled = false;
                    }
                }
            }
            if (!selectedZero)
            {
                btSortDown.Enabled = lvObjects.SelectedIndices[lvObjects.SelectedIndices.Count - 1] < activeObjectList.objects.Count - 1;
                btSortUp.Enabled = lvObjects.SelectedIndices[0] > 0;
            }

            mapScreen.Invalidate();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            saveToFile();
            /*saveToJsonFile("test.json");
            loadFromJsonFile("test.json");
            fillObjectsListBox();*/
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
                for (int i = 0; i < lvObjects.SelectedIndices.Count; i++)
                {
                    int ind = lvObjects.SelectedIndices[i];
                    selInds.Add(ind);
                    var xchg = activeObjectList.objects[ind];
                    activeObjectList.objects[ind] = activeObjectList.objects[ind - 1];
                    activeObjectList.objects[ind - 1] = xchg;
                }
                fillObjectsListBox();
                for (int i = 0; i < selInds.Count; i++)
                    lvObjects.Items[selInds[i] - 1].Selected = true;
                canMoveUp = lvObjects.SelectedIndices[0] > 0;
            }
            dirty = true;
            lvObjects.Select();
            int firstInd = lvObjects.SelectedIndices[0];
            lvObjects.Items[firstInd].Focused = true;
            lvObjects.TopItem = lvObjects.Items[firstInd];
        }

        private void btSortDown_Click(object sender, EventArgs e)
        {
            bool canMoveDown = true;
            int repeatCount = (Control.ModifierKeys == Keys.Shift) ? 10 : 1;
            var activeObjectList = objectLists[curActiveObjectListIndex];
            for (int count = 0; count < repeatCount && canMoveDown; count++)
            {
                var selInds = new List<int>();
                for (int i = lvObjects.SelectedIndices.Count - 1; i >= 0; i--)
                {
                    int ind = lvObjects.SelectedIndices[i];
                    selInds.Add(ind);
                    var xchg = activeObjectList.objects[ind];
                    activeObjectList.objects[ind] = activeObjectList.objects[ind + 1];
                    activeObjectList.objects[ind + 1] = xchg;
                }
                fillObjectsListBox();
                for (int i = 0; i < selInds.Count; i++)
                    lvObjects.Items[selInds[i] + 1].Selected = true;
                canMoveDown = lvObjects.SelectedIndices[lvObjects.SelectedIndices.Count - 1] < activeObjectList.objects.Count - 1;
            }
            lvObjects.Select();
            dirty = true;
            int firstInd = lvObjects.SelectedIndices[0];
            lvObjects.Items[firstInd].Focused = true;
            lvObjects.TopItem = lvObjects.Items[firstInd];
        }

        private void cbManualSort_CheckedChanged(object sender, EventArgs e)
        {
            if (lvObjects.SelectedIndices.Count > 0)
            {
                var activeObjectList = objectLists[curActiveObjectListIndex];
                btSortDown.Enabled = lvObjects.SelectedIndices[lvObjects.SelectedIndices.Count - 1] < activeObjectList.objects.Count - 1;
                btSortUp.Enabled = lvObjects.SelectedIndices[0] > 0;
            }
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
            ConfigScript.sortObjects(getActiveLayoutNo(), curActiveObjectListIndex, activeObjectList.objects);
            fillObjectsListBox();
        }

        private int getActiveLayoutNo()
        {
            return curActiveLayout;
        }

        private void mapScreen_MouseDown(object sender, MouseEventArgs e)
        {
            Point coord = screenNoToCoord();
            int type = curActiveBlock;
            int sx = coord.X, sy = coord.Y;
            int x = (int)(e.X / curScale);
            int y = (int)(e.Y / curScale);
            if (curTool == ToolType.Select)
            {
                if (Control.ModifierKeys != Keys.Shift && Control.ModifierKeys != Keys.Control)
                    lvObjects.SelectedItems.Clear();

                var activeObjectList = objectLists[curActiveObjectListIndex]; //TODO: all
                for (int i = 0; i < activeObjectList.objects.Count; i++)
                {
                    var obj = activeObjectList.objects[i];
                    if ((obj.sx == sx) && (obj.sy == sy) && (Math.Abs(obj.x - x) < 8) && (Math.Abs(obj.y - y) < 8))
                        lvObjects.Items[i].Selected = !lvObjects.Items[i].Selected;
                }
                lvObjects.Select();
            }
            objectDragged = true;
        }

        private void mapScreen_MouseMove(object sender, MouseEventArgs e)
        {
            if (!objectDragged)
                return;
            Point coord = screenNoToCoord();
           // int sx = coord.X, sy = coord.Y;
            int x = (int)(e.X / curScale);
            int y = (int)(e.Y / curScale);

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
                for (int i = 0; i < lvObjects.SelectedIndices.Count; i++)
                {
                    var obj = activeObjectList.objects[lvObjects.SelectedIndices[i]];
                    if (bindToAxis)
                    {
                        obj.x = (x / 8) * 8;
                        obj.y = (y / 8) * 8;
                    }
                    else
                    {
                        obj.x = x;
                        obj.y = y;
                    }
                    activeObjectList.objects[lvObjects.SelectedIndices[i]] = obj;
                }
            }
            dirty = true;
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
                lvObjects_SelectedIndexChanged(lvObjects, new EventArgs());
            }
        }

        private void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            Point coord = screenNoToCoord();
            int type = curActiveBlock;
            int sx = coord.X, sy = coord.Y;
            int x = (int)(e.X / curScale);
            int y = (int)(e.Y / curScale);

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
                dirty = true;
                if (bindToAxis)
                {
                    x = (x / 8) * 8;
                    y = (y / 8) * 8;
                }
                var dictionary = ConfigScript.getObjectDictionary(curActiveObjectListIndex, type);
                var obj = new ObjectRec(type, sx, sy, x, y, dictionary);

                int insertPos = lvObjects.SelectedItems.Count > 0 ? lvObjects.SelectedIndices[0] + 1 : lvObjects.Items.Count;
                var activeObjectList = objectLists[curActiveObjectListIndex];
                activeObjectList.objects.Insert(insertPos, obj);

                lvObjects.Items.Insert(insertPos, new ListViewItem(makeStringForObject(obj), obj.type));
                lbObjectsCount.Text = String.Format("objects count: {0}/{1}", lvObjects.Items.Count, getLevelRecForGameType().objCount);
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
                        dirty = true;
                        activeObjectList.objects.RemoveAt(i);
                        fillObjectsListBox();
                        break;
                    }
                }
                lvObjects.Select();
            }
            mapScreen.Invalidate();
        }

        public void setFormMain(FormMain f)
        {
            formMain = f;
        }

        private void cbBindToAxis_CheckedChanged(object sender, EventArgs e)
        {
            bindToAxis = cbBindToAxis.Checked;
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
            activeBlock.Image = objectSprites.Images[curActiveBlock];
            lbActive.Text = String.Format("({0:X2})", curActiveBlock);
        }

        private void cbBigObjectNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            curActiveBlock = cbBigObjectNo.SelectedIndex;
            pbBigObject.Image = objectSpritesBig[curActiveBlock];
            activeBlock.Image = objectSprites.Images[curActiveBlock];
            lbActive.Text = String.Format("({0:X2})", curActiveBlock);
        }

        private void btSaveJson_Click(object sender, EventArgs e)
        {
            saveToJsonFile("test.json");
        }

        private void btLoadJson_Click(object sender, EventArgs e)
        {
            loadFromJsonFile("test.json");
        }

        private void cbObjectList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbObjectList.SelectedIndex == -1)
                return;
            curActiveObjectListIndex = cbObjectList.SelectedIndex;
            fillObjectsListBox();
            mapScreen.Invalidate();
        }
    }

    public class ObjectList : IEquatable<ObjectList>
    {
        public ObjectList()
        {
            objects = new List<ObjectRec>();
            name = "Objects";
        }
        public List<ObjectRec> objects;
        public string name;

        bool IEquatable<ObjectList>.Equals(ObjectList other)
        {
            if (name != other.name)
                return false;
            return objects.SequenceEqual(other.objects);
        }
    }

    enum ToolType
    {
        Create,
        Select,
        Delete
    }
}
