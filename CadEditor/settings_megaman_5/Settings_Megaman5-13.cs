using CadEditor;
using System.Collections.Generic;
//css_include Settings_CapcomBase.cs;
public class Data:CapcomBase
{
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginChrView.dll"
    };
  }
  
  private int LEVEL_OFFSET = 0x2000;
  private int CHR_OFFSET   = 0x1000;
  private int LEVEL_NO     = 12;
  public OffsetRec getPalOffset()       { return new OffsetRec(0x0998 + LEVEL_OFFSET* LEVEL_NO, 32  , 16);    }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x60010+ CHR_OFFSET  * LEVEL_NO, 16 , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x60010+ CHR_OFFSET  * LEVEL_NO, 16 , 0x1000); } //
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x1210 + LEVEL_OFFSET * LEVEL_NO, 1 , 0x2000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x0D10 + LEVEL_OFFSET * LEVEL_NO, 1 , 0x2000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x1610 + LEVEL_OFFSET * LEVEL_NO, 256 , 0x40, 8, 8); }
  
  public bool isBigBlockEditorEnabled() { return true;  }
  public bool isBlockEditorEnabled()    { return true;  }
  public bool isEnemyEditorEnabled()    { return false; }
}