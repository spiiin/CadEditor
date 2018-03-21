using CadEditor;
using System;
using System.Collections.Generic;
//css_include settings_little_nemo/LittleNemoUtils.cs;

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
  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x8910, 33 , 8*8);   }
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public GetObjectsFunc getObjectsFunc()   { return LittleNemoUtils.getObjectsNemo; }
  public SetObjectsFunc setObjectsFunc()   { return LittleNemoUtils.setObjectsNemo; }
  public SortObjectsFunc sortObjectsFunc() { return LittleNemoUtils.sortObjectsNemo; }
  
  public GetLayoutFunc getLayoutFunc() { return LittleNemoUtils.getLayoutLinearPlusOne; }
  public SetLayoutFunc setLayoutFunc() { return LittleNemoUtils.setLayoutLinearPlusOne; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public GetBigBlocksFunc     getBigBlocksFunc()     { return LittleNemoUtils.getBigBlocks;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return LittleNemoUtils.setBigBlocks;}
  public GetBlocksFunc        getBlocksFunc()        { return LittleNemoUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()        { return LittleNemoUtils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return Utils.getPalleteLinear;}
  public SetPalFunc           setPalFunc()           { return Utils.setPalleteLinear;}
  
  public OffsetRec getPalOffset()       { return new OffsetRec(0x9230, 1  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x29010, 1 , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x39010, 1 , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x8510 , 1   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x8010 , 1   , 0x4000); }
  public int getPalBytesAddr()         { return 0x9240; }
  
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x92b1, 55, 16, 7, 0x9150), 
  };
}