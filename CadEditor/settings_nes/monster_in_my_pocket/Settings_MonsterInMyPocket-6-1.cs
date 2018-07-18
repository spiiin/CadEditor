using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public OffsetRec[] getScreensOffsetsForLevels() {
    var ans = new OffsetRec[] {
      new OffsetRec(0x96eb, 6 , 48, 8, 6),
      new OffsetRec(0x980b, 2 , 64, 8, 8),
      new OffsetRec(0x988b, 5 , 48, 8, 6),
      new OffsetRec(0x997b, 1 , 64, 8, 6),
      new OffsetRec(0x99bb, 1 , 48, 8, 6),
      new OffsetRec(0x99eb, 3 , 64, 8, 8),
      new OffsetRec(0x9aab, 1 , 48, 8, 6),
      new OffsetRec(0x9adb, 1 , 64, 8, 8),
    };
    return ans;  
  }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()   { return new OffsetRec(0x39010, 1, 0x1000); }
  public OffsetRec getPalOffset()     { return new OffsetRec(0x1d5e7, 1, 16   ); }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()         { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()            { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()            { return Utils.setVideoChunk; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x9d91, 1, 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 256; }
  public int getPalBytesAddr()          { return 0xa881; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc() { return Utils.getPalleteLinear;}
  public SetPalFunc           setPalFunc() { return Utils.setPalleteLinear;}
  
  //----------------------------------------------------------------------------
}