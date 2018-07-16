using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x17d69, 1 , 6*29, 6, 29);   }
  public bool getScreenVertical()      { return true; }
  public string getBlocksFilename()    { return "young_indiana_jones_chronicles_1_5_6.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}