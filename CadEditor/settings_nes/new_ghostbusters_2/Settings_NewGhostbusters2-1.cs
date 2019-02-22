using CadEditor;
using System.Collections.Generic;
//css_include shared_settings/BlockUtils.cs;
//css_include shared_settings/SharedUtils.cs;

public class Data
{ 
  public OffsetRec getVideoOffset()     { return new OffsetRec(0, 2  , 0x1000); }
  public OffsetRec getPalOffset()       { return new OffsetRec(0, 2  , 16);     }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0, 1  , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x77C1 - 16*15*8  , 13 , 16*15, 16, 15);   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk(new[] {"chr1.bin", "chr1-2.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x6B51, 1  , 0x1000);  }
  public int getBlocksCount()           { return 240; }
  public int getBigBlocksCount()        { return 240; }
  
  public GetObjectsFunc getObjectsFunc()   { return getObjects;  }
  public SetObjectsFunc setObjectsFunc()   { return setObjects;  }
  
  public GetBlocksFunc        getBlocksFunc() { return BlockUtils.getBlocksFromAlignedArrays;}
  public SetBlocksFunc        setBlocksFunc() { return BlockUtils.setBlocksToAlignedArrays;}
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal1.bin", "pal1-2.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
  
  public GetLayoutFunc getLayoutFunc()     { return getLayout;   } 
  LevelLayerData getLayout(int levelNo)
  {
    int[] layer = new int[20] { 
      0x00, 0x00, 0x02, 0x00, 0x00,
      0x00, 0x00, 0x0C, 0x00, 0x00,
      0x00, 0x03, 0x04, 0x05, 0x06,
      0x07, 0x08, 0x09, 0x0A, 0x0B,
    };
    return new LevelLayerData(5, 4, layer);
  }
  
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x69D8, 4, 5, 4, 0x1),
    new LevelRec(0x69FC, 1, 5, 4, 0x2),
    new LevelRec(0x6A0D, 4, 5, 4, 0x3),
    new LevelRec(0x6A31, 1, 5, 4, 0x4),
    new LevelRec(0x6A40, 1, 5, 4, 0x5),
    new LevelRec(0x6A4F, 2, 5, 4, 0x6),
    new LevelRec(0x6A65, 8, 5, 4, 0x7),
    new LevelRec(0x6A9D, 2, 5, 4, 0x8),
    new LevelRec(0x6AB1, 2, 5, 4, 0x9),
    //new LevelRec(0x6AC5, 0, 5, 4, 0x0),
    //new LevelRec(0x6AD1, 5, 5, 4, 0xA),
  };
  
  //----------------------------------------------------------------------------
  
  //Read only standart enemies (5 bytes per enemy)
  //other types:
  //0x0D - Bags (+1 byte)
  //0x1B - Fly chairs (+1 byte)
  //0xFF - var length (checkpoints, pointers to next rooms)
  //0x12 - arrow between rooms (7 bytes per arrow)
  public List<ObjectList> getObjects(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    int baseAddr = lr.objectsBeginAddr;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
      int v    = Globals.romdata[baseAddr + 5*i + 0];
      int x    = Globals.romdata[baseAddr + 5*i + 1] * 2;
      int sx   = Globals.romdata[baseAddr + 5*i + 2];
      int y    = Globals.romdata[baseAddr + 5*i + 3] * 2;
      int sy   = Globals.romdata[baseAddr + 5*i + 4];
      var obj = new ObjectRec(v, sx, sy, x, y);
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
        Globals.romdata[baseAddr + 5*i + 0] = (byte)obj.type;
        Globals.romdata[baseAddr + 5*i + 1] = (byte)(obj.x/2);
        Globals.romdata[baseAddr + 5*i + 2] = (byte)obj.sx;
        Globals.romdata[baseAddr + 5*i + 3] = (byte)(obj.y/2);
        Globals.romdata[baseAddr + 5*i + 4] = (byte)obj.sy;
    }
    for (int i = objects.Count; i < objCount; i++)
    {
        Globals.romdata[baseAddr + 5*i + 0] = 0xff;
        Globals.romdata[baseAddr + 5*i + 1] = 0xff;
        Globals.romdata[baseAddr + 5*i + 2] = 0xff;
        Globals.romdata[baseAddr + 5*i + 3] = 0xff;
        Globals.romdata[baseAddr + 5*i + 4] = 0xff;
    }
    return true;
  }
}