using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0xC190, 64 , 16*15, 16, 15);   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xf610, 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 256; }
  public int getPalBytesAddr()          { return 0xfe10; }
  
  public GetBlocksFunc        getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  //----------------------------------------------------------------------------
  public static ObjRec[] getBlocks(int tileId)
  {
      int count = ConfigScript.getBlocksCount(tileId);
      var bb = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(tileId), 2, 2, count, false, false);
      var palAddr = ConfigScript.getPalBytesAddr(tileId);
      for (int i = 0; i < count; i++)
      {
          bb[i].palBytes[0] = Globals.romdata[palAddr + i] & 0x3; //get only pal, not physics
      }
      return bb;
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocksData)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount(tileId);
    var palAddr = ConfigScript.getPalBytesAddr(tileId);
    Utils.writeBlocksLinear(blocksData, Globals.romdata, addr, count, false, false);
    for (int i = 0; i < count; i++)
    {
        int t = Globals.romdata[palAddr + i];
        t =  t &  0xFC | blocksData[i].palBytes[0];
        Globals.romdata[palAddr + i] = (byte)t; //save only pal bits, not physics
    }
  }
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal3.bin");
  }
  
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr3.bin", videoPageId);
  }
}