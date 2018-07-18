using CadEditor;
using System;
using System.Collections.Generic;
//css_include little_nemo/LittleNemoUtils.cs;

public class Data 
{ 
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginChrView.dll",
      "PluginEditLayout.dll",
    };
  }
  public string getObjTypesPicturesDir() { return "obj_sprites_nemo"; }
  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x12910, 30 , 8*8, 8, 8);   }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public GetObjectsFunc getObjectsFunc()   { return LittleNemoUtils.getObjectsNemo; }
  public SetObjectsFunc setObjectsFunc()   { return LittleNemoUtils.setObjectsNemo; }
  public SortObjectsFunc sortObjectsFunc() { return LittleNemoUtils.sortObjectsNemo; }
  
  public GetLayoutFunc getLayoutFunc() { return LittleNemoUtils.getLayoutLinearPlusOne; }
  public SetLayoutFunc setLayoutFunc() { return LittleNemoUtils.setLayoutLinearPlusOne; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()          { return LittleNemoUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()             { return LittleNemoUtils.getVideoChunk("chr8-3.bin");   }
  public GetBigBlocksFunc     getBigBlocksFunc()     { return LittleNemoUtils.getBigBlocks;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return LittleNemoUtils.setBigBlocks;}
  public GetBlocksFunc        getBlocksFunc()        { return LittleNemoUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()        { return LittleNemoUtils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return Utils.getPalleteLinear;}
  public SetPalFunc           setPalFunc()           { return Utils.setPalleteLinear;}
  
  public OffsetRec getPalOffset()       { return new OffsetRec(0xedd0, 1  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x2b010, 1 , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x3b010, 1 , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x12510 , 1   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x12010 , 1   , 0x4000); }
  
  public int getPalBytesAddr()         { return 0x13120; }  
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x1315c, 34, 16, 4, 0x13090), 
  };
}