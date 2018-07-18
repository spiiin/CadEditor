using CadEditor;
using System.Collections.Generic;
//css_include flintstones_rescue_of_dino_and_hoppy/Flintstones-Utils.cs;
public class Data
{ 
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginChrView.dll",
    };
  }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x1519 + 64*8 , 1  , 8*12, 8, 12);   }
  public OffsetRec getPalOffset()       { return new OffsetRec(0x0    , 1  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x3B010, 1  , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x20010, 1  , 0x1000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x24F9 , 1  , 0x4000); }
  public int getPhysicsBytesAddr()      { return 0x1C2F0; }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x1779 , 1  , 0x4000); }
  public bool getScreenVertical()         { return true; }
  
  public string[] getBlockTypeNames()   { return FliUtils.getBlockTypeNames();  }
  
  public int getBlocksCount()    { return 179; }
  public int getBigBlocksCount() { return 216; }
  
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
  public SortObjectsFunc sortObjectsFunc() { return FliUtils.sortObjects; }
  public GetObjectDictionaryFunc getObjectDictionaryFunc() { return FliUtils.getObjectDictionary; }
  public ConvertScreenTileFunc getConvertScreenTileFunc()     { return FliUtils.getConvertScreenTile; }
  public ConvertScreenTileFunc getBackConvertScreenTileFunc() { return FliUtils.getBackConvertScreenTile; }
  
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x118CB, 6, 1, 1, 0x0),
  };
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0F, 0x02, 0x12, 0x22, 0x0F, 0x12, 0x19, 0x29,
      0x0F, 0x02, 0x12, 0x35, 0x0F, 0x07, 0x17, 0x27
    }; 
    return pallete;
  }
}