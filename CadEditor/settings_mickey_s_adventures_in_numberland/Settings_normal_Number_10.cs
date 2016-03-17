using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x0d75d, 1 , 36*21);   }
  public int getScreenWidth()          { return 36; }
  public int getScreenHeight()         { return 21; }
  public string getBlocksFilename()    { return "mickey_s_adventures_in_numberland_5.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}