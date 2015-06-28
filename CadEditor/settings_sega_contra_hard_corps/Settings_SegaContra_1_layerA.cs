using CadEditor;
using System;
using System.Collections.Generic;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x0, 1 , 256*16);   }
  public int getScreenWidth()          { return 256; }
  public int getScreenHeight()         { return 16; }
  public string getBlocksFilename()    { return "sega_contra_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public GetObjectsFunc getObjectsFunc() { return getObjects; }
  public SetObjectsFunc setObjectsFunc() { return null; }
  public IList<LevelRec> getLevelRecs()  { return levelRecs;  }
  public GetLayoutFunc getLayoutFunc()   { return getLayout;  }
  
  public int getMaxObjType()             { return 0x500; }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x7E1E8, 33, 1, 1, 0), 
  };
  
  public List<ObjectRec> getObjects(int levelNo)
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
    
  public bool setObjects(int levelNo, List<ObjectRec> objects)
  {
      return true;
  }
  
  LevelLayerData getLayout(int levelNo)
  {
    byte[] layer = new byte[1];
    layer[0] = 0;
    return new LevelLayerData(1, 1, layer);
  }
}