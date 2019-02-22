using CadEditor;
using System;
//css_include shared_settings/SharedUtils.cs;
//css_include addams_family_the_pugsley_s_scavenger_hunt/AddamsUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x02fd2, 1 , 15*7, 15, 7);   }

  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }

  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk(new[] {"chr2.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }

  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x3C22, 1, 0x1000); }
  public int getBlocksCount()           { return 158; }
  
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x353E, 1, 0x1000); }
  public int getBigBlocksCount()        { return 256; }
  
  public static int getPalBytesAddr()          { return 0x3E9A; }

  public GetBlocksFunc        getBlocksFunc() { return AddamsUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return AddamsUtils.setBlocks;}
  
  public GetBigBlocksFunc     getBigBlocksFunc()     { return Utils.getBigBlocksCapcomDefault;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return Utils.setBigBlocksCapcomDefault;}
  
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal2.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
}