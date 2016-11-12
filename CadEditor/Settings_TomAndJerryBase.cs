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
  
  public GetObjectsFunc getObjectsFunc()   { return getObjects;  }
  public SetObjectsFunc setObjectsFunc()   { return setObjects;  }
  public GetLayoutFunc getLayoutFunc()     { return getLayout;   } 
  public GetObjectDictionaryFunc getObjectDictionaryFunc() { return getObjectDictionary; }
  
  public ObjRec[] getBlocks(int blockIndex)
  {
    return Utils.readBlocksFromAlignedArrays(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), getBlocksCount());
  }
  
  public void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
    Utils.writeBlocksToAlignedArrays(blocksData, Globals.romdata, ConfigScript.getTilesAddr(blockIndex), getBlocksCount());
  }
  
  public List<ObjectList> getObjects(int levelNo)
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
  
  LevelLayerData getLayout(int levelNo)
  {
    byte[] layer = new byte[1];
    layer[0] = 1;
    return new LevelLayerData(1, 1, layer);
  }
  
  public Dictionary<String,int> getObjectDictionary(int listNo, int type)
  {
    return new Dictionary<String, int> { {"data", 0} };
  }
}