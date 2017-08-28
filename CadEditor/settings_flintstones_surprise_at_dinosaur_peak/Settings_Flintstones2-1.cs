using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(0xD833, 1 , 8*96);   }
  public int getScreenWidth()             { return 8; }
  public int getScreenHeight()            { return 96; }
  public bool getScreenVertical()         { return true; }
  public string getBlocksFilename()       { return "flintstones2_1.png"; }
  public int    getPictureBlocksWidth()   { return 16; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return true; }
  public IList<LevelRec> getLevelRecs() { return levelRecs; }
  
  public GetObjectsFunc getObjectsFunc()   { return getObjects;  }
  public SetObjectsFunc setObjectsFunc()   { return setObjects;  }
  public GetLayoutFunc getLayoutFunc()     { return getLayout;   } 
   
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x0, 23, 1, 1, 0x0),
  };
  
  LevelLayerData getLayout(int levelNo)
  {
    int[] layer = new int[1];
    layer[0] = 0;
    return new LevelLayerData(1, 1, layer);
  }
  
  //addrs saved in ram at DD-DF-E1-E3
  public List<ObjectList> getObjects(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
      byte x    = Globals.romdata[0x14D84 + i];
      byte y    = Globals.romdata[0x14D9C + i];
      int realx = x * 8;
      int realy = y * 8;
      byte v    = Globals.romdata[0x14DB4 + i];
      var obj = new ObjectRec(v, 0, 0, realx, realy);
      objects.Add(obj);
    }
    return new List<ObjectList> { new ObjectList { objects = objects, name = "Objects" } };
  }

  public bool setObjects(int levelNo, List<ObjectList> objLists)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    var objects = objLists[0].objects;
    for (int i = 0; i < objects.Count; i++)
    {
        var obj = objects[i];
        byte x = (byte)(obj.x /8);
        byte y = (byte)(obj.y /8);
        Globals.romdata[0x14DB4 + i] = (byte)obj.type;
        Globals.romdata[0x14D84 + i] = x;
        Globals.romdata[0x14D9C + i] = y;
    }
    for (int i = objects.Count; i < objCount; i++)
    {
        Globals.romdata[0x14DB4 + i] = 0xFF;
        Globals.romdata[0x14D84 + i] = 0xFF;
        Globals.romdata[0x14D9C + i] = 0xFF;
    }
    return true;
  }
}