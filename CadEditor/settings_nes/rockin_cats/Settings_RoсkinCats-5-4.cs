using CadEditor;
using System;
using System.Drawing;
//css_include rockin_cats/RockinCats-Utils.cs;

public class Data : RockinCatsBase
{
  public override OffsetRec getScreensOffset()  { return new OffsetRec(0x15e95, 10 , 3*2, 2, 3);    }
  public override int getVideoIndex1()          { return 0x64; }
  public override int getVideoIndex2()          { return 0x18; }
  public override OffsetRec getBlocksOffset()   { return new OffsetRec(0x15d4b ,1  , 0x4000); }
  public override int getBlocksCount()          { return 18; }
  
  public override OffsetRec getBigBlocksOffsetHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return new OffsetRec(0x15da5, 1  , 0x4000); }
    if (hierarchyLevel == 1) { return new OffsetRec(0x15df9, 1  , 0x4000); }
    if (hierarchyLevel == 2) { return new OffsetRec(0x15e69, 1  , 0x4000); }
    return new OffsetRec(0x0, 1  , 0x4000);
  }
  
  public override int getBigBlocksCountHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return 21; }
    if (hierarchyLevel == 1) { return 28; }
    if (hierarchyLevel == 2) { return 22;  }
    return 256;
  }
  
  public override byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x16, 0x28, 0x30, 0x0f, 0x08, 0x18, 0x38,
      0x0f, 0x00, 0x10, 0x06, 0x0f, 0x0c, 0x1c, 0x3c
    }; 
    return pallete;
  }
}