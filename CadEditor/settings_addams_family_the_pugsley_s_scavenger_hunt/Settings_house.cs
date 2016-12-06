using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x00290, 1 , 50*21);   }
  public int getScreenWidth()          { return 50; }
  public int getScreenHeight()         { return 21; }
  public string getBlocksFilename()    { return "house_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}