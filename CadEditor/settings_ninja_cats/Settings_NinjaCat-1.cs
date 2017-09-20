using CadEditor;
using System;
using System.Collections.Generic;
//css_include settings_ninja_cats/NinjaCatUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(88, 22 , 8*5);   }
  public int getScreenWidth()          { return 5; }
  public int getScreenHeight()         { return 8; }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  //public OffsetRec getVideoOffset()   { return new OffsetRec(0x2D010, 1, 0x1000); }
  //public OffsetRec getPalOffset()     { return new OffsetRec(0x2B10,  1, 16   ); }
  public int getPalBytesAddr()          { return 0x4B2E; }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x4052 , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x5376  , 8   , 0x440); }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()          { return NinjaCatUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()             { return NinjaCatUtils.getVideoChunk("chr1.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()             { return null; }
  public GetBlocksFunc        getBlocksFunc()                 { return NinjaCatUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()                 { return NinjaCatUtils.setBlocks;}
  public GetBigBlocksFunc     getBigBlocksFunc()              { return NinjaCatUtils.getBigBlocks;}
  public SetBigBlocksFunc     setBigBlocksFunc()              { return NinjaCatUtils.setBigBlocks;}
  public GetPalFunc           getPalFunc()                    { return NinjaCatUtils.readPalFromBin("pal1.bin"); }
  public SetPalFunc           setPalFunc()                    { return null;}
  public GetObjectsFunc getObjectsFunc()                      { return NinjaCatUtils.getObjects;  }
  public SetObjectsFunc setObjectsFunc()                      { return NinjaCatUtils.setObjects;  }
  
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
}