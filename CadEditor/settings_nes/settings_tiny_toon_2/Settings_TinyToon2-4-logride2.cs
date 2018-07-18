using CadEditor;
using System;
//css_include settings_tiny_toon_2/TT2Utils.cs;

public class Data 
{  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0xad3d, 13 , 16*12, 16, 12);   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return TT2Utils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return TT2Utils.getVideoChunk("chr4.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xbdbd, 1  , 0x1000);  }
  public int getBlocksCount()           { return 128; }
  public int getBigBlocksCount()        { return 128; }
  public int getPalBytesAddr()          { return 0xbfbd; }
  
  public GetBlocksFunc        getBlocksFunc() { return TT2Utils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return TT2Utils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return TT2Utils.readPalFromBin("pal4.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
}