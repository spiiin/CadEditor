using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x13779   , 19 , 3*2);   }
  public int getScreenWidth()          { return 2; }
  public int getScreenHeight()         { return 3; }
  public string getBlocksFilename()    { return "rokin_cat_1.png"; }
  public int    getPictureBlocksWidth(){ return 64; }
  
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
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x13383, 1  , 0x4000); }
  public int getBigBlocksCount() { return 374; }
  public GetBigBlocksFunc getBigBlocksFunc() { return getBigBlocks;}
  public SetBigBlocksFunc setBigBlocksFunc() { return setBigBlocks;}
  
  //-------------------------------------------------------------------------------------------------------------------
  public ObjRec[] getBlocks(int blockIndex)
  {
    return Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), getBlocksCount());
  }
  
  public void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
    Utils.writeBlocksLinear(blocksData, Globals.romdata, ConfigScript.getTilesAddr(blockIndex), getBlocksCount());
  }
  
  public static BigBlock[] getBigBlocks(int bigTileIndex)
  {
    var data = Utils.readLinearBigBlockData(bigTileIndex, 2);
    return Utils.unlinearizeBigBlocks(data, 1, 2);
  }

  public static void setBigBlocks(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
    var data = Utils.linearizeBigBlocks(bigBlockIndexes);
    Utils.writeLinearBigBlockData(bigTileIndex, data);
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