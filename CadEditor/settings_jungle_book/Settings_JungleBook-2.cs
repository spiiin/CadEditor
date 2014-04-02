using CadEditor;
using System.Collections.Generic;
//css_include Settings_CapcomBase.cs;
public class Data : CapcomBase
{ 
  public OffsetRec getPalOffset()       { return new OffsetRec(0, 32  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0, 16  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0, 16  , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0 , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0 , 8   , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x18149 - 24, 1 , 24*64);   }
  public override int getScreenWidth()    { return 24; }
  public override int getScreenHeight()   { return 64; }
  public string getBlocksFilename() { return "jungle_book_2.png"; }
  public IList<LevelRec> getLevelRecs() { return levelRecsJB; }
  public GetObjectsFunc getObjectsFunc()   { return getObjectsJungleBook;  }
  public SetObjectsFunc setObjectsFunc()   { return setObjectsJungleBook;  }
  public SortObjectsFunc sortObjectsFunc() { return sortObjectsJungleBook; }
  public GetLayoutFunc getLayoutFunc()     { return getLayoutJungleBook;   } 
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return true; }
  public bool isVideoEditorEnabled()    { return false; }
  
  public IList<LevelRec> levelRecsJB = new List<LevelRec>() 
  {
    new LevelRec(0x0, 47, 1, 1, 0x0),
  };
  
  //addrs saved in ram at 77-79-7E-81
  public List<ObjectRec> getObjectsJungleBook(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
        byte y    = Globals.romdata[0x187E8 + i];
        byte x    = Globals.romdata[0x187BB + i];
        byte data = Globals.romdata[0x18842 + i];
        byte v    = Globals.romdata[0x18815 + i];
        int realx = x* 32 + 16;
        int realy = y* 32 + 16;
        var dataDict = new Dictionary<string,int>();
        dataDict["data"] = data;
        var obj = new ObjectRec(v, 0, 0, realx, realy, dataDict);
        objects.Add(obj);
    }
    return objects;
  }

  public bool setObjectsJungleBook(int levelNo, List<ObjectRec> objects)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    for (int i = 0; i < objects.Count; i++)
    {
        var obj = objects[i];
        byte x = (byte)((obj.x - 16) /32);
        byte y = (byte)((obj.y - 16) /32);
        Globals.romdata[0x187E8 + i] = y;
        Globals.romdata[0x187BB + i] = x;
        Globals.romdata[0x18842 + i] = (byte)obj.additionalData["data"];
        Globals.romdata[0x18815 + i] = (byte)obj.type;
    }
    for (int i = objects.Count; i < objCount; i++)
    {
        Globals.romdata[0x187E8 + i] = 0xFF;
        Globals.romdata[0x187BB + i] = 0xFF;
        Globals.romdata[0x18842 + i] = 0xFF;
        Globals.romdata[0x18815 + i] = 0xFF;
    }
    return true;
  }
  
  public void sortObjectsJungleBook(int levelNo, List<ObjectRec> objects)
  {
    objects.Sort((o1, o2) => { return o1.x > o2.x ? 1 : o1.x < o2.x ? -1 : o1.y < o2.y ? -1 : o1.y > o2.y ? 1 : 0; });
  }
  
  LevelLayerData getLayoutJungleBook(int levelNo)
  {
    byte[] layer = new byte[1];
    layer[0] = 0;
    return new LevelLayerData(1, 1, layer);
  }
}