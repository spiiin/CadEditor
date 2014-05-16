using CadEditor;
using System.Collections.Generic;
//css_include Settings_Flintstones-Utils.cs;
public class Data
{ 
  public GameType getGameType()           { return GameType.TT; }
  public OffsetRec getScreensOffset()     { return new OffsetRec(0x28B1, 1 , 8*80);   }
  public int getScreenWidth()             { return 8; }
  public int getScreenHeight()            { return 80; }
  public bool getScreenVertical()         { return true; }
  public string getBlocksFilename()       { return "flintstones_4.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return true; }
  public bool isVideoEditorEnabled()    { return false; }
  
  public GetObjectsFunc getObjectsFunc()   { return getObjects;  }
  public SetObjectsFunc setObjectsFunc()   { return setObjects;  }
  public GetLayoutFunc getLayoutFunc()     { return FliUtils.getLayout;   }
  public GetObjectDictionaryFunc getObjectDictionaryFunc() { return FliUtils.getObjectDictionary; }
  
  public IList<LevelRec> getLevelRecs() { return levelRecs; }
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x1165F, 25, 1, 1, 0x0),
  };
  
   //addrs saved in ram at D7-D9-DC-DC
  public List<ObjectRec> getObjects(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    int baseAddr = lr.objectsBeginAddr;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
      byte x    = Globals.romdata[baseAddr + objCount*0 + i];
      byte y    = Globals.romdata[baseAddr + objCount*2 + i];
      int realx = x * 8 + 32*32;
      int realy = y * 8;
      byte v    = Globals.romdata[baseAddr + objCount*3 + i];
      byte data = Globals.romdata[baseAddr + objCount*1 + i];
      var dataDict = new Dictionary<string,int>();
      dataDict["data"] = data;
      var obj = new ObjectRec(v, 0, 0, realx, realy, dataDict);
      objects.Add(obj);
    }
    return objects;
  }

  public bool setObjects(int levelNo, List<ObjectRec> objects)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    int baseAddr = lr.objectsBeginAddr;
    for (int i = 0; i < objects.Count; i++)
    {
        var obj = objects[i];
        byte x = (byte)((obj.x-32*32)/8);
        byte y = (byte)(obj.y /8);
        Globals.romdata[baseAddr + objCount*3 + i] = (byte)obj.type;
        Globals.romdata[baseAddr + objCount*1 + i] = (byte)obj.additionalData["data"];
        Globals.romdata[baseAddr + objCount*0 + i] = x;
        Globals.romdata[baseAddr + objCount*2 + i] = y;
    }
    for (int i = objects.Count; i < objCount; i++)
    {
        Globals.romdata[baseAddr + objCount*0 + i] = 0xFF;
        Globals.romdata[baseAddr + objCount*1 + i] = 0xFF;
        Globals.romdata[baseAddr + objCount*2 + i] = 0xFF;
        Globals.romdata[baseAddr + objCount*3 + i] = 0xFF;
    }
    return true;
  }
}