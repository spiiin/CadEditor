using CadEditor;
using System.Collections.Generic;
//css_include Settings_CapcomBase.cs;

//Changes for levels 7,8,9:
// screensOffset
// palOffset

public class Data : CapcomBase
{ 
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginChrView.dll",
      "PluginEditLayout.dll"
    };
  }
  public OffsetRec getPalOffset()       { return new OffsetRec(0xF291 - 16  , 32  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x20010 , 32  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0, 16   , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0 , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xC010  , 8   , 0x440); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x210 + 32, 256 , 16*15);   }
  public override int getScreenWidth()     { return 16; }
  public override int getScreenHeight()    { return 15; }
  public IList<LevelRec> getLevelRecs()    { return levelRecsCad2; }
  public int getScrollsOffsetFromLayout()  { return 538; } //offset scrolls array from layout array
  public GetObjectsFunc getObjectsFunc()   { return getObjectsCad2; }
  public SetObjectsFunc setObjectsFunc()   { return setObjectsCad2; }
  public override GetBlocksFunc        getBlocksFunc()        { return getBlocksDt2;}
  public override SetBlocksFunc        setBlocksFunc()        { return setBlocksDt2;}
  public override GetVideoPageAddrFunc getVideoPageAddrFunc() { return getChrAddress; }
  public bool isBuildScreenFromSmallBlocks() { return true; }
  public GetLayoutFunc getLayoutFunc()       { return getLayout;   }
  
  const int LEVEL_REC_COUNT = 15;
  
  LevelLayerData getLayout(int levelNo)
  {
    if (levelNo < LEVEL_REC_COUNT)
      return Utils.getLayoutLinear(levelNo);
    else
      return getLayoutForPrizes(levelNo - LEVEL_REC_COUNT);
  }
  
  LevelLayerData getLayoutForPrizes(int levelNo)
  {
    var levelLayers = new byte[][]
    {
        new byte[] { 0x6A, 0x6B, 0x69, 0x68},                               //7-1
        new byte[] { 0x5F, 0x60, 0x5F, 0x60, 0x5F, 0x5F, 0x5F, 0x61},       //7-2
        new byte[] { 0x62, 0x63, 0x63, 0x62, 0x5F, 0x60, 0x60, 0x64},       //7-3
        new byte[] { 0x62, 0x63, 0x60, 0x5F, 0x60, 0x61, 0x65, 0x66},       //7-4
        
        new byte[] { 0x7B, 0x6D, 0x7C, 0x78, 0x79, 0x7A, 0x77},            //8-1
        new byte[] { 0x71, 0x72, 0x73, 0x72, 0x74, 0x75, 0x76},            //8-2
        new byte[] { 0x6C, 0x6D, 0x6E, 0x6F, 0x6D, 0x70},                  //8-3
    };
    return new LevelLayerData(levelLayers[levelNo].Length, 1, levelLayers[levelNo]);
  }
  
  public IList<LevelRec> levelRecsCad2 = new List<LevelRec>() 
  {
    //level 7
    new LevelRec(0xE607, 8 , 8, 8, 0xDFFA, "7-1"),
    new LevelRec(0xE620, 7 , 8, 8, 0xDFFA, "7-2"),
    new LevelRec(0xE636, 7 , 8, 8, 0xDFFA, "7-3"),
    new LevelRec(0xE64C, 11, 8, 8, 0xDFFA, "7-4"),
    new LevelRec(0xE66E, 1 , 8, 8, 0xDFFA, "7-5 (boss)"),
    
    //level 8
    new LevelRec(0xE672, 9 , 8, 8, 0xE03A, "8-1"),
    new LevelRec(0xE68E, 23, 8, 8, 0xE03A, "8-2"),
    //bonus
    new LevelRec(0xE6B0, 9 , 8, 8, 0xE03A, "8-3"),
    new LevelRec(0xE6CC, 1 , 8, 8, 0xE03A, "8-4 (boss)"),
    
    //level 9
    new LevelRec(0xE6D0, 10, 8, 8, 0xE07A, "9-1"),
    new LevelRec(0xE6EF, 7 , 8, 8, 0xE07A, "9-2"),
    //bonus
    new LevelRec(0xE705, 12, 8, 8, 0xE07A, "9-3"),
    new LevelRec(0xE72A, 5 , 8, 8, 0xE07A, "9-4"),
    new LevelRec(0xE73A, 4 , 8, 8, 0xE07A, "9-5"),
    new LevelRec(0xE747, 1 , 8, 8, 0xE07A, "9-6 (boss)"),
    
    //prizes
    new LevelRec(0xECC8, 40, 8, 8, 0xDFFA, "7-1 stars"),
    new LevelRec(0xED19, 20, 8, 8, 0xDFFA, "7-2 stars"),
    new LevelRec(0xED42, 27, 8, 8, 0xDFFA, "7-3 stars"),
    new LevelRec(0xED79, 31, 8, 8, 0xDFFA, "7-4 stars"),
    
    new LevelRec(0xEDB8, 57, 8, 8, 0xE03A, "8-1 stars"),
    new LevelRec(0xEE2B, 23, 8, 8, 0xE03A, "8-2 stars"),
    new LevelRec(0xEE5A, 32, 8, 8, 0xE03A, "8-3 stars"),
  };
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public List<ObjectList> getPrizesCad2(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int addr = lr.objectsBeginAddr;
    int objCount = lr.objCount;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
      int xx = Globals.romdata[addr + i * 2 + 0];
      int yy = Globals.romdata[addr + i * 2 + 1];
      int sx = (xx >> 4);
      int v =  (yy >> 4);
      int sy = 0;
      int x = (xx & 0x0F) * 32;
      int y = (yy & 0x0F) * 32;
      var obj = new ObjectRec(v, sx, sy, x, y);
      objects.Add(obj);
    }
    return new List<ObjectList> { new ObjectList { objects = objects, name = "Items" } };
  }
  
  public bool setPrizesCad2(int levelNo, List<ObjectList> objLists)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int addrBase = lr.objectsBeginAddr;
    int objCount = lr.objCount;
    var objects = objLists[0].objects;
    for (int i = 0; i < objects.Count; i++)
    {
      var obj = objects[i];
      Globals.romdata[addrBase + i * 2 + 0] = (byte)((obj.x / 32) | (obj.sx << 4));
      Globals.romdata[addrBase + i * 2 + 1] = (byte)((obj.y / 32) | (obj.type << 4));
    }
    for (int i = objects.Count; i < objCount; i++)
    {
      Globals.romdata[addrBase + i * 2 + 0] = 0xFF;
      Globals.romdata[addrBase + i * 2 + 1] = 0xFF;
    }
    return true;
  }
  
  
  public List<ObjectList> getObjectsCad2(int levelNo)
  {
    //hack for prizes
    if (levelNo >= LEVEL_REC_COUNT)
      return getPrizesCad2(levelNo);
      
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int addr = lr.objectsBeginAddr;
    int objCount = lr.objCount;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
      int v  = Globals.romdata[addr + i * 3 + 2];
      int xx = Globals.romdata[addr + i * 3 + 0];
      int yy = Globals.romdata[addr + i * 3 + 1];
      int sx = (xx >> 4);
      int sy = (yy >> 4);
      int x = (xx & 0x0F) * 32;
      int y = (yy & 0x0F) * 32;
      var obj = new ObjectRec(v, sx, sy, x, y);
      objects.Add(obj);
    }
    return new List<ObjectList> { new ObjectList { objects = objects, name = "Objects" } };
  }

  public bool setObjectsCad2(int levelNo, List<ObjectList> objLists)
  {
    //hack for prizes
    if (levelNo >= LEVEL_REC_COUNT)
      return setPrizesCad2(levelNo, objLists);
     
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int addrBase = lr.objectsBeginAddr;
    int objCount = lr.objCount;
    var objects = objLists[0].objects;
    for (int i = 0; i < objects.Count; i++)
    {
      var obj = objects[i];
      Globals.romdata[addrBase + i * 3 + 2] = (byte)obj.type;
      Globals.romdata[addrBase + i * 3 + 0] = (byte)((obj.x / 32) | (obj.sx << 4));
      Globals.romdata[addrBase + i * 3 + 1] = (byte)((obj.y / 32) | (obj.sy << 4));
    }
    for (int i = objects.Count; i < objCount; i++)
    {
      Globals.romdata[addrBase + i * 3 + 2] = 0xFF;
      Globals.romdata[addrBase + i * 3 + 0] = 0xFF;
      Globals.romdata[addrBase + i * 3 + 1] = 0xFF;
    }
    return true;
  }
  
  public static int getChrAddress(int id)
  {
      if (id >= 0x90)
        return ConfigScript.videoOffset.beginAddr + ConfigScript.videoOffset.recSize * (id - 0x90);
      return -1;
  }
  
  //-----------------------------------------------------------------------------------------------
  public class Dt2ObjRec : ObjRec
  {
    public Dt2ObjRec(ObjRec b, int _index)
        :base(b)
    {
        index = _index;
    }
    
    public override int getType()
    {
        return index < 0xA0 ? 0 : 5;
    }
    
    int index;
  }
  
  public ObjRec[] getBlocksDt2(int blockIndex)
  {
    ObjRec[] blocks = Utils.readBlocksFromAlignedArrays(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), getBlocksCount());
    //decode typeColor
    int palInfoCount = getBlocksCount()/4;
    var palInfo = new byte[palInfoCount];
    for (int i = 0; i < palInfoCount; i++)
    {
        palInfo[i] = (byte)blocks[i].typeColor;
    }
    for (int i = 0; i < blocks.Length; i++)
    {
        var palInfoByte = palInfo[i/4];
        int parByteNo = i % 4;
        blocks[i].typeColor = (byte)((palInfoByte >> parByteNo*2) & 3);
    }
    //
    //rebuild blocks to dt2 blocks
    for (int i = 0; i < blocks.Length; i++)
    {
        blocks[i] = new Dt2ObjRec(blocks[i], i);
    }
    //
    return blocks;
  }
  
  public void setBlocksDt2(int blockIndex, ObjRec[] objects)
  {
    int addr = ConfigScript.getTilesAddr(blockIndex);
    int count = getBlocksCount();
    for (int i = 0; i < count; i++)
    {
        var obj = objects[i];
        Globals.romdata[addr + i] = (byte)obj.c1;
        Globals.romdata[addr + count * 1 + i] = (byte)obj.c2;
        Globals.romdata[addr + count * 2 + i] = (byte)obj.c3;
        Globals.romdata[addr + count * 3 + i] = (byte)obj.c4;
    }
    
    int palInfoCount = getBlocksCount()/4;
    for (int i = 0; i < palInfoCount; i++)
    {
        var palInfoByte = 
          (objects[i*4+0].typeColor<<0) | 
          (objects[i*4+1].typeColor<<2) |
          (objects[i*4+2].typeColor<<4) | 
          (objects[i*4+3].typeColor<<6);
          
        Globals.romdata[addr + count * 4 + i] = (byte)palInfoByte;
    }
  }
}