using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x4036, 6, 7*128); }
  public int getScreenWidth()         { return 7; }
  public int getScreenHeight()        { return 128; }
  public string getBlocksFilename()    { return "yonoid_1.png"; }
  public int getBigBlocksCount() { return 138; }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}