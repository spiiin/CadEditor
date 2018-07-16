using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x2C071, 14, 64, 8, 8); }
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public OffsetRec getVideoOffset()                          { return new OffsetRec(0x6a010, 1  , 0x1000); }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x2C3f1 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 165; }
  public int getBigBlocksCount()        { return 165; }
  public int getPalBytesAddr()          { return 0x2C3f1+165*16; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  public OffsetRec getPalOffset()       { return new OffsetRec(0, 1, 16);  }
  
  //----------------------------------------------------------------------------
  
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal3.bin");
  }
}