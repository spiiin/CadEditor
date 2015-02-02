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
  public OffsetRec getPalOffset()       { return new OffsetRec(0x415A0, 1  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x20010, 1   , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x20010, 1   , 0x1000); } //
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x40510 , 1  , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x40010 , 1  , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x40910 , 32 , 0x40);   }
  public IList<LevelRec> getLevelRecs() { return levelRecsDwd; } //
  
  public IList<LevelRec> levelRecsDwd = new List<LevelRec>() 
  {
    new LevelRec(0x10000, 1, 8, 4,  0x41510), 
  };
  public bool isBigBlockEditorEnabled() { return true;  }
  public bool isBlockEditorEnabled()    { return true;  }
  public bool isEnemyEditorEnabled()    { return false; }
}