using CadEditor;
using System.Collections.Generic;
using System.IO;
using System;
using System.Windows.Forms;

public static class MermaidUtils
{
  public static List<ObjectList> getObjectsLM(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount, addr = lr.objectsBeginAddr;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
        byte v = Globals.romdata[addr + i];
        byte sx = Globals.romdata[addr - 3 * objCount + i];
        byte x = Globals.romdata[addr - 2 * objCount + i];
        byte y = Globals.romdata[addr - 1 * objCount + i];
        byte sy = 0;
        var obj = new ObjectRec(v, sx, sy, x, y);
        objects.Add(obj);
    }
    return new List<ObjectList> { new ObjectList { objects = objects, name = "Objects" } };
  }

  public static bool setObjectsLM(int levelNo, List<ObjectList> objLists)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int addrBase = lr.objectsBeginAddr;
    int objCount = lr.objCount;
    var objects = objLists[0].objects;
    for (int i = 0; i < objects.Count; i++)
    {
        var obj = objects[i];
        Globals.romdata[addrBase + i] = (byte)obj.type;
        Globals.romdata[addrBase - 1 * objCount + i] = (byte)obj.y;
        Globals.romdata[addrBase - 2 * objCount + i] = (byte)obj.x;
        Globals.romdata[addrBase - 3 * objCount + i] = (byte)obj.sx;
    }
    for (int i = objects.Count; i < objCount; i++)
    {
        Globals.romdata[addrBase + i] = 0xFF;
        Globals.romdata[addrBase - 1 * objCount + i] = 0xFF;
        Globals.romdata[addrBase - 2 * objCount + i] = 0xFF;
        Globals.romdata[addrBase - 3 * objCount + i] = 0xFF;
    }
    return true;
  }
  
  public static LevelLayerData getLayoutLinearMermaid(int curActiveLayout)
  {
      int layoutAddr = ConfigScript.getLayoutAddr(curActiveLayout);
      int width =  ConfigScript.getLevelWidth(curActiveLayout);
      int height = ConfigScript.getLevelHeight(curActiveLayout);
      int[] layer = new int[width * height];
      for (int i = 0; i < width * height; i++)
          layer[i] = (Globals.romdata[layoutAddr + i] + 1)%256;
      return new LevelLayerData(width, height, layer, null, null);
  }
}