using CadEditor;
using System;
using PluginMapEditor;

public class DonDokoDonUtils 
{ 
  public static OffsetRec getScreensOffset()
  {
    return ConfigScript.screensOffset[0];
  }
  
  public static MapInfo[] makeMapsInfo()
  {
     var mapsInfo = new MapInfo[getScreensOffset().recCount];
     int scrSize = ConfigScript.getScreenWidth(0) * ConfigScript.getScreenHeight(0) * ConfigScript.getWordLen();
     int actualScrSize = getScreensOffset().recSize;
     int attrSize = 64;
     for (int i = 0; i < mapsInfo.Length; i++)
     {
         int da = getScreensOffset().beginAddr + actualScrSize * i;
         int aa = da + scrSize;
         mapsInfo[i] = new MapInfo(){ dataAddr = da, palAddr = ConfigScript.palOffset.beginAddr, videoNo = 0, attribsAddr = aa};
     }
     return mapsInfo;
  }
}