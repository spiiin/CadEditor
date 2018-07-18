using CadEditor;
using System;
using System.Collections.Generic;
//css_include jackal/JackalUtils.cs;

public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(0x126c0, 1, 16*96, 16, 96);  }
  
  public GetBigTileNoFromScreenFunc getBigTileNoFromScreenFunc() { return JackalUtils.getBigTileNoFromScreen; }
  public SetBigTileToScreenFunc     setBigTileToScreenFunc()     { return JackalUtils.setBigTileToScreen; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x12bb0, 2  , 0x1000);  }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x12bb0, 2  , 0x1000);  }
  public int getBlocksCount()           { return 124; }
  public int getBigBlocksCount()        { return 124; }
  public int getPalBytesAddr()          { return 0x12bb0 + 16*124; }
  public GetBlocksFunc        getBlocksFunc() { return JackalUtils.getBlocksFromTiles16Pal1Shifted;}
  public SetBlocksFunc        setBlocksFunc() { return JackalUtils.setBlocksFromTiles16Pal1Shifted;}
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  //----------------------------------------------------------------------------
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr5.bin", videoPageId);
  }
  
  public byte[] getPallete(int palId)
  {
    return Utils.readBinFile("pal5.bin");
  }
}