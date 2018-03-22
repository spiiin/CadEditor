using CadEditor;
using System;
using System.Collections.Generic;

public static class FliUtils
{
  //addrs saved in ram at D7-D9-DC-DC
  public static List<ObjectList> getObjects(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    int baseAddr = lr.objectsBeginAddr;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
      byte x    = Globals.romdata[baseAddr + objCount*0 + i];
      byte y    = Globals.romdata[baseAddr + objCount*2 + i];
      int realx = x * 8;
      int realy = y * 8;
      byte v    = Globals.romdata[baseAddr + objCount*3 + i];
      byte data = Globals.romdata[baseAddr + objCount*1 + i];
      var dataDict = new Dictionary<string,int>();
      dataDict["data"] = data;
      var obj = new ObjectRec(v, 0, 0, realx, realy, dataDict);
      objects.Add(obj);
    }
    return new List<ObjectList> { new ObjectList { objects = objects, name = "Objects" } };
  }

  public static bool setObjects(int levelNo, List<ObjectList> objLists)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    int baseAddr = lr.objectsBeginAddr;
    var objects = objLists[0].objects;
    for (int i = 0; i < objects.Count; i++)
    {
        var obj = objects[i];
        byte x = (byte)(obj.x /8);
        byte y = (byte)(obj.y /8);
        Globals.romdata[baseAddr + objCount*3 + i] = (byte)obj.type;
        Globals.romdata[baseAddr + objCount*1 + i] = (byte)obj.additionalData["data"];
        Globals.romdata[baseAddr + objCount*0 + i] = x;
        Globals.romdata[baseAddr + objCount*2 + i] = y;
    }
    for (int i = objects.Count; i < objCount; i++)
    {
        Globals.romdata[baseAddr + objCount*0 + i] = 0xFF;
        Globals.romdata[baseAddr + objCount*1 + i] = 0xFF;
        Globals.romdata[baseAddr + objCount*2 + i] = 0xFF;
        Globals.romdata[baseAddr + objCount*3 + i] = 0xFF;
    }
    return true;
  }
  
  public static LevelLayerData getLayout(int levelNo)
  {
    int[] layer = new int[1];
    layer[0] = 0;
    return new LevelLayerData(1, 1, layer);
  }
  
  public static Dictionary<String,int> getObjectDictionary(int listNo, int type)
  {
    return new Dictionary<String, int> { {"data", 0} };
  }
  
  public static ObjRec[] getBlocks(int blockIndex)
  {
    int count = ConfigScript.getBlocksCount();
    int addr  = ConfigScript.getTilesAddr(blockIndex);
    var objects = Utils.readBlocksLinear(Globals.romdata, addr, 2, 2, count, false, true);
    for(int i = 0; i < objects.Length; i++)
    {
        objects[i].palBytes[0] = Globals.romdata[addr + count * 4 + i];
    }
    return objects;
  }
  
  public static void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
    int count = ConfigScript.getBlocksCount();
    int addr  = ConfigScript.getTilesAddr(blockIndex);
    Utils.writeBlocksLinear(blocksData, Globals.romdata, addr, count, false, true);
    for (int i = 0; i < count; i++)
    {
        Globals.romdata[addr + count * 4 + i] = (byte)blocksData[i].palBytes[0];
    }
  }
  
  private static void transposeBigBlocks(BigBlock[] bblocks)
  {
    for (int i = 0; i < bblocks.Length; i++)
    {
        var bb = bblocks[i];
        bb.indexes = Utils.transpose(bb.indexes, 4, 4);
    }
  }
  
  public static BigBlock[] getBigBlocks(int bigTileIndex)
  {
    var data = Utils.readLinearBigBlockData(0, bigTileIndex, 16);
    var bblocks = Utils.unlinearizeBigBlocks<BigBlock>(data, 4, 4);
    transposeBigBlocks(bblocks);
    return bblocks;
  }
  
  public static void setBigBlocks(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
    transposeBigBlocks(bigBlockIndexes);
    var data = Utils.linearizeBigBlocks(bigBlockIndexes);
    Utils.writeLinearBigBlockData(0, bigTileIndex, data);
  }
  
  public static int getConvertScreenTile(int v)         { return (v >> 4) | (v & 0x0F) << 4;}
  public static int getBackConvertScreenTile(int v)     { return (v >> 4) | (v & 0x0F) << 4;}
}