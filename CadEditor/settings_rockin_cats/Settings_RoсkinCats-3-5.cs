using CadEditor;
using System;
using System.Drawing;
//css_include Settings_RockinCats-Utils;

public class Data : RockinCatsBase
{
  public override OffsetRec getScreensOffset()  { return new OffsetRec(0x17321, 10 , 3*2);    }
  public override int getVideoIndex1()          { return 0x30; }
  public override int getVideoIndex2()          { return 0x38; }
  public override OffsetRec getBlocksOffset()   { return new OffsetRec(0x17136 ,1  , 0x4000); }
  public override int getBlocksCount()          { return 55; }
  
  public override OffsetRec getBigBlocksOffsetHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return new OffsetRec(0x17249, 1  , 0x4000); }
    if (hierarchyLevel == 1) { return new OffsetRec(0x172c9, 1  , 0x4000); }
    if (hierarchyLevel == 2) { return new OffsetRec(0x17309, 1  , 0x4000); }
    return new OffsetRec(0x0, 1  , 0x4000);
  }
  
  public override int getBigBlocksCountHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return 32; }
    if (hierarchyLevel == 1) { return 16; }
    if (hierarchyLevel == 2) { return 12;  }
    return 256;
  }
  
  public override byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x16, 0x28, 0x30, 0x0f, 0x12, 0x15, 0x30,
      0x0f, 0x12, 0x29, 0x30, 0x0f, 0x12, 0x10, 0x30,
    }; 
    return pallete;
  }
}