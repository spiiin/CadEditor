using CadEditor;
using System;
using PluginMapEditor;
//css_include toxic_crusaders/ToxicUtils.cs;

public class Data 
{  
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginMapEditor.dll",
    };
  }
  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x968c-16*2, 3 , 16*13, 16, 13);   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return ToxicUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return ToxicUtils.getVideoChunk("chr6-2.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x904e, 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 256; }
  
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksLinear2x2withoutAttrib;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksLinearWithoutAttrib;}
  public GetPalFunc           getPalFunc()           { return ToxicUtils.readPalFromBin("pal6-2.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
  
  //----------------------------------------------------------------------------
  public MapInfo[] getMapsInfo()      { return ToxicUtils.makeMapsInfo(); }
  public LoadMapFunc getLoadMapFunc() { return ToxicUtils.loadMapToxic; }
  public SaveMapFunc getSaveMapFunc() { return ToxicUtils.saveAttribsToxic; }
  public bool isMapReadOnly()         { return true; }
  public bool mapEditorSharePallete() { return true; }
  //----------------------------------------------------------------------------
}