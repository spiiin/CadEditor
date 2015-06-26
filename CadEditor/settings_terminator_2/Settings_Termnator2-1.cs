using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x1A177 - 160*12, 1, 160*14); }
  public int getScreenWidth()         { return 160; }
  public int getScreenHeight()        { return 14; }
  public string getBlocksFilename()   { return "termnator2_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}