using CadEditor;
using System;

public static class DoraemonUtils 
{ 
  public static ObjRec[] getBlocks(int tileId)
  {
      int count = ConfigScript.getBlocksCount();
      var bb = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(tileId), 2, 2, count, false, false);
      var palAddr = ConfigScript.getPalBytesAddr();
      for (int i = 0; i < count; i++)
      {
          bb[i].palBytes[0] = Globals.romdata[palAddr + i];
      }
      return bb;
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocksData)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount();
    var palAddr = ConfigScript.getPalBytesAddr();
    Utils.writeBlocksLinear(blocksData, Globals.romdata, addr, count, false, false);
    int realBlocksCount = 128;
    for (int i = 0; i < realBlocksCount; i++)
    {
        Globals.romdata[palAddr + i] = (byte)blocksData[i].palBytes[0];
    }
  }
}