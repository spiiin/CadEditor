using CadEditor;
using System;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{
  public OffsetRec[] getScreensOffsetsForLevels() {
    var ans = new OffsetRec[] {
        new OffsetRec( 0x0e10e, 1 , 60*15, 60, 15),
        new OffsetRec( 0x0e492, 1 , 52*22, 52, 22),
        new OffsetRec( 0x0e90a, 1 , 58*22, 58, 22),
        new OffsetRec( 0x0ee06, 1 , 41*30, 41, 30),
   };
    return ans;
  }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk(new[] {"chr3.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x18E35 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 126; }
  public int getBigBlocksCount()        { return 126; }
  public int getPalBytesAddr()          { return 0x1961d; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal3.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}