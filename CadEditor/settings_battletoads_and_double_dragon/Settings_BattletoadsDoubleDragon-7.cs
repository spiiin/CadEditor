using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x2F614, 1 , 16*6);   }
  public int getScreenWidth()          { return 16; }
  public int getScreenHeight()         { return 6; }
  public string getBlocksFilename()    { return "battletoads_doubledragon_7.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}