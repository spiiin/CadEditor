using CadEditor;
using System.Collections.Generic;
//css_include Settings_TomAndJerryBase.cs;

public class Data : TomAndJerryBase
{ 
  public OffsetRec getPalOffset()       { return new OffsetRec(0xA20F , 1  , 16  ); }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x30010, 1  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0      , 16 , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0      , 8  , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xB8B5 , 1  , 72*4); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0xA23C , 1  , 48*84); }
  public override int getScreenWidth()     { return 48; }
  public override int getScreenHeight()    { return 84; }
  
  public IList<LevelRec> getLevelRecs() { return levelRecs; }
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public override int getBlocksCount()    { return 72; }
  public override int getBigBlocksCount()    { return 72; }
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x8257, 38, 1, 1, 0x0),
  };
}