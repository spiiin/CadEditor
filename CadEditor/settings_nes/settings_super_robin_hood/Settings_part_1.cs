using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x006b6, 1 , 14*976, 14, 976);   }
  public bool getScreenVertical()      { return true; }
  public string getBlocksFilename()    { return "super_robin_hood_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}