using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x1088f, 1 , 40*19);   }
  public int getScreenWidth()          { return 40; }
  public int getScreenHeight()         { return 19; }
  public string getBlocksFilename()    { return "mickey_s_adventures_in_numberland_2.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}