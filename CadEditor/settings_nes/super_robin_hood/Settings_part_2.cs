using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x03c1e, 1 , 14*72, 14, 72);   }
  public bool getScreenVertical()      { return true; }
  public string getBlocksFilename()    { return "super_robin_hood_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}