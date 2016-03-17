using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x12970, 1 , 24*40);   }
  public int getScreenWidth()          { return 24; }
  public int getScreenHeight()         { return 40; }
  public string getBlocksFilename()    { return "hook_3.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}