using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CadEditor
{
    public partial class BlockEditCad : Form
    {
        public BlockEditCad()
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
            Utils.setCbIndexWithoutUpdateLevel(cbLevelSelect, cbLevelSelect_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbDoor, VisibleOnlyChange_SelectedIndexChanged);
            Utils.setCbIndexWithoutUpdateLevel(cbSubpalette, cbSubpalette_SelectedIndexChanged);

            updatePanelsVisible();
            reloadLevel();

            readOnly = false; //must be read from config
            btSave.Enabled = !readOnly;
            lbReadOnly.Visible = readOnly;
            btFlipHorizontal.Visible = !readOnly;
            btFlipVertical.Visible = !readOnly;
            btImport.Visible = !readOnly;
            //btExport.Visible = !readOnly;
        }

        private void reloadLevel(bool resetDirty = true)
        {
            setPal();
            setVideo();
            setVideoImage();
            setObjects();

            setBack();

            pbBacks.Refresh();
            if (resetDirty)
              dirty = false;
        }

        private void setBack()
        {
            int backAddr = Globals.getBackTileAddr(curActiveLevel);
            for (int i = 0; i < 16; i++)
              curActiveBack[i] = Globals.romdata[backAddr + i];
        }

        private void setPal()
        {
            byte palId;
            if (curDoor <= 0)
                palId = (byte)GlobalsCad.levelData[curActiveLevel].palId;
            else
                palId = (byte)GlobalsCad.doorsData[curDoor - 1].palId;
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

        private void setObjects()
        {
            byte bigBlockId = (byte)GlobalsCad.levelData[curActiveLevel].bigBlockId;
            objects = ConfigScript.getBlocks(bigBlockId);
            refillPanel();
        }

        private void setVideo()
        {
            byte backId = getBackId();
            for (int i = 0; i < 4; i++)
            {
                Bitmap b = ConfigScript.videoNes.makeImageStrip(ConfigScript.getVideoChunk(backId), palette, i, 2, !showAxis);
                videoSprites[i].Images.Clear();
                videoSprites[i].Images.AddStrip(b);
            }
        }

        private void setVideoImage()
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

        //cad specific
        private int curActiveLevel = 0;
        private int curDoor = 0;
        //editor
        private int curSubpalIndex = 0;
        private int curActiveBlock = 0;

        private byte[] palette = new byte[Globals.PAL_LEN];
        private ObjRec[] objects = new ObjRec[256];
        private ImageList[] videoSprites = new ImageList[4];
        private byte[] curActiveBack = new byte[16];
        private bool dirty;
        private bool readOnly;
        private bool showAxis;

        private string[] subPalItems = { "1", "2", "3", "4" };

        private FormMain formMain;

        private void cbSubpalette_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSubpalette.SelectedIndex == -1)
                return;
            curSubpalIndex = cbSubpalette.SelectedIndex;
            setVideoImage();
        }

        private void cbSubpalette_DrawItemEvent(object sender, DrawItemEventArgs e)
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

        void pb_MouseClick(object sender, MouseEventArgs e)
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
            dirty = true;
        }

        void cbColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            PictureBox pb = (PictureBox)cb.Tag;
            int index = (int)pb.Tag;
            objects[index].typeColor = (byte)(objects[index].typeColor & 0xF0 | cb.SelectedIndex);
            pb.Image = makeObjImage((int)pb.Tag);
            dirty = true;
        }

        void cbColor_SelectedIndexChangedDt2(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            PictureBox pb = (PictureBox)cb.Tag;
            int index = (int)pb.Tag;
            objects[index / 4].setSubpalleteForDt2(index % 4, cb.SelectedIndex);
            pb.Image = makeObjImageDt2((int)pb.Tag);
            dirty = true;
        }

        void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            int index = (int)cb.Tag;
            objects[index].typeColor = (byte)((objects[index].typeColor & 0x0F) | (cb.SelectedIndex << 4));
            //try
            int t = objects[index].getType();
            var pbBack = (PictureBox)mapObjects.Controls[index].Controls[4];
            pbBack.Visible = t >= 0x0E || t == 1;
            dirty = true;
        }

        public Image makeObjImage(int index)
        {
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

        private void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X / 16;
            int y = e.Y / 16;
            curActiveBlock = y * 16 + x;
            pbActive.Image = videoSprites[curSubpalIndex].Images[curActiveBlock];
            dirty = true;
        }

        private byte getBlockId()
        {
            byte blockId;
            blockId = (byte)GlobalsCad.levelData[curActiveLevel].bigBlockId;
            return blockId;
        }

        private bool saveToFile()
        {
            byte blockId = getBlockId();
            ConfigScript.setBlocks(blockId, objects);
            int backAddr = Globals.getBackTileAddr(curActiveLevel);
            for (int i = 0; i < 16; i++)
                Globals.romdata[backAddr + i] = curActiveBack[i];
            dirty = !Globals.flushToFile();
            return !dirty;
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            saveToFile();
        }

        private void cbLevelSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLevelSelect.SelectedIndex == -1)
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
            curActiveLevel = cbLevelSelect.SelectedIndex;
            curActiveBlock = 0;
            curSubpalIndex = 0;
            reloadLevel();
        }

        private void BlockEdit_FormClosing(object sender, FormClosingEventArgs e)
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
            cbLevelSelect.SelectedIndexChanged -= cbLevelSelect_SelectedIndexChanged;
            cbLevelSelect.SelectedIndex = curActiveLevel;
            cbLevelSelect.SelectedIndexChanged += cbLevelSelect_SelectedIndexChanged;
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
                cbColor.SelectedIndexChanged += cbColor_SelectedIndexChanged;
                fp.Controls.Add(cbColor);
                //
                ComboBox cbType = new ComboBox();
                var objectTypes = ConfigScript.getBlockTypeNames();
                cbType.Items.AddRange(objectTypes);
                cbType.Location = new Point(156, 0);
                cbType.Size = new Size(120, 21);
                cbType.Tag = i;
                cbType.DropDownStyle = ComboBoxStyle.DropDownList;
                cbType.SelectedIndexChanged += cbType_SelectedIndexChanged;
                cbType.Enabled = true;
                 fp.Controls.Add(cbType);
                //
                PictureBox pb2 = new PictureBox();
                pb2.Location = new Point(280, 0);
                pb2.Size = new Size(32, 32);
                pb2.Tag = i;
                pb2.Visible = false;
                fp.Controls.Add(pb2);
                //
                mapObjects.Controls.Add(fp);
            }
            mapObjects.ResumeLayout();
        }

        private void refillPanel()
        {
            for (int i = 0; i < ConfigScript.getBlocksCount(); i++)
            {
                Panel p = (Panel)mapObjects.Controls[i];
                PictureBox pb = (PictureBox)p.Controls[1];
                pb.Image = makeObjImage(i);
                ComboBox cbColor = (ComboBox)p.Controls[2];
                cbColor.SelectedIndex = objects[i].getSubpallete();
                ComboBox cbType = (ComboBox)p.Controls[3];
                cbType.SelectedIndex = objects[i].getType();

                PictureBox pb2 = (PictureBox)p.Controls[4];
                pb2.Image = makeObjImage(curActiveBack[i % 16]);
                int t = objects[i].getType();
                pb2.Visible = (t >= 0x0E || t == 1);
            }
        }

        private void VisibleOnlyChange_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDoor.SelectedIndex == -1)
                return;
            curDoor = cbDoor.SelectedIndex;
            reloadLevel(false);
        }

        private void pbBacks_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            for (int i = 0; i < 16; i++)
                g.DrawImage(makeObjImage(curActiveBack[i]), new Point(i % 8 * 32 + i % 8, i / 8 * 32));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var f = new BoxBackForm();
            f.setParentForm(this);
            f.ShowDialog();
            reloadLevel(false);
        }

        public int getActiveLevel()
        {
            return curActiveLevel;
        }

        private void updatePanelsVisible()
        {
            pnCad.Visible = true;
            pnBacks.Visible = true;
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to clear all blocks?", "Clear", MessageBoxButtons.YesNo)!= DialogResult.Yes)
              return;
            for (int i = 0; i < ConfigScript.getBlocksCount(); i++)
                objects[i] = new ObjRec(0,0,0,0,0);
            dirty = true;
            refillPanel();
        }

        private byte getBackId()
        {
            byte backId;
            if (curDoor <= 0)
                backId = (byte)GlobalsCad.levelData[curActiveLevel].backId;
            else
                backId = (byte)GlobalsCad.doorsData[curDoor - 1].backId;
            return backId;
        }

        private void btFlipHorizontal_Click(object sender, EventArgs e)
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

        private void btFlipVertical_Click(object sender, EventArgs e)
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

        private void btExport_Click(object sender, EventArgs e)
        {
            var f = new SelectFile();
            f.Filename = "exportedBlocks.bin";
            //f.ShowExportParams = true;
            f.ShowDialog();
            if (!f.Result)
                return;
            var fn = f.Filename;
            byte blockId = getBlockId();
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

        private void btImport_Click(object sender, EventArgs e)
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

            byte bigBlockId = (byte)GlobalsCad.levelData[curActiveLevel].bigBlockId;
            int addr = Globals.getTilesAddr(bigBlockId);
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

        private void cbShowAxis_CheckedChanged(object sender, EventArgs e)
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
