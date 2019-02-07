using CadEditor;
using System;
using System.Collections.Generic;
//css_include shared_settings/BlockUtils.cs;

public class Data 
{

  public OffsetRec getVideoOffset()     { return new OffsetRec(0x20010, 16, 0x1000); }
    
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x94, 29 , 8*8, 8, 8);   }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x165c, 1  , 0x1000);  }
  public int getBlocksCount()           { return 211; }
  
  public OffsetRec getBigBlocksOffset()    { return new OffsetRec(0x7d4 , 1  , 0x1000);  }
  public int getBigBlocksCount()           { return 256; }
  
  public int getPalBytesAddr()             { return 0x19A8; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public GetBlocksFunc        getBlocksFunc()        { return BlockUtils.getBlocksLinear2x2;}
  public SetBlocksFunc        setBlocksFunc()        { return BlockUtils.setBlocksLinear2x2;}
  
  public GetBigBlocksFunc     getBigBlocksFunc()     { return getBigBlocksTT;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return setBigBlocksTT;}
  
  //----------------------------------------------------------------------------
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr1.bin", videoPageId);
  }
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal1.bin");
  }
  
  public ObjRec[] getBlocks(int blockIndex)
  {
    return Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), 2, 2, ConfigScript.getBlocksCount(blockIndex), false, false);
  }
  
  public void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
    Utils.writeBlocksLinear(blocksData, Globals.romdata, ConfigScript.getTilesAddr(blockIndex), ConfigScript.getBlocksCount(blockIndex), false, false);
  }
  
  public BigBlock[] getBigBlocksTT(int bigTileIndex)
  {
    var data = Utils.readLinearBigBlockData(0, bigTileIndex, 4);
    var bb = Utils.unlinearizeBigBlocks<BigBlock>(data, 2, 2);
    for (int i = 0; i < bb.Length; i++)
    {
      var b = bb[i];
      var i1 = b.indexes[1];
      var i2 = b.indexes[2];
      b.indexes[2] = i2;
      b.indexes[1] = i1;
    }
    return bb;
  }
  
  public void setBigBlocksTT(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
      var bigBlocksAddr = ConfigScript.getBigTilesAddr(0, bigTileIndex); 
      for (int v = 0; v < bigBlockIndexes.Length; v++)
      {
          var bb = bigBlockIndexes[v] as BigBlockWithPal;
          var i0 = bb.indexes[0];
          var i1 = bb.indexes[1];
          var i2 = bb.indexes[2];
          var i3 = bb.indexes[3];
          Globals.romdata[bigBlocksAddr + v * 4 + 0] = (byte)i0;
          Globals.romdata[bigBlocksAddr + v * 4 + 1] = (byte)i2;
          Globals.romdata[bigBlocksAddr + v * 4 + 2] = (byte)i1;
          Globals.romdata[bigBlocksAddr + v * 4 + 3] = (byte)i3;
      }
  }
}