using CadEditor;
using System;
using PluginMapEditor;

public static class KabukiUtils
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
  
  public static OffsetRec getScrOffet()
  {
    return ConfigScript.screensOffset[0];
  }
}