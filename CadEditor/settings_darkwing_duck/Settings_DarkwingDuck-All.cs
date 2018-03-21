using CadEditor;
using PluginMapEditor;
using System.Collections.Generic;
//css_include Settings_CapcomBase.cs;

public class Data : CapcomBase
{ 
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginMapEditor.dll",
      "PluginChrView.dll",
      "PluginEditLayout.dll",
      "PluginAnimEditor.dll",
    };
  }
  public OffsetRec getPalOffset()       { return new OffsetRec(0x1C36D, 32  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x30010, 16  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x20010, 16  , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x36F0 , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x3AF0 , 8   , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x10   , 300 , 0x40);   }
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecsDwd;}; }
  public int getScrollsOffsetFromLayout() { return 508; } //offset scrolls array from layout array
  public string[] getBlockTypeNames()   { return objTypesDwd;  }
  public GetObjectsFunc getObjectsFunc() { return getObjectsDwd; }
  public SetObjectsFunc setObjectsFunc() { return setObjectsDwd; }
  public string getObjTypesPicturesDir() { return "obj_sprites_dwd"; }
  public override GetLayoutFunc getLayoutFunc() { return dwdGetLayout; }
  public IList<LevelRec> levelRecsDwd = new List<LevelRec>() 
  {
    new LevelRec(0x10315, 51, 17, 4,  0x1DFA0),
    new LevelRec(0x10438, 60, 17, 4,  0x1DFE4), 
    new LevelRec(0x10584, 68, 17, 4,  0x1E028),  
    new LevelRec(0x106A0, 54, 10, 12, 0x1E06C), 
    new LevelRec(0x10816, 80, 19, 3,  0x1E0E4),  
    new LevelRec(0x10962, 63, 19, 3,  0x1E11D),  
    new LevelRec(0x10A89, 58, 19, 3,  0x1E156),  
  };
  
  public GetGroupsFunc getGroupsFunc() { return getGroups; }
  public GroupRec[] getGroups()
  {
    return new GroupRec[] { 
      new GroupRec("Brigde"         , 0,0,0,0, 0x01),
      new GroupRec("City"           , 1,1,1,2, 0x21),
      new GroupRec("Sewers"         , 2,2,2,4, 0x41),
      new GroupRec("Tower"          , 3,3,3,6, 0x61),
      new GroupRec("Forest"         , 4,4,4,10,0x81),
      new GroupRec("Warehouse"      , 5,5,5,12,0xA1),
      new GroupRec("FOWL’s Fortress", 6,6,6,9 ,0x101),
    };
  }
  
  string[] objTypesDwd =
    new[] {
        "0 (back)","1 (hook)","2 (platform)","3 (block)","4 (spikes)","5 (door)",
        "6","7","8","9","A","B","C","D","E","F"
    };
    
  public MapInfo[] getMapsInfo() { return mapsDwd; }
  public LoadMapFunc getLoadMapFunc() { return MapUtils.loadMapDwd; }
  public SaveMapFunc getSaveMapFunc() { return MapUtils.saveMapDwd; }

  MapInfo[] mapsDwd = new MapInfo[]
  { 
      new MapInfo(){ dataAddr = 0x80B1, palAddr = 0x1C43D, videoNo = 9 },
      new MapInfo(){ dataAddr = 0x83FC, palAddr = 0x8E6E , videoNo = 10 } 
  };
    
  public List<ObjectList> getObjectsDwd(int levelNo)
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
      return new List<ObjectList> { new ObjectList { objects = objects, name = "Objects" } };
  }
    
  public bool setObjectsDwd(int levelNo, List<ObjectList> objLists)
  {
      LevelRec lr = ConfigScript.getLevelRec(levelNo);
      int addrBase = lr.objectsBeginAddr;
      int objCount = lr.objCount;
      var objects = objLists[0].objects;
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
  
  public LevelLayerData dwdGetLayout(int curActiveLayout)
  {
      if (curActiveLayout < 6)
      {
          return Utils.getLayoutLinear(curActiveLayout);
      }
      
      int layoutAddr = ConfigScript.getLayoutAddr(curActiveLayout);
      int width =  ConfigScript.getLevelWidth(curActiveLayout);
      int height = ConfigScript.getLevelHeight(curActiveLayout);
      int[] layer = new int[width * height];
      for (int i = 0; i < width * height; i++)
          layer[i] = Globals.romdata[layoutAddr + i] + 256;
      return new LevelLayerData(width, height, layer, null, null);
  }
  
  //Anim Editor
  public static int getAnimCount()   { return 199; }
  public static int getAnimAddrHi()  { return Utils.getCapcomAnimAddr(5, 0xB4F0); }
  public static int getAnimAddrLo()  { return Utils.getCapcomAnimAddr(5, 0xB429); }
  public static int getFrameCount()  { return 314; }
  public static int getFrameAddrHi() { return Utils.getCapcomAnimAddr(5, 0x9C45); }
  public static int getFrameAddrLo() { return Utils.getCapcomAnimAddr(5, 0x9B0B); }
  public static int getCoordCount()  { return 208;/*256;*/ }
  public static int getCoordAddrHi() { return Utils.getCapcomAnimAddr(5, 0xAF23); }
  public static int getCoordAddrLo() { return Utils.getCapcomAnimAddr(5, 0xAE53); }
  public static byte[] getAnimPal()  { return new byte[]{ 0x00, 0x0F, 0x30, 0x27, 0x00, 0x0F, 0x27, 0x13, 0x00, 0x0f, 0x27, 0x15, 0x00, 0x0F, 0x37, 0x07 }; }
  public static int getAnimBankNo()  { return 5;}
}