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
        int curActiveBlock;
        float curScale;
        //MapViewType curViewType;
        //bool showAxis;
        Image[] bigBlocks;
        TileStructure curTileStruct;
        static List<TileStructure> tileStructs = new List<TileStructure>();
        FormMain formMain;

        private void FormStructures_Load(object sender, EventArgs e)
        {
            blockWidth = ConfigScript.getBlocksPicturesWidth();
            blockHeight = 32;
            curActiveBlock = 0;
            //curViewType = MapViewType.Tiles;
            curScale = 2.0f;
            //showAxis = true;
            curTileStruct = null;
            resetControls();
        }

        void resetControls()
        {
            lbStructures.Items.Clear();
            for (int i = 0; i < tileStructs.Count; i++)
                lbStructures.Items.Add(tileStructs[i].name);
            bigBlocks = formMain.getBigBlockImages();
            UtilsGui.resizeBlocksScreen(bigBlocks, blocksScreen, blockWidth, blockHeight, curScale);
            resetTileStructControls();
        }

        private void resetTileStructControls()
        {
            if (curTileStruct != null)
            {
                UtilsGui.setCbItemsCount(cbWidth, 64, 1);
                UtilsGui.setCbItemsCount(cbHeight, 64, 1);
                UtilsGui.setCbIndexWithoutUpdateLevel(cbWidth, cbWidth_SelectedIndexChanged, curTileStruct.width - 1);
                UtilsGui.setCbIndexWithoutUpdateLevel(cbHeight, cbWidth_SelectedIndexChanged, curTileStruct.height - 1);
            }
        }

        private void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            if (curTileStruct == null)
                return;
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

            if (dx < 0 || dx >= curTileStruct.width || dy < 0 || dy >= curTileStruct.height)
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
            var visibleRect = UtilsGui.getVisibleRectangle(this, mapScreen);
            g.Clear(Color.Black);
            var blockLayer = new BlockLayer(curTileStruct.toArray());
            blockLayer.showLayer = true;
            var screens = new[] { new Screen(blockLayer, curTileStruct.width, curTileStruct.height) };
            MapEditor.render(g, bigBlocks, visibleRect, screens, 0, 2.0f, false, formMain.showAxis, 0, 0, curTileStruct.width, curTileStruct.height, false);
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
            lbStructures.Items.Add(ts.name);
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
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
              serializeStructs(saveFileDialog1.FileName);
        }

        private void btLoad_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                deserializeStructs(openFileDialog1.FileName);
                lbStructures.Items.Clear();
                for (int i = 0; i < tileStructs.Count; i++)
                    lbStructures.Items.Add(tileStructs[i].name);
            }
        }

        private void lbStructures_DoubleClick(object sender, EventArgs e)
        {
            int index = lbStructures.SelectedIndex;
            if (index == -1)
                return;
            var f = new FormStructuresName();
            FormStructuresName.structName = curTileStruct.name;
            FormStructuresName.structWidth = curTileStruct.width;
            FormStructuresName.structHeight = curTileStruct.height;
            f.ShowDialog();
            if (FormStructuresName.result)
            {
                var ts = tileStructs[index];
                ts.name = FormStructuresName.structName;
                ts.resetDim(FormStructuresName.structWidth, FormStructuresName.structHeight);
                lbStructures.Items[index] = ts.name;
                lbStructures_SelectedIndexChanged(sender, new EventArgs());
            }
        }

        public static void addTileStruct(int[][] indexes)
        {
            var ts = new TileStructure("NewStruct", indexes[0].Length, indexes.Length);
            for (int i = 0; i < indexes.Length; i++)
            {
                for (int j = 0; j < indexes[i].Length; j++)
                    ts[j,i] = indexes[i][j];
            }
            //
            /*var f = new FormStructuresName();
            FormStructuresName.StructName = ts.Name;
            FormStructuresName.StructWidth = ts.Width;
            FormStructuresName.StructHeight = ts.Height;
            f.ShowDialog();
            //
            if (FormStructuresName.Result)
            {
                ts.Name = FormStructuresName.StructName;
                ts.resetDim(FormStructuresName.StructWidth, FormStructuresName.StructHeight);
                tileStructs.Add(ts);
                lbStructures.Items.Add(ts.Name);
                lbStructures_SelectedIndexChanged(lbStructures, new EventArgs());
            }*/
            tileStructs.Add(ts);
        }

        private void FormStructures_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool closed = MessageBox.Show("Do you really want to close form (check that you save results)", "Attention", MessageBoxButtons.YesNo) == DialogResult.Yes;
            e.Cancel = !closed;
        }

        public static List<TileStructure> getTileStructures()
        {
            return tileStructs;
        }

        public void setFormMain(FormMain f)
        {
            formMain = f;
        }

        private void blocksScreen_Paint(object sender, PaintEventArgs e)
        {
            var visibleRect = UtilsGui.getVisibleRectangle(pnBlocks, blocksScreen);
            MapEditor.renderAllBlocks(e.Graphics, blocksScreen, bigBlocks, blockWidth, blockHeight, visibleRect, curScale, curActiveBlock, formMain.showAxis);
        }

        private void blocksScreen_MouseDown(object sender, MouseEventArgs e)
        {
            var p = blocksScreen.PointToClient(Cursor.Position);
            int x = p.X, y = p.Y;
            int tileSizeX = (int)(blockWidth * curScale);
            int tileSizeY = (int)(blockHeight * curScale);
            int tx = x / tileSizeX, ty = y / tileSizeY;
            int maxtX = blocksScreen.Width / tileSizeX;
            int index = ty * maxtX + tx;
            if ((tx < 0) || (tx >= maxtX) || (index < 0) || (index > bigBlocks.Length))
            {
                return;
            }

            curActiveBlock = index;
            blocksScreen.Invalidate();
        }

        private void FormStructures_Resize(object sender, EventArgs e)
        {
            blocksScreen.Invalidate();
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

        public void resetDim(int widthD, int heightD)
        {
            int[,] newIndexes = new int[widthD,heightD];
            int endWidth = Math.Min(widthD, width);
            int endHeight = Math.Min(heightD, height);
            for (int x = 0; x < widthD; x++)
                for (int y = 0; y < heightD; y++)
                    newIndexes[x, y] = -1;
            for (int x = 0; x < endWidth; x++)
                for (int y = 0; y < endHeight; y++)
                    newIndexes[x,y] = tileIndexes[x,y];
            tileIndexes = newIndexes;
            width = widthD;
            height = heightD;
        }

        public int this[int x, int y]
        {
            get { return tileIndexes[x, y]; }
            set { tileIndexes[x,y] = value; }
        }

        public int[] toArray() //inverted
        {
            int[] arr = new int[width * height];
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    arr[j*width + i] = tileIndexes[i,j];
            //Buffer.BlockCopy(tileIndexes, 0, arr, 0, width * height * sizeof(int));
            return arr;
        }

        public int width { get; private set; }
        public int height { get; private set; }
        public string name { get; set; }
        int[,] tileIndexes;
    }
}
