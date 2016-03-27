using CadEditor;
using System;
using System.Drawing;
using System.Collections.Generic;

public class Data 
{ 
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginChrView.dll"
    };
  }
  public OffsetRec getPalOffset()                           { return new OffsetRec(0x1CC19, 32  , 16);     }
  public OffsetRec getVideoOffset()                         { return new OffsetRec(0x30010, 16  , 0x1000); }
  public OffsetRec getVideoObjOffset()                      { return new OffsetRec(0x20010, 16  , 0x1000); }
  public OffsetRec getBigBlocksOffset()                     { return new OffsetRec(0 , 3   , 1); }
  public OffsetRec getBlocksOffset()                        { return new OffsetRec(0 , 3   , 1); }
  
  public int getLevelsCount()                               { return 9; }
  
  public OffsetRec[] getScreensOffsetsForLevels() {
    var ans = new OffsetRec[] {
      new OffsetRec(  90441 - 96 , 1, 17*96, 96, 17),
      new OffsetRec(0x18149 - 24 , 1, 24*64, 24, 64),
      new OffsetRec(  93367 , 1  , 168*10, 168  , 10),
      new OffsetRec(  81938 , 1  , 168*16, 168  , 16),
      new OffsetRec(  85106 , 1  , 62*32 , 62   , 32),
      new OffsetRec(0x18C21 , 1  , 64*32 , 64   , 32),
      new OffsetRec(0x12012 , 1  , 64*32 , 64   , 32),
      new OffsetRec(0x12DF6 , 1  , 33*54 , 33   , 54),
      new OffsetRec(95542   , 1  , 64*32 , 64   , 32)
    };
    return ans;  
  }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  public IList<LevelRec> getLevelRecs() { return levelRecsJB; }
  
  public SetBlocksFunc setBlocksFunc()     { return setBlocksJB;}
  public GetBlocksFunc getBlocksFunc()     { return getBlocksJB;}
  public GetBigBlocksFunc getBigBlocksFunc()  { return getBigBlocksJB;}
  public SetBigBlocksFunc setBigBlocksFunc()  { return setBigBlocksJB;}
  public GetObjectsFunc getObjectsFunc()   { return getObjectsJungleBook;  }
  public SetObjectsFunc setObjectsFunc()   { return setObjectsJungleBook;  }
  public SortObjectsFunc sortObjectsFunc() { return sortObjectsJungleBook; }
  public GetLayoutFunc getLayoutFunc()     { return getLayoutJungleBook;   }
  public GetPalFunc           getPalFunc() { return Utils.getPalleteLinear;}
  public SetPalFunc           setPalFunc() { return Utils.setPalleteLinear;}
  public GetVideoPageAddrFunc getVideoPageAddrFunc()         { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()            { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()            { return Utils.setVideoChunk; }
  public GetObjectDictionaryFunc getObjectDictionaryFunc()   { return getObjectDictionary; }
  
  public IList<LevelRec> levelRecsJB = new List<LevelRec>() 
  {
    new LevelRec(0x0, 47, 1, 1, 0x0, "", 0),
    new LevelRec(0x0, 47, 1, 1, 0x0, "", 1),
  };
  
  public List<ObjectList> getObjectsJungleBook(int levelNo)
  {
    if (levelNo == 0)
      return getObjectsJungleBook1(levelNo);
    else
      return getObjectsJungleBook2(levelNo);
  }
  
  public bool setObjectsJungleBook(int levelNo, List<ObjectList> objLists)
  {
    if (levelNo == 0)
      return setObjectsJungleBook1(levelNo, objLists);
    else
      return setObjectsJungleBook2(levelNo, objLists);
  }
  
  //copy-paste
  //addrs saved in ram at 77-79-7E-81
  public List<ObjectList> getObjectsJungleBook1(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
        byte y    = Globals.romdata[0x167A5 + i];
        byte x    = Globals.romdata[0x16775 + i];
        byte data = Globals.romdata[0x16805 + i];
        byte v    = Globals.romdata[0x167D5 + i];
        int realx = x* 32 + 16;
        int realy = y* 32 + 16;
        var dataDict = new Dictionary<string,int>();
        dataDict["data"] = data;
        var obj = new ObjectRec(v, 0, 0, realx, realy, dataDict);
        objects.Add(obj);
    }
    return new List<ObjectList> { new ObjectList { objects = objects, name = "Objects" } };
  }

  public bool setObjectsJungleBook1(int levelNo, List<ObjectList> objLists)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    var objects = objLists[0].objects;
    for (int i = 0; i < objects.Count; i++)
    {
        var obj = objects[i];
        byte x = (byte)((obj.x - 16) /32);
        byte y = (byte)((obj.y - 16) /32);
        Globals.romdata[0x167D5 + i] = (byte)obj.type;
        Globals.romdata[0x16805 + i] = (byte)obj.additionalData["data"];
        Globals.romdata[0x16775 + i] = x;
        Globals.romdata[0x167A5 + i] = y;
    }
    for (int i = objects.Count; i < objCount; i++)
    {
        Globals.romdata[0x167D5 + i] = 0xFF;
        Globals.romdata[0x16805 + i] = 0xFF;
        Globals.romdata[0x16775 + i] = 0xFF;
        Globals.romdata[0x167A5 + i] = 0xFF;
    }
    return true;
  }
  
  //copy-paste
   //addrs saved in ram at 77-79-7E-81
  public List<ObjectList> getObjectsJungleBook2(int levelNo)
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
    return new List<ObjectList> { new ObjectList { objects = objects, name = "Objects" } };
  }

  public bool setObjectsJungleBook2(int levelNo, List<ObjectList> objLists)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    var objects = objLists[0].objects;
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
  
  public void sortObjectsJungleBook(int levelNo, int listNo, List<ObjectRec> objects)
  {
    objects.Sort((o1, o2) => { return o1.x > o2.x ? 1 : o1.x < o2.x ? -1 : o1.y < o2.y ? -1 : o1.y > o2.y ? 1 : 0; });
  }
  
  LevelLayerData getLayoutJungleBook(int levelNo)
  {
    byte[] layer = new byte[1];
    layer[0] = 1;
    return new LevelLayerData(1, 1, layer);
  }
  
  public Dictionary<String,int> getObjectDictionary(int listNo, int type)
  {
    return new Dictionary<String, int> { {"data", 0} };
  }
  
  //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  struct BigBlockRec
  {
    public int hiAddr;
    public int hiCount;
    public int loAddr;
    public int loCount;
  }
  
  struct BlockRec
  {
    public int hiAddr;
    public int loAddr;
  }
  
  static BlockRec[] BlocksAddrs = new BlockRec[]{
    new BlockRec{ hiAddr = 0x1D984, loAddr = 0x16859 },
    new BlockRec{ hiAddr = 0x1D984, loAddr = 0x1889F },
    //new BlockRec{ hiAddr = 0x1D984, loAddr = 0x15654 },
    new BlockRec{ hiAddr = 0x1D984, loAddr = 0x12970 },
  };
  
  static BigBlockRec[] BigBlocksAddrs = new BigBlockRec[] { 
    new BigBlockRec { hiAddr = 0x1DC04, hiCount = 101, loAddr = 0x16A59 + 128, loCount = 119 }, //1,3,4,5,6,9
    new BigBlockRec { hiAddr = 0x1DC04, hiCount = 101, loAddr = 0x19779 + 128, loCount = 120 }, //2,6
    new BigBlockRec { hiAddr = 0x1DC04, hiCount = 101, loAddr = 0x12B28 + 128, loCount = 83 },  //7,8
  };
  
  //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  public static ObjRec[] getBlocksJB(int blockIndex)
  {
    var part1 = Utils.readBlocksFromAlignedArrays(Globals.romdata, BlocksAddrs[blockIndex].hiAddr, 0x80);
    var part2 = Utils.readBlocksFromAlignedArrays(Globals.romdata, BlocksAddrs[blockIndex].loAddr, 0x80);
    return Utils.mergeArrays(part1, part2);
  }
  
  public BigBlock[] getBigBlocksJB(int bigTileIndex)
  {
    var bigBlockRec = BigBlocksAddrs[bigTileIndex];
    byte[] ans = new byte[256 * 4];
    int bigBlocksAddr1 = bigBlockRec.hiAddr;
    int bigBlocksCount1 = bigBlockRec.hiCount;
    int bigBlocksAddr2 = bigBlockRec.loAddr;
    int bigBlocksCount2 = bigBlockRec.loCount;
    var bb1 = Utils.readDataFromAlignedArrays(Globals.romdata, bigBlocksAddr1, bigBlocksCount1);
    var bb2 = Utils.readDataFromAlignedArrays(Globals.romdata, bigBlocksAddr2, bigBlocksCount2);
    bb1.CopyTo(ans, 0);
    bb2.CopyTo(ans, 128*4);
    return Utils.unlinearizeBigBlocks(ans, 2, 2);
  }
  
  public void setBlocksJB(int blockIndex, ObjRec[] objects)
  {
    var secondPart = new ObjRec[0x80];
    Array.Copy(objects, 0x80, secondPart, 0, 0x80);
    Utils.writeBlocksToAlignedArrays(objects   , Globals.romdata, BlocksAddrs[blockIndex].hiAddr, 0x80);
    Utils.writeBlocksToAlignedArrays(secondPart, Globals.romdata, BlocksAddrs[blockIndex].loAddr, 0x80);
  }
  
  public void setBigBlocksJB(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
    var data = Utils.linearizeBigBlocks(bigBlockIndexes);
    var bigBlockRec = BigBlocksAddrs[bigTileIndex];
    var secondPart  = new byte[bigBlockRec.loCount*4];
    Array.Copy(data, 128*4, secondPart, 0, bigBlockRec.loCount*4);
    Utils.writeDataToAlignedArrays(data      , Globals.romdata, bigBlockRec.hiAddr, bigBlockRec.hiCount);
    Utils.writeDataToAlignedArrays(secondPart, Globals.romdata, bigBlockRec.loAddr, bigBlockRec.loCount);
  }
}