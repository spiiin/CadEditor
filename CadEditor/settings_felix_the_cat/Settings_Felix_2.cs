using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x0, 1 , 24*256*3);   }
  public int getScreenWidth()          { return 24; }
  public int getScreenHeight()         { return 256*3; }
  public bool getScreenVertical()      { return true;   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(  0x16dfb, 1  , 0x1000);  }
  public int getBlocksCount()           { return 128; }
  public int getBigBlocksCount()        { return 128; }
  
  public GetBlocksFunc        getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  //----------------------------------------------------------------------------
  public static ObjRec[] getBlocks(int tileId)
  {
      int count = ConfigScript.getBlocksCount();
      var bb = Utils.readBlocksFromAlignedArrays(Globals.romdata, ConfigScript.getTilesAddr(tileId), count);
      for (int i = 0; i < count; i++)
      {
          bb[i].palBytes[0] = bb[i].palBytes[0] >> 6;
      }
      return bb;
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocksData)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount();
    var palAddr = addr + count * 4;
    for (int i = 0; i < count; i++)
    {
        var obj = blocksData[i];
        Globals.romdata[addr + i] = (byte)obj.indexes[0];
        Globals.romdata[addr + count * 1 + i] = (byte)obj.indexes[1];
        Globals.romdata[addr + count * 2 + i] = (byte)obj.indexes[2];
        Globals.romdata[addr + count * 3 + i] = (byte)obj.indexes[3];
        int t = Globals.romdata[palAddr + i];
        t =  t &  0x3F | (blocksData[i].palBytes[0]<<6);
        Globals.romdata[palAddr + i] = (byte)t; //save only pal bits, not physics
    }
  }
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal2.bin");
  }
  
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr2.bin", videoPageId);
  }
}