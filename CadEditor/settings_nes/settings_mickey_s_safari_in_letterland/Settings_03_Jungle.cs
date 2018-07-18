using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec[] getScreensOffsetsForLevels() {
      var ans = new OffsetRec[] {
          new OffsetRec(0x0e010, 1 , 64*7 , 64, 7),
          new OffsetRec(0x0e1e0, 1 , 64*14, 64, 14),
          new OffsetRec(0x106c0, 1 , 40*14, 40, 14),
          new OffsetRec(0x10900, 1 , 56*14, 56, 14),
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
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x14796 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 176; }
  public int getBigBlocksCount()        { return 176; }
  public int getPalBytesAddr()          { return 0x15335+0*16; }
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
     return Utils.readVideoBankFromFile("chr3.bin", videoPageId);
  }
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal3.bin");
  }
}