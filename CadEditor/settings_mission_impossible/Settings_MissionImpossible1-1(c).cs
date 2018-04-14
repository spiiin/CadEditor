using CadEditor;
using System;

public class Data 
{
  public OffsetRec getScreensOffset() { return new OffsetRec(0xCC6f - 3*56, 2, 8*7); }
  public int getScreenWidth()         { return 8; }
  public int getScreenHeight()        { return 7; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public OffsetRec getVideoOffset()                          { return new OffsetRec(0x38010, 1  , 0x1000); }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x10011 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 124; }
  public int getBigBlocksCount()        { return 124; }
  public int getPalBytesAddr()          { return 0x107d1; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  public OffsetRec getPalOffset()       { return new OffsetRec(0, 2, 16);  }
  
  public byte[] getPallete(int palId)
  {
    return Utils.readBinFile("pal1.bin");
  }
}