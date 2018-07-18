using CadEditor;
using System;
using PluginMapEditor;
//css_include settings_silent_assault_unl/SilentUtils.cs;

public class Data 
{ 
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginMapEditor.dll",
    };
  }
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x59da, 1 , 15*16, 15, 16);   }
  public bool getScreenVertical()      { return true; }
  public bool isBuildScreenFromSmallBlocks() { return true; }

  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public OffsetRec getVideoOffset()                  { return new OffsetRec(0x15010, 1 , 0x1000); }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x4e5e , 1  , 0x1000);  }
  public int getBlocksCount()           { return 242; }
  public int getBigBlocksCount()        { return 242; }
  public GetBlocksFunc getBlocksFunc() { return Utils.getBlocksLinear2x2withoutAttrib;}
  public SetBlocksFunc setBlocksFunc() { return Utils.setBlocksLinearWithoutAttrib;}
  public int getPalBytesAddr()          { return 0x5cca; }
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  //----------------------------------------------------------------------------
  public byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x10, 0x0f, 0x15, 0x27, 0x10, 0x01, 0x2c, 0x24,
      0x10, 0x19, 0x29, 0x39, 0x10, 0x0d, 0x10, 0x30
    }; 
    return pallete;
  }
  
  //-------------------------------------------------------------------------------------------------------------------
  public MapInfo[] getMapsInfo()      { return SilentUtils.makeMapsInfo(); }
  public LoadMapFunc getLoadMapFunc() { return SilentUtils.loadMap; }
  public SaveMapFunc getSaveMapFunc() { return MapUtils.saveAttribs; }
  public bool isMapReadOnly()         { return true; }
  public bool mapEditorSharePallete() { return true; }
  //-------------------------------------------------------------------------------------------------------------------
    
}