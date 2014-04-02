using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public GameType getGameType()        { return GameType.Generic; }
  public OffsetRec getScreensOffset()  { return new OffsetRec(4096, 1 , 0x20*0x40);   }
  public OffsetRec getScreensOffset2() { return new OffsetRec(0   , 1 , 0x20*0x40);   }
  public int getScreenWidth()          { return 0x20; }
  public int getScreenHeight()         { return 0x40; }
  public int getWordLen()              { return 2;}
  public int getLayersCount()          { return 2;}
  public bool isLittleEndian()         { return true; }
  public int    getPictureBlocksWidth(){ return 64; }
  public int getBigBlocksCount()       { return 542; }
  public string getBlocksFilename()    { return "gba_ffta_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
}