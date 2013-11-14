using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public GameType getGameType()  { return GameType.Generic; }
  public OffsetRec getScreensOffset()     { return new OffsetRec(166366   , 1 , 11*64);  }
  public int getScreenWidth()    { return 64; }
  public int getScreenHeight()   { return 11; }
  public int getBigBlocksCount() { return 44; }
  public string getBlocksFilename()       { return "battletoads_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
}