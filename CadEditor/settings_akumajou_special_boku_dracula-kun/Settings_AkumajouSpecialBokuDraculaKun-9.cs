using CadEditor;
using System;
//css_include settings_super_c/SuperCUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x2521, 1 , 8*90, 8, 90);   }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 2   , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 1   , 16); }
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SuperCUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SuperCUtils.getVideoChunk(new[] {"chr9(a).bin", "chr9(b).bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x1d9c, 1  , 0x1000);  }
  public int getBlocksCount()           { return 164; }
  public int getBigBlocksCount()        { return 164; }
  public int getPalBytesAddr()          { return 0x248c; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return SuperCUtils.readPalFromBin(new[] {"pal9.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}