using CadEditor;
using System;
using System.Drawing;
//css_include rockin_cats/RockinCats-Utils.cs;

public class Data : RockinCatsBase
{
  public override OffsetRec getScreensOffset()  { return new OffsetRec(0x1401b, 4 , 3*2, 3, 2);    }
  public override int getVideoIndex1()          { return 0x4; }
  public override int getVideoIndex2()          { return 0xc; }
  public override OffsetRec getBlocksOffset()   { return new OffsetRec(0x13e7b,1  , 0x4000); }
  public override int getBlocksCount()          { return 48; }
  
  public override OffsetRec getBigBlocksOffsetHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return new OffsetRec(0x13f6b, 1  , 0x4000); }
    if (hierarchyLevel == 1) { return new OffsetRec(0x13fcb, 1  , 0x4000); }
    if (hierarchyLevel == 2) { return new OffsetRec(0x14007, 1  , 0x4000); }
    return new OffsetRec(0x0, 1  , 0x4000);
  }
  
  public override int getBigBlocksCountHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return 24; }
    if (hierarchyLevel == 1) { return 15; }
    if (hierarchyLevel == 2) { return 10;  }
    return 256;
  }
  
  public override byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x16, 0x28, 0x30, 0x0f, 0x16, 0x21, 0x2a,
      0x0f, 0x00, 0x10, 0x30, 0x0f, 0x1a, 0x27, 0x30,
    }; 
    return pallete;
  }
}