using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x2DAD2, 1 , 32*7);   }
  public int getScreenWidth()          { return 32; }
  public int getScreenHeight()         { return 7; }
  public string getBlocksFilename()    { return "battletoads_doubledragon_6.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}