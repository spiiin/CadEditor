using CadEditor;
using System;
//css_include shared_settings/BlockUtils.cs;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x1a7bc, 1, 96*37, 96, 37); }
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk(new[] {"chr5-2.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x1B59C, 1  , 0x1000);  }
  public int getBlocksCount()           { return 93; }
  public int getBigBlocksCount()        { return 93; }
  public int getPalBytesAddr()          { return 0x1B59C + 93*4; }
  
  public GetBlocksFunc        getBlocksFunc() { return BlockUtils.getBlocksFromAlignedArrays;}
  public SetBlocksFunc        setBlocksFunc() { return BlockUtils.setBlocksToAlignedArrays;}
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal5.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}