using CadEditor;
using System;
using System.Drawing;
//css_include rockin_cats/RockinCats-Utils.cs;

public class Data : RockinCatsBase
{
  public override OffsetRec getScreensOffset()  { return new OffsetRec(0x15499, 10 , 3*2, 3, 2);    }
  public override int getVideoIndex1()          { return 0x22; }
  public override int getVideoIndex2()          { return 0x24; }
  public override OffsetRec getBlocksOffset()   { return new OffsetRec(0x15142 ,1  , 0x4000); }
  public override int getBlocksCount()          { return 79; }
  
  public override OffsetRec getBigBlocksOffsetHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return new OffsetRec(0x152cd, 1  , 0x4000); }
    if (hierarchyLevel == 1) { return new OffsetRec(0x153d5, 1  , 0x4000); }
    if (hierarchyLevel == 2) { return new OffsetRec(0x15469, 1  , 0x4000); }
    return new OffsetRec(0x0, 1  , 0x4000);
  }
  
  public override int getBigBlocksCountHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return 66; }
    if (hierarchyLevel == 1) { return 37; }
    if (hierarchyLevel == 2) { return 24;  }
    return 256;
  }
  
  public override byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x16, 0x28, 0x30, 0x0f, 0x02, 0x10, 0x00,
      0x0f, 0x02, 0x0a, 0x1a, 0x0f, 0x02, 0x12, 0x31,
    }; 
    return pallete;
  }
}