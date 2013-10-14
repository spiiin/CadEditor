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
  public static OffsetRec getVideoOffset()     { return new OffsetRec(0xF010, 1 , 0xD00); }
  public static OffsetRec getVideoObjOffset()  { return new OffsetRec(0xF010, 1 , 0xD00); }
  public static OffsetRec getBigBlocksOffset() { return new OffsetRec(0x2910, 1 , 0x4000); }
  public static OffsetRec getBlocksOffset()    { return new OffsetRec(0x2410,  1 , 0x4000); }
  public static OffsetRec getScreensOffset()   { return new OffsetRec(0x5250, 32 , 0x40);   }
  public static int getBigBlocksCount()        { return 256; }
  public static IList<LevelRec> getLevelRecs() { return levelRecs; }
  
  public static GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public static GetVideoChunkFunc    getVideoChunkFunc()    { return getLMVideoChunk; }
  public static SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public static GetBigBlocksFunc     getBigBlocksFunc()     { return Utils.getBigBlocksCapcomDefault;}
  public static SetBigBlocksFunc     setBigBlocksFunc()     { return Utils.setBigBlocksCapcomDefault;}
  public static GetPalFunc           getPalFunc()           { return Utils.getPalleteLinear;}
  public static SetPalFunc           setPalFunc()           { return Utils.setPalleteLinear;}
  
  public static IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x10000, 1, 8, 8,  0x1DAF5), 
  };
  
  public static byte[] getLMVideoChunk(int videoPageId)
  {
    byte[] videoChunk = Utils.getVideoChunk(videoPageId);
    //fill first quarter of videoChunk with constant to all video memory data
    for (int i = 0; i < 16 * 16 * 4; i++)
        videoChunk[i] = Globals.romdata[0xC010 + i];
    return videoChunk;
  }
  public static bool isBigBlockEditorEnabled() { return true;  }
  public static bool isBlockEditorEnabled()    { return true;  }
  public static bool isLayoutEditorEnabled()   { return false; }
  public static bool isEnemyEditorEnabled()    { return false; }
  public static bool isVideoEditorEnabled()    { return false; }
}