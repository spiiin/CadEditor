using CadEditor;
using System;
using System.Collections.Generic;
using System.Drawing;

public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(0x10625 - 16 * 96, 1, 16*96);  }
  public int getScreenWidth()    { return 16; }
  public int getScreenHeight()   { return 96; }
  public int getBigBlocksCount() { return 256; }
  public string getBlocksFilename()       { return "jackal_1.png"; }
  
  public GetBigTileNoFromScreenFunc getBigTileNoFromScreenFunc() { return getBigTileNoFromScreen; }
  public SetBigTileToScreenFunc     setBigTileToScreenFunc()     { return setBigTileToScreen; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public int getBigTileNoFromScreen(int[] screenData, int index)
  {
    int w = getScreenWidth();
    int noY = index / w;
    noY = (noY/8)*8 + 7 - (noY%8);
    int noX = index % w;
    return screenData[noY*w + noX];
  }

  public void setBigTileToScreen(int[] screenData, int index, int value)
  {
    int w = getScreenWidth();
    int noY = index / w;
    noY = (noY/8)*8 + 7 - (noY%8);
    int noX = index % w;
    screenData[noY*w + noX] = value;
  }
}