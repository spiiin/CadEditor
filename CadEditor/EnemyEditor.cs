using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace CadEditor
{
    public partial class EnemyEditor : Form
    {
        public EnemyEditor()
        {
            InitializeComponent();
        }

        private int curActiveLevel = 0;
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

        private bool bindToAxis = false;
        private bool useBigPictures = false;

        private ToolType curTool = ToolType.Create;

        private LevelLayerData curLevelLayerData = new LevelLayerData();
        private Image[] scrImages;
        private List<ObjectRec> objects = new List<ObjectRec>();
        private bool dirty = false;
        private bool readOnly = false;

        private FormMain formMain;
        ComboBox[] cbDatas;
        Label[] lbDatas;

        private Image[] objectSpritesBig;

        const int MAX_SIZE = 64;
        const int OBJECTS_COUNT = 256;

        bool objectDragged = false;

        //render back
        private int[][] screens = null;

        private void reloadLevelLayerData(bool resetScreenPos)
        {
            if (ConfigScript.getLayoutFunc != null)
            {
                //copy-paste
                curWidth = Globals.getLevelWidth(curActiveLayout);
                curHeight = Globals.getLevelHeight(curActiveLayout);
                curActiveLayout = cbLayoutNo.SelectedIndex;
                curVideoNo = cbVideoNo.SelectedIndex + 0x90;
                curBigBlockNo = cbBigBlockNo.SelectedIndex;
                curBlockNo = cbBlockNo.SelectedIndex;
                curPaletteNo = cbPaletteNo.SelectedIndex;
                curActiveScreen = cbScreenNo.SelectedIndex;
                curLevelLayerData = ConfigScript.getLayout(curActiveLayout);
            }
            else
            {
                if (Globals.gameType == GameType.CAD)
                {
                    var lr = GlobalsCad.levelData[curActiveLevel];
                    int layoutAddr = lr.getActualLayoutAddr();
                    int scrollAddr = lr.getActualScrollAddr();
                    int dirAddr = lr.getActualDirsAddr();
                    int width = lr.getWidth();
                    int height = lr.getHeight();
                    byte[] layer = new byte[width * height];
                    byte[] scroll = new byte[width * height];
                    byte[] dirs = new byte[height];
                    for (int i = 0; i < width * height; i++)
                    {
                        layer[i] = Globals.romdata[layoutAddr + i];
                        scroll[i] = Globals.romdata[scrollAddr + i];
                    }
                    for (int i = 0; i < height; i++)
                    {
                        dirs[i] = Globals.romdata[dirAddr + i];
                    }
                    curLevelLayerData = new LevelLayerData(width, height, layer, scroll, dirs);
                }
                else
                {
                    //copy-paste
                    curWidth = Globals.getLevelWidth(curActiveLayout);
                    curHeight = Globals.getLevelHeight(curActiveLayout);
                    curActiveLayout = cbLayoutNo.SelectedIndex;
                    curVideoNo = cbVideoNo.SelectedIndex + 0x90;
                    curBigBlockNo = cbBigBlockNo.SelectedIndex;
                    curBlockNo = cbBlockNo.SelectedIndex;
                    curPaletteNo = cbPaletteNo.SelectedIndex;
                    curActiveScreen = cbScreenNo.SelectedIndex;
                    curLevelLayerData = Utils.getLayoutLinear(curActiveLayout);
                }
            }

            if (resetScreenPos)
            {
                cbScreenNo.Items.Clear();
                for (int i = 0; i < curLevelLayerData.width * curLevelLayerData.height; i++)
                    cbScreenNo.Items.Add(String.Format("{0:X}", i));
                cbScreenNo.SelectedIndex = findStartPosition();
            }
        }

        private void makeScreensCad()
        {
            scrImages = GlobalsCad.makeScreensCad(curActiveLevel, false /*cbStopOnDoors.Checked*/);
        }

        private int findStartPosition()
        {
            int w = curLevelLayerData.width;
            int h = curLevelLayerData.height;
            return w * (h - 1);
        }

        //for generic editor
        private Bitmap makeCurScreen(int scrNo, int levelNo)
        {
            if (Globals.gameType == GameType.LM)
                scrNo = (scrNo + 1) % 256;
            return  ConfigScript.videoNes.makeScreen(scrNo, levelNo, curVideoNo, curBigBlockNo, curBlockNo, curPaletteNo, curScale);
        }

        private void setBackImage(int levelNo)
        {
            if (!ConfigScript.usePicturesInstedBlocks)
            {
                int scrNo = curLevelLayerData.layer[curActiveScreen];
                if (Globals.gameType != GameType.CAD && cbPlus256.Checked)
                    scrNo += 256;
                lbScrNo.Text = String.Format("({0:X})", scrNo);
                if (scrNo < ConfigScript.screensOffset[levelNo].recCount)
                    mapScreen.Image = (Globals.gameType != GameType.CAD) ? makeCurScreen(scrNo - 1, getLevelRecForGameType().levelNo) : scrImages[scrNo];
                else
                    mapScreen.Image = VideoHelper.emptyScreen(512, 512);
            }
        }

        private void reloadLevel(bool reloadObjects)
        {
            if (Globals.gameType == GameType.CAD)
                makeScreensCad();
            reloadLevelLayerData(reloadObjects);
            setBackImage(getLevelRecForGameType().levelNo);
            if (reloadObjects)
              setObjects();
            curActiveBlock = 0;
            btDelete.Enabled = false;
        }

        private void cbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLevel.SelectedIndex == -1 || cbLayoutNo.SelectedIndex == -1)
                return;
            bool realReload = (sender == cbLevel) || (sender == cbLayoutNo);
            bool reloadObjects = realReload;
            if (!readOnly && realReload)
            {
                if (!Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
                    return;
            }
           
            curActiveLevel = cbLevel.SelectedIndex;
            curActiveLayout = cbLayoutNo.SelectedIndex;
            curScale = cbScale.SelectedIndex + 1; //TODO: normal scale factors;
            cbLayoutNo.Items.Clear();
            foreach (var lr in ConfigScript.levelRecs)
                cbLayoutNo.Items.Add(String.Format("{0}:0x{1:X} ({2}x{3})", lr.name, lr.layoutAddr, lr.width, lr.height));
            Utils.setCbIndexWithoutUpdateLevel(cbLayoutNo, cbLevel_SelectedIndexChanged, curActiveLayout);
            reloadLevel(reloadObjects);
            mapScreen.Invalidate();
        }

        private void EnemyEditor_Load(object sender, EventArgs e)
        {
            if (ConfigScript.usePicturesInstedBlocks)
            {
                screens = Utils.setScreens(getLevelRecForGameType().levelNo);
                //bigBlocks.ImageSize = new Size(2 * ConfigScript.getBlocksPicturesWidth(), 2 * 32);
                Utils.setBlocks(bigBlocks, 2, 32,32, MapViewType.Tiles, formMain.ShowAxis);
            }

            cbDatas = new ComboBox[] { cbD0, cbD1, cbD2, cbD3, cbD4, cbD5 };
            lbDatas = new Label[]    { lbD0, lbD1, lbD2, lbD3, lbD4, lbD5 };

            reloadPictures();
            fillObjPanel();

            int coordXCount = (ConfigScript.getMaxObjCoordX() != -1) ? ConfigScript.getMaxObjCoordX() : ConfigScript.getScreenWidth(getLevelRecForGameType().levelNo) * 32;
            int coordYCount = (ConfigScript.getMaxObjCoordY() != -1) ? ConfigScript.getMaxObjCoordY() : ConfigScript.getScreenHeight(getLevelRecForGameType().levelNo) * 32;
            int objType = (ConfigScript.getMaxObjType() != -1) ? ConfigScript.getMaxObjType() : 256;
            int minCoordX = ConfigScript.getMinObjCoordX();
            int minCoordY = ConfigScript.getMinObjCoordY();
            int minObjType = ConfigScript.getMinObjType();
            if (!ConfigScript.getScreenVertical())
            {
                Utils.setCbItemsCount(cbCoordX, coordXCount - minCoordX, minCoordX, true);
                Utils.setCbItemsCount(cbCoordY, coordYCount - minCoordY, minCoordY, true);
            }
            else
            {
                Utils.setCbItemsCount(cbCoordY, coordXCount - minCoordX, minCoordX, true);
                Utils.setCbItemsCount(cbCoordX, coordYCount - minCoordY, minCoordY, true);
            }
            Utils.setCbItemsCount(cbObjType, objType - minObjType, minObjType, true);

            Utils.setCbItemsCount(cbVideoNo, ConfigScript.videoOffset.recCount);
            Utils.setCbItemsCount(cbBigBlockNo, ConfigScript.bigBlocksOffset.recCount);
            Utils.setCbItemsCount(cbBlockNo, ConfigScript.blocksOffset.recCount);
            Utils.setCbItemsCount(cbPaletteNo, ConfigScript.palOffset.recCount);
            Utils.setCbItemsCount(cbScale, 2, 1);
            if (Globals.gameType != GameType.CAD)
            {
                Utils.setCbIndexWithoutUpdateLevel(cbVideoNo, cbLevel_SelectedIndexChanged, formMain.CurActiveVideoNo - 0x90);
                Utils.setCbIndexWithoutUpdateLevel(cbBlockNo, cbLevel_SelectedIndexChanged, formMain.CurActiveBlockNo);
                Utils.setCbIndexWithoutUpdateLevel(cbBigBlockNo, cbLevel_SelectedIndexChanged, formMain.CurActiveBigBlockNo);
                Utils.setCbIndexWithoutUpdateLevel(cbPaletteNo, cbLevel_SelectedIndexChanged, formMain.CurActivePalleteNo);
                Utils.setCbIndexWithoutUpdateLevel(cbTool, cbTool_SelectedIndexChanged);
                Utils.setCbIndexWithoutUpdateLevel(cbScale, cbLevel_SelectedIndexChanged, 1);
            }
            cbLayoutNo.Items.Clear();
            foreach (var lr in ConfigScript.levelRecs)
                cbLayoutNo.Items.Add(String.Format("{0}:0x{1:X} ({2}x{3})", lr.name, lr.layoutAddr, lr.width, lr.height));
            Utils.setCbIndexWithoutUpdateLevel(cbLayoutNo, cbLevel_SelectedIndexChanged);
            cbLevel.SelectedIndex = 0;
            updatePanelsVisibility();

            readOnly = ConfigScript.setObjectsFunc == null;
            btSave.Enabled = !readOnly;
            lbReadOnly.Visible = readOnly;

            btSort.Visible = ConfigScript.sortObjectsFunc != null;

            int blockWidth = ConfigScript.getBlocksPicturesWidth();
            int scrLevelNo = getLevelRecForGameType().levelNo;
            if (ConfigScript.getScreenVertical())
                mapScreen.Size = new Size(ConfigScript.getScreenHeight(scrLevelNo) * blockWidth * 2, (ConfigScript.getScreenWidth(scrLevelNo) + 2) * 64);
            else
                mapScreen.Size = new Size((ConfigScript.getScreenWidth(scrLevelNo) + 2) * blockWidth * 2, ConfigScript.getScreenHeight(scrLevelNo) * 64);

            Utils.setCbItemsCount(cbBigObjectNo, 256, 0, true);
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
            setBackImage(getLevelRecForGameType().levelNo);
            mapScreen.Invalidate();
        }

        private void fillObjPanel()
        {
            objPanel.Controls.Clear();
            for (int i = 0; i < objectSprites.Images.Count; i++)
            {
                var but = new Button();
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

        private void sortObjects()
        {
            List<ObjectRec> sortedObjects = new List<ObjectRec>();
            bool stopOnDoor = false; //cbStopOnDoors.Checked;
            List<ScreenRec> sortedScreens = GlobalsCad.buildScreenRecs(curActiveLevel, stopOnDoor);
            for (int i = 0; i < sortedScreens.Count; i++)
            {
                var scrrec = sortedScreens[i];
                var objectsAtScreen = objects.FindAll((o) => { return (o.sx == scrrec.sx) && (o.sy == scrrec.sy); });
                int backSortCoeff = scrrec.backSort ? -1 : 1;
                if (scrrec.upsort)
                    objectsAtScreen.Sort((obj1, obj2) => { return obj1.y < obj2.y ? 1 : obj1.y > obj2.y ? -1 : 0; });
                else
                    objectsAtScreen.Sort((obj1, obj2) => { return obj1.x > obj2.x ? backSortCoeff : obj1.x < obj2.x ? -backSortCoeff : 0; });
                sortedObjects.AddRange(objectsAtScreen);
            }
            var undefinedObjects = objects.FindAll(isUndefindedObj);
            sortedObjects.AddRange(undefinedObjects);
            var ffObjects = objects.FindAll((o) => { return o.type == 0xFF; });
            sortedObjects.AddRange(ffObjects);
            if (sortedObjects.Count != objects.Count)
            {
                MessageBox.Show("Error while sorting objects. Some objects no parent screen. View object list and delete dead objects");
            }
            objects = sortedObjects;
            fillObjectsListBox();
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
            var toRemove = new List<ObjectRec>();
            for (int i = 0; i < lvObjects.SelectedIndices.Count; i++)
            {
                int index = lvObjects.SelectedIndices[i];
                if (index == -1)
                    continue;
                toRemove.Add(objects[index]);
            }
            for (int i = 0; i < toRemove.Count; i++)
                objects.Remove(toRemove[i]);
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
            return ConfigScript.getLevelRec(GameType.CAD == Globals.gameType ? curActiveLevel : curActiveLayout);
        }

        private void setObjects()
        {
            objects = ConfigScript.getObjects(Globals.gameType == GameType.CAD ? curActiveLevel : curActiveLayout);
            updateAddDataVisible();
            fillObjectsListBox();
        }

        public void updateAddDataVisible()
        {
            pnAddData.Visible = objects.Count > 0 && objects[0].additionalData != null;
            if (pnAddData.Visible)
            {
                int addDataCount = 0;
                foreach (var addData in objects[0].additionalData)
                {
                    var key = addData.Key;
                    lbDatas[addDataCount].Text = key;
                    cbDatas[addDataCount].Tag = key;
                    Utils.setCbItemsCount(cbDatas[addDataCount], 256, 0, true);
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
            lvObjects.Items.Clear();
            for (int i = 0; i < objects.Count; i++)
                lvObjects.Items.Add(new ListViewItem(makeStringForObject(objects[i]), objects[i].type));
            cbCoordX.Enabled = false;
            cbCoordY.Enabled = false;
            cbObjType.Enabled = false;
            lbObjectsCount.Text = String.Format("Objects count: {0}/{1}", lvObjects.Items.Count, getLevelRecForGameType().objCount);
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
            if (MessageBox.Show("Do you really want to delete all objects at screen?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            List<ObjectRec> toRemove = new List<ObjectRec>();
            for (int i = 0; i < objects.Count; i++)
            {
                int screenNo = coordToScreenNo(objects[i]);
                if (screenNo == curActiveScreen)
                    toRemove.Add(objects[i]);
            }
            for (int i = 0; i < toRemove.Count; i++)
                objects.Remove(toRemove[i]);

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

        private void paintBack(Graphics g)
        {
            if (curLevelLayerData.layer[curActiveScreen] < screens.Length)
            {
                int[] indexes = screens[curLevelLayerData.layer[curActiveScreen]];
                int scrLevelNo = getLevelRecForGameType().levelNo;
                int width = ConfigScript.getScreenWidth(scrLevelNo);
                int height = ConfigScript.getScreenHeight(scrLevelNo);
                var visibleRect = Utils.getVisibleRectangle(pnView, mapScreen);
                MapEditor.Render(g, bigBlocks, visibleRect, indexes, null, curScale, true, false, false, 0, width, height, ConfigScript.getScreenVertical());
                ConfigScript.renderToMainScreen(g, (int)curScale);
            }
            else
            {
                g.FillRectangle(Brushes.Black, new Rectangle(0, 0, 512, 512));
            }
        }

        private void mapScreen_Paint(object sender, PaintEventArgs e)
        {
            if (ConfigScript.usePicturesInstedBlocks)
              paintBack(e.Graphics);
            var g = e.Graphics;
            var selectedInds = lvObjects.SelectedIndices;
            for (int i = 0; i < objects.Count; i++)
            {
                var curObject = objects[i];
                int screenIndex = coordToScreenNo(curObject);
                if (screenIndex == curActiveScreen)
                {
                    int x = curObject.x, y = curObject.y;
                    if (!useBigPictures)
                    {
                        if (curObject.type < objectSprites.Images.Count)
                            g.DrawImage(objectSprites.Images[curObject.type], new Point((int)(x * curScale) - 8, (int)(y * curScale) - 8));
                        if (selectedInds.Contains(i))
                            g.DrawRectangle(new Pen(Brushes.Red, 2.0f), new Rectangle((int)(x * curScale) - 8, (int)(y * curScale) - 8, 16, 16));
                    }
                    else
                    {
                        int xsize = objectSpritesBig[curObject.type].Size.Width;
                        int ysize = objectSpritesBig[curObject.type].Size.Height;
                        if (curObject.type < objectSpritesBig.Length)
                            g.DrawImage(objectSpritesBig[curObject.type], new Rectangle((int)(x * curScale) - xsize / 2, (int)(y * curScale) - ysize / 2, xsize, ysize));
                        if (selectedInds.Contains(i))
                            g.DrawRectangle(new Pen(Brushes.Red, 2.0f), new Rectangle((int)(x * curScale) - xsize / 2, (int)(y * curScale) - ysize / 2, xsize, ysize));
                    }
                }
            }
        }

        private void cbCoordX_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvObjects.SelectedItems.Count != 1)
                return;
            int index = lvObjects.SelectedItems[0].Index;
            var obj = objects[index];
            int minCoordX = ConfigScript.getMinObjCoordX();
            int minCoordY = ConfigScript.getMinObjCoordY();
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
                        objects[index].additionalData[key] = cb.SelectedIndex;
                    }
                }
            }
            objects[index] = obj;
            lvObjects.SelectedItems[0].ImageIndex = obj.type;
            lvObjects.SelectedItems[0].Text = makeStringForObject(obj);
            mapScreen.Invalidate();
        }

        private bool saveToFile()
        {
            var romFname = OpenFile.FileName;
            LevelRec lr = getLevelRecForGameType();
            //write objects
            if (!cbManualSort.Checked)
              sortObjects();

            int addrBase = lr.objectsBeginAddr;
            int objCount = lr.objCount;
             
            if (objects.Count > objCount)
            {
                MessageBox.Show(String.Format("Too many objects in level ({0}). Maximum: {1}", objects.Count, lr.objCount));
                return false;
            }
            try
            {
                ConfigScript.setObjects(Globals.gameType == GameType.CAD ? curActiveLevel : curActiveLayout, objects);
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
            if (!cbManualSort.Checked)
                sortObjects();
            LevelRec lr = getLevelRecForGameType();
            try
            {
                using (TextWriter f = new StreamWriter(fname))
                {
                    for (int i = 0; i < objects.Count; i++)
                    {
                        var obj = objects[i];
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
            objects.Clear();
            try
            {
                using (TextReader f = new StreamReader(fname))
                {
                    string line;
                    while ((line = f.ReadLine()) != null)
                    {
                        var obj = JsonConvert.DeserializeObject<ObjectRec>(line);
                        objects.Add(obj);
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

            if (selectedOne)
            {
                int index = lvObjects.SelectedItems[0].Index;
                int minCoordX = ConfigScript.getMinObjCoordX();
                int minCoordY = ConfigScript.getMinObjCoordY();
                int minObjType = ConfigScript.getMinObjType();
                try
                {
                    Utils.setCbIndexWithoutUpdateLevel(cbCoordX, cbCoordX_SelectedIndexChanged, objects[index].x - minCoordX);
                    Utils.setCbIndexWithoutUpdateLevel(cbCoordY, cbCoordX_SelectedIndexChanged, objects[index].y - minCoordY);
                    Utils.setCbIndexWithoutUpdateLevel(cbObjType, cbCoordX_SelectedIndexChanged, objects[index].type - minObjType);
                    if (objects[index].additionalData != null)
                    {
                        int addDataCount = 0;
                        foreach (var addData in objects[index].additionalData)
                        {
                   
                            Utils.setCbIndexWithoutUpdateLevel(cbDatas[addDataCount], cbCoordX_SelectedIndexChanged, addData.Value);
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
                    if (objects[index].additionalData != null)
                    {
                        foreach (var cb in cbDatas)
                          cb.Enabled = false;
                    }
                }
            }
            if (!selectedZero)
            {
                btSortDown.Enabled = cbManualSort.Checked && lvObjects.SelectedIndices[lvObjects.SelectedIndices.Count - 1] < objects.Count - 1;
                btSortUp.Enabled = cbManualSort.Checked && lvObjects.SelectedIndices[0] > 0;
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
            Utils.setCbIndexWithoutUpdateLevel(cbLevel, cbLevel_SelectedIndexChanged, curActiveLevel);
            Utils.setCbIndexWithoutUpdateLevel(cbLayoutNo, cbLevel_SelectedIndexChanged, curActiveLayout);
            //save width/height
        }

        private void btSortUp_Click(object sender, EventArgs e)
        {
            bool canMoveUp = true;
            int repeatCount = (Control.ModifierKeys == Keys.Shift) ? 10 : 1;
            for (int count = 0; count < repeatCount && canMoveUp; count++)
            {
                var selInds = new List<int>();
                for (int i = 0; i < lvObjects.SelectedIndices.Count; i++)
                {
                    int ind = lvObjects.SelectedIndices[i];
                    selInds.Add(ind);
                    var xchg = objects[ind];
                    objects[ind] = objects[ind - 1];
                    objects[ind - 1] = xchg;
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
            for (int count = 0; count < repeatCount && canMoveDown; count++)
            {
                var selInds = new List<int>();
                for (int i = lvObjects.SelectedIndices.Count - 1; i >= 0; i--)
                {
                    int ind = lvObjects.SelectedIndices[i];
                    selInds.Add(ind);
                    var xchg = objects[ind];
                    objects[ind] = objects[ind + 1];
                    objects[ind + 1] = xchg;
                }
                fillObjectsListBox();
                for (int i = 0; i < selInds.Count; i++)
                    lvObjects.Items[selInds[i] + 1].Selected = true;
                canMoveDown = lvObjects.SelectedIndices[lvObjects.SelectedIndices.Count - 1] < objects.Count - 1;
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
                btSortDown.Enabled = cbManualSort.Checked && lvObjects.SelectedIndices[lvObjects.SelectedIndices.Count - 1] < objects.Count - 1;
                btSortUp.Enabled = cbManualSort.Checked && lvObjects.SelectedIndices[0] > 0;
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            deleteSelected();
        }

        private void reloadPictures()
        {
            var objSpritesDir = ConfigScript.getObjTypesPicturesDir();
            var objSpritesDirGeneric = "obj_sprites";
            var templ = objSpritesDir + "\\object{0}.png";
            var templGeneric = objSpritesDirGeneric + "\\object{0}.png";
            var templBig = objSpritesDir + "\\object{0}b.png";
            var templGenericBig = objSpritesDirGeneric + "\\object{0}b.png";
            objectSprites.Images.Clear();
            objectSpritesBig = new Image[256];
            for (int i = 0; i < OBJECTS_COUNT; i++)
            {
                var fname = String.Format(templ, i);
                var fnameGeneric = String.Format(templGeneric,i);
                //".." hack for WinXP compatibility
                if (File.Exists(fname))
                {
                    objectSprites.Images.Add(Image.FromFile(fname));
                }
                else if (File.Exists("..\\" + fname))
                {
                    objectSprites.Images.Add(Image.FromFile("..\\" + fname));
                }
                else if (File.Exists(fnameGeneric))
                {
                    objectSprites.Images.Add(Image.FromFile(fnameGeneric));
                }
                else if (File.Exists("..\\" + fnameGeneric))
                {
                    objectSprites.Images.Add(Image.FromFile("..\\" + fnameGeneric));
                }

                //
                var fnameBig = String.Format(templBig, i);
                var fnameGenericBig = String.Format(templGenericBig, i);
                if (File.Exists(fnameBig))
                {
                    objectSpritesBig[i] = Image.FromFile(fnameBig);
                }
                else if (File.Exists("..\\" + fnameBig))
                {
                    objectSpritesBig[i] = Image.FromFile("..\\" + fnameBig);
                }
                else if (File.Exists(fnameGenericBig))
                {
                    objectSpritesBig[i] = Image.FromFile(fnameGenericBig);
                }
                else if (File.Exists("..\\" + fnameGenericBig))
                {
                    objectSpritesBig[i] = Image.FromFile("..\\" + fnameGenericBig);
                }
                else
                {
                    objectSpritesBig[i] = objectSprites.Images[i];
                }
            }
        }

        private void updatePanelsVisibility()
        {
            bool generic = Globals.gameType != GameType.CAD;
            pnCad.Visible = !generic;
            pnGeneric.Visible = generic;
            cbManualSort.Visible = !generic;
        }

        private void cbTool_SelectedIndexChanged(object sender, EventArgs e)
        {
            curTool = (ToolType)cbTool.SelectedIndex;
        }

        private void btSort_Click(object sender, EventArgs e)
        {
            ConfigScript.sortObjects(Globals.gameType == GameType.CAD ? curActiveLevel : curActiveLayout, objects);
            fillObjectsListBox();
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
                for (int i = 0; i < objects.Count; i++)
                {
                    var obj = objects[i];
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
            int coordXCount = (ConfigScript.getMaxObjCoordX() != -1) ? ConfigScript.getMaxObjCoordX() : ConfigScript.getScreenWidth(scrLevelNo) * 32;
            int coordYCount = (ConfigScript.getMaxObjCoordY() != -1) ? ConfigScript.getMaxObjCoordY() : ConfigScript.getScreenHeight(scrLevelNo) * 32;
            int minCoordX = ConfigScript.getMinObjCoordX();
            int minCoordY = ConfigScript.getMinObjCoordY();
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
                for (int i = 0; i < lvObjects.SelectedIndices.Count; i++)
                {
                    var obj = objects[lvObjects.SelectedIndices[i]];
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
                    objects[lvObjects.SelectedIndices[i]] = obj;
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
                int coordXCount = (ConfigScript.getMaxObjCoordX() != -1) ? ConfigScript.getMaxObjCoordX() : ConfigScript.getScreenWidth(scrLevelNo) * 32;
                int coordYCount = (ConfigScript.getMaxObjCoordY() != -1) ? ConfigScript.getMaxObjCoordY() : ConfigScript.getScreenHeight(scrLevelNo) * 32;
                int minCoordX = ConfigScript.getMinObjCoordX();
                int minCoordY = ConfigScript.getMinObjCoordY();

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
                var dictionary = ConfigScript.getObjectDictionary(type);
                var obj = new ObjectRec(type, sx, sy, x, y, dictionary);

                int insertPos = lvObjects.SelectedItems.Count > 0 ? lvObjects.SelectedIndices[0] + 1 : lvObjects.Items.Count;
                objects.Insert(insertPos, obj);

                lvObjects.Items.Insert(insertPos, new ListViewItem(makeStringForObject(obj), obj.type));
                lbObjectsCount.Text = String.Format("Objects count: {0}/{1}", lvObjects.Items.Count, getLevelRecForGameType().objCount);
            }
            else if (curTool == ToolType.Delete)
            {
                for (int i = objects.Count - 1; i >= 0; i--)
                {
                    var obj = objects[i];
                    if ((obj.sx == sx) && (obj.sy == sy) && (Math.Abs(obj.x - x) < 8) && (Math.Abs(obj.y - y) < 8))
                    {
                        if (MessageBox.Show("Do you really want to delete object?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes)
                            return;
                        dirty = true;
                        objects.RemoveAt(i);
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
    }



    enum ToolType
    {
        Create,
        Select,
        Delete
    }
}
