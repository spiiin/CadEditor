using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public GameType getGameType()           { return GameType.Generic; }
  public OffsetRec getScreensOffset()     { return new OffsetRec(0x31, 6, 64);   }
  public int getScreenWidth()             { return 8; }
  public int getScreenHeight()            { return 8; }
  public string getBlocksFilename()       { return "monster_in_my_pocket_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
}