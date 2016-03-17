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
  
  public static Dictionary<String,int> getObjectDictionary(int listNo, int type)
  {
    return new Dictionary<String, int> { {"data", 0} };
  }
}