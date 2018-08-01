using CadEditor;
using System;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x0c190, 12 , 8*6, 8, 6);   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xe69c, 1 , 0x1000);  }
  public int getBlocksCount()           { return 31; }
  public int getBigBlocksCount()        { return 31; }
  public int getPalBytesAddr()          { return 0xe69c - 31; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk("chr2.bin");    }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal2.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}