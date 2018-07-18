using CadEditor;
using System;
using System.Drawing;
//css_include settings_rockin_cats/RockinCats-Utils.cs;

public class Data : RockinCatsBase
{
  public override OffsetRec getScreensOffset()  { return new OffsetRec(0x14515, 9 , 3*2, 3, 2);    }
  public override int getVideoIndex1()          { return 0x10; }
  public override int getVideoIndex2()          { return 0x12; }
  public override OffsetRec getBlocksOffset()   { return new OffsetRec(0x14033 ,1  , 0x4000); }
  public override int getBlocksCount()          { return 128; }
  
  public override OffsetRec getBigBlocksOffsetHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return new OffsetRec(0x142b3, 1  , 0x4000); }
    if (hierarchyLevel == 1) { return new OffsetRec(0x14427, 1  , 0x4000); }
    if (hierarchyLevel == 2) { return new OffsetRec(0x144d7, 1  , 0x4000); }
    return new OffsetRec(0x0, 1  , 0x4000);
  }
  
  public override int getBigBlocksCountHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return 93; }
    if (hierarchyLevel == 1) { return 44; }
    if (hierarchyLevel == 2) { return 31;  }
    return 256;
  }
  
  public override byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x16, 0x28, 0x30, 0x0f, 0x12, 0x18, 0x38,
      0x0f, 0x12, 0x00, 0x38, 0x0f, 0x17, 0x00, 0x30,
    }; 
    return pallete;
  }
}