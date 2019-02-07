using CadEditor;
using System;
//css_include kabuki_quantum_fighter/KabukiUtils.cs;

public class Data 
{
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x411, 1 , 12*240, 12, 240);   }
  public bool getScreenVertical()      { return true; }
  public bool isBuildScreenFromSmallBlocks() { return true; }

  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public OffsetRec getVideoOffset()                  { return new OffsetRec(0x30010, 1 , 0x1000); }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x11 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 256; }
  public GetBlocksFunc getBlocksFunc() { return Utils.getBlocksLinear2x2withoutAttrib;}
  public SetBlocksFunc setBlocksFunc() { return Utils.setBlocksLinearWithoutAttrib;}
  public int getPalBytesAddr()          { return 0xf51-6; }
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  //----------------------------------------------------------------------------
  public byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x0b, 0x1a, 0x28, 0x0f, 0x06, 0x15, 0x35,
      0x0f, 0x07, 0x17, 0x39, 0x0f, 0x21, 0x16, 0x30
    }; 
    return pallete;
  }  
}