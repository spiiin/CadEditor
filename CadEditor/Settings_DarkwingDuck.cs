using CadEditor;
public class Data
{
  //Using types
  //OffsetRec
  //public delegate int GetVideoPageAddrFunc(int videoPageId);
  //public delegate byte[] GetVideoChunkFunc(int videoPageId);
  
  public static GameType getGameType()  { return GameType.Generic; }
  
  public static OffsetRec getPalOffset()       { return new OffsetRec(0x1C36D, 32  , 16);     }
  public static OffsetRec getVideoOffset()     { return new OffsetRec(0x30010, 16  , 0x1000); }
  public static OffsetRec getVideoObjOffset()  { return new OffsetRec(0x20010, 16  , 0x1000); }
  public static OffsetRec getBigBlocksOffset() { return new OffsetRec(0x36F0 , 8   , 0x4000); }
  public static OffsetRec getBlocksOffset()    { return new OffsetRec(0x3AF0 , 8   , 0x4000); }
  public static OffsetRec getScreensOffset()   { return new OffsetRec(0x10   , 300 , 0x40);   }
  public static int getBigBlocksCount()        { return 256; }
  
  public static GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public static GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
}