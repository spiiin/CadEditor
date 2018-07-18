using CadEditor;
using System;
//css_include shared_settings/BlockUtils.cs;
//css_include aliens_fds/AliensUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x16e98, 1 , 10*256, 10, 256);   }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x17898, 1  , 0x1000);  }
  public int getBlocksCount()           { return 64; }
  public int getBigBlocksCount()        { return 64; }
  public int getPalBytesAddr()          { return 0x17998; }
  
  public GetBlocksFunc        getBlocksFunc() { return BlockUtils.getBlocksLinear2x2MaskedTransposed;}
  public SetBlocksFunc        setBlocksFunc() { return BlockUtils.setBlocksLinear2x2MaskedTransposed;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  public GetBigTileNoFromScreenFunc getBigTileNoFromScreenFunc() { return AliensUtils.getBigTileNoFromScreen; }
  public SetBigTileToScreenFunc     setBigTileToScreenFunc()     { return AliensUtils.setBigTileToScreen; }
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