using CadEditor;
using System;
//css_include settings_ninja_gaiden/NinjaGaidenUtils.cs;
public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x280, 1 , 6*128, 6, 128);   }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x4010 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  
  public OffsetRec getBigBlocksOffset()    { return new OffsetRec(0x6010 , 1  , 0x1000);  }
  public int getBigBlocksCount()           { return 256; }
  
  public int getPalBytesAddr()             { return 0x7010; }

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
     return Utils.readVideoBankFromFile("chr2-1.bin", videoPageId);
  }
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal2-1.bin");
  }
}