using CadEditor;
using System;
//css_include shared_settings/SharedUtils.cs;
//css_include shared_settings/BlockUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x4010, 20 , 32*15);   }
  public int getScreenWidth()          { return 32; }
  public int getScreenHeight()         { return 15; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk("chr1.bin");    }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xe7aa, 1  , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 3   , 16); }
  public int getBlocksCount()           { return 120; }
  public int getBigBlocksCount()        { return 120; }
  public int getPalBytesAddr()          { return 0xe9aa; }
  
  public GetBlocksFunc        getBlocksFunc() { return BlockUtils.getBlocksLinear2x2Masked;}
  public SetBlocksFunc        setBlocksFunc() { return BlockUtils.setBlocksLinear2x2Masked;}
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal1.bin", "pal2.bin", "pal3.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}