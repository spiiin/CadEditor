using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x03410, 1 , 8*128);   }
  public int getScreenWidth()          { return 8; }
  public int getScreenHeight()         { return 128; }
  public bool getScreenVertical()      { return true; }
  public string getBlocksFilename()    { return "mappy_kids_13.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}