using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public GameType getGameType()  { return GameType.Generic; }
  public OffsetRec getScreensOffset()     { return new OffsetRec(172510   , 1 , 26*86);  }
  public int getScreenWidth()    { return 26; }
  public int getScreenHeight()   { return 86; }
  public int getBigBlocksCount() { return 65; }
  public string getBlocksFilename()       { return "battletoads_4.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
}