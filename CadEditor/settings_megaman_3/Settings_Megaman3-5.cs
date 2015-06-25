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
  private int CHR_OFFSET   = 0x400;
  private int LEVEL_NO     = 4;
  public OffsetRec getPalOffset()       { return new OffsetRec(0xA92  + LEVEL_OFFSET* LEVEL_NO     , 32 , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x40010+ CHR_OFFSET  * getVideoNo() , 1 , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x40010+ CHR_OFFSET  * getVideoNo() , 1 , 0x1000); } //
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x1710 + LEVEL_OFFSET * LEVEL_NO    , 1  , 0x2000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x1B10 + LEVEL_OFFSET * LEVEL_NO    , 1  , 0x2000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x0F10 + LEVEL_OFFSET * LEVEL_NO    , 256, 0x40);   }
  public IList<LevelRec> getLevelRecs() { return levelRecsDwd; } //
  
  public IList<LevelRec> levelRecsDwd = new List<LevelRec>() 
  {
    new LevelRec(0x10000, 1, 8, 4,  0x41510), 
  };
  
  private int getVideoNo() { return Globals.romdata[0xA90 + LEVEL_OFFSET*LEVEL_NO]; }
  
  public bool isBigBlockEditorEnabled() { return true;  }
  public bool isBlockEditorEnabled()    { return true;  }
  public bool isEnemyEditorEnabled()    { return false; }
}