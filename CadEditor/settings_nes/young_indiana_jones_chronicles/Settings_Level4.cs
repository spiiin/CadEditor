using CadEditor;
using System;
//css_include shared_settings/SharedUtils.cs;
//css_include young_indiana_jones_chronicles/IndyUtils.cs;

public class Data 
{ 
  public OffsetRec[] getScreensOffsetsForLevels() {
    var ans = new OffsetRec[] {
      new OffsetRec(0xf316, 1 , 12*80, 12, 80),
      new OffsetRec(0xf7ce, 1 ,  6*32,  6, 32),
    };
    return ans;  
  }
  public bool getScreenVertical()      { return true; }
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xeb56 , 1  , 0x1000);  }
  public int getPalBytesAddr()          { return 0xf6d6; }
  public GetBlocksFunc        getBlocksFunc() { return IndyUtils.getBlocksFromTiles16Pal1V;}
  public SetBlocksFunc        setBlocksFunc() { return IndyUtils.setBlocksFromTiles16Pal1V;}
  public int getBigBlocksCount() { return 124; }
  public int getBlocksCount()    { return 124; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk("chr4.bin");    }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getPalOffset()                    { return new OffsetRec(0, 2, 16);     }
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal4.bin", "pal42.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}