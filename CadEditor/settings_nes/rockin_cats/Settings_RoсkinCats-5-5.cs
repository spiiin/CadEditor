using CadEditor;
using System;
using System.Drawing;
//css_include rockin_cats/RockinCats-Utils.cs;

public class Data : RockinCatsBase
{
  public override OffsetRec getScreensOffset()  { return new OffsetRec(0x1B53c, 12 , 3*2, 2, 3);    }
  public override int getVideoIndex1()          { return 0x64; }
  public override int getVideoIndex2()          { return 0x6e; }
  public override OffsetRec getBlocksOffset()   { return new OffsetRec(0x1B1bc ,1  , 0x4000); }
  public override int getBlocksCount()          { return 90; }
  
  public override OffsetRec getBigBlocksOffsetHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return new OffsetRec(0x1b37e, 1  , 0x4000); }
    if (hierarchyLevel == 1) { return new OffsetRec(0x1b45e, 1  , 0x4000); }
    if (hierarchyLevel == 2) { return new OffsetRec(0x1b4fa, 1  , 0x4000); }
    return new OffsetRec(0x0, 1  , 0x4000);
  }
  
  public override int getBigBlocksCountHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return 56; }
    if (hierarchyLevel == 1) { return 59; }
    if (hierarchyLevel == 2) { return 33;  }
    return 256;
  }
  
  public override byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x16, 0x28, 0x30, 0x0f, 0x17, 0x19, 0x0a,
      0x0f, 0x00, 0x10, 0x0a, 0x0f, 0x0c, 0x00, 0x10
    }; 
    return pallete;
  }
}