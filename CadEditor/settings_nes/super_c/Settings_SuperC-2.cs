using CadEditor;
using System;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x2bcf, 13 , 8*8, 8, 8);   }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 3   , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 5   , 16); }
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return  SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return  SharedUtils.getVideoChunk(new[] {"chr2_000.bin", "chr2_001.bin", "chr2_002.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x2f4f, 1  , 0x1000);  }
  public int getBlocksCount()           { return 245; }
  public int getBigBlocksCount()        { return 245; }
  public int getPalBytesAddr()          { return 0x3e9f; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return  SharedUtils.readPalFromBin(new[] {"pal2.bin", "pal2-2.bin", "pal2-3.bin", "pal2-4.bin", "pal2-5.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}