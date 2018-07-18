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
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x2d673, 29 , 16*15, 16, 15);   }
  public bool isBuildScreenFromSmallBlocks() { return true; }

  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public OffsetRec getVideoOffset()                  { return new OffsetRec(0x57010, 1 , 0x1000); }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x2f1a3 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 158; }
  public int getBigBlocksCount()        { return 158; }
  public GetBlocksFunc getBlocksFunc() { return Utils.getBlocksLinear2x2withoutAttrib;}
  public SetBlocksFunc setBlocksFunc() { return Utils.setBlocksLinearWithoutAttrib;}
  public int getPalBytesAddr()          { return 0xeb36; }
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  //----------------------------------------------------------------------------
  public byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x21, 0x0d, 0x3c, 0x15, 0x21, 0x0d, 0x11, 0x01,
      0x21, 0x0d, 0x29, 0x19, 0x21, 0x17, 0x27, 0x37
    }; 
    return pallete;
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