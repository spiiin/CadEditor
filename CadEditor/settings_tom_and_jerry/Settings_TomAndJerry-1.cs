using CadEditor;
using System.Collections.Generic;
//css_include Settings_TomAndJerryBase.cs;

public class Data : TomAndJerryBase
{ 
  public OffsetRec getPalOffset()       { return new OffsetRec(0x80FA , 1  , 16  ); }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x2F010, 1  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0      , 16 , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0      , 8  , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xB3F5 , 1  , 160*4); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x84AF , 1  , 160*47); }
  public override int getScreenWidth()     { return 160; }
  public override int getScreenHeight()    { return 47; }
  
  public IList<LevelRec> getLevelRecs() { return levelRecs; }
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public override int getBlocksCount()    { return 160; }
  public override int getBigBlocksCount()    { return 160; }
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x8153, 52, 1, 1, 0x0),
  };
}