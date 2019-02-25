using CadEditor;
using System;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{
  public OffsetRec[] getScreensOffsetsForLevels() {
    var ans = new OffsetRec[] {
        new OffsetRec( 0x10fd9, 1 , 64*12, 64, 12),
        new OffsetRec( 0x112d9, 1 , 40*15, 40, 15),
        new OffsetRec( 0x11531, 1 , 58*17, 58, 17),
        new OffsetRec( 0x1190b, 1 , 52*20, 52, 20),
   };
    return ans;
  }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk(new[] {"chr4.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x14e13 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 158; }
  public int getBigBlocksCount()        { return 158; }
  public int getPalBytesAddr()          { return 0x15801; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal4.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal4.bin");
  }
}