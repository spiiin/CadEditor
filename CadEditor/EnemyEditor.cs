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
        private LevelLayerData curLevelLayerData = new LevelLayerData();
        private Image[] scrImages = new Image[SCREENS_COUNT];
        private List<ObjectRec> objects = new List<ObjectRec>();
        private bool dirty = false;

        const int MAX_SIZE = 64;
        const int SCREENS_COUNT = 300;
        const int OBJECTS_COUNT = 96;

        private void reloadLevelLayerData()
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

            cbScreenNo.Items.Clear();
            for (int i = 0; i < curLevelLayerData.width * curLevelLayerData.height; i++)
                cbScreenNo.Items.Add(String.Format("{0:X}", i));
            cbScreenNo.SelectedIndex = findStartPosition();
        }

        private void makeScreens()
        {
            //!!!duplicate in EditLayout.cs
            const int BIG_BLOCKS_COUNT = 256;
            const int SCREEN_SIZE = 64;

            for (int i = 0; i < SCREENS_COUNT; i++)
                scrImages[i] = emptyScreen(512, 512);

            bool stopOnDoors = cbStopOnDoors.Checked;
            var screenList = Globals.buildScreenRecs(curActiveLevel, stopOnDoors);

            var sortedScreenList = new List<ScreenRec>(screenList);
            sortedScreenList.Sort((r1, r2) => { return r1.door > r2.door ? 1 : r1.door < r2.door ? -1 : 0; });
            int lastDoorNo = -1;
            ImageList smallBlocks = new ImageList();
            smallBlocks.ImageSize = new Size(16, 16);
            Image[] bigBlocks = null;
            int levelNo = curActiveLevel;
            for (int i = 0; i < sortedScreenList.Count; i++)
            {
                if (lastDoorNo != sortedScreenList[i].door)
                {
                    lastDoorNo = sortedScreenList[i].door;
                    smallBlocks.Images.Clear();
                    bigBlocks = new Image[BIG_BLOCKS_COUNT];
                    byte[] bigBlockIndexes = new byte[BIG_BLOCKS_COUNT * 4];
                    //set big blocks
                    byte blockId = (byte)Globals.levelData[curActiveLevel].bigBlockId;
                    byte backId, palId;
                    if (lastDoorNo == 0)
                    {
                        backId = (byte)Globals.levelData[curActiveLevel].backId;
                        palId = (byte)Globals.levelData[curActiveLevel].palId;
                    }
                    else
                    {
                        backId = (byte)Globals.doorsData[lastDoorNo - 1].backId;
                        palId = (byte)Globals.doorsData[lastDoorNo - 1].palId;
                    }
                    int bigBlockAddr = Globals.getBigTilesAddr(blockId);
                    for (int btileId = 0; btileId < BIG_BLOCKS_COUNT * 4; btileId++)
                        bigBlockIndexes[btileId] = Globals.romdata[bigBlockAddr + btileId];

                    var im = Video.makeObjectsStrip(backId, blockId, palId, 1, false);
                    smallBlocks.Images.AddStrip(im);

                    //make big blocks
                    for (int btileId = 0; btileId < BIG_BLOCKS_COUNT; btileId++)
                    {
                        var b = new Bitmap(64, 64);
                        using (Graphics g = Graphics.FromImage(b))
                        {
                            g.DrawImage(smallBlocks.Images[bigBlockIndexes[btileId * 4]], new Rectangle(0, 0, 32, 32));
                            g.DrawImage(smallBlocks.Images[bigBlockIndexes[btileId * 4 + 1]], new Rectangle(31, 0, 32, 32));
                            g.DrawImage(smallBlocks.Images[bigBlockIndexes[btileId * 4 + 2]], new Rectangle(0, 31, 32, 32));
                            g.DrawImage(smallBlocks.Images[bigBlockIndexes[btileId * 4 + 3]], new Rectangle(31, 31, 32, 32));
                        }
                        bigBlocks[btileId] = b;
                    }
                }
                int scrNo = sortedScreenList[i].no;
                byte[] indexes;
                //level E & H hack
                if (curActiveLevel == 5 || curActiveLevel==8)
                    indexes = Video.getScreen(256 + scrNo-1);
                else
                    indexes = Video.getScreen(scrNo - 1);

                Bitmap bitmap = new Bitmap(512, 512);
                using (var g = Graphics.FromImage(bitmap))
                {
                    for (int tileNo = 0; tileNo < SCREEN_SIZE; tileNo++)
                    {
                        int index = indexes[tileNo];
                        g.DrawImage(bigBlocks[index], new Rectangle(tileNo % 8 * 63, tileNo / 8 * 63, 64, 64));
                    }
                }
                scrImages[scrNo] = bitmap;
            }
        }

        private Bitmap emptyScreen(int w, int h)
        {
            var b = new Bitmap(w, h);
            using (var g = Graphics.FromImage(b))
            {
                g.FillRectangle(Brushes.Black, new Rectangle(0, 0, w, h));
                g.DrawRectangle(new Pen(Color.Green, w / 32), new Rectangle(0, 0, w, h));
            }
            return b;
        }

        private int findStartPosition()
        {
            int w = curLevelLayerData.width;
            int h = curLevelLayerData.height;
            return w * (h - 1);
        }

        private void setBackImage()
        {
            int scrNo = curLevelLayerData.layer[curActiveScreen];
            mapScreen.Image = scrImages[scrNo];
        }

        private void reloadLevel()
        {
            reloadLevelLayerData();
            makeScreens();
            setBackImage();
            setObjects();
            curActiveBlock = 0;
        }

        private void cbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //TODO: refactor this block to separate method
            if (dirty)
            {
                DialogResult dr = MessageBox.Show("Level was changed. Do you want to save current level?", "Save", MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Cancel)
                {
                    returnCbLevelIndex();
                    return;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!saveToFile())
                    {
                        returnCbLevelIndex();
                        return;
                    }
                }
                else
                {
                    dirty = false;
                }
            }

            if (cbLevel.SelectedIndex == -1)
                return;
            curActiveLevel = cbLevel.SelectedIndex;
            reloadLevel();
        }

        private void EnemyEditor_Load(object sender, EventArgs e)
        {
            var templ = "obj_sprites\\object{0}.png";
            for (int i = 0; i < OBJECTS_COUNT; i++)
            {
                var fname = String.Format(templ, i);
                if (File.Exists(fname))
                    objectSprites.Images.Add(Image.FromFile(fname));
            }

            fillObjPanel();

            cbCoordX.Items.Clear();
            cbCoordY.Items.Clear();
            for (int i = 0; i < 256; i++)
            {
                cbCoordX.Items.Add(String.Format("{0:X}", i));
                cbCoordY.Items.Add(String.Format("{0:X}", i));
            }

            cbLevel.SelectedIndex = 0;

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

        private void lbObjects_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
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
                mapScreen.Invalidate();
                dirty = true;
            }
        }

        private void setObjects()
        {
            objects.Clear();
            var lr = Globals.levelRecs[curActiveLevel];
            int objCount = lr.objCount;
            int addr = lr.objectsBeginAddr;
            for (int i = 0; i < lr.objCount; i++)
            {
                byte v = Globals.romdata[addr + i];
                if (curActiveLevel != 4)
                {
                    byte sx = Globals.romdata[addr - 4 * objCount + i];
                    byte x = Globals.romdata[addr - 3 * objCount + i];
                    byte sy = Globals.romdata[addr - 2 * objCount + i];
                    byte y = Globals.romdata[addr - objCount + i];
                    var obj = new ObjectRec(v, sx, sy, x, y);
                    objects.Add(obj);
                }
                else  //LEVEL D EXCEPTION
                {
                    byte sx = Globals.romdata[addr - 4 * objCount + 1 + i];
                    byte x = Globals.romdata[addr - 3 * objCount + 1 + i];
                    byte sy = Globals.romdata[addr - 2 * objCount + 1 + i];
                    byte y = Globals.romdata[addr - objCount + i];
                    var obj = new ObjectRec(v, sx, sy, x, y);
                    objects.Add(obj);
                }
            }
            fillObjectsListBox();
        }

        private void fillObjectsListBox()
        {
            lvObjects.Items.Clear();
            for (int i = 0; i < objects.Count; i++)
                lvObjects.Items.Add(new ListViewItem(makeStringForObject(objects[i]), objects[i].type));
            cbCoordX.Enabled = false;
            cbCoordY.Enabled = false;
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
            int index = dy * 8 + dx;
            dirty = true;
            Point coord = screenNoToCoord();
            byte type = (byte)curActiveBlock;
            byte sx = (byte)coord.X, sy = (byte)coord.Y;
            byte x = (byte)(e.X / 2);
            byte y = (byte)(e.Y / 2);
            var obj = new ObjectRec(type, sx, sy, x, y);
            objects.Add(obj);

            String ans = String.Format("{0} <{1}>", obj.ToString(), curActiveScreen);
            lvObjects.Items.Add(new ListViewItem(ans, obj.type));
            mapScreen.Invalidate();
        }

        private void mapScreen_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            for (int i = 0; i < objects.Count; i++)
            {
                var curObject = objects[i];
                int screenIndex = coordToScreenNo(curObject);
                if (screenIndex == curActiveScreen)
                {
                    int x = curObject.x, y = curObject.y;
                    if (curObject.type < objectSprites.Images.Count)
                        g.DrawImage(objectSprites.Images[curObject.type], new Point(x * 2 - 8, y * 2 - 8));
                }
            }
        }

        private void cbCoordX_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvObjects.SelectedItems.Count != 1)
                return;
            int index = lvObjects.SelectedItems[0].Index;
            var obj = objects[index];
            obj.x = (byte)cbCoordX.SelectedIndex;
            objects[index] = obj;
            lvObjects.SelectedItems[0].Text = makeStringForObject(obj);
            mapScreen.Invalidate();
        }

        private void cbCoordY_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvObjects.SelectedItems.Count != 1)
                return;
            int index = lvObjects.SelectedItems[0].Index;
            var obj = objects[index];
            obj.y = (byte)cbCoordY.SelectedIndex;
            objects[index] = obj;
            lvObjects.SelectedItems[0].Text = makeStringForObject(obj);
            mapScreen.Invalidate();
        }

        private bool saveToFile()
        {
            var romFname = "Chip 'n Dale Rescue Rangers (U) [!].nes";
            LevelRec lr = Globals.levelRecs[curActiveLevel];
            //write objects
            if (!cbManualSort.Checked)
              sortObjects();
            int addrBase = lr.objectsBeginAddr;
            int objCount = lr.objCount;
            if (objects.Count > lr.objCount)
            {
                MessageBox.Show(String.Format("Too many objects in level ({0}). Maximum: {1}", objects.Count, lr.objCount));
                return false;
            }
            //level D hack
            int levelDhack = (curActiveLevel == 4) ? 1 : 0;
            try
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    var obj = objects[i];
                    Globals.romdata[addrBase + i] = obj.type;
                    Globals.romdata[addrBase - 4 * objCount + levelDhack + i] = obj.sx;
                    Globals.romdata[addrBase - 3 * objCount + levelDhack + i] = obj.x;
                    Globals.romdata[addrBase - 2 * objCount + levelDhack + i] = obj.sy;
                    Globals.romdata[addrBase - objCount + i] = obj.y;
                }
                for (int i = objects.Count; i < objCount; i++)
                {
                    Globals.romdata[addrBase + i] = 0xFF;
                    Globals.romdata[addrBase - 4 * objCount + levelDhack + i] = 0xFF;
                    Globals.romdata[addrBase - 3 * objCount + levelDhack + i] = 0xFF;
                    Globals.romdata[addrBase - 2 * objCount + levelDhack + i] = 0xFF;
                    Globals.romdata[addrBase - objCount + levelDhack + i] = 0xFF;
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message, "Save error");
                return false;
            }

            //write to file
            try
            {
                using (FileStream f = File.OpenWrite(romFname))
                {
                    f.Write(Globals.romdata, 0, Globals.FILE_SIZE);
                    f.Seek(0, SeekOrigin.Begin);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            dirty = false;
            return true;
        }

        private void lvObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvObjects.SelectedItems.Count != 1)
            {
                cbCoordX.Enabled = false;
                cbCoordY.Enabled = false;
                //btSortDown.Enabled = false;
                //btSortUp.Enabled = false;
                return;
            }
            int index = lvObjects.SelectedItems[0].Index;
            cbCoordX.Enabled = true;
            cbCoordY.Enabled = true;
            cbCoordX.SelectedIndex = objects[index].x;
            cbCoordY.SelectedIndex = objects[index].y;
            if (lvObjects.SelectedIndices.Count > 0)
            {
                btSortDown.Enabled = cbManualSort.Checked && lvObjects.SelectedIndices[lvObjects.SelectedIndices.Count - 1] < objects.Count - 1;
                btSortUp.Enabled = cbManualSort.Checked && lvObjects.SelectedIndices[0] > 0;
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            saveToFile();
        }

        private void EnemyEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dirty)
            {
                DialogResult dr = MessageBox.Show("Level was changed. Do you want to save current level?", "Save", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                    saveToFile();
            }
        }

        private void returnCbLevelIndex()
        {
            cbLevel.SelectedIndexChanged -= cbLevel_SelectedIndexChanged;
            cbLevel.SelectedIndex = curActiveLevel;
            cbLevel.SelectedIndexChanged += cbLevel_SelectedIndexChanged;
        }

        private void btSortUp_Click(object sender, EventArgs e)
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
            btSortUp.Enabled = lvObjects.SelectedIndices[0] > 1;
            btSortDown.Enabled = true;
            fillObjectsListBox();
            for (int i = 0; i < selInds.Count; i++)
                lvObjects.Items[selInds[i] - 1].Selected = true;
            lvObjects.Select();
            dirty = true;
        }

        private void btSortDown_Click(object sender, EventArgs e)
        {
            var selInds = new List<int>();
            for (int i = lvObjects.SelectedIndices.Count-1; i >=0 ; i--)
            {
                int ind = lvObjects.SelectedIndices[i];
                selInds.Add(ind);
                var xchg = objects[ind];
                objects[ind] = objects[ind + 1];
                objects[ind + 1] = xchg;
            }
            btSortDown.Enabled = lvObjects.SelectedIndices[lvObjects.SelectedIndices.Count-1] < objects.Count - 3;
            btSortUp.Enabled = true;
            fillObjectsListBox();
            for (int i = 0; i < selInds.Count; i++)
                lvObjects.Items[selInds[i] + 1].Selected = true;
            lvObjects.Select();
            dirty = true;
        }

        private void cbManualSort_CheckedChanged(object sender, EventArgs e)
        {
            if (lvObjects.SelectedIndices.Count > 0)
            {
                btSortDown.Enabled = cbManualSort.Checked && lvObjects.SelectedIndices[lvObjects.SelectedIndices.Count - 1] < objects.Count - 1;
                btSortUp.Enabled = cbManualSort.Checked && lvObjects.SelectedIndices[0] > 0;
            }
        }

    }
}
