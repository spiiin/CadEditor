using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x00a78, 1 , 30*20);   }
  public int getScreenWidth()          { return 30; }
  public int getScreenHeight()         { return 20; }
  public string getBlocksFilename()    { return "addams_family_the_pugsley_s_scavenger_hunt_2.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}