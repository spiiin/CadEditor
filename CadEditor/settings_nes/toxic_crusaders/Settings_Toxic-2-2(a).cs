using CadEditor;
using System;
using PluginMapEditor;
//css_include toxic_crusaders/ToxicUtils.cs;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{  
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginMapEditor.dll",
    };
  }
  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x58f0-16*2, 1 , 16*13, 16, 13);   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return  SharedUtils.getVideoChunk("chr2-2.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x557a, 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 256; }
  
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksLinear2x2withoutAttrib;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksLinearWithoutAttrib;}
  public GetPalFunc           getPalFunc()           { return  SharedUtils.readPalFromBin("pal2-2.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
  
  //----------------------------------------------------------------------------
  public MapInfo[] getMapsInfo()      { return ToxicUtils.makeMapsInfo(); }
  public LoadMapFunc getLoadMapFunc() { return ToxicUtils.loadMapToxic; }
  public SaveMapFunc getSaveMapFunc() { return ToxicUtils.saveAttribsToxic; }
  public bool isMapReadOnly()         { return true; }
  public bool mapEditorSharePallete() { return true; }
  //----------------------------------------------------------------------------
}