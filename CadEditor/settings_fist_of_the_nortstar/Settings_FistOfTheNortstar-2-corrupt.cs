using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x01D4D8, 1 , 78); }
  public int getScreenWidth()          { return 11; }
  public int getScreenHeight()         { return 7; }
  public string getBlocksFilename()    { return "fist_of_the_nortstar_2.png"; }
  public int    getPictureBlocksWidth()   { return 16; }

// Width
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }

  public bool getScreenVertical()         { return false; }
}