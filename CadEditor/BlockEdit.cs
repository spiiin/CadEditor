using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CadEditor
{
    public partial class BlockEdit : Form
    {
        public BlockEdit()
        {
            InitializeComponent();
        }

        private void BlockEdit_Load(object sender, EventArgs e)
        {
            showAxis = true;
            cbSubpalette.DrawItem += new DrawItemEventHandler(cbSubpalette_DrawItemEvent);
            videoSprites[0] = videoSprites1;
            videoSprites[1] = videoSprites2;
            videoSprites[2] = videoSprites3;
            videoSprites[3] = videoSprites4;
            dirty = false;

            preparePanel();
            resetControls();
            reloadLevel();

            readOnly = false; //must be read from config
            btSave.Enabled = !readOnly;
            lbReadOnly.Visible = readOnly;
            btFlipHorizontal.Visible = !readOnly;
            btFlipVertical.Visible = !readOnly;
            btImport.Visible = !readOnly;
            //btExport.Visible = !readOnly;
        }

        protected virtual void resetControls()
        {
            Utils.setCbItemsCount(cbVideo, ConfigScript.videoOffset.recCount);
            Utils.setCbItemsCount(cbTileset, ConfigScript.blocksOffset.recCount);
            Utils.setCbItemsCount(cbPalette, ConfigScript.palOffset.recCount);


            Utils.setCbIndexWithoutUpdateLevel(cbTileset, cbLevelSelect_SelectedIndexChanged, formMain.CurActiveBigBlockNo);  //small blocks no?
            Utils.setCbIndexWithoutUpdateLevel(cbVideo, VisibleOnlyChange_SelectedIndexChanged, formMain.CurActiveVideoNo - 0x90);
            Utils.setCbIndexWithoutUpdateLevel(cbPalette, VisibleOnlyChange_SelectedIndexChanged, formMain.CurActivePalleteNo);
            curActiveBigBlock = formMain.CurActiveBigBlockNo; //small blocks no?
            curActiveVideo = formMain.CurActiveVideoNo;
            curActivePal = formMain.CurActivePalleteNo;
            Utils.setCbIndexWithoutUpdateLevel(cbSubpalette, cbSubpalette_SelectedIndexChanged);
        }

        protected void reloadLevel(bool resetDirty = true)
        {
            setPal();
            setVideo();
            setVideoImage();
            setObjects();
            reloadLevelEx();
            if (resetDirty)
              dirty = false;
        }

        protected virtual void reloadLevelEx()
        {
        }

        protected void setPal()
        {
            byte palId = getPalNo();
            palette = ConfigScript.getPal(palId);
            //set image for pallete
            var b = new Bitmap(16 * 16, 16);
            using (Graphics g = Graphics.FromImage(b))
            {
                for (int i = 0; i < Globals.PAL_LEN; i++)
                    g.FillRectangle(new SolidBrush(ConfigScript.videoNes.NesColors[palette[i]]), i * 16, 0, 16, 16);
            }
            paletteMap.Image = b;

            //set images for subpalletes
            subpalSprites.Images.Clear();
            for (int i = 0; i < 4; i++)
            {
                var sb = new Bitmap(16 * 4, 16);
                using (Graphics g = Graphics.FromImage(sb))
                {
                    g.FillRectangle(new SolidBrush(ConfigScript.videoNes.NesColors[palette[i * 4]]), 0, 0, 16, 16);
                    g.FillRectangle(new SolidBrush(ConfigScript.videoNes.NesColors[palette[i * 4 + 1]]), 16, 0, 16, 16);
                    g.FillRectangle(new SolidBrush(ConfigScript.videoNes.NesColors[palette[i * 4 + 2]]), 32, 0, 16, 16);
                    g.FillRectangle(new SolidBrush(ConfigScript.videoNes.NesColors[palette[i * 4 + 3]]), 48, 0, 16, 16);
                }
                subpalSprites.Images.Add(sb);
            }
        }

        protected virtual byte getPalNo()
        {
            return (byte)curActivePal;
        }

        protected virtual byte getBigBlockNo()
        {
            return (byte)curActiveBigBlock;
        }

        private void setObjects()
        {
            byte bigBlockId = getBigBlockNo();
            objects = ConfigScript.getBlocks(bigBlockId);
            refillPanel();
        }

        protected void setVideo()
        {
            byte backId = getBackId();
            for (int i = 0; i < 4; i++)
            {
                Bitmap b = ConfigScript.videoNes.makeImageStrip(ConfigScript.getVideoChunk(backId), palette, i, 2, !showAxis);
                videoSprites[i].Images.Clear();
                videoSprites[i].Images.AddStrip(b);
            }
        }

        protected void setVideoImage()
        {
            var b = new Bitmap(256, 256);
            using (Graphics g = Graphics.FromImage(b))
            {
                for (int i = 0; i < Globals.CHUNKS_COUNT; i++)
                {
                    int x = i % 16;
                    int y = i / 16;
                    g.DrawImage(videoSprites[curSubpalIndex].Images[i], new Rectangle(x * 16, y * 16, 16, 16));
                }
            }
            mapScreen.Image = b;
        }

        //generic
        private int curActiveVideo = 0x90;
        private int curActiveBigBlock = 0;
        private int curActivePal = 0;
        //editor
        protected int curSubpalIndex = 0;
        protected int curActiveBlock = 0;

        protected byte[] palette = new byte[Globals.PAL_LEN];
        protected ObjRec[] objects = new ObjRec[256];
        protected ImageList[] videoSprites = new ImageList[4];
        protected bool dirty;
        protected bool readOnly;
        protected bool showAxis;

        protected string[] subPalItems = { "1", "2", "3", "4" };

        protected FormMain formMain;

        protected void cbSubpalette_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSubpalette.SelectedIndex == -1)
                return;
            curSubpalIndex = cbSubpalette.SelectedIndex;
            setVideoImage();
        }

        protected void cbSubpalette_DrawItemEvent(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                return;
            }
            e.DrawBackground();
            e.Graphics.DrawImage(subpalSprites.Images[e.Index], e.Bounds.Width - 63, e.Bounds.Y, 64, 16);
            string text = cbSubpalette.Items[e.Index].ToString();
            e.Graphics.DrawString(text, cbSubpalette.Font,
                System.Drawing.Brushes.Black,
                new RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
            e.DrawFocusRectangle();
        }

        protected void pb_MouseClick(object sender, MouseEventArgs e)
        {
            bool left = e.Button == MouseButtons.Left;
            int x = e.X / 16;
            int y = e.Y / 16;
            PictureBox p = (PictureBox)sender;
            int objIndex = (int)p.Tag;
            if (x == 0)
            {
                if (y == 0)
                {
                    if (left)
                        objects[objIndex].c1 = (byte)curActiveBlock;
                    else
                        curActiveBlock = objects[objIndex].c1;
                }
                else
                {
                    if (left)
                        objects[objIndex].c3 = (byte)curActiveBlock;
                    else
                        curActiveBlock = objects[objIndex].c3;
                }
            }
            else
            {
                if (y == 0)
                {
                    if (left)
                        objects[objIndex].c2 = (byte)curActiveBlock;
                    else
                        curActiveBlock = objects[objIndex].c2;
                }
                else
                {
                    if (left)
                        objects[objIndex].c4 = (byte)curActiveBlock;
                    else
                        curActiveBlock = objects[objIndex].c4;

                }
            }
            p.Image = makeObjImage(objIndex);
            pbActive.Image = videoSprites[curSubpalIndex].Images[curActiveBlock];
            lbActive.Text = String.Format("({0:X})", curActiveBlock);
            dirty = true;
        }

        protected void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            PictureBox pb = (PictureBox)cb.Tag;
            int index = (int)pb.Tag;
            objects[index].typeColor = (byte)(objects[index].typeColor & 0xF0 | cb.SelectedIndex);
            pb.Image = makeObjImage((int)pb.Tag);
            dirty = true;
        }

        protected void cbColor_SelectedIndexChangedDt2(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            PictureBox pb = (PictureBox)cb.Tag;
            int index = (int)pb.Tag;
            objects[index / 4].setSubpalleteForDt2(index % 4, cb.SelectedIndex);
            pb.Image = makeObjImageDt2((int)pb.Tag);
            dirty = true;
        }

        protected void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            int index = (int)cb.Tag;
            objects[index].typeColor = (byte)((objects[index].typeColor & 0x0F) | (cb.SelectedIndex << 4));
            dirty = true;
        }

        public Image makeObjImage(int index)
        {
            if (Globals.getGameType() == GameType.DT2)
            {
                return makeObjImageDt2(index);
            }
            Bitmap b = new Bitmap(32, 32);
            var obj = objects[index];
            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawImage(videoSprites[obj.getSubpallete()].Images[obj.c1], new Rectangle(0, 0, 16, 16));
                g.DrawImage(videoSprites[obj.getSubpallete()].Images[obj.c2], new Rectangle(16, 0, 16, 16));
                g.DrawImage(videoSprites[obj.getSubpallete()].Images[obj.c3], new Rectangle(0, 16, 16, 16));
                g.DrawImage(videoSprites[obj.getSubpallete()].Images[obj.c4], new Rectangle(16, 16, 16, 16));
            }
            return b;
        }

        public Image makeObjImageDt2(int index)
        {
            Bitmap b = new Bitmap(32, 32);
            var obj = objects[index];
            var objectForColor = objects[index / 4];
            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawImage(videoSprites[objectForColor.getSubpalleteForDt2(index % 4)].Images[obj.c1], new Rectangle(0, 0, 16, 16));
                g.DrawImage(videoSprites[objectForColor.getSubpalleteForDt2(index % 4)].Images[obj.c2], new Rectangle(16, 0, 16, 16));
                g.DrawImage(videoSprites[objectForColor.getSubpalleteForDt2(index % 4)].Images[obj.c3], new Rectangle(0, 16, 16, 16));
                g.DrawImage(videoSprites[objectForColor.getSubpalleteForDt2(index % 4)].Images[obj.c4], new Rectangle(16, 16, 16, 16));
            }
            return b;
        }

        protected void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X / 16;
            int y = e.Y / 16;
            curActiveBlock = y * 16 + x;
            pbActive.Image = videoSprites[curSubpalIndex].Images[curActiveBlock];
            lbActive.Text = String.Format("({0:X})", curActiveBlock);
            dirty = true;
        }

        protected virtual bool saveToFile()
        {
            byte blockId = getBigBlockNo();
            ConfigScript.setBlocks(blockId, objects);
            dirty = !Globals.flushToFile();
            return !dirty;
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            saveToFile();
        }

        private void cbLevelSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTileset.SelectedIndex == -1)
                return;
            if (!readOnly && dirty)
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
                }
            }
            curActiveBigBlock = cbTileset.SelectedIndex;
            curActiveBlock = 0;
            curSubpalIndex = 0;
            reloadLevel();
        }

        protected void BlockEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!readOnly && dirty)
            {
                DialogResult dr = MessageBox.Show("Tiles was changed. Do you want to save current tileset?", "Save", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                    saveToFile();
            }
        }


        private void returnCbLevelIndexes()
        {
            cbTileset.SelectedIndexChanged -= cbLevelSelect_SelectedIndexChanged;
            cbTileset.SelectedIndex = curActiveBigBlock;
            cbTileset.SelectedIndexChanged += cbLevelSelect_SelectedIndexChanged;
        }

        private void preparePanel()
        {
            //GUI
            mapObjects.Controls.Clear();
            mapObjects.SuspendLayout();
            for (int i = 0; i < ConfigScript.getBlocksCount(); i++)
            {
                Panel fp = new Panel();
                fp.Size = new Size(mapObjects.Width - 25, 32);
                //
                Label lb = new Label();
                lb.Location = new Point(0, 0);
                lb.Size = new Size(24, 32);
                lb.Tag = i;
                lb.Text = String.Format("{0:X}",i);
                fp.Controls.Add(lb);
                //
                PictureBox pb = new PictureBox();
                pb.Location = new Point(24, 0);
                pb.Size = new Size(32, 32);
                pb.Tag = i;
                pb.MouseClick += new MouseEventHandler(pb_MouseClick);
                fp.Controls.Add(pb);
                //
                ComboBox cbColor = new ComboBox();
                cbColor.Size = cbSubpalette.Size;
                cbColor.Location = new Point(60, 0);
                cbColor.Tag = pb;
                cbColor.DrawMode = DrawMode.OwnerDrawVariable;
                cbColor.DrawItem += new DrawItemEventHandler(cbSubpalette_DrawItemEvent);
                cbColor.Items.AddRange(subPalItems);
                cbColor.DropDownStyle = ComboBoxStyle.DropDownList;
                if (Globals.getGameType() != GameType.DT2)
                    cbColor.SelectedIndexChanged += cbColor_SelectedIndexChanged;
                else
                    cbColor.SelectedIndexChanged += cbColor_SelectedIndexChangedDt2;
                fp.Controls.Add(cbColor);
                //
                ComboBox cbType = new ComboBox();
                var objectTypes = ConfigScript.getBlockTypeNames();
                cbType.Items.AddRange(objectTypes);
                cbType.Location = new Point(156, 0);
                cbType.Size = new Size(120, 21);
                cbType.Tag = i;
                cbType.DropDownStyle = ComboBoxStyle.DropDownList;
                if (Globals.getGameType() != GameType.DT2)
                  cbType.SelectedIndexChanged += cbType_SelectedIndexChanged;
                cbType.Enabled = Globals.getGameType() != GameType.DT2;
                fp.Controls.Add(cbType);
                mapObjects.Controls.Add(fp);
            }
            mapObjects.ResumeLayout();
        }

        protected virtual void refillPanel()
        {
            //GUI
            for (int i = 0; i < ConfigScript.getBlocksCount(); i++)
            {
                Panel p = (Panel)mapObjects.Controls[i];
                PictureBox pb = (PictureBox)p.Controls[1];
                pb.Image = makeObjImage(i);
                ComboBox cbColor = (ComboBox)p.Controls[2];
                cbColor.SelectedIndex = Globals.getGameType() == GameType.DT2 ? objects[i/4].getSubpalleteForDt2(i%4) : objects[i].getSubpallete();
                ComboBox cbType = (ComboBox)p.Controls[3];
                cbType.SelectedIndex = objects[i].getType();
            }
        }

        private void VisibleOnlyChange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbVideo.SelectedIndex == -1 || cbPalette.SelectedIndex == -1)
                return;
            curActiveVideo = cbVideo.SelectedIndex + 0x90;
            curActivePal = cbPalette.SelectedIndex;
            reloadLevel(false);
        }

        protected void btClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to clear all blocks?", "Clear", MessageBoxButtons.YesNo)!= DialogResult.Yes)
              return;
            for (int i = 0; i < ConfigScript.getBlocksCount(); i++)
                objects[i] = new ObjRec(0,0,0,0,0);
            dirty = true;
            refillPanel();
        }

        protected virtual byte getBackId()
        {
            byte backId = (byte)curActiveVideo;
            return backId;
        }

        protected void btFlipHorizontal_Click(object sender, EventArgs e)
        {
            var videoChunk = ConfigScript.getVideoChunk(getBackId());
            int beginIndex = 16 * curActiveBlock;
            for (int line = 0; line < 8; line++)
            {
                videoChunk[beginIndex + line    ] = Utils.ReverseBits(videoChunk[beginIndex + line]);
                videoChunk[beginIndex + line + 8] = Utils.ReverseBits(videoChunk[beginIndex + line + 8]); 
            }
            ConfigScript.setVideoChunk(getBackId(), videoChunk);
            reloadLevel(false);
            dirty = true;
        }

        protected void btFlipVertical_Click(object sender, EventArgs e)
        {
            var videoChunk = ConfigScript.getVideoChunk(getBackId());
            int beginIndex = 16 * curActiveBlock;
            Utils.Swap(ref videoChunk[beginIndex + 0], ref videoChunk[beginIndex + 7]);
            Utils.Swap(ref videoChunk[beginIndex + 1], ref videoChunk[beginIndex + 6]);
            Utils.Swap(ref videoChunk[beginIndex + 2], ref videoChunk[beginIndex + 5]);
            Utils.Swap(ref videoChunk[beginIndex + 3], ref videoChunk[beginIndex + 4]);

            Utils.Swap(ref videoChunk[beginIndex + 8], ref videoChunk[beginIndex +15]);
            Utils.Swap(ref videoChunk[beginIndex + 9], ref videoChunk[beginIndex +14]);
            Utils.Swap(ref videoChunk[beginIndex +10], ref videoChunk[beginIndex +13]);
            Utils.Swap(ref videoChunk[beginIndex +11], ref videoChunk[beginIndex +12]);
            ConfigScript.setVideoChunk(getBackId(), videoChunk);
            reloadLevel(false);
            dirty = true;
        }

        protected void btExport_Click(object sender, EventArgs e)
        {
            var f = new SelectFile();
            f.Filename = "exportedBlocks.bin";
            //f.ShowExportParams = true;
            f.ShowDialog();
            if (!f.Result)
                return;
            var fn = f.Filename;
            byte blockId = getBigBlockNo();
            int blocksCount = ConfigScript.getBlocksCount();
            var data = new byte[blocksCount * 5];
            for (int i = 0; i < blocksCount; i++)
            {
                data[i] = objects[i].c1;
                data[blocksCount * 1 + i] = objects[i].c2;
                data[blocksCount * 2 + i] = objects[i].c3;
                data[blocksCount * 3 + i] = objects[i].c4;
                data[blocksCount * 4 + i] = objects[i].typeColor;
            }

            Utils.saveDataToFile(fn, data);
        }

        protected void btImport_Click(object sender, EventArgs e)
        {
            var f = new SelectFile();
            f.Filename = "exportedBlocks.bin";
            f.ShowDialog();
            if (!f.Result)
                return;
            var fn = f.Filename;
            var data = Utils.loadDataFromFile(fn);
            if (data == null)
                return;

            int addr = ConfigScript.getTilesAddr(getBigBlockNo());
            for (int i = 0; i < ConfigScript.getBlocksCount(); i++)
            {
                Globals.romdata[addr + i] = data[i];
                Globals.romdata[addr + 0x100 + i] = data[i + 0x100];
                Globals.romdata[addr + 0x200 + i] = data[i + 0x200];
                Globals.romdata[addr + 0x300 + i] = data[i + 0x300];
                Globals.romdata[addr + 0x400 + i] = data[i + 0x400];
            }
            reloadLevel(false);
            dirty = true;
        }

        protected void cbShowAxis_CheckedChanged(object sender, EventArgs e)
        {
            showAxis = cbShowAxis.Checked;
            reloadLevel(false);
        }

        public void setFormMain(FormMain f)
        {
            formMain = f;
        }
    }
}
