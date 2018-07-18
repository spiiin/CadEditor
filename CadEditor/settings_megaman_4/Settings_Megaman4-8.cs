using CadEditor;
using System.Collections.Generic;
//css_include shared_settings/CapcomBase.cs;
public class Data:CapcomBase
{
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginChrView.dll"
    };
  }
  private static int LEVEL_OFFSET = 0x2000;
  private static int CHR_OFFSET   = 0x1000;
  private static int LEVEL_NO     = 7;
  public OffsetRec getPalOffset()       { return new OffsetRec(0x415A0 + LEVEL_OFFSET* LEVEL_NO, 1  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x2E010 , 1   , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x28010 + CHR_OFFSET* LEVEL_NO, 1   , 0x1000); } //
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x40510 + LEVEL_OFFSET* LEVEL_NO , 1  , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x40010 + LEVEL_OFFSET* LEVEL_NO , 1  , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x40910 + LEVEL_OFFSET* LEVEL_NO, 32 , 0x40, 8, 8);   }
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x10000, 1, 8, 4,  0x41510 + LEVEL_OFFSET* LEVEL_NO ), 
  };
  public bool isBigBlockEditorEnabled() { return true;  }
  public bool isBlockEditorEnabled()    { return true;  }
  public bool isEnemyEditorEnabled()    { return false; }
}