using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x1BAB0, 1, 32*32); }
  public int getScreenWidth()         { return 32; }
  public int getScreenHeight()        { return 32; }
  //public int getBigBlocksCount()      { return 64; }
  public string getBlocksFilename()   { return "mm0.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  /*public ConvertScreenTileFunc getConvertScreenTileFunc()     { return getConvertScreenTile; }
  public ConvertScreenTileFunc getBackConvertScreenTileFunc() { return getBackConvertScreenTile; }
  
  public int getConvertScreenTile(int v)         { return v&0x3F;}
  public int getBackConvertScreenTile(int v)     { return v&0x3F;}*/
}