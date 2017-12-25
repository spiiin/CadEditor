using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0xB10, 20, 8*8); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x510   , 1, 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x10    , 1, 0x400);  }
  public OffsetRec getPalOffset()       { return new OffsetRec(0x90a9 , 1, 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x20010 , 1, 0x1000);  }
  public int getScreenWidth()         { return 8; }
  public int getScreenHeight()        { return 8; }
  
  public int getBigBlocksCount() { return 256; }
  public int getBlocksCount()    { return 256; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetBlocksFunc        getBlocksFunc()        { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc()        { return setBlocks;}
  public GetBigBlocksFunc     getBigBlocksFunc()     { return getBigBlocks;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return setBigBlocks;}
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public GetPalFunc           getPalFunc()           { return Utils.getPalleteLinear;}
  public SetPalFunc           setPalFunc()           { return Utils.setPalleteLinear;}
  
  public ObjRec[] getBlocks(int blockIndex)
  {
    var bb = Utils.readBlocksFromAlignedArraysWithoutCropPal(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), getBlocksCount());
    foreach (var b in bb)
    {
      b.palBytes[0] = b.palBytes[0] >> 4;
    }
    return bb;
  }
  
  public void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
      int addr = ConfigScript.getTilesAddr(blockIndex);
      int count = getBlocksCount();
      var romdata = Globals.romdata;
      
      for (int i = 0; i < count; i++)
      {
          var obj = blocksData[i];
          romdata[addr + i] = (byte)obj.indexes[0];
          romdata[addr + count * 1 + i] = (byte)obj.indexes[1];
          romdata[addr + count * 2 + i] = (byte)obj.indexes[2];
          romdata[addr + count * 3 + i] = (byte)obj.indexes[3];
          
          var mask = 0x0F & romdata[addr + count * 4 + i];
          romdata[addr + count * 4 + i] = (byte)(mask | obj.palBytes[0]<<4);
      }
  }
  
  public BigBlock[] getBigBlocks(int bigTileIndex)
  {
    var bigBlocksAddr = ConfigScript.getBigTilesAddr(0, bigTileIndex);
    var data = Utils.readDataFromAlignedArrays(Globals.romdata, bigBlocksAddr, ConfigScript.getBigBlocksCount(0));
    return Utils.unlinearizeBigBlocks<BigBlock>(data, 2, 2);
  }
  
  public void setBigBlocks(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
    var bigBlocksAddr = ConfigScript.getBigTilesAddr(0, bigTileIndex);
    var data = Utils.linearizeBigBlocks(bigBlockIndexes);
    Utils.writeDataToAlignedArrays(data, Globals.romdata, bigBlocksAddr, ConfigScript.getBigBlocksCount(0));
  }
}