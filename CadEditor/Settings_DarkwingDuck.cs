using CadEditor;
using System.Collections.Generic;
//css_include Settings_CapcomBase.cs;
public class Data : CapcomBase
{ 
  public OffsetRec getPalOffset()       { return new OffsetRec(0x1C36D, 32  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x30010, 16  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x20010, 16  , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x36F0 , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x3AF0 , 8   , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x10   , 300 , 0x40);   }
  public IList<LevelRec> getLevelRecs() { return levelRecsDwd; }
  public string[] getBlockTypeNames()   { return objTypesDwd;  }
  public string   getObjTypesPicturesDir() { return "obj_sprites_dwd"; }
  public bool isMapEditorEnabled()      { return true; }
  public IList<LevelRec> levelRecsDwd = new List<LevelRec>() 
  {
    new LevelRec(0x10315, 51, 17, 4,  0x1DFA0),
    new LevelRec(0x10438, 60, 17, 4,  0x1DFE4), 
    new LevelRec(0x10584, 68, 17, 4,  0x1E028),  
    new LevelRec(0x106A0, 54, 10, 12, 0x1E06C), 
    new LevelRec(0x10816, 80, 19, 3,  0x1E0E4),  
    new LevelRec(0x10962, 63, 19, 3,  0x1E11D),  
    new LevelRec(0x10A89, 58, 19, 3,  0x1E156),  
  };
  
  string[] objTypesDwd =
    new[] {
        "0 (back)","1 (hook)","2 (platform)","3 (block)","4 (spikes)","5 (door)",
        "6","7","8","9","A","B","C","D","E","F"
    };
}