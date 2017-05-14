using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x2312, 1 ,12*256);   }
  public int getScreenWidth()          { return 12; }
  public int getScreenHeight()         { return 256; }
  public string getBlocksFilename()    { return "SD_Hero_Soukessen_-_Taose!_Aku_no_Gundan_3.png"; }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
}