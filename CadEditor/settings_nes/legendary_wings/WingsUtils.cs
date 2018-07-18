using CadEditor;
using System;
using System.Collections.Generic;

public static class WingsUtils 
{
  public static ObjRec[] getBlocks(int tileId)
  {
      var blocks = new ObjRec[64];
      for (int i = 0; i < 64; i++)
      {
          blocks[i] = new ObjRec(i*4, i*4+2, i*4+1, i*4+3, 0, 0);
      }
      return blocks;
  }
  
  public static BigBlock[] getBigBlocks(int bigTileIndex)
  {
      int count = ConfigScript.getBigBlocksCount(0);
      var bigBlocks =  new BigBlockWithPal[count];
      var bigBlocksAddr = ConfigScript.getBigTilesAddr(0, bigTileIndex);
      for (int i = 0; i < count; i++)
      {
         var bb = new BigBlockWithPal(2, 2);
         //physics in hi bits
         bb.indexes[0]  = Globals.romdata[bigBlocksAddr + i * 5 + 0] & 0x3f;
         bb.indexes[1]  = Globals.romdata[bigBlocksAddr + i * 5 + 1] & 0x3f;
         bb.indexes[2]  = Globals.romdata[bigBlocksAddr + i * 5 + 2] & 0x3f;
         bb.indexes[3]  = Globals.romdata[bigBlocksAddr + i * 5 + 3] & 0x3f;
         int palByte = Globals.romdata[bigBlocksAddr + i * 5 + 4];
         bb.palBytes[0] = palByte >> 0 & 0x3;
         bb.palBytes[1] = palByte >> 2 & 0x3;
         bb.palBytes[2] = palByte >> 4 & 0x3;
         bb.palBytes[3] = palByte >> 6 & 0x3;
         bigBlocks[i] = bb;
      }
      return bigBlocks;
  }
  
  public static void writeMaskedValue(int address, int block)
  {
      int masked = Globals.romdata[address] & 0xC0;
      Globals.romdata[address] = (byte)(masked | (block&0x3F));
  }
  
  public static void setBigBlocks(int bigTileIndex, BigBlock[] bigBlocks)
  {
      var bigBlocksAddr = ConfigScript.getBigTilesAddr(0, bigTileIndex); 
      for (int i = 0; i < bigBlocks.Length; i++)
      {
         var bb = bigBlocks[i] as BigBlockWithPal;
         writeMaskedValue(bigBlocksAddr + i * 5 + 0, bb.indexes[0]);
         writeMaskedValue(bigBlocksAddr + i * 5 + 1, bb.indexes[1]);
         writeMaskedValue(bigBlocksAddr + i * 5 + 2, bb.indexes[2]);
         writeMaskedValue(bigBlocksAddr + i * 5 + 3, bb.indexes[3]);
         int palByte = bb.palBytes[0] | bb.palBytes[1] << 2 | bb.palBytes[2]<<4 | bb.palBytes[3]<< 6;
         Globals.romdata[bigBlocksAddr + i * 5 + 4] = (byte)palByte;
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