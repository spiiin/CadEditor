using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec[] getScreensOffsetsForLevels() {
      var ans = new OffsetRec[] {
          new OffsetRec(0x0ead0, 1 , 56*14, 56, 14),
          new OffsetRec(0x0edf0, 1 , 56*14, 56, 14),
          new OffsetRec(0x0c7a0, 1 , 36*32, 36, 32),
          new OffsetRec(0x0cf50, 1 , 72*14, 72, 14),
          new OffsetRec(0x0daf0, 1 , 32*35, 32, 35),
      };
      return ans;  
  }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x192c4 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 90; }
  public int getBigBlocksCount()        { return 90; }
  public int getPalBytesAddr()          { return 0x198be+0*16; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  //----------------------------------------------------------------------------
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr4.bin", videoPageId);
  }
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal4.bin");
  }
}