using CadEditor;
using System;
using System.Collections.Generic;
//css_include ninja_cats/NinjaCatUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(1940, 22 , 8*5, 5, 8);   }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public string getObjTypesPicturesDir() { return "obj_sprites_ninjacats"; }
  public DrawObjectBigFunc getDrawObjectBigFunc() { return NinjaCatUtils.drawObjectBig; }
  public SelectObjectBigFunc getSelectObjectBigFunc() { return NinjaCatUtils.selectObjectBig; }
  
  public int getPalBytesAddr()          { return 0x4d15; }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x47ee , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x5376  , 8   , 0x440); }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()          { return NinjaCatUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()             { return NinjaCatUtils.getVideoChunk("chr3.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()             { return null; }
  public GetBlocksFunc        getBlocksFunc()                 { return NinjaCatUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()                 { return NinjaCatUtils.setBlocks;}
  public GetBigBlocksFunc     getBigBlocksFunc()              { return NinjaCatUtils.getBigBlocks;}
  public SetBigBlocksFunc     setBigBlocksFunc()              { return NinjaCatUtils.setBigBlocks;}
  public GetPalFunc           getPalFunc()                    { return NinjaCatUtils.readPalFromBin("pal3.bin"); }
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
        new int[] { 6 },
        new int[] { 7,8,9 },
        new int[] { 10 },
        new int[] { 11 },
        new int[] { 12,13 },
        new int[] { 14, },
        new int[] { 15 }, //zero
        new int[] { 16,17 },
        new int[] { 18,19,20,21,22 },
        new int[] { 22 }
    };
    return new LevelLayerData(levelLayers[levelNo].Length, 1, levelLayers[levelNo]);
  }
  
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0xA6B2, 5 , 2, 1, 0x0), //room 1
    new LevelRec(0xA6C3, 8 , 3, 1, 0x0), //room 2
    new LevelRec(0xA6DD, 5 , 1, 1, 0x0), //room 3
    new LevelRec(0xA6EE, 7 , 3, 1, 0x0), //room 4
    new LevelRec(0xA705, 1 , 1, 1, 0x0), //room 5
    new LevelRec(0xA70A, 2 , 1, 1, 0x0), //room 6
    new LevelRec(0xA711, 3 , 2, 1, 0x0), //room 7
    new LevelRec(0xA71D, 3, 1, 1, 0x0), //room 8
    new LevelRec(0xA728, 0, 1, 1, 0x0), //room 9
    new LevelRec(0xA72C, 4, 2, 1, 0x0), //room 10
    new LevelRec(0xA73A, 10, 5, 1, 0x0), //room 11
    new LevelRec(0xA75A, 3, 1, 1, 0x0), //room 12
  };
}