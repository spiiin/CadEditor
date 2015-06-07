using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(0x403C, 24, 64);   }
  public int getScreenWidth()             { return 8; }
  public int getScreenHeight()            { return 8; }
  public string getBlocksFilename()       { return "banana_prince_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}