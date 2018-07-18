using CadEditor;
using System;
using System.Drawing;
//css_include rockin_cats/RockinCats-Utils.cs;

public class Data : RockinCatsBase
{
  public override OffsetRec getScreensOffset()  { return new OffsetRec(0x169fd, 3 , 3*2, 3, 2);    }
  public override int getVideoIndex1()          { return 0x30; }
  public override int getVideoIndex2()          { return 0x1E; }
  public override OffsetRec getBlocksOffset()   { return new OffsetRec(0x16910 ,1  , 0x4000); }
  public override int getBlocksCount()          { return 19; }
  
  public override OffsetRec getBigBlocksOffsetHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return new OffsetRec(0x1696f, 1  , 0x4000); }
    if (hierarchyLevel == 1) { return new OffsetRec(0x169bf, 1  , 0x4000); }
    if (hierarchyLevel == 2) { return new OffsetRec(0x169ef, 1  , 0x4000); }
    return new OffsetRec(0x0, 1  , 0x4000);
  }
  
  public override int getBigBlocksCountHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return 20; }
    if (hierarchyLevel == 1) { return 12; }
    if (hierarchyLevel == 2) { return 7;  }
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