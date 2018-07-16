using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x00010, 1 , 16*960, 16, 960);   }
  public bool getScreenVertical()      { return true; }
  public string getBlocksFilename()    { return "predator_1.png"; }
  public int    getPictureBlocksWidth(){ return 64; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}