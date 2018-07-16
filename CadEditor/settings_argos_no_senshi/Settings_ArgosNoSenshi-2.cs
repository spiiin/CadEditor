using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x010098, 1 , 13, 13, 1); }
  public string getBlocksFilename()    { return "argos_no_senshi_2.png"; }
  public int    getPictureBlocksWidth()   { return 32; }

  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}