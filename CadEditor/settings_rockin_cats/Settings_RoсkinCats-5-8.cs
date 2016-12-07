using CadEditor;
using System;
using System.Drawing;
//css_include Settings_RockinCats-Utils;

public class Data : RockinCatsBase
{
  public override OffsetRec getScreensOffset()  { return new OffsetRec(0x1b7bf, 11 , 3*2);    }
  public override int getVideoIndex1()          { return 0x22; }
  public override int getVideoIndex2()          { return 0x6a; }
  public override OffsetRec getBlocksOffset()   { return new OffsetRec(0x1b584 ,1  , 0x4000); }
  public override int getBlocksCount()          { return 45; }
  
  public override OffsetRec getBigBlocksOffsetHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return new OffsetRec(0x1b665, 1  , 0x4000); }
    if (hierarchyLevel == 1) { return new OffsetRec(0x1b6e9, 1  , 0x4000); }
    if (hierarchyLevel == 2) { return new OffsetRec(0x1b785, 1  , 0x4000); }
    return new OffsetRec(0x0, 1  , 0x4000);
  }
  
  public override int getBigBlocksCountHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return 33; }
    if (hierarchyLevel == 1) { return 39; }
    if (hierarchyLevel == 2) { return 29;  }
    return 256;
  }
  
  public override byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x16, 0x28, 0x30, 0x0f, 0x08, 0x18, 0x28,
      0x0f, 0x08, 0x18, 0x0f, 0x0f, 0x08, 0x00, 0x30
    }; 
    return pallete;
  }
}