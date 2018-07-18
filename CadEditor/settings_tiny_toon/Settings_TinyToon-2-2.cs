using CadEditor;
using System.Collections.Generic;
//css_include settings_tiny_toon/TinyToon-Utils.cs;

public class Data
{ 
  public OffsetRec getPalOffset()       { return new OffsetRec(0xB1F0, 16, 16        ) ;}
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x4D10 , 1   , 0xD00  ) ;}
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x4D10 , 1   , 0xD00  ) ;}
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x5D88 , 1   , 0x4000 ) ;}
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x1F1CB, 1   , 0x440  ) ;}
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x5768 , 14   , 112     , 8, 14) ;}
  public int getBigBlocksCount()        { return 86; }
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecsTT;}; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getTinyToonVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getTinyToonVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  public GetBigBlocksFunc     getBigBlocksFunc()     { return TinyToonUtils.getBigBlocksTT;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return TinyToonUtils.setBigBlocksTT;}
  public GetBlocksFunc        getBlocksFunc()        { return TinyToonUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()        { return TinyToonUtils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPalleteLevel_2_1;}
  public SetPalFunc           setPalFunc()           { return null;}
  public GetObjectsFunc getObjectsFunc() { return TinyToonUtils.getObjectsTT; }
  public SetObjectsFunc setObjectsFunc() { return TinyToonUtils.setObjectsTT; }
  public string getObjTypesPicturesDir() { return "obj_sprites_TT"; }
  public GetLayoutFunc  getLayoutFunc()  { return TinyToonUtils.getLayoutLinearTT;   }
  
  public IList<LevelRec> levelRecsTT = new List<LevelRec>() 
  {
    new LevelRec(0x14404, 37, 14, 1, 0x0),
  };
  
  public int getTinyToonVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getTinyToonVideoChunk(int videoPageId)
  {
    return Utils.readVideoBankFromFile("videoBack_TT_21.bin", videoPageId);
  }
  
  public byte[] getPalleteLevel_2_1(int palId)
  {
    var pallete = new byte[] { 
      0x12, 0x0F, 0x26, 0x2A, 0x12, 0x0f, 0x20, 0x2B,
      0x12, 0x0f, 0x20, 0x2C, 0x12, 0x0f, 0x20, 0x38
    }; 
    return pallete;
  }
  
  public bool isBigBlockEditorEnabled() { return true;  }
  public bool isBlockEditorEnabled()    { return true;  }
  public bool isEnemyEditorEnabled()    { return true; }
  //--------------------------------------------------------------------------------------------
}