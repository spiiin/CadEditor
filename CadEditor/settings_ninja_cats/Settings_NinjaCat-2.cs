using CadEditor;
using System;
using System.Collections.Generic;
//css_include settings_ninja_cats/NinjaCatUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(1052, 21 , 8*5);   }
  public int getScreenWidth()          { return 5; }
  public int getScreenHeight()         { return 8; }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public int getPalBytesAddr()          { return 0x4B2E; }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x4052 , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x5376  , 8   , 0x440); }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()          { return NinjaCatUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()             { return NinjaCatUtils.getVideoChunk("chr2.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()             { return null; }
  public GetBlocksFunc        getBlocksFunc()                 { return NinjaCatUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()                 { return NinjaCatUtils.setBlocks;}
  public GetBigBlocksFunc     getBigBlocksFunc()              { return NinjaCatUtils.getBigBlocks;}
  public SetBigBlocksFunc     setBigBlocksFunc()              { return NinjaCatUtils.setBigBlocks;}
  public GetPalFunc           getPalFunc()                    { return NinjaCatUtils.readPalFromBin("pal2.bin"); }
  public SetPalFunc           setPalFunc()                    { return null;}
  public GetObjectsFunc getObjectsFunc()                      { return NinjaCatUtils.getObjects;  }
  public SetObjectsFunc setObjectsFunc()                      { return NinjaCatUtils.setObjects;  }
  
  public GetLayoutFunc getLayoutFunc()     { return getLayout;   }
  LevelLayerData getLayout(int levelNo)
  {
    var levelLayers = new int[][]
    {
        new int[] { 1,2 },
        new int[] { 3,4,5 },
        new int[] { 6,7,8 },
        new int[] { 9 },
        new int[] { 10, 11, 12 },
        new int[] { 14 },
        new int[] { 13 },
        new int[] { 15,16,17, },
        new int[] { 18,19, 20 },
        new int[] { 21 },
    };
    return new LevelLayerData(levelLayers[levelNo].Length, 1, levelLayers[levelNo]);
  }
  
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0xA605, 5 , 2, 1, 0x0), //room 1
    new LevelRec(0xA616, 5 , 3, 1, 0x0), //room 2
    new LevelRec(0xA627, 8 , 3, 1, 0x0), //room 3
    new LevelRec(0xA641, 1 , 1, 1, 0x0), //room 4
    new LevelRec(0xA646, 5 , 3, 1, 0x0), //room 5
    new LevelRec(0xA657, 2 , 1, 1, 0x0), //room 6
    new LevelRec(0xA65F, 2, 1, 1, 0x0), //room 7
    new LevelRec(0xA667, 7, 3, 1, 0x0), //room 8
    new LevelRec(0xA67E, 7, 3, 1, 0x0), //room 9
    new LevelRec(0xA695, 1, 1, 1, 0x0), //room 10
  };
}