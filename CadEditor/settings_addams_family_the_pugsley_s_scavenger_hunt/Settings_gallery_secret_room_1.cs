using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x04b3f, 1 , 9*16);   }
  public int getScreenWidth()          { return 9; }
  public int getScreenHeight()         { return 14; }
  public string getBlocksFilename()    { return "gallery_5.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}