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
  public string getObjTypesPicturesDir() { return "little_mermaid_sprites"; }
  public OffsetRec getPalOffset()       { return new OffsetRec(0x1DB53, 32  , 16);  }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0xCC10, 1 , 0xD00); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0xCC10, 1 , 0xD00); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0xE10 , 1 , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x910 ,  1 , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x4490,32 , 0x40, 8, 8);   }
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  public override GetVideoChunkFunc    getVideoChunkFunc()    { return getLMVideoChunk; }
  public GetObjectsFunc getObjectsFunc() { return getObjectsLM2; }
  public SetObjectsFunc setObjectsFunc() { return setObjectsLM2; }
  public override GetLayoutFunc  getLayoutFunc()  { return MermaidUtils.getLayoutLinearMermaid;   }
  public override SetLayoutFunc  setLayoutFunc()  { return MermaidUtils.setLayoutLinearMermaid;   }  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x13930, 52, 17, 1,  0x1DABB),
  };
  
  public byte[] getLMVideoChunk(int videoPageId)
  {
    byte[] videoChunk = Utils.getVideoChunk(videoPageId);
    //fill first quarter of videoChunk with constant to all video memory data
    for (int i = 0; i < 16 * 16 * 4; i++)
        videoChunk[i] = Globals.romdata[0xC010 + i];
    return videoChunk;
  }
  
  public static List<ObjectList> getObjectsLM2(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount, addr = lr.objectsBeginAddr;
    int addrSx = 0x13890;
    int addrX  = 0x138C4;
    int addrY  = addr - 1 * objCount;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
        byte v = Globals.romdata[addr + i];
        byte sx = Globals.romdata[addrSx + i];
        byte x = Globals.romdata[addrX + i];
        byte y = Globals.romdata[addrY + i];
        byte sy = 0;
        var obj = new ObjectRec(v, sx, sy, x, y);
        objects.Add(obj);
    }
    return new List<ObjectList> { new ObjectList { objects = objects, name = "Objects" } };
  }

  public static bool setObjectsLM2(int levelNo, List<ObjectList> objLists)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int addrBase = lr.objectsBeginAddr;
    int objCount = lr.objCount;
    var objects = objLists[0].objects;
    
    int addrSx = 0x13890;
    int addrX  = 0x138C4;
    int addrY  = addrBase - 1 * objCount;
    for (int i = 0; i < objects.Count; i++)
    {
        var obj = objects[i];
        Globals.romdata[addrBase + i] = (byte)obj.type;
        Globals.romdata[addrY + i] = (byte)obj.y;
        Globals.romdata[addrX + i] = (byte)obj.x;
        Globals.romdata[addrSx + i] = (byte)obj.sx;
    }
    for (int i = objects.Count; i < objCount; i++)
    {
        Globals.romdata[addrBase + i] = 0xFF;
        Globals.romdata[addrY  + i] = 0xFF;
        Globals.romdata[addrX  + i] = 0xFF;
        Globals.romdata[addrSx + i] = 0xFF;
    }
    return true;
  }
  
  public bool isBigBlockEditorEnabled() { return true;  }
  public bool isBlockEditorEnabled()    { return true;  }
  public bool isEnemyEditorEnabled()    { return true; }
}