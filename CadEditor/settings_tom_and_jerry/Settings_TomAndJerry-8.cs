using CadEditor;
using System.Collections.Generic;
//css_include Settings_TomAndJerryBase.cs;

public class Data : TomAndJerryBase
{ 
  public OffsetRec getPalOffset()       { return new OffsetRec(0x1483F, 1  , 16  );    }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x3A810, 1  , 0x1000);  }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0      , 16 , 0x1000);  }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0      , 8  , 0x4000);  }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x16DDB, 1  , 44*4);    }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x1615B, 1  , 32*100, 32, 100); }
  
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public override int getBlocksCount()       { return 44; }
  public override int getBigBlocksCount()    { return 44; }
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x14874, 55, 1, 1, 0x0),
  };
  
  public override int getCheeseAddr() { return 0x1484f; }
}