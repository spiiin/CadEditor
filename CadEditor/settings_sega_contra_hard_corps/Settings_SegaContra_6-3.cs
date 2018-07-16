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
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x0, 1 , 512*8, 512, 8);   }
  
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
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  public DrawObjectFunc getDrawObjectFunc() { return CHCUtils.drawObject; }
  
  public int getMaxObjType()             { return 0x500; }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x7EFC6, /*16*/ 34, 1, 1, 0), 
  };
  
  private string VIDEO_NAME  = "vram_63.bin";
  private string BLOCKS_NAME = "blocks_63.bin"; //1EC630//1EEAF4
  private string PAL_NAME    = "pal_63.bin";
  private string BACK_NAME    = "back_63.bin"; //1ED2CC
  
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
          new CompressParams {address = 0x1EBAA2, maxSize = 522},
          new CompressParams {address = 0x1EC630, maxSize = 3228, fname = BLOCKS_NAME},
          new CompressParams {address = 0x1ED2CC, maxSize = 1000, fname = BACK_NAME},
      };
  }
}