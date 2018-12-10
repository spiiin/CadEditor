using CadEditor;
using System;

public class JamesUtils 
{ 
  
  public static ObjRec[] getBlocks(int tileId)
  {
      int count = ConfigScript.getBlocksCount(tileId);
      var bb = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(tileId), 2, 2, count, false, false);
      /*for (int i = 0; i < count; i++)
      {
          bb[i].palBytes[0] = bb[i].palBytes[0] >> 6;
      }*/
      return bb;
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocksData)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount(tileId);
    var palAddr = addr + count * 4;
    for (int i = 0; i < count; i++)
    {
        var obj = blocksData[i];
        Globals.romdata[addr + i*4 + 0] = (byte)obj.indexes[0];
        Globals.romdata[addr + i*4 + 1] = (byte)obj.indexes[1];
        Globals.romdata[addr + i*4 + 2] = (byte)obj.indexes[2];
        Globals.romdata[addr + i*4 + 3] = (byte)obj.indexes[3];
        /*int t = Globals.romdata[palAddr + i];
        t =  t &  0x3F | (blocksData[i].palBytes[0]<<6);
        Globals.romdata[palAddr + i] = (byte)t; //save only pal bits, not physics*/
    }
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