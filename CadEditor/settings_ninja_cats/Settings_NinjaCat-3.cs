using CadEditor;
using System;
using System.Drawing;
using System.Collections.Generic;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(1940, 22 , 8*5);   }
  public int getScreenWidth()          { return 5; }
  public int getScreenHeight()         { return 8; }
  public string getBlocksFilename()    { return "ninja_cats_3.png"; }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public GetObjectsFunc getObjectsFunc()   { return getObjects;  }
  public SetObjectsFunc setObjectsFunc()   { return setObjects;  }
  
  public GetLayoutFunc getLayoutFunc()     { return getLayout;   }
  LevelLayerData getLayout(int levelNo)
  {
    var levelLayers = new byte[][]
    {
        new byte[] { 0,1 },
        new byte[] { 2,3,4 },
        new byte[] { 5 },
        new byte[] { 6,7,8 },
        new byte[] { 9 },
        new byte[] { 10 },
        new byte[] { 11,12 },
        new byte[] { 13, },
        new byte[] { 16 }, //zero
        new byte[] { 14,15 },
        new byte[] { 17,18,19,20,21 },
        new byte[] { 21 }
    };
    return new LevelLayerData(levelLayers[levelNo].Length, 1, levelLayers[levelNo]);
  }
  
  public IList<LevelRec> getLevelRecs() { return levelRecs; }
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0xA6B2, 5 , 2, 1, 0x0), //room 1
    new LevelRec(0xA6C3, 8 , 3, 1, 0x0), //room 2
    new LevelRec(0xA6DD, 5 , 1, 1, 0x0), //room 3
    new LevelRec(0xA6EE, 7 , 3, 1, 0x0), //room 4
    new LevelRec(0xA705, 1 , 1, 1, 0x0), //room 5
    new LevelRec(0xA70A, 2 , 1, 1, 0x0), //room 6
    new LevelRec(0xA711, 3 , 2, 1, 0x0), //room 7
    new LevelRec(0xA71D, 3, 1, 1, 0x0), //room 8
    new LevelRec(0xA728, 0, 1, 1, 0x0), //room 9
    new LevelRec(0xA72C, 4, 2, 1, 0x0), //room 10
    new LevelRec(0xA73A, 10, 5, 1, 0x0), //room 11
    new LevelRec(0xA75A, 3, 1, 1, 0x0), //room 12
  };
  
  public List<ObjectList> getObjects(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    int baseAddr = lr.objectsBeginAddr;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
      byte x    = Globals.romdata[baseAddr + objCount*0 + i + 1];
      byte y    = Globals.romdata[baseAddr + objCount*1 + i + 2];
      byte v    = Globals.romdata[baseAddr + objCount*2 + i + 2];
      int scrx  = x >> 4; scrx &= 0x7; //if bit 8 set, that something happen
      int realx = (x &0x0F)*16;
      int realy = y;
      var obj = new ObjectRec(v, scrx, 0, realx, realy);
      objects.Add(obj);
    }
    return new List<ObjectList> { new ObjectList { objects = objects, name = "Objects" } };
  }

  public bool setObjects(int levelNo, List<ObjectList> objLists)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    int baseAddr = lr.objectsBeginAddr;
    var objects = objLists[0].objects;
    for (int i = 0; i < objects.Count; i++)
    {
        var obj = objects[i];
        byte x = (byte)((obj.x >> 4) | (obj.sx << 4));
        byte y = (byte)(obj.y & 0xF0);  //first bits can demand enemy creation
        Globals.romdata[baseAddr + objCount*0 + i + 1] = x;
        Globals.romdata[baseAddr + objCount*1 + i + 2] = y;
        Globals.romdata[baseAddr + objCount*2 + i + 2] = (byte)obj.type;
    }
    for (int i = objects.Count; i < objCount; i++)
    {
        Globals.romdata[baseAddr + objCount*0 + i + 1] = 0xFF;
        Globals.romdata[baseAddr + objCount*1 + i + 2] = 0xFF;
        Globals.romdata[baseAddr + objCount*2 + i + 2] = 0xFF;
    }
    return true;
  }
}