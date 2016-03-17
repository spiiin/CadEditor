using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x0ce8b, 1 , 48*26);   }
  public int getScreenWidth()          { return 48; }
  public int getScreenHeight()         { return 26; }
  public string getBlocksFilename()    { return "mickey_s_adventures_in_numberland_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}