using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CadEditor
{
    public partial class SegaBlockEdit : Form
    {
        public SegaBlockEdit()
        {
            InitializeComponent();
        }

        public void changeModeToBackEdit()
        {
            editMapMode = true;
        }

        private bool editMapMode;

        int curActiveBlock;
        int curActiveTile;
        int curActivePalNo;
        int curSelectedTilePart;
        private bool dirty;

        ushort[] tiles;
        byte[] videoChunk;
        Color[] cpal;

        bool showAxis = true;

        Image[] bigBlocks = new Image[0];

        const int SegaTilesCount = 0x800;
        const int BlockWidth = 32;
        const int BlockHeight = 32;

        private void SegaBlockEdit_Load(object sender, EventArgs e)
        {
            //Change size without event, it will call later
            /*pnBlocks.SizeChanged -= pnBlocks_SizeChanged;
            splitContainer1.Location = new Point(0, 35);
            splitContainer1.Width = this.Width - 21;
            splitContainer1.Height = this.Height - 81;
            pnBlocks.SizeChanged += pnBlocks_SizeChanged;*/
            

            dirty = false;
            reloadTiles();
            UtilsGui.setCbItemsCount(cbPalSubpart, 4);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbPalSubpart, cbPalNo_SelectedIndexChanged);

            UtilsGui.setCbItemsCount(cbBlockNo, getBlocksCount(), inHex:true);
            UtilsGui.setCbItemsCount(cbTile, SegaTilesCount, inHex:true);
            UtilsGui.setCbItemsCount(cbPal, 4);
            UtilsGui.setCbIndexWithoutUpdateLevel(cbBlockNo, cbBlockNo_SelectedIndexChanged);
            resetControls();
        }

        void reloadTiles()
        {
            var mapping = loadMappingData();
            tiles = Mapper.loadMapping(mapping);
        }

        bool saveTiles()
        {
            byte[] tileBytes = new byte[tiles.Length*2];
            Mapper.applyMapping(ref tileBytes, tiles);
            ConfigScript.setSegaMapping(0, tileBytes);
            dirty = !Globals.flushToFile();
            return !dirty;
        }

        bool saveSegaBack()
        {
            byte[] tileBytes = new byte[tiles.Length * 2];
            Mapper.applyMapping(ref tileBytes, tiles);
            ConfigScript.saveSegaBack(tileBytes);
            return true;
        }

        void resetControls()
        {
            fillSegaTiles();
            int tileWidth = getTileWidth();
            int tileHeight = getTileHeight();
            mapScreen.Size = new Size(tileWidth * BlockWidth, tileHeight * BlockHeight);
            updateBlocksImages();
        }

        private void fillSegaTiles()
        {
            videoChunk = ConfigScript.getVideoChunk(0);
            byte[] pal = ConfigScript.getPal(0);
            cpal = ConfigScript.videoSega.getPalette(pal);
            bigBlocks = new Image[SegaTilesCount];
            for (ushort idx = 0; idx < SegaTilesCount; idx++)
            {
                bigBlocks[idx] = ConfigScript.videoSega.getTile(videoChunk, idx, cpal, (byte)curActivePalNo, false, false);
            }
        }

        private void cbPalNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPalSubpart.SelectedIndex == -1)
                return;
            curActivePalNo = cbPalSubpart.SelectedIndex;
            resetControls();
        }

        private void cbBlockNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbBlockNo.SelectedIndex == -1)
                return;
            curActiveBlock = cbBlockNo.SelectedIndex;
            curSelectedTilePart = 0;
            mapScreen.Invalidate();

            updateMappingControls(getCurTileBeginIdx());
        }

        private void updateMappingControls(int index)
        {
            ushort word = tiles[index];
            UtilsGui.setCbIndexWithoutUpdateLevel(cbTile, cbTile_SelectedIndexChanged, Mapper.tileIdx(word));
            UtilsGui.setCbIndexWithoutUpdateLevel(cbPal, cbPal_SelectedIndexChanged, Mapper.palIdx(word));
            UtilsGui.setCbCheckedWithoutUpdateLevel(cbHFlip, cbHFlip_CheckedChanged, Mapper.hf(word));
            UtilsGui.setCbCheckedWithoutUpdateLevel(cbVFlip, cbVFlip_CheckedChanged, Mapper.vf(word));
            UtilsGui.setCbCheckedWithoutUpdateLevel(cbPrior, cbPrior_CheckedChanged, Mapper.p(word));
        }

        private void mapScreen_Paint(object sender, PaintEventArgs e)
        {
            int tileWidth = getTileWidth();
            int tileHeight = getTileHeight(); 
            int tileSize = tileWidth * tileHeight;
            int index = curActiveBlock * tileSize;
            var g = e.Graphics;

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            for (int i = 0; i < tileSize; i++)
            {
                ushort word = tiles[index + i];
                var tileRect = new Rectangle(i % tileWidth * BlockWidth, i / tileWidth * BlockHeight, BlockWidth, BlockHeight);
                ushort tileIdx = Mapper.tileIdx(word);
                byte pal = Mapper.palIdx(word);
                bool hf = Mapper.hf(word);
                bool vf = Mapper.vf(word);
                var b = ConfigScript.videoSega.getTile(videoChunk, tileIdx, cpal, pal, hf, vf);
                g.DrawImage(b, tileRect);
                if (showAxis)
                {
                    g.DrawRectangle(new Pen(Color.FromArgb(255, 255, 255, 255)), tileRect);
                }
                if (i == curSelectedTilePart)
                {
                    g.DrawRectangle(new Pen(Brushes.Red, 2.5f), tileRect);
                }
            }
        }

        private void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int tileWidth = getTileWidth();
            int tileHeight = getTileHeight(); 
            int tileSize = tileWidth * tileHeight;
            int index = curActiveBlock * tileSize;

            int dx = e.X / BlockWidth;
            int dy = e.Y / BlockHeight;
            if (dx < 0 || dx >= tileWidth || dy < 0 || dy >= tileHeight)
                return;
            int tileIdx = dy * tileWidth + dx;
            curSelectedTilePart = tileIdx;
            int changeIndex = getCurTileIdx();
            //
            if (e.Button == MouseButtons.Left)
            {
                ushort w = tiles[changeIndex];
                w = Mapper.applyPalIdx(w, (byte)curActivePalNo);
                w = Mapper.applyTileIdx(w, (ushort)curActiveTile);
                tiles[changeIndex] = w;
                dirty = true;
            }
            else
            {
                curActiveTile = Mapper.tileIdx(tiles[changeIndex]);
                updateActiveTileNo();
                blocksScreen.Invalidate();
            }
            //

            updateMappingControls(index + tileIdx);
            mapScreen.Invalidate();
        }

        private void updateActiveTileNo()
        {
            lbActive.Text = String.Format("TileNo: ({0:X})", curActiveTile);
        }

        private int getCurTileIdx()
        {
            return curSelectedTilePart + curActiveBlock * getCurTileSize();
        }

        private int getCurTileBeginIdx()
        {
            return curActiveBlock * getCurTileSize();
        }

        private int getCurTileSize()
        {
            int tileWidth = getTileWidth();
            int tileHeight = getTileHeight();
            return tileWidth * tileHeight;
        }


        private void cbTile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTile.SelectedIndex == -1)
                return;
            int tileIdx = getCurTileIdx();
            ushort val = (ushort)cbTile.SelectedIndex;
            tiles[tileIdx] = Mapper.applyTileIdx(tiles[tileIdx], val);

            bool altPressed = ModifierKeys == Keys.Alt;
            if (altPressed)
            {
                for (int i = getCurTileBeginIdx(); i < getCurTileBeginIdx()+ getCurTileSize(); i++)
                {
                    tiles[i] = Mapper.applyTileIdx(tiles[i], val);
                }
            }

            mapScreen.Invalidate();
            dirty = true;
        }

        private void cbPal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPal.SelectedIndex == -1)
                return;
            int tileIdx = getCurTileIdx();
            byte val = (byte)cbPal.SelectedIndex;
            tiles[tileIdx] = Mapper.applyPalIdx(tiles[tileIdx], val);

            bool altPressed = ModifierKeys == Keys.Alt;
            if (altPressed)
            {
                for (int i = getCurTileBeginIdx(); i < getCurTileBeginIdx() + getCurTileSize(); i++)
                {
                    tiles[i] = Mapper.applyPalIdx(tiles[i], val);
                }
            }

            mapScreen.Invalidate();
            dirty = true;
        }

        private void cbHFlip_CheckedChanged(object sender, EventArgs e)
        {
            int tileIdx = getCurTileIdx();
            int val = cbHFlip.Checked ? 1 : 0;
            tiles[tileIdx] = Mapper.applyHf(tiles[tileIdx], val);
 
            bool altPressed = ModifierKeys == Keys.Alt;
            if (altPressed)
            {
                for (int i = getCurTileBeginIdx(); i < getCurTileBeginIdx() + getCurTileSize(); i++)
                {
                    tiles[i] = Mapper.applyHf(tiles[i], val);
                }
            }

            mapScreen.Invalidate();
            dirty = true;
        }

        private void cbVFlip_CheckedChanged(object sender, EventArgs e)
        {
            int tileIdx = getCurTileIdx();
            int val = cbVFlip.Checked ? 1 : 0;
            tiles[tileIdx] = Mapper.applyVf(tiles[tileIdx], val);

            bool altPressed = ModifierKeys == Keys.Alt;
            if (altPressed)
            {
                for (int i = getCurTileBeginIdx(); i < getCurTileBeginIdx() + getCurTileSize(); i++)
                {
                    tiles[i] = Mapper.applyVf(tiles[i], val);
                }
            }

            mapScreen.Invalidate();
            dirty = true;
        }

        private void cbPrior_CheckedChanged(object sender, EventArgs e)
        {
            int tileIdx = getCurTileIdx();
            int val = cbVFlip.Checked ? 1 : 0;
            tiles[tileIdx] = Mapper.applyP(tiles[tileIdx], val);

            bool altPressed = ModifierKeys == Keys.Alt;
            if (altPressed)
            {
                for (int i = getCurTileBeginIdx(); i < getCurTileBeginIdx() + getCurTileSize(); i++)
                {
                    tiles[i] = Mapper.applyP(tiles[i], val);
                }
            }

            mapScreen.Invalidate();
            dirty = true;
        }

        private bool saveFunc()
        {
            if (editMapMode)
                return saveSegaBack();
            else
                return saveTiles();
        }

        private void tbbSave_Click(object sender, EventArgs e)
        {
            saveFunc();
        }

        private void SegaBlockEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!UtilsGui.askToSave(ref dirty, saveFunc, null))
            {
                e.Cancel = true;
            }
        }

        private int getBlocksCount()
        {
            return editMapMode ? 1 : ConfigScript.getBigBlocksCount(0, 0);
        }

        private int getTileWidth()
        {
            return editMapMode ? ConfigScript.getSegaBackWidth() : ConfigScript.isBlockSize4x4() ? 4 : 2;
        }

        private int getTileHeight()
        {
            return editMapMode ? ConfigScript.getSegaBackHeight() : ConfigScript.isBlockSize4x4() ? 4 : 2;
        }

        private byte[] loadMappingData()
        {
            return editMapMode ? ConfigScript.loadSegaBack() : ConfigScript.getSegaMapping(0);//curActiveBigBlock;
        }

        private void blocksScreen_Paint(object sender, PaintEventArgs e)
        {
            var visibleRect = UtilsGui.getVisibleRectangle(pnBlocks, blocksScreen);
            var g = e.Graphics;

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

            MapEditor.renderAllBlocks(g, blocksScreen, bigBlocks, BlockWidth, BlockHeight, visibleRect, 1.0f, curActiveTile, showAxis);
        }

        private void blocksScreen_MouseDown(object sender, MouseEventArgs e)
        {
            var p = blocksScreen.PointToClient(Cursor.Position);
            int x = p.X, y = p.Y;
            int TILE_SIZE_X = (int)(BlockWidth * 1.0f);
            int TILE_SIZE_Y = (int)(BlockHeight * 1.0f);
            int tx = x / TILE_SIZE_X, ty = y / TILE_SIZE_Y;
            int maxtX = blocksScreen.Width / TILE_SIZE_X;
            int index = ty * maxtX + tx;
            if ((tx < 0) || (tx >= maxtX) || (index < 0) || (index > bigBlocks.Length))
            {
                return;
            }
            //!
            curActiveTile = index;
            updateActiveTileNo();
            blocksScreen.Invalidate();
        }

        private void updateBlocksImages()
        {
            UtilsGui.resizeBlocksScreen(bigBlocks, blocksScreen, BlockWidth, BlockHeight, 1.0f);
            blocksScreen.Invalidate();
        }

        private void pnBlocks_SizeChanged(object sender, EventArgs e)
        {
            updateBlocksImages();
        }

        private void pnView_SizeChanged(object sender, EventArgs e)
        {
            pnMapping.Location = new Point(pnViewScroll.Location.X, pnViewScroll.Location.Y + pnViewScroll.Height);
        }

        private void tbbShowAxis_CheckedChanged(object sender, EventArgs e)
        {
            showAxis = tbbShowAxis.Checked;
            mapScreen.Invalidate();
            blocksScreen.Invalidate();
        }
    }
}
