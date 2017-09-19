using CadEditor;
using System;
using System.Drawing;
using System.Collections.Generic;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(88, 22 , 8*5);   }
  public int getScreenWidth()          { return 5; }
  public int getScreenHeight()         { return 8; }
  //public string getBlocksFilename()    { return "ninja_cats_1.png"; }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public OffsetRec getVideoOffset()   { return new OffsetRec(0x2D010, 1, 0x1000); }
  //public OffsetRec getPalOffset()     { return new OffsetRec(0x2B10,  1, 16   ); }
  public int getPalBytesAddr()          { return 0x4B2E; }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x4052 , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x5376  , 8   , 0x440); }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()         { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()            { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()            { return Utils.setVideoChunk; }
  public GetBlocksFunc        getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocks;}
  public GetBigBlocksFunc     getBigBlocksFunc()     { return getBigBlocks;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return setBigBlocks;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  
  public GetObjectsFunc getObjectsFunc()   { return getObjects;  }
  public SetObjectsFunc setObjectsFunc()   { return setObjects;  }
  
  public GetLayoutFunc getLayoutFunc()     { return getLayout;   }
  LevelLayerData getLayout(int levelNo)
  {
    var levelLayers = new int[][]
    {
        new int[] { 1,2,3,4 },
        new int[] { 5,6 },
        new int[] { 7,8,9,0xA },
        new int[] { 0xE },
        new int[] { 0xF, 0x10, 0x11, 0x12 },
        new int[] { 0xB, 0xC, 0xD },
        new int[] { 0x13, 0x14, 0x15, 0x16 },
    };
    return new LevelLayerData(levelLayers[levelNo].Length, 1, levelLayers[levelNo], null, null);
  }
  
  public IList<LevelRec> getLevelRecs() { return levelRecs; }
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0xA548, 6 , 4, 1, 0x0), //room 1
    new LevelRec(0xA55C, 4 , 2, 1, 0x0), //room 2
    new LevelRec(0xA56A, 10, 4, 1, 0x0), //room 3
    new LevelRec(0xA58A, 2 , 1, 1, 0x0), //room 4
    new LevelRec(0xA592, 9 , 4, 1, 0x0), //room 5
    new LevelRec(0xA5AF, 7 , 3, 1, 0x0), //room 6
    new LevelRec(0xA5C6, 10, 4, 1, 0x0), //room 7
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
  
  public ObjRec[] getBlocks(int tileId)
  {
      int addr = ConfigScript.getTilesAddr(tileId);
      int count = ConfigScript.getBlocksCount();
      var blocks = Utils.readBlocksLinear(Globals.romdata, addr, 2, 2, count, false);
      return blocks;
  }
  
  public void setBlocks(int tileId, ObjRec[] blocksData)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount();
    Utils.writeBlocksLinear(blocksData, Globals.romdata, addr, count, false);
  }
  
  public BigBlock[] getBigBlocks(int bigTileIndex)
  {
    var data = Utils.readLinearBigBlockData(0, bigTileIndex);
    var bb = Utils.unlinearizeBigBlocks<BigBlockWithPal>(data, 2, 2);
    for (int i = 0; i < bb.Length; i++)
    {
      int palByte = Globals.romdata[getPalBytesAddr() + i];
      bb[i].palBytes[0] = palByte >> 0 & 0x3;
      bb[i].palBytes[1] = palByte >> 2 & 0x3;
      bb[i].palBytes[2] = palByte >> 4 & 0x3;
      bb[i].palBytes[3] = palByte >> 6 & 0x3;
    }
    return bb;
  }
  
  public void setBigBlocks(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
    var data = Utils.linearizeBigBlocks(bigBlockIndexes);
    Utils.writeLinearBigBlockData(0, bigTileIndex, data);
    //save pal bytes
    for (int i = 0; i < bigBlockIndexes.Length; i++)
    {
      var bb = bigBlockIndexes[i] as BigBlockWithPal;
      int palByte = bb.palBytes[0] | bb.palBytes[1] << 2 | bb.palBytes[2]<<4 | bb.palBytes[3]<< 6;
      Globals.romdata[getPalBytesAddr() + i] = (byte)palByte;
    }
  }
  
  public byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x2a, 0x30, 0x25, 0x0f, 0x15, 0x10, 0x30,
      0x0f, 0x21, 0x26, 0x30, 0x0f, 0x21, 0x1b, 0x3c
    }; 
    return pallete;
  }
}