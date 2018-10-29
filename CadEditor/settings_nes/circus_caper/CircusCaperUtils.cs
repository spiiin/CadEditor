using CadEditor;
using System;
using PluginMapEditor;

public class CircusCaperUtils 
{
  public static GetPalFunc readPalFromBin(string fname)
  {
      return (int _)=> { return Utils.readBinFile(fname); };
  }
  
  public static GetVideoPageAddrFunc fakeVideoAddr()
  {
      return (int _)=> { return -1; };
  }
  
  public static GetVideoChunkFunc getVideoChunk(string fname)
  {
     return (int _)=> { return Utils.readVideoBankFromFile(fname, 0); };
  }
  
  public static OffsetRec getScreensOffset()
  {
    return ConfigScript.screensOffset[0];
  }
  
  public static MapData[] loadMap(int mapNo)
  {
      int w = (getScreensOffset().width+4)*2;
      int h = getScreensOffset().height*2;
      var mapData = MapUtils.loadMapFromBlocks(mapNo, w*h, w*h/4, h, true, fillAttribsT, 2);
      return mapData;
  }
  
  public static void fillAttribsT(int[] attrData, byte[] romdata, int attribAddr)
  {
      int WIDTH = getScreensOffset().width/2+2;
      int HEIGHT = getScreensOffset().height/2;
      for (int i = 0; i < HEIGHT*WIDTH; i++)
      {
          int x = i % WIDTH;
          int y = i / WIDTH;
          int ind = y * WIDTH + x;
          int tind = x * HEIGHT + y;
          attrData[tind] = Globals.romdata[attribAddr + ind];
      }
  }

  public static int saveAttribsT(int mapNo, MapData[] mapData, out byte[] packedData)
  {
      packedData = new byte[0];
      int attribAddr = MapConfig.mapsInfo[mapNo].attribsAddr;

      int WIDTH = getScreensOffset().width/2+2;
      int HEIGHT = getScreensOffset().height/2;
      for (int i = 0; i < HEIGHT * WIDTH; i++)
      {
          int x = i % WIDTH;
          int y = i / WIDTH;
          int ind = y * WIDTH + x;
          int tind = x * HEIGHT + y;
          Globals.romdata[attribAddr + ind] = (byte)mapData[0].attrData[tind];
      }
      Globals.flushToFile();
      return 0;
  }
  
  public static MapInfo[] makeMapsInfo()
  {
     var mapsInfo = new MapInfo[getScreensOffset().recCount];
     int scrSize = getScreensOffset().width * getScreensOffset().height * ConfigScript.getWordLen();
     for (int i = 0; i < mapsInfo.Length; i++)
     {
         int da = getScreensOffset().beginAddr + scrSize  * i;
         int aa = ConfigScript.getPalBytesAddr(0);
         mapsInfo[i] = new MapInfo(){ dataAddr = da, palAddr = ConfigScript.palOffset.beginAddr, videoNo = 0, attribsAddr = aa};
     }
     return mapsInfo;
  }
}