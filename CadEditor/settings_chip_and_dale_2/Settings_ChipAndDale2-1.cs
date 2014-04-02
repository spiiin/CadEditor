using CadEditor;
using System.Collections.Generic;
//css_include Settings_CapcomBase.cs;
public class Data : CapcomBase
{ 
  public OffsetRec getPalOffset()       { return new OffsetRec(0, 32  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0, 16  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0, 16  , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0 , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0 , 8   , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x5D0   , 3 , 16*15);   }
  public override int getScreenWidth()    { return 16; }
  public override int getScreenHeight()   { return 15; }
  public string getBlocksFilename() { return "cad2_1.png"; }
  public IList<LevelRec> getLevelRecs() { return levelRecsCad2; }
  public GetLayoutFunc getLayoutFunc()  { return getLayoutCad2; }
  public GetObjectsFunc getObjectsFunc()   { return getObjectsCad2; }
  public SetObjectsFunc setObjectsFunc()   { return setObjectsCad2; }
  
  public IList<LevelRec> levelRecsCad2 = new List<LevelRec>() 
  {
    new LevelRec(0xE401, 12, 8, 1, 0x0),
  };
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return true; }
  public bool isVideoEditorEnabled()    { return false; }
  
  LevelLayerData getLayoutCad2(int levelNo)
  {
    int width = 8;
    byte[] layer = new byte[width];
    layer[0] = 0;
    layer[1] = 1;
    layer[2] = 2;
    layer[3] = 0;
    layer[4] = 1;
    layer[5] = 2;
    layer[6] = 0;
    layer[7] = 1;
    return new LevelLayerData(width, 1, layer);
  }
  
  public List<ObjectRec> getObjectsCad2(int levelNo)
  {
    int addr = 0xE401;
    int objCount = 12;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
      int v  = Globals.romdata[addr + i * 3 + 2];
      int xx = Globals.romdata[addr + i * 3 + 0];
      int yy = Globals.romdata[addr + i * 3 + 1];
      int sx = xx >> 4;
      int sy = 0;
      int x = (xx & 0x0F) * 32;
      int y = (yy & 0x0F) * 32;
      var obj = new ObjectRec(v, sx, sy, x, y);
      objects.Add(obj);
    }
    return objects;
  }

  public bool setObjectsCad2(int levelNo, List<ObjectRec> objects)
  {
    int addrBase = 0xE401;
    int objCount = 12;
    for (int i = 0; i < objects.Count; i++)
    {
      var obj = objects[i];
      Globals.romdata[addrBase + i * 3 + 2] = (byte)obj.type;
      Globals.romdata[addrBase + i * 3 + 0] = (byte)((obj.x / 32) | (obj.sx << 4));
      Globals.romdata[addrBase + i * 3 + 1] = (byte)((obj.y / 32) | (2 << 4));
    }
    for (int i = objects.Count; i < objCount; i++)
    {
      Globals.romdata[addrBase + i * 3 + 2] = 0xFF;
      Globals.romdata[addrBase + i * 3 + 0] = 0xFF;
      Globals.romdata[addrBase + i * 3 + 1] = 0xFF;
    }
    return true;
  }
}