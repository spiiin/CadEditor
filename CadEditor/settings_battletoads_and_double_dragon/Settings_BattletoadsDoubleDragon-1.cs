using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x2C927, 1 , 72*8);   }
  public int getScreenWidth()          { return 72; }
  public int getScreenHeight()         { return 8; }
  public string getBlocksFilename()    { return "battletoads_doubledragon_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}