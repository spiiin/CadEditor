using CadEditor;
using System;
//css_include tiny_toon_2/TT2Utils.cs;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{  
  public OffsetRec[] getScreensOffsetsForLevels() {
    var ans = new OffsetRec[] {
      new OffsetRec(0x66a5, 2 , 16*15, 16, 15),
      new OffsetRec(0x6885, 2 , 16*12, 16, 12),
      new OffsetRec(0x6a61, 8 , 16*13, 16, 13),
    };
    return ans;  
  }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk("chr2.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x70dd, 1  , 0x1000);  }
  public int getBlocksCount()           { return 64; }
  public int getBigBlocksCount()        { return 64; }
  public int getPalBytesAddr()          { return 0x71d5; }
  
  public GetBlocksFunc        getBlocksFunc() { return TT2Utils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return TT2Utils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin("pal2.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
}