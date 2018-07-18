using CadEditor;
using System;
using PluginMapEditor;

public class HudsonUtils
{
  public static OffsetRec getScreensOffset()
  {
    return ConfigScript.screensOffset[0];
  }
  
  public static MapData[] loadMap(int mapNo)
  {
      int w = getScreensOffset().width*2;
      int h = getScreensOffset().height*2;
      var mapData = MapUtils.loadMapFromBlocks(mapNo, w*h, w*h/4, w, false, MapUtils.fillAttribs);
      return mapData;
  }
  
  public static MapInfo[] makeMapsInfo()
  {
     var mapsInfo = new MapInfo[getScreensOffset().recCount];
     int scrSize = getScreensOffset().width * getScreensOffset().height * ConfigScript.getWordLen();
     for (int i = 0; i < mapsInfo.Length; i++)
     {
         int da = getScreensOffset().beginAddr + scrSize  * i;
         int aa = ConfigScript.getPalBytesAddr();
         mapsInfo[i] = new MapInfo(){ dataAddr = da, palAddr = ConfigScript.palOffset.beginAddr, videoNo = 0, attribsAddr = aa};
     }
     return mapsInfo;
  }
}