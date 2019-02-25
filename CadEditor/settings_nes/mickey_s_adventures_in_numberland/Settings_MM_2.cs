using CadEditor;
using System;
//css_include shared_settings/SharedUtils.cs;

public class Data
{ 
  public OffsetRec[] getScreensOffsetsForLevels() {
    var ans = new OffsetRec[] {
        new OffsetRec( 0x100ee, 1 , 64*13, 64, 13),
        new OffsetRec( 0x1042e, 1 , 59*19, 59, 19),
        new OffsetRec( 0x1088f, 1 , 40*19, 40, 19),
        new OffsetRec( 0x10b87, 1 , 41*22, 41, 22),
   };
    return ans;
  }

  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk(new[] {"chr2.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x18010 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 214; }
  public int getBigBlocksCount()        { return 214; }
  public int getPalBytesAddr()          { return 0x18010+214*16; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal2.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}