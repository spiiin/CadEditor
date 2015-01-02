using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(194860   , 1 , 9*112);  }
  public int getScreenWidth()    { return 9; }
  public int getScreenHeight()   { return 112; }
  public int getBigBlocksCount() { return 110; }
  public string getBlocksFilename()       { return "battletoads_8.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}