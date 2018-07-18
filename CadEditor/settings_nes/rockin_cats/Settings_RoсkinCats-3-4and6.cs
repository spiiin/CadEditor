using CadEditor;
using System;
using System.Drawing;
//css_include rockin_cats/RockinCats-Utils.cs;

public class Data : RockinCatsBase
{
  public override OffsetRec getScreensOffset()  { return new OffsetRec(0x17088, 29 , 3*2, 3, 2);    }
  public override int getVideoIndex1()          { return 0x30; }
  public override int getVideoIndex2()          { return 0x1a; }
  public override OffsetRec getBlocksOffset()   { return new OffsetRec(0x16d1b ,1  , 0x4000); }
  public override int getBlocksCount()          { return 45; }
  
  public override OffsetRec getBigBlocksOffsetHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return new OffsetRec(0x16dfc, 1  , 0x4000); }
    if (hierarchyLevel == 1) { return new OffsetRec(0x16f48, 1  , 0x4000); }
    if (hierarchyLevel == 2) { return new OffsetRec(0x17020, 1  , 0x4000); }
    return new OffsetRec(0x0, 1  , 0x4000);
  }
  
  public override int getBigBlocksCountHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return 83; }
    if (hierarchyLevel == 1) { return 54; }
    if (hierarchyLevel == 2) { return 52;  }
    return 256;
  }
  
  public override byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x16, 0x28, 0x30, 0x0f, 0x05, 0x28, 0x30,
      0x0f, 0x05, 0x16, 0x30, 0x0f, 0x12, 0x21, 0x30,
    }; 
    return pallete;
  }
}