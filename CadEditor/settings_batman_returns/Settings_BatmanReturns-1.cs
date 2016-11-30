using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x446E, 6, 8*6); }
  public int getScreenWidth()         { return 8; }
  public int getScreenHeight()        { return 6; }
  public string getBlocksFilename()    { return "batman_returns_1.png"; }
  public int getBigBlocksCount() { return 65; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}