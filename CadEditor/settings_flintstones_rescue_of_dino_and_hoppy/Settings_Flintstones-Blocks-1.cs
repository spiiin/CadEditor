using CadEditor;
using System.Collections.Generic;
public class Data
{
  public OffsetRec getScreensOffset()     { return new OffsetRec(0x1F0, 256 , 4*4);   }
  public int getScreenWidth()             { return 4; }
  public int getScreenHeight()            { return 4; }
  public bool getScreenVertical()         { return true; }
  public string getBlocksFilename()       { return "flintstones_blocks_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}