using CadEditor;
using System;
//css_include shared_settings/SharedUtils.cs;
//css_include captain_silver/CaptainSilverUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x147a2, 2 , 16*15, 16, 15);   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk("chr5-2.bin");    }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x14982, 1  , 0x1000);  }
  public int getBlocksCount()           { return 51; }
  public int getBigBlocksCount()        { return 51; }
  public int getPalBytesAddr()          { return 0x14a81; }
  
  public GetBlocksFunc        getBlocksFunc() { return CaptainSilverUtils.getBlocksLinear2x2MaskedWithAttribs;}
  public SetBlocksFunc        setBlocksFunc() { return CaptainSilverUtils.setBlocksLinear2x2MaskedWithAttribs;}
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin("pal5-2.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
}