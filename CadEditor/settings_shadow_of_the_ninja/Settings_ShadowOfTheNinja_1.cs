using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x1719e, 13 , 8*8);   }
  public int getScreenWidth()          { return 8; }
  public int getScreenHeight()         { return 8; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x178de, 1  , 0x1000);  }
  public int getBlocksCount()           { return 64; }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x17b5a, 1  , 0x1000);  }
  public int getBigBlocksCount()        { return 256; }
  
  public GetBlocksFunc        getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocks;}
  public GetBigBlocksFunc     getBigBlocksFunc()     { return getBigBlocks;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return setBigBlocks;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  //----------------------------------------------------------------------------
  public static ObjRec[] getBlocks(int tileId)
  {
      int count = ConfigScript.getBlocksCount();
      var bb = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(tileId), 2, 2, count, false, true);
      return bb;
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocksData)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount();
    Utils.writeBlocksLinear(blocksData, Globals.romdata, addr, count, false, true);
  }
  
  public BigBlock[] getBigBlocks(int bigTileIndex)
  {
      var data = Utils.readLinearBigBlockData(0, bigTileIndex);
      var bblocks = Utils.unlinearizeBigBlocks<BigBlockWithPal>(data, 2, 2);
      for (int v = 0; v < bblocks.Length; v++)
      {
          var bb = bblocks[v];
          var i0 = bb.indexes[0];
          var i1 = bb.indexes[1];
          var i2 = bb.indexes[2];
          var i3 = bb.indexes[3];
          bb.indexes[0] = i0 & 0x3f;
          bb.indexes[1] = i2 & 0x3F;
          bb.indexes[2] = i1 & 0x3f;
          bb.indexes[3] = i3 & 0x3f;
          bb.palBytes[0] = i0 >> 6;
          bb.palBytes[1] = i2 >> 6;
          bb.palBytes[2] = i1 >> 6;
          bb.palBytes[3] = i3 >> 6;
      }
      return bblocks;
  }
  
  public void setBigBlocks(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
      var bigBlocksAddr = ConfigScript.getBigTilesAddr(0, bigTileIndex); 
      for (int v = 0; v < bigBlockIndexes.Length; v++)
      {
          var bb = bigBlockIndexes[v] as BigBlockWithPal;
          var i0 = bb.indexes[0] | bb.palBytes[0] << 6;
          var i1 = bb.indexes[1] | bb.palBytes[1] << 6;
          var i2 = bb.indexes[2] | bb.palBytes[2] << 6;
          var i3 = bb.indexes[3] | bb.palBytes[3] << 6;
          Globals.romdata[bigBlocksAddr + v * 4 + 0] = (byte)i0;
          Globals.romdata[bigBlocksAddr + v * 4 + 1] = (byte)i2;
          Globals.romdata[bigBlocksAddr + v * 4 + 2] = (byte)i1;
          Globals.romdata[bigBlocksAddr + v * 4 + 3] = (byte)i3;
      }
  }
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal1.bin");
  }
  
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr1.bin", videoPageId);
  }
}