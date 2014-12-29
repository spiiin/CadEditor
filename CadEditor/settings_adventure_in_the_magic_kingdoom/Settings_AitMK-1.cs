using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(0xA890, 4 /*next - castle level too, with other graphics*/, 8*9);   }
  public int getScreenWidth()             { return 8; }
  public int getScreenHeight()            { return 9; }
  public string getBlocksFilename()       { return "adv_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
}