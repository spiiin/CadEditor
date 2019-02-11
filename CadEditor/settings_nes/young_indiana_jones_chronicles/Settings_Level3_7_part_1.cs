using CadEditor;
using System;
//css_include shared_settings/SharedUtils.cs;
//css_include young_indiana_jones_chronicles/IndyUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x3950, 1 , 6*64, 6, 64);   }
  public bool getScreenVertical()      { return true; }
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x3250, 1  , 0x1000);  }
  public int getPalBytesAddr()          { return 0x3ad0; }
  public GetBlocksFunc        getBlocksFunc() { return IndyUtils.getBlocksFromTiles16Pal1V;}
  public SetBlocksFunc        setBlocksFunc() { return IndyUtils.setBlocksFromTiles16Pal1V;}
  public int getBigBlocksCount() { return 112; }
  public int getBlocksCount()    { return 112; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk("chr6.bin");    }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin("pal6.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
}