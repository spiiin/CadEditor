using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x13779   , 19 , 3*2);   }
  public int getScreenWidth()          { return 2; }
  public int getScreenHeight()         { return 3; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  //
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x21010, 1  , 0x1000); }
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public GetPalFunc getPalFunc() { return getPallete;}
  public SetPalFunc setPalFunc() { return null;}
  //
  public OffsetRec getBlocksOffset() { return new OffsetRec(0x13117, 1  , 0x4000); }
  public int getBlocksCount() { return 124; }
  public GetBlocksFunc getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc setBlocksFunc() { return setBlocks;}
  //
  public int getBigBlocksHierarchyCount() { return 3; }
  public OffsetRec getBigBlocksOffsetHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return new OffsetRec(0x13383, 1  , 0x4000); }
    if (hierarchyLevel == 1) { return new OffsetRec(0x13547, 1  , 0x4000); }
    if (hierarchyLevel == 2) { return new OffsetRec(0x136E3, 1  , 0x4000); }
    return new OffsetRec(0x0, 1  , 0x4000);
  }
  
  public int getBigBlocksCountHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return 187; }
    if (hierarchyLevel == 1) { return 103; }
    if (hierarchyLevel == 2) { return 75; }
    return 256;
  }
  
  public GetBigBlocksFunc[] getBigBlocksFuncs() 
  { 
    return new GetBigBlocksFunc[] { getBigBlocks0, getBigBlocks1, getBigBlocks2 };
  }
  public SetBigBlocksFunc[] setBigBlocksFuncs()
  { 
     return new SetBigBlocksFunc[] { setBigBlocks0, setBigBlocks1, setBigBlocks2 };
  }
  
  //-------------------------------------------------------------------------------------------------------------------
  public ObjRec[] getBlocks(int blockIndex)
  {
    return Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), getBlocksCount());
  }
  
  public void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
    Utils.writeBlocksLinear(blocksData, Globals.romdata, ConfigScript.getTilesAddr(blockIndex), getBlocksCount());
  }
  
  //-------------------------------------------------------------------------------------------------------------------
  public static BigBlock[] getBigBlocks0(int bigTileIndex)
  {
    var data = Utils.readLinearBigBlockData(0, bigTileIndex, 4);
    return Utils.unlinearizeBigBlocks(data, 1, 4);
  }
  
  public static BigBlock[] getBigBlocks1(int bigTileIndex)
  {
    var data = Utils.readLinearBigBlockData(1, bigTileIndex, 4);
    return Utils.unlinearizeBigBlocks(data, 4, 1);
  }
  
  public static BigBlock[] getBigBlocks2(int bigTileIndex)
  {
    var data = Utils.readLinearBigBlockData(2, bigTileIndex, 2);
    return Utils.unlinearizeBigBlocks(data, 2, 1);
  }

  public static void setBigBlocks0(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
    var data = Utils.linearizeBigBlocks(bigBlockIndexes);
    Utils.writeLinearBigBlockData(0, bigTileIndex, data);
  }
  
  public static void setBigBlocks1(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
    var data = Utils.linearizeBigBlocks(bigBlockIndexes);
    Utils.writeLinearBigBlockData(1, bigTileIndex, data);
  }
  
  public static void setBigBlocks2(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
    var data = Utils.linearizeBigBlocks(bigBlockIndexes);
    Utils.writeLinearBigBlockData(2, bigTileIndex, data);
  }
  
  public byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x16, 0x28, 0x30, 0x0f, 0x0a, 0x07, 0x17,
      0x0f, 0x07, 0x00, 0x10, 0x0f, 0x02, 0x12, 0x32,
    }; 
    return pallete;
  }
}