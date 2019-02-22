using CadEditor;
using System;
//css_include shared_settings/BlockUtils.cs;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0xD6B4-44*16 + 14*44+16, 1, 44*62, 44, 62); }
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk(new[] {"chr3.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xe194-84, 1  , 0x1000);  }
  public int getBlocksCount()           { return 167; }
  public int getBigBlocksCount()        { return 167; }
  public int getPalBytesAddr()          { return 0xe194 - 84 + 167*4; }
  
  public GetBlocksFunc        getBlocksFunc() { return BlockUtils.getBlocksFromAlignedArrays;}
  public SetBlocksFunc        setBlocksFunc() { return BlockUtils.setBlocksToAlignedArrays;}
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal3.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}