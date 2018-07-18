using CadEditor;
using System;

public static class AitMKUtils 
{ 
  
 public static int getBigTileNoFromScreen(int[] screenData, int index)
  {
    int noY = index % 8;
    int noX = index / 8;
    int lineByte = screenData[0x40 + noX];
    int addValue = (lineByte & (1 << (7 - noY))) != 0 ? 256 : 0;
    return addValue + screenData[index];
  }

  public static void setBigTileToScreen(int[] screenData, int index, int value)
  {
    bool hiPart = value > 0xFF;
    int noY = index % 8;
    int noX = index / 8;
    int lineByte = screenData[0x40 + noX];
    int mask = 1 << (7 - noY);
    if (hiPart)
        lineByte |= mask;
    else
        lineByte &= ~mask;
    screenData[index] = (byte)value;
    screenData[0x40 + noX] = (byte)lineByte;
  }
  
  public static ObjRec[] getBlocks(int tileId)
  {
    return Utils.readBlocksFromAlignedArrays(Globals.romdata, ConfigScript.getTilesAddr(tileId), ConfigScript.getBlocksCount(), true);
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocks)
  {
    Utils.writeBlocksToAlignedArrays(blocks, Globals.romdata, ConfigScript.getTilesAddr(tileId), ConfigScript.getBlocksCount(), true, true);
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