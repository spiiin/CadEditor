using CadEditor;
using System.Collections.Generic;
//css_include settings_adventure_in_the_magic_kingdoom/AitMKUtils.cs;

public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(0x80E8, 43, 8*9);   }
  public int getScreenWidth()             { return 8; }
  public int getScreenHeight()            { return 8; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 3   , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 2   , 16); }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return AitMKUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return AitMKUtils.getVideoChunk(new[] {"chr3.bin", "chr3-2.bin", "chr3-3.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0xC010, 1  , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xC810, 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 512; }
  
  public GetBigTileNoFromScreenFunc getBigTileNoFromScreenFunc() { return AitMKUtils.getBigTileNoFromScreen; }
  public SetBigTileToScreenFunc     setBigTileToScreenFunc()     { return AitMKUtils.setBigTileToScreen; }
  
  public GetBlocksFunc        getBlocksFunc() { return AitMKUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return AitMKUtils.setBlocks;}
  public GetBigBlocksFunc     getBigBlocksFunc()     { return Utils.getBigBlocksCapcomDefault;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return Utils.setBigBlocksCapcomDefault;}
  public GetPalFunc           getPalFunc()           { return AitMKUtils.readPalFromBin(new[] {"pal3.bin", "pal3-2.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}