using CadEditor;
using System.Collections.Generic;

public static class ShatterhandUtils 
{
  public static ObjRec[] getBlocks(int tileId)
  {
    var objects = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(tileId), 2, 2, ConfigScript.getBlocksCount(), false, true);
    int palAddr = ConfigScript.getPalBytesAddr();
    for (int i = 0; i < ConfigScript.getBlocksCount(); i++)
    {
        objects[i].palBytes[0] =  Globals.romdata[palAddr + i];
    }
    return objects;
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocks)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount();
    int palAddr = ConfigScript.getPalBytesAddr();
    Utils.writeBlocksLinear(blocks, Globals.romdata, addr, count, false, true);
    for (int i = 0; i < count; i++)
    {
        Globals.romdata[palAddr + i] = (byte)blocks[i].palBytes[0];
    }
  }
  
  public static BigBlock[] getBigBlocks(int bigTileIndex)
  {
      var bblocks = Utils.getBigBlocksCapcomDefault(bigTileIndex);
      for (int v = 0; v < bblocks.Length; v++)
      {
         var b = bblocks[v];
         int temp = b.indexes[1];
         b.indexes[1] = b.indexes[2];
         b.indexes[2] = temp;
      }
      return bblocks;
  }
  
  public static void setBigBlocks(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
      for (int v = 0; v < bigBlockIndexes.Length; v++)
      {
         var b = bigBlockIndexes[v];
         int temp = b.indexes[1];
         b.indexes[1] = b.indexes[2];
         b.indexes[2] = temp;
      }
      Utils.setBigBlocksCapcomDefault(bigTileIndex, bigBlockIndexes);
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
}