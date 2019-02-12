using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x43D0, 17, 64, 8, 8); }
  public bool getScreenVertical()     { return true; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0, 1 , 0x1000);  }
  public int getBlocksCount()           { return 64; }
  
  public GetBigBlocksFunc     getBigBlocksFunc() { return getBigBlocks;}
  public SetBigBlocksFunc     setBigBlocksFunc() { return setBigBlocks;}
  public GetBlocksFunc        getBlocksFunc() { return getBlocksConsts;}
  public SetBlocksFunc        setBlocksFunc() { return null;}
  public GetPalFunc           getPalFunc()    { return getPallete;}
  public SetPalFunc           setPalFunc()    { return null;}
  
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x4010, 1 , 0x4000); }
  public int getPalBytesAddr()          { return 0x4310; }
  public int getBigBlocksCount() { return 192; }
  
  //----------------------------------------------------------------------------
  public ObjRec[] getBlocksConsts(int blockIndex)
  {
        var objects = new ObjRec[getBlocksCount()];
        for (int i = 0; i < objects.Length; i++)
        {
            var indexes  = new int[4];
            var palBytes = new int[1];
            int bi = i*4;
            indexes[0] = bi + 0;
            indexes[2] = bi + 1;
            indexes[1] = bi + 2;
            indexes[3] = bi + 3;
            objects[i] = new ObjRec(2, 2, 0, indexes, palBytes);
        }
        return objects;
  }
  
  public BigBlock[] getBigBlocks(int bigTileIndex)
  {
    var data = Utils.readLinearBigBlockData(0, bigTileIndex);
    var bb = Utils.unlinearizeBigBlocks<BigBlockWithPal>(data, 2, 2);
    for (int i = 0; i < bb.Length; i++)
    {
      var b = bb[i];
      for (int ii = 0; ii < b.indexes.Length; ii++)
      {
        b.indexes[ii] = b.indexes[ii] & 0x3F;
      }
      Utils.swap(ref b.indexes[1], ref b.indexes[2]);
      
      int palByte = Globals.romdata[getPalBytesAddr() + i];
      b.palBytes[0] = palByte >> 0 & 0x3;
      b.palBytes[1] = palByte >> 2 & 0x3;
      b.palBytes[2] = palByte >> 4 & 0x3;
      b.palBytes[3] = palByte >> 6 & 0x3;
    }
    return bb;
  }
  
  public void setBigBlocks(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
    var bigBlocksAddr = ConfigScript.getBigTilesAddr(0, bigTileIndex);
    var data = Utils.linearizeBigBlocks(bigBlockIndexes);
    
    int size = data.Length;
    int addr = ConfigScript.getBigTilesAddr(0, bigTileIndex);
    for (int i = 0; i < size/4; i++)
    {
        Globals.romdata[addr + i*4 + 0] = (byte)((Globals.romdata[addr + i*4 + 0]&0xC0) | data[i*4+0]);
        Globals.romdata[addr + i*4 + 1] = (byte)((Globals.romdata[addr + i*4 + 1]&0xC0) | data[i*4+2]);
        Globals.romdata[addr + i*4 + 2] = (byte)((Globals.romdata[addr + i*4 + 2]&0xC0) | data[i*4+1]);
        Globals.romdata[addr + i*4 + 3] = (byte)((Globals.romdata[addr + i*4 + 3]&0xC0) | data[i*4+3]);
    }
    //save pal bytes
    for (int i = 0; i < bigBlockIndexes.Length; i++)
    {
      var bb = bigBlockIndexes[i] as BigBlockWithPal;
      int palByte = bb.palBytes[0] | bb.palBytes[1] << 2 | bb.palBytes[2]<<4 | bb.palBytes[3]<< 6;
      Globals.romdata[getPalBytesAddr() + i] = (byte)palByte;
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