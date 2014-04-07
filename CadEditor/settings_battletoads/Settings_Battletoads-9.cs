using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public GameType getGameType()  { return GameType.Generic; }
  public OffsetRec getScreensOffset()     { return new OffsetRec(189036   , 1 , 48*66);  }
  public int getScreenWidth()    { return 48; }
  public int getScreenHeight()   { return 66; }
  public int getBigBlocksCount() { return 114; }
  public string getBlocksFilename()       { return "battletoads_9.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
}