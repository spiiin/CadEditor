using CadEditor;
using System;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x98f, 7 , 8*8, 8, 8);   }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 7   , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 7   , 16); }
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk(new[] {"chr-stages1.bin", "chr-stages2.bin", "chr-stages3.bin", "chr-stages4.bin", "chr-stages5.bin", "chr-stages6.bin", "chr-stages7.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x157, 1  , 0x1000);  }
  public int getBlocksCount()           { return 127; }
  public int getBigBlocksCount()        { return 127; }
  public int getPalBytesAddr()          { return 0xd8; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal-stages1.bin", "pal-stages2.bin", "pal-stages3.bin", "pal-stages4.bin", "pal-stages5.bin", "pal-stages6.bin", "pal-stages7.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}