using CadEditor;
using System;

public class Data 
{

  public OffsetRec getVideoOffset()    { return new OffsetRec(0x34010, 1, 0x1000); }
  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x6210, 1, 128*8); }
  public int getScreenWidth()          { return 8; }
  public int getScreenHeight()         { return 128; }
  public bool getScreenVertical()      { return true; }

  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }

  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }

  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x6c10, 1, 0x1000); }
  public int getBlocksCount()           { return 93; }
  
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x7390, 1, 0x1000); }
  public int getBigBlocksCount()        { return 256; }

  public GetBlocksFunc        getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocks;}
  
  public GetBigBlocksFunc     getBigBlocksFunc()     { return getBigBlocks;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return setBigBlocks;}
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public static ObjRec[] getBlocks(int tileId)
  {
      int addr = ConfigScript.getTilesAddr(tileId);
      int count = ConfigScript.getBlocksCount();
      var blocks = Utils.readBlocksLinear(Globals.romdata, addr, 2, 2, count, true, true);
      return blocks;
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocksData)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount();
    Utils.writeBlocksLinear(blocksData, Globals.romdata, addr, count, true, true);
  }
  
  public BigBlock[] getBigBlocks(int bigTileIndex)
  {
      int count = ConfigScript.getBigBlocksCount(0);
      var bigBlocks =  new BigBlockWithPal[count];
      var bigBlocksAddr = ConfigScript.getBigTilesAddr(0, bigTileIndex);
      for (int i = 0; i < count; i++)
      {
         var bb = new BigBlockWithPal(2, 2);
         bb.indexes[0]  = Globals.romdata[bigBlocksAddr + i * 5 + 0] & 0x7f;
         bb.indexes[2]  = Globals.romdata[bigBlocksAddr + i * 5 + 1] & 0x7f;
         bb.indexes[1]  = Globals.romdata[bigBlocksAddr + i * 5 + 2] & 0x7f;
         bb.indexes[3]  = Globals.romdata[bigBlocksAddr + i * 5 + 3] & 0x7f;
         int palByte = Globals.romdata[bigBlocksAddr + i * 5 + 4];
         bb.palBytes[0] = palByte >> 0 & 0x3;
         bb.palBytes[1] = palByte >> 2 & 0x3;
         bb.palBytes[2] = palByte >> 4 & 0x3;
         bb.palBytes[3] = palByte >> 6 & 0x3;
         bigBlocks[i] = bb;
      }
      return bigBlocks;
  }
  
  public void setBigBlocks(int bigTileIndex, BigBlock[] bigBlocks)
  {
      var bigBlocksAddr = ConfigScript.getBigTilesAddr(0, bigTileIndex); 
      for (int i = 0; i < bigBlocks.Length; i++)
      {
         var bb = bigBlocks[i] as BigBlockWithPal;
         Globals.romdata[bigBlocksAddr + i * 5 + 0] = (byte)bb.indexes[0];
         Globals.romdata[bigBlocksAddr + i * 5 + 1] = (byte)bb.indexes[2];
         Globals.romdata[bigBlocksAddr + i * 5 + 2] = (byte)bb.indexes[1];
         Globals.romdata[bigBlocksAddr + i * 5 + 3] = (byte)bb.indexes[3];
         int palByte = bb.palBytes[0] | bb.palBytes[1] << 2 | bb.palBytes[2]<<4 | bb.palBytes[3]<< 6;
         Globals.romdata[bigBlocksAddr + i * 5 + 4] = (byte)palByte;
      }
  }



  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal1.bin");
  }
  
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr1.bin", videoPageId);
  }
}
