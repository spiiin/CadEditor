using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  //public OffsetRec getScreensOffset()  { return new OffsetRec(0x8010, 50 , 32*8);   }
  //public int getScreenWidth()          { return 32; }
  //public int getScreenHeight()         { return 8; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()   { return new OffsetRec(0x22010, 1, 0x1000); }
  public OffsetRec getPalOffset()     { return new OffsetRec(0x169a, 16, 16   ); }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()         { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()            { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()            { return Utils.setVideoChunk; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x10 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 256; }
  public int getPalBytesAddr()          { return 0; }
  public GetBlocksFunc        getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocks;}
  public GetPalFunc           getPalFunc() { return Utils.getPalleteLinear;}
  public SetPalFunc           setPalFunc() { return Utils.setPalleteLinear;}

  public OffsetRec[] getScreensOffsetsForLevels() {
    var ans = new OffsetRec[] {
      new OffsetRec(0x8010  , 1  , 32*8, 32, 8),
      new OffsetRec(0x8210  , 1  , 32*10, 32, 10),
      new OffsetRec(0x8490  , 1  , 32*8, 32, 8),
      new OffsetRec(0x8610  , 1  , 32*10, 32, 10),
      new OffsetRec(0x8890  , 1  , 32*12, 32, 12),
      new OffsetRec(0x8b10  , 1  , 32*8, 32, 8),
      new OffsetRec(0x8d10  , 1  , 32*10, 32, 10),
      new OffsetRec(0x8f90  , 1  , 32*8, 32, 8),
      new OffsetRec(0x9190  , 1  , 32*12, 32, 12),
      new OffsetRec(0x9490  , 1  , 32*10, 32, 10),

      new OffsetRec(0x9790  , 1  , 32*12, 32, 12),
      new OffsetRec(0x9a10  , 1  , 32*10, 32, 10),
      new OffsetRec(0x9d10  , 1  , 32*12, 32, 12),
      new OffsetRec(0xa010  , 1  , 32*10, 32, 10),
      new OffsetRec(0xa310  , 1  , 32*14, 32, 14),
      new OffsetRec(0xa690  , 1  , 32*16, 32, 16),
      new OffsetRec(0xaa10  , 1  , 32*12, 32, 12),
      new OffsetRec(0xac90  , 1  , 32*10, 32, 10),
      new OffsetRec(0xaf10  , 1  , 32*12, 32, 12),
      new OffsetRec(0xb190  , 1  , 32*10, 32, 10),

      new OffsetRec(0xb410  , 1  , 32*10, 32, 10),
      new OffsetRec(0xb690  , 1  , 32*10, 32, 10),
      new OffsetRec(0xb890  , 1  , 32*15, 32, 15),
      new OffsetRec(0xbb90  , 1  , 32*12, 32, 12),
      new OffsetRec(0xc010  , 1  , 32*8, 32, 8),
      new OffsetRec(0xc190  , 1  , 32*16, 32, 16),
      new OffsetRec(0xc490  , 1  , 32*19, 32, 19),
      new OffsetRec(0xc790  , 1  , 32*10, 32, 10),
      new OffsetRec(0xca10  , 1  , 32*16, 32, 16),
      new OffsetRec(0xcd10  , 1  , 32*16, 32, 16),

      new OffsetRec(0xcf90  , 1  , 32*8, 32, 8),
      new OffsetRec(0xd110  , 1  , 32*16, 32, 16),
      new OffsetRec(0xd410  , 1  , 32*8, 32, 8),
      new OffsetRec(0xd590  , 1  , 32*12, 32, 12),
      new OffsetRec(0xd890  , 1  , 32*10, 32, 10),
      new OffsetRec(0xdad0  , 1  , 32*12, 32, 12),
      new OffsetRec(0xdd50  , 1  , 32*16, 32, 16),
      new OffsetRec(0xe010  , 1  , 32*10, 32, 10),
      new OffsetRec(0xe210  , 1  , 32*10, 32, 10),
      new OffsetRec(0xe410  , 1  , 32*16, 32, 16),

      new OffsetRec(0xe650  , 1  , 32*8, 32, 8),
      new OffsetRec(0xe7d0  , 1  , 32*16, 32, 16),
      new OffsetRec(0xead0  , 1  , 32*8, 32, 8),
      new OffsetRec(0xec90  , 1  , 32*8, 32, 8),
      new OffsetRec(0xee10  , 1  , 32*11, 32, 11),
      new OffsetRec(0xf010  , 1  , 32*16, 32, 16),
      new OffsetRec(0xf290  , 1  , 32*12, 32, 12),
      new OffsetRec(0xf4b0  , 1  , 32*8, 32, 8),
      new OffsetRec(0xf610  , 1  , 32*16, 32, 16),
      new OffsetRec(0xf910  , 1  , 32*8, 32, 8),

      new OffsetRec(0xfa90  , 1  , 32*12, 32, 12),
      new OffsetRec(0xfcd0  , 1  , 32*8, 32, 8),
    };
    return ans;  
  }
        
  public static ObjRec[] getBlocks(int blockIndex)
  {
      return Utils.readBlocksLinear(Globals.romdata,  ConfigScript.getTilesAddr(blockIndex), 4, 4, ConfigScript.getBlocksCount(), false);
  }

  public static void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
      Utils.writeBlocksLinear(blocksData, Globals.romdata, ConfigScript.getTilesAddr(blockIndex), ConfigScript.getBlocksCount(), false);
  }
}