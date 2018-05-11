using CadEditor;
using System;
//css_include shared_settings/SharedUtils.cs;
//css_include shared_settings/BlockUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0xa72, 5 , 16*15);   }
  public int getScreenWidth()          { return 16; }
  public int getScreenHeight()         { return 15; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk("chr1-2.bin");    }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xf22, 1  , 0x1000);  }
  public int getBlocksCount()           { return 157; }
  public int getBigBlocksCount()        { return 157; }
  public int getPalBytesAddr()          { return 0x122e; }
  
  public GetBlocksFunc        getBlocksFunc() { return BlockUtils.getBlocksLinear2x2MaskedWithAttribs;}
  public SetBlocksFunc        setBlocksFunc() { return BlockUtils.setBlocksLinear2x2MaskedWithAttribs;}
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin("pal1-2.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
}