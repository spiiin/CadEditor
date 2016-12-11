using CadEditor;
using System.IO;
using System.Collections.Generic;

public class MetaData 
{
  public static string makeConfig() { return File.ReadAllText(Directory.GetCurrentDirectory() + "/Settings_TinyToonTemplate.cs"); }
  public static Dictionary<string, object> getPatchDictionary()
  {
    return new Dictionary<string, object>{
        { "/*OFFSET_REC_BIG_BLOCKS*/", "OffsetRec(0x491F , 1   , 0x4000)" },
        { "/*OFFSET_REC_BLOCKS*/", "OffsetRec(0x1F1CB , 1  , 0x440)" },
        { "/*OFFSET_REC_SCREEN*/", "OffsetRec(0x470F, 11 ,   48)"},
        { "/*BIG_BLOCKS_COUNT*/", 0x40},
        { "/*LEVEL_REC*/", "LevelRec(0x14333, 15, 11, 1, 0x0)" },
        { "/*VIDEO_NAME*/", "videoBack_TT_11.bin" },
        { "/*PALETTE*/", "byte[] { 0x31, 0x0f, 0x27, 0x2a, 0x31, 0x0f, 0x27, 0x2a, 0x31, 0x0f, 0x37, 0x17, 0x31, 0x0f, 0x20, 0x38}" }
    };
  }
}