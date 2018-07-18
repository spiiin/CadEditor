using CadEditor;
using System;

public class Data 
{ 
  public OffsetRec[] getScreensOffsetsForLevels() {
      var ans = new OffsetRec[] {
          new OffsetRec(0xCBC7 - 3*56, 1, 8*9, 8, 9),
          new OffsetRec(0xCC0f - 3*56, 3, 8*4, 8, 4),
          new OffsetRec(0xCC6f - 3*56, 2, 8*7, 8, 7),
          new OffsetRec(0xCCdf - 3*56, 1, 8*11, 8, 11),
          new OffsetRec(0xcd37 - 3*56, 2, 8*6, 8, 6),
          new OffsetRec(0xcd97 - 3*56, 1, 8*2, 8, 2),
          new OffsetRec(0xcda7 - 3*56, 1, 8*13, 8, 13),
      };
      return ans;  
  }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public OffsetRec getVideoOffset()                          { return new OffsetRec(0x38010, 1  , 0x1000); }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x10011 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 124; }
  public int getBigBlocksCount()        { return 124; }
  public int getPalBytesAddr()          { return 0x107d1; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  public OffsetRec getPalOffset()       { return new OffsetRec(0, 2, 16);  }
  
  public byte[] getPallete(int palId)
  {
    return Utils.readBinFile("pal1.bin");
  }
}