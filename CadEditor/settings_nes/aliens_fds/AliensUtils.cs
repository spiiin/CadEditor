using CadEditor;
using System;

public static class AliensUtils 
{ 
  public static int getBigTileNoFromScreen(int[] screenData, int index)
  {
    return screenData[index] & 0x3F;
  }

  public static void setBigTileToScreen(int[] screenData, int index, int value)
  {
    int oldValue = screenData[index];
    screenData[index] = (oldValue & 0xC0) | (value & 0x3F);
  }
}