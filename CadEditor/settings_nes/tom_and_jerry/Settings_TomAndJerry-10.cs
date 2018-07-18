using CadEditor;
using System.Collections.Generic;
//css_include tom_and_jerry/TomAndJerryBase.cs;

public class Data : TomAndJerryBase
{ 
  public OffsetRec getPalOffset()       { return new OffsetRec(0x181ED, 1  , 16  );     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x3D010-1, 1  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0      , 16 , 0x1000);   }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0      , 8  , 0x4000);   }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x1b28b, 1  , 151*4);    }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x1a25e, 1  , 101*41, 101, 41);   }
  
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public override int getBigBlocksCount()    { return 151; }
  public override int getBlocksCount()       { return 151; }
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x18226, 48, 1, 1, 0x0),
  };
  
  public override int getCheeseAddr() { return 0x181FD; }
}