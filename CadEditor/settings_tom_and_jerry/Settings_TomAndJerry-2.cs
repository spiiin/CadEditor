using CadEditor;
using System.Collections.Generic;
//css_include Settings_CapcomBase.cs;
public class Data : CapcomBase
{ 
  public OffsetRec getPalOffset()       { return new OffsetRec(0, 32  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0, 16  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0, 16  , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0 , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0 , 8   , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(41540   , 1 , 48*84);   }
  public override int getScreenWidth()    { return 48; }
  public override int getScreenHeight()   { return 84; }
  public string getBlocksFilename() { return "settings_tom_and_jerry/tom_and_jerry_2.png"; }
  public IList<LevelRec> getLevelRecs() { return null; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
}