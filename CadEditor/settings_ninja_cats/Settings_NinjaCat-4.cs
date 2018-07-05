using CadEditor;
using System;
using System.Collections.Generic;
//css_include settings_ninja_cats/NinjaCatUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(2864, 20 , 8*5);   }
  public int getScreenWidth()          { return 5; }
  public int getScreenHeight()         { return 8; }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public string getObjTypesPicturesDir() { return "obj_sprites_ninjacats"; }
  public DrawObjectBigFunc getDrawObjectBigFunc() { return NinjaCatUtils.drawObjectBig; }
  
  public int getPalBytesAddr()          { return 0x4d15; }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x47ee , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x5F22  , 8   , 0x440); }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()          { return NinjaCatUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()             { return NinjaCatUtils.getVideoChunk("chr3.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()             { return null; }
  public GetBlocksFunc        getBlocksFunc()                 { return NinjaCatUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()                 { return NinjaCatUtils.setBlocks;}
  public GetBigBlocksFunc     getBigBlocksFunc()              { return NinjaCatUtils.getBigBlocks;}
  public SetBigBlocksFunc     setBigBlocksFunc()              { return NinjaCatUtils.setBigBlocks;}
  public GetPalFunc           getPalFunc()                    { return NinjaCatUtils.readPalFromBin("pal4.bin"); }
  public SetPalFunc           setPalFunc()                    { return null;}
  public GetObjectsFunc getObjectsFunc()                      { return NinjaCatUtils.getObjects;  }
  public SetObjectsFunc setObjectsFunc()                      { return NinjaCatUtils.setObjects;  }
  
  public GetLayoutFunc getLayoutFunc()     { return getLayout;   }
  LevelLayerData getLayout(int levelNo)
  {
    var levelLayers = new int[][]
    {
        new int[] { 1,2,3,4 },
        new int[] { 5,6,7 },
        new int[] { 8, },
        new int[] { 9,10,11,12 },
        new int[] { 13,14},
        new int[] { 15 },
        new int[] { 16,17,18,19,20 },
        new int[] { 20 },
    };
    return new LevelLayerData(levelLayers[levelNo].Length, 1, levelLayers[levelNo]);
  }
  
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0xA775, 9 , 4, 1, 0x0), //room 1
    new LevelRec(0xA792, 5 , 3, 1, 0x0), //room 2
    new LevelRec(0xA7A3, 4 , 1, 1, 0x0), //room 3
    new LevelRec(0xA7B1, 7 , 4, 1, 0x0), //room 4
    new LevelRec(0xA7C8, 5 , 2, 1, 0x0), //room 5
    new LevelRec(0xA7D9, 3 , 1, 1, 0x0), //room 6
    new LevelRec(0xA7E4, 10,  5, 1, 0x0), //room 7
    new LevelRec(0xA804, 1 ,  1, 1, 0x0), //room 8
  };
}