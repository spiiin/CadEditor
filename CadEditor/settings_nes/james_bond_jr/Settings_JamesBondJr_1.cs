using CadEditor;
using System;
//css_include james_bond_jr/JamesUtils.cs;

public class Data 
{ 
  public bool showDumpFileField()  { return true;  }
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x0, 1 , 12*32, 12, 32);   }
  public bool getScreenVertical()      { return true;   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return JamesUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return JamesUtils.getVideoChunk("chr1.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xF6B5, 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 256; }
  
  public GetBlocksFunc        getBlocksFunc() { return JamesUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return JamesUtils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return JamesUtils.readPalFromBin("pal1.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
}