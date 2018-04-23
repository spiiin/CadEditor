using CadEditor;
using System;
//css_include settings_bad_dudes/BadDudesUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x5476, 7 , 16*6);   }
  public int getScreenWidth()          { return 16; }
  public int getScreenHeight()         { return 6; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x59b6, 1  , 0x1000);  }
  public int getBlocksCount()           { return 96; }
  public int getBigBlocksCount()        { return 96; }
  public int getPalBytesAddr()          { return 0x5b32; }
  
  public GetBlocksFunc        getBlocksFunc() { return BadDudesUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return BadDudesUtils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  //----------------------------------------------------------------------------
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal2.bin");
  }
  
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr2.bin", videoPageId);
  }
}