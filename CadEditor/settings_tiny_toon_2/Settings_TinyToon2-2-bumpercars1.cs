using CadEditor;
using System;
//css_include settings_tiny_toon_2/TT2Utils.cs;

public class Data 
{  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x66a5, 2 , 16*15);   }
  public int getScreenWidth()          { return 16; }
  public int getScreenHeight()         { return 15; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return TT2Utils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return TT2Utils.getVideoChunk("chr2.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x70dd, 1  , 0x1000);  }
  public int getBlocksCount()           { return 64; }
  public int getBigBlocksCount()        { return 64; }
  public int getPalBytesAddr()          { return 0x71d5; }
  
  public GetBlocksFunc        getBlocksFunc() { return TT2Utils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return TT2Utils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return TT2Utils.readPalFromBin("pal2.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
}