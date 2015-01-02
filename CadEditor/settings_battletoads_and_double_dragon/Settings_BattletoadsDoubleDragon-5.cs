using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(179167, 1 , 144*8);   }
  public int getScreenWidth()          { return 144; }
  public int getScreenHeight()         { return 8; }
  public string getBlocksFilename()    { return "battletoads_doubledragon_5.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}