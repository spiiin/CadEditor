using CadEditor;
using System;
//css_include settings_mickey_mouse_3_yume_fuusen/MickeyUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x500c, 32 , 8*6, 8, 6);   }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x5a0c, 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x560c, 1  , 0x1000);  }
  public int getBigBlocksCount()        { return 256; }
  public int getPalBytesAddr()          { return 0x5a0c + 128*4; }
  
  public GetBlocksFunc        getBlocksFunc() { return MickeyUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return MickeyUtils.setBlocks;}
  public GetBigBlocksFunc     getBigBlocksFunc()     { return Utils.getBigBlocksCapcomDefault;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return Utils.setBigBlocksCapcomDefault;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  //----------------------------------------------------------------------------
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal5.bin");
  }
  
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr5.bin", videoPageId);
  }
}