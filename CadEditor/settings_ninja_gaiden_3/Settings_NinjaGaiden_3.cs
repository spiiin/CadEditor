using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x10, 1 , 6*40);   }
  public int getScreenWidth()          { return 6; }
  public int getScreenHeight()         { return 40; }
  public string getBlocksFilename()    { return "ninja_gaiden_3.png"; }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}