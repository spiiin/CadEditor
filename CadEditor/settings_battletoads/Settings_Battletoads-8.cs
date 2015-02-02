using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(180909   , 1 , 9*135);  }
  public int getScreenWidth()    { return 9; }
  public int getScreenHeight()   { return 135; }
  public int getBigBlocksCount() { return 110; }
  public string getBlocksFilename()       { return "battletoads_8.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}