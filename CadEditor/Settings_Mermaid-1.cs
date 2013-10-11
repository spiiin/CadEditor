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
  
  public static OffsetRec getPalOffset()       { return new OffsetRec(0x1DB53, 32  , 16);  }
  public static OffsetRec getVideoOffset()     { return new OffsetRec(0xC010, 1 , 0x1000); }
  public static OffsetRec getVideoObjOffset()  { return new OffsetRec(0xC010, 1 , 0x1000); }
  public static OffsetRec getBigBlocksOffset() { return new OffsetRec(0x510 , 1 , 0x4000); }
  public static OffsetRec getBlocksOffset()    { return new OffsetRec(0x10 ,  1 , 0x4000); }
  public static OffsetRec getScreensOffset()   { return new OffsetRec(0x4010,64 , 0x40);   }
  public static int getBigBlocksCount()        { return 256; }
  public static IList<LevelRec> getLevelRecs() { return levelRecs; }
  
  public static GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public static GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public static SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  
  public static IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x10000, 1, 8, 8,  0x1DAA8), 
  };
  public static bool isBigBlockEditorEnabled() { return true;  }
  public static bool isBlockEditorEnabled()    { return true;  }
  public static bool isLayoutEditorEnabled()   { return false; }
  public static bool isEnemyEditorEnabled()    { return false; }
  public static bool isVideoEditorEnabled()    { return false; }
}