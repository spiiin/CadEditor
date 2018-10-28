using CadEditor;
using System;

public static class MickeyUtils 
{ 
  public static ObjRec[] getBlocks(int tileId)
  {
      int count = ConfigScript.getBlocksCount(tileId);
      var bb = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(tileId), 2, 2, count, false, false);
      var palAddr = ConfigScript.getPalBytesAddr(tileId);
      for (int i = 0; i < count; i++)
      {
          bb[i].palBytes[0] = Globals.romdata[palAddr + i];
      }
      return bb;
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocksData)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount(tileId);
    var palAddr = ConfigScript.getPalBytesAddr(tileId);
    Utils.writeBlocksLinear(blocksData, Globals.romdata, addr, count, false, false);
    int realBlocksCount = ConfigScript.getBlocksCount(tileId); //all 256 used?
    for (int i = 0; i < realBlocksCount; i++)
    {
        Globals.romdata[palAddr + i] = (byte)blocksData[i].palBytes[0];
    }
  }
  
  public static GetPalFunc readPalFromBin(string[] fname)
  {
      return (int x)=> { return Utils.readBinFile(fname[x]); };
  }
  
  public static GetVideoPageAddrFunc fakeVideoAddr()
  {
      return (int _)=> { return -1; };
  }
  
  public static GetVideoChunkFunc getVideoChunk(string[] fname)
  {
     return (int x)=> { return Utils.readVideoBankFromFile(fname[x], 0); };
  }
}