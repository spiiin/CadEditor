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
    return readBlocksFromAlignedArraysTT(Globals.romdata, Globals.getTilesAddr(blockIndex), getBlocksCount());
  }
  
  public static void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
    writeBlocksToAlignedArraysTT(blocksData, Globals.romdata, Globals.getTilesAddr(blockIndex), getBlocksCount());
  }
  
  //--------------------------------------------------------------------------------------------------------------
  static int getBlocksCount()
  {
    return 256;
  }
  
  static ObjRec[] readBlocksFromAlignedArraysTT(byte[] romdata, int addr, int count)
  {
      var objects = new ObjRec[count];
      byte c1, c2, c3, c4, typeColor;
      for (int i = 0; i < count; i++)
      {
          c1 = romdata[addr + i];
          c3 = romdata[addr + count*1 + i]; //tt version
          c2 = romdata[addr + count*2 + i];
          c4 = romdata[addr + count*3 + i];
          typeColor = 0;
          objects[i] = new ObjRec(c1, c2, c3, c4, typeColor);
      }
      return objects;
  }
  
  static void writeBlocksToAlignedArraysTT(ObjRec[] objects, byte[] romdata, int addr, int count)
  {
    for (int i = 0; i < count; i++)
    {
      var obj = objects[i];
      romdata[addr + i] = obj.c1;
      romdata[addr + count * 1 + i] = obj.c3;
      romdata[addr + count * 2 + i] = obj.c2;
      romdata[addr + count * 3 + i] = obj.c4;
    }
  }
  
  public static List<ObjectRec> getObjectsTT(int levelNo)
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
    return objects;
  }

  public static bool setObjectsTT(int levelNo, List<ObjectRec> objects)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int addrBase = lr.objectsBeginAddr;
    int objCount = lr.objCount;
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
      int layoutAddr = Globals.getLayoutAddr(curActiveLayout);
      int width = Globals.getLevelWidth(curActiveLayout);
      int height = Globals.getLevelHeight(curActiveLayout);
      byte[] layer = new byte[width * height];
      for (int i = 0; i < width * height; i++)
          layer[i] = (byte)(i + 1);
      return new LevelLayerData(width, height, layer, null, null);
  }
  //--------------------------------------------------------------------------------------------------------------
}