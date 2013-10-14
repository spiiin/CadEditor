using CadEditor;
using System.Collections.Generic;

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
  
  public static OffsetRec getPalOffset()       { return new OffsetRec(0x1C374, 32  , 16);     }
  public static OffsetRec getVideoOffset()     { return new OffsetRec(0x30010, 16  , 0x1000); }
  public static OffsetRec getVideoObjOffset()  { return new OffsetRec(0x20010, 16  , 0x1000); }
  public static OffsetRec getBigBlocksOffset() { return new OffsetRec(0x36F0 , 8   , 0x4000); }
  public static OffsetRec getBlocksOffset()    { return new OffsetRec(0x3AF0 , 8   , 0x4000); }
  public static OffsetRec getScreensOffset()   { return new OffsetRec(0x10   , 300 , 0x40);   }
  public static int getBigBlocksCount()        { return 256; }
  public static IList<LevelRec> getLevelRecs() { return levelRecsTS; }
  
  public static GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public static GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public static SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public static GetBigBlocksFunc     getBigBlocksFunc()     { return Utils.getBigBlocksCapcomDefault;}
  public static SetBigBlocksFunc     setBigBlocksFunc()     { return Utils.setBigBlocksCapcomDefault;}
  
  public static IList<LevelRec> levelRecsTS = new List<LevelRec>() 
  {
    new LevelRec(0x0, 1, 8, 8,  0x0),
  };
  public static bool isBigBlockEditorEnabled() { return true;  }
  public static bool isBlockEditorEnabled()    { return true;  }
  public static bool isLayoutEditorEnabled()   { return false; }
  public static bool isEnemyEditorEnabled()    { return false; }
  public static bool isVideoEditorEnabled()    { return false; }
}