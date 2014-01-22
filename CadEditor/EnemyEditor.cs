using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

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

        private ToolType curTool = ToolType.Create;

        private LevelLayerData curLevelLayerData = new LevelLayerData();
        private Image[] scrImages;
        private List<ObjectRec> objects = new List<ObjectRec>();
        private bool dirty = false;
        private bool readOnly = false;

        const int MAX_SIZE = 64;
        const int OBJECTS_COUNT = 256;

        //render back
        private byte[][] screens = null;

        private void reloadLevelLayerData(bool resetScreenPos)
        {
            if (Globals.gameType == GameType.CAD)
            {
                var lr = Globals.levelData[curActiveLevel];
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
                curWidth = Globals.getLevelWidth(curActiveLayout);
                curHeight = Globals.getLevelHeight(curActiveLayout);
                curActiveLayout = cbLayoutNo.SelectedIndex;
                curVideoNo = cbVideoNo.SelectedIndex + 0x90;
                curBigBlockNo = cbBigBlockNo.SelectedIndex;
                curBlockNo = cbBlockNo.SelectedIndex;
                curPaletteNo = cbPaletteNo.SelectedIndex;
                curActiveScreen = cbScreenNo.SelectedIndex;

                int layoutAddr = Globals.getLayoutAddr(curActiveLayout);
                int width = curWidth;
                int height = curHeight;
                byte[] layer = new byte[width * height];
                if (Globals.gameType != GameType.TT)
                {
                    for (int i = 0; i < width * height; i++)
                        layer[i] = Globals.romdata[layoutAddr + i];
                }
                else
                {
                    for (int i = 0; i < width * height; i++)
                        layer[i] = (byte)(i+1);
                }
                curLevelLayerData = new LevelLayerData(width, height, layer, null, null);
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
            scrImages = Globals.makeScreensCad(curActiveLevel, cbStopOnDoors.Checked);
        }

        private int findStartPosition()
        {
            int w = curLevelLayerData.width;
            int h = curLevelLayerData.height;
            return w * (h - 1);
        }

        //for generic editor
        private Bitmap makeCurScreen(int scrNo)
        {
            if (Globals.gameType == GameType.LM)
                scrNo = (scrNo + 1) % 256;
            return  Video.makeScreen(scrNo, curVideoNo, curBigBlockNo, curBlockNo, curPaletteNo);
        }

        private void setBackImage()
        {
            if (!ConfigScript.usePicturesInstedBlocks)
            {
                int scrNo = curLevelLayerData.layer[curActiveScreen];
                if (Globals.gameType != GameType.CAD && cbPlus256.Checked)
                    scrNo += 256;
                lbScrNo.Text = String.Format("({0:X})", scrNo);
                mapScreen.Image = (Globals.gameType != GameType.CAD) ? makeCurScreen(scrNo - 1) : scrImages[scrNo];
            }
        }

        private void reloadLevel(bool reloadObjects)
        {
            if (Globals.gameType == GameType.CAD)
                makeScreensCad();
            reloadLevelLayerData(reloadObjects);
            setBackImage();
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
            cbLayoutNo.Items.Clear();
            foreach (var lr in ConfigScript.levelRecs)
                cbLayoutNo.Items.Add(String.Format("0x{0:X} ({1}x{2})", lr.layoutAddr, lr.width, lr.height));
            Utils.setCbIndexWithoutUpdateLevel(cbLayoutNo, cbLevel_SelectedIndexChanged, curActiveLayout);
            reloadLevel(reloadObjects);
        }

        private void EnemyEditor_Load(object sender, EventArgs e)
        {
            if (ConfigScript.usePicturesInstedBlocks)
            {
                screens = Utils.setScreens();
                //bigBlocks.ImageSize = new Size(2 * ConfigScript.getBlocksPicturesWidth(), 2 * 32);
                Utils.setBlocks(bigBlocks /*, 2, ConfigScript.getBlocksPicturesWidth()*/);
            }

            reloadPictures();
            fillObjPanel();

            cbCoordX.Items.Clear();
            cbCoordY.Items.Clear();
            cbObjType.Items.Clear();
            for (int i = 0; i < ConfigScript.getScreenWidth()*32; i++)
                cbCoordX.Items.Add(String.Format("{0:X}", i));
            for (int i = 0; i < ConfigScript.getScreenHeight()*32; i++)
                cbCoordY.Items.Add(String.Format("{0:X}", i));
            for (int i = 0; i < 256; i++)
                cbObjType.Items.Add(String.Format("{0:X}", i));

            Utils.setCbItemsCount(cbVideoNo, ConfigScript.videoOffset.recCount);
            Utils.setCbItemsCount(cbBigBlockNo, ConfigScript.bigBlocksOffset.recCount);
            Utils.setCbItemsCount(cbBlockNo, ConfigScript.blocksOffset.recCount);
            Utils.setCbItemsCount(cbPaletteNo, ConfigScript.palOffset.recCount);
            Utils.setCbIndexWithoutUpdateLevel(cbVideoNo, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbBlockNo, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbBigBlockNo, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbPaletteNo, cbLevel_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbTool, cbTool_SelectedIndexChanged);
            cbLayoutNo.Items.Clear();
            foreach (var lr in ConfigScript.levelRecs)
                cbLayoutNo.Items.Add(String.Format("0x{0:X} ({1}x{2})", lr.layoutAddr, lr.width, lr.height));
            Utils.setCbIndexWithoutUpdateLevel(cbLayoutNo, cbLevel_SelectedIndexChanged);
            cbLevel.SelectedIndex = 0;
            updatePanelsVisibility();

            readOnly = Globals.gameType == GameType.DT2;
            btSave.Enabled = !readOnly;
            lbReadOnly.Visible = readOnly;

            if (ConfigScript.getScreenVertical())
                mapScreen.Size = new Size(ConfigScript.getScreenHeight() *64, (ConfigScript.getScreenWidth() + 2) * 64);
            else
                mapScreen.Size = new Size((ConfigScript.getScreenWidth() + 2) * 64, ConfigScript.getScreenHeight() * 64);

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
            setBackImage();
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
            bool stopOnDoor = cbStopOnDoors.Checked;
            List<ScreenRec> sortedScreens = Globals.buildScreenRecs(curActiveLevel, stopOnDoor);
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
            fillObjectsListBox();
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

        private void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int dx = e.X / 64;
            int dy = e.Y / 64;
            //int index = dy * 8 + dx;
            Point coord = screenNoToCoord();
            int type = curActiveBlock;
            int sx = coord.X, sy = coord.Y;
            int x = e.X / 2;
            int y = e.Y / 2;

            if (curTool == ToolType.Create)
            {
                if (x > cbCoordX.Items.Count || y > cbCoordY.Items.Count)
                    return;
                dirty = true;
                var obj = new ObjectRec(type, sx, sy, x, y);

                int insertPos = lvObjects.SelectedItems.Count > 0 ? lvObjects.SelectedIndices[0] + 1 : lvObjects.Items.Count;
                objects.Insert(insertPos, obj);

                lvObjects.Items.Insert(insertPos, new ListViewItem(makeStringForObject(obj), obj.type));
                lbObjectsCount.Text = String.Format("Objects count: {0}/{1}", lvObjects.Items.Count, getLevelRecForGameType().objCount);
            }
            else if (curTool == ToolType.Select)
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

        private void paintBack(Graphics g)
        {
            int WIDTH = ConfigScript.getScreenWidth();
            int HEIGHT = ConfigScript.getScreenHeight();
            int curScale = 2;
            int TILE_SIZE_X = 32 * curScale;
            int TILE_SIZE_Y = 32 * curScale;
            int SIZE = WIDTH * HEIGHT;
            byte[] indexes = screens[curActiveScreen];
            var visibleRect = Utils.getVisibleRectangle(pnView, mapScreen);
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
 
            //Additional rendering
            ConfigScript.renderToMainScreen(g, curScale);
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
                    if (curObject.type < objectSprites.Images.Count)
                        g.DrawImage(objectSprites.Images[curObject.type], new Point(x * 2 - 8, y * 2 - 8));
                    if (selectedInds.Contains(i))
                        g.DrawRectangle(new Pen(Brushes.Red, 2.0f), new Rectangle(x * 2 - 8, y * 2 - 8, 16, 16));
                }
            }
        }

        private void cbCoordX_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvObjects.SelectedItems.Count != 1)
                return;
            int index = lvObjects.SelectedItems[0].Index;
            var obj = objects[index];
            obj.x = cbCoordX.SelectedIndex;
            obj.y = cbCoordY.SelectedIndex;
            obj.type = cbObjType.SelectedIndex;
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
                Utils.setCbIndexWithoutUpdateLevel(cbCoordX, cbCoordX_SelectedIndexChanged, objects[index].x);
                Utils.setCbIndexWithoutUpdateLevel(cbCoordY, cbCoordX_SelectedIndexChanged, objects[index].y);
                Utils.setCbIndexWithoutUpdateLevel(cbObjType, cbCoordX_SelectedIndexChanged, objects[index].type);
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
            objectSprites.Images.Clear();
            for (int i = 0; i < OBJECTS_COUNT; i++)
            {
                var fname = String.Format(templ, i);
                var fnameGeneric = String.Format(templGeneric,i);
                if (File.Exists(fname))
                    objectSprites.Images.Add(Image.FromFile(fname));
                else if (File.Exists(fnameGeneric))
                    objectSprites.Images.Add(Image.FromFile(fnameGeneric));
            }
        }

        private void updatePanelsVisibility()
        {
            bool generic = Globals.gameType != GameType.CAD;
            pnCad.Visible = !generic;
            pnGeneric.Visible = generic;
            cbStopOnDoors.Visible = !generic;
            cbManualSort.Visible = !generic;
        }

        private void cbTool_SelectedIndexChanged(object sender, EventArgs e)
        {
            curTool = (ToolType)cbTool.SelectedIndex;
        }
    }

    enum ToolType
    {
        Create,
        Select,
        Delete
    }
}
