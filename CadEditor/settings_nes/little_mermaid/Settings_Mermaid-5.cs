using CadEditor;
using System.Collections.Generic;
//css_include shared_settings/CapcomBase.cs;
//css_include little_mermaid/Mermaid-Utils.cs;
public class Data:CapcomBase
{
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginChrView.dll",
      "PluginEditLayout.dll"
    };
  }
  public OffsetRec getPalOffset()       { return new OffsetRec(0x1DB53, 32  , 16);  }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0xF010, 1 , 0xD00); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0xF010, 1 , 0xD00); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x2910, 1 , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x2410,  1 , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x5250, 32 , 0x40, 8, 8);   }
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  public GetObjectsFunc getObjectsFunc() { return MermaidUtils.getObjectsLM; }
  public SetObjectsFunc setObjectsFunc() { return MermaidUtils.setObjectsLM; }
  public override GetLayoutFunc  getLayoutFunc()  { return MermaidUtils.getLayoutLinearMermaid;   }
  public override SetLayoutFunc  setLayoutFunc()  { return MermaidUtils.setLayoutLinearMermaid;   }
  public override GetVideoChunkFunc    getVideoChunkFunc()    { return getLMVideoChunk; }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x13B87, 53, 25, 1,  0x1DAF5), 
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
  public bool isEnemyEditorEnabled()    { return true; }
}