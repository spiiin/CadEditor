using CadEditor;
using System.Collections.Generic;
//css_include Settings_CapcomBase.cs;
public class Data:CapcomBase
{
  public OffsetRec getPalOffset()       { return new OffsetRec(0x1DB53, 32  , 16);  }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0xC010, 1 , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0xC010, 1 , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x510 , 1 , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x10 ,  1 , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x4010,64 , 0x40);   }
  public IList<LevelRec> getLevelRecs() { return levelRecs; }
  public override GetVideoChunkFunc    getVideoChunkFunc()    { return getLMVideoChunk; }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x10000, 1, 8, 8,  0x1DAA8), 
  };
  
  public byte[] getLMVideoChunk(int videoPageId)
  {
    byte[] videoChunk = Utils.getVideoChunk(videoPageId);
    //fill first quarter of videoChunk with constant to all video memory data
    for (int i = 0; i < 16 * 16 * 4; i++)
        videoChunk[i] = Globals.romdata[0xC010 + i];
    return videoChunk;
  }
  
  public bool isBigBlockEditorEnabled() { return true;  }
  public bool isBlockEditorEnabled()    { return true;  }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
}