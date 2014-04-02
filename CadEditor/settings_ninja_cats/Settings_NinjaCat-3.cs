using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public GameType getGameType()        { return GameType.Generic; }
  public OffsetRec getScreensOffset()  { return new OffsetRec(1940, 22 , 8*5);   }
  public int getScreenWidth()          { return 5; }
  public int getScreenHeight()         { return 8; }
  public string getBlocksFilename()    { return "ninja_cats_3.png"; }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
}