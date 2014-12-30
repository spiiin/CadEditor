using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x13779   , 19 , 3*2);   }
  public int getScreenWidth()          { return 2; }
  public int getScreenHeight()         { return 3; }
  public string getBlocksFilename()    { return "rokin_cat_1.png"; }
  public int    getPictureBlocksWidth(){ return 64; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
}