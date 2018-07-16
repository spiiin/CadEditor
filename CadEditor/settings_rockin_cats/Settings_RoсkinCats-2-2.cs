using CadEditor;
using System;
using System.Drawing;
//css_include Settings_RockinCats-Utils;

public class Data : RockinCatsBase
{
  public override OffsetRec getScreensOffset()  { return new OffsetRec(0x157b2, 16 , 3*2, 3, 2);    }
  public override int getVideoIndex1()          { return 0x2e; }
  public override int getVideoIndex2()          { return 0x26; }
  public override OffsetRec getBlocksOffset()   { return new OffsetRec(0x154d5 ,1  , 0x4000); }
  public override int getBlocksCount()          { return 83; }
  
  public override OffsetRec getBigBlocksOffsetHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return new OffsetRec(0x15674, 1  , 0x4000); }
    if (hierarchyLevel == 1) { return new OffsetRec(0x15720, 1  , 0x4000); }
    if (hierarchyLevel == 2) { return new OffsetRec(0x1578c, 1  , 0x4000); }
    return new OffsetRec(0x0, 1  , 0x4000);
  }
  
  public override int getBigBlocksCountHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return 43; }
    if (hierarchyLevel == 1) { return 27; }
    if (hierarchyLevel == 2) { return 19;  }
    return 256;
  }
  
  public override byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x16, 0x28, 0x30, 0x0f, 0x12, 0x10, 0x00,
      0x0f, 0x12, 0x10, 0x30, 0x0f, 0x12, 0x16, 0x26,
    }; 
    return pallete;
  }
}