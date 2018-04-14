using CadEditor;
using System;
//css_include settings_ninja_gaiden/NinjaGaidenUtils.cs;
public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x1150, 1 , 6*104);   }
  public int getScreenWidth()          { return 6; }
  public int getScreenHeight()         { return 104; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x4c10 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  
  public OffsetRec getBigBlocksOffset()    { return new OffsetRec(0x6810 , 1  , 0x1000);  }
  public int getBigBlocksCount()           { return 256; }
  
  public int getPalBytesAddr()             { return 0x7210; }

  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public GetBlocksFunc        getBlocksFunc()        { return NinjaGaidenUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()        { return NinjaGaidenUtils.setBlocks;}
  
  public GetBigBlocksFunc     getBigBlocksFunc()     { return NinjaGaidenUtils.getBigBlocksTT;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return NinjaGaidenUtils.setBigBlocksTT;}
  
  //----------------------------------------------------------------------------
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr4-3.bin", videoPageId);
  }
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal4-3.bin");
  }
}