using CadEditor;
using System;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0xcf5e, 10 , 8*8, 8, 8);   }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 1   , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 2   , 16); }
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return  SharedUtils.getVideoChunk(new[] {"chr1.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xd1de, 1  , 0x1000);  }
  public int getBlocksCount()           { return 76; }
  public int getBigBlocksCount()        { return 76; }
  public int getPalBytesAddr()          { return 0xd69e; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return  SharedUtils.readPalFromBin(new[] {"pal1(a).bin", "pal1(b).bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}