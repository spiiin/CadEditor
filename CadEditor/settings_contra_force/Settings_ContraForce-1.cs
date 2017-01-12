using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(98321, 24 , 8*8); }
  public int getScreenWidth()          { return 8; }
  public int getScreenHeight()         { return 8; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()   { return new OffsetRec(0x2C010, 1, 0x1000); }
  public OffsetRec getPalOffset()     { return new OffsetRec(0x1AAEE, 16, 16   ); }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x18611, 1  , 0x1000);  }
  public int getBlocksCount()           { return 152; }
  public int getBigBlocksCount()        { return 152; }
  public int getPalBytesAddr()          { return 0x18FA1; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()         { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()            { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()            { return Utils.setVideoChunk; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  //-------------------------------------------------------------------------------
  
  
  public byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x20, 0x31, 0x00, 0x0f, 0x38, 0x27, 0x07,
      0x0f, 0x31, 0x21, 0x12, 0x0f, 0x39, 0x29, 0x18
    }; 
    return pallete;
  }
}