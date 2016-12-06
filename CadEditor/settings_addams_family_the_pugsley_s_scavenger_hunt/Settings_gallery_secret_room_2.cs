using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x05680, 1 , 8*11);   }
  public int getScreenWidth()          { return 8; }
  public int getScreenHeight()         { return 11; }
  public string getBlocksFilename()    { return "gallery_2.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}