using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  //public OffsetRec getScreensOffset()  { return new OffsetRec(0x1a70, 50 , 11*13, 11, 13);   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x19cc, 1  , 0x1000);  }
  public int getBlocksCount()           { return 16; }
  public int getBigBlocksCount()        { return 16; }
  public int getPalBytesAddr()          { return 0x19bc; }
  
  public GetBlocksFunc        getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}

  public int getLevelsCount()           { return 50; }

  public OffsetRec[] getScreensOffsetsForLevels() {
    var ans = new OffsetRec[] {
      new OffsetRec(0x250d  , 1  , 11*13, 11, 13),
      new OffsetRec(0x3304  , 1  , 11*13, 11, 13),
      new OffsetRec(0x2dfd  , 1  , 11*13, 11, 13),
      new OffsetRec(0x3540  , 1  , 11*13, 11, 13),
      new OffsetRec(0x259c  , 1  , 11*13, 11, 13),
      new OffsetRec(0x2cdf  , 1  , 11*13, 11, 13),
      new OffsetRec(0x30c8  , 1  , 11*13, 11, 13),
      new OffsetRec(0x35cf  , 1  , 11*13, 11, 13),
      new OffsetRec(0x2b32  , 1  , 11*13, 11, 13),
      new OffsetRec(0x2d6e  , 1  , 11*13, 11, 13),

      new OffsetRec(0x2faa  , 1  , 11*13, 11, 13),
      new OffsetRec(0x2749  , 1  , 11*13, 11, 13),
      new OffsetRec(0x2f1b  , 1  , 11*13, 11, 13),
      new OffsetRec(0x2c50  , 1  , 11*13, 11, 13),
      new OffsetRec(0x2bc1  , 1  , 11*13, 11, 13),
      new OffsetRec(0x262b  , 1  , 11*13, 11, 13),
      new OffsetRec(0x2aa3  , 1  , 11*13, 11, 13),
      new OffsetRec(0x1e59  , 1  , 11*13, 11, 13),
      new OffsetRec(0x27d8  , 1  , 11*13, 11, 13),
      new OffsetRec(0x1cac  , 1  , 11*13, 11, 13),

      new OffsetRec(0x2867  , 1  , 11*13, 11, 13),
      new OffsetRec(0x3393  , 1  , 11*13, 11, 13),
      new OffsetRec(0x3157  , 1  , 11*13, 11, 13),
      new OffsetRec(0x2985  , 1  , 11*13, 11, 13),
      new OffsetRec(0x31e6  , 1  , 11*13, 11, 13),
      new OffsetRec(0x3275  , 1  , 11*13, 11, 13),
      new OffsetRec(0x21b3  , 1  , 11*13, 11, 13),
      new OffsetRec(0x1f77  , 1  , 11*13, 11, 13),
      new OffsetRec(0x2242  , 1  , 11*13, 11, 13),
      new OffsetRec(0x3422  , 1  , 11*13, 11, 13),

      new OffsetRec(0x2e8c  , 1  , 11*13, 11, 13),
      new OffsetRec(0x1d3b  , 1  , 11*13, 11, 13),
      new OffsetRec(0x2124  , 1  , 11*13, 11, 13),
      new OffsetRec(0x2a14  , 1  , 11*13, 11, 13),
      new OffsetRec(0x1b8e  , 1  , 11*13, 11, 13),
      new OffsetRec(0x247e  , 1  , 11*13, 11, 13),
      new OffsetRec(0x34b1  , 1  , 11*13, 11, 13),
      new OffsetRec(0x3039  , 1  , 11*13, 11, 13),
      new OffsetRec(0x1ee8  , 1  , 11*13, 11, 13),
      new OffsetRec(0x2360  , 1  , 11*13, 11, 13),

      new OffsetRec(0x2095  , 1  , 11*13, 11, 13),
      new OffsetRec(0x2006  , 1  , 11*13, 11, 13),
      new OffsetRec(0x1a70  , 1  , 11*13, 11, 13),
      new OffsetRec(0x1dca  , 1  , 11*13, 11, 13),
      new OffsetRec(0x22d1  , 1  , 11*13, 11, 13),
      new OffsetRec(0x28f6  , 1  , 11*13, 11, 13),
      new OffsetRec(0x26ba  , 1  , 11*13, 11, 13),
      new OffsetRec(0x1c1d  , 1  , 11*13, 11, 13),
      new OffsetRec(0x1aff  , 1  , 11*13, 11, 13),
      new OffsetRec(0x23ef  , 1  , 11*13, 11, 13),
    };
    return ans;  
  }
  //----------------------------------------------------------------------------
  public static ObjRec[] getBlocks(int tileId)
  {
      int count = ConfigScript.getBlocksCount();
      var bb = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(tileId), 2, 2, count, false, false);
      var palAddr = ConfigScript.getPalBytesAddr();
      for (int i = 0; i < count; i++)
      {
          bb[i].palBytes[0] = Globals.romdata[palAddr + i] & 0x3; //get only pal, not physics
      }
      return bb;
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocksData)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount();
    var palAddr = ConfigScript.getPalBytesAddr();
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