using CadEditor;
using System.Collections.Generic;
//css_include adventure_in_the_magic_kingdoom/AitMKUtils.cs;
//css_include shared_settings/SharedUtils.cs;

public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(0xA890, 24, 8*9, 8, 8);   }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 3   , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 5   , 16); }
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk(new[] {"chr1.bin", "chr1-2.bin", "chr1-3.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x4010, 1  , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x4810, 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 512; }
  
  public GetBigTileNoFromScreenFunc getBigTileNoFromScreenFunc() { return AitMKUtils.getBigTileNoFromScreen; }
  public SetBigTileToScreenFunc     setBigTileToScreenFunc()     { return AitMKUtils.setBigTileToScreen; }
  
  public GetBlocksFunc        getBlocksFunc() { return AitMKUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return AitMKUtils.setBlocks;}
  public GetBigBlocksFunc     getBigBlocksFunc()     { return Utils.getBigBlocksCapcomDefault;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return Utils.setBigBlocksCapcomDefault;}
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal1.bin", "pal1-2.bin", "pal1-3.bin", "pal1-4.bin", "pal1-5.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}