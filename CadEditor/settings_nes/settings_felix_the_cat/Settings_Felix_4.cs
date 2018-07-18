using CadEditor;
using System;
//css_include settings_felix_the_cat/FelixUtils.cs;


public class Data 
{ 
  public bool showDumpFileField()  { return true;  }
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x0, 1 , 24*(256*4), 24, 2564);   }
  public bool getScreenVertical()      { return true;   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return FelixUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return FelixUtils.getVideoChunk("chr4.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x14068, 1  , 0x1000);  }
  public int getBlocksCount()           { return 128; }
  public int getBigBlocksCount()        { return 128; }
  
  public GetBlocksFunc        getBlocksFunc() { return FelixUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return FelixUtils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return FelixUtils.readPalFromBin("pal4.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
}