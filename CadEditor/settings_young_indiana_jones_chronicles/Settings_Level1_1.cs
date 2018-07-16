using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0xc7b0, 1 , 6*216, 6, 216);   }
  public bool getScreenVertical()      { return true; }
  public string getBlocksFilename()    { return "young_indiana_jones_chronicles_1_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}