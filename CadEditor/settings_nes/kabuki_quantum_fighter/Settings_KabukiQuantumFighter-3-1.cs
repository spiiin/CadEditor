using CadEditor;
using System;

public class Data 
{
  public OffsetRec getScreensOffset()  { return new OffsetRec(0xa411, 1 , 16*176, 16, 176);   }
  public bool isBuildScreenFromSmallBlocks() { return true; }

  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public OffsetRec getVideoOffset()                  { return new OffsetRec(0x34010, 1 , 0x1000); }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xa011 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 256; }
  public GetBlocksFunc getBlocksFunc() { return Utils.getBlocksLinear2x2withoutAttrib;}
  public SetBlocksFunc setBlocksFunc() { return Utils.setBlocksLinearWithoutAttrib;}
  public int getPalBytesAddr()          { return 0xaf11; }
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  //----------------------------------------------------------------------------
  public byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x04, 0x16, 0x27, 0x0f, 0x11, 0x22, 0x33,
      0x0f, 0x09, 0x0a, 0x10, 0x0f, 0x21, 0x16, 0x30
    }; 
    return pallete;
  }
}