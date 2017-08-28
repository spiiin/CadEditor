using CadEditor;
using System.Collections.Generic;
using System;

public class TomAndJerryBase
{
  public virtual int getBigBlocksCount() { return 256; }
  public virtual int getBlocksCount()    { return 256; }
  public virtual int getScreenWidth()    { return 8; }
  public virtual int getScreenHeight()   { return 8; }
  
  public virtual GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public virtual GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public virtual SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public virtual GetBigBlocksFunc     getBigBlocksFunc()     { return Utils.getBigBlocksCapcomDefault;}
  public virtual SetBigBlocksFunc     setBigBlocksFunc()     { return Utils.setBigBlocksCapcomDefault;}
  public virtual GetBlocksFunc        getBlocksFunc()        { return getBlocks;}
  public virtual SetBlocksFunc        setBlocksFunc()        { return setBlocks;}
  public virtual GetPalFunc           getPalFunc()           { return Utils.getPalleteLinear;}
  public virtual SetPalFunc           setPalFunc()           { return Utils.setPalleteLinear;}
  
  public virtual GetObjectsFunc getObjectsFunc()   { return getObjects;  }
  public virtual SetObjectsFunc setObjectsFunc()   { return setObjects;  }
  public virtual GetLayoutFunc getLayoutFunc()     { return getLayout;   } 
  public virtual GetObjectDictionaryFunc getObjectDictionaryFunc() { return getObjectDictionary; }
  
  public virtual int getCheeseAddr() { return -1; }
  
  public ObjRec[] getBlocks(int blockIndex)
  {
    return Utils.readBlocksFromAlignedArrays(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), getBlocksCount());
  }
  
  public void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
    Utils.writeBlocksToAlignedArrays(blocksData, Globals.romdata, ConfigScript.getTilesAddr(blockIndex), getBlocksCount());
  }
  
  //-------------------------------------------------------------------------------
  public List<ObjectList> getObjects(int levelNo)
  {
      var enemies = getEnemies(levelNo);
      var chesses = getCheese(levelNo);
      return new List<ObjectList> { 
        new ObjectList { objects = enemies, name = "Enemies" },
        new ObjectList { objects = chesses, name = "Items" },
      };
  }
  
  public bool setObjects(int levelNo, List<ObjectList> objLists)
  {
      bool enemiesSaved = setEnemies(levelNo, objLists);
      bool cheeseSaved  = setCheese (levelNo, objLists);
      return enemiesSaved && cheeseSaved;
  }
  
  //-------------------------------------------------------------------------------
  public List<ObjectRec> getEnemies(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    var objects = new List<ObjectRec>();
    int baseAddr = lr.objectsBeginAddr;
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

  public bool setEnemies(int levelNo, List<ObjectList> objLists)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    int baseAddr = lr.objectsBeginAddr;
    var objects = objLists[0].objects;
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
  }
  
  //-------------------------------------------------------------------------------
  public List<ObjectRec> getCheese(int levelNo)
  {
    var objects = new List<ObjectRec>();
    int baseAddr = getCheeseAddr();
    if (baseAddr == -1)
    {
        return objects;
    }
    
    int curAddr = baseAddr;
    byte objType = 0;
    while(objects.Count < 255)
    {
        byte b = Globals.romdata[curAddr];
        if (b == 0xFF)
        {
            break;
        }
        if (b == 0xFE)
        {
            curAddr++;
            objType = Globals.romdata[curAddr++];
        }
        byte x    = Globals.romdata[curAddr++];
        byte y    = Globals.romdata[curAddr++];
        var obj = new ObjectRec(objType, 0, 0, x*32, y*32);
        objects.Add(obj);
    }
    return objects;
  }

  public bool setCheese(int levelNo, List<ObjectList> objLists)
  {
    int baseAddr = getCheeseAddr();
    if (baseAddr == -1)
    {
        return true;
    }
    
    var objects = objLists[1].objects;
    int curAddr = baseAddr;
    
    var obj0 = objects[0];
    Globals.romdata[curAddr++] = 0xFE;
    Globals.romdata[curAddr++] = (byte)obj0.type;
    Globals.romdata[curAddr++] = (byte)(obj0.x/32);
    Globals.romdata[curAddr++] = (byte)(obj0.y/32);
    byte prevObjType = (byte)obj0.type;
    
    for (int i = 1; i < objects.Count; i++)
    {
      var obj = objects[i];
      if (obj.type != prevObjType)
      {
          Globals.romdata[curAddr++] = 0xFE;
          Globals.romdata[curAddr++] = (byte)obj.type;
      }
      Globals.romdata[curAddr++] = (byte)(obj.x/32);
      Globals.romdata[curAddr++] = (byte)(obj.y/32);
      prevObjType = (byte)obj.type;
    }
    
    Globals.romdata[curAddr++] = 0xFF;
    return true;
  }
  
  LevelLayerData getLayout(int levelNo)
  {
    var layer = new int[1];
    layer[0] = 1;
    return new LevelLayerData(1, 1, layer);
  }
  
  public Dictionary<String,int> getObjectDictionary(int listNo, int type)
  {
    if (listNo == 0) //enemies
    {
        return new Dictionary<String, int> { {"data", 0} };
    }
    else //items
    {
        return null;
    }
  }
}