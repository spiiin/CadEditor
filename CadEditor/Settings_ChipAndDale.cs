using CadEditor;
using System.Collections.Generic;

public class Data
{
  public static GameType getGameType()  { return GameType.CAD; }
   
  public static OffsetRec getPalOffset()       { return new OffsetRec(0x1C354, 32  , 16);     }
  public static OffsetRec getVideoOffset()     { return new OffsetRec(0x30010, 16  , 0x1000); }
  public static OffsetRec getVideoObjOffset()  { return new OffsetRec(0x20010, 16  , 0x1000); }
  public static OffsetRec getBigBlocksOffset() { return new OffsetRec(0x36F0 , 8   , 0x4000); }
  public static OffsetRec getBlocksOffset()    { return new OffsetRec(0x3AF0 , 8   , 0x4000); }
  public static OffsetRec getScreensOffset()   { return new OffsetRec(0x10   , 300 , 0x40);   }
  
  public static GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public static GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public static SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  
  public static int getBigBlocksCount()        { return 256; }
  public static IList<LevelRec> getLevelRecs() { return levelRecsCad; }
  
  //chip and dale specific
  public static OffsetRec getBoxesBackOffset() { return new OffsetRec(0x1E909, 11  , 16);     }
  public static int getLevelRecBaseOffset() { return 0x1E201; }
  public static int getLevelRecDirOffset()  { return 0x10239; }
  public static int getLayoutPtrAdd()       { return 0x10010; }
  public static int getScrollPtrAdd()       { return 0x10010; }
  public static int getDirPtrAdd()          { return 0x8010;  }
  public static int getDoorRecBaseOffset()  { return 0x1E673; }
  
  public static IList<LevelRec> levelRecsCad = new List<LevelRec>() 
  {
    new LevelRec(0x10388, 76),
    new LevelRec(0x10456, 31),
    new LevelRec(0x105A1, 73),
    new LevelRec(0x106D1, 57),
    new LevelRec(0x10890, 97),
    new LevelRec(0x10A1D, 74),
    new LevelRec(0x10B0E, 41),
    new LevelRec(0x10C88, 83),
    new LevelRec(0x10DB3, 53),
    new LevelRec(0x10EA1, 45),
    new LevelRec(0x10FED, 71),
  };
}