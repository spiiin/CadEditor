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
  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0xD7A1, 23, 8*8, 8, 8);   }

  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xDD61, 2, 0x400); }
  public OffsetRec getBlocksOffset2()    { return new OffsetRec(0xDF09, 2, 0x400); }
  public int getPalBytesAddr()          { return 0xE051; }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0xE1C7, 2, 0x4000);  }
  public OffsetRec getBigBlocksOffset2() { return new OffsetRec(0xE3BB, 2, 0x4000);  }

public int getBigBlocksCountShatterhand(int hierLevel, int bigBlockId)
  {
    return 128;
  }
  public GetBigBlocksCountFunc getBigBlocksCountFunc() { return getBigBlocksCountShatterhand; }
  
  public int getBlocksCountShatterhand(int blockId)
  {
    return 128;
  }
  public GetBlocksCountFunc getBlocksCountFunc() { return getBlocksCountShatterhand; }
  
  public int getPalBytesAddr(int blockId)
  { 
    return (blockId == 0) ? 0xE051 : 0xE0BB;
  }
  public GetPalBytesAddrFunc getPalBytesAddrFunc() { return getPalBytesAddr; }
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 1   , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 2   , 16); }

  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return ShatterhandUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return ShatterhandUtils.getVideoChunk(new[]{"chr6.bin"});   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  public GetBigBlocksFunc     getBigBlocksFunc()     { return ShatterhandUtils.getBigBlocks; }
  public SetBigBlocksFunc     setBigBlocksFunc()     { return ShatterhandUtils.setBigBlocks;}
  public GetBigBlocksAddrFunc getBigBlocksAddrFunc() { return getBigBlocksAddr; }
  public GetBlocksFunc        getBlocksFunc()        { return ShatterhandUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()        { return ShatterhandUtils.setBlocks;}
  public GetBlocksAddrFunc    getBlocksAddrFunc()    { return getBlocksAddr; }
  public GetPalFunc           getPalFunc()           { return ShatterhandUtils.readPalFromBin(new[]{"pal6.bin", "pal6-3.bin"}); }
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
  
  public GetGroupsFunc getGroupsFunc() { return getGroups; }
  public GroupRec[] getGroups()
  {
    return new GroupRec[] { 
      new GroupRec("6-1"         , 0,0,0,0, 1),
      new GroupRec("6-2"         , 0,1,1,0, 12),
      new GroupRec("6-boss"      , 0,1,1,1, 14),
    };
  }
  
  /*public IList<LevelRec> levelRec = new List<LevelRec>() 
  {
    new LevelRec(0x13001, 35, 16, 7, 0x81a3),
  };*/
}