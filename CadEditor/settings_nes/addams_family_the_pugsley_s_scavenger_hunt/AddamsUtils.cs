using CadEditor;
using System;

public static class AddamsUtils 
{
  public static ObjRec[] getBlocks(int tileId)
  {
      int addr = ConfigScript.getTilesAddr(tileId);
      int count = ConfigScript.getBlocksCount(tileId);
      var blocks = Utils.readBlocksLinear(Globals.romdata, addr, 2, 2, count, false);
      for (int i = 0; i < blocks.Length; i++)
      {
        blocks[i].palBytes[0] = (Globals.romdata[ConfigScript.getPalBytesAddr(tileId)+i] >> 4) & 0x3;
      }
      return blocks;
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocksData)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount(tileId);
    Utils.writeBlocksLinear(blocksData, Globals.romdata, addr, count, false);
    for (int i = 0; i < blocksData.Length; i++)
    {
      var oldValue = Globals.romdata[ConfigScript.getPalBytesAddr(tileId)+i];
      //set only 2 last bits.
      //really, game uses first nibble as pallette numbers for blocks on odd positions, 
      // and second nibbles  for blocks on even positions, so there are two variants of blocks exists
      Globals.romdata[ConfigScript.getPalBytesAddr(tileId)+i] = (byte)((oldValue & 0xCF) | (blocksData[i].palBytes[0] << 4));
    }
  }
}