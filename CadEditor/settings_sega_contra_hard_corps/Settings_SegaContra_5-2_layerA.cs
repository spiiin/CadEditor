using CadEditor;
using System;
using System.Collections.Generic;

public class Data 
{ 
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginSegaBackEditor.dll",
    };
  }
  public bool isUseSegaGraphics()      { return true; }
  public bool isBlockSize4x4()         { return true; }
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x0, 1 , 512*8);   }
  public int getScreenWidth()          { return 512; }
  public int getScreenHeight()         { return 8;  }
  
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChuck;}
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public GetBigBlocksFunc     getBigBlocksFunc()     { return getBigBlocks; }
  public SetBigBlocksFunc     setBigBlocksFunc()     { return setBigBlocks; }
  
  public GetPalFunc           getPalFunc()           { return readPal;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public LoadSegaBackFunc     loadSegaBackFunc()     { return loadBack;}
  public SaveSegaBackFunc     saveSegaBackFunc()     { return saveBack;}
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public GetObjectsFunc getObjectsFunc() { return getObjects; }
  public SetObjectsFunc setObjectsFunc() { return null; }
  public IList<LevelRec> getLevelRecs()  { return levelRecs;  }
  public GetLayoutFunc getLayoutFunc()   { return getLayout;  }
  
  public int getMaxObjType()             { return 0x500; }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x7F70E, 12, 1, 1, 0), 
  };
  
  private string VIDEO_NAME  = "vram_52.bin";
  private string BLOCKS_NAME = "blocks_52.bin"; //1F3A36//1F6A6E
  private string PAL_NAME    = "pal_52.bin";
  private string BACK_NAME    = "back_52.bin";  //1F529E
  
  public List<ObjectRec> getObjects(int levelNo)
  {
      int OBJ_SIZE = 14;
      var commandDict = new Dictionary<int,int> {
        { 0xFFFF , 1},
        { 0xFFFE , 5},
        { 0xFFFD , 8},
        { 0xFFFC , 8},
        { 0xFFFB , 8},
        { 0xFFFA , 5},
        { 0xFFF9 , 11},
        { 0xFFF8 , 4},
        { 0xFFF7 , 7},
        { 0xFFF6 , 1},
        { 0xFFF5 , 1},
        { 0xFFF4 , 2},
        { 0xFFF3 , 2},
        { 0xFFF2 , 1},
        { 0xFFF1 , 1},
        { 0xFFF0 , 4},
        { 0xFFEF , 6},
        { 0xFFEE , 6},
        { 0xFFED , 6},
      };
      
      LevelRec lr = ConfigScript.getLevelRec(levelNo);
      int objCount = lr.objCount, addr = lr.objectsBeginAddr;
      var objects = new List<ObjectRec>();
      int i = 0;
      int curAddr = addr;
      while (objects.Count != objCount)
      {
          int baseAddr = curAddr;
          int v = Utils.readWord(Globals.romdata, baseAddr);
          if (v < 0xFF00)
          {
            int sx, sy, x, y, baseX, baseY, fromFloor;
            sx = 0;
            sy = 0;
            x = Utils.readWord(Globals.romdata, baseAddr + 8);
            y = Utils.readWord(Globals.romdata, baseAddr + 10);
            fromFloor = Utils.readWord(Globals.romdata, baseAddr + 6);
            if (fromFloor == 0)
            {
              x = Utils.readWord(Globals.romdata, baseAddr + 2);
              x += Utils.readWord(Globals.romdata, baseAddr + 8)/2;
              y = Utils.readWord(Globals.romdata, baseAddr + 4);
              y += Utils.readWord(Globals.romdata, baseAddr + 10)/2;
            }
            var obj = new ObjectRec(v, sx, sy, x, y);
            objects.Add(obj);
            curAddr += OBJ_SIZE;
          }
          else
          {
            curAddr += commandDict[v]*2;
          }
      }
      return objects;
  }
    
  public bool setObjects(int levelNo, List<ObjectRec> objects)
  {
      return true;
  }
  
  LevelLayerData getLayout(int levelNo)
  {
    byte[] layer = new byte[1];
    layer[0] = 1;
    return new LevelLayerData(1, 1, layer);
  }
  
  public byte[] getVideoChuck(int videoPageId)
  {
    return Utils.readBinFile(VIDEO_NAME);
  }

  public byte[] getBigBlocks(int bigTileIndex)
  {
    return Utils.readBinFile(BLOCKS_NAME);
  }
  
  public void setBigBlocks(int bigTileIndex, byte[] data)
  {
    Utils.saveDataToFile(BLOCKS_NAME, data);
  }
  
  public byte[] readPal(int palNo)
  {
    return Utils.readBinFile(PAL_NAME);
  }
  
  public byte[] loadBack()
  {
    return Utils.loadDataFromFile(BACK_NAME);
  }
  
  public void saveBack(byte[] data)
  {
    Utils.saveDataToFile(BACK_NAME, data);
  }
}