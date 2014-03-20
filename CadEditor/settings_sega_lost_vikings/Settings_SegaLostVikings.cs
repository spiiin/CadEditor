using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public GameType getGameType()        { return GameType.Generic; }
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x2B60+1, 1 , 30*29);   }
  public int getScreenWidth()          { return 30; }
  public int getScreenHeight()         { return 29; }
  public int getScreenDataStride()     { return 2;} 
  public string getBlocksFilename()    { return "settings_sega_lost_vikings/sega_lost_vikings_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
}