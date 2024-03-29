using CadEditor;
using System;
using System.Collections.Generic;
//css_include shatterhand/ShatterhandUtils.cs;

public class Data 
{
  /*public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginEditLayout.dll",
    };
  }*/
  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0xa987 - 64*4, 26, 8*8, 8, 8);   }

  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xaf47, 1, 0x400); }
  public int getPalBytesAddr()          { return 0xb437; }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0xb6af, 1, 0x4000);  }

  public int getBigBlocksCount()        { return 256; }
  public int getBlocksCount()           { return 256; }
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 1   , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 1   , 16); }

  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return ShatterhandUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return ShatterhandUtils.getVideoChunk(new[] {"chr7.bin"});   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  public GetBigBlocksFunc     getBigBlocksFunc()     { return ShatterhandUtils.getBigBlocks; }
  public SetBigBlocksFunc     setBigBlocksFunc()     { return ShatterhandUtils.setBigBlocks;}
  public GetBlocksFunc        getBlocksFunc()        { return ShatterhandUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()        { return ShatterhandUtils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return ShatterhandUtils.readPalFromBin(new[] {"pal7.bin"}); }
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