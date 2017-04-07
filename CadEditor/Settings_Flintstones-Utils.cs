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
    byte[] layer = new byte[1];
    layer[0] = 0;
    return new LevelLayerData(1, 1, layer);
  }
  
  public static LevelLayerData getLayoutRom(int levelNo)
  {
    byte[] layer = new byte[1];
    layer[0] = 1;
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
    var objects = new ObjRec[count];
    for (int i = 0; i < count; i++)
    {
        byte c1, c2, c3, c4, typeColor;
        c1 = Globals.romdata[addr + i*4 + 0];
        c2 = Globals.romdata[addr + i*4 + 2];
        c3 = Globals.romdata[addr + i*4 + 1];
        c4 = Globals.romdata[addr + i*4 + 3];
        typeColor = Globals.romdata[addr + count * 4 + i];
        objects[i] = new ObjRec(c1, c2, c3, c4, typeColor);
    }
    return objects;
  }
  
  public static void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
    int count = ConfigScript.getBlocksCount();
    int addr  = ConfigScript.getTilesAddr(blockIndex);
    for (int i = 0; i < count; i++)
    {
        var obj = blocksData[i];
        Globals.romdata[addr + i*4 + 0] = (byte)obj.c1;
        Globals.romdata[addr + i*4 + 2] = (byte)obj.c2;
        Globals.romdata[addr + i*4 + 1] = (byte)obj.c3;
        Globals.romdata[addr + i*4 + 3] = (byte)obj.c4;
        Globals.romdata[addr + count * 4 + i] = (byte)obj.typeColor;
    }
  }
  
  private static void xchg(int[] arr, int i1, int i2)
  {
      int tmp = arr[i1];
      arr[i1] = arr[i2];
      arr[i2] = tmp;
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