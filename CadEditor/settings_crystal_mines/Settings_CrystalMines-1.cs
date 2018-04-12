using CadEditor;
using System;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x859f, 1, 30*11); }
  public int getScreenWidth()         { return 30; }
  public int getScreenHeight()        { return 11; }
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public GetBlocksFunc        getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public int getBlocksCount()           { return 62; }
  public int getBigBlocksCount()        { return 62; }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x432, 1  , 0x1000);  }
  public int blockAddr1 = 0x432;
  public int blockAddr2 = 0x51b;
  public int blockAddr3 = 0x4dd;
  public int blockAddr4 = 0x559;
  public int getPalBytesAddr()          { return 0x597; }

  //----------------------------------------------------------------------------
  public ObjRec[] getBlocks(int tileId)
  {
      int bc = getBlocksCount();
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
  
  public void setBlocks(int tileId, ObjRec[] blocks)
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