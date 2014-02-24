using CadEditor;
using System;
using System.Collections.Generic;

public static class ZamnUtils 
{
  public static int victimAddrToVictimNo(int addr)
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
  
  
  public static int victimNoToVictimAddr(int victimNo)
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
  
  public static int enemyAddrToEnemyNo(int addr)
  {
    var enemyAddrs = new Dictionary<int,int> {
      { 0x15296 , 0x1},// Обычный зомби
      { 0x1540C , 0x2},// Быстрый зомби
      { 0x15872 , 0x3},// Мумия
      { 0x16C86 , 0x4},// Клон
      { 0x16d6C , 0x5},// Быстрый клон
      { 0x162BE , 0x6},// Марсианин 1 
      { 0x16346 , 0x7},// Марсианин 2
      { 0x1614E , 0x8},// Оборотень
      { 0x1861A , 0x9},// Чаки
      { 0x18B92 , 0xA},// Бегающий огонь
      { 0x19C5E , 0xB},// Синий муравей
      { 0x19E10,  0xC},// Синий муравей 2 
      { 0x174B6 , 0xD},// Регбист
      { 0x17A8E , 0xE},// Слизень
      { 0x1B1A8 , 0xF},// Гриб с ногами
      { 0x1CF32 , 0x10},// Подводный монстр
      { 0x1B698 , 0x11},// Тентакль
      { 0x227B8 , 0x12},// Паучок
    };
    int enemyCode = 0;
    enemyAddrs.TryGetValue(addr, out enemyCode);
    return enemyCode;
  }
  
  public static int enemyNoToEnemyAddr(int addr)
  {
    var enemyAddrs = new Dictionary<int,int> {
      {0x1,0x15296},// Обычный зомби
      {0x2,0x1540C},// Быстрый зомби
      {0x3,0x15872},// Мумия
      {0x4,0x16C86},// Клон
      {0x5,0x16d6C},// Быстрый клон
      {0x6,0x162BE},// Марсианин 1 
      {0x7,0x16346},// Марсианин 2
      {0x8,0x1614E},// Оборотень
      {0x9,0x1861A},// Чаки
      {0xA,0x18B92},// Бегающий огонь
      {0xB,0x19C5E},// Синий муравей
      {0xC,0x19E10},// Синий муравей 2 
      {0xD,0x174B6},// Регбист
      {0xE,0x17A8E},// Слизень
      {0xF,0x1B1A8},// Гриб с ногами
      {0x10, 0x1CF32},// Подводный монстр
      {0x11, 0x1B698},// Тентакль
      {0x12, 0x227B8},// Паучок
    };
    int enemyAddr = 0;
    enemyAddrs.TryGetValue(addr, out enemyAddr);
    return enemyAddr;
  }
  
  public static List<ObjectRec> getVictimsFromArray(byte[] romdata, int baseAddr, int objCount)
  {
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
        int x           = Utils.readWord(romdata, baseAddr + i * 12 + 0);
        int y           = Utils.readWord(romdata, baseAddr + i * 12 + 2);
        int data        = Utils.readWord(romdata, baseAddr + i * 12 + 6);
        int victimAddr  = Utils.readInt(romdata, baseAddr + i * 12 + 8);
        int victimNo    = victimAddrToVictimNo(victimAddr);
        var dataDict = new Dictionary<string,int>();
        dataDict["no"] = data;
        var obj = new ObjectRec(victimNo, 0, 0, x/2, y/2, dataDict);
        objects.Add(obj);
    }
    return objects;
  }

  public static bool setVictimsToArray(List<ObjectRec> objects, byte[] romdata, int baseAddr, int objCount)
  {
    for (int i = 0; i < objects.Count; i++)
    {
        var obj = objects[i];
        int victimAddr = victimNoToVictimAddr(obj.type);
        Utils.writeWord(romdata, baseAddr + i * 12 + 0, obj.x*2);
        Utils.writeWord(romdata, baseAddr + i * 12 + 2, obj.y*2);
        Utils.writeWord(romdata, baseAddr + i * 12 + 4, 0);
        Utils.writeWord(romdata, baseAddr + i * 12 + 6, obj.additionalData["no"]);
        Utils.writeInt (romdata, baseAddr + i * 12 + 8, victimAddr);
    }
    for (int i = objects.Count; i < objCount; i++)
    {
        Utils.writeWord(romdata, baseAddr + i * 12 + 0, 0);
        Utils.writeWord(romdata, baseAddr + i * 12 + 2, 0);
        Utils.writeWord(romdata, baseAddr + i * 12 + 4, 0);
        Utils.writeWord(romdata, baseAddr + i * 12 + 6, 0);
        Utils.writeInt (romdata, baseAddr + i * 12 + 8, 0);
    }
    return true;
  }
  
  public static List<ObjectRec> getEnemiesFromArray(byte[] romdata, int baseAddr, int objCount)
  {
    var objects = new List<ObjectRec>();
    const int ENEMY_REC_LEN = 12;
    for (int i = 0; i < objCount; i++)
    {
        int r           = Utils.readWord(romdata, baseAddr + i * ENEMY_REC_LEN + 0);
        int x           = Utils.readWord(romdata, baseAddr + i * ENEMY_REC_LEN + 2);
        int y           = Utils.readWord(romdata, baseAddr + i * ENEMY_REC_LEN + 4);
        int t           = Utils.readWord(romdata, baseAddr + i * ENEMY_REC_LEN + 6);
        int enemyAddr   = Utils.readInt (romdata, baseAddr + i * ENEMY_REC_LEN + 8);
        int victimNo    = enemyAddrToEnemyNo(enemyAddr);
        var dataDict = new Dictionary<string,int>();
        dataDict["R"] = r;
        dataDict["T"] = t;
        var obj = new ObjectRec(victimNo, 0, 0, x/2, y/2, dataDict);
        objects.Add(obj);
    }
    return objects;
  }
  
  public static bool setEnemiesToArray(List<ObjectRec> objects, byte[] romdata, int baseAddr, int objCount)
  {
    const int ENEMY_REC_LEN = 12;
    for (int i = 0; i < objects.Count; i++)
    {
        var obj = objects[i];
        int enemyAddr = enemyNoToEnemyAddr(obj.type);
        Utils.writeWord(romdata, baseAddr + i * ENEMY_REC_LEN + 0, obj.additionalData["R"]);
        Utils.writeWord(romdata, baseAddr + i * ENEMY_REC_LEN + 2, obj.x*2);
        Utils.writeWord(romdata, baseAddr + i * ENEMY_REC_LEN + 4, obj.y*2);
        Utils.writeWord(romdata, baseAddr + i * ENEMY_REC_LEN + 6, obj.additionalData["T"]);
        Utils.writeInt (romdata, baseAddr + i * ENEMY_REC_LEN + 8, enemyAddr);
    }
    for (int i = objects.Count; i < objCount; i++)
    {
        Utils.writeWord(romdata, baseAddr + i * ENEMY_REC_LEN + 0, 0x00);
        Utils.writeWord(romdata, baseAddr + i * ENEMY_REC_LEN + 2, 0x00);
        Utils.writeWord(romdata, baseAddr + i * ENEMY_REC_LEN + 4, 0x00);
        Utils.writeWord(romdata, baseAddr + i * ENEMY_REC_LEN + 6, 0x00);
        Utils.writeInt (romdata, baseAddr + i * ENEMY_REC_LEN + 8, 0);
    }
    return true;
  }
  
  public static List<ObjectRec> getItemsFromArray(byte[] romdata, int baseAddr, int objCount)
  {
    var objects = new List<ObjectRec>();
    const int ITEM_REC_LEN = 6;
    for (int i = 0; i < objCount; i++)
    {
        int x           = Utils.readWord(romdata, baseAddr + i * ITEM_REC_LEN + 0);
        int y           = Utils.readWord(romdata, baseAddr + i * ITEM_REC_LEN + 2);
        int t           = Utils.readWord(romdata, baseAddr + i * ITEM_REC_LEN + 4);
        var obj = new ObjectRec(t, 0, 0, x/2, y/2);
        objects.Add(obj);
    }
    return objects;
  }
  
  public static bool setItemsToArray(List<ObjectRec> objects, byte[] romdata, int baseAddr, int objCount)
  {
    const int ITEM_REC_LEN = 6;
    for (int i = 0; i < objects.Count; i++)
    {
        var obj = objects[i];
        Utils.writeWord(romdata, baseAddr + i * ITEM_REC_LEN + 0, obj.x*2);
        Utils.writeWord(romdata, baseAddr + i * ITEM_REC_LEN + 2, obj.y*2);
        Utils.writeWord(romdata, baseAddr + i * ITEM_REC_LEN + 4, obj.type);
    }
    for (int i = objects.Count; i < objCount; i++)
    {
        Utils.writeWord(romdata, baseAddr + i * ITEM_REC_LEN + 0, 0x00);
        Utils.writeWord(romdata, baseAddr + i * ITEM_REC_LEN + 2, 0x00);
        Utils.writeWord(romdata, baseAddr + i * ITEM_REC_LEN + 4, 0x00);
    }
    return true;
  }
  
  //-----------------------------------------------------------------------------------------------
  public static List<ObjectRec> getVictimsFromRom(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int baseAddr = lr.objectsBeginAddr;
    int objCount = lr.objCount;
    return getVictimsFromArray(Globals.romdata, baseAddr, objCount);
  }
  
  public static List<ObjectRec> getVictimsFromFile(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    byte[] data = Utils.loadDataFromFile("settings_sega_zombies_ate_my_neighbors/victims.bin");
    return getVictimsFromArray(data, 0, lr.objCount);
  }
  
  public static bool setVictimsToRom(int levelNo, List<ObjectRec> objects)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int baseAddr = lr.objectsBeginAddr;
    int objCount = lr.objCount;
    return setVictimsToArray(objects, Globals.romdata, baseAddr, objCount);
  }
  
  public static bool setVictimsToFile(int levelNo, List<ObjectRec> objects)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int baseAddr = 0;
    int objCount = lr.objCount;
    byte[] data = new byte[objCount*12];
    setVictimsToArray(objects, data, baseAddr, objCount);
    Utils.saveDataToFile("settings_sega_zombies_ate_my_neighbors/victims.bin", data);
    return true;
  }
  
  public static List<ObjectRec> getEnemiesFromRom(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int baseAddr = lr.objectsBeginAddr;
    int objCount = lr.objCount;
    return getEnemiesFromArray(Globals.romdata, baseAddr, objCount);
  }
  
  public static List<ObjectRec> getEnemiesFromFile(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    byte[] data = Utils.loadDataFromFile("settings_sega_zombies_ate_my_neighbors/enemies.bin");
    return getEnemiesFromArray(data, 0, lr.objCount);
  }
  
  public static bool setEnemiesToRom(int levelNo, List<ObjectRec> objects)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int baseAddr = lr.objectsBeginAddr;
    int objCount = lr.objCount;
    return setEnemiesToArray(objects, Globals.romdata, baseAddr, objCount);
  }
  
  public static bool setEnemiesToFile(int levelNo, List<ObjectRec> objects)
  {
    const int ENEMY_REC_LEN = 12;
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int baseAddr = 0;
    int objCount = lr.objCount;
    byte[] data = new byte[objCount * ENEMY_REC_LEN];
    setEnemiesToArray(objects, data, baseAddr, objCount);
    Utils.saveDataToFile("settings_sega_zombies_ate_my_neighbors/enemies.bin", data);
    return true;
  }
  
  public static List<ObjectRec> getItemsFromRom(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int baseAddr = lr.objectsBeginAddr;
    int objCount = lr.objCount;
    return getItemsFromArray(Globals.romdata, baseAddr, objCount);
  }
  
  public static List<ObjectRec> getItemsFromFile(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    byte[] data = Utils.loadDataFromFile("settings_sega_zombies_ate_my_neighbors/items.bin");
    return getItemsFromArray(data, 0, lr.objCount);
  }
  
  public static bool setItemsToRom(int levelNo, List<ObjectRec> objects)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int baseAddr = lr.objectsBeginAddr;
    int objCount = lr.objCount;
    return setItemsToArray(objects, Globals.romdata, baseAddr, objCount);
  }
  
  public static bool setItemsToFile(int levelNo, List<ObjectRec> objects)
  {
    const int ITEMS_REC_LEN = 6;
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int baseAddr = 0;
    int objCount = lr.objCount;
    byte[] data = new byte[objCount * ITEMS_REC_LEN];
    setItemsToArray(objects, data, baseAddr, objCount);
    Utils.saveDataToFile("settings_sega_zombies_ate_my_neighbors/items.bin", data);
    return true;
  }
  
  
  public static LevelLayerData getSingleLayout(int levelNo)
  {
    byte[] layer = new byte[1];
    layer[0] = 0;
    return new LevelLayerData(1, 1, layer);
  }
  
}