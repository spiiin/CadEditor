using CadEditor;
using System;
using PluginMapEditor;
//css_include shared_settings/SharedUtils.cs;
//css_include don_doko_don/DonDokoDonUtils.cs;

public class Data 
{
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginMapEditor.dll",
    };
  }
  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x140, 7 , 16*15+64, 16, 15);   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk("chr1.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x1310, 1  , 0x1000);  }
  public int getBlocksCount()           { return 136; }
  public int getBigBlocksCount()        { return 136; }
  public int getPalBytesAddr()          { return 0x230; }
  
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksLinear2x2withoutAttrib;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksLinearWithoutAttrib;}
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin("pal1.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
  //----------------------------------------------------------------------------
  public MapInfo[] getMapsInfo()      { return DonDokoDonUtils.makeMapsInfo(); }
  public LoadMapFunc getLoadMapFunc() { return MapUtils.loadMapBatman; }
  public SaveMapFunc getSaveMapFunc() { return MapUtils.saveAttribs; }
  public bool isMapReadOnly()         { return true; }
  public bool mapEditorSharePallete() { return true; }
  //----------------------------------------------------------------------------
}