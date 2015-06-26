using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x2020-16, 1, 64*64); }
  public int getScreenWidth()         { return 64; }
  public int getScreenHeight()        { return 64; }
  public string getBlocksFilename()   { return "alien3_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}