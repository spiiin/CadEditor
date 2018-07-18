using CadEditor;
using System;

public static class NinjaGaidenUtils 
{ 
  public static ObjRec[] getBlocks(int blockIndex)
  {
    return Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), 2, 2, ConfigScript.getBlocksCount(), false, true);
  }
  
  public static void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
    Utils.writeBlocksLinear(blocksData, Globals.romdata, ConfigScript.getTilesAddr(blockIndex), ConfigScript.getBlocksCount(), false, true);
  }
  
  private static byte getTTSmallBlocksColorByte(int index)
  {
    return Globals.romdata[ConfigScript.getPalBytesAddr()+index];
  }
  
  private static void setTTSmallBlocksColorByte(int index, byte colorByte)
  {
    Globals.romdata[ConfigScript.getPalBytesAddr()+index] = colorByte;
  }
  
  public static BigBlock[] getBigBlocksTT(int bigTileIndex)
  {
    var data = Utils.readLinearBigBlockData(0, bigTileIndex, 4);
    var bb = Utils.unlinearizeBigBlocks<BigBlockWithPal>(data, 2, 2);
    for (int i = 0; i < bb.Length; i++)
    {
      var b = bb[i];
      var i1 = b.indexes[1];
      var i2 = b.indexes[2];
      b.indexes[1] = i2;
      b.indexes[2] = i1;
      
      int palByte = getTTSmallBlocksColorByte(i);
      b.palBytes[0] = palByte >> 0 & 0x3;
      b.palBytes[1] = palByte >> 2 & 0x3;
      b.palBytes[2] = palByte >> 4 & 0x3;
      b.palBytes[3] = palByte >> 6 & 0x3;
    }
    return bb;
  }
  
  public static void setBigBlocksTT(int bigTileIndex, BigBlock[] bigBlockIndexes)
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