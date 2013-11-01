using CadEditor;
using System.Collections.Generic;
using System.IO;
using System;
using System.Windows.Forms;

public static class TinyToonUtils
{
  //--------------------------------------------------------------------------------------------------------------
  public static ObjRec[] getBlocks(int blockIndex)
  {
    return readBlocksFromAlignedArraysTT(Globals.romdata, Globals.getTilesAddr(blockIndex), getBlocksCount());
  }
  
  public static void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
    writeBlocksToAlignedArraysTT(blocksData, Globals.romdata, Globals.getTilesAddr(blockIndex), getBlocksCount());
  }
  
  //--------------------------------------------------------------------------------------------------------------
  static int getBlocksCount()
  {
    return 256;
  }
  
  static ObjRec[] readBlocksFromAlignedArraysTT(byte[] romdata, int addr, int count)
  {
      var objects = new ObjRec[count];
      byte c1, c2, c3, c4, typeColor;
      for (int i = 0; i < count; i++)
      {
          c1 = romdata[addr + i];
          c3 = romdata[addr + count*1 + i]; //tt version
          c2 = romdata[addr + count*2 + i];
          c4 = romdata[addr + count*3 + i];
          typeColor = 0;
          objects[i] = new ObjRec(c1, c2, c3, c4, typeColor);
      }
      return objects;
  }
  
  static void writeBlocksToAlignedArraysTT(ObjRec[] objects, byte[] romdata, int addr, int count)
  {
    for (int i = 0; i < count; i++)
    {
      var obj = objects[i];
      romdata[addr + i] = obj.c1;
      romdata[addr + count * 1 + i] = obj.c3;
      romdata[addr + count * 2 + i] = obj.c2;
      romdata[addr + count * 3 + i] = obj.c4;
    }
  }
  //--------------------------------------------------------------------------------------------------------------
}