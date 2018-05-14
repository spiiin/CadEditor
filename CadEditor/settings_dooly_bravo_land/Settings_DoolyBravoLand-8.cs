using CadEditor;
using System;
using PluginMapEditor;
//css_include settings_dooly_bravo_land/DoolyUtils.cs;

public class Data 
{
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginMapEditor.dll",
    };
  }
  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x20010, 15 , 16*14);   }
  public int getScreenWidth()          { return 16; }
  public int getScreenHeight()         { return 14; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return DoolyUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return DoolyUtils.getVideoChunk("chr8.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x21350, 1  , 0x1000);  }
  public int getBlocksCount()           { return 136; }
  public int getBigBlocksCount()        { return 136; }
  public int getPalBytesAddr()          { return 0x21750; }
  
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksLinear2x2withoutAttrib;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksLinearWithoutAttrib;}
  public GetPalFunc           getPalFunc()           { return DoolyUtils.readPalFromBin("pal8.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
  //----------------------------------------------------------------------------
  public MapInfo[] getMapsInfo()      { return DoolyUtils.makeMapsInfo(); }
  public LoadMapFunc getLoadMapFunc() { return MapUtils.loadMapBatman; }
  public SaveMapFunc getSaveMapFunc() { return MapUtils.saveAttribs; }
  public bool isMapReadOnly()         { return true; }
  public bool mapEditorSharePallete() { return true; }
  //----------------------------------------------------------------------------
}