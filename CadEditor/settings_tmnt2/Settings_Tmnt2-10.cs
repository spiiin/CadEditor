using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x1E213, 32, 8*6); }
  public int getScreenWidth()         { return 8; }
  public int getScreenHeight()        { return 6; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public OffsetRec getVideoOffset()                          { return new OffsetRec(0x60010, 2  , 0x1000); }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x1D493 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 216; }
  public int getBigBlocksCount()        { return 216; }
  public int getPalBytesAddr()          { return 0x1D493 - 216; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  public OffsetRec getPalOffset()       { return new OffsetRec(0, 1, 16);  }
  
  //----------------------------------------------------------------------------
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal9.bin");
  }
}