using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x2B60+1, 1 , 30*29);   }
  public int getScreenWidth()          { return 30; }
  public int getScreenHeight()         { return 29; }
  public int getScreenDataStride()     { return 2;} 
  public string getBlocksFilename()    { return "sega_lost_vikings_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}