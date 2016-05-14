using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginChrView.dll",
    };
  }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0xD833  , 1  , 8*96);   }
  public OffsetRec getPalOffset()       { return new OffsetRec(0x0     , 1  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x38010 , 1  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x20010 , 1  , 0x1000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xDE7B  , 1  , 0x4000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0xDB33  , 1  , 0x4000); }
  public int getScreenWidth()             { return 8; }
  public int getScreenHeight()            { return 96; }
  public bool getScreenVertical()         { return true; }
  //public string getBlocksFilename()       { return "flintstones2_1.png"; }
  public int    getPictureBlocksWidth()   { return 16; }
  
  public int getBlocksCount()    { return 163; }
  public int getBigBlocksCount() { return 105; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  public IList<LevelRec> getLevelRecs() { return levelRecs; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public virtual GetBlocksFunc getBlocksFunc()       { return getBlocks;}
  public virtual SetBlocksFunc setBlocksFunc()       { return setBlocks;}
  public virtual GetBigBlocksFunc getBigBlocksFunc() { return getBigBlocks;}
  public virtual SetBigBlocksFunc setBigBlocksFunc() { return setBigBlocks;}
  public GetObjectsFunc getObjectsFunc()   { return getObjects;  }
  public SetObjectsFunc setObjectsFunc()   { return setObjects;  }
  public GetLayoutFunc getLayoutFunc()     { return getLayout;   } 
  
  public ConvertScreenTileFunc getConvertScreenTileFunc()      { return (v => v >> 3 | (v & 0x07) << 5);}
  public ConvertScreenTileFunc getBackConvertScreenTileFunc()  { return (v => v >> 5 | (v & 0x1F) << 3);}
   
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x0, 23, 1, 1, 0x0),
  };
  
  LevelLayerData getLayout(int levelNo)
  {
    byte[] layer = new byte[1];
    layer[0] = 1;
    return new LevelLayerData(1, 1, layer);
  }
  
  //addrs saved in ram at DD-DF-E1-E3
  public List<ObjectList> getObjects(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
      byte x    = Globals.romdata[0x14D84 + i];
      byte y    = Globals.romdata[0x14D9C + i];
      int realx = x * 8;
      int realy = y * 8;
      byte v    = Globals.romdata[0x14DB4 + i];
      var obj = new ObjectRec(v, 0, 0, realx, realy);
      objects.Add(obj);
    }
    return new List<ObjectList> { new ObjectList { objects = objects, name = "Objects" } };
  }

  public bool setObjects(int levelNo, List<ObjectList> objLists)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    var objects = objLists[0].objects;
    for (int i = 0; i < objects.Count; i++)
    {
        var obj = objects[i];
        byte x = (byte)(obj.x /8);
        byte y = (byte)(obj.y /8);
        Globals.romdata[0x14DB4 + i] = (byte)obj.type;
        Globals.romdata[0x14D84 + i] = x;
        Globals.romdata[0x14D9C + i] = y;
    }
    for (int i = objects.Count; i < objCount; i++)
    {
        Globals.romdata[0x14DB4 + i] = 0xFF;
        Globals.romdata[0x14D84 + i] = 0xFF;
        Globals.romdata[0x14D9C + i] = 0xFF;
    }
    return true;
  }
  
  
  //------------------------------
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public byte[] getPallete(int palId)
  {
    //for test level 1-1
    var pallete = new byte[] { 
      0x0F, 0x28, 0x24, 0x18, 0x0F, 0x28, 0x21, 0x24,
      0x0F, 0x30, 0x21, 0x29, 0x0F, 0x29, 0x18, 0x24
    }; 
    return pallete;
  }
  
  public ObjRec[] getBlocks(int blockIndex)
  {
    int count = getBlocksCount();
    int addr  = getBlocksOffset().beginAddr;
    var objects = new ObjRec[count];
    for (int i = 0; i < count; i++)
    {
        byte c1, c2, c3, c4, typeColor;
        c1 = Globals.romdata[addr + i*4 + 0];
        c2 = Globals.romdata[addr + i*4 + 2];
        c3 = Globals.romdata[addr + i*4 + 1];
        c4 = Globals.romdata[addr + i*4 + 3];
        typeColor = Globals.romdata[addr + count * 4 + i];
        objects[i] = new ObjRec(c1, c2, c3, c4, typeColor);
    }
    return objects;
  }
  
  public void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
    int count = getBlocksCount();
    int addr  = getBlocksOffset().beginAddr;
    for (int i = 0; i < count; i++)
    {
        var obj = blocksData[i];
        Globals.romdata[addr + i*4 + 0] = obj.c1;
        Globals.romdata[addr + i*4 + 2] = obj.c2;
        Globals.romdata[addr + i*4 + 1] = obj.c3;
        Globals.romdata[addr + i*4 + 3] = obj.c4;
        Globals.romdata[addr + count * 4 + i] = obj.typeColor;
    }
  }
  
  private void xchg(int[] arr, int i1, int i2)
  {
      int tmp = arr[i1];
      arr[i1] = arr[i2];
      arr[i2] = tmp;
  }
  
  private void transposeBigBlocks(BigBlock[] bblocks)
  {
    for (int i = 0; i < bblocks.Length; i++)
    {
        var bb = bblocks[i];
        var newIndexes = new int[] { 
          bb.indexes[0], bb.indexes[4], bb.indexes[1], bb.indexes[5],
          bb.indexes[2], bb.indexes[6], bb.indexes[3], bb.indexes[7]
        };
        bb.indexes = newIndexes;
    }
  }
  
  private void reverseTransposeBigBlocks(BigBlock[] bblocks)
  {
    for (int i = 0; i < bblocks.Length; i++)
    {
        var bb = bblocks[i];
        var newIndexes = new int[] { 
          bb.indexes[0], bb.indexes[2], bb.indexes[4], bb.indexes[6],
          bb.indexes[1], bb.indexes[3], bb.indexes[5], bb.indexes[7]
        };
        bb.indexes = newIndexes;
    }
  }
  
  public BigBlock[] getBigBlocks(int bigTileIndex)
  {
    var data = Utils.readLinearBigBlockData(bigTileIndex, 8);
    var bblocks = Utils.unlinearizeBigBlocks(data, 2, 4);
    transposeBigBlocks(bblocks);
    return bblocks;
  }
  
  public void setBigBlocks(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
    reverseTransposeBigBlocks(bigBlockIndexes);
    var data = Utils.linearizeBigBlocks(bigBlockIndexes);
    Utils.writeLinearBigBlockData(bigTileIndex, data);
  }
}