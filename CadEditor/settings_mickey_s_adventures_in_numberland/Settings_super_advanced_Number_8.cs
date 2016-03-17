using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x1190b, 1 , 52*20);   }
  public int getScreenWidth()          { return 52; }
  public int getScreenHeight()         { return 20; }
  public string getBlocksFilename()    { return "mickey_s_adventures_in_numberland_4.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}