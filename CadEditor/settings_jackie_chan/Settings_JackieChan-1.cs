using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x1F8, 1, 6*150); }
  public int getScreenWidth()         { return 6; }
  public int getScreenHeight()        { return 150; }
  public bool getScreenVertical()      { return true; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x64c , 1  , 0x1000);  }
  public int getBlocksCount()           { return 196; }
  public int getBigBlocksCount()        { return 196; }
  public int getPalBytesAddr()          { return 0x588; }
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
      0x0f, 0x16, 0x37, 0x18, 0x0f, 0x21, 0x20, 0x11,
      0x0f, 0x10, 0x00, 0x0c, 0x0f, 0x05, 0x27, 0x17
    }; 
    return pallete;
  }
}