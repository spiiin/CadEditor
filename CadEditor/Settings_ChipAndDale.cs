using CadEditor;
using System.Collections.Generic;
//css_include Settings_CapcomBase.cs;
public class Data:CapcomBase
{
  public override GameType getGameType()  { return GameType.CAD; }
   
  public OffsetRec getPalOffset()       { return new OffsetRec(0x1C354, 32  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x30010, 16  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x20010, 16  , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x36F0 , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x3AF0 , 8   , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x10   , 300 , 0x40);   }
  public IList<LevelRec> getLevelRecs() { return levelRecsCad; }
  public string[] getBlockTypeNames()   { return objTypesCad;  }
  
  public string getObjTypesPicturesDir() { return "obj_sprites_cad"; }
  
  //chip and dale specific
  public OffsetRec getBoxesBackOffset() { return new OffsetRec(0x1E909, 11  , 16);     }
  public int getLevelRecBaseOffset() { return 0x1E201; }
  public int getLevelRecDirOffset()  { return 0x10239; }
  public int getLayoutPtrAdd()       { return 0x10010; }
  public int getScrollPtrAdd()       { return 0x10010; }
  public int getDirPtrAdd()          { return 0x8010;  }
  public int getDoorRecBaseOffset()  { return 0x1E673; }
  
  public IList<LevelRec> levelRecsCad = new List<LevelRec>() 
  {
    new LevelRec(0x10388, 76),
    new LevelRec(0x10456, 31),
    new LevelRec(0x105A1, 73),
    new LevelRec(0x106D1, 57),
    new LevelRec(0x10890, 97),
    new LevelRec(0x10A1D, 74),
    new LevelRec(0x10B0E, 41),
    new LevelRec(0x10C88, 83),
    new LevelRec(0x10DB3, 53),
    new LevelRec(0x10EA1, 45),
    new LevelRec(0x10FED, 71),
  };
  
  string[] objTypesCad =
    new[]  {
        "0 (back)",
        "1 (collect)",
        "2 (platform)",
        "3 (block)",
        "4 (spikes)",
        "5 (door)",
        "6 (mask)",
        "7 (? block and go up)",
        "8 (? block and go down)",
        "9 (? block and go down)",
        "A (Block)",
        "B (Pit)",
        "C (Block)",
        "D (Block)",
        "E (throwable stone)",
        "F (throwable box)"
    };
}