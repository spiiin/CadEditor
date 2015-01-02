using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(184828   , 1 , 56*48);  }
  public int getScreenWidth()    { return 56; }
  public int getScreenHeight()   { return 48; }
  public int getBigBlocksCount() { return 89; }
  public string getBlocksFilename()       { return "battletoads_11.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}