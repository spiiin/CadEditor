using CadEditor;
using System;
using System.Collections.Generic;

public class Data 
{ 
  public GameType getGameType()        { return GameType.Generic; }
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x0, 1 , 22*13);   }
  public int getScreenWidth()          { return 22; }
  public int getScreenHeight()         { return 13; }
  public string getBlocksFilename()    { return "settings_sega_zombies_ate_my_neighbors/zamn_1.png"; }
  public int getWordLen()              { return 2;} 
  
  public IList<LevelRec> getLevelRecs() { return levelRecs; }
  public GetObjectsFunc getObjectsFunc()   { return getObjects;  }
  public SetObjectsFunc setObjectsFunc()   { return setObjects;  }
  public GetLayoutFunc getLayoutFunc()     { return getLayout;   }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x2AA1C, 10, 1, 1, 0x0),
  };
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return true; }
  public bool isVideoEditorEnabled()    { return false; }
  
  public int victimAddrToVictimNo(int addr)
  {
    var victimAddrs = new Dictionary<int,int> {
      { 0x11CF0 , 0x1},// Чувак с сосисками
      { 0x11E44 , 0x2},// Младенец
      { 0x11F7C , 0x3},// Девка на батуте
      { 0x12164 , 0x4},// Вояка
      { 0x1236C , 0x5},// Собака
      { 0x12550 , 0x6},// Сын доктора тонгу
      { 0x12668 , 0x7},// Училка
      { 0x128EE , 0x8},// Археолог
      { 0x12748 , 0x9},// чувак в бассейне
      { 0x129EC , 0xA},// Девка из группы поддержки
      { 0x12B7E , 0xB},// Туристы 
    };
    int victimCode = 0;
    victimAddrs.TryGetValue(addr, out victimCode);
    return victimCode;
  }
  
  
  public int victimNoToVictimAddr(int victimNo)
  {
    var victimAddrs = new Dictionary<int,int> {
      { 0x1 ,    0x11CF0 },// Чувак с сосисками
      { 0x2 ,    0x11E44 },// Младенец
      { 0x3 ,    0x11F7C },// Девка на батуте
      { 0x4 ,    0x12164 },// Вояка
      { 0x5 ,    0x1236C },// Собака
      { 0x6 ,    0x12550 },// Сын доктора тонгу
      { 0x7 ,    0x12668 },// Училка
      { 0x8 ,    0x128EE },// Археолог
      { 0x9 ,    0x12748 },// чувак в бассейне
      { 0xA ,    0x129EC },// Девка из группы поддержки
      { 0xB ,    0x12B7E },// Туристы 
    };
    int victimAddr = 0;
    victimAddrs.TryGetValue(victimNo, out victimAddr);
    return victimAddr;
  }
  
  public List<ObjectRec> getObjects(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int baseAddr = lr.objectsBeginAddr;
    int objCount = lr.objCount;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
        int x           = Utils.readWord(Globals.romdata, baseAddr + i * 12 + 0);
        int y           = Utils.readWord(Globals.romdata, baseAddr + i * 12 + 2);
        int data        = Utils.readWord(Globals.romdata, baseAddr + i * 12 + 6);
        int victimAddr  = Utils.readInt(Globals.romdata, baseAddr + i * 12 + 8);
        int victimNo    = victimAddrToVictimNo(victimAddr);
        var dataDict = new Dictionary<string,int>();
        dataDict["no"] = data;
        var obj = new ObjectRec(victimNo, 0, 0, x/2, y/2, dataDict);
        objects.Add(obj);
    }
    return objects;
  }

  public bool setObjects(int levelNo, List<ObjectRec> objects)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int baseAddr = lr.objectsBeginAddr;
    int objCount = lr.objCount;
    for (int i = 0; i < objects.Count; i++)
    {
        var obj = objects[i];
        int victimAddr = victimNoToVictimAddr(obj.type);
        Utils.writeWord(Globals.romdata, baseAddr + i * 12 + 0, obj.x*2);
        Utils.writeWord(Globals.romdata, baseAddr + i * 12 + 2, obj.y*2);
        Utils.writeWord(Globals.romdata, baseAddr + i * 12 + 4, 0);
        Utils.writeWord(Globals.romdata, baseAddr + i * 12 + 6, obj.additionalData["no"]);
        Utils.writeInt (Globals.romdata, baseAddr + i * 12 + 8, victimAddr);
    }
    for (int i = objects.Count; i < objCount; i++)
    {
        Utils.writeWord(Globals.romdata, baseAddr + i * 12 + 0, 0xFF);
        Utils.writeWord(Globals.romdata, baseAddr + i * 12 + 2, 0xFF);
        Utils.writeWord(Globals.romdata, baseAddr + i * 12 + 4, 0xFF);
        Utils.writeWord(Globals.romdata, baseAddr + i * 12 + 6, 0xFF);
        Utils.writeInt (Globals.romdata, baseAddr + i * 12 + 8, 0xFF);
    }
    return true;
  }
  
  LevelLayerData getLayout(int levelNo)
  {
    byte[] layer = new byte[1];
    layer[0] = 0;
    return new LevelLayerData(1, 1, layer);
  }
}