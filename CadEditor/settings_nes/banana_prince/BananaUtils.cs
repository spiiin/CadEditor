using CadEditor;
using System;

public class BananaUtils
{ 
  //----------------------------------------------------------------------------
  public static ObjRec[] getBlocksConsts(int blockIndex)
  {
        var objects = new ObjRec[ConfigScript.getBlocksCount(blockIndex)];
        for (int i = 0; i < objects.Length; i++)
        {
            var indexes  = new int[4];
            var palBytes = new int[1];
            int bi = (i/8)*32 + i%8 * 2;
            indexes[0] = bi;
            indexes[2] = bi + 1;
            indexes[1] = bi + 16;
            indexes[3] = bi + 17;
            objects[i] = new ObjRec(2, 2, 0, indexes, palBytes);
        }
        return objects;
  }
  
  public static BigBlock[] getBigBlocks(int bigTileIndex)
  {
    var data = Utils.readLinearBigBlockData(0, bigTileIndex);
    var bb = Utils.unlinearizeBigBlocks<BigBlockWithPal>(data, 2, 2);
    for (int i = 0; i < bb.Length; i++)
    {
      int palByte = Globals.romdata[ConfigScript.getPalBytesAddr(bigTileIndex) + i];
      bb[i].palBytes[0] = palByte >> 0 & 0x3;
      bb[i].palBytes[1] = palByte >> 2 & 0x3;
      bb[i].palBytes[2] = palByte >> 4 & 0x3;
      bb[i].palBytes[3] = palByte >> 6 & 0x3;
    }
    return bb;
  }
  
  public static void setBigBlocks(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
    var bigBlocksAddr = ConfigScript.getBigTilesAddr(0, bigTileIndex);
    var data = Utils.linearizeBigBlocks(bigBlockIndexes);
    
    int size = data.Length;
    int addr = ConfigScript.getBigTilesAddr(0, bigTileIndex);
    for (int i = 0; i < size; i++)
    {
        Globals.romdata[addr + i] =  data[i];
    }
    //save pal bytes
    for (int i = 0; i < bigBlockIndexes.Length; i++)
    {
      var bb = bigBlockIndexes[i] as BigBlockWithPal;
      int palByte = bb.palBytes[0] | bb.palBytes[1] << 2 | bb.palBytes[2]<<4 | bb.palBytes[3]<< 6;
      Globals.romdata[ConfigScript.getPalBytesAddr(bigTileIndex) + i] = (byte)palByte;
    }
  }
}