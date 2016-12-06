using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x00010, 1 , 8*128);   }
  public int getScreenWidth()          { return 8; }
  public int getScreenHeight()         { return 128; }
  public string getBlocksFilename()    { return "mappy_kids_1.png"; }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}