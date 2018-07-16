using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0xcbfb, 6 , 16*15, 16, 15);   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xd5cf, 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 256; }
  
  public GetBlocksFunc        getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  //----------------------------------------------------------------------------
  public static ObjRec[] getBlocks(int tileId)
  {
      int count = ConfigScript.getBlocksCount();
      var bb = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(tileId), 2, 2, count, true, false);
      return bb;
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocksData)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount();
    Utils.writeBlocksLinear(blocksData, Globals.romdata, addr, count, true, false);
  }
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal4b.bin");
  }
  
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr4b.bin", videoPageId);
  }
}