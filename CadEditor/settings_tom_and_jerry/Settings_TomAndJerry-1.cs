using CadEditor;
using System;
using System.Collections.Generic;
//css_include Settings_CapcomBase.cs;
public class Data : CapcomBase
{ 
  public OffsetRec getPalOffset()       { return new OffsetRec(0, 32  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0, 16  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0, 16  , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0 , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0 , 8   , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(34135   , 1 , 160*46);   }
  public override int getScreenWidth()     { return 160; }
  public override int getScreenHeight()    { return 46; }
  public string getBlocksFilename()        { return "tom_and_jerry_1.png"; }
  public IList<LevelRec> getLevelRecs()    { return levelRecs; }
  public GetObjectsFunc getObjectsFunc()   { return getObjects;  }
  public SetObjectsFunc setObjectsFunc()   { return setObjects;  }
  public GetLayoutFunc getLayoutFunc()     { return getLayout;   } 
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return true; }
  public GetObjectDictionaryFunc getObjectDictionaryFunc() { return getObjectDictionary; }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x0, 52, 1, 1, 0x0),
  };
  
   public List<ObjectRec> getObjects(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    var objects = new List<ObjectRec>();
    int baseAddr = 0x8153;
    for (int i = 0; i < objCount; i++)
    {
        byte x    = Globals.romdata[baseAddr + i*4 + 0];
        byte y    = Globals.romdata[baseAddr + i*4 + 1];
        int realx = x* 32;
        int realy = y* 32;
        byte v    = Globals.romdata[baseAddr + i*4 + 3];
        byte data = Globals.romdata[baseAddr + i*4 + 2];
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
    int baseAddr = 0x8153;
    for (int i = 0; i < objects.Count; i++)
    {
      var obj = objects[i];
      Globals.romdata[baseAddr + i*4 + 0] = (byte) (obj.x/32);
      Globals.romdata[baseAddr + i*4 + 1] = (byte) (obj.y/32);
      Globals.romdata[baseAddr + i*4 + 2] = (byte)obj.additionalData["data"];
      Globals.romdata[baseAddr + i*4 + 3] = (byte) obj.type;
    }
    for (int i = objects.Count; i < objCount; i++)
    {
      Globals.romdata[baseAddr + i * 4 + 0] = 0xFF;
      Globals.romdata[baseAddr + i * 4 + 1] = 0xFF;
      Globals.romdata[baseAddr + i * 4 + 2] = 0xFF;
      Globals.romdata[baseAddr + i * 4 + 3] = 0xFF;
    }
    return true;
    return true;
  }
  
  LevelLayerData getLayout(int levelNo)
  {
    byte[] layer = new byte[1];
    layer[0] = 0;
    return new LevelLayerData(1, 1, layer);
  }
  public Dictionary<String,int> getObjectDictionary(int type)
  {
    return new Dictionary<String, int> { {"data", 0} };
  }
}