using CadEditor;
using System;
using System.Collections.Generic;
//css_include settings_jackal/JackalUtils.cs;

public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(0x10625 - 16 * 96, 1, 16*96, 16, 96);  }
  
  public GetBigTileNoFromScreenFunc getBigTileNoFromScreenFunc() { return JackalUtils.getBigTileNoFromScreen; }
  public SetBigTileToScreenFunc     setBigTileToScreenFunc()     { return JackalUtils.setBigTileToScreen; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x10625 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 102; }
  public int getBigBlocksCount()        { return 102; }
  public int getPalBytesAddr()          { return 0x10c85; }
  public GetBlocksFunc        getBlocksFunc() { return JackalUtils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return JackalUtils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  //----------------------------------------------------------------------------
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr1.bin", videoPageId);
  }
  
  public byte[] getPallete(int palId)
  {
    return Utils.readBinFile("pal1.bin");
  }
}