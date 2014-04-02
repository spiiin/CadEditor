using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public GameType getGameType()       { return GameType.Generic; }
  public OffsetRec getScreensOffset() { return new OffsetRec(0x300B1, 10, 64); }
  public int getScreenWidth()         { return 8; }
  public int getScreenHeight()        { return 8; }
  public string getBlocksFilename()   { return "tmnt3_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
}