using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x000de, 10 , 8*6);   }
  public int getScreenWidth()          { return 8; }
  public int getScreenHeight()         { return 6; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x6be, 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public OffsetRec getBigBlocksOffset()    { return new OffsetRec(0x2BE, 1  , 0x1000);  }
  public int getBigBlocksCount()        { return 256; }
  public int getPalBytesAddr()          { return 0x6be + 128*4; }
  
  public GetBlocksFunc        getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocks;}
  public GetBigBlocksFunc     getBigBlocksFunc()     { return Utils.getBigBlocksCapcomDefault;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return Utils.setBigBlocksCapcomDefault;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  //----------------------------------------------------------------------------
  public ObjRec[] getBlocks(int tileId)
  {
      int count = ConfigScript.getBlocksCount();
      var bb = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(tileId), 2, 2, count, false, false);
      var palAddr = getPalBytesAddr();
      for (int i = 0; i < count; i++)
      {
          bb[i].palBytes[0] = Globals.romdata[palAddr + i];
      }
      return bb;
  }
  
  public void setBlocks(int tileId, ObjRec[] blocksData)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = getBlocksCount();
    var palAddr = getPalBytesAddr();
    Utils.writeBlocksLinear(blocksData, Globals.romdata, addr, count, false, false);
    int realBlocksCount = 128;
    for (int i = 0; i < realBlocksCount; i++)
    {
        Globals.romdata[palAddr + i] = (byte)blocksData[i].palBytes[0];
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