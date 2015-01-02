using CadEditor;
using System.Collections.Generic;
//css_include Settings_CapcomBase.cs;
public class Data : CapcomBase
{ 
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginChrView.dll"
    };
  }
  public GameType getGameType()  { return GameType.DT2; }
  public OffsetRec getPalOffset()       { return new OffsetRec(0xF12E  , 32  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x20010 , 32  , 0x1000); } //15 for 1st level
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0, 16   , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0 , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xC010  , 8   , 0x440); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x210   , 256 , 16*15);   }
  public override int getScreenWidth()    { return 16; }
  public override int getScreenHeight()   { return 15; }
  public IList<LevelRec> getLevelRecs() { return levelRecsCad2; }
  public int getScrollsOffsetFromLayout() { return 538; } //offset scrolls array from layout array
  public GetObjectsFunc getObjectsFunc()   { return getObjectsCad2; }
  public SetObjectsFunc setObjectsFunc()   { return setObjectsCad2; }
  public override GetVideoPageAddrFunc getVideoPageAddrFunc() { return getChrAddress; }
  public bool isBuildScreenFromSmallBlocks() { return true; }
  public GetLayoutFunc getLayoutFunc()     { return getLayout;   }
  
  const int LEVEL_REC_COUNT = 30;
  
  LevelLayerData getLayout(int levelNo)
  {
    if (levelNo < LEVEL_REC_COUNT)
      return Utils.getLayoutLinear(levelNo);
    else
      return getLayoutForPrizes(levelNo - LEVEL_REC_COUNT);
  }
  
  LevelLayerData getLayoutForPrizes(int levelNo)
  {
    var levelLayers = new byte[][]
    {
        new byte[] { 5,6,7,5,6,7,5,6 },                                     //1-1
        new byte[] { 1, 0xC, 0x12, 1, 0xC, 0x13, 0x10, 0x11, 0xF },         //1-2
        new byte[] { 9, 0xA, 0xB, 0xC, 0xA, 0xD, 0xE },                     //1-3
        
        new byte[] { 0x18, 0x1A, 0x1D, 0x1E, 0x18, 0x1D, 0x1F, 0x20 },      //2-1
        new byte[] { 0x19, 0x18, 0x1A, 0x1B, 0x1C, 0x17, 0x18, 0x16 },      //2-2
        
        new byte[] { 0x2C, 0x2D, 0x2C, 0x2E, 0x27, 0x28, 0x27, 0x29 },      //3-1 begin
        //
    };
    return new LevelLayerData(levelLayers[levelNo].Length, 1, levelLayers[levelNo]);
  }
  
  public IList<LevelRec> levelRecsCad2 = new List<LevelRec>() 
  {
    //level 1
    new LevelRec(0xE401, 12, 8, 8, 0xDE7A, "1-1"),
    new LevelRec(0xE426, 15, 8, 8, 0xDE7A, "1-2"),
    new LevelRec(0xE454, 8 , 8, 8, 0xDE7A, "1-3"),
   //bonus
    new LevelRec(0xE471, 1 , 8, 8, 0xDE7A, "1-4 (boss)"),
    
    //level 2
    new LevelRec(0xE475, 10, 8, 8, 0xDEBA, "2-1"),
    new LevelRec(0xE494, 13, 8, 8, 0xDEBA, "2-2"),
    new LevelRec(0xE4BC, 1 , 8, 8, 0xDEBA, "2-3 (boss)"),
    //bonuses
    //copy of boss 2
    
    //level 3
    new LevelRec(0xE4C0, 28, 8, 8, 0xDEFA, "3-1"),
    new LevelRec(0xE515, 1 , 8, 8, 0xDEFA, "3-2 (boss)"),
    //bonuses
    //copy of boss 3
    
    //level 4
    new LevelRec(0xE5BB, 25, 8, 8, 0xDFBA, "4-1"),
    
    //level 5
    new LevelRec(0xE55B, 14, 8, 8, 0xDF7A, "5-1"),
    new LevelRec(0xE588, 15, 8, 8, 0xDF7A, "5-2"),
    new LevelRec(0xE5B7, 1 , 8, 8, 0xDF7A, "5-3 (boss)"),
    //bonuses
    //copy of boss 5
    //bonuses
    //copy of boss 5
    
    //level 6
    new LevelRec(0xE519, 21, 8, 8, 0xDF3A, "6-1"),
    new LevelRec(0xE559, 1 , 8, 8, 0xDF3A, "6-2 (boss)"),
    
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
    new LevelRec(0xE75C, 53, 8, 8, 0xDE7A, "1-1 stars"),
    new LevelRec(0xE7C7, 55, 8, 8, 0xDE7A, "1-2 stars"),
    new LevelRec(0xE836, 33, 8, 8, 0xDE7A, "1-3 stars"),
    
    new LevelRec(0xE879, 46, 8, 8, 0xDEBA, "2-1 stars"),
    new LevelRec(0xE8D6, 57, 8, 8, 0xDEBA, "2-2 stars"),
    
    new LevelRec(0xE949, 76, 8, 8, 0xDEFA, "3-1* stars"),
  };
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isLayoutEditorEnabled()   { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public List<ObjectRec> getPrizesCad2(int levelNo)
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
    return objects;
  }
  
  public bool setPrizesCad2(int levelNo, List<ObjectRec> objects)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int addrBase = lr.objectsBeginAddr;
    int objCount = lr.objCount;
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
  
  
  public List<ObjectRec> getObjectsCad2(int levelNo)
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
    return objects;
  }

  public bool setObjectsCad2(int levelNo, List<ObjectRec> objects)
  {
    //hack for prizes
    if (levelNo >= LEVEL_REC_COUNT)
      return  setPrizesCad2(levelNo, objects);
     
    
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int addrBase = lr.objectsBeginAddr;
    int objCount = lr.objCount;
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
  
  public static int getChrAddress(int id)
  {
      if (id >= 0x90)
        return ConfigScript.videoOffset.beginAddr + ConfigScript.videoOffset.recSize * (id - 0x90);
      return -1;
  }
}