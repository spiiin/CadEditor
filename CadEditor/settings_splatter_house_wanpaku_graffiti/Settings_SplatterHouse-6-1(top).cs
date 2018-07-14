using CadEditor;
using System;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x987b, 1 , 3*151);   }
  public int getScreenWidth()          { return 3; }
  public int getScreenHeight()         { return 151; }
  public bool getScreenVertical()      { return true; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 3   , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 3   , 16); }
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk(new[] {"chr5-1.bin", "chr6(a).bin", "chr6(b).bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x81cb, 1  , 0x1000);  }
  public int getBlocksCount()           { return 232; }
  public int getBigBlocksCount()        { return 232; }
  public int getPalBytesAddr()          { return 0x80e3; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal5-1.bin", "pal6(a).bin", "pal6(b).bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}