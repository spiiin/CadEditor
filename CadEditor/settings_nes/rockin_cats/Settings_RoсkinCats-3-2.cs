using CadEditor;
using System;
using System.Drawing;
//css_include rockin_cats/RockinCats-Utils.cs;

public class Data : RockinCatsBase
{
  public override OffsetRec getScreensOffset()  { return new OffsetRec(0x168e0, 8 , 3*2, 3, 2);    }
  public override int getVideoIndex1()          { return 0x30; }
  public override int getVideoIndex2()          { return 0x34; }
  public override OffsetRec getBlocksOffset()   { return new OffsetRec(0x16703 ,1  , 0x4000); }
  public override int getBlocksCount()          { return 41; }
  
  public override OffsetRec getBigBlocksOffsetHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return new OffsetRec(0x167d0, 1  , 0x4000); }
    if (hierarchyLevel == 1) { return new OffsetRec(0x1685c, 1  , 0x4000); }
    if (hierarchyLevel == 2) { return new OffsetRec(0x168c0, 1  , 0x4000); }
    return new OffsetRec(0x0, 1  , 0x4000);
  }
  
  public override int getBigBlocksCountHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return 35; }
    if (hierarchyLevel == 1) { return 25; }
    if (hierarchyLevel == 2) { return 16;  }
    return 256;
  }
  
  public override byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x16, 0x28, 0x30, 0x0f, 0x19, 0x18, 0x0b,
      0x0f, 0x19, 0x00, 0x10, 0x0f, 0x19, 0x18, 0x08,
    }; 
    return pallete;
  }
}