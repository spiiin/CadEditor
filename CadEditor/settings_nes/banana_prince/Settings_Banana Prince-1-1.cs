using CadEditor;
using System;
using System.Collections.Generic;
using System.Linq;
//css_include shared_settings/SharedUtils.cs;
//css_include banana_prince/BananaUtils.cs;

public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(0x403C, 7, 64, 8, 8);   } 
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk(new[] {"chr1.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0, 1 , 0x1000);  }
  public int getBlocksCount()           { return 64; }
  
  public GetBigBlocksFunc     getBigBlocksFunc() { return BananaUtils.getBigBlocks;}
  public SetBigBlocksFunc     setBigBlocksFunc() { return BananaUtils.setBigBlocks;}
  public GetBlocksFunc        getBlocksFunc() { return BananaUtils.getBlocksConsts;}
  public SetBlocksFunc        setBlocksFunc() { return null;}
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal1.bin"}); }
  public SetPalFunc           setPalFunc()    { return null;}
  
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x463C, 1 , 0x4000); }
  public int getPalBytesAddr()          { return 0x4964; }
  public int getBigBlocksCount() { return 202; }
  
  public GetObjectsFunc getObjectsFunc()   { return getObjects;  }
  public SetObjectsFunc setObjectsFunc()   { return setObjects;  }
  public GetLayoutFunc getLayoutFunc()     { return getLayout;   } 
  public GetObjectDictionaryFunc getObjectDictionaryFunc() { return getObjectDictionary; }
  
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x18B71, 22, 7, 1, 0x0),
  };
  
  //----------------------------------------------------------------------------

  //decode table
  Dictionary<int,int> enemyNoToEnemyType = new Dictionary<int, int> {
      {0xE300, 0}, //DOOR
      {0xE402, 2}, //MASTER
      {0xE403, 3}, //RING
      {0xE404, 4}, //INVINCIBILITY
      {0xE405, 5}, //BANANA
      {0xE406, 6}, //BANANAS
      {0xE407, 7}, //FLOWER
      {0xE408, 8}, //LIFE
      {0xE409, 9}, //CHIKEN
      {0xE40A, 0xA}, //POINTS
      {0xE40B, 0xB}, //CHIKEN BONUS
      {0xE107, 0xC}, //MOVING PLATFORM
      {0x0ECC, 0xD}, //RED JUMPING ENEMY
      {0x0646, 0xE}, //GREEN WALKING ENEMY
  };
  
  public List<ObjectList> getObjects(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    int baseAddr = lr.objectsBeginAddr;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
      byte data = Globals.romdata[baseAddr + objCount*0 + i];
      byte x    = Globals.romdata[baseAddr + objCount*1 + i];
      byte sx   = (byte)(x & 0x0F);
      x = (byte)(x & 0xF0);
      byte y    = Globals.romdata[baseAddr + objCount*2 + i];
      byte sy   = (byte)(y & 0x0F);
      y = (byte)(y & 0xF0);
      int  v    = Utils.readWordUnsigned(Globals.romdata, baseAddr + objCount*3 + i*2);
      int enemyType = enemyNoToEnemyType[v];
      var dataDict = new Dictionary<string,int>();
      dataDict["data"] = data;
      var obj = new ObjectRec(enemyType, sx, sy, x, y, dataDict);
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
        byte x = (byte)((obj.x & 0xF0) | (obj.sx & 0x0F));
        byte y = (byte)((obj.y & 0xF0) | (obj.sy & 0x0F));
        int  reversedType = enemyNoToEnemyType.FirstOrDefault(n => n.Value == obj.type).Key;
        byte data = (byte)obj.additionalData["data"];
        
        Globals.romdata[baseAddr + objCount*0 + i] = data;
        Globals.romdata[baseAddr + objCount*1 + i] = x;
        Globals.romdata[baseAddr + objCount*2 + i] = y;
        Utils.writeWord(Globals.romdata, baseAddr + objCount*3 + i*2, reversedType);
    }
    for (int i = objects.Count; i < objCount; i++)
    {
        Globals.romdata[baseAddr + objCount*0 + i] = 0xFF;
        Globals.romdata[baseAddr + objCount*1 + i] = 0xFF;
        Globals.romdata[baseAddr + objCount*2 + i] = 0xFF;
        Globals.romdata[baseAddr + objCount*3 + i*2]   = 0xFF;
        Globals.romdata[baseAddr + objCount*3 + i*2+1] = 0xFF;
    }
    return true;
  }
  
  //not real layout, simple screen line
  public  LevelLayerData getLayout(int curActiveLayout)
  {
      int width =  ConfigScript.getLevelWidth(curActiveLayout);
      int height = ConfigScript.getLevelHeight(curActiveLayout);
      int[] layer = new int[width * height];
      for (int i = 0; i < width * height; i++)
          layer[i] = i+1;
      return new LevelLayerData(width, height, layer, null, null);
  }
  
  public static Dictionary<String,int> getObjectDictionary(int listNo, int type)
  {
    return new Dictionary<String, int> { {"data", 0} };
  }
}