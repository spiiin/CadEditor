using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x3110, 2 , 8*8);   }
  public int getScreenWidth()          { return 8; }
  public int getScreenHeight()         { return 8; }
  public string getBlocksFilename()    { return "mighty_final_fight_1a.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}