using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public OffsetRec[] getScreensOffsetsForLevels() {
    var ans = new OffsetRec[] {
      new OffsetRec(0x81e3, 10, 64, 8, 8),
      new OffsetRec(0x8463, 7, 48, 8, 6),
    };
    return ans;  
  }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()   { return new OffsetRec(0x34010, 1, 0x1000); }
  public OffsetRec getPalOffset()     { return new OffsetRec(0x1d4c7, 1, 16   ); }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()         { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()            { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()            { return Utils.setVideoChunk; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x886b, 1, 0x1000);  }
  public int getBlocksCount()           { return 128; }
  public int getBigBlocksCount()        { return 128; }
  public int getPalBytesAddr()          { return 0x925b; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc() { return Utils.getPalleteLinear;}
  public SetPalFunc           setPalFunc() { return Utils.setPalleteLinear;}
  
  //----------------------------------------------------------------------------
}