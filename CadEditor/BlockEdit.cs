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
    public partial class BlockEdit : Form
    {
        public BlockEdit()
        {
            InitializeComponent();
        }

        private void BlockEdit_Load(object sender, EventArgs e)
        {
            cbSubpalette.DrawItem += new DrawItemEventHandler(cbSubpalette_DrawItemEvent);
            videoSprites[0] = videoSprites1;
            videoSprites[1] = videoSprites2;
            videoSprites[2] = videoSprites3;
            videoSprites[3] = videoSprites4;
            dirty = false;
            preparePanel();
            cbLevelSelect.SelectedIndex = 0;
            cbDoor.SelectedIndex = 0;
            cbSubpalette.SelectedIndex = 0;
        }

        private void reloadLevel(bool resetDirty = true)
        {
            setPal();
            setVideo();
            setVideoImage();
            setObjects();
            if (resetDirty)
              dirty = false;
        }

        private void setPal()
        {
            byte palId;
            if (curDoor <= 0)
                palId = (byte)Globals.levelData[curActiveLevel].palId;
            else
                palId = (byte)Globals.doorsData[curDoor - 1].palId;
            int addr = Globals.getPalAddr(palId);
            for (int i = 0; i < Globals.PAL_LEN; i++)
                palette[i] = Globals.romdata[addr + i];

            //set image for pallete
            var b = new Bitmap(16 * 16, 16);
            using (Graphics g = Graphics.FromImage(b))
            {
                for (int i = 0; i < Globals.PAL_LEN; i++)
                    g.FillRectangle(new SolidBrush(Video.NesColors[palette[i]]), i * 16, 0, 16, 16);
            }
            paletteMap.Image = b;

            //set images for subpalletes
            subpalSprites.Images.Clear();
            for (int i = 0; i < 4; i++)
            {
                var sb = new Bitmap(16 * 4, 16);
                using (Graphics g = Graphics.FromImage(sb))
                {
                    g.FillRectangle(new SolidBrush(Video.NesColors[palette[i * 4]]), 0, 0, 16, 16);
                    g.FillRectangle(new SolidBrush(Video.NesColors[palette[i * 4 + 1]]), 16, 0, 16, 16);
                    g.FillRectangle(new SolidBrush(Video.NesColors[palette[i * 4 + 2]]), 32, 0, 16, 16);
                    g.FillRectangle(new SolidBrush(Video.NesColors[palette[i * 4 + 3]]), 48, 0, 16, 16);
                }
                subpalSprites.Images.Add(sb);
            }
        }

        private void setObjects()
        {
            byte bigBlockId = (byte)Globals.levelData[curActiveLevel].bigBlockId;
            int addr = Globals.getTilesAddr(bigBlockId);
            for (int i = 0; i < Globals.OBJECTS_COUNT; i++)
            {
                byte c1 = Globals.romdata[addr + i];
                byte c2 = Globals.romdata[addr + 0x100 + i];
                byte c3 = Globals.romdata[addr + 0x200 + i];
                byte c4 = Globals.romdata[addr + 0x300 + i];
                byte typeColor = Globals.romdata[addr + 0x400 + i];
                objects[i] = new ObjRec(c1, c2, c3, c4, typeColor);
            }

            refillPanel();
        }

        private void setVideo()
        {
            byte[] videoChunk = new byte[Globals.VIDEO_PAGE_SIZE];
            byte backId;
            if (curDoor <= 0)
                backId = (byte)Globals.levelData[curActiveLevel].backId;
            else
                backId = (byte)Globals.doorsData[curDoor - 1].backId; 
            int videoAddr = Globals.getVideoPageAddr(backId);
            for (int i = 0; i < Globals.VIDEO_PAGE_SIZE; i++)
            {
                videoChunk[i] = Globals.romdata[videoAddr + i];
            }
            for (int i = 0; i < 4; i++)
            {
                Bitmap b = Video.makeImageStrip(videoChunk, palette, i, 2);
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
                    g.DrawImage(videoSprites[curSubpalIndex].Images[i], new Point(x * 16, y * 16));
                }
            }
            mapScreen.Image = b;
        }

        private int curActiveLevel = 0;
        private int curSubpalIndex = 0;
        private int curActiveBlock = 0;
        private int curDoor = 0;

        private byte[] palette = new byte[Globals.PAL_LEN];
        private ObjRec[] objects = new ObjRec[256];
        private ImageList[] videoSprites = new ImageList[4];
        private bool dirty;

        private string[] subPalItems = { "1", "2", "3", "4" };

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
            int x = e.X / 16;
            int y = e.Y / 16;
            PictureBox p = (PictureBox)sender;
            int objIndex = (int)p.Tag;
            if (x == 0)
            {
                if (y == 0)
                    objects[objIndex].c1 = (byte)curActiveBlock;
                else
                    objects[objIndex].c3 = (byte)curActiveBlock;
            }
            else
            {
                if (y == 0)
                    objects[objIndex].c2 = (byte)curActiveBlock;
                else
                    objects[objIndex].c4 = (byte)curActiveBlock;
            }
            p.Image = makeObjImage(objIndex);
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

        void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            int index = (int)cb.Tag;
            objects[index].typeColor = (byte)((objects[index].typeColor & 0x0F) | (cb.SelectedIndex << 4));
            dirty = true;
        }

        Image makeObjImage(int index)
        {
            Bitmap b = new Bitmap(32, 32);
            var obj = objects[index];
            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawImage(videoSprites[obj.getSubpallete()].Images[obj.c1], new Point(0, 0));
                g.DrawImage(videoSprites[obj.getSubpallete()].Images[obj.c2], new Point(16, 0));
                g.DrawImage(videoSprites[obj.getSubpallete()].Images[obj.c3], new Point(0, 16));
                g.DrawImage(videoSprites[obj.getSubpallete()].Images[obj.c4], new Point(16, 16));
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

        private bool saveToFile()
        {
            byte blockId = (byte)Globals.levelData[curActiveLevel].bigBlockId;
            int addr = Globals.getTilesAddr(blockId);
            for (int i = 0; i < Globals.OBJECTS_COUNT; i++)
            {
                Globals.romdata[addr + i] = objects[i].c1;
                Globals.romdata[addr + 0x100 + i] = objects[i].c2;
                Globals.romdata[addr + 0x200 + i] = objects[i].c3;
                Globals.romdata[addr + 0x300 + i] = objects[i].c4;
                Globals.romdata[addr + 0x400 + i] = objects[i].typeColor;
            }

            string romFname = "Chip 'n Dale Rescue Rangers (U) [!].nes";
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

        private void button1_Click(object sender, EventArgs e)
        {
            saveToFile();
        }

        private void cbLevelSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLevelSelect.SelectedIndex == -1)
                return;
            if (dirty)
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
            if (dirty)
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
            mapObjects.SuspendLayout();
            var objectTypes =
                new[]  {
                    "0 (back)",
                    "1 (collect)",
                    "2 (platform)",
                    "3 (block)",
                    "4 (spikes)",
                    "5 (door)",
                    "6 (mask)",
                    "7 (? block and go up)",
                    "8 (? block and go down)",
                    "9 (? block and go down)",
                    "A (Block)",
                    "B (Pit)",
                    "C (Block)",
                    "D (Block)",
                    "E (throwable stone)",
                    "F (throwable box)"
                };
            for (int i = 0; i < Globals.OBJECTS_COUNT; i++)
            {
                Panel fp = new Panel();
                fp.Size = new Size(mapObjects.Width - 25, 32);
                //
                PictureBox pb = new PictureBox();
                pb.Location = new Point(0, 0);
                pb.Size = new Size(32, 32);
                pb.Tag = i;
                pb.MouseClick += new MouseEventHandler(pb_MouseClick);
                fp.Controls.Add(pb);
                //
                ComboBox cbColor = new ComboBox();
                cbColor.Size = cbSubpalette.Size;
                cbColor.Location = new Point(36, 0);
                cbColor.Tag = pb;
                cbColor.DrawMode = DrawMode.OwnerDrawVariable;
                cbColor.DrawItem += new DrawItemEventHandler(cbSubpalette_DrawItemEvent);
                cbColor.Items.AddRange(subPalItems);
                cbColor.DropDownStyle = ComboBoxStyle.DropDownList;
                cbColor.SelectedIndexChanged += cbColor_SelectedIndexChanged;
                fp.Controls.Add(cbColor);
                //
                ComboBox cbType = new ComboBox();
                cbType.Items.AddRange(objectTypes);
                cbType.Location = new Point(130, 0);
                cbType.Size = new Size(120, 21);
                cbType.Tag = i;
                cbType.DropDownStyle = ComboBoxStyle.DropDownList;
                cbType.SelectedIndexChanged += cbType_SelectedIndexChanged;
                fp.Controls.Add(cbType);
                //
                mapObjects.Controls.Add(fp);
            }
            mapObjects.ResumeLayout();
        }

        private void refillPanel()
        {
            //GUI
            for (int i = 0; i < Globals.OBJECTS_COUNT; i++)
            {
                Panel p = (Panel)mapObjects.Controls[i];
                PictureBox pb = (PictureBox)p.Controls[0];
                pb.Image = makeObjImage(i);
                ComboBox cbColor = (ComboBox)p.Controls[1];
                cbColor.SelectedIndex = objects[i].getSubpallete();
                ComboBox cbType = (ComboBox)p.Controls[2];
                cbType.SelectedIndex = objects[i].getType();
            }
        }

        private void cbDoor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDoor.SelectedIndex !=-1)
            {
              curDoor = cbDoor.SelectedIndex;
              reloadLevel(false);
            }
        }
    }
}
