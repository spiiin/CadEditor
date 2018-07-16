using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0xD55D, 9, 8*6, 8, 6); }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public OffsetRec getVideoOffset()                  { return new OffsetRec(0x40010, 4  , 0x1000); }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xC6CD , 1  , 0x1000);  }
  public int getBlocksCount()           { return 233; }
  public int getBigBlocksCount()        { return 233; }
  public int getPalBytesAddr()          { return 0xC6CD - 233; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc getPalFunc()  { return Utils.getPalleteLinear;}
  public SetPalFunc setPalFunc()  { return Utils.setPalleteLinear;}
  public OffsetRec getPalOffset() { return new OffsetRec(0x3d5e3, 1, 16);  }
}