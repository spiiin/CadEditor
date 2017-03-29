using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x8376, 24 , 16*15); }
  public int getScreenWidth()          { return 16; }
  public int getScreenHeight()         { return 15; }
  
  public int getWordLen()              { return 2;}
  public bool isLittleEndian()          { return true;}
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()   { return new OffsetRec(0x55010, 1, 0x1000); }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x4AA3, 1  , 0x1000);  }
  public int getBlocksCount()           { return 1024; }
  public int getBigBlocksCount()        { return 1024; }
  public int getPalBytesAddr()          { return 0x0; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()         { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()            { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()            { return Utils.setVideoChunk; }
  public GetBlocksFunc getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc setBlocksFunc() { return setBlocks;}
  
  public GetPalFunc getPalFunc() { return getPallete;}
  public SetPalFunc setPalFunc() { return null;}
  public OffsetRec getPalOffset()                    { return new OffsetRec(0x3DFBD , 1  , 16  ); }
  //-------------------------------------------------------------------------------------------------------------------
  public virtual byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x38, 0x28, 0x18, 0x0f, 0x37, 0x17, 0x07,
      0x0f, 0x1c, 0x0c, 0x12, 0x0f, 0x30, 0x10, 0x00,
    }; 
    return pallete;
  }
  public ObjRec[] getBlocks(int blockIndex)
  {
      var romdata = Globals.romdata;
      int addr = ConfigScript.getTilesAddr(blockIndex);
      int count = ConfigScript.getBlocksCount();
      var objects = new ObjRec[count];
      for (int i = 0; i < count; i++)
      {
          byte c1, c2, c3, c4;
          c1 = romdata[addr + i * 4 + 0];
          c2 = romdata[addr + i * 4 + 1];
          c3 = romdata[addr + i * 4 + 2];
          c4 = romdata[addr + i * 4 + 3];
          objects[i] = new ObjRec(c1, c2, c3, c4, 0);
      }
      return objects;
  }
  
  public void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
      var romdata = Globals.romdata;
      int addr = ConfigScript.getTilesAddr(blockIndex);
      int count = ConfigScript.getBlocksCount();
      for (int i = 0; i < count; i++)
      {
          var obj = blocksData[i];
          romdata[addr + i * 4 + 0] = (byte)obj.c1;
          romdata[addr + i * 4 + 1] = (byte)obj.c2;
          romdata[addr + i * 4 + 2] = (byte)obj.c3;
          romdata[addr + i * 4 + 3] = (byte)obj.c4;
      }
  }
}