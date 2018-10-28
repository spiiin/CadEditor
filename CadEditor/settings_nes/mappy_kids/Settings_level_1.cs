using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x00010, 1 , 8*128, 8, 128);   }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x6410 , 1  , 0x1000);  }
  public GetBlocksFunc        getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocks;}
  public int getPalBytesAddr()          { return 0xf3f4; }
  
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x8110  , 1  , 0x4000); }
  public GetBigBlocksFunc getBigBlocksFunc() { return getBigBlocks;}
  public SetBigBlocksFunc setBigBlocksFunc() { return setBigBlocks;}
  
  //----------------------------------------------------------------------------
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
      return Utils.readVideoBankFromFile("chr1.bin", 0);
  }
  
  public ObjRec[] getBlocks(int blockIndex)
  {
      var bb = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), 2, 2, ConfigScript.getBlocksCount(blockIndex), false, true);
      for (int i = 0; i < bb.Length; i++)
      {
          var b = bb[i];
          b.palBytes[0] = Globals.romdata[getPalBytesAddr()+i] >> 6;
      }
      return bb;
  }
  
  public void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
      Utils.writeBlocksLinear(blocksData, Globals.romdata, ConfigScript.getTilesAddr(blockIndex), ConfigScript.getBlocksCount(blockIndex), false, true);
      for (int i = 0; i < blocksData.Length; i++)
      {
          var b = blocksData[i];
          int mask = Globals.romdata[getPalBytesAddr()+i] & 0x3F;
          Globals.romdata[getPalBytesAddr()+i] = (byte)(mask | b.palBytes[0] << 6);
      }
  }
  
  private void transposeBigBlocks(BigBlock[] bblocks)
  {
    for (int i = 0; i < bblocks.Length; i++)
    {
        var bb = bblocks[i];
        bb.indexes = Utils.transpose(bb.indexes, 2, 2);
    }
  }
  
  public BigBlock[] getBigBlocks(int bigTileIndex)
  {
    var data = Utils.readLinearBigBlockData(0, bigTileIndex, 4);
    var bblocks = Utils.unlinearizeBigBlocks<BigBlock>(data, 2, 2);
    transposeBigBlocks(bblocks);
    return bblocks;
  }
  
  public void setBigBlocks(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
    transposeBigBlocks(bigBlockIndexes);
    var data = Utils.linearizeBigBlocks(bigBlockIndexes);
    Utils.writeLinearBigBlockData(0, bigTileIndex, data);
  }
}