using CadEditor;
using System;
//css_include Settings_CapcomBase.cs;

public class Data : CapcomBase
{ 
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginChrView.dll",
    };
  }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0xB010, 5 , 8*8);   }
  public OffsetRec getPalOffset()       { return new OffsetRec(0xAEEC , 1  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x34010, 1  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x20010, 1  , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0xB710 , 1  , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xBB10, 1  , 0x4000); }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
}