using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x100f, 24, 16*11); }
  public int getScreenWidth()         { return 16; }
  public int getScreenHeight()        { return 11; }
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xfa82, 1  , 0x1000);  }
  public int getBlocksCount()           { return 176; }
  public int getBigBlocksCount()        { return 176; }
  public int getPalBytesAddr()          { return 0xf2f; }
  
  public GetBlocksFunc        getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  //----------------------------------------------------------------------------
  public ObjRec[] getBlocks(int tileId)
  {
      var bb = Utils.readBlocksFromAlignedArrays(Globals.romdata, ConfigScript.getTilesAddr(tileId), ConfigScript.getBlocksCount(), false);
      int count = ConfigScript.getBlocksCount();
      var palAddr = ConfigScript.getPalBytesAddr();
      for (int i = 0; i < count; i++)
      {
          bb[i].palBytes[0] = Globals.romdata[palAddr + i] & 0x3; //get only pal, not physics
      }
      return bb;
  }
  
  public void setBlocks(int tileId, ObjRec[] blocksData)
  {
    Utils.writeBlocksToAlignedArrays(blocksData, Globals.romdata, ConfigScript.getTilesAddr(tileId), ConfigScript.getBlocksCount(), false, false);
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount();
    var palAddr = ConfigScript.getPalBytesAddr();
    for (int i = 0; i < count; i++)
    {
        int t = Globals.romdata[palAddr + i];
        t =  t &  0xFC | blocksData[i].palBytes[0];
        Globals.romdata[palAddr + i] = (byte)t; //save only pal bits, not physics
    }
  }
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal.bin");
  }
  
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr.bin", videoPageId);
  }
}