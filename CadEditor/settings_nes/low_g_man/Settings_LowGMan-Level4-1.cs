using CadEditor;
using System;
//css_include shared_settings/BlockUtils.cs;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{
  public OffsetRec[] getScreensOffsetsForLevels() {
    var ans = new OffsetRec[] {
      new OffsetRec(0xa41f, 3 , 16*15, 16, 15),
      new OffsetRec(0xa7af, 1 , 16*15, 16, 15),
      new OffsetRec(0xa6ef, 1 , 16*12, 16, 12),
    };
    return ans;  
  }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk("chr4-1.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xa89f, 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 256; }
  public int getPalBytesAddr()          { return 0xac43; }
  
  public GetBlocksFunc        getBlocksFunc() { return BlockUtils.getBlocksLinear2x2Masked;}
  public SetBlocksFunc        setBlocksFunc() { return BlockUtils.setBlocksLinear2x2Masked;}
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin("pal4-1.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
}