using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public GameType getGameType()        { return GameType.Generic; }
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x1, 1 , 22*13);   }
  public int getScreenWidth()          { return 22; }
  public int getScreenHeight()         { return 13; }
  public string getBlocksFilename()    { return "settings_sega_zombies_ate_my_neighbors/zamn_1.png"; }
  public int getScreenDataStride()      { return 2;} 
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
}