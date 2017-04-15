using CadEditor;
using System.Collections.Generic;
using PluginMapEditor;

public class Data
{
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginMapEditor.dll",
    };
  }
  public OffsetRec getScreensOffset()  { return new OffsetRec(0xcb06, 1 , 256);   }
  public int getScreenWidth()          { return 256; }
  public int getScreenHeight()         { return 1; }
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public OffsetRec getVideoOffset()                  { return new OffsetRec(0x21010, 1 , 0x1000); }
  
  public GetPalFunc           getPalFunc() { return Utils.getPalleteLinear;}
  public SetPalFunc           setPalFunc() { return Utils.setPalleteLinear;}
  public OffsetRec getPalOffset()          { return new OffsetRec(0x2e0d,  1, 16   ); }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xCC34 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 128; }
  public int getBigBlocksCount()        { return 128; }
  
  public GetBlocksFunc        getBlocksFunc() { return getBlocksLinear1x6withoutAttrib;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksLinearWithoutAttrib;}
  //in fact, there is one attribs array, for background and floor, but it's impossible to describe blocks of different types in one config
  public int getPalBytesAddr()          { return 0xCCCC + 64*5; } 
  
  //-------------------------------------------------------------------------------------------------------------------
  public static ObjRec[] getBlocksLinear1x6withoutAttrib(int blockIndex)
  {
      return Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), 1, 6, ConfigScript.getBlocksCount(), false);
  }
  
  public MapInfo[] getMapsInfo() { return getMaps(); }
  public LoadMapFunc getLoadMapFunc() { return MapUtils.loadMapAddamsFamilyFloor; }
  public SaveMapFunc getSaveMapFunc() { return MapUtils.saveAttribs; }
  public bool isMapReadOnly()         { return true; }

  MapInfo[] getMaps()
  {
    return new MapInfo[]
    { 
        new MapInfo(){ dataAddr = getScreensOffset().beginAddr, palAddr = getPalOffset().beginAddr, videoNo = 0, attribsAddr = getPalBytesAddr() },
    };
  }
}