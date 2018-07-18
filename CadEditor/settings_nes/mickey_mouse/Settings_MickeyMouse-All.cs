using CadEditor;
using System;
//css_include mickey_mouse/MickeyMouseUtils.cs;
public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0xa010, 1, 64*64, 64, 64);   }
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 4   , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 6   , 16); }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return MickeyUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return MickeyUtils.getVideoChunk(new[] {"chr1.bin", "chr2.bin", "chr3.bin", "chr4.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x4389, 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x4789, 1  , 0x1000);  }
  public int getBigBlocksCount()        { return 256; }
  public int getPalBytesAddr()          { return 0x4289; }
  
  public GetBlocksFunc        getBlocksFunc() { return MickeyUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return MickeyUtils.setBlocks;}
  public GetBigBlocksFunc     getBigBlocksFunc()     { return Utils.getBigBlocksCapcomDefault;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return Utils.setBigBlocksCapcomDefault;}
  public GetPalFunc           getPalFunc()           { return MickeyUtils.readPalFromBin(new[] {"pal1.bin", "pal2.bin", "pal3.bin", "pal3-1.bin", "pal4.bin", "pal5.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
  
  public GetGroupsFunc getGroupsFunc() { return getGroups; }
  public GroupRec[] getGroups()
  {
    return new GroupRec[] { 
      new GroupRec("Level 1" , 0,0,0,0, 0x01),
      new GroupRec("Level 2" , 1,0,0,1, 0x01),
      new GroupRec("Level 3" , 2,0,0,2, 0x01),
      new GroupRec("Level 3-flowers", 2,0,0,3, 0x01),
      new GroupRec("Level 4" , 3,0,0,4, 0x01),
      new GroupRec("Level 5" , 3,0,0,5, 0x01),
    };
  }
}