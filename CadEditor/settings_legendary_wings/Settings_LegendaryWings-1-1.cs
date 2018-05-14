using CadEditor;
using System;
using System.Collections.Generic;
//css_include settings_legendary_wings/WingsUtils.cs;
//css_include shared_settings/SharedUtils.cs;

public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(0x1024d, 1, 8*144);  }
  public OffsetRec getBigBlocksOffset()   { return new OffsetRec(0x127bd ,1, 0x4000); }
  public int getScreenWidth()    { return 8; }
  public int getScreenHeight()   { return 144; }
  public int getBigBlocksCount() { return 256; }
  public int getBlocksCount()    { return 256; }
  
  public OffsetRec getVideoOffset()   { return new OffsetRec(0, 3, 0x1000); }
  public OffsetRec getPalOffset  ()   { return new OffsetRec(0, 3, 16); }
  
  public GetBigTileNoFromScreenFunc getBigTileNoFromScreenFunc() { return getBigTileNoFromScreen; }
  public SetBigTileToScreenFunc     setBigTileToScreenFunc()     { return setBigTileToScreen; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()          { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()             { return SharedUtils.getVideoChunk(new[] {"chr1(a).bin", "chr1(b).bin", "chr1(c).bin"});   }
  public SetVideoChunkFunc    setVideoChunkFunc()             { return null; }
  public GetBlocksFunc        getBlocksFunc()                 { return WingsUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()                 { return null; }
  public GetBigBlocksFunc     getBigBlocksFunc()              { return WingsUtils.getBigBlocks;}
  public SetBigBlocksFunc     setBigBlocksFunc()              { return WingsUtils.setBigBlocks;}
  public GetPalFunc           getPalFunc()                    { return SharedUtils.readPalFromBin(new[] {"pal1(a).bin", "pal1(b).bin", "pal1(c).bin"}); }
  public SetPalFunc           setPalFunc()                    { return null;}
  
  public int getBigTileNoFromScreen(int[] screenData, int index)
  {
    int w = getScreenWidth();
    int h = getScreenHeight();
    int noY = index / w;
    noY = h - noY - 1;
    int noX = index % w;
    return screenData[noY*w + noX];
  }

  public void setBigTileToScreen(int[] screenData, int index, int value)
  {
    int w = getScreenWidth();
    int h = getScreenHeight();
    int noY = index / w;
    noY = h - noY - 1;
    int noX = index % w;
    screenData[noY*w + noX] = value;
  }
}