using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public GameType getGameType()           { return GameType.Generic; }
  public OffsetRec getScreensOffset()     { return new OffsetRec(0x10, 1 , 8*60);   }
  public int getScreenWidth()             { return 8; }
  public int getScreenHeight()            { return 60; }
  public bool getScreenVertical()         { return true; }
  public string getBlocksFilename()       { return "flintstones_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
}