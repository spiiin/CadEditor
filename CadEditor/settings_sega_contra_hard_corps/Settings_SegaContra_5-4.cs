using CadEditor;
using System;
using System.Collections.Generic;
using PluginCompressLZKN;
//css_include settings_sega_contra_hard_corps/CHC-Utils.cs;

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
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x0, 1 , 256*16, 256, 16);   }
  
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
    new LevelRec(0x7EC84, /*15*/ 62, 1, 1, 0), 
  };
  
  private string VIDEO_NAME  = "vram_54.bin";
  private string BLOCKS_NAME = "blocks_54.bin"; //1EA950//1EBAA2
  private string PAL_NAME    = "pal_54.bin";
  private string BACK_NAME    = "back_54.bin"; //1EB4F8
  
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
          new CompressParams {address = 0x1EA950, maxSize = 2983, fname = BLOCKS_NAME},
          new CompressParams {address = 0x1EB4F8, maxSize = 705, fname = BACK_NAME},
      };
  }
}