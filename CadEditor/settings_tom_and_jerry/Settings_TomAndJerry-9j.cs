using CadEditor;
using System.Collections.Generic;
//css_include settings_tom_and_jerry/TomAndJerryBase.cs;

public class Data : TomAndJerryBase
{ 
  public OffsetRec getPalOffset()       { return new OffsetRec(0x149AF, 1  , 16  );     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x3C010-1, 1  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0      , 16 , 0x1000);   }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0      , 8  , 0x4000);   }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x19CFA, 1  , 182*4);    }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x18530, 1  , 105*58, 105, 58);   }
  
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public override int getBigBlocksCount()    { return 182; }
  public override int getBlocksCount()       { return 182; }
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x1812D, 42, 1, 1, 0x0),
  };
  
  public override int getCheeseAddr() { return 0x1810A; }
}