using CadEditor;
using System;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{
    public OffsetRec[] getScreensOffsetsForLevels() {
    var ans = new OffsetRec[] {
        new OffsetRec( 0x0d3d9, 1 , 60*15, 60, 15),
        new OffsetRec( 0x0d75d, 1 , 36*21, 36, 21),
        new OffsetRec( 0x0f35e, 1 , 51*21, 51, 21),
        new OffsetRec( 0x0f78d, 1 , 45*29, 45, 29),
   };
    return ans;
  }

  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk(new[] {"chr5.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x12c0f, 1  , 0x1000);  }
  public int getBlocksCount()           { return 201; }
  public int getBigBlocksCount()        { return 201; }
  public int getPalBytesAddr()          { return 0x138ad; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal5.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}