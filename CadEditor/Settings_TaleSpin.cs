using CadEditor;
using System.Collections.Generic;
//css_include Settings_CapcomBase.cs;
public class Data:CapcomBase
{
  public OffsetRec getPalOffset()       { return new OffsetRec(0x1C374, 32  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x30010, 16  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x20010, 16  , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x36F0 , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x3AF0 , 8   , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x10   , 300 , 0x40);   }
  public IList<LevelRec> getLevelRecs() { return levelRecsTS; }
  
  public IList<LevelRec> levelRecsTS = new List<LevelRec>() 
  {
    new LevelRec(0x1035F, 59,  8, 5,  0x1E10C), 
    new LevelRec(0x104C2, 74, 10, 5,  0x1E13C),
    new LevelRec(0x10600, 61, 17, 4,  0x1E16E),
    new LevelRec(0x1078D, 84, 12, 7,  0x1E1A1),
    new LevelRec(0x108DD, 63, 14, 6,  0x1E1F5),
    new LevelRec(0x109FC, 56, 9 , 8,  0x1E249),
    new LevelRec(0x10B00, 51, 12, 5,  0x1E292),
    new LevelRec(0x10C07, 53, 8,  6,  0x1E2D9),
  };
  public bool isBigBlockEditorEnabled() { return true;  }
  public bool isBlockEditorEnabled()    { return true;  }
  public bool isLayoutEditorEnabled()   { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  public bool isVideoEditorEnabled()    { return true; }
}