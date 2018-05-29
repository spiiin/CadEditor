using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x63c1, 4, 8*7+8); }
  public int getScreenWidth()         { return 8; }
  public int getScreenHeight()        { return 7; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x64e9 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 40; }
  public int getBigBlocksCount()        { return 40; }
  public int getPalBytesAddr()          { return 0x64c1; }
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
     return Utils.readVideoBankFromFile("chr7.bin", videoPageId);
  }
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal7.bin");
  }
}