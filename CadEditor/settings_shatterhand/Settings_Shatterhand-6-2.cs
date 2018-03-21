using CadEditor;
using System;
using System.Collections.Generic;
//css_include settings_shatterhand/ShatterhandUtils.cs;

public class Data 
{
  /*public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginEditLayout.dll",
    };
  }*/
  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0xDA61, 12, 8*8);   }
  public int getScreenWidth()          { return 8; }
  public int getScreenHeight()         { return 8; }

  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xDF09, 1, 0x400); }
  public int getPalBytesAddr()          { return 0xE0BB; }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0xE3BB, 1, 0x4000);  }

  public int getBigBlocksCount()        { return 128; }
  public int getBlocksCount()           { return 128; }

  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return ShatterhandUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return ShatterhandUtils.getVideoChunk("chr6.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  public GetBigBlocksFunc     getBigBlocksFunc()     { return ShatterhandUtils.getBigBlocks; }
  public SetBigBlocksFunc     setBigBlocksFunc()     { return ShatterhandUtils.setBigBlocks;}
  public GetBlocksFunc        getBlocksFunc()        { return ShatterhandUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()        { return ShatterhandUtils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return ShatterhandUtils.readPalFromBin("pal6-2.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
  //public GetObjectsFunc       getObjectsFunc()       { return ShatterhandUtils.getObjects; }
  //public SetObjectsFunc       setObjectsFunc()       { return ShatterhandUtils.setObjects; }
  //public GetObjectDictionaryFunc getObjectDictionaryFunc() { return ShatterhandUtils.getObjectDictionary; }
  //public GetLayoutFunc        getLayoutFunc()        { return ShatterhandUtils.getLayoutLinearSH; }
  //public SetLayoutFunc        setLayoutFunc()        { return ShatterhandUtils.setLayoutLinearSH; }
  //public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  /*public IList<LevelRec> levelRec = new List<LevelRec>() 
  {
    new LevelRec(0x13001, 35, 16, 7, 0x81a3),
  };*/
}