using CadEditor;
using System;
using PluginMapEditor;
//css_include silent_assault_unl/SilentUtils.cs;

public class Data 
{ 
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginMapEditor.dll",
    };
  }
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x9e20, 10 , 16*16, 16, 16);   }
  public bool getScreenVertical()      { return true; }
  public bool isBuildScreenFromSmallBlocks() { return true; }

  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public OffsetRec getVideoOffset()                  { return new OffsetRec(0x14010, 1 , 0x1000); }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xdd4c , 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 256; }
  public GetBlocksFunc getBlocksFunc() { return Utils.getBlocksLinear2x2withoutAttrib;}
  public SetBlocksFunc setBlocksFunc() { return Utils.setBlocksLinearWithoutAttrib;}
  public int getPalBytesAddr()          { return 0xa780; }
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  //----------------------------------------------------------------------------
  public byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x34, 0x2d, 0x3d, 0x27, 0x34, 0x2d, 0x10, 0x3d,
      0x34, 0x12, 0x22, 0x31, 0x34, 0x06, 0x16, 0x27
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