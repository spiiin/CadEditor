using CadEditor;
using PluginMapEditor;
using System.Collections.Generic;
//css_include Settings_CapcomBase.cs;

//css_reference PluginLevelParamsCad.dll

public class Data:CapcomBase
{
  public string[] getPluginNames() 
  {
    return new string[] {
      "PluginMapEditor.dll",
      "PluginChrView.dll",
      "PluginLevelParamsCad.dll",
      "PluginAnimEditor.dll",
      "PluginEditLayout.dll",
    };
  }
   
  public OffsetRec getPalOffset()       { return new OffsetRec(0x1C354, 32  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x30010, 16  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x20010, 16  , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x36F0 , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x3AF0 , 8   , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x10   , 300 , 0x40);   }
  public GetLevelRecsFunc getLevelRecsFunc() { return getLevelRecsCad; }
  public string[] getBlockTypeNames()   { return objTypesCad;  }
  
  public GetObjectsFunc getObjectsFunc() { return getObjectsCad; }
  public SetObjectsFunc setObjectsFunc() { return setObjectsCad; }
  
  public override GetLayoutFunc  getLayoutFunc()  { return getLayoutCad;   }          
  public override SetLayoutFunc  setLayoutFunc()  { return setLayoutCad;   } //auto crop hi-part of numbers >256
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public string getObjTypesPicturesDir() { return "obj_sprites_cad"; }
  
  //chip and dale specific
  public OffsetRec getBoxesBackOffset() { return new OffsetRec(0x1E909, 11  , 16);     }
  public int getLevelRecBaseOffset() { return 0x1E201; }
  public int getLevelRecDirOffset()  { return 0x10239; }
  public int getLayoutPtrAdd()       { return 0x10010; }
  public int getScrollPtrAdd()       { return 0x10010; }
  public int getDirPtrAdd()          { return 0x8010;  }
  public int getDoorRecBaseOffset()  { return 0x1E673; }
  
  public byte[] getScrollByteArray() { return new byte[] { 0x40, 0x40, 0x80, 0x03, 0x00, 0xA0, 0xC0, 0xE0 }; }
  
  public MapInfo[] getMapsInfo() { return mapsCad; }
  public LoadMapFunc getLoadMapFunc() { return MapUtils.loadMapCad; }
  public SaveMapFunc getSaveMapFunc() { return MapUtils.saveMapCad; }
  
  public GetGroupsFunc getGroupsFunc() { return getGroups; }

  MapInfo[] mapsCad = new MapInfo[]
  { 
      new MapInfo(){ dataAddr = 0x8237, palAddr = 0x8871, videoNo = 15 },
      new MapInfo(){ dataAddr = 0x83C2, palAddr = 0x923F, videoNo = -3 },
      new MapInfo(){ dataAddr = 0x8496, palAddr = 0x924F, videoNo = -3 },
      new MapInfo(){ dataAddr = 0x863E, palAddr = 0x926F, videoNo = -2 },
      new MapInfo(){ dataAddr = 0x856A, palAddr = 0x925F, videoNo = -2 },
      //new MapInfo(){ dataAddr = 0x8061, palAddr = 0x1C494, videoNo = 12 },
      //new MapInfo(){ dataAddr = 0x8152, palAddr = 0x1C4A4, videoNo = 13 },
  };
  
  public int getLayoutAddr(int no)
  {
      int baseOffset = getLevelRecBaseOffset();
      return (Globals.romdata[baseOffset + 165 + no] << 8) + Globals.romdata[baseOffset + 150 + no] + getLayoutPtrAdd();
  }
  
  public int getScrollAddr(int no)
  {
      int baseOffset = getLevelRecBaseOffset();
      return (Globals.romdata[baseOffset + 195 + no] << 8) + Globals.romdata[baseOffset + 180 + no] + getScrollPtrAdd();
  }
  
  public int getLayoutWidth(int no)
  {
      int baseOffset = getLevelRecBaseOffset();
      return Globals.romdata[baseOffset + 45 + no]+1;
  }
  
  public int getLayoutHeight(int no)
  {
       int baseOffset = getLevelRecBaseOffset();
       return Globals.romdata[baseOffset + 30 + no]+1;
  }
  
  public LevelLayerData getLayoutCad(int curActiveLayout)
  {
      var layout = Utils.getLayoutLinear(curActiveLayout);
      
      if ((curActiveLayout == 5) || (curActiveLayout == 8))
      {
          //additional +256 for levels E and H
          var layer = layout.layer;
          for (int i = 0; i < layer.Length; i++)
          {
            layer[i] += 0x100;
          }
      }
      
      var scrolls = new int[layout.layer.Length];
      int scrollAddr = getScrollAddr(curActiveLayout);
      for (int i = 0; i < scrolls.Length; i++)
      {
        scrolls[i] = Globals.romdata[scrollAddr + i];
      }
      layout.scroll = scrolls;
      return layout;
  }
  
  public bool setLayoutCad(LevelLayerData curActiveLayerData, int curActiveLayout)
  {
      int layerAddr, scrollAddr, width, height;
      layerAddr = getLayoutAddr(curActiveLayout);
      scrollAddr = getScrollAddr(curActiveLayout);
      width = curActiveLayerData.width;
      height = curActiveLayerData.height;
      for (int i = 0; i < width * height; i++)
      {
          Globals.romdata[layerAddr + i] = (byte)curActiveLayerData.layer[i];
      }
      if (curActiveLayerData.scroll != null)
      {
          for (int i = 0; i < width * height; i++)
          {
              Globals.romdata[scrollAddr + i] = (byte)curActiveLayerData.scroll[i];
          }
      }
      return true;
  }
  
  public GroupRec[] getGroups()
  {
    GlobalsCad.reloadLevelParamsData();
    var levelsData = GlobalsCad.levelData;
    //var doorsData = GlobalsCad.doorsData;
    var levelNames = new[] {
      new { name = "Level 0", screen = 0x01 },
      new { name = "Level A", screen = 0x34 },
      new { name = "Level B", screen = 0x1F },
      new { name = "Level C", screen = 0x5A },
      new { name = "Level D", screen = 0x3E },
      new { name = "Level E", screen = 0x113 },
      new { name = "Level F", screen = 0x84 },
      new { name = "Level G", screen = 0x6B },
      new { name = "Level H", screen = 0x101 },
      new { name = "Level I", screen = 0x90 },
      new { name = "Level J", screen = 0xA2 }
    };
    /*var doorNames = new string[] {
      "Door 1",
      "Door 2",
      "Door 3",
      "Door 4",
      "Door 5",
      "Door 6",
      "Door 7",
      "Door 8",
      "Door 9",
      "Door A",
      "Door B",
      "Door C",
      "Door D",
      "Door E",
      "Door F",
      "Door 10",
      "Door 11",
      "Door 12",
      "Door 13",
      "Door 14",
      "Door 15",
      "Door 16",
      "Door 17",
      "Door 18"      
    };*/
    
    var groups = new GroupRec[GlobalsCad.LEVELS_COUNT];
    
    for (int i = 0; i < GlobalsCad.LEVELS_COUNT; i++)
    {
      var levelData = levelsData[i];
      var levelName = levelNames[i];
      groups[i] = new GroupRec(levelName.name, (levelData.backId) - 0x90, levelData.bigBlockId, levelData.bigBlockId, levelData.palId, levelName.screen);
    }
    
    return groups;
  }
  
  public IList<LevelRec> getLevelRecsCad()
  {
      var groups = ConfigScript.getGroups();
      return new List<LevelRec>() 
      {
          new LevelRec(0x10388, 76, getLayoutWidth(0), getLayoutHeight(0), getLayoutAddr(0), groups[0].name, 0, groups[0]), //broke first boss after save?
          new LevelRec(0x10456, 31, getLayoutWidth(1), getLayoutHeight(1), getLayoutAddr(1), groups[1].name, 0, groups[1]),
          new LevelRec(0x105A1, 73, getLayoutWidth(2), getLayoutHeight(2), getLayoutAddr(2), groups[2].name, 0, groups[2]),
          new LevelRec(0x106D1, 57, getLayoutWidth(3), getLayoutHeight(3), getLayoutAddr(3), groups[3].name, 0, groups[3]),
          new LevelRec(0x10890, 97, getLayoutWidth(4), getLayoutHeight(4), getLayoutAddr(4), groups[4].name, 0, groups[4]),
          new LevelRec(0x10A1D, 74, getLayoutWidth(5), getLayoutHeight(5), getLayoutAddr(5), groups[5].name, 0, groups[5]),
          new LevelRec(0x10B0E, 41, getLayoutWidth(6), getLayoutHeight(6), getLayoutAddr(6), groups[6].name, 0, groups[6]),
          new LevelRec(0x10C88, 83, getLayoutWidth(7), getLayoutHeight(7), getLayoutAddr(7), groups[7].name, 0, groups[7]),
          new LevelRec(0x10DB3, 53, getLayoutWidth(8), getLayoutHeight(8), getLayoutAddr(8), groups[8].name, 0, groups[8]),
          new LevelRec(0x10EA1, 45, getLayoutWidth(9), getLayoutHeight(9), getLayoutAddr(9), groups[9].name, 0, groups[9]),
          new LevelRec(0x10FED, 71, getLayoutWidth(10), getLayoutHeight(10), getLayoutAddr(10), groups[10].name, 0, groups[10]),
      };
  }
  
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
    
    public List<ObjectList> getObjectsCad(int levelNo)
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
      return new List<ObjectList> { new ObjectList { objects = objects, name = "Objects" } };
    }
    
    public bool setObjectsCad(int levelNo, List<ObjectList> objLists)
    {
      LevelRec lr = ConfigScript.getLevelRec(levelNo);
      int levelDhack = (levelNo == 4) ? 1 : 0;
      int addrBase = lr.objectsBeginAddr;
      int objCount = lr.objCount;
      var objects = objLists[0].objects;
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
    
  //--------------------------------------------------------------------------------------------
  //Anim Editor
  public static int getAnimCount()   { return 168; }
  public static int getAnimAddrHi()  { return Utils.getCapcomAnimAddr(5, 0xB55F); }
  public static int getAnimAddrLo()  { return Utils.getCapcomAnimAddr(5, 0xB4B6); }
  public static int getFrameCount()  { return 300; }
  public static int getFrameAddrHi() { return Utils.getCapcomAnimAddr(5, 0x9CAE); }
  public static int getFrameAddrLo() { return Utils.getCapcomAnimAddr(5, 0x9B82); }
  public static int getCoordCount()  { return 183; }
  public static int getCoordAddrHi() { return Utils.getCapcomAnimAddr(5, 0xB145); }
  public static int getCoordAddrLo() { return Utils.getCapcomAnimAddr(5, 0xB08E); }
  public static byte[] getAnimPal()  { return new byte[] { 0x00, 0x0F, 0x37, 0x16, 0x00, 0x0F, 0x30, 0x10, 0x00, 0x0f, 0x30, 0x29, 0x00, 0x0F, 0x37, 0x27 }; }
  public static int getAnimBankNo()  { return 5;}
}