using CadEditor;
using System;
using PluginMapEditor;
//css_include settings_circus_caper/CircusCaperUtils.cs;

public class Data 
{
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginMapEditor.dll",
    };
  }
  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x15254, 1 , 12*240);   }
  public int getScreenWidth()          { return 12; }
  public int getScreenHeight()         { return 240; }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return CircusCaperUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return CircusCaperUtils.getVideoChunk("chr4.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x150ac, 1  , 0x1000);  }
  public int getBlocksCount()           { return 106; }
  public int getBigBlocksCount()        { return 106; }
  public int getPalBytesAddr()          { return 0x15d94+1; }
  
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksLinear2x2withoutAttrib;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksLinearWithoutAttrib;}
  public GetPalFunc           getPalFunc()           { return CircusCaperUtils.readPalFromBin("pal4.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
  //----------------------------------------------------------------------------
  public MapInfo[] getMapsInfo()      { return CircusCaperUtils.makeMapsInfo(); }
  public LoadMapFunc getLoadMapFunc() { return CircusCaperUtils.loadMap; }
  public SaveMapFunc getSaveMapFunc() { return CircusCaperUtils.saveAttribsT; }
  public bool isMapReadOnly()         { return true; }
  public bool mapEditorSharePallete() { return true; }
  //----------------------------------------------------------------------------
}