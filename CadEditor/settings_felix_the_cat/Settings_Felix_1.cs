using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x0, 1 , 24*256*3);   }
  public int getScreenWidth()          { return 24; }
  public int getScreenHeight()         { return 256*3; }
  public string getBlocksFilename()    { return "felix_1.png"; }
  public bool getScreenVertical()      { return true;   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}