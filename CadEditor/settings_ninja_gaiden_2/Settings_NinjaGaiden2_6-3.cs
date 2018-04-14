using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x1b16, 1 , 6*8);   }
  public int getScreenWidth()          { return 6; }
  public int getScreenHeight()         { return 8; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x4c10 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  
  public OffsetRec getBigBlocksOffset()    { return new OffsetRec(0x6410 , 1  , 0x1000);  }
  public int getBigBlocksCount()           { return 256; }
  
  public int getPalBytesAddr()             { return 0x7c10; }

  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public GetBlocksFunc        getBlocksFunc()        { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc()        { return setBlocks;}
  
  public GetBigBlocksFunc     getBigBlocksFunc()     { return getBigBlocksTT;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return setBigBlocksTT;}
  
  //----------------------------------------------------------------------------
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr6-3.bin", videoPageId);
  }
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal6-3.bin");
  }
  
  public ObjRec[] getBlocks(int blockIndex)
  {
    return Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), 2, 2, ConfigScript.getBlocksCount(), false, false);
  }
  
  public void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
    Utils.writeBlocksLinear(blocksData, Globals.romdata, ConfigScript.getTilesAddr(blockIndex), ConfigScript.getBlocksCount(), false, false);
  }
  
  private byte getTTSmallBlocksColorByte(int index)
  {
    return Globals.romdata[getPalBytesAddr()+index];
  }
  
  private  void setTTSmallBlocksColorByte(int index, byte colorByte)
  {
    Globals.romdata[getPalBytesAddr()+index] = colorByte;
  }
  
  public BigBlock[] getBigBlocksTT(int bigTileIndex)
  {
    var data = Utils.readLinearBigBlockData(0, bigTileIndex, 4);
    var bb = Utils.unlinearizeBigBlocks<BigBlockWithPal>(data, 2, 2);
    for (int i = 0; i < bb.Length; i++)
    {
      int palByte = getTTSmallBlocksColorByte(i);
      bb[i].palBytes[0] = palByte >> 0 & 0x3;
      bb[i].palBytes[1] = palByte >> 2 & 0x3;
      bb[i].palBytes[2] = palByte >> 4 & 0x3;
      bb[i].palBytes[3] = palByte >> 6 & 0x3;
    }
    return bb;
  }
  
  public void setBigBlocksTT(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
      var bigBlocksAddr = ConfigScript.getBigTilesAddr(0, bigTileIndex); 
      for (int v = 0; v < bigBlockIndexes.Length; v++)
      {
          var bb = bigBlockIndexes[v] as BigBlockWithPal;
          var i0 = bb.indexes[0];
          var i1 = bb.indexes[1];
          var i2 = bb.indexes[2];
          var i3 = bb.indexes[3];
          Globals.romdata[bigBlocksAddr + v * 4 + 0] = (byte)i0;
          Globals.romdata[bigBlocksAddr + v * 4 + 1] = (byte)i2;
          Globals.romdata[bigBlocksAddr + v * 4 + 2] = (byte)i1;
          Globals.romdata[bigBlocksAddr + v * 4 + 3] = (byte)i3;
          
          int palByte = bb.palBytes[0] | bb.palBytes[1] << 2 | bb.palBytes[2]<<4 | bb.palBytes[3]<< 6;
          setTTSmallBlocksColorByte(v, (byte)palByte);
      }
  }
}