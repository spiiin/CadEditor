using CadEditor;
using System;
//css_include cheetahmen_2/CheetahUtils.cs;

public class Data 
{
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x305a3, 1 , 16*16, 16, 16); }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()   { return new OffsetRec(0x54010, 1, 0x256); }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x3049a, 1  , 0x32);  }
  public int getBlocksCount()           { return 128; }
  public int getBigBlocksCount()        { return 128; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()         { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()            { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()            { return Utils.setVideoChunk; }
  
  public GetBlocksFunc getBlocksFunc() { return Cheetah.getBlocks;}
  public SetBlocksFunc setBlocksFunc() { return Cheetah.setBlocks;}
  public int getPalBytesAddr()         { return 0x3056e; }
  
  public GetPalFunc getPalFunc()  { return Utils.getPalleteLinear;}
  public SetPalFunc setPalFunc()  { return Utils.setPalleteLinear;}
  public OffsetRec getPalOffset() { return new OffsetRec(0x306a3, 1  , 16  ); }
}