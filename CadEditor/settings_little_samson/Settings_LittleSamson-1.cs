using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x10, 16, 8*8); }
  public int getScreenWidth()         { return 8; }
  public int getScreenHeight()        { return 8; }
  public string getBlocksFilename()   { return "little_samson_1.png"; }
  public int getBigBlocksCount() { return 230; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}