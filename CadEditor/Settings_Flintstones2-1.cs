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
  public OffsetRec getScreensOffset()   { return new OffsetRec(0xD832, 1 , 8*96);   }
  public override int getScreenWidth()    { return 8; }
  public override int getScreenHeight()   { return 96; }
  public bool getScreenVertical()         { return true; }
  public string getBlocksFilename() { return "flintstones2_1.png"; }
  public int    getPictureBlocksWidth()    { return 16; }
  public IList<LevelRec> getLevelRecs() { return null; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
}