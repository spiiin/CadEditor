using CadEditor;
using System;
using PluginMapEditor;

public class ToxicUtils 
{
  public static OffsetRec getScreensOffset()
  {
    return ConfigScript.screensOffset[0];
  }
  
  public static MapData[] loadMapToxic(int mapNo)
  {
      return MapUtils.loadMapFromBlocks(mapNo, 480, 32, 32, false, fillAttribsToxic);
  }
  
  public static MapInfo[] makeMapsInfo()
  {
     var mapsInfo = new MapInfo[getScreensOffset().recCount];
     int scrSize = getScreensOffset().width * getScreensOffset().height * ConfigScript.getWordLen();
     for (int i = 0; i < mapsInfo.Length; i++)
     {
         int da = getScreensOffset().beginAddr + scrSize  * i;
         int aa = da;
         da += 16;
         mapsInfo[i] = new MapInfo(){ dataAddr = da, palAddr = ConfigScript.palOffset.beginAddr, videoNo = 0, attribsAddr = aa};
     }
     return mapsInfo;
  }
  
  public static void fillAttribsToxic(int[] attrData, byte[] romdata, int attribAddr)
  {
      int HEIGHT = 4;
      int WIDTH = 8;
      for (int i = 0; i < HEIGHT*WIDTH; i++)
      {
          int x = i % HEIGHT;
          int y = i / HEIGHT;
          int ind = y * HEIGHT + x;
          attrData[ind] = Globals.romdata[attribAddr + ind];
      }
  }

  public static int saveAttribsToxic(int mapNo, MapData[] mapData, out byte[] packedData)
  {
      packedData = new byte[0];
      int attribAddr = MapConfig.mapsInfo[mapNo].attribsAddr;

      int HEIGHT = 4;
      int WIDTH = 8;
      for (int i = 0; i < HEIGHT * WIDTH; i++)
      {
          int x = i % HEIGHT;
          int y = i / HEIGHT;
          int ind = y * HEIGHT + x;
          Globals.romdata[attribAddr + ind] = (byte)mapData[0].attrData[ind];
      }
      Globals.flushToFile();
      return 0;
  }
}