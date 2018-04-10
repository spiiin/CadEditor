using CadEditor;
using System;
using PluginMapEditor;

public class ToxicUtils 
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
  
  public static MapData loadMapToxic(int mapNo)
  {
      return MapUtils.loadMapFromBlocks(mapNo, 480, 32, 32, false, fillAttribsToxic);
  }
  
  public static MapInfo[] makeMapsInfo()
  {
     var mapsInfo = new MapInfo[getScreensOffset().recCount];
     int scrSize = ConfigScript.getScreenWidth(0) * ConfigScript.getScreenHeight(0) * ConfigScript.getWordLen();
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

  public static int saveAttribsToxic(int mapNo, MapData mapData, out byte[] packedData)
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
          Globals.romdata[attribAddr + ind] = (byte)mapData.attrData[ind];
      }
      Globals.flushToFile();
      return 0;
  }
}