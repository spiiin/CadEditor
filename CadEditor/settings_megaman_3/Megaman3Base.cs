using CadEditor;
using System.Collections.Generic;
//css_include Settings_CapcomBase.cs;
public class Megaman3Base:CapcomBase
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
  protected virtual int LEVEL_NO()      { return 0; }
  public OffsetRec getPalOffset()       { return new OffsetRec(0xA92  + LEVEL_OFFSET* LEVEL_NO()     , 32 , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x40010+ CHR_OFFSET  * getVideoNo() , 1 , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x40010+ CHR_OFFSET  * getVideoNo() , 1 , 0x1000); } //
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x1710 + LEVEL_OFFSET * LEVEL_NO()    , 1  , 0x2000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x1B10 + LEVEL_OFFSET * LEVEL_NO()    , 1  , 0x2000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x0F10 + LEVEL_OFFSET * LEVEL_NO()    , 256, 0x40, 8, 8);   }
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x10000, 1, 8, 4,  0x41510), 
  };
  
  private int getVideoNo() { return Globals.romdata[0xA90 + LEVEL_OFFSET*LEVEL_NO()]; }
  
  public bool isBigBlockEditorEnabled() { return true;  }
  public bool isBlockEditorEnabled()    { return true;  }
  public bool isEnemyEditorEnabled()    { return false; }
}