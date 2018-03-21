using CadEditor;
using System.Collections.Generic;
//css_include Settings_Flintstones-Utils.cs;
public class Data
{ 
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginChrView.dll",
    };
  }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x10   , 1  , 8*60);   }
  public OffsetRec getPalOffset()       { return new OffsetRec(0x0    , 1  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x38010, 1  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x20010, 1  , 0x1000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x1050 , 1  , 0x4000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x1F0  , 1  , 0x4000); }
  public int getScreenWidth()           { return 8; }
  public int getScreenHeight()          { return 60; }
  public bool getScreenVertical()       { return true; }
  
  public int getBlocksCount()    { return 236; }
  public int getBigBlocksCount() { return 230; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public virtual GetBlocksFunc getBlocksFunc()       { return FliUtils.getBlocks;}
  public virtual SetBlocksFunc setBlocksFunc()       { return FliUtils.setBlocks;}
  public virtual GetBigBlocksFunc getBigBlocksFunc() { return FliUtils.getBigBlocks;}
  public virtual SetBigBlocksFunc setBigBlocksFunc() { return FliUtils.setBigBlocks;}
  public GetObjectsFunc getObjectsFunc()   { return FliUtils.getObjects;  }
  public SetObjectsFunc setObjectsFunc()   { return FliUtils.setObjects;  }
  public GetObjectDictionaryFunc getObjectDictionaryFunc() { return FliUtils.getObjectDictionary; }
  public ConvertScreenTileFunc getConvertScreenTileFunc()     { return FliUtils.getConvertScreenTile; }
  public ConvertScreenTileFunc getBackConvertScreenTileFunc() { return FliUtils.getBackConvertScreenTile; }
  
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  public IList<LevelRec> levelRecs = new List<LevelRec>()
  {
    new LevelRec(0x113AF, 35, 1, 1, 0x0),
  };
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0F, 0x10, 0x21, 0x30, 0x0F, 0x29, 0x17, 0x21,
      0x0F, 0x29, 0x30, 0x10, 0x0F, 0x27, 0x17, 0x29
    }; 
    return pallete;
  }
}