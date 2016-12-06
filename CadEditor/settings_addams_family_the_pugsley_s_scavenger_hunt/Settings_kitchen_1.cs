using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x0326d, 1 , 44*7);   }
  public int getScreenWidth()          { return 22; }
  public int getScreenHeight()         { return 7; }
  public string getBlocksFilename()    { return "kitchen_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}