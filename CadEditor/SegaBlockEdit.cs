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

        private bool editMapMode = false;

        int curActiveBlock = 0;
        int curActiveTile = 0;
        int curActivePalNo = 0;
        float curScale = 2.0f;
        int curSelectedTilePart = 0;
        private bool dirty;

        ushort[] tiles;
        byte[] videoChunk;
        Color[] cpal;

        const int SEGA_TILES_COUNT = 0x800;
        const int BLOCK_WIDTH = 32;
        const int BLOCK_HEIGHT = 32;

        private void SegaBlockEdit_Load(object sender, EventArgs e)
        {
            dirty = false;
            reloadTiles();
            Utils.prepareBlocksPanel(pnBlocks, ilSegaTiles.ImageSize, ilSegaTiles, buttonObjClick, 0, SEGA_TILES_COUNT);
            Utils.setCbItemsCount(cbPalSubpart, 4);
            Utils.setCbIndexWithoutUpdateLevel(cbPalSubpart, cbPalNo_SelectedIndexChanged);

            Utils.setCbItemsCount(cbBlockNo, getBlocksCount(), inHex:true);
            Utils.setCbItemsCount(cbTile, SEGA_TILES_COUNT, inHex:true);
            Utils.setCbItemsCount(cbPal, 4);
            Utils.setCbIndexWithoutUpdateLevel(cbBlockNo, cbBlockNo_SelectedIndexChanged);
            resetControls();
        }

        void reloadTiles()
        {
            var mapping = loadMappingData();
            tiles = Mapper.LoadMapping(mapping);
        }

        bool saveTiles()
        {
            byte[] tileBytes = new byte[tiles.Length*2];
            Mapper.ApplyMapping(ref tileBytes, tiles);
            ConfigScript.setBigBlocks(0, tileBytes);
            dirty = !Globals.flushToFile();
            return !dirty;
        }

        void resetControls()
        {
            fillSegaTiles();
            Utils.reloadBlocksPanel(pnBlocks, ilSegaTiles, 0, SEGA_TILES_COUNT);
            int TILE_WIDTH = getTileWidth();
            int TILE_HEIGHT = getTileHeight();
            mapScreen.Size = new Size(TILE_WIDTH * BLOCK_WIDTH, TILE_HEIGHT * BLOCK_HEIGHT);
            pnBlocks.Invalidate(true);
        }

        private void buttonObjClick(Object button, EventArgs e)
        {
            int index = ((Button)button).ImageIndex;
            pbActive.Image = ilSegaTiles.Images[index];
            curActiveTile = index;
        }

        private void fillSegaTiles()
        {
            videoChunk = ConfigScript.getVideoChunk(0);
            byte[] pal = ConfigScript.getPal(0);
            cpal = ConfigScript.videoSega.GetPalette(pal);
            ilSegaTiles.Images.Clear();
            for (ushort idx = 0; idx < SEGA_TILES_COUNT; idx++)
            {
                ilSegaTiles.Images.Add(ConfigScript.videoSega.GetZoomTile(videoChunk, idx, cpal, (byte)curActivePalNo, false, false, curScale));
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

            int TILE_WIDTH = ConfigScript.isBlockSize4x4() ? 4 : 2;
            int TILE_HEIGHT = ConfigScript.isBlockSize4x4() ? 4 : 2;
            int TILE_SIZE = TILE_WIDTH * TILE_HEIGHT;
            updateMappingControls(curActiveBlock * TILE_SIZE + curSelectedTilePart);
        }

        private void updateMappingControls(int index)
        {
            ushort word = tiles[index];
            Utils.setCbIndexWithoutUpdateLevel(cbTile, cbTile_SelectedIndexChanged, Mapper.TileIdx(word));
            Utils.setCbIndexWithoutUpdateLevel(cbPal, cbPal_SelectedIndexChanged, Mapper.PalIdx(word));
            Utils.setCbCheckedWithoutUpdateLevel(cbHFlip, cbHFlip_CheckedChanged, Mapper.HF(word));
            Utils.setCbCheckedWithoutUpdateLevel(cbVFlip, cbVFlip_CheckedChanged, Mapper.VF(word));
            Utils.setCbCheckedWithoutUpdateLevel(cbPrior, cbPrior_CheckedChanged, Mapper.P(word));
        }

        private void mapScreen_Paint(object sender, PaintEventArgs e)
        {
            int TILE_WIDTH = getTileWidth();
            int TILE_HEIGHT = getTileHeight(); 
            int TILE_SIZE = TILE_WIDTH * TILE_HEIGHT;
            int index = curActiveBlock * TILE_SIZE;
            var g = e.Graphics;
            
            for (int i = 0; i < TILE_SIZE; i++)
            {
                ushort word = tiles[index + i];
                var tileRect = new Rectangle(i % TILE_WIDTH * BLOCK_WIDTH, i / TILE_WIDTH * BLOCK_HEIGHT, BLOCK_WIDTH, BLOCK_HEIGHT);
                ushort tileIdx = Mapper.TileIdx(word);
                byte pal = Mapper.PalIdx(word);
                bool hf = Mapper.HF(word);
                bool vf = Mapper.VF(word);
                var b = ConfigScript.videoSega.GetZoomTile(videoChunk, tileIdx, cpal, pal, hf, vf, curScale);
                g.DrawImage(b, tileRect);
                if (i == curSelectedTilePart)
                    g.DrawRectangle(new Pen(Brushes.Red, 2.0f), tileRect);
            }
        }

        private void mapScreen_MouseClick(object sender, MouseEventArgs e)
        {
            int TILE_WIDTH = getTileWidth();
            int TILE_HEIGHT = getTileHeight(); 
            int TILE_SIZE = TILE_WIDTH * TILE_HEIGHT;
            int index = curActiveBlock * TILE_SIZE;

            int dx = e.X / (int)(BLOCK_WIDTH);
            int dy = e.Y / (int)(BLOCK_HEIGHT);
            if (dx < 0 || dx >= TILE_WIDTH || dy < 0 || dy >= TILE_HEIGHT)
                return;
            int tileIdx = dy * TILE_WIDTH + dx;
            curSelectedTilePart = tileIdx;
            int changeIndex = getCurTileIdx();
            //
            if (e.Button == MouseButtons.Left)
            {
                ushort w = tiles[changeIndex];
                w = Mapper.ApplyPalIdx(w, (byte)curActivePalNo);
                w = Mapper.ApplyTileIdx(w, (ushort)curActiveTile);
                tiles[changeIndex] = w;
                dirty = true;
            }
            else
            {
                curActiveTile = Mapper.TileIdx(tiles[changeIndex]);
                pbActive.Image = ilSegaTiles.Images[curActiveTile];
            }
            //

            updateMappingControls(index + tileIdx);
            mapScreen.Invalidate();
        }

        private int getCurTileIdx()
        {
            int TILE_WIDTH = ConfigScript.isBlockSize4x4() ? 4 : 2;
            int TILE_HEIGHT = ConfigScript.isBlockSize4x4() ? 4 : 2;
            int TILE_SIZE = TILE_WIDTH * TILE_HEIGHT;
            return curSelectedTilePart + curActiveBlock * TILE_SIZE;
        }

        private void cbTile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTile.SelectedIndex == -1)
                return;
            int tileIdx = getCurTileIdx();
            ushort val = (ushort)cbTile.SelectedIndex;
            tiles[tileIdx] = Mapper.ApplyTileIdx(tiles[tileIdx], val);
            mapScreen.Invalidate();
            dirty = true;
        }

        private void cbPal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPal.SelectedIndex == -1)
                return;
            int tileIdx = getCurTileIdx();
            byte val = (byte)cbPal.SelectedIndex;
            tiles[tileIdx] = Mapper.ApplyPalIdx(tiles[tileIdx], val);
            mapScreen.Invalidate();
            dirty = true;
        }

        private void cbHFlip_CheckedChanged(object sender, EventArgs e)
        {
            int tileIdx = getCurTileIdx();
            int val = cbHFlip.Checked ? 1 : 0;
            tiles[tileIdx] = Mapper.ApplyHF(tiles[tileIdx], val);
            mapScreen.Invalidate();
            dirty = true;
        }

        private void cbVFlip_CheckedChanged(object sender, EventArgs e)
        {
            int tileIdx = getCurTileIdx();
            int val = cbVFlip.Checked ? 1 : 0;
            tiles[tileIdx] = Mapper.ApplyVF(tiles[tileIdx], val);
            mapScreen.Invalidate();
            dirty = true;
        }

        private void cbPrior_CheckedChanged(object sender, EventArgs e)
        {
            int tileIdx = getCurTileIdx();
            int val = cbVFlip.Checked ? 1 : 0;
            tiles[tileIdx] = Mapper.ApplyP(tiles[tileIdx], val);
            mapScreen.Invalidate();
            dirty = true;
        }

        private void tbbSave_Click(object sender, EventArgs e)
        {
            saveTiles();
        }

        private void SegaBlockEdit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Utils.askToSave(ref dirty, saveTiles, null))
            {
                e.Cancel = true;
            }
        }

        private int getBlocksCount()
        {
            return editMapMode ? 1 : ConfigScript.getBigBlocksCount();
        }

        private int getTileWidth()
        {
            return editMapMode ? 64 : ConfigScript.isBlockSize4x4() ? 4 : 2;
        }

        private int getTileHeight()
        {
            return editMapMode ? 32 : ConfigScript.isBlockSize4x4() ? 4 : 2; ;
        }

        private byte[] loadMappingData()
        {
            return editMapMode ? Utils.loadDataFromFile("back_11.bin") : ConfigScript.getBigBlocks(0);//curActiveBigBlock;
        }
    }
}
