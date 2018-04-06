using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginChrView.dll",
    };
  }
  public OffsetRec getVideoOffset()   { return new OffsetRec(0x22010, 1, 0x1000); }
  public OffsetRec getScreensOffset() { return new OffsetRec(0xe017 , 1, 7*144 ); }
  public OffsetRec getPalOffset()     { return new OffsetRec(0x1ab93, 16, 16   ); }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xe4bd , 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getPalBytesAddr()          { return 0xe407; }
  
  public int getScreenWidth()         { return 7; }
  public int getScreenHeight()        { return 144; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public int getBigBlocksCount()  { return 256; }
  public bool getScreenVertical() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()         { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()            { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()            { return Utils.setVideoChunk; }
  public GetPalFunc           getPalFunc() { return Utils.getPalleteLinear;}
  public SetPalFunc           setPalFunc() { return Utils.setPalleteLinear;}
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
}