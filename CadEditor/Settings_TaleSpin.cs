using CadEditor;
using System.Collections.Generic;
//css_include Settings_CapcomBase.cs;
public class Data:CapcomBase
{
  public OffsetRec getPalOffset()       { return new OffsetRec(0x1C374, 32  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x30010, 16  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x20010, 16  , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x36F0 , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x3AF0 , 8   , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x10   , 300 , 0x40);   }
  public IList<LevelRec> getLevelRecs() { return levelRecsTS; }
  public GetObjectsFunc getObjectsFunc() { return getObjectsDwd; }
  public SetObjectsFunc setObjectsFunc() { return setObjectsDwd; }
  
  public IList<LevelRec> levelRecsTS = new List<LevelRec>() 
  {
    new LevelRec(0x1035F, 59,  8, 5,  0x1E10C), 
    new LevelRec(0x104C2, 74, 10, 5,  0x1E13C),
    new LevelRec(0x10600, 61, 17, 4,  0x1E16E),
    new LevelRec(0x1078D, 84, 12, 7,  0x1E1A1),
    new LevelRec(0x108DD, 63, 14, 6,  0x1E1F5),
    new LevelRec(0x109FC, 56, 9 , 8,  0x1E249),
    new LevelRec(0x10B00, 51, 12, 5,  0x1E292),
    new LevelRec(0x10C07, 53, 8,  6,  0x1E2D9),
  };
  public bool isBigBlockEditorEnabled() { return true;  }
  public bool isBlockEditorEnabled()    { return true;  }
  public bool isLayoutEditorEnabled()   { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  public bool isVideoEditorEnabled()    { return true; }
  
  public List<ObjectRec> getObjectsDwd(int levelNo)
  {
      LevelRec lr = ConfigScript.getLevelRec(levelNo);
      int objCount = lr.objCount, addr = lr.objectsBeginAddr;
      var objects = new List<ObjectRec>();
      for (int i = 0; i < objCount; i++)
      {
          byte v = Globals.romdata[addr + i];
          byte sx, sy, x, y;
          sx = Globals.romdata[addr - 4 * objCount + i];
          x = Globals.romdata[addr - 3 * objCount + i];
          sy = Globals.romdata[addr - 2 * objCount + i];
          y = Globals.romdata[addr - objCount + i];
          var obj = new ObjectRec(v, sx, sy, x, y);
          objects.Add(obj);
      }
      return objects;
  }
    
  public bool setObjectsDwd(int levelNo, List<ObjectRec> objects)
  {
      LevelRec lr = ConfigScript.getLevelRec(levelNo);
      int addrBase = lr.objectsBeginAddr;
      int objCount = lr.objCount;
      for (int i = 0; i < objects.Count; i++)
      {
          var obj = objects[i];
          Globals.romdata[addrBase + i] = (byte)obj.type;
          Globals.romdata[addrBase - 4 * objCount + i] = (byte)obj.sx;
          Globals.romdata[addrBase - 3 * objCount + i] = (byte)obj.x;
          Globals.romdata[addrBase - 2 * objCount + i] = (byte)obj.sy;
          Globals.romdata[addrBase - 1 * objCount + i] = (byte)obj.y;
      }
      for (int i = objects.Count; i < objCount; i++)
      {
          Globals.romdata[addrBase + i] = 0xFF;
          Globals.romdata[addrBase - 4 * objCount + i] = 0xFF;
          Globals.romdata[addrBase - 3 * objCount + i] = 0xFF;
          Globals.romdata[addrBase - 2 * objCount + i] = 0xFF;
          Globals.romdata[addrBase - 1 * objCount + i] = 0xFF;
      }
      return true;
  }
}