using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x10, 7, 16*16, 16, 16); }
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x8010, 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 256; }
  public int getPalBytesAddr()          { return 0xa010; }
  
  public GetBlocksFunc        getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  //----------------------------------------------------------------------------
  public ObjRec[] getBlocks(int tileId)
  {
    var blocks = Utils.readBlocksFromAlignedArrays(Globals.romdata, ConfigScript.getTilesAddr(tileId), ConfigScript.getBlocksCount(), false);
    //decode palByte
    int palInfoCount = ConfigScript.getBlocksCount()/4;
    var palInfo = new byte[palInfoCount];
    for (int i = 0; i < palInfoCount; i++)
    {
        palInfo[i] = Globals.romdata[ConfigScript.getPalBytesAddr()+i];
    }
    for (int i = 0; i < blocks.Length; i++)
    {
        var palInfoByte = palInfo[i/4];
        int parByteNo = i % 4;
        blocks[i].palBytes[0] = (byte)((palInfoByte >> parByteNo*2) & 3);
    }
    return blocks;
  }
  
  public void setBlocks(int tileId, ObjRec[] blocks)
  {
    Utils.writeBlocksToAlignedArrays(blocks, Globals.romdata, ConfigScript.getTilesAddr(tileId), ConfigScript.getBlocksCount(), false, false);
    int palInfoCount = ConfigScript.getBlocksCount()/4;
    for (int i = 0; i < palInfoCount; i++)
    {
        var palInfoByte = 
          (blocks[i*4+0].palBytes[0]<<0) | 
          (blocks[i*4+1].palBytes[0]<<2) |
          (blocks[i*4+2].palBytes[0]<<4) | 
          (blocks[i*4+3].palBytes[0]<<6);
          
        Globals.romdata[ConfigScript.getPalBytesAddr() + i] = (byte)palInfoByte;
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