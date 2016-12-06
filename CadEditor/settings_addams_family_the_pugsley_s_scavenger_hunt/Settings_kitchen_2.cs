using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x02fd2, 1 , 15*7);   }
  public int getScreenWidth()          { return 15; }
  public int getScreenHeight()         { return 7; }
  public string getBlocksFilename()    { return "kitchen_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}