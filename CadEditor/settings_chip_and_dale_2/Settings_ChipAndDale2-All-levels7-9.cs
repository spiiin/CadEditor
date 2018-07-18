using CadEditor;
using System.Collections.Generic;
//css_include shared_settings/CapcomBase.cs;
//css_include shared_settings/Dt2Cad2.cs;

//Changes for levels 7,8,9:
// screensOffset
// palOffset

public class Data : CapcomBase
{ 
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginChrView.dll",
      "PluginEditLayout.dll"
    };
  }
  public OffsetRec getPalOffset()       { return new OffsetRec(0xF291 - 16  , 32  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x20010 , 32  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0, 16   , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0 , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xC010  , 8   , 0x440); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x210 + 32, 256 , 16*15, 16, 15);   }
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecsCad2;}; }
  public int getScrollsOffsetFromLayout()  { return 538; } //offset scrolls array from layout array
  public GetObjectsFunc getObjectsFunc()   { return getObjectsCad2; }
  public SetObjectsFunc setObjectsFunc()   { return setObjectsCad2; }
  public override GetBlocksFunc        getBlocksFunc()        { return BlockUtils.getBlocksDt2;}
  public override SetBlocksFunc        setBlocksFunc()        { return BlockUtils.setBlocksDt2;}
  public override GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public bool isBuildScreenFromSmallBlocks() { return true; }
  public override GetLayoutFunc getLayoutFunc()       { return getLayout;   }
  
  const int LEVEL_REC_COUNT = 15;
  
  LevelLayerData getLayout(int levelNo)
  {
    if (levelNo < LEVEL_REC_COUNT)
      return Utils.getLayoutLinear(levelNo);
    else
      return getLayoutForPrizes(levelNo - LEVEL_REC_COUNT);
  }
  
  LevelLayerData getLayoutForPrizes(int levelNo)
  {
    var levelLayers = new int[][]
    {
        new int[] { 5,6,7,5,6,7,5,6 },                                     //1-1
        new int[] { 1, 0xC, 0x12, 1, 0xC, 0x13, 0x10, 0x11, 0xF },         //1-2
        new int[] { 9, 0xA, 0xB, 0xC, 0xA, 0xD, 0xE },                     //1-3
        
        new int[] { 0x18, 0x1A, 0x1D, 0x1E, 0x18, 0x1D, 0x1F, 0x20 },      //2-1
        new int[] { 0x19, 0x18, 0x1A, 0x1B, 0x1C, 0x17, 0x18, 0x16 },      //2-2
        
        new int[] { 0x2C, 0x2D, 0x2C, 0x2E, 0x27, 0x28, 0x27, 0x29 },      //3-1 begin
        //
    };
    return new LevelLayerData(levelLayers[levelNo].Length, 1, levelLayers[levelNo]);
  }
  
  public GetGroupsFunc getGroupsFunc() { return getGroups; }
  
  public GroupRec[] getGroups()
  {
    return new GroupRec[] { 
      //wrong pals
      new GroupRec("1-1"             , 15,0,0,0, 0x05),
      new GroupRec("1-2"             , 16,0,0,2, 0x01),
      new GroupRec("1-boss"          , 15,0,0,3, 0x02),
      new GroupRec("2-any"           , 17,1,1,4, 0x15),
      new GroupRec("3-any"           , 18,2,2,5, 0x21),
      new GroupRec("4-any"           , 21,2,2,5, 0x51),
      new GroupRec("5-any"           , 19,4,4,7, 0x43),
      new GroupRec("5-boss1"         , 19,6,6,7, 0x40),
      new GroupRec("5-boss2"         , 27,6,6,7, 0x41), 
      new GroupRec("6-any"           , 25,3,3,6, 0x2f),
      new GroupRec("7-any"           , 20,3,3,6, 0x5f),
      new GroupRec("8-any"           , 22,1,1,6, 0x6c),
      new GroupRec("8-boss"          , 26,6,6,0, 0x03),
      new GroupRec("9-any"           , 23,5,5,0, 0x84),
      new GroupRec("monty bonus"     , 21,1,1,0, 0x08),
      new GroupRec("mini-game"       , 29,6,6,2, 0x3F),
      new GroupRec("mini-game-2"     , 29,4,4,2, 0x42),
      
    };
  }
  
  public IList<LevelRec> levelRecsCad2 = new List<LevelRec>() 
  {
    //level 7
    new LevelRec(0xE607, 8 , 8, 8, 0xDFFA, "7-1"),
    new LevelRec(0xE620, 7 , 8, 8, 0xDFFA, "7-2"),
    new LevelRec(0xE636, 7 , 8, 8, 0xDFFA, "7-3"),
    new LevelRec(0xE64C, 11, 8, 8, 0xDFFA, "7-4"),
    new LevelRec(0xE66E, 1 , 8, 8, 0xDFFA, "7-5 (boss)"),
    
    //level 8
    new LevelRec(0xE672, 9 , 8, 8, 0xE03A, "8-1"),
    new LevelRec(0xE68E, 23, 8, 8, 0xE03A, "8-2"),
    //bonus
    new LevelRec(0xE6B0, 9 , 8, 8, 0xE03A, "8-3"),
    new LevelRec(0xE6CC, 1 , 8, 8, 0xE03A, "8-4 (boss)"),
    
    //level 9
    new LevelRec(0xE6D0, 10, 8, 8, 0xE07A, "9-1"),
    new LevelRec(0xE6EF, 7 , 8, 8, 0xE07A, "9-2"),
    //bonus
    new LevelRec(0xE705, 12, 8, 8, 0xE07A, "9-3"),
    new LevelRec(0xE72A, 5 , 8, 8, 0xE07A, "9-4"),
    new LevelRec(0xE73A, 4 , 8, 8, 0xE07A, "9-5"),
    new LevelRec(0xE747, 1 , 8, 8, 0xE07A, "9-6 (boss)"),
    
    //prizes
    new LevelRec(0xECC8, 40, 8, 8, 0xDFFA, "7-1 stars"),
    new LevelRec(0xED19, 20, 8, 8, 0xDFFA, "7-2 stars"),
    new LevelRec(0xED42, 27, 8, 8, 0xDFFA, "7-3 stars"),
    new LevelRec(0xED79, 31, 8, 8, 0xDFFA, "7-4 stars"),
    
    new LevelRec(0xEDB8, 57, 8, 8, 0xE03A, "8-1 stars"),
    new LevelRec(0xEE2B, 23, 8, 8, 0xE03A, "8-2 stars"),
    new LevelRec(0xEE5A, 32, 8, 8, 0xE03A, "8-3 stars"),
  };
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public List<ObjectList> getPrizesCad2(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int addr = lr.objectsBeginAddr;
    int objCount = lr.objCount;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
      int xx = Globals.romdata[addr + i * 2 + 0];
      int yy = Globals.romdata[addr + i * 2 + 1];
      int sx = (xx >> 4);
      int v =  (yy >> 4);
      int sy = 0;
      int x = (xx & 0x0F) * 32;
      int y = (yy & 0x0F) * 32;
      var obj = new ObjectRec(v, sx, sy, x, y);
      objects.Add(obj);
    }
    return new List<ObjectList> { new ObjectList { objects = objects, name = "Items" } };
  }
  
  public bool setPrizesCad2(int levelNo, List<ObjectList> objLists)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int addrBase = lr.objectsBeginAddr;
    int objCount = lr.objCount;
    var objects = objLists[0].objects;
    for (int i = 0; i < objects.Count; i++)
    {
      var obj = objects[i];
      Globals.romdata[addrBase + i * 2 + 0] = (byte)((obj.x / 32) | (obj.sx << 4));
      Globals.romdata[addrBase + i * 2 + 1] = (byte)((obj.y / 32) | (obj.type << 4));
    }
    for (int i = objects.Count; i < objCount; i++)
    {
      Globals.romdata[addrBase + i * 2 + 0] = 0xFF;
      Globals.romdata[addrBase + i * 2 + 1] = 0xFF;
    }
    return true;
  }
  
  
  public List<ObjectList> getObjectsCad2(int levelNo)
  {
    //hack for prizes
    if (levelNo >= LEVEL_REC_COUNT)
      return getPrizesCad2(levelNo);
      
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int addr = lr.objectsBeginAddr;
    int objCount = lr.objCount;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
      int v  = Globals.romdata[addr + i * 3 + 2];
      int xx = Globals.romdata[addr + i * 3 + 0];
      int yy = Globals.romdata[addr + i * 3 + 1];
      int sx = (xx >> 4);
      int sy = (yy >> 4);
      int x = (xx & 0x0F) * 32;
      int y = (yy & 0x0F) * 32;
      var obj = new ObjectRec(v, sx, sy, x, y);
      objects.Add(obj);
    }
    return new List<ObjectList> { new ObjectList { objects = objects, name = "Objects" } };
  }

  public bool setObjectsCad2(int levelNo, List<ObjectList> objLists)
  {
    //hack for prizes
    if (levelNo >= LEVEL_REC_COUNT)
      return setPrizesCad2(levelNo, objLists);
     
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int addrBase = lr.objectsBeginAddr;
    int objCount = lr.objCount;
    var objects = objLists[0].objects;
    for (int i = 0; i < objects.Count; i++)
    {
      var obj = objects[i];
      Globals.romdata[addrBase + i * 3 + 2] = (byte)obj.type;
      Globals.romdata[addrBase + i * 3 + 0] = (byte)((obj.x / 32) | (obj.sx << 4));
      Globals.romdata[addrBase + i * 3 + 1] = (byte)((obj.y / 32) | (obj.sy << 4));
    }
    for (int i = objects.Count; i < objCount; i++)
    {
      Globals.romdata[addrBase + i * 3 + 2] = 0xFF;
      Globals.romdata[addrBase + i * 3 + 0] = 0xFF;
      Globals.romdata[addrBase + i * 3 + 1] = 0xFF;
    }
    return true;
  }
}