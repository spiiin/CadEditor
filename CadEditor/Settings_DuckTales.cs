using CadEditor;
using System.Collections.Generic;

public class Data
{
  //--------------------------------------------------------------------------------------------
  public static GameType getGameType()  { return GameType.DT; }
  
  public static OffsetRec getPalOffset()       { return new OffsetRec(0x1DA44, 10  , 16);     }
  public static OffsetRec getVideoOffset()     { return new OffsetRec(0x4D10 , 6   , 0xD00);  }
  public static OffsetRec getVideoObjOffset()  { return new OffsetRec(0x4D10 , 6   , 0xD00);  }
  public static OffsetRec getBigBlocksOffset() { return new OffsetRec(0x7310 , 3   , 0x4000); }
  public static OffsetRec getBlocksOffset()    { return new OffsetRec(0x7B10 , 3   , 0x4000); }
  public static OffsetRec getScreensOffset()   { return new OffsetRec(0x10058, 300 , 0x48);   }
  public static int getBigBlocksCount()        { return 512; }
  public static IList<LevelRec> getLevelRecs() { return levelRecsDt; }
  
  public static GetVideoPageAddrFunc getVideoPageAddrFunc() { return getDuckTalesVideoAddress; }
  public static GetVideoChunkFunc    getVideoChunkFunc()    { return getDuckTalesVideoChunk;   }
  public static SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  
  public static IList<LevelRec> levelRecsDt = new List<LevelRec>() 
  {
    new LevelRec(0x1B43B, 181, 8, 7, 0x1CE7B),
    new LevelRec(0x1B6CC, 156, 8, 8, 0x1CEB3),
    new LevelRec(0x1B8E8, 126, 8, 6, 0x1CEF3),
    new LevelRec(0x1BAD1, 119, 8, 6, 0x1CF23),
    new LevelRec(0x1BD70, 182, 8, 6, 0x1CF53),
  };
  
  //--------------------------------------------------------------------------------------------
  //duck tales specific
  
  public static int getDuckTalesVideoAddress(int id)
  {
    if (id == 0x90) return 0x4010;
    if (id == 0x91) return 0x4D10;
    if (id == 0x92) return 0x5A10;
    if (id == 0x93) return 0x7D10;
    if (id == 0x94) return 0x8A10;
    if (id == 0x95) return 0x9710;
    return -1;
  }
  
  public static byte[] getDuckTalesVideoChunk(int videoPageId)
  {
    byte[] videoChunk = Utils.getVideoChunk(videoPageId);
    //fill first quarter of videoChunk with constant to all video memory data
    for (int i = 0; i < 16 * 16 * 3; i++)
        videoChunk[i] = Globals.romdata[0x4010 + i];
    return videoChunk;
  }
  //--------------------------------------------------------------------------------------------
}