using CadEditor;
using System;

public static class BlockUtils
{ 
  //-----------------------------------------------------------------------------------------------------------------
  public static ObjRec[] getBlocksFromAlignedArrays(int tileId)
  {
      return Utils.readBlocksFromAlignedArrays(Globals.romdata, ConfigScript.getTilesAddr(tileId), ConfigScript.getBlocksCount(tileId), false);
  }
  
  public static void setBlocksToAlignedArrays(int tileId, ObjRec[] blocksData)
  {
      Utils.writeBlocksToAlignedArrays(blocksData, Globals.romdata, ConfigScript.getTilesAddr(tileId), ConfigScript.getBlocksCount(tileId), true, false);
  }
  
  //-----------------------------------------------------------------------------------------------------------------
  public static ObjRec[] getBlocksLinear2x2(int tileId)
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
  
  public static void setBlocksLinear2x2(int tileId, ObjRec[] blocksData)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount(tileId);
    var palAddr = ConfigScript.getPalBytesAddr(tileId);
    Utils.writeBlocksLinear(blocksData, Globals.romdata, addr, count, false, false);
    for (int i = 0; i < count; i++)
    {
        int t = blocksData[i].palBytes[0];
        Globals.romdata[palAddr + i] = (byte)t;
    }
  }
  
  //-----------------------------------------------------------------------------------------------------------------
  public static ObjRec[] getBlocksLinear2x2WithoutAttribs(int tileId)
  {
      int count = ConfigScript.getBlocksCount(tileId);
      var bb = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(tileId), 2, 2, count, false, false);
      return bb;
  }
  
  public static void setBlocksLinear2x2WithoutAttribs(int tileId, ObjRec[] blocksData)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount(tileId);
    Utils.writeBlocksLinear(blocksData, Globals.romdata, addr, count, false, false);
  }

  //-----------------------------------------------------------------------------------------------------------------
  public static ObjRec[] getBlocksLinear2x2Masked(int tileId)
  {
      int count = ConfigScript.getBlocksCount(tileId);
      var bb = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(tileId), 2, 2, count, false, false);
      var palAddr = ConfigScript.getPalBytesAddr(tileId);
      for (int i = 0; i < count; i++)
      {
          bb[i].palBytes[0] = Globals.romdata[palAddr + i] & 0x3; //get only pal, not physics
      }
      return bb;
  }
  
  public static void setBlocksLinear2x2Masked(int tileId, ObjRec[] blocksData)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount(tileId);
    var palAddr = ConfigScript.getPalBytesAddr(tileId);
    Utils.writeBlocksLinear(blocksData, Globals.romdata, addr, count, false, false);
    for (int i = 0; i < count; i++)
    {
        int t = Globals.romdata[palAddr + i];
        t =  t &  0xFC | blocksData[i].palBytes[0];
        Globals.romdata[palAddr + i] = (byte)t; //save only pal bits, not physics
    }
  }
  
  //-----------------------------------------------------------------------------------------------------------------
  public static ObjRec[] getBlocksLinear2x2MaskedTransposed(int tileId)
  {
      int count = ConfigScript.getBlocksCount(tileId);
      var bb = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(tileId), 2, 2, count, false, true);
      var palAddr = ConfigScript.getPalBytesAddr(tileId);
      for (int i = 0; i < count; i++)
      {
          bb[i].palBytes[0] = Globals.romdata[palAddr + i] & 0x3; //get only pal, not physics
      }
      return bb;
  }
  
  public static void setBlocksLinear2x2MaskedTransposed(int tileId, ObjRec[] blocksData)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount(tileId);
    var palAddr = ConfigScript.getPalBytesAddr(tileId);
    Utils.writeBlocksLinear(blocksData, Globals.romdata, addr, count, false, true);
    for (int i = 0; i < count; i++)
    {
        int t = Globals.romdata[palAddr + i];
        t =  t &  0xFC | blocksData[i].palBytes[0];
        Globals.romdata[palAddr + i] = (byte)t; //save only pal bits, not physics
    }
  }
  
  //-----------------------------------------------------------------------------------------------------------------
  public static ObjRec[] getBlocksAlignedWithSeparatePal(int tileId)
  {
    int count = ConfigScript.getBlocksCount(tileId);
    var bb = Utils.readBlocksFromAlignedArrays(Globals.romdata, ConfigScript.getTilesAddr(tileId), count, false);
    var palAddr = ConfigScript.getPalBytesAddr(tileId);
    for (int i = 0; i < count; i++)
    {
        bb[i].palBytes[0] = Globals.romdata[palAddr + i] & 0x3; //get only pal, not physics
    }
    return bb;
  }
  
  public static void setBlocksAlignedWithSeparatePal(int tileId, ObjRec[] blocks)
  {
    int count = ConfigScript.getBlocksCount(tileId);
    Utils.writeBlocksToAlignedArrays(blocks, Globals.romdata, ConfigScript.getTilesAddr(tileId), count, false, false);
    var palAddr = ConfigScript.getPalBytesAddr(tileId);
    for (int i = 0; i < count; i++)
    {
        Globals.romdata[palAddr + i] = (byte)blocks[i].palBytes[0];
    }
  }
}