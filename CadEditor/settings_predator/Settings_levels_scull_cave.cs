using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x08010, 1 , 16*960);   }
  public int getScreenWidth()          { return 16; }
  public int getScreenHeight()         { return 960; }
  public bool getScreenVertical()      { return true; }
  public string getBlocksFilename()    { return "predator_3.png"; }
  public int    getPictureBlocksWidth(){ return 64; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}