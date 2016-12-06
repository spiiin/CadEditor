using CadEditor;
using System;
using System.Drawing;
//css_include Settings_RockinCats-Utils;

public class Data : RockinCatsBase
{
  public override OffsetRec getScreensOffset()  { return new OffsetRec(0x16cd9, 11 , 3*2);    }
  public override int getVideoIndex1()          { return 0x30; }
  public override int getVideoIndex2()          { return 0x36; }
  public override OffsetRec getBlocksOffset()   { return new OffsetRec(0x16a0f ,1  , 0x4000); }
  public override int getBlocksCount()          { return 48; }
  
  public override OffsetRec getBigBlocksOffsetHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return new OffsetRec(0x16aff, 1  , 0x4000); }
    if (hierarchyLevel == 1) { return new OffsetRec(0x16be7, 1  , 0x4000); }
    if (hierarchyLevel == 2) { return new OffsetRec(0x16c97, 1  , 0x4000); }
    return new OffsetRec(0x0, 1  , 0x4000);
  }
  
  public override int getBigBlocksCountHierarchy(int hierarchyLevel)
  { 
    if (hierarchyLevel == 0) { return 58; }
    if (hierarchyLevel == 1) { return 44; }
    if (hierarchyLevel == 2) { return 33;  }
    return 256;
  }
  
  public override byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x16, 0x28, 0x30, 0x0f, 0x12, 0x15, 0x35,
      0x0f, 0x00, 0x1a, 0x30, 0x0f, 0x12, 0x00, 0x30,
    }; 
    return pallete;
  }
}