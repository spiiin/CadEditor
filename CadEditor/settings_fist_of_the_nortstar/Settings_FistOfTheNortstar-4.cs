using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public GameType getGameType()        { return GameType.Generic; }
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x01D53F, 1 , 12); }
  public int getScreenWidth()          { return 12; }
  public int getScreenHeight()         { return 1; }
  public string getBlocksFilename()    { return "settings_fist_of_the_nortstar/fist_of_the_nortstar_4.png"; }
  public int    getPictureBlocksWidth()   { return 16; }

// Width
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }

  public bool getScreenVertical()         { return false; }
}