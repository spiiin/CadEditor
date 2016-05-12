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
//public OffsetRec getVideoOffset()                         { return new OffsetRec(0x30810, 16  , 0x1000); } //for correct view level 7-8 (with video no 2)
  public OffsetRec getVideoOffset()                         { return new OffsetRec(0x30010, 16  , 0x1000); }
  public OffsetRec getVideoObjOffset()                      { return new OffsetRec(0x20010, 16  , 0x1000); } 
  public OffsetRec getBigBlocksOffset()                     { return new OffsetRec(0 , 4   , 1); }
  public OffsetRec getBlocksOffset()                        { return new OffsetRec(0 , 4   , 1); }
  
  public int getLevelsCount()                               { return 9; }
  
  public OffsetRec[] getScreensOffsetsForLevels() {
    var ans = new OffsetRec[] {
      new OffsetRec(0x16149 - 96  , 1  , 17*96, 96, 17),
      new OffsetRec(0x18149 - 24  , 1  , 24*64, 24, 64),
      new OffsetRec(0x16CB7 - 168 , 1  , 168*11, 168  , 11),
      new OffsetRec(0x14012 - 168 , 1  , 168*17, 168  , 17),
      new OffsetRec(0x14C72 - 62  , 1  , 62*33 , 62   , 33),
      new OffsetRec(0x18C21 - 64  , 1  , 64*33 , 64   , 33),
      new OffsetRec(0x12012 - 64  , 1  , 64*33 , 64   , 33),
      new OffsetRec(0x12DF6 - 33  , 1  , 33*55 , 33   , 55),
      new OffsetRec(0x17536 - 64  , 1  , 64*33 , 64   , 33)
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
  
  public GroupRec[] getGroups()
  {
    return new GroupRec[] { 
      new GroupRec("Jungle by Day"         , 14,0,0,0, 0x01),
      new GroupRec("Great Tree"            , 11,2,2,0, 0x01),
      new GroupRec("Dawn Patrol"           , 14,0,0,0, 0x01),
      new GroupRec("River"                 , 14,1,1,0, 0x01),
      new GroupRec("Baloo and River"       , 14,1,1,0, 0x01),
      new GroupRec("Tree Village"          , 11,2,2,0, 0x01),
      new GroupRec("Ruins"                 , 2 ,3,3,9 ,0x01),
      new GroupRec("Falling Ruins"         , 2 ,3,3,9 ,0x01),
      new GroupRec("Jungle by Night"       , 14,0,0,0, 0x01),
    };
  }
  
  public IList<LevelRec> levelRecsJB = new List<LevelRec>() 
  {
    new LevelRec(0x167D5, 48, 1, 1, 0x0, "Jungle By Day", 0),
    new LevelRec(0x18815, 45, 1, 1, 0x0, "Great Tree", 1),
    new LevelRec(0x173F6, 67, 1, 1, 0x0, "Dawn Patrol", 2),
    new LevelRec(0x14B7A, 89, 1, 1, 0x0, "River", 3),
    new LevelRec(0x1550E, 67, 1, 1, 0x0, "Baloo and River", 4),
    new LevelRec(0x194D3, 51, 1, 1, 0x0, "Tree Village", 5),
    new LevelRec(0x128D6, 60, 1, 1, 0x0, "Ruins", 6),
    new LevelRec(0x13642, 111, 1, 1, 0x0, "Falling Ruins", 7),
    new LevelRec(0x17E04, 65, 1, 1, 0x0, "Jungle By Night", 8),
    //new LevelRec(0x10912, 74, 1, 1, 0x0, "Wastelands", 9),
  };
  
  //addrs saved in ram at 77-79-7E-81
  public List<ObjectList> getObjectsJungleBook(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    int objAddr  = lr.objectsBeginAddr;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
        byte x    = Globals.romdata[objAddr - objCount*2 + i];
        byte y    = Globals.romdata[objAddr - objCount*1 + i];
        byte v    = Globals.romdata[objAddr + objCount*0 + i];
        byte data = Globals.romdata[objAddr + objCount*1 + i];
        int realx = x* 32 + 16;
        int realy = y* 32 + 16;
        var dataDict = new Dictionary<string,int>();
        dataDict["data"] = data;
        var obj = new ObjectRec(v, 0, 0, realx, realy, dataDict);
        objects.Add(obj);
    }
    return new List<ObjectList> { new ObjectList { objects = objects, name = "Objects" } };
  }

  public bool setObjectsJungleBook(int levelNo, List<ObjectList> objLists)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    int objAddr  = lr.objectsBeginAddr;
    var objects = objLists[0].objects;
    for (int i = 0; i < objects.Count; i++)
    {
        var obj = objects[i];
        byte x = (byte)((obj.x - 16) /32);
        byte y = (byte)((obj.y - 16) /32);
        Globals.romdata[objAddr + objCount*0 + i] = (byte)obj.type;
        Globals.romdata[objAddr + objCount*1 + i] = (byte)obj.additionalData["data"];
        Globals.romdata[objAddr - objCount*2 + i] = x;
        Globals.romdata[objAddr - objCount*1 + i] = y;
    }
    for (int i = objects.Count; i < objCount; i++)
    {
        Globals.romdata[objAddr + objCount*0 + i] = 0xFF;
        Globals.romdata[objAddr + objCount*1 + i] = 0xFF;
        Globals.romdata[objAddr - objCount*2 + i] = 0xFF;
        Globals.romdata[objAddr - objCount*1 + i] = 0xFF;
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
  
  //---------------------------------------------------------------------------
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
    public int hiCount;
    public int loAddr;
    public int loCount;
  }
  
  static BlockRec[] BlocksAddrs = new BlockRec[]{
    new BlockRec{ hiAddr = 0x1D984, hiCount = 128, loAddr = 0x16859, loCount = 128 }, //1,3,9
    new BlockRec{ hiAddr = 0x1D984, hiCount = 128, loAddr = 0x15654, loCount = 128 }, //4,5
    new BlockRec{ hiAddr = 0x1D984, hiCount = 128, loAddr = 0x1889F, loCount = 128 }, //2,6
    new BlockRec{ hiAddr = 0x1D984, hiCount = 128, loAddr = 0x12970, loCount = 110 }, //7,8
  };
  
  static BigBlockRec[] BigBlocksAddrs = new BigBlockRec[] { 
    new BigBlockRec { hiAddr = 0x1DC04, hiCount = 101, loAddr = 0x16A59 + 128, loCount = 119 }, //1,3,9
    new BigBlockRec { hiAddr = 0x1DC04, hiCount = 101, loAddr = 0x15854 + 128, loCount = 119 }, //4,5
    new BigBlockRec { hiAddr = 0x1DC04, hiCount = 101, loAddr = 0x19779 + 128, loCount = 120 }, //2,6
    new BigBlockRec { hiAddr = 0x1DC04, hiCount = 101, loAddr = 0x12B28 + 128, loCount = 83 },  //7,8
  };
  
  //----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  public static ObjRec[] getBlocksJB(int blockIndex)
  {
    var part1 = Utils.readBlocksFromAlignedArrays(Globals.romdata, BlocksAddrs[blockIndex].hiAddr, BlocksAddrs[blockIndex].hiCount);
    var part2 = Utils.readBlocksFromAlignedArrays(Globals.romdata, BlocksAddrs[blockIndex].loAddr, BlocksAddrs[blockIndex].loCount);
    var total = new ObjRec[256];
    Array.Copy(part1, total, part1.Length);
    Array.Copy(part2, 0, total, BlocksAddrs[blockIndex].loCount, part2.Length); //copy to index 110, no 128!!! bug of game developers?
    return total;
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
    int loCount = BlocksAddrs[blockIndex].loCount;
    var secondPart = new ObjRec[loCount];
    Array.Copy(objects, loCount, secondPart, 0, loCount);
    Utils.writeBlocksToAlignedArrays(objects   , Globals.romdata, BlocksAddrs[blockIndex].hiAddr, BlocksAddrs[blockIndex].hiCount);
    Utils.writeBlocksToAlignedArrays(secondPart, Globals.romdata, BlocksAddrs[blockIndex].loAddr, loCount);
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