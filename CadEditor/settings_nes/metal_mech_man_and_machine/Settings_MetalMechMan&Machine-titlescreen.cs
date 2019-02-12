using CadEditor;
using System;
//css_include metal_mech_man_and_machine/MMUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x30c5, 1 , 16*15, 16, 15);   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x2ea1, 1  , 0x1000);  }
  public int getBlocksCount()           { return 180; }
  public int getBigBlocksCount()        { return 180; }
  public int getPalBytesAddr()          { return 0x3055; }
  
  public GetBlocksFunc        getBlocksFunc() { return MMUtils.getBlocksLinear2x2MaskPal;}
  public SetBlocksFunc        setBlocksFunc() { return MMUtils.setBlocksLinear2x2MaskPal;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  //----------------------------------------------------------------------------
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal-title.bin");
  }
  
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr-title.bin", videoPageId);
  }
}