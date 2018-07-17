using CadEditor;
using System;
using PluginMapEditor;

public static class SilentUtils 
{ 
  public static OffsetRec getScrOffet()
  {
    return ConfigScript.screensOffset[0];
  }
  
  public static MapData[] loadMap(int mapNo)
  {
      int w = getScrOffet().width*2;
      int h = getScrOffet().height*2;
      var mapData = MapUtils.loadMapFromBlocks(mapNo, w*h, w*h/4, h, true, MapUtils.fillAttribs);
      return mapData;
  }
  
  public static MapInfo[] makeMapsInfo()
  {
     var mapsInfo = new MapInfo[getScrOffet().recCount];
     int scrSize = getScrOffet().width * getScrOffet().height * ConfigScript.getWordLen();
     for (int i = 0; i < mapsInfo.Length; i++)
     {
         int da = getScrOffet().beginAddr + scrSize  * i;
         int aa = ConfigScript.getPalBytesAddr() + 64*i;
         mapsInfo[i] = new MapInfo(){ dataAddr = da, palAddr = ConfigScript.palOffset.beginAddr, videoNo = 0, attribsAddr = aa};
     }
     return mapsInfo;
  }
}