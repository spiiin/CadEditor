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
    public partial class BigBlockEditCad : BigBlockEdit
    {
        public BigBlockEditCad()
        {
            InitializeComponent();
        }

        private void BigBlockEdit_Load(object sender, EventArgs e)
        {
            curTileset = 0;
            curLevel = 0;
            curDoor = -1;
            curPart = 0;
            dirty = false;
            updateSaveVisibility();
            curViewType = MapViewType.Tiles;

            initControls();

            blocksPanel.Controls.Clear();
            blocksPanel.SuspendLayout();
            for (int i = 0; i < SMALL_BLOCKS_COUNT; i++)
            {
                var but = new Button();
                but.Size = new Size(32, 32);
                but.ImageList = smallBlocks;
                but.ImageIndex = i;
                but.Margin = new Padding(0);
                but.Padding = new Padding(0);
                but.Click += new EventHandler(buttonObjClick);
                blocksPanel.Controls.Add(but);
            }
            blocksPanel.ResumeLayout();
            prepareAxisLabels();
            reloadLevel();

            readOnly = false; //must be read from config
            tbbSave.Enabled = !readOnly;
            tbbImport.Enabled = !readOnly;
        }

        protected override void initControls()
        {
            Utils.setCbItemsCount(cbPart, Math.Max(ConfigScript.getBigBlocksCount() / 256, 1));
            cbTileset.Items.Clear();
            for (int i = 0; i < ConfigScript.bigBlocksOffset.recCount; i++)
            {
                var str = String.Format("Tileset{0}", i);
                cbTileset.Items.Add(str);
            }

            //cad version
            cbLevel.SelectedIndex = 0;
            cbDoor.SelectedIndex = 0;
            cbTileset.SelectedIndex = 0;
            cbPart.SelectedIndex = 0;
            cbViewType.SelectedIndex = 0; // Math.Min((int)formMain.CurActiveViewType, cbViewType.Items.Count - 1);
        }

        protected override void setSmallBlocks()
        {
            int backId, palId;
            var ld = GlobalsCad.levelData[curLevel];
            if (curDoor < 0)
            {
                backId = ld.backId;
                palId = ld.palId;
            }
            else
            {
                DoorData dd = GlobalsCad.doorsData[curDoor];
                backId = dd.backId;
                palId = dd.palId;
            }

            var im = Video.makeObjectsStrip((byte)backId, (byte)curTileset, (byte)palId, 1, curViewType);
            smallBlocks.Images.Clear();
            smallBlocks.Images.AddStrip(im);
            blocksPanel.Invalidate(true);
        }

        protected override void setBigBlocksIndexes()
        {
            bigBlockIndexes = ConfigScript.getBigBlocks(curTileset);
        }

        //chip and dale
        private int curLevel;
        private int curDoor;

        private void cbLevelPair_SelectedIndexChangedCad(object sender, EventArgs e)
        {
            if (cbLevel.SelectedIndex == -1 || cbTileset.SelectedIndex == -1 || cbDoor.SelectedIndex == -1 ||
                cbPart.SelectedIndex == -1 || cbViewType.SelectedIndex == -1)
            {
                return;
            }
            if (!readOnly && dirty && sender == cbTileset)
            {
                DialogResult dr = MessageBox.Show("Tiles was changed. Do you want to save current tileset?", "Save", MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Cancel)
                {
                    returnCbLevelIndexes();
                    return;
                }
                else if (dr == DialogResult.Yes)
                {
                    if (!saveToFile())
                    {
                        returnCbLevelIndexes();
                        return;
                    }
                }
                else
                {
                    dirty = false;
                    updateSaveVisibility();
                }
            }
            curTileset = cbTileset.SelectedIndex;
            curViewType = (MapViewType)cbViewType.SelectedIndex;

            //cad version
            curLevel = cbLevel.SelectedIndex;
            curDoor = cbDoor.SelectedIndex - 1;
            curPart = cbPart.SelectedIndex;
            Utils.setCbItemsCount(cbPart, Math.Max(ConfigScript.getBigBlocksCount() / 256, 1));
            Utils.setCbIndexWithoutUpdateLevel(cbPart, cbLevelPair_SelectedIndexChangedCad, curPart);
            reloadLevel();
        }

        private void returnCbLevelIndexes()
        {
            cbTileset.SelectedIndexChanged -= cbLevelPair_SelectedIndexChangedCad;
            cbTileset.SelectedIndex = curTileset;
            cbTileset.SelectedIndexChanged += cbLevelPair_SelectedIndexChangedCad;
        }

        protected override void exportBlocks()
        {
            //duck tales 2 has other format
            var f = new SelectFile();
            f.Filename = "exportedBigBlocks.bin";
            f.ShowExportParams = true;
            f.ShowDialog();
            if (!f.Result)
                return;
            var fn = f.Filename;
            if (f.getExportType() == ExportType.Binary)
            {
                Utils.saveDataToFile(fn, bigBlockIndexes);
            }
            else
            {
                Bitmap result = new Bitmap(64 * 256, 64); //need some hack for duck tales 1
                using (Graphics g = Graphics.FromImage(result))
                {
                    for (int i = 0; i < ConfigScript.getBigBlocksCount(); i++)
                    {
                        Bitmap b = Video.makeBigBlock(i, 64, 64, bigBlockIndexes, smallBlocks);
                        g.DrawImage(b, new Point(64 * i, 0));
                    }
                }
                result.Save(fn);
            }
        }
    }
}
