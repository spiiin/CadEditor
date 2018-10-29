using CadEditor;
using System;
using System.Collections.Generic;

public static class ShatterhandUtils 
{
  public static ObjRec[] getBlocks(int tileId)
  {
    var objects = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(tileId), 2, 2, ConfigScript.getBlocksCount(tileId), false, true);
    int palAddr = ConfigScript.getPalBytesAddr(tileId);
    for (int i = 0; i < ConfigScript.getBlocksCount(tileId); i++)
    {
        objects[i].palBytes[0] =  Globals.romdata[palAddr + i] >> 6; //physics also in this blocks
    }
    return objects;
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocks)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount(tileId);
    int palAddr = ConfigScript.getPalBytesAddr(tileId);
    Utils.writeBlocksLinear(blocks, Globals.romdata, addr, count, false, true);
    for (int i = 0; i < count; i++)
    {
        int mask = Globals.romdata[palAddr + i] & 0x3F;
        Globals.romdata[palAddr + i] = (byte)(mask | (blocks[i].palBytes[0] << 6));
    }
  }
  
  public static BigBlock[] getBigBlocks(int bigTileIndex)
  {
      var bblocks = Utils.getBigBlocksCapcomDefault(bigTileIndex);
      for (int v = 0; v < bblocks.Length; v++)
      {
         var b = bblocks[v];
         int temp = b.indexes[1];
         b.indexes[1] = b.indexes[2];
         b.indexes[2] = temp;
      }
      return bblocks;
  }
  
  public static void setBigBlocks(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
      for (int v = 0; v < bigBlockIndexes.Length; v++)
      {
         var b = bigBlockIndexes[v];
         int temp = b.indexes[1];
         b.indexes[1] = b.indexes[2];
         b.indexes[2] = temp;
      }
      Utils.setBigBlocksCapcomDefault(bigTileIndex, bigBlockIndexes);
  }
  
  public static GetPalFunc readPalFromBin(string[] fname)
  {
      return (int x)=> { return Utils.readBinFile(fname[x]); };
  }
  
  public static GetVideoPageAddrFunc fakeVideoAddr()
  {
      return (int _)=> { return -1; };
  }
  
  public static GetVideoChunkFunc getVideoChunk(string[] fname)
  {
     return (int x)=> { return Utils.readVideoBankFromFile(fname[x], 0); };
  }
  
  public static LevelLayerData getLayoutLinearSH(int curActiveLayout)
  {
      int layoutAddr = ConfigScript.getLayoutAddr(curActiveLayout);
      int width =  ConfigScript.getLevelWidth(curActiveLayout);
      int height = ConfigScript.getLevelHeight(curActiveLayout);
      int[] layer = new int[width * height];
      for (int i = 0; i < width * height; i++)
      {
          var scrNo = Globals.romdata[layoutAddr + i];
          if (scrNo > 0) //not change zero values (for easy view)
          {
              layer[i] = (scrNo + 1)%256;
          }
      }
      return new LevelLayerData(width, height, layer, null, null);
  }
  
  public static bool setLayoutLinearSH(LevelLayerData layerData, int curActiveLayout)
  {
      int layoutAddr = ConfigScript.getLayoutAddr(curActiveLayout);
      int width =  ConfigScript.getLevelWidth(curActiveLayout);
      int height = ConfigScript.getLevelHeight(curActiveLayout);
      for (int i = 0; i < width * height; i++)
      {
          var scrNo = layerData.layer[i];
          Globals.romdata[layoutAddr + i] = (byte)((scrNo == 0) ? scrNo : (scrNo-1));
      }
      return true;
  }
  
  public static List<ObjectList> getObjects(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    int baseAddr = lr.objectsBeginAddr;
    var objects = new List<ObjectRec>();
    int objSize = 6;
    for (int i = 0; i < objCount; i++)
    {
      int dirIndex = Globals.romdata[baseAddr + objSize*i + 0];
      int type = Globals.romdata[baseAddr + objSize*i + 1];
      int flags1 = Globals.romdata[baseAddr + objSize*i + 2];
      int x = Globals.romdata[baseAddr + objSize*i + 3];
      int flags2 = Globals.romdata[baseAddr + objSize*i + 4];
      int y = Globals.romdata[baseAddr + objSize*i + 5];
      
      int direction = dirIndex & 0x40;
      int index = dirIndex & 0x3F;
      var dataDict = new Dictionary<string,int>();
      dataDict["direction"] = direction;
      dataDict["index"] = index;
      dataDict["flags1"] = flags1;
      dataDict["flags2"] = flags2;
      int scrX = (x >> 4) - 1;
      int realX = (x & 0x0F) * 16;
      int scrY = (y >> 4) - 1;
      int realY = (y & 0x0F) * 16;
      var obj = new ObjectRec(type, scrX, scrY, realX, realY, dataDict);
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
    int objSize = 6;
    for (int i = 0; i < objects.Count; i++)
    {
      var obj = objects[i];
      var dict = obj.additionalData;
      int dirIndex = dict["direction"] | dict["index"];
      int x = ((obj.sx+1) << 4) | (obj.x/16);
      int y = ((obj.sy+1) << 4) | (obj.y/16);
      
      Globals.romdata[baseAddr + objSize*i + 0] = (byte)dirIndex;
      Globals.romdata[baseAddr + objSize*i + 1] = (byte)obj.type;
      Globals.romdata[baseAddr + objSize*i + 2] = (byte)dict["flags1"];
      Globals.romdata[baseAddr + objSize*i + 3] = (byte)x;
      Globals.romdata[baseAddr + objSize*i + 4] = (byte)dict["flags2"];
      Globals.romdata[baseAddr + objSize*i + 5] = (byte)y;
    }
    for (int i = objects.Count; i < objCount; i++)
    {
      Globals.romdata[baseAddr + objSize*i + 0] = (byte)0xFF;
      Globals.romdata[baseAddr + objSize*i + 1] = (byte)0xFF;
      Globals.romdata[baseAddr + objSize*i + 2] = (byte)0xFF;
      Globals.romdata[baseAddr + objSize*i + 3] = (byte)0xFF;
      Globals.romdata[baseAddr + objSize*i + 4] = (byte)0xFF;
      Globals.romdata[baseAddr + objSize*i + 5] = (byte)0xFF;
    }
    return true;
  }
  
  public static Dictionary<String,int> getObjectDictionary(int listNo, int type)
  {
    return new Dictionary<String, int> { 
        {"direction", 0},
        {"index", 0},
        {"flags1", 0},
        {"flags2", 0},
    };
  }
}