using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x1001A, 16 , 8*6); }
  public int getScreenWidth()          { return 6; }
  public int getScreenHeight()         { return 8; }
  public string getBlocksFilename()    { return "castlevania_1.png"; }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}