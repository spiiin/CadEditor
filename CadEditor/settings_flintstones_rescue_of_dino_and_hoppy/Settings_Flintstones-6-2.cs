using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public GameType getGameType()           { return GameType.TT; }
  public OffsetRec getScreensOffset()     { return new OffsetRec(0x52A9, 1 , 8*48);   }
  public int getScreenWidth()             { return 8; }
  public int getScreenHeight()            { return 48; }
  public bool getScreenVertical()         { return true; }
  public string getBlocksFilename()       { return "flintstones_6-2.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
  
  public GetObjectsFunc getObjectsFunc()   { return getObjects;  }
  public SetObjectsFunc setObjectsFunc()   { return setObjects;  }
  ///public int getMaxObjCoordX()           { return 60*32; }
  ///public int getMaxObjCoordY()           { return 8 *32; }
  //public SortObjectsFunc sortObjectsFunc() { return sortObjects; }
  public GetLayoutFunc getLayoutFunc()     { return getLayout;   } 
  
  public IList<LevelRec> getLevelRecs() { return levelRecs; }
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x0, 0, 1, 1, 0x0),
  };
  
  LevelLayerData getLayout(int levelNo)
  {
    byte[] layer = new byte[1];
    layer[0] = 0;
    return new LevelLayerData(1, 1, layer);
  }
  
  //addrs saved in ram at D7-D9-DC-DC
  public List<ObjectRec> getObjects(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    var objects = new List<ObjectRec>();
    /*for (int i = 0; i < objCount; i++)
    {
      byte x    = Globals.romdata[0x113AF + i];
      byte y    = Globals.romdata[0x113F5 + i];
      int realx = x * 8;
      int realy = y * 8;
      byte v    = Globals.romdata[0x11418 + i];
      byte data = Globals.romdata[0x113D2 + i];
      var dataDict = new Dictionary<string,int>();
      dataDict["data"] = data;
      var obj = new ObjectRec(v, 0, 0, realx, realy, dataDict);
      objects.Add(obj);
    }*/
    return objects;
  }

  public bool setObjects(int levelNo, List<ObjectRec> objects)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    /*for (int i = 0; i < objects.Count; i++)
    {
        var obj = objects[i];
        byte x = (byte)(obj.x /8);
        byte y = (byte)(obj.y /8);
        Globals.romdata[0x11418 + i] = (byte)obj.type;
        Globals.romdata[0x113D2 + i] = (byte)obj.additionalData["data"];
        Globals.romdata[0x113AF + i] = x;
        Globals.romdata[0x113F5 + i] = y;
    }
    for (int i = objects.Count; i < objCount; i++)
    {
        Globals.romdata[0x11418 + i] = 0xFF;
        Globals.romdata[0x113D2 + i] = 0xFF;
        Globals.romdata[0x113AF + i] = 0xFF;
        Globals.romdata[0x113F5 + i] = 0xFF;
    }*/
    return true;
  }
}