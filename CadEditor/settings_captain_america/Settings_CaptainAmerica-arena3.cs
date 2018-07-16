using CadEditor;
using System;
//css_include settings_captain_america/CaptainAmericaUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x4970, 1 , 16*15, 16, 15);   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x6850, 1  , 0x1000);  }
  public int getBlocksCount()           { return 77; }
  public int getBigBlocksCount()        { return 77; }
  public int getPalBytesAddr()          { return 0x5220; }
  
  public GetBlocksFunc        getBlocksFunc() { return CAUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return CAUtils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  //----------------------------------------------------------------------------
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal1.bin");
  }
  
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr1.bin", videoPageId);
  }
}