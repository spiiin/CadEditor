using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x8B10, 17, 8*6); }
  public int getScreenWidth()         { return 8; }
  public int getScreenHeight()        { return 6; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()   { return new OffsetRec(0x20010, 1, 0x1000); }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()         { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()            { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()            { return Utils.setVideoChunk; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x8011 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 157; }
  public int getBigBlocksCount()        { return 157; }
  public int getPalBytesAddr()          { return 0x89e1; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x13, 0x21, 0x20, 0x0f, 0x13, 0x29, 0x19,
      0x0f, 0x13, 0x17, 0x27, 0x0f, 0x13, 0x00, 0x10
    }; 
    return pallete;
  }
}