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
      "PluginCompressLZKN.dll"
    };
  }
  public bool showDumpFileField()  { return true;  }

  public bool isUseSegaGraphics()      { return true; }
  public bool isBlockSize4x4()         { return true; }
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x0, 1 , 128*32);   }
  public int getScreenWidth()          { return 128; }
  public int getScreenHeight()         { return 32;  }
  
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChuck;}
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public GetSegaMappingFunc     getSegaMappingFunc()     { return getBigBlocks; }
  public SetSegaMappingFunc     setSegaMappingFunc()     { return setBigBlocks; }
  
  public GetPalFunc           getPalFunc()           { return readPal;}
  public SetPalFunc           setPalFunc()           { return null;}
  
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
    new LevelRec(0x7F324, /*30*/92, 1, 1, 0), 
  };
  
  private string VIDEO_NAME  = "vram_51.bin";
  private string BLOCKS_NAME = "blocks_51.bin";  //1F0B28//1F36D0
  private string PAL_NAME    = "pal_51.bin";
  
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
  
  //-------------------------------------------
  public CompressParams[] getCompressParams()
  {
      return new CompressParams[] {
          new CompressParams {address = 0x1F36D0, maxSize = 869},
          new CompressParams {address = 0x1F0B28, maxSize = 5156, fname = BLOCKS_NAME},
      };
  }
}