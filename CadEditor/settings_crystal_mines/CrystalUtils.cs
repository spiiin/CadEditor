using CadEditor;
using System;

public static class CrystalUtils
{
  public static int blockAddr1 = 0x432;
  public static int blockAddr2 = 0x51b;
  public static int blockAddr3 = 0x4dd;
  public static int blockAddr4 = 0x559;
  public static int getPalBytesAddr()          { return 0x597; }
  
  //----------------------------------------------------------------------------
  public static ObjRec[] getBlocks(int tileId)
  {
      int bc = ConfigScript.getBlocksCount();
      int palAddr = getPalBytesAddr();
      byte[] blocksData = Utils.readDataFromUnalignedArrays(Globals.romdata, blockAddr1, blockAddr2, blockAddr3, blockAddr4, bc);
      var blocks = new ObjRec[bc];
      for (int i = 0; i < blocks.Length; i++)
      {
          var tileData = new int[] {blocksData[i*4+0], blocksData[i*4+1], blocksData[i*4+2], blocksData[i*4+3]};
          var attrData = new int[] {Globals.romdata[palAddr + i]};
          blocks[i] = new ObjRec(2, 2, 0, tileData, attrData);
      }
      return blocks;
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocks)
  {
      int bc = ConfigScript.getBlocksCount();
      int palAddr = getPalBytesAddr();
      byte[] blocksData = new byte[blocks.Length*4];
      for (int i = 0; i < blocks.Length; i++)
      {
          blocksData[i*4+0] = (byte)blocks[i].indexes[0];
          blocksData[i*4+1] = (byte)blocks[i].indexes[1];
          blocksData[i*4+2] = (byte)blocks[i].indexes[2];
          blocksData[i*4+3] = (byte)blocks[i].indexes[3];
      }
      Utils.writeDataToUnalignedArrays(blocksData, Globals.romdata, blockAddr1, blockAddr2, blockAddr3, blockAddr4, bc);
      for (int i = 0; i < blocks.Length; i++)
      {
          Globals.romdata[palAddr+i] = (byte)blocks[i].palBytes[0];
      }
  }
  
  public static byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal1.bin");
  }
  
  public static int getVideoAddress(int id)
  {
    return -1;
  }
  
  public static byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr1.bin", videoPageId);
  }
}