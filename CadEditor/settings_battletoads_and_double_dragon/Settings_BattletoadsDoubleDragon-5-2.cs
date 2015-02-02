using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(185778, 1 , 32*8);   }
  public int getScreenWidth()          { return 32; }
  public int getScreenHeight()         { return 8; }
  public int getBigBlocksCount()       { return 62; }
  public string getBlocksFilename()    { return "battletoads_doubledragon_5-2.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}