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

        int curActiveBlock = 0;
        int curActiveTile = 0;
        int curActivePalNo = 0;
        float curScale = 2.0f;

        ushort[] tiles;
        byte[] videoChunk;
        Color[] cpal;

        const int SEGA_TILES_COUNT = 0x800;

        private void SegaBlockEdit_Load(object sender, EventArgs e)
        {
            reloadTiles();
            Utils.prepareBlocksPanel(pnBlocks, ilSegaTiles.ImageSize, ilSegaTiles, buttonObjClick, 0, SEGA_TILES_COUNT);
            Utils.setCbItemsCount(cbPalSubpart, 4);
            Utils.setCbIndexWithoutUpdateLevel(cbPalSubpart, cbPalNo_SelectedIndexChanged);

            Utils.setCbItemsCount(cbBlockNo, ConfigScript.getBigBlocksCount());
            Utils.setCbItemsCount(cbTile, SEGA_TILES_COUNT);
            Utils.setCbItemsCount(cbPal, 4);
            resetControls();
        }

        void reloadTiles()
        {
            var mapping = ConfigScript.getBigBlocks(0); //curActiveBigBlock;
            tiles = Mapper.LoadMapping(mapping, true);
        }

        void saveTiles()
        {
            //
        }

        void resetControls()
        {
            fillSegaTiles();
            Utils.reloadBlocksPanel(pnBlocks, ilSegaTiles, 0, SEGA_TILES_COUNT);
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
            cpal = VideoSega.GetPalette(pal);
            ilSegaTiles.Images.Clear();
            for (ushort idx = 0; idx < SEGA_TILES_COUNT; idx++)
            {
                ilSegaTiles.Images.Add(VideoSega.GetZoomTile(videoChunk, idx, cpal, (byte)curActivePalNo, false, false, curScale));
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
            mapScreen.Invalidate();
        }

        private void mapScreen_Paint(object sender, PaintEventArgs e)
        {
            int TILE_WIDTH = 2;
            int TILE_HEIGHT = 2;
            int TILE_SIZE = TILE_WIDTH * TILE_HEIGHT;
            int BLOCK_WIDTH = 32;
            int BLOCK_HEIGHT =32;
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
                var b = VideoSega.GetZoomTile(videoChunk, tileIdx, cpal, pal, hf, vf, curScale);
                g.DrawImage(b, tileRect);
            }
        }
    }
}
