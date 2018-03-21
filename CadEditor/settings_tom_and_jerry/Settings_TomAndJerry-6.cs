using CadEditor;
using System.Collections.Generic;
//css_include Settings_TomAndJerryBase.cs;

public class Data : TomAndJerryBase
{ 
  public OffsetRec getPalOffset()       { return new OffsetRec(0x1027A, 1  , 16  );    }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x35010, 1  , 0x1000);  }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0      , 16 , 0x1000);  }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0      , 8  , 0x4000);  }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x12DAC, 1  , 66*4);    }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x11C2C , 1  , 40*112); }
  public override int getScreenWidth()     { return  40; }
  public override int getScreenHeight()    { return 112; }
  
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public override int getBlocksCount()       { return 66; }
  public override int getBigBlocksCount()    { return 66; }
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x102B3, 73, 1, 1, 0x0),
  };
  
  public override int getCheeseAddr() { return 0x1028a; }
}