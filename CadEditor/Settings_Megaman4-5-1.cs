using CadEditor;
using System.Collections.Generic;
//CHR-correct only before rhino subboss.
public class Data
{
  //Using types
  //OffsetRec
  //public delegate int GetVideoPageAddrFunc(int videoPageId);
  //public delegate byte[] GetVideoChunkFunc(int videoPageId);
  //public delegate void SetVideoChunkFunc(int videoPageId, byte[] videoChunk);
  /*public struct LevelRec
  {
      public LevelRec(int objectsBeginAddr, int objCount, int width = 0, int height = 0, int layoutAddr = 0)
      {
          this.objCount = objCount;
          this.objectsBeginAddr = objectsBeginAddr;
          this.width = width;
          this.height = height;
          this.layoutAddr = layoutAddr;
      }
      public int objCount;
      public int objectsBeginAddr;
      public int width;
      public int height;
      public int layoutAddr;
  }*/
  
  public static GameType getGameType()  { return GameType.Generic; }
  
  public static OffsetRec getPalOffset()       { return new OffsetRec(0x495A0, 1  , 16);     }
  public static OffsetRec getVideoOffset()     { return new OffsetRec(0x24010, 1   , 0x1000); }
  public static OffsetRec getVideoObjOffset()  { return new OffsetRec(0x20010, 1   , 0x1000); } //
  public static OffsetRec getBigBlocksOffset() { return new OffsetRec(0x48510 , 1  , 0x4000); }
  public static OffsetRec getBlocksOffset()    { return new OffsetRec(0x48010 , 1  , 0x4000); }
  public static OffsetRec getScreensOffset()   { return new OffsetRec(0x48910 , 32 , 0x40);   }
  public static int getBigBlocksCount()        { return 256; }
  public static IList<LevelRec> getLevelRecs() { return levelRecsDwd; } //
  
  public static GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public static GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public static SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  
  public static IList<LevelRec> levelRecsDwd = new List<LevelRec>() 
  {
    new LevelRec(0x10000, 1, 8, 4,  0x49510), 
  };
  //temp hack
  public static bool isDwdAdvanceLastLevel() { return false; }
}