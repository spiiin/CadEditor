using CadEditor;
using System;

public static class Cheetah
{
  //----------------------------------------------------------------------------
  public static ObjRec[] getBlocks(int tileId)
  {
      int count = ConfigScript.getBlocksCount();
      var bb = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(tileId), 2, 2, count, false, false);
      var palAddr = ConfigScript.getPalBytesAddr();
      for (int i = 0; i < count; i++)
      {
          bb[i].palBytes[0] = (Globals.romdata[palAddr + i] & 0x0C)>>2; //get only pal, not physics
      }
      return bb;
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocksData)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount();
    var palAddr = ConfigScript.getPalBytesAddr();
    Utils.writeBlocksLinear(blocksData, Globals.romdata, addr, count, false, false);
    for (int i = 0; i < count; i++)
    {
        int t = Globals.romdata[palAddr + i];
        t =  t &  ~0x0C | (blocksData[i].palBytes[0] << 2);
        Globals.romdata[palAddr + i] = (byte)t; //save only pal bits, not physics
    }
  }
  //----------------------------------------------------------------------------
}