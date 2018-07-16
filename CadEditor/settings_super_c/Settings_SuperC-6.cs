using CadEditor;
using System;
//css_include settings_super_c/SuperCUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x12daa, 11 , 8*8, 8, 8);   }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 5   , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 5   , 16); }
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SuperCUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SuperCUtils.getVideoChunk(new[] {"chr6_000.bin", "chr6_001.bin", "chr6_002.bin", "chr6_003.bin", "chr6_004.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x1306a, 1  , 0x1000);  }
  public int getBlocksCount()           { return 147; }
  public int getBigBlocksCount()        { return 147; }
  public int getPalBytesAddr()          { return 0x1399a; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return SuperCUtils.readPalFromBin(new[] {"pal6_000.bin", "pal6_001.bin", "pal6_002.bin", "pal6_003.bin", "pal6_004.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}