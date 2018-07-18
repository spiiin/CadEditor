using CadEditor;
using System;
//css_include super_c/SuperCUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x7894, 1 , 8*161, 8, 161);   }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 2   , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 2   , 16); }
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SuperCUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SuperCUtils.getVideoChunk(new[] {"chr3(a).bin", "chr3(b).bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x695e, 1  , 0x1000);  }
  public int getBlocksCount()           { return 240; }
  public int getBigBlocksCount()        { return 240; }
  public int getPalBytesAddr()          { return 0x686e; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return SuperCUtils.readPalFromBin(new[] {"pal3(a).bin", "pal3(b).bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}