using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x0e492, 1 , 52*22);   }
  public int getScreenWidth()          { return 52; }
  public int getScreenHeight()         { return 22; }
  public string getBlocksFilename()    { return "mickey_s_adventures_in_numberland_3.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}