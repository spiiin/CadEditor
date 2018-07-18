using CadEditor;
using System;
//css_include bucky_ohare/BuckyUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x90c0, 1 , 8*25, 8, 25);   }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 2   , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 1   , 16); }
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return BuckyUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return BuckyUtils.getVideoChunk(new[] {"chr1(d).bin", "chr1(e).bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x8011, 1  , 0x1000);  }
  public int getBlocksCount()           { return 244; }
  public int getBigBlocksCount()        { return 244; }
  public int getPalBytesAddr()          { return 0x89e1; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return BuckyUtils.readPalFromBin(new[] {"pal1(e).bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}