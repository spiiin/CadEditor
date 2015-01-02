using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x2804D + 48*3, 2, 8*7); }
  public int getScreenWidth()         { return 8; }
  public int getScreenHeight()        { return 7; }
  public string getBlocksFilename()   { return "tmnt3_8.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}