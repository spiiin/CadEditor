using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x1F8, 1, 6*150); }
  public int getScreenWidth()         { return 6; }
  public int getScreenHeight()        { return 150; }
  public string getBlocksFilename()    { return "jackie_chan_1.png"; }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}