using CadEditor;
using System;
using System.Collections.Generic;
//css_include shatterhand/ShatterhandUtils.cs;

public class Data 
{
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginEditLayout.dll",
    };
  }
  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x8D5f, 13, 8*8, 8, 8);   }

  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x909F, 1, 0x400); }
  public int getPalBytesAddr()          { return 0x929F; }
  public OffsetRec getBigBlocksOffset()  { return new OffsetRec(0x931F         , 2, 0x4000);  }
  public OffsetRec getBigBlocksOffset2() { return new OffsetRec(0x931F + 0x3d*4, 2, 0x4000);  }

  public int getBigBlocksCount()        { return 128; }
  public int getBlocksCount()           { return 128; }
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 1   , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 3   , 16); }

  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return ShatterhandUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return ShatterhandUtils.getVideoChunk(new[]{"chr2.bin"});   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  public GetBigBlocksFunc     getBigBlocksFunc()     { return ShatterhandUtils.getBigBlocks; }
  public SetBigBlocksFunc     setBigBlocksFunc()     { return ShatterhandUtils.setBigBlocks; }
  public GetBigBlocksAddrFunc getBigBlocksAddrFunc() { return getBigBlocksAddr; }
  public GetBlocksFunc        getBlocksFunc()        { return ShatterhandUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()        { return ShatterhandUtils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return ShatterhandUtils.readPalFromBin(new[]{"pal2.bin", "pal2-2.bin", "pal2-3.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
  public GetObjectsFunc       getObjectsFunc()       { return ShatterhandUtils.getObjects; }
  public SetObjectsFunc       setObjectsFunc()       { return ShatterhandUtils.setObjects; }
  public GetObjectDictionaryFunc getObjectDictionaryFunc() { return ShatterhandUtils.getObjectDictionary; }
  public GetLayoutFunc        getLayoutFunc()        { return ShatterhandUtils.getLayoutLinearSH; }
  public SetLayoutFunc        setLayoutFunc()        { return ShatterhandUtils.setLayoutLinearSH; }
  public GetLevelRecsFunc getLevelRecsFunc()         { return getLevelRecs; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public IList<LevelRec> getLevelRecs()
  {
      var groups = ConfigScript.getGroups();
      return new List<LevelRec>() 
      {
        new LevelRec(0x13584, 10, 16, 7, 0x8d08, groups[0].name, groups[0]),
        new LevelRec(0x135E0, 14, 16, 7, 0x8d08, groups[1].name, groups[1]),
        //new LevelRec(0x135E0, 14, 16, 7, 0x8d07, groups[2].name, groups[2]),
      };
  }
  
  public int getBigBlocksAddr(int blockId)
  {
    if (blockId == 0 )     { return getBigBlocksOffset().beginAddr;  }
    else if (blockId == 1) { return getBigBlocksOffset2().beginAddr; }
    return -1;
  }
  
  public GetGroupsFunc getGroupsFunc() { return getGroups; }
  public GroupRec[] getGroups()
  {
    return new GroupRec[] { 
      new GroupRec("2-1"         , 0,0,0,0, 2),
      new GroupRec("2-2"         , 0,1,0,1, 8),
      new GroupRec("2-boss"      , 0,1,0,2, 1),
    };
  }
}