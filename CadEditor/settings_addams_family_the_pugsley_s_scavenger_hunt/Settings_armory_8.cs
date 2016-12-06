using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x016a2, 1 , 8*16);   }
  public int getScreenWidth()          { return 8; }
  public int getScreenHeight()         { return 14; }
  public string getBlocksFilename()    { return "armory_3.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}