using CadEditor;
using System.Collections.Generic;
//css_include Settings_CapcomBase.cs;
public class Data:CapcomBase
{
  public override GameType getGameType()  { return GameType.CAD; }
   
  public OffsetRec getPalOffset()       { return new OffsetRec(0x1C354, 32  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x30010, 16  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x20010, 16  , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x36F0 , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x3AF0 , 8   , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x10   , 300 , 0x40);   }
  public IList<LevelRec> getLevelRecs() { return levelRecsCad; }
  public string[] getBlockTypeNames()   { return objTypesCad;  }
  
  public GetObjectsFunc getObjectsFunc() { return getObjectsCad; }
  public SetObjectsFunc setObjectsFunc() { return setObjectsCad; }
  
  public string getObjTypesPicturesDir() { return "settings_chip_and_dale/obj_sprites_cad"; }
  
  //chip and dale specific
  public OffsetRec getBoxesBackOffset() { return new OffsetRec(0x1E909, 11  , 16);     }
  public int getLevelRecBaseOffset() { return 0x1E201; }
  public int getLevelRecDirOffset()  { return 0x10239; }
  public int getLayoutPtrAdd()       { return 0x10010; }
  public int getScrollPtrAdd()       { return 0x10010; }
  public int getDirPtrAdd()          { return 0x8010;  }
  public int getDoorRecBaseOffset()  { return 0x1E673; }
  
  public IList<LevelRec> levelRecsCad = new List<LevelRec>() 
  {
    new LevelRec(0x10388, 76),
    new LevelRec(0x10456, 31),
    new LevelRec(0x105A1, 73),
    new LevelRec(0x106D1, 57),
    new LevelRec(0x10890, 97),
    new LevelRec(0x10A1D, 74),
    new LevelRec(0x10B0E, 41),
    new LevelRec(0x10C88, 83),
    new LevelRec(0x10DB3, 53),
    new LevelRec(0x10EA1, 45),
    new LevelRec(0x10FED, 71),
  };
  
  string[] objTypesCad =
    new[]  {
        "0 (back)",
        "1 (collect)",
        "2 (platform)",
        "3 (block)",
        "4 (spikes)",
        "5 (door)",
        "6 (mask)",
        "7 (? block and go up)",
        "8 (? block and go down)",
        "9 (? block and go down)",
        "A (Block)",
        "B (Pit)",
        "C (Block)",
        "D (Block)",
        "E (throwable stone)",
        "F (throwable box)"
    };
    
    public List<ObjectRec> getObjectsCad(int levelNo)
    {
      LevelRec lr = ConfigScript.getLevelRec(levelNo);
      int objCount = lr.objCount, addr = lr.objectsBeginAddr;
      var objects = new List<ObjectRec>();
      for (int i = 0; i < objCount; i++)
      {
          byte v = Globals.romdata[addr + i];
          if (levelNo != 4)
          {
              byte sx, sy, x, y;
              sx = Globals.romdata[addr - 4 * objCount + i];
              x  = Globals.romdata[addr - 3 * objCount + i];
              sy = Globals.romdata[addr - 2 * objCount + i];
              y  = Globals.romdata[addr - objCount + i];
              var obj = new ObjectRec(v, sx, sy, x, y);
              objects.Add(obj);
          }
          else  //C&D LEVEL D EXCEPTION, unaligned pointers
          {
              byte sx = Globals.romdata[addr - 4 * objCount + 1 + i];
              byte x  = Globals.romdata[addr - 3 * objCount + 1 + i];
              byte sy = Globals.romdata[addr - 2 * objCount + 1 + i];
              byte y  = Globals.romdata[addr - objCount + i];
              var obj = new ObjectRec(v, sx, sy, x, y);
              objects.Add(obj);
          }
      }
      return objects;
    }
    
    public bool setObjectsCad(int levelNo, List<ObjectRec> objects)
    {
      LevelRec lr = ConfigScript.getLevelRec(levelNo);
      int levelDhack = (levelNo == 4) ? 1 : 0;
      int addrBase = lr.objectsBeginAddr;
      int objCount = lr.objCount;
      try
      {
          for (int i = 0; i < objects.Count; i++)
          {
              var obj = objects[i];
              Globals.romdata[addrBase + i] = (byte)obj.type;
              Globals.romdata[addrBase - 4 * objCount + levelDhack + i] = (byte)obj.sx;
              Globals.romdata[addrBase - 3 * objCount + levelDhack + i] = (byte)obj.x;
              Globals.romdata[addrBase - 2 * objCount + levelDhack + i] = (byte)obj.sy;
              Globals.romdata[addrBase - objCount + i] = (byte)obj.y;

          }
          for (int i = objects.Count; i < objCount; i++)
          {
              Globals.romdata[addrBase + i] = 0xFF;
              Globals.romdata[addrBase - 4 * objCount + levelDhack + i] = 0xFF;
              Globals.romdata[addrBase - 3 * objCount + levelDhack + i] = 0xFF;
              Globals.romdata[addrBase - 2 * objCount + levelDhack + i] = 0xFF;
              Globals.romdata[addrBase - objCount + levelDhack + i] = 0xFF;
          }
          return true;
      
      }
      catch (System.IndexOutOfRangeException ex)
      {
          return false;
      }
    }
}