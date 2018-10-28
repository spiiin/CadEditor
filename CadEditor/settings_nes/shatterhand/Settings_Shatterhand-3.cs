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
  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0xF474, 9, 8*8, 8, 8);   }

  public OffsetRec getBlocksOffset()     { return new OffsetRec(0xF8F2, 1, 0x400); }
  public OffsetRec getBlocksOffset2()    { return new OffsetRec(0xFA16, 1, 0x400); }
  public int getPalBytesAddr(/*int blockId*/){ return 0xFE7A; /*return (blockId == 0) ? 0xFE7A : (0xFE7A + 73);*/ }
  public OffsetRec getBigBlocksOffset()  { return new OffsetRec(0xFC16, 2, 0x4000);  }
  public OffsetRec getBigBlocksOffset2() { return new OffsetRec(0xFD66, 2, 0x4000);  }

  public int getBigBlocksCount()        { return 128; }
  public int getBlocksCount()           { return 128; }
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 1   , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 3   , 16); }

  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return ShatterhandUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return ShatterhandUtils.getVideoChunk(new[]{"chr3.bin"});   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  public GetBigBlocksFunc     getBigBlocksFunc()     { return ShatterhandUtils.getBigBlocks; }
  public SetBigBlocksFunc     setBigBlocksFunc()     { return ShatterhandUtils.setBigBlocks;}
  public GetBigBlocksAddrFunc getBigBlocksAddrFunc() { return getBigBlocksAddr; }
  public GetBlocksFunc        getBlocksFunc()        { return ShatterhandUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()        { return ShatterhandUtils.setBlocks;}
  public GetBlocksAddrFunc    getBlocksAddrFunc()    { return getBlocksAddr; }
  public GetPalFunc           getPalFunc()           { return ShatterhandUtils.readPalFromBin(new[]{"pal3.bin", "pal3-2.bin", "pal3-3.bin"}); }
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
  
  public int getBigBlocksAddr(int blockId)
  {
    if (blockId == 0 )    { return getBigBlocksOffset().beginAddr;  }
    else if (blockId == 1) { return getBigBlocksOffset2().beginAddr; }
    return -1;
  }
  
  public int getBlocksAddr(int blockId)
  {
    if (blockId == 0 )    { return getBlocksOffset().beginAddr;  }
    else if (blockId == 1) { return getBlocksOffset2().beginAddr; }
    return -1;
  }
  
  /*public IList<LevelRec> levelRec = new List<LevelRec>() 
  {
    new LevelRec(0x13001, 35, 16, 7, 0x81a3),
  };*/
}