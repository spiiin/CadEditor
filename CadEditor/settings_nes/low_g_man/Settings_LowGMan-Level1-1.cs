using CadEditor;
using System;
//css_include shared_settings/BlockUtils.cs;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x40d0, 3 , 16*12, 16, 12);   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk("chr1-1.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x4360, 1  , 0x1000);  }
  public int getBlocksCount()           { return 68; }
  public int getBigBlocksCount()        { return 68; }
  public int getPalBytesAddr()          { return 0x4470; }
  
  public GetBlocksFunc        getBlocksFunc() { return BlockUtils.getBlocksLinear2x2Masked;}
  public SetBlocksFunc        setBlocksFunc() { return BlockUtils.setBlocksLinear2x2Masked;}
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin("pal1-1.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
}