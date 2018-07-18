using CadEditor;
using System;
using System.Drawing;
//css_include rockin_cats/RockinCats-Utils.cs;

public class Data : RockinCatsBase
{
  public override OffsetRec getScreensOffset()  { return new OffsetRec(0x161ee, 11 , 3*2, 3, 2);    }
  public override int getVideoIndex1()          { return 0x2e; }
  public override int getVideoIndex2()          { return 0x68; }
  public override OffsetRec getBlocksOffset()   { return new OffsetRec(0x16010 ,1  , 0x4000); }
  public override int getBlocksCount()          { return 40; }
  
  public override OffsetRec getBigBlocksOffsetHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return new OffsetRec(0x160d8, 1  , 0x4000); }
    if (hierarchyLevel == 1) { return new OffsetRec(0x16148, 1  , 0x4000); }
    if (hierarchyLevel == 2) { return new OffsetRec(0x161c0, 1  , 0x4000); }
    return new OffsetRec(0x0, 1  , 0x4000);
  }
  
  public override int getBigBlocksCountHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return 28; }
    if (hierarchyLevel == 1) { return 30; }
    if (hierarchyLevel == 2) { return 23;  }
    return 256;
  }
  
  public override byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x16, 0x28, 0x30, 0x0f, 0x0a, 0x07, 0x17,
      0x0f, 0x07, 0x00, 0x10, 0x0f, 0x02, 0x12, 0x32,
    }; 
    return pallete;
  }
}