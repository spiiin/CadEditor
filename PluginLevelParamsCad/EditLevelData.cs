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
    public partial class EditLevelData : Form
    {
        public EditLevelData()
        {
            InitializeComponent();
        }

        private bool dirty = false;
        private int curActiveLevel;

        private void returnCbLevelIndex()
        {
            cbLevel.SelectedIndexChanged -= cbLevel_SelectedIndexChanged;
            cbLevel.SelectedIndex = curActiveLevel;
            cbLevel.SelectedIndexChanged += cbLevel_SelectedIndexChanged;
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
            int no = cbLevel.SelectedIndex;
            curActiveLevel = no;
            if (no == -1)
                return;
            bool isLevel = no < GlobalsCad.LEVELS_COUNT;
            if (isLevel)
            {
                LevelData ld = GlobalsCad.levelData[no];
                cbBackGfx.SelectedIndex = id89toIndex(ld.backId);
                cbObjGfx.SelectedIndex = id89toIndex(ld.objId);
                cbPalleteNo.SelectedIndex = ld.palId;
                cbPallete2No.SelectedIndex = ld.palId2;
                cbPalBlinkByte.SelectedIndex = ld.palBlink;

                cbStartLoc.SelectedIndex = ld.startLoc;
                cbLayoutWidth.SelectedIndex = ld.getWidth()-1;
                cbLayoutHeight.SelectedIndex = ld.getHeight()-1;
                cbLayoutAddr.Text = String.Format("{0:X}", ld.layoutAddr);
                cbScrollAddr.Text = String.Format("{0:X}", ld.scrollAddr);
                cbDirsAddr.Text = String.Format("{0:X}", ld.dirsAddr);
                lbLayoutAddress.Text = String.Format("{0:X}", ld.getActualLayoutAddr());
                lbScrollAddr.Text = String.Format("{0:X}", ld.getActualScrollAddr());
                lbDirsAddr.Text = String.Format("{0:X}", ld.getActualDirsAddr());

                cbBigBlocks.SelectedIndex = ld.bigBlockId;
                cbMusicNo.SelectedIndex = ld.musicNo;

                cbScrX.Enabled = false;
                cbScrY.Enabled = false;
                cbPlayerX.Enabled = false;
                cbPlayerY.Enabled = false;
                cbLayoutWidth.Enabled = true;
                cbLayoutHeight.Enabled = true;
                //cbScrollAddr.Enabled = true;
                //cbLayoutAddr.Enabled = true;
                //cbDirsAddr.Enabled = true;
                cbBigBlocks.Enabled = true;
                cbMusicNo.Enabled = true;
            }
            else
            {
                no -= GlobalsCad.LEVELS_COUNT;
                DoorData ld = GlobalsCad.doorsData[no];
                cbBackGfx.SelectedIndex = id89toIndex(ld.backId);
                cbObjGfx.SelectedIndex = id89toIndex(ld.objId);
                cbPalleteNo.SelectedIndex = ld.palId;
                cbPallete2No.SelectedIndex = ld.palId2;
                cbPalBlinkByte.SelectedIndex = ld.palBlink;

                cbStartLoc.SelectedIndex = ld.startLoc;
                cbScrX.SelectedIndex = ld.scrX;
                cbScrY.SelectedIndex = ld.scrY;
                cbPlayerX.SelectedIndex = ld.playerX;
                cbPlayerY.SelectedIndex = ld.playerY;

                cbScrX.Enabled = true;
                cbScrY.Enabled = true;
                cbPlayerX.Enabled = true;
                cbPlayerY.Enabled = true;
                cbLayoutWidth.Enabled = false;
                cbLayoutHeight.Enabled = false;
                //cbScrollAddr.Enabled = false;
                //cbLayoutAddr.Enabled = false;
                //cbDirsAddr.Enabled = false;
                cbBigBlocks.Enabled = false;
                cbMusicNo.Enabled = false;
            }
            lbl1.Enabled = isLevel;
            lbl2.Enabled = isLevel;
            lbl3.Enabled = isLevel;
            lbl4.Enabled = isLevel;
            lbl5.Enabled = isLevel;
            lbl6.Enabled = isLevel;
            lbl7.Enabled = isLevel;
            lbd1.Enabled = !isLevel;
            lbd2.Enabled = !isLevel;
            lbd3.Enabled = !isLevel;
            lbd4.Enabled = !isLevel;
            dirty = false;
        }

        private void EditLevelData_Load(object sender, EventArgs e)
        {
            loadLevelData();

            cbBackGfx.Items.Clear();
            cbObjGfx.Items.Clear();
            cbPalleteNo.Items.Clear();
            cbPallete2No.Items.Clear();
            cbPalBlinkByte.Items.Clear();
            cbStartLoc.Items.Clear();
            cbLayoutWidth.Items.Clear();
            cbLayoutHeight.Items.Clear();
            cbMusicNo.Items.Clear();
            cbBigBlocks.Items.Clear();
            for (int i = 0; i < 16; i++)
            {
                cbBackGfx.Items.Add(String.Format("8{0:X}", i));
                cbObjGfx.Items.Add(String.Format("8{0:X}", i));
            }
            for (int i = 0; i < 16; i++)
            {
                cbBackGfx.Items.Add(String.Format("9{0:X}", i));
                cbObjGfx.Items.Add(String.Format("9{0:X}", i));
            }

            for (int i = 0; i < 0x18; i++)
            {
                cbPalleteNo.Items.Add(String.Format("{0:X}", i));
                cbPallete2No.Items.Add(String.Format("{0:X}", i));
            }

            for (int i = 0; i < 256; i++)
            {
                cbPalBlinkByte.Items.Add(String.Format("{0:X}", i));
                cbPlayerX.Items.Add(String.Format("{0:X}", i));
                cbPlayerY.Items.Add(String.Format("{0:X}", i));
            }

            for (int i = 0; i < 64; i++)
                cbStartLoc.Items.Add(String.Format("{0:X}", i));

            for (int i = 0; i < 8; i++)
            {
                cbBigBlocks.Items.Add(String.Format("{0:X}", i));
                cbMusicNo.Items.Add(String.Format("{0:X}", i));
                cbLayoutWidth.Items.Add(String.Format("{0:X}", i+1));
                cbLayoutHeight.Items.Add(String.Format("{0:X}", i+1));
                cbScrX.Items.Add(String.Format("{0:X}", i));
                cbScrY.Items.Add(String.Format("{0:X}", i));
            }

            cbLevel.SelectedIndex = 0;
        }

        private void loadLevelData()
        {
            LevelData.LoadOffsetsFromConfig();
            DoorData.LoadOffsetsFromConfig();
            GlobalsCad.reloadLevelParamsData();
        }

        private int id89toIndex(int id)
        {
            if ((id & 0xF0) == 0x80)
                return id & 0xF;
            else if ((id & 0xF0) == 0x90)
                return 0x10 + (id & 0x0F);
            else
                return -1;
        }

        private byte indexToId89(int index)
        {
            if ((index & 0x10) == 0)
                return (byte)(0x80 | index);
            else
                return (byte)(0x90 | (index & 0xF));
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            saveToFile();
        }

        bool saveToFile()
        {
            var romFname = OpenFile.FileName;
            int no = cbLevel.SelectedIndex;
            if (no == -1)
                return false;
            bool isLevel = no < GlobalsCad.LEVELS_COUNT;

            if (isLevel)
            {
                LevelData ld = GlobalsCad.levelData[no];
                ld.backId = indexToId89(cbBackGfx.SelectedIndex);
                ld.objId = indexToId89(cbObjGfx.SelectedIndex);
                ld.palId = cbPalleteNo.SelectedIndex;
                ld.palId2 = cbPallete2No.SelectedIndex;
                ld.palBlink = cbPalBlinkByte.SelectedIndex;
                ld.startLoc = cbStartLoc.SelectedIndex;

                ld.musicNo = cbMusicNo.SelectedIndex;
                ld.bigBlockId = cbBigBlocks.SelectedIndex;
                ld.setWidth(cbLayoutWidth.SelectedIndex+1);
                ld.setHeight(cbLayoutHeight.SelectedIndex+1);
                ld.saveToFile(Globals.romdata, no);
            }
            else
            {
                no -= GlobalsCad.LEVELS_COUNT;
                DoorData ld = GlobalsCad.doorsData[no];
                ld.backId = indexToId89(cbBackGfx.SelectedIndex);
                ld.objId = indexToId89(cbObjGfx.SelectedIndex);
                ld.palId = cbPalleteNo.SelectedIndex;
                ld.palId2 = cbPallete2No.SelectedIndex;
                ld.palBlink = cbPalBlinkByte.SelectedIndex;
                ld.startLoc = cbStartLoc.SelectedIndex;

                ld.scrX = cbScrX.SelectedIndex;
                ld.scrY = cbScrY.SelectedIndex;
                ld.playerX = cbPlayerX.SelectedIndex;
                ld.playerY = cbPlayerY.SelectedIndex;
                ld.saveToFile(Globals.romdata, no);
            }

            dirty = !Globals.flushToFile();
            return !dirty;
        }

        private void EditLevelData_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dirty)
            {
                DialogResult dr = MessageBox.Show("Level was changed. Do you want to save current level?", "Save", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                    saveToFile();
            }
        }

        private void dirty_SelectedIndexChanged(object sender, EventArgs e)
        {
            dirty = true;
        }

        private void btChangeDoorIndex_Click(object sender, EventArgs e)
        {
            var f = new SelectDoorIndex();
            f.ShowDialog();
            if (f.getSelectedIndex() != -1)
                cbStartLoc.SelectedIndex = f.getSelectedIndex();
        }
    }
}
