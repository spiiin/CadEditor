using CadEditor;
using System;
using PluginMapEditor;
//css_include asterix/AsterixUtils.cs;

public class Data 
{
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginMapEditor.dll",
    };
  }
  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0xc710, 1 , 16*12, 16, 12);   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return AsterixUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return AsterixUtils.getVideoChunk("chr-bonus1.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xf6a6, 1  , 0x1000);  }
  public int getBlocksCount()           { return 44; }
  public int getBigBlocksCount()        { return 44; }
  public int getPalBytesAddr()          { return 0xc010; }
  
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksLinear2x2withoutAttrib;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksLinearWithoutAttrib;}
  public GetPalFunc           getPalFunc()           { return AsterixUtils.readPalFromBin("pal-bonus1.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
  //----------------------------------------------------------------------------
  public MapInfo[] getMapsInfo()      { return AsterixUtils.makeMapsInfo(); }
  public LoadMapFunc getLoadMapFunc() { return AsterixUtils.loadMap; }
  public SaveMapFunc getSaveMapFunc() { return AsterixUtils.saveAttribsT; }
  public bool isMapReadOnly()         { return true; }
  public bool mapEditorSharePallete() { return true; }
  //----------------------------------------------------------------------------
}