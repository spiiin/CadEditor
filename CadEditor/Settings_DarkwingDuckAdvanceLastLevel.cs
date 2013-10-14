using CadEditor;
using System.Collections.Generic;

public class Data
{
  //Using types
  //OffsetRec
  //public delegate int GetVideoPageAddrFunc(int videoPageId);
  //public delegate byte[] GetVideoChunkFunc(int videoPageId);
  
  public static GameType getGameType()  { return GameType.Generic; }
  
  public static OffsetRec getPalOffset()       { return new OffsetRec(0x3F1B0, 32  , 16);     }
  public static OffsetRec getVideoOffset()     { return new OffsetRec(0x70010, 32  , 0x1000); }
  public static OffsetRec getVideoObjOffset()  { return new OffsetRec(0x60010, 32  , 0x1000); }
  public static OffsetRec getBigBlocksOffset() { return new OffsetRec(0x276F0 , 1   , 0x4000); }
  public static OffsetRec getBlocksOffset()    { return new OffsetRec(0x27AF0 , 1   , 0x4000); }
  public static OffsetRec getScreensOffset()   { return new OffsetRec(0x20010   , 300 , 0x40);   }
  public static int getBigBlocksCount()        { return 256; }
  public static IList<LevelRec> getLevelRecs() { return levelRecsDwd; }
  
  public static GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public static GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public static SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public static GetBigBlocksFunc     getBigBlocksFunc()     { return Utils.getBigBlocksCapcomDefault;}
  public static SetBigBlocksFunc     setBigBlocksFunc()     { return Utils.setBigBlocksCapcomDefault;}
  
  public static IList<LevelRec> levelRecsDwd = new List<LevelRec>() 
  {
    new LevelRec(0x30410, 128, 17, 4,  0x1C394), 
  };
  //temp hack
  public static bool isDwdAdvanceLastLevel() { return true; }
}