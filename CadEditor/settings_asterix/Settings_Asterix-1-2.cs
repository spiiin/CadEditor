using CadEditor;
using System;
using PluginMapEditor;
//css_include settings_asterix/AsterixUtils.cs;

public class Data 
{
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginMapEditor.dll",
    };
  }
  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x490c, 1 , 12*63, 12, 63);   }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return AsterixUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return AsterixUtils.getVideoChunk("chr1-2.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x4c00, 1  , 0x1000);  }
  public int getBlocksCount()           { return 116; }
  public int getBigBlocksCount()        { return 116; }
  public int getPalBytesAddr()          { return 0x4700; }
  
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksLinear2x2withoutAttrib;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksLinearWithoutAttrib;}
  public GetPalFunc           getPalFunc()           { return AsterixUtils.readPalFromBin("pal1-2.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
  //----------------------------------------------------------------------------
  public MapInfo[] getMapsInfo()      { return AsterixUtils.makeMapsInfo(); }
  public LoadMapFunc getLoadMapFunc() { return AsterixUtils.loadMap; }
  public SaveMapFunc getSaveMapFunc() { return AsterixUtils.saveAttribsT; }
  public bool isMapReadOnly()         { return true; }
  public bool mapEditorSharePallete() { return true; }
  //----------------------------------------------------------------------------
}