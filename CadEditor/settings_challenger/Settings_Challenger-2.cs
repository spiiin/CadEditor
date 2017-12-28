using CadEditor;
using System;

public class Data 
{
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginChrView.dll"
    };
  }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x9010, 1, 0x1000); }
  public OffsetRec getPalOffset()       { return new OffsetRec(0x5842, 1, 16);    }
  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x5bfc - 40, 1, 40*40); }
  public int getScreenWidth()          { return 40; }
  public int getScreenHeight()         { return 40; }

  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }

  public virtual GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public virtual GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public virtual SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public virtual GetPalFunc           getPalFunc()           { return Utils.getPalleteLinear;}
  public virtual SetPalFunc           setPalFunc()           { return Utils.setPalleteLinear;}

  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x6691 , 1, 0x1000); }
  public int getPalBytesAddr()          { return 0x65B3; }
  public int getBlocksCount()           { return 256; }
  
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x6213, 1, 0x1000); }
  public int getBigBlocksCount()        { return 256; }

  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetBigBlocksFunc     getBigBlocksFunc()     { return Utils.getBigBlocksCapcomDefault;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return Utils.setBigBlocksCapcomDefault;}
}
