using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public GameType getGameType()        { return GameType.Generic; }
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x010098, 1 , 13); }
  public int getScreenWidth()          { return 13; }
  public int getScreenHeight()         { return 1; }
  public string getBlocksFilename()    { return "settings_argos_no_senshi/argos_no_senshi_2.png"; }
  public int    getPictureBlocksWidth()   { return 32; }

  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
}