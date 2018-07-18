using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec[] getScreensOffsetsForLevels() {
      var ans = new OffsetRec[] {
          new OffsetRec(0x0c3a0, 1 , 72*14, 72, 14),
          new OffsetRec(0x10010, 1 , 56*14, 56, 14),
          new OffsetRec(0x10330, 1 , 64*14, 64, 14),
          new OffsetRec(0x11420, 1 , 67*14, 67, 14),
          new OffsetRec(0x117da, 1 , 48*14, 48, 14),
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
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x153e4 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 61; }
  public int getBigBlocksCount()        { return 61; }
  public int getPalBytesAddr()          { return 0x157f1+0*16; }
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
     return Utils.readVideoBankFromFile("chr5.bin", videoPageId);
  }
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal5.bin");
  }
}