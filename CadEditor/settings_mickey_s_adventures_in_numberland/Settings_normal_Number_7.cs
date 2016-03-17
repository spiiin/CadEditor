using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x10fd9, 1 , 64*12);   }
  public int getScreenWidth()          { return 64; }
  public int getScreenHeight()         { return 12; }
  public string getBlocksFilename()    { return "mickey_s_adventures_in_numberland_4.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}