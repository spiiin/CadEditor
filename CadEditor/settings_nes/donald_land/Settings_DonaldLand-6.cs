using CadEditor;
using System;
//css_include donald_land/DonaldLandUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x20db, 16 , 16*13, 16, 13);   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x2ddb, 1  , 0x1000);  }
  public int getBlocksCount()           { return 103; }
  public int getBigBlocksCount()        { return 103; }
  public int getPalBytesAddr()          { return 0x2f77; }
  
  public GetBlocksFunc        getBlocksFunc() { return DonaldLandUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return DonaldLandUtils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  //----------------------------------------------------------------------------
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal6.bin");
  }
  
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr6.bin", videoPageId);
  }
}