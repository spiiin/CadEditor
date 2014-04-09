using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CadEditor
{
    public partial class FormStructures : Form
    {
        public FormStructures()
        {
            InitializeComponent();
        }

        int blockWidth;
        int blockHeight;
        int curButtonScale;
        int curActiveBlock;
        float curScale;
        //MapViewType curViewType;
        //bool showAxis;
        TileStructure curTileStruct;
        static List<TileStructure> tileStructs = new List<TileStructure>();
        FormMain formMain;

        private void FormStructures_Load(object sender, EventArgs e)
        {
            blockWidth = ConfigScript.getBlocksPicturesWidth();
            blockHeight = 32;
            curButtonScale = 2;
            curActiveBlock = 0;
            //curViewType = MapViewType.Tiles;
            curScale = 2.0f;
            //showAxis = true;
            curTileStruct = null;
            resetControls(true);
        }

        void resetControls(bool needToRefillBlockPanel)
        {
            lbStructures.Items.Clear();
            for (int i = 0; i < tileStructs.Count; i++)
                lbStructures.Items.Add(tileStructs[i].Name);
            bigBlocks = formMain.getBigBlockImageList();
            //Utils.setBlocks(bigBlocks, curButtonScale, blockWidth, blockHeight, curViewType, showAxis);
            if (needToRefillBlockPanel)
                Utils.prepareBlocksPanel(blocksPanel, new Size((int)(blockWidth * curButtonScale + 1), (int)(blockHeight * curButtonScale + 1)), bigBlocks, buttonBlockClick);
            else
                Utils.reloadBlocksPanel(blocksPanel, bigBlocks);
            resetTileStructControls();
        }

        private void resetTileStructControls()
        {
            if (curTileStruct != null)
            {
                Utils.setCbItemsCount(cbWidth, 8, 1);
                Utils.setCbItemsCount(cbHeight, 8, 1);
                Utils.setCbIndexWithoutUpdateLevel(cbWidth, cbWidth_SelectedIndexChanged, curTileStruct.Width - 1);
                Utils.setCbIndexWithoutUpdateLevel(cbHeight, cbWidth_SelectedIndexChanged, curTileStruct.Height - 1);
            }
        }

        private void buttonBlockClick(Object button, EventArgs e)
        {
            int index = ((Button)button).ImageIndex;
            //activeBlock.Image = bigBlocks.Images[index];
            curActiveBlock = index;
            //lbActiveBlock.Text = String.Format("Label: ({0:X})", index);
        }

        private void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            if (curTileStruct == null)
                return;
            int WIDTH = curTileStruct.Width;
            int HEIGHT = curTileStruct.Height;
            int dx, dy;
            if (ConfigScript.getScreenVertical())
            {
                dy = e.X / (int)(blockWidth * curScale);
                dx = e.Y / (int)(blockHeight * curScale);
            }
            else
            {
                dx = e.X / (int)(blockWidth * curScale);
                dy = e.Y / (int)(blockHeight * curScale);
            }

            if (dx < 0 || dx >= curTileStruct.Width || dy < 0 || dy >= curTileStruct.Height)
                return;

            if (e.Button == MouseButtons.Left)
                curTileStruct[dx, dy] = curActiveBlock;
            else
                curActiveBlock = curTileStruct[dx, dy];

            mapScreen.Invalidate();
        }

        private void mapScreen_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            if (curTileStruct == null)
            {
                g.Clear(Color.Black);
                return;
            }
            int WIDTH = curTileStruct.Width;
            int HEIGHT = curTileStruct.Height;
            int TILE_SIZE_X = (int)(blockWidth * curScale);
            int TILE_SIZE_Y = (int)(blockHeight * curScale);
            var visibleRect = Utils.getVisibleRectangle(this, mapScreen);

            g.Clear(Color.Black);
            for (int x = 0; x < WIDTH; x++)
            {
                for (int y = 0; y < HEIGHT; y++)
                {
                    int index = curTileStruct[x, y];
                    Rectangle tileRect;
                    if (ConfigScript.getScreenVertical())
                        tileRect = new Rectangle(y * TILE_SIZE_X, x * TILE_SIZE_Y, TILE_SIZE_X, TILE_SIZE_Y);
                    else
                        tileRect = new Rectangle(x * TILE_SIZE_X, y * TILE_SIZE_Y, TILE_SIZE_X, TILE_SIZE_Y);

                    if ((visibleRect.Contains(tileRect)) || (visibleRect.IntersectsWith(tileRect)))
                    {
                        if (index == -1)
                            g.FillRectangle(Brushes.White, tileRect);
                        else if (index < bigBlocks.Images.Count)
                            g.DrawImage(bigBlocks.Images[index], tileRect);
                    }
                }
            }
        }

        private void cbWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int w = cbWidth.SelectedIndex + 1;
            int h = cbHeight.SelectedIndex + 1;
            curTileStruct.resetDim(w, h);
            mapScreen.Invalidate();
        }

        private void btAddStructure_Click(object sender, EventArgs e)
        {
            var ts = new TileStructure("Struct" + tileStructs.Count.ToString(), 4, 4);
            tileStructs.Add(ts);
            lbStructures.Items.Add(ts.Name);
        }

        private void lbStructures_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lbStructures.SelectedIndex;
            if (index == -1)
                return;
            curTileStruct = tileStructs[index];

            resetTileStructControls();
            mapScreen.Invalidate();
        }

        private void btRemoveStructure_Click(object sender, EventArgs e)
        {
            if (lbStructures.SelectedIndices.Count != 1)
                return;
            if (MessageBox.Show("Do you really want to delete structure?", "Warning", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;
            int index = lbStructures.SelectedIndex;
            curTileStruct = null;
            resetTileStructControls();
            lbStructures.Items.RemoveAt(index);
            tileStructs.RemoveAt(index);
            mapScreen.Invalidate();
        }

        private void serializeStructs(string filename)
        {
            try
            {
                using (Stream stream = File.Open(filename, FileMode.Create))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    bin.Serialize(stream, tileStructs);
                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Error while save structures:" + e.Message, "Error");
            }
        }

        private void deserializeStructs(string filename)
        {
            try
            {
                using (Stream stream = File.Open(filename, FileMode.Open))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    tileStructs = (List<TileStructure>)bin.Deserialize(stream);
                }
            }
            catch (IOException)
            {
            }
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
              serializeStructs(saveFileDialog1.FileName);
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                deserializeStructs(openFileDialog1.FileName);
                lbStructures.Items.Clear();
                for (int i = 0; i < tileStructs.Count; i++)
                    lbStructures.Items.Add(tileStructs[i].Name);
            }
        }

        private void lbStructures_DoubleClick(object sender, EventArgs e)
        {
            int index = lbStructures.SelectedIndex;
            if (index == -1)
                return;
            var f = new FormStructuresName();
            FormStructuresName.StructName = curTileStruct.Name;
            FormStructuresName.StructWidth = curTileStruct.Width;
            FormStructuresName.StructHeight = curTileStruct.Height;
            f.ShowDialog();
            if (FormStructuresName.Result)
            {
                var ts = tileStructs[index];
                ts.Name = FormStructuresName.StructName;
                ts.resetDim(FormStructuresName.StructWidth, FormStructuresName.StructHeight);
                lbStructures.Items[index] = ts.Name;
                lbStructures_SelectedIndexChanged(sender, new EventArgs());
            }
        }

        private void FormStructures_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool closed = MessageBox.Show("Do you really want to close form (check that you save results)", "Attention", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes;
            e.Cancel = !closed;
        }

        public void setFormMain(FormMain f)
        {
            formMain = f;
        }

    }

    [Serializable]
    public class TileStructure
    {
        public TileStructure(string name, int width, int height)
        {
            this.name = name;
            this.width = width;
            this.height = height;
            tileIndexes = new int[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    tileIndexes[x, y] = -1;
        }

        public void resetDim(int width, int height)
        {
            int[,] newIndexes = new int[width,height];
            int endWidth = Math.Min(width, this.width);
            int endHeight = Math.Min(height, this.height);
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    newIndexes[x, y] = -1;
            for (int x = 0; x < endWidth; x++)
                for (int y = 0; y < endHeight; y++)
                    newIndexes[x,y] = tileIndexes[x,y];
            tileIndexes = newIndexes;
            this.width = width;
            this.height = height;
        }

        public int this[int x, int y]
        {
            get { return tileIndexes[x, y]; }
            set { tileIndexes[x,y] = value; }
        }

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public string Name { get { return name; } set { name = value; } }
        string name;
        int width;
        int height;
        int[,] tileIndexes;
    }
}
