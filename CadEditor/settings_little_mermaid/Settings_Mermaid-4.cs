using CadEditor;
using System.Collections.Generic;
//css_include Settings_CapcomBase.cs;
//css_include Settings_Mermaid-Utils.cs;
public class Data:CapcomBase
{
  public OffsetRec getPalOffset()       { return new OffsetRec(0x1DB53, 32  , 16);  }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0xE410, 1 , 0xD00); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0xE410, 1 , 0xD00); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x2010, 1 , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x1B10,  1 , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x4DD0, 32 , 0x40);   }
  public IList<LevelRec> getLevelRecs() { return levelRecs; }
  
  public override GetVideoChunkFunc    getVideoChunkFunc()    { return getLMVideoChunk; }
  public GetObjectsFunc getObjectsFunc() { return MermaidUtils.getObjectsLM; }
  public SetObjectsFunc setObjectsFunc() { return MermaidUtils.setObjectsLM; }
  public GetLayoutFunc  getLayoutFunc()  { return MermaidUtils.getLayoutLinearMermaid;   }
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x13ABA, 46, 18, 1,  0x1DAE2), 
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
  public bool isLayoutEditorEnabled()   { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
}