using CadEditor;
using System;

public static class NinjaGaiden3Utils 
{ 
  public static ObjRec[] getBlocks(int blockIndex)
  {
    return Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), 2, 2, ConfigScript.getBlocksCount(), false, false);
  }
  
  public static void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
    Utils.writeBlocksLinear(blocksData, Globals.romdata, ConfigScript.getTilesAddr(blockIndex), ConfigScript.getBlocksCount(), false, false);
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
      int palByte = getTTSmallBlocksColorByte(i);
      bb[i].palBytes[0] = palByte >> 0 & 0x3;
      bb[i].palBytes[1] = palByte >> 2 & 0x3;
      bb[i].palBytes[2] = palByte >> 4 & 0x3;
      bb[i].palBytes[3] = palByte >> 6 & 0x3;
    }
    return bb;
  }
  
  public static void setBigBlocksTT(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
    var data = Utils.linearizeBigBlocks(bigBlockIndexes);
    Utils.writeLinearBigBlockData(0, bigTileIndex, data);
    //save pal bytes
    for (int i = 0; i < bigBlockIndexes.Length; i++)
    {
      var bb = bigBlockIndexes[i] as BigBlockWithPal;
      int palByte = bb.palBytes[0] | bb.palBytes[1] << 2 | bb.palBytes[2]<<4 | bb.palBytes[3]<< 6;
      setTTSmallBlocksColorByte(i, (byte)palByte);
    }
  }
}