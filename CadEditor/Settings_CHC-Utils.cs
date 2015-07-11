using CadEditor;
using System.Collections.Generic;
using System.IO;
using System;
using System.Windows.Forms;

public static class CHCUtils
{
  public static List<ObjectRec> getObjects(int levelNo)
  {
      int OBJ_SIZE = 14;
      var commandDict = new Dictionary<int,int> {
        { 0xFFFF , 1},
        { 0xFFFE , 5},
        { 0xFFFD , 8},
        { 0xFFFC , 8},
        { 0xFFFB , 8},
        { 0xFFFA , 5},
        { 0xFFF9 , 11},
        { 0xFFF8 , 4},
        { 0xFFF7 , 7},
        { 0xFFF6 , 1},
        { 0xFFF5 , 1},
        { 0xFFF4 , 2},
        { 0xFFF3 , 2},
        { 0xFFF2 , 1},
        { 0xFFF1 , 1},
        { 0xFFF0 , 4},
        { 0xFFEF , 6},
        { 0xFFEE , 6},
        { 0xFFED , 6},
      };
      
      LevelRec lr = ConfigScript.getLevelRec(levelNo);
      int objCount = lr.objCount, addr = lr.objectsBeginAddr;
      var objects = new List<ObjectRec>();
      int i = 0;
      int curAddr = addr;
      while (objects.Count != objCount)
      {
          int baseAddr = curAddr;
          int v = Utils.readWord(Globals.romdata, baseAddr);
          if (v < 0xFF00)
          {
            int sx, sy, x, y, baseX, baseY, fromFloor;
            sx = 0;
            sy = 0;
            x = Utils.readWord(Globals.romdata, baseAddr + 8);
            y = Utils.readWord(Globals.romdata, baseAddr + 10);
            fromFloor = Utils.readWord(Globals.romdata, baseAddr + 6);
            if (fromFloor == 0)
            {
              x = Utils.readWord(Globals.romdata, baseAddr + 2);
              x += Utils.readWord(Globals.romdata, baseAddr + 8)/2;
              y = Utils.readWord(Globals.romdata, baseAddr + 4);
              y += Utils.readWord(Globals.romdata, baseAddr + 10)/2;
            }
            var obj = new ObjectRec(v, sx, sy, x, y);
            objects.Add(obj);
            curAddr += OBJ_SIZE;
          }
          else
          {
            curAddr += commandDict[v]*2;
          }
      }
      return objects;
  }
    
  public static bool setObjects(int levelNo, List<ObjectRec> objects)
  {
      return true;
  }
}