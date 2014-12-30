using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x10917, 6 , 8*15);   }
  public int getScreenWidth()          { return 8; }
  public int getScreenHeight()         { return 15; }
  public string getBlocksFilename()    { return "batman_1.png"; }
  public int    getPictureBlocksWidth(){ return 64; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
}