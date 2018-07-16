using CadEditor;
using System;
using System.Drawing;
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
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x156b0, 16 , 16*15, 16, 15);   }
  public bool isBuildScreenFromSmallBlocks() { return true; }

  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }

  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x166e8 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 54; }
  public int getBigBlocksCount()        { return 54; }
  public GetBlocksFunc getBlocksFunc() { return Utils.getBlocksLinear2x2withoutAttrib;}
  public SetBlocksFunc setBlocksFunc() { return Utils.setBlocksLinearWithoutAttrib;}
  public int getPalBytesAddr()          { return 0x14738; }
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  //----------------------------------------------------------------------------
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal4.bin");
  }

  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr4.bin", videoPageId);
  }
  
  //-------------------------------------------------------------------------------------------------------------------
  public MapInfo[] getMapsInfo() { return makeMapsInfo(); }
  public LoadMapFunc getLoadMapFunc() { return MapUtils.loadMapBatman; }
  public SaveMapFunc getSaveMapFunc() { return MapUtils.saveAttribs; }
  public bool isMapReadOnly()         { return true; }
  public bool mapEditorSharePallete() { return true; }

  public MapInfo[] makeMapsInfo()
  {
     var mapsInfo = new MapInfo[getScreensOffset().recCount];
     int scrSize = getScreensOffset().width * getScreensOffset().height * ConfigScript.getWordLen();
     int attrSize = 64;
     for (int i = 0; i < mapsInfo.Length; i++)
     {
         int da = getScreensOffset().beginAddr + scrSize  * i;
         int aa = getPalBytesAddr() + attrSize * i;
         mapsInfo[i] = new MapInfo(){ dataAddr = da, palAddr = ConfigScript.palOffset.beginAddr, videoNo = 0, attribsAddr = aa};
     }
     return mapsInfo;
  }
  //-------------------------------------------------------------------------------------------------------------------
    
}