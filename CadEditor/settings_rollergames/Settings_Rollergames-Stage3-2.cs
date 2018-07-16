using CadEditor;
using System;
//css_include settings_rollergames/RollergamesUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x84db, 42 , 8*8, 8, 8);   }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 1   , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 1   , 16); }
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return RollergamesUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return RollergamesUtils.getVideoChunk(new[] {"chr3-2.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x8f5b, 1  , 0x1000);  }
  public int getBlocksCount()           { return 224; }
  public int getBigBlocksCount()        { return 224; }
  public int getPalBytesAddr()          { return 0x9c6b; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return RollergamesUtils.readPalFromBin(new[] {"pal3-2.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}