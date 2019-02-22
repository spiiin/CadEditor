using CadEditor;
using System;
//css_include shared_settings/BlockUtils.cs;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x199f7, 1, 160*14, 160, 14); }
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk(new[] {"chr1.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x1a2b6, 1  , 0x1000);  }
  public int getBlocksCount()           { return 206; }
  public int getBigBlocksCount()        { return 206; }
  public int getPalBytesAddr()          { return 0x1a2b6 + 206*4; }
  
  public GetBlocksFunc        getBlocksFunc() { return BlockUtils.getBlocksFromAlignedArrays;}
  public SetBlocksFunc        setBlocksFunc() { return BlockUtils.setBlocksToAlignedArrays;}
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal1.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}