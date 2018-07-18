using CadEditor;
using System;
using System.Drawing;
//css_include rockin_cats/RockinCats-Utils.cs;

public class Data : RockinCatsBase
{
  public override OffsetRec getScreensOffset()  { return new OffsetRec(0x15cc1, 23 , 3*2, 3, 2);    }
  public override int getVideoIndex1()          { return 0x28; }
  public override int getVideoIndex2()          { return 0x2a; }
  public override OffsetRec getBlocksOffset()   { return new OffsetRec(0x15812 ,1  , 0x4000); }
  public override int getBlocksCount()          { return 77; }
  
  public override OffsetRec getBigBlocksOffsetHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return new OffsetRec(0x15993, 1  , 0x4000); }
    if (hierarchyLevel == 1) { return new OffsetRec(0x15b37, 1  , 0x4000); }
    if (hierarchyLevel == 2) { return new OffsetRec(0x15c57, 1  , 0x4000); }
    return new OffsetRec(0x0, 1  , 0x4000);
  }
  
  public override int getBigBlocksCountHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return 105; }
    if (hierarchyLevel == 1) { return 72; }
    if (hierarchyLevel == 2) { return 53;  }
    return 256;
  }
  
  public override byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x16, 0x28, 0x30, 0x0f, 0x0c, 0x1c, 0x2c,
      0x0f, 0x0c, 0x00, 0x10, 0x0f, 0x16, 0x27, 0x37,
    }; 
    return pallete;
  }
}