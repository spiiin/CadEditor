using CadEditor;
using System;
using System.Collections.Generic;
using System.Drawing;

public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(0x289de, 1 , 11*64, 64, 11);  }
  public int getBigBlocksCount() { return 44; }
  public int getBlocksCount()    { return 44; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public RenderToMainScreenFunc getRenderToMainScreenFunc() { return renderObjects; }
  
  public void renderObjects(Graphics g, int curScale, int scrNo)
  {
    for (int i = 0; i < 64; i++)
    {
        byte b1  = Globals.romdata[0x2BE4D + i*6];
        byte b2  = Globals.romdata[0x2BE4D + i*6 + 1];
        byte b3  = Globals.romdata[0x2BE4D + i*6 + 2];
        byte b4  = Globals.romdata[0x2BE4D + i*6 + 3];
        byte b5  = Globals.romdata[0x2BE4D + i*6 + 4];
        byte b6  = Globals.romdata[0x2BE4D + i*6 + 5];
        var rect = new Rectangle(64*curScale*(i+1), 0, 64*curScale*(i+2), curScale * 11 * 64);
        g.DrawRectangle(new Pen(Color.Red, 4.0f), rect);
        g.DrawString(String.Format("{0:X2}", b1), new Font("Arial", 8), Brushes.Red, rect);
        g.DrawString(String.Format("{0:X2}", b2), new Font("Arial", 8), Brushes.Red, rect.X + 16, rect.Y);
        g.DrawString(String.Format("{0:X2}", b3), new Font("Arial", 8), Brushes.Red, rect.X + 32, rect.Y);
        g.DrawString(String.Format("{0:X2}", b4), new Font("Arial", 8), Brushes.Red, rect.X + 0 , rect.Y+16);
        g.DrawString(String.Format("{0:X2}", b5), new Font("Arial", 8), Brushes.Red, rect.X + 16, rect.Y+16);
        g.DrawString(String.Format("{0:X2}", b6), new Font("Arial", 8), Brushes.Red, rect.X + 32, rect.Y+16);
    }
  }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x28c9e , 1  , 0x1000);  }
  public int getPalBytesAddr()          { return 0x28F5E; } //it's before blocks descr and screens descr
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  //----------------------------------------------------------------------------
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("ppu_dump1.bin", videoPageId);
  }
  
  public byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x07, 0x17, 0x27, 0x0f, 0x08, 0x17, 0x10,
      0x0f, 0x08, 0x09, 0x07, 0x0f, 0x02, 0x12, 0x20
    }; 
    return pallete;
  }
}