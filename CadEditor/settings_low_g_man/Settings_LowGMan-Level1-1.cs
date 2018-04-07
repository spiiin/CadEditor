using CadEditor;
using System;
using PluginMapEditor;
//css_include settings_low_g_man/LowGManUtils.cs;

public class Data 
{  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x40d0, 3 , 16*12);   }
  public int getScreenWidth()          { return 16; }
  public int getScreenHeight()         { return 12; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return LowGManUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return LowGManUtils.getVideoChunk("chr1-1.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x4360, 1  , 0x1000);  }
  public int getBlocksCount()           { return 68; }
  public int getBigBlocksCount()        { return 68; }
  public int getPalBytesAddr()          { return 0x4470; }
  
  public GetBlocksFunc        getBlocksFunc() { return LowGManUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return LowGManUtils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return LowGManUtils.readPalFromBin("pal1-1.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
}