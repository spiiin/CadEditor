using CadEditor;
using System;

public class IndyUtils 
{ 
  public static ObjRec[] getBlocksFromTiles16Pal1V(int blockIndex)
  {
      return readBlocksLinearTiles16Pal1V(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), ConfigScript.getPalBytesAddr(blockIndex), ConfigScript.getBlocksCount(blockIndex));
  }

  public static void setBlocksFromTiles16Pal1V(int blockIndex, ObjRec[] blocksData)
  {
      writeBlocksLinearTiles16Pal1V(blocksData, Globals.romdata, ConfigScript.getTilesAddr(blockIndex), ConfigScript.getPalBytesAddr(blockIndex), ConfigScript.getBlocksCount(blockIndex));
  }
  
  public static ObjRec[] readBlocksLinearTiles16Pal1V(byte[] romdata, int addr, int palBytesAddr, int count)
  {
      int BLOCK_W = 4;
      int BLOCK_H = 4;
      var objects = Utils.readBlocksLinear(romdata, addr, BLOCK_W, BLOCK_H, count, false, true);
      for (int i = 0; i < count; i++)
      {
          int palByte = romdata[palBytesAddr + i];
          var palBytes = new[] { (palByte >> 0) & 3, (palByte >> 2) & 3, (palByte >> 4) & 3, (palByte >> 6) & 3 };
          objects[i].palBytes = palBytes;
      }
      return objects;
  }

  public static void writeBlocksLinearTiles16Pal1V(ObjRec[] objects, byte[] romdata, int addr, int palBytesAddr, int count)
  {
      Utils.writeBlocksLinear(objects, romdata, addr, count, false, true);
      for (int i = 0; i < count; i++)
      {
          var objPalBytes = objects[i].palBytes;
          int palByte = objPalBytes[0] | objPalBytes[1] << 2 | objPalBytes[2] << 4 | objPalBytes[3] << 6;
          romdata[palBytesAddr + i] = (byte)palByte;
      }
  }
}