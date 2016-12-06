using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x01b5e, 1 , 8*50);   }
  public int getScreenWidth()          { return 8; }
  public int getScreenHeight()         { return 50; }
  public string getBlocksFilename()    { return "armory_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}