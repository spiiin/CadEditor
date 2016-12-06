using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x01712, 1 , 10*41);   }
  public int getScreenWidth()          { return 10; }
  public int getScreenHeight()         { return 40; }
  public string getBlocksFilename()    { return "armory_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}