using CadEditor;
using System;
using System.Collections.Generic;
using PluginCompressLZKN;
//css_include Settings_CHC-Utils.cs;

public class Data 
{ 
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginSegaBackEditor.dll",
      "PluginCompressLZKN.dll"
    };
  }
  
  public bool showDumpFileField()  { return true;  }
  
  public bool isUseSegaGraphics()      { return true; }
  public bool isBlockSize4x4()         { return true; }
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x0, 1 , 256*16);   }
  public int getScreenWidth()          { return 256; }
  public int getScreenHeight()         { return 16;  }
  
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChuck;}
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public GetSegaMappingFunc     getSegaMappingFunc()     { return getBigBlocks; }
  public SetSegaMappingFunc     setSegaMappingFunc()     { return setBigBlocks; }
  
  public GetPalFunc           getPalFunc()           { return readPal;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public LoadSegaBackFunc     loadSegaBackFunc()     { return loadBack;}
  public SaveSegaBackFunc     saveSegaBackFunc()     { return saveBack;}
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public GetObjectsFunc getObjectsFunc() { return CHCUtils.getObjects; }
  public GetObjectDictionaryFunc getObjectDictionaryFunc() { return CHCUtils.getObjectDictionary; }
  public SetObjectsFunc setObjectsFunc() { return null; }
  public IList<LevelRec> getLevelRecs()  { return levelRecs;  }
  public DrawObjectFunc getDrawObjectFunc() { return CHCUtils.drawObject; }
  
  public int getMaxObjType()             { return 0x500; }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x7EF20, /*5*/ 22, 1, 1, 0), 
  };
  
  private string VIDEO_NAME  = "vram_53.bin";
  private string BLOCKS_NAME = "blocks_53.bin"; //1EBCAC//1EC4E2
  private string PAL_NAME    = "pal_53.bin";
  private string BACK_NAME    = "back_53.bin"; //1EC15E
  
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
  
  //-------------------------------------------
  public CompressParams[] getCompressParams()
  {
      return new CompressParams[] {
          new CompressParams {address = 0x1EC4E2, maxSize = 333},
          new CompressParams {address = 0x1EBCAC, maxSize = 1201, fname = BLOCKS_NAME},
          new CompressParams {address = 0x1EC15E, maxSize = 473, fname = BACK_NAME},
      };
  }
}