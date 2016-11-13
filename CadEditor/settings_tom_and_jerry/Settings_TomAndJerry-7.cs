using CadEditor;
using System.Collections.Generic;
//css_include Settings_TomAndJerryBase.cs;

public class Data : TomAndJerryBase
{ 
  public OffsetRec getPalOffset()       { return new OffsetRec(0x14753, 1  , 16  );    }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x39810, 1  , 0x1000);  }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0      , 16 , 0x1000);  }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0      , 8  , 0x4000);  }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x15C65, 1  , 169*4);    }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x14C75, 1  , 170*24); }
  public override int getScreenWidth()     { return  170; }
  public override int getScreenHeight()    { return  24; }
  
  public IList<LevelRec> getLevelRecs() { return levelRecs; }
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public override int getBlocksCount()       { return 169; }
  public override int getBigBlocksCount()    { return 169; }
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x1478A, 38, 1, 1, 0x0),
  };
}