using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x02010, 1 , 8*128, 8, 128);   }
  public bool getScreenVertical()      { return true; }
  public string getBlocksFilename()    { return "mappy_kids_9.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}