using CadEditor;
using System;

public class Data 
{
  public OffsetRec[] getScreensOffsetsForLevels() {
      var ans = new OffsetRec[] {
          new OffsetRec(0xcec7 - 3*56, 1, 8*13, 8, 13),
          new OffsetRec(0xcf2f - 3*56, 2, 8*12, 8, 12),
          new OffsetRec(0xcfef - 3*56, 1, 8*13, 8, 13),
          new OffsetRec(0xd057 - 3*56, 1, 8*12, 8, 12),
          new OffsetRec(0xd0b7 - 3*56, 1, 8*12, 8, 12),
          new OffsetRec(0xd11f - 3*56, 1, 8*6, 8, 6),
          new OffsetRec(0xd14f - 3*56, 1, 8*8, 8, 8),
          new OffsetRec(0xd18f - 3*56, 1, 8*5, 8, 5),
          new OffsetRec(0xd1b7 - 3*56, 1, 8*12, 8, 12),
          new OffsetRec(0xd217 - 3*56, 1, 8*13, 8, 13),
          new OffsetRec(0xd27f - 3*56, 17, 8*6, 8, 6),
          new OffsetRec(0xd5af - 3*56, 2, 8*7, 8, 7),
          new OffsetRec(0xd61f - 3*56, 2, 8*6, 8, 6),
          new OffsetRec(0xd67f - 3*56, 1, 8*13, 8, 13),
          new OffsetRec(0xd6e7 - 3*56, 3, 8*6, 8, 6),
          new OffsetRec(0xd777 - 3*56, 1, 8*8, 8, 8),
          new OffsetRec(0xd7b7 - 3*56, 1, 8*12, 8, 12),
          new OffsetRec(0xd817 - 3*56, 1, 8*6, 8, 6),
          new OffsetRec(0xd847 - 3*56, 1, 8*24, 8, 24),
          new OffsetRec(0xd907 - 3*56, 4, 8*6, 8, 6),
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
  public OffsetRec getVideoOffset()                          { return new OffsetRec(0x3b010, 1  , 0x1000); }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xa864 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 256; }
  public int getPalBytesAddr()          { return 0xafc4; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  public OffsetRec getPalOffset()       { return new OffsetRec(0, 2, 16);  }
  
  public byte[] getPallete(int palId)
  {
    return Utils.readBinFile("pal2.bin");
  }
}