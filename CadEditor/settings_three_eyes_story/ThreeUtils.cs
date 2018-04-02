using CadEditor;
using System;

public static class ThreeUtils
{
    public static ObjRec[] getBlocks(int tileId)
    {
      int addr = ConfigScript.getTilesAddr(tileId);
      var objects = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(tileId), 2, 2, ConfigScript.getBlocksCount(), false, false);
      for (int i = 0; i < objects.Length; i++)
      {
          objects[i].palBytes[0] = Globals.romdata[ConfigScript.getPalBytesAddr() + i];
      }
      return objects;
    }
    
    public static void setBlocks(int tileId, ObjRec[] blocks)
    {
      int addr = ConfigScript.getTilesAddr(tileId);
      int blocksCount = ConfigScript.getBlocksCount();
      Utils.writeBlocksLinear(blocks, Globals.romdata, addr, blocksCount, false, false);
      int palBytesAddr = ConfigScript.getPalBytesAddr();
      for (int i = 0; i < blocksCount; i++)
      {
          Globals.romdata[palBytesAddr + i] = (byte)blocks[i].palBytes[0];
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