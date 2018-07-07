using CadEditor;
using System;
//css_include settings_crystal_mines/CrystalUtils.cs

public class Data 
{
  public int getScreenWidth()         { return 30; }
  public int getScreenHeight()        { return 11; }
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return CrystalUtils.getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return CrystalUtils.getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public GetBlocksFunc        getBlocksFunc() { return CrystalUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return CrystalUtils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return CrystalUtils.getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public int getBlocksCount()           { return 62; }
  public int getBigBlocksCount()        { return 62; }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x432, 1  , 0x1000);  }
  
  public int getLevelsCount()           { return 50; }
  public LoadScreensFunc loadScreensFunc()                  { return Utils.loadScreensDiffSize; }
  
  public OffsetRec[] getScreensOffsetsForLevels() {
    var ans = new OffsetRec[] {
      new OffsetRec(0x859f  , 1  , 30*11, 30, 11),
      new OffsetRec(0x875e  , 1  , 30*11, 30, 11),
      new OffsetRec(0x8926  , 1  , 30*11, 30, 11),
      new OffsetRec(0x8b0c  , 1  , 30*11, 30, 11),
      new OffsetRec(0x8cf8  , 1  , 30*11, 30, 11),
      new OffsetRec(0x8eed  , 1  , 30*11, 30, 11),
      new OffsetRec(0x90c2  , 1  , 30*11, 30, 11),
      new OffsetRec(0x92a0  , 1  , 30*11, 30, 11),
      new OffsetRec(0x946c  , 1  , 30*11, 30, 11),
      
      new OffsetRec(0x961e  , 1  , 30*11, 30, 11),
      new OffsetRec(0x97b2  , 1  , 30*11, 30, 11),
      new OffsetRec(0x994f  , 1  , 30*11, 30, 11),
      new OffsetRec(0x9af2  , 1  , 30*11, 30, 11),
      new OffsetRec(0x9cae  , 1  , 30*11, 30, 11),
      new OffsetRec(0x9e4f  , 1  , 30*11, 30, 11),
      new OffsetRec(0xa00c  , 1  , 30*11, 30, 11),
      new OffsetRec(0xa1be  , 1  , 30*11, 30, 11),
      new OffsetRec(0xa34e  , 1  , 30*11, 30, 11),
      new OffsetRec(0xa4e4  , 1  , 30*11, 30, 11),
      
      new OffsetRec(0xa681  , 1  , 30*11, 30, 11),
      new OffsetRec(0xa838  , 1  , 30*11, 30, 11),
      new OffsetRec(0xaa0d  , 1  , 30*11, 30, 11),
      new OffsetRec(0xaba5  , 1  , 30*11, 30, 11),
      new OffsetRec(0xad64  , 1  , 30*11, 30, 11),
      new OffsetRec(0xaef4  , 1  , 30*11, 30, 11),
      new OffsetRec(0xb0a4  , 1  , 30*11, 30, 11),
      new OffsetRec(0xb233  , 1  , 30*11, 30, 11),
      new OffsetRec(0xb3cf  , 1  , 30*11, 30, 11),
      new OffsetRec(0xb578  , 1  , 30*11, 30, 11),
      
      new OffsetRec(0xb715  , 1  , 30*11, 30, 11),
      new OffsetRec(0xb8ba  , 1  , 30*11, 30, 11),
      new OffsetRec(0xba50  , 1  , 30*11, 30, 11),
      new OffsetRec(0xbc26  , 1  , 30*11, 30, 11),
      new OffsetRec(0xbdd4  , 1  , 30*11, 30, 11),
      new OffsetRec(0xbfc1  , 1  , 30*11, 30, 11),
      new OffsetRec(0xc14d  , 1  , 30*11, 30, 11),
      new OffsetRec(0xc2e3  , 1  , 30*11, 30, 11),
      new OffsetRec(0xc470  , 1  , 30*11, 30, 11),
      new OffsetRec(0xc68e  , 1  , 30*11, 30, 11),
      
      new OffsetRec(0xc8a1  , 1  , 30*11, 30, 11),
      new OffsetRec(0xca3e  , 1  , 30*11, 30, 11),
      new OffsetRec(0xcc34  , 1  , 30*11, 30, 11),
      new OffsetRec(0xce1f  , 1  , 30*11, 30, 11),
      new OffsetRec(0xd009  , 1  , 30*11, 30, 11),
      new OffsetRec(0xd225  , 1  , 30*11, 30, 11),
      new OffsetRec(0xd393  , 1  , 30*11, 30, 11),
      new OffsetRec(0xd5d9  , 1  , 30*11, 30, 11),
      new OffsetRec(0xd74a  , 1  , 30*11, 30, 11),
      new OffsetRec(0xd8ad  , 1  , 30*11, 30, 11),
      
      new OffsetRec(0xda26  , 1  , 30*11, 30, 11),
    };
    return ans;  
  }

  //----------------------------------------------------------------------------
}