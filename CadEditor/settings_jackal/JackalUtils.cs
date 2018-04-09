using CadEditor;
using System;
public static class JackalUtils 
{
  public static ObjRec vertMirror(ObjRec obj)
  {
     var ind = new int[16];
     
     ind[0] = obj.indexes[12];
     ind[1] = obj.indexes[13];
     ind[2] = obj.indexes[14];
     ind[3] = obj.indexes[15];
     
     ind[4] = obj.indexes[8];
     ind[5] = obj.indexes[9];
     ind[6] = obj.indexes[10];
     ind[7] = obj.indexes[11];
     
     ind[8] = obj.indexes[4];
     ind[9] = obj.indexes[5];
     ind[10] = obj.indexes[6];
     ind[11] = obj.indexes[7];
     
     ind[12] = obj.indexes[0];
     ind[13] = obj.indexes[1];
     ind[14] = obj.indexes[2];
     ind[15] = obj.indexes[2];
     
     return new ObjRec(4, 4, obj.type, ind, obj.palBytes);
  }
  
  public static ObjRec[] getBlocksFromTiles16Pal1(int blockIndex)
  {
      int tileAddr = ConfigScript.getTilesAddr(blockIndex);
      var bb = Utils.readBlocksLinearTiles16Pal1(Globals.romdata, tileAddr, ConfigScript.getPalBytesAddr(), ConfigScript.getBlocksCount());
      for (int i = 0; i < bb.Length; i++)
      {
        bb[i] = vertMirror(bb[i]);
      }
      return bb;
  }

  public static void setBlocksFromTiles16Pal1(int blockIndex, ObjRec[] blocksData)
  {
    int tileAddr = ConfigScript.getTilesAddr(blockIndex);
    for (int i = 0; i < blocksData.Length; i++)
    {
      blocksData[i] = vertMirror(blocksData[i]); //TODO: remove inplace changes
    }
    Utils.writeBlocksLinearTiles16Pal1(blocksData, Globals.romdata,tileAddr, ConfigScript.getPalBytesAddr(), ConfigScript.getBlocksCount());
  }
  
  public static int getBigTileNoFromScreen(int[] screenData, int index)
  {
    int w = ConfigScript.getScreenWidth(0);
    int noY = index / w;
    noY = (noY/8)*8 + 7 - (noY%8);
    int noX = index % w;
    return screenData[noY*w + noX];
  }

  public static void setBigTileToScreen(int[] screenData, int index, int value)
  {
    int w = ConfigScript.getScreenWidth(0);
    int noY = index / w;
    noY = (noY/8)*8 + 7 - (noY%8);
    int noX = index % w;
    screenData[noY*w + noX] = value;
  }
}