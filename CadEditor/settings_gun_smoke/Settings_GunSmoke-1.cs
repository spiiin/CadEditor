using CadEditor;
using System;
using System.Collections.Generic;
using System.Drawing;

public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(0x410, 1, 8*128);  }
  public int getScreenWidth()    { return 8; }
  public int getScreenHeight()   { return 128; }
  public int getBigBlocksCount() { return 77; }
  public string getBlocksFilename()       { return "gun_smoke_1.png"; }
  
  public GetBigTileNoFromScreenFunc getBigTileNoFromScreenFunc() { return getBigTileNoFromScreen; }
  public SetBigTileToScreenFunc     setBigTileToScreenFunc()     { return setBigTileToScreen; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public int getBigTileNoFromScreen(int[] screenData, int index)
  {
    int w = getScreenWidth();
    int h = getScreenHeight();
    int noY = index / w;
    noY = h - noY - 1;
    int noX = index % w;
    return screenData[noY*w + noX];
  }

  public void setBigTileToScreen(int[] screenData, int index, int value)
  {
    int w = getScreenWidth();
    int h = getScreenHeight();
    int noY = index / w;
    noY = h - noY - 1;
    int noX = index % w;
    screenData[noY*w + noX] = value;
  }
}