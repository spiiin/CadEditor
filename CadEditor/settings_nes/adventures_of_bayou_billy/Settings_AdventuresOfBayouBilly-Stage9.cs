using CadEditor;
using System;
//css_include adventures_of_bayou_billy/BayouBillyUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x88bf, 3 , 8*6, 8, 6);   }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 1   , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 1   , 16); }
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return BayouBillyUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return BayouBillyUtils.getVideoChunk(new[] {"chr9.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xa6f3, 1  , 0x1000);  }
  public int getBlocksCount()           { return 142; }
  public int getBigBlocksCount()        { return 142; }
  public int getPalBytesAddr()          { return 0xa913; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return BayouBillyUtils.readPalFromBin(new[] {"pal9.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}