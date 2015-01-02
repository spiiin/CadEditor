using CadEditor;
using System;
using System.Drawing;
using System.Collections.Generic;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(2864, 20 , 8*5);   }
  public int getScreenWidth()          { return 5; }
  public int getScreenHeight()         { return 8; }
  public string getBlocksFilename()    { return "ninja_cats_4.png"; }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public GetObjectsFunc getObjectsFunc()   { return getObjects;  }
  public SetObjectsFunc setObjectsFunc()   { return setObjects;  }
  
  public GetLayoutFunc getLayoutFunc()     { return getLayout;   }
  LevelLayerData getLayout(int levelNo)
  {
    var levelLayers = new byte[][]
    {
        new byte[] { 0,1,2,3 },
        new byte[] { 4,5,6 },
        new byte[] { 7, },
        new byte[] { 8,9,10,11 },
        new byte[] { 12,13},
        new byte[] { 14 },
        new byte[] { 15,16,17,18,19 },
        new byte[] { 19 },
    };
    return new LevelLayerData(levelLayers[levelNo].Length, 1, levelLayers[levelNo]);
  }
  
  public IList<LevelRec> getLevelRecs() { return levelRecs; }
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0xA775, 9 , 4, 1, 0x0), //room 1
    new LevelRec(0xA792, 5 , 3, 1, 0x0), //room 2
    new LevelRec(0xA7A3, 4 , 1, 1, 0x0), //room 3
    new LevelRec(0xA7B1, 7 , 4, 1, 0x0), //room 4
    new LevelRec(0xA7C8, 5 , 2, 1, 0x0), //room 5
    new LevelRec(0xA7D9, 3 , 1, 1, 0x0), //room 6
    new LevelRec(0xA7E4, 10,  5, 1, 0x0), //room 7
    new LevelRec(0xA804, 1 ,  1, 1, 0x0), //room 8
  };
  
  public List<ObjectRec> getObjects(int levelNo)
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