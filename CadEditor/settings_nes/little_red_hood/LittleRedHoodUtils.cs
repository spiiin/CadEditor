using CadEditor;
using System;
using PluginMapEditor;

public static class LittleRedHoodUtils 
{  
  public static OffsetRec getScreensOffset()
  {
    return ConfigScript.screensOffset[0];
  }
  
  public static MapInfo[] makeMapsInfo()
  {
     var mapsInfo = new MapInfo[getScreensOffset().recCount];
     int scrSize = getScreensOffset().width * getScreensOffset().height * ConfigScript.getWordLen();
     int actualScrSize = getScreensOffset().recSize;
     int attrSize = 64;
     for (int i = 0; i < mapsInfo.Length; i++)
     {
         int da = getScreensOffset().beginAddr + actualScrSize * i;
         int aa = ConfigScript.getPalBytesAddr(0);
         mapsInfo[i] = new MapInfo(){ dataAddr = da, palAddr = ConfigScript.palOffset.beginAddr, videoNo = 0, attribsAddr = aa};
     }
     return mapsInfo;
  }
}