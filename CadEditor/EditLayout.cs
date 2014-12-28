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
    public partial class EditLayout : Form
    {
        public EditLayout()
        {
            InitializeComponent();
        }

        private void EditForm_Load(object sender, EventArgs e)
        {
            makeScreens();

            var addPath = "";
            if (!File.Exists("scroll_sprites//scrolls.png"))
                addPath = "..//";
            scrollSprites.Images.Clear();
            scrollSprites.Images.AddStrip(Image.FromFile(addPath + "scroll_sprites//scrolls.png"));
            doorSprites.Images.Clear();
            doorSprites.Images.AddStrip(Image.FromFile(addPath + "scroll_sprites//doors.png"));
            dirSprites.Images.Clear();
            dirSprites.Images.AddStrip(Image.FromFile(addPath + "scroll_sprites//dirs.png"));
            objPanel.Controls.Clear();
            objPanel.SuspendLayout();

            for (int i = 0; i < scrollSprites.Images.Count; i++)
            {
                var but = new Button();
                but.Size = new Size(32, 32);
                but.ImageList = scrollSprites;
                but.ImageIndex = i;
                but.Click += new EventHandler(buttonScrollClick);
                objPanel.Controls.Add(but);
            }
            objPanel.ResumeLayout();

            doorsPanel.SuspendLayout();

            for (int i = 0; i < doorSprites.Images.Count; i++)
            {
                var but = new Button();
                but.Size = new Size(32, 32);
                but.ImageList = doorSprites;
                but.ImageIndex = i;
                but.Click += new EventHandler(buttonDoorClick);
                doorsPanel.Controls.Add(but);
            }
            doorsPanel.ResumeLayout();

            blocksPanel.Controls.Clear();
            blocksPanel.SuspendLayout();
            for (int i = 0; i < ConfigScript.screensOffset[scrLevelNo].recCount; i++)
            {
                var but = new Button();
                but.Size = new Size(64, 64);
                but.ImageList = screenImages;
                but.ImageIndex = i;
                but.Click += new EventHandler(buttonBlockClick);
                blocksPanel.Controls.Add(but);

            }
            blocksPanel.ResumeLayout();

            Utils.setCbItemsCount(cbVideoNo, ConfigScript.videoOffset.recCount);
            Utils.setCbItemsCount(cbBigBlockNo, ConfigScript.bigBlocksOffset.recCount);
            Utils.setCbItemsCount(cbBlockNo, ConfigScript.blocksOffset.recCount);
            Utils.setCbItemsCount(cbPaletteNo, ConfigScript.palOffset.recCount);
            cbVideoNo.SelectedIndex = 0;
            cbBigBlockNo.SelectedIndex = 0;
            cbBlockNo.SelectedIndex = 0;
            cbPaletteNo.SelectedIndex = 0;

            cbLayoutNo.Items.Clear();
            foreach (var lr in ConfigScript.levelRecs)
                cbLayoutNo.Items.Add(String.Format("0x{0:X} ({1}x{2})", lr.layoutAddr, lr.width, lr.height));
            Utils.setCbIndexWithoutUpdateLevel(cbLayoutNo, cbLevel_SelectedIndexChanged);

            cbLevel.SelectedIndex = 0;

            cbShowScrolls.Visible = ConfigScript.isShowScrollsInLayout();
            btExport.Visible = 
            pnParamGeneric.Visible = Globals.gameType != GameType.CAD;
        }

        private void reloadLevelLayer()
        {
            if (GameType.CAD == Globals.gameType)
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
                int layoutAddr = Globals.getLayoutAddr(curActiveLayout);
                int scrollAddr = Globals.getScrollAddr(curActiveLayout); //darkwing duck specific
                int width = curWidth;
                int height = curHeight;
                byte[] layer = new byte[width * height];
                byte[] scroll = new byte[width * height];
                for (int i = 0; i < width * height; i++)
                {
                    layer[i] = Globals.romdata[layoutAddr + i];
                    scroll[i] = Globals.romdata[scrollAddr + i];
                }
                curLevelLayerData = new LevelLayerData(width, height, layer, scroll, null);
            }
            curActiveBlock = 0;
            lvObjects.Items.Clear();
            pbMap.Invalidate();
        }

        private void makeScreens()
        {
            screenImages.Images.Clear();
            screenImages.Images.Add(makeBlackScreen(64,64,0));
            for (int scrNo = 0; scrNo < ConfigScript.screensOffset[scrLevelNo].recCount; scrNo++)
                screenImages.Images.Add(makeBlackScreen(64, 64, scrNo + 1));
        }

        //C&D specific
        private void previewScreens(List<ScreenRec> screenList)
        {
            makeScreens();
            var sortedScreenList = new List<ScreenRec>(screenList);
            sortedScreenList.Sort((r1, r2) => { return r1.door > r2.door ? 1 : r1.door < r2.door ? -1 : 0; });
            int lastDoorNo = -1;
            int levelNo = curActiveLevel;
            byte blockId = (byte)GlobalsCad.levelData[curActiveLevel].bigBlockId;
            byte backId = 0, palId = 0;
            for (int i = 0; i < sortedScreenList.Count; i++)
            {
                if (lastDoorNo != sortedScreenList[i].door)
                {
                    lastDoorNo = sortedScreenList[i].door;
                    if (lastDoorNo == 0)
                    {
                        backId = (byte)GlobalsCad.levelData[curActiveLevel].backId;
                        palId = (byte)GlobalsCad.levelData[curActiveLevel].palId;
                    }
                    else
                    {
                        backId = (byte)GlobalsCad.doorsData[lastDoorNo-1].backId;
                        palId = (byte)GlobalsCad.doorsData[lastDoorNo-1].palId;
                    }
                }
                int scrNo = sortedScreenList[i].no;
                int levelEHadd = (curActiveLevel == 5 || curActiveLevel == 8) ? 256 : 0; //level E & H hack
                int realScrNo = scrNo - 1 + levelEHadd;
                if (scrNo != 0) 
                {
                    Bitmap bitmap = ConfigScript.videoNes.makeScreen(realScrNo, scrLevelNo, backId, blockId, blockId, palId, 2.0f);
                    using (var g = Graphics.FromImage(bitmap))
                        g.DrawString(String.Format("{0:X}", scrNo), new Font("Arial", 64), Brushes.White, new Point(0, 0));
                    Bitmap convertedSize = new Bitmap(64, 64);
                    using (var g = Graphics.FromImage(convertedSize))
                        g.DrawImage(bitmap, new Rectangle(0, 0, 64, 64));

                    screenImages.Images[scrNo + levelEHadd] = convertedSize;
                }
            }
        }

        private Image makeBlackScreen(int w, int h, int no)
        {
            var b = new Bitmap(w, h);
            using (var g = Graphics.FromImage(b))
            {
                g.FillRectangle(Brushes.Black, new Rectangle(0, 0, w, h));
                g.DrawRectangle(new Pen(Color.Green, w/32), new Rectangle(0, 0, w, h));
                if (no % 256 != 0)
                  g.DrawString(String.Format("{0:X}", no), new Font("Arial", w/8), Brushes.White, new Point(0, 0));
            }
            return b;
        }

        const int SCREEN_SIZE = 64;

      

        private void pb_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            int w = Globals.gameType != GameType.CAD ? curWidth : curLevelLayerData.width;
            int h = Globals.gameType != GameType.CAD ? curHeight : curLevelLayerData.height;
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    int levelEHhack = (curActiveLevel == 5 || curActiveLevel == 8) ? 256 : 0;
                    int index = curLevelLayerData.layer[y * w + x] + levelEHhack;
                    int scroll = curLevelLayerData.scroll[y * w + x];
                    int scrollIndex = scroll >> 5;
                    int doorIndex = scroll & 0x01F;
                    g.DrawImage(screenImages.Images[index], new Rectangle(x*64, y*64, 64, 64));
                    if (showScrolls)
                    {
                        if (Globals.gameType == GameType.CAD)
                        {
                            g.DrawImage(scrollSprites.Images[scrollIndex], new Rectangle(x * 64 + 24, y * 64 + 24, 16, 16));
                            if (doorIndex != 0)
                                g.DrawImage(doorSprites.Images[doorIndex], new Rectangle(x * 64 + 48, y * 64 + 48, 16, 16));
                        }
                        else
                        {
                            g.DrawString(String.Format("{0:X}", scroll), new Font("Arial", 8), new SolidBrush(Color.Red), new Rectangle(x * 64 + 24, y * 64 + 24, 32, 16));
                        }
                    }
                }
            }
            if (Globals.gameType == GameType.CAD)
            {
                for (int i = 0; i < curLevelLayerData.height; i++)
                {
                    int dirIndex = (curLevelLayerData.dirs[i] % 2 == 0) ? 0 : 1;
                    g.DrawImage(dirSprites.Images[dirIndex], new Rectangle(curLevelLayerData.width * 64, i * 64, 64, 64));
                }
            }
        }

        private void changeScroll(int index)
        {
            var scrollByteArray = new byte[]{ 0x42, 0x42, 0x43, 0x03, 0x00, 0xC0, 0xC0, 0x41 };
            if (Globals.gameType == GameType.CAD)
                curLevelLayerData.scroll[index] = (byte)((curActiveBlock << 5) | (curLevelLayerData.scroll[index] & 0x01F));
            else
                curLevelLayerData.scroll[index] = scrollByteArray[curActiveBlock];
        }

        private void pb_MouseUp(object sender, MouseEventArgs e)
        {
            int dx = e.X / 64;
            int dy = e.Y / 64;
            if (dx >= curLevelLayerData.width + 1 || dy >= curLevelLayerData.height)
                return;
            dirty = true;
            if (dx == curLevelLayerData.width)
            {
                if (Globals.gameType == GameType.CAD)
                {
                    int dir = curLevelLayerData.dirs[dy];
                    curLevelLayerData.dirs[dy] = (byte)((dir == 0) ? 3 : 0);
                }
            }
            else
            {
                int index = dy * curLevelLayerData.width + dx;

                if (drawMode == MapDrawMode.Screens)
                    curLevelLayerData.layer[index] = (byte)(curActiveBlock & 0xFF);
                else if (drawMode == MapDrawMode.Scrolls)
                    changeScroll(index);
                else if (drawMode == MapDrawMode.Doors)
                    curLevelLayerData.scroll[index] = (byte)((curActiveBlock & 0x1F) | (curLevelLayerData.scroll[index] & 0xE0));
            }
            pbMap.Invalidate();
        }

        private int curActiveLevel = 0;
        private int curActiveBlock = 0;
        private MapDrawMode drawMode = MapDrawMode.Screens;
        private bool dirty = false;
        private bool showScrolls = false;
        private LevelLayerData curLevelLayerData = new LevelLayerData();

        private int curActiveLayout = 0;

        //for export params
        private int curVideoNo = 0;
        private int curBigBlockNo = 0;
        private int curBlockNo = 0;
        private int curPalleteNo = 0;

        private int curWidth = 1;
        private int curHeight = 1;

        //
        private int scrLevelNo = 0;
        

        private void cbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!Utils.askToSave(ref dirty, saveToFile, returnCbLevelIndex))
                return;
            if (cbLevel.SelectedIndex == -1 || cbLayoutNo.SelectedIndex == -1)
                return;
            curActiveLevel = cbLevel.SelectedIndex;

            curActiveLayout = cbLayoutNo.SelectedIndex;
            curWidth = Globals.getLevelWidth(curActiveLayout);
            curHeight = Globals.getLevelHeight(curActiveLayout);

            drawMode = MapDrawMode.Screens;
            curActiveBlock = 0;
            activeBlock.Image = screenImages.Images[0];

            updatePanelsVisibility();
            cbLayoutNo.Items.Clear();
            foreach (var lr in ConfigScript.levelRecs)
                cbLayoutNo.Items.Add(String.Format("0x{0:X} ({1}x{2})", lr.layoutAddr, lr.width, lr.height));
            Utils.setCbIndexWithoutUpdateLevel(cbLayoutNo, cbLevel_SelectedIndexChanged, curActiveLayout);
            reloadLevelLayer();
        }

        private void updatePanelsVisibility()
        {
            bool generic = Globals.gameType != GameType.CAD;
            bool showScroll = ConfigScript.isShowScrollsInLayout();
            pnDoors.Visible = showScroll;
            pnSelectScroll.Visible = showScroll;
            pnIngameScreenOrder.Visible = !generic;
            pnGeneric.Visible = generic;
            pnCad.Visible = !generic;
        }

        private void buttonBlockClick(Object button, EventArgs e)
        {
            int index = ((Button)button).ImageIndex;
            activeBlock.Image = screenImages.Images[index];
            curActiveBlock = index;
            drawMode = MapDrawMode.Screens;
        }

        private void buttonScrollClick(Object button, EventArgs e)
        {
            int index = ((Button)button).ImageIndex;
            activeBlock.Image = scrollSprites.Images[index];
            curActiveBlock = index;
            drawMode = MapDrawMode.Scrolls;
        }

        private void buttonDoorClick(Object button, EventArgs e)
        {
            int index = ((Button)button).ImageIndex;
            activeBlock.Image = doorSprites.Images[index];
            curActiveBlock = index;
            drawMode = MapDrawMode.Doors;
        }

        private void EditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dirty)
            {
                DialogResult dr = MessageBox.Show("Level was changed. Do you want to save current level?", "Save", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                    saveToFile();
            }
        }

        private bool saveToFile()
        {
            int layerAddr, scrollAddr, width, height;
            if (Globals.gameType != GameType.CAD)
            {
                layerAddr = Globals.getLayoutAddr(curActiveLayout);
                scrollAddr = Globals.getScrollAddr(curActiveLayout); //darkwing duck specific
                width = curWidth;
                height = curHeight;
            }
            else
            {
                width = curLevelLayerData.width;
                height = curLevelLayerData.height;
                layerAddr = GlobalsCad.levelData[curActiveLevel].getActualLayoutAddr();
                scrollAddr = GlobalsCad.levelData[curActiveLevel].getActualScrollAddr();
            }
            for (int i = 0; i < width * height; i++)
            {
                Globals.romdata[layerAddr + i] = curLevelLayerData.layer[i];
                Globals.romdata[scrollAddr + i] = curLevelLayerData.scroll[i];
            }
            if (Globals.gameType == GameType.CAD)
            {
                int dirAddr = GlobalsCad.levelData[curActiveLevel].getActualDirsAddr();
                for (int i = 0; i < height; i++)
                {
                    Globals.romdata[dirAddr + i] = curLevelLayerData.dirs[i];
                }
            }

            dirty = !Globals.flushToFile();
            return !dirty; 
        }

        private void returnCbLevelIndex()
        {
            Utils.setCbIndexWithoutUpdateLevel(cbLevel, cbLevel_SelectedIndexChanged, curActiveLevel);
            Utils.setCbIndexWithoutUpdateLevel(cbLayoutNo, cbLevel_SelectedIndexChanged, curActiveLayout);
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            saveToFile();
        }

        private void cbShowScrolls_CheckedChanged(object sender, EventArgs e)
        {
            showScrolls = cbShowScrolls.Checked;
            pbMap.Invalidate();
        }

        private void btPreview_Click(object sender, EventArgs e)
        {
            saveToFile();
            bool stopOnDoor = cbStopOnDoor.Checked;
            var screenList = GlobalsCad.buildScreenRecs(curActiveLevel, stopOnDoor);
            lvObjects.Items.Clear();
            for (int i = 0; i < screenList.Count; i++)
                lvObjects.Items.Add(String.Format("{0:X} ({1}:{2}) [{3:X}]", screenList[i].no, screenList[i].sx, screenList[i].sy, screenList[i].door));
            previewScreens(screenList);
            pbMap.Invalidate();
            objPanel.Invalidate(true);
        }

        private Bitmap makeLevelImage()
        {
            var answer = new Bitmap(curWidth*512, curHeight*512);
            using (var g = Graphics.FromImage(answer))
            {
                for (int w = 0; w < curWidth; w++)
                {
                    for (int h = 0; h < curHeight; h++)
                    {
                        int scrNo = curLevelLayerData.layer[h*curWidth + w] - 1;
                        Bitmap scr = scrNo >= 0 ? ConfigScript.videoNes.makeScreen(scrNo, scrLevelNo, curVideoNo, curBigBlockNo, curBlockNo, curPalleteNo, 2.0f) : VideoHelper.emptyScreen(512,512,false);
                        g.DrawImage(scr, new Point(w*512,h*512));
                    }
                }
            }
            return answer;
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            var f = new SelectFile();
            f.Filename = "level.png";
            f.ShowDialog();
            if (!f.Result)
                return;
            var fn = f.Filename;
            Bitmap levelImage = makeLevelImage();
            levelImage.Save(fn);
        }

        private void cbVideoNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            curVideoNo = cbVideoNo.SelectedIndex + 0x90;
            curBigBlockNo = cbBigBlockNo.SelectedIndex;
            curBlockNo = cbBlockNo.SelectedIndex;
            curPalleteNo = cbPaletteNo.SelectedIndex;
        }
    }        
    enum MapDrawMode
    {
        Screens,
        Scrolls,
        Doors
    };
}
