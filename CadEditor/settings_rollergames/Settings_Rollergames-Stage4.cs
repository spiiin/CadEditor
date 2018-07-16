using CadEditor;
using System;
//css_include settings_rollergames/RollergamesUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x6249, 31 , 8*8, 8, 8);   }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 2   , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 1   , 16); }
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return RollergamesUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return RollergamesUtils.getVideoChunk(new[] {"chr4(a).bin", "chr4(b).bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x6c09, 1  , 0x1000);  }
  public int getBlocksCount()           { return 156; }
  public int getBigBlocksCount()        { return 156; }
  public int getPalBytesAddr()          { return 0x76a9; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return RollergamesUtils.readPalFromBin(new[] {"pal4(a).bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}