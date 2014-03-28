using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public GameType getGameType()       { return GameType.Generic; }
  public OffsetRec getScreensOffset() { return new OffsetRec(0x31BF7, 6, 8*7); }
  public int getScreenWidth()         { return 8; }
  public int getScreenHeight()        { return 7; }
  public string getBlocksFilename()   { return "settings_tmnt3/tmnt3_2.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
}