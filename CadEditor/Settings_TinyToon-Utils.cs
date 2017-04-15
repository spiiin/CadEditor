using CadEditor;
using System.Collections.Generic;
using System.IO;
using System;
using System.Windows.Forms;

public static class TinyToonUtils
{
  //--------------------------------------------------------------------------------------------------------------
  public static ObjRec[] getBlocks(int blockIndex)
  {
    return readBlocksFromAlignedArraysTT(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), ConfigScript.getBlocksCount());
  }
  
  public static void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
    writeBlocksToAlignedArraysTT(blocksData, Globals.romdata, ConfigScript.getTilesAddr(blockIndex), ConfigScript.getBlocksCount());
  }
  
  //--------------------------------------------------------------------------------------------------------------
  
  private static byte getTTSmallBlocksColorByte(int index)
  {
      int btc = ConfigScript.getBigBlocksCount(0);
      int addr = ConfigScript.getBigTilesAddr(0, 0);
      return Globals.romdata[addr + btc * 4 + index];
  }
  
    private static void setTTSmallBlocksColorByte(int index, byte colorByte)
  {
      int btc = ConfigScript.getBigBlocksCount(0);
      int addr = ConfigScript.getBigTilesAddr(0, 0);
      Globals.romdata[addr + btc * 4 + index] = colorByte;
  }
  
  public static BigBlock[] getBigBlocksTT(int bigTileIndex)
  {
    var bigBlocksAddr = ConfigScript.getBigTilesAddr(0, bigTileIndex);
    var data = Utils.readDataFromAlignedArrays(Globals.romdata, bigBlocksAddr, ConfigScript.getBigBlocksCount(0));
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
    var bigBlocksAddr = ConfigScript.getBigTilesAddr(0, bigTileIndex);
    var data = Utils.linearizeBigBlocks(bigBlockIndexes);
    Utils.writeDataToAlignedArrays(data, Globals.romdata, bigBlocksAddr, ConfigScript.getBigBlocksCount(0));
    //save pal bytes
    for (int i = 0; i < bigBlockIndexes.Length; i++)
    {
      var bb = bigBlockIndexes[i] as BigBlockWithPal;
      int palByte = bb.palBytes[0] | bb.palBytes[1] << 2 | bb.palBytes[2]<<4 | bb.palBytes[3]<< 6;
      setTTSmallBlocksColorByte(i, (byte)palByte);
    }
  }
  
  static ObjRec[] readBlocksFromAlignedArraysTT(byte[] romdata, int addr, int count)
  {
      var objects = Utils.readBlocksFromAlignedArrays(romdata, addr, count);
      foreach (var o in objects)
      {
          o.indexes = Utils.transpose(o.indexes, 2, 2);
          o.palBytes[0] = 0;
      }
      return objects;
  }
  
  static void writeBlocksToAlignedArraysTT(ObjRec[] objects, byte[] romdata, int addr, int count)
  {
    for (int i = 0; i < count; i++)
    {
      var obj = objects[i];
      romdata[addr + count * 0 + i] = (byte)obj.indexes[0];
      romdata[addr + count * 1 + i] = (byte)obj.indexes[2];
      romdata[addr + count * 2 + i] = (byte)obj.indexes[1];
      romdata[addr + count * 3 + i] = (byte)obj.indexes[3];
    }
  }
  
  public static List<ObjectList> getObjectsTT(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount, addr = lr.objectsBeginAddr;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
        int v = Globals.romdata[addr + i * 3 + 0];
        int xx = Globals.romdata[addr + i * 3 + 1];
        int yy = Globals.romdata[addr + i * 3 + 2];
        int sx = xx >> 4;
        int sy = 0;
        int x = (xx & 0x0F) * 16;
        int y = yy * 16;
        var obj = new ObjectRec(v, sx, sy, x, y);
        objects.Add(obj);
    }
    return new List<ObjectList> { new ObjectList { objects = objects, name = "Objects" } };
  }

  public static bool setObjectsTT(int levelNo, List<ObjectList> objLists)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int addrBase = lr.objectsBeginAddr;
    int objCount = lr.objCount;
    var objects = objLists[0].objects;
    for (int i = 0; i < objects.Count; i++)
    {
        var obj = objects[i];
        Globals.romdata[addrBase + i * 3 + 0] = (byte)obj.type;
        Globals.romdata[addrBase + i * 3 + 1] = (byte)((obj.x / 16) | (obj.sx << 4));
        Globals.romdata[addrBase + i * 3 + 2] = (byte)((obj.y / 16) | (obj.sy << 4));
    }
    for (int i = objects.Count; i < objCount; i++)
    {
        Globals.romdata[addrBase + i * 3 + 0] = 0xFF;
        Globals.romdata[addrBase + i * 3 + 1] = 0xFF;
        Globals.romdata[addrBase + i * 3 + 2] = 0xFF;
    }
    return true;
  }
  
  public static LevelLayerData getLayoutLinearTT(int curActiveLayout)
  {
      int layoutAddr = ConfigScript.getLayoutAddr(curActiveLayout);
      int width = ConfigScript.getLevelWidth(curActiveLayout);
      int height = ConfigScript.getLevelHeight(curActiveLayout);
      byte[] layer = new byte[width * height];
      for (int i = 0; i < width * height; i++)
          layer[i] = (byte)(i + 1);
      return new LevelLayerData(width, height, layer, null, null);
  }
  //--------------------------------------------------------------------------------------------------------------
}