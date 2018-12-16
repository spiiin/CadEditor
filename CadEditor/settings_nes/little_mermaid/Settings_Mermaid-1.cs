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
      "PluginEditLayout.dll",
      "PluginAnimEditor.dll",
    };
  }
  public string getObjTypesPicturesDir() { return "little_mermaid_sprites"; }
  public OffsetRec getPalOffset()       { return new OffsetRec(0x1DB53, 32  , 16);  }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0xC010, 1 , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x8010, 10, 0x400); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x510 , 1 , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x10 ,  1 , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x4010,64 , 0x40, 8, 8);   }
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  public override GetVideoChunkFunc    getVideoChunkFunc()    { return getLMVideoChunk; }
  public GetObjectsFunc getObjectsFunc() { return MermaidUtils.getObjectsLM; }
  public SetObjectsFunc setObjectsFunc() { return MermaidUtils.setObjectsLM; }
  public override GetLayoutFunc  getLayoutFunc()  { return MermaidUtils.getLayoutLinearMermaid;   }
  public override SetLayoutFunc  setLayoutFunc()  { return MermaidUtils.setLayoutLinearMermaid;   }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x13871, 31, 16, 1,  0x1DAA8), 
  };
  
  public byte[] getLMVideoChunk(int videoPageId)
  {
    byte[] videoChunk = Utils.getVideoChunk(videoPageId);
    
    //if background bank, fill first quarter of videoChunk with constant to all video memory data
    if (videoPageId>=0)
      for (int i = 0; i < 16 * 16 * 4; i++)
        videoChunk[i] = Globals.romdata[0xC010 + i];
    return videoChunk;
  }
  
  public bool isBigBlockEditorEnabled() { return true;  }
  public bool isBlockEditorEnabled()    { return true;  }
  public bool isEnemyEditorEnabled()    { return true; }
  
  //Anim Editor
  public static int getAnimCount()   { return 139; }
  public static int getAnimAddrHi()  { return Utils.getCapcomAnimAddr(4, 0xA7A7); }
  public static int getAnimAddrLo()  { return Utils.getCapcomAnimAddr(4, 0xA71C); }
  public static int getFrameCount()  { return 251; }
  public static int getFrameAddrHi() { return Utils.getCapcomAnimAddr(4, 0x93B6); }
  public static int getFrameAddrLo() { return Utils.getCapcomAnimAddr(4, 0x92BB); }
  public static int getCoordCount()  { return 184; }
  public static int getCoordAddrHi() { return Utils.getCapcomAnimAddr(4, 0xA3D5); }
  public static int getCoordAddrLo() { return Utils.getCapcomAnimAddr(4, 0xA31D); }
  public static byte[] getAnimPal()  { return new byte[] { 0xF, 0x5, 0x14, 0x36, 0xF, 0xB, 0x2B, 0x20, 0xF, 0x0, 0x10, 0x20, 0xF, 0x4, 0x26, 0x36  }; }
  public static int getAnimBankNo()  { return 4;}
}