using CadEditor;
using System;
using PluginMapEditor;

public class TakeshiUtils
{
  public static ObjRec[] getBlocks(int blockIndex)
  {
      return Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), 2, 2, ConfigScript.getBlocksCount(), false, true);
  }

  public static void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
      Utils.writeBlocksLinear(blocksData, Globals.romdata, ConfigScript.getTilesAddr(blockIndex), ConfigScript.getBlocksCount(), false, true);
  }
        
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
  
  public static MapData[] loadMapMoonCrystal(int mapNo)
  {
      return MapUtils.loadMapFromBlocks(mapNo, 120, 30, 4, true, MapUtils.fillAttribs);
  }
  
  public static MapInfo[] makeMapsInfo()
  {
     var mapsInfo = new MapInfo[getScreensOffset().recCount];
     int scrSize = getScreensOffset().recSize;
     for (int i = 0; i < mapsInfo.Length; i++)
     {
         int da = getScreensOffset().beginAddr + scrSize  * i;
         int aa = da + ConfigScript.getScreenWidth(0) * ConfigScript.getScreenHeight(0);
         mapsInfo[i] = new MapInfo(){ dataAddr = da, palAddr = ConfigScript.palOffset.beginAddr, videoNo = 0, attribsAddr = aa};
     }
     return mapsInfo;
  }
}