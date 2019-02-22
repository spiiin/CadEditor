using CadEditor;
using System;
//css_include shared_settings/BlockUtils.cs;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0xa22f, 4 , 16*16, 16, 16);   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk(new[] {"chr1.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xa02a, 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 256; }
  public int getPalBytesAddr()          { return 0xa982; }
  
  public GetBlocksFunc        getBlocksFunc() { return BlockUtils.getBlocksLinear2x2Masked;}
  public SetBlocksFunc        setBlocksFunc() { return BlockUtils.setBlocksLinear2x2Masked;}
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal1.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
  
  public GetBigTileNoFromScreenFunc getBigTileNoFromScreenFunc() { return getBigTileNoFromScreen; }
  public SetBigTileToScreenFunc     setBigTileToScreenFunc()     { return setBigTileToScreen; }
  //----------------------------------------------------------------------------
  
  public static int getBigTileNoFromScreen(int[] screenData, int index)
  {
    var screen = ConfigScript.loadScreens()[0];
    int w = screen.width;
    int noY = index / w;
    noY = (noY/8)*8 + 7 - (noY%8);
    int noX = index % w;
    return screenData[noY*w + noX];
  }

  public static void setBigTileToScreen(int[] screenData, int index, int value)
  {
    var screen = ConfigScript.loadScreens()[0];
    int w = screen.width;
    int noY = index / w;
    noY = (noY/8)*8 + 7 - (noY%8);
    int noX = index % w;
    screenData[noY*w + noX] = value;
  }
}