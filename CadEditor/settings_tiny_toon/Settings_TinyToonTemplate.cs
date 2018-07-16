using CadEditor;
using System.Collections.Generic;
//css_include Settings_TinyToon-Utils.cs;

public class Data
{
  public OffsetRec getPalOffset()       { return new OffsetRec(0x0, 1, 16);            }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 1   , 0x1000);    }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x0 , 1   , 0x1000);    }
  
  public OffsetRec getBigBlocksOffset() { return new /*OFFSET_REC_BIG_BLOCKS*/; }
  public OffsetRec getBlocksOffset()    { return new /*OFFSET_REC_BLOCKS*/;    }
  public OffsetRec getScreensOffset()   { return new /*OFFSET_REC_SCREEN*/;     }
  public int getBigBlocksCount()        { return /*BIG_BLOCKS_COUNT*/; }
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecsTT;}; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getTinyToonVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getTinyToonVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  public GetBigBlocksFunc     getBigBlocksFunc()     { return TinyToonUtils.getBigBlocksTT;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return TinyToonUtils.setBigBlocksTT;}
  public GetBlocksFunc        getBlocksFunc()        { return TinyToonUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()        { return TinyToonUtils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPal;}
  public SetPalFunc           setPalFunc()           { return null;}
  public GetObjectsFunc getObjectsFunc() { return TinyToonUtils.getObjectsTT; }
  public SetObjectsFunc setObjectsFunc() { return TinyToonUtils.setObjectsTT; }
  public GetLayoutFunc  getLayoutFunc()  { return TinyToonUtils.getLayoutLinearTT;   }
  public string getObjTypesPicturesDir() { return "obj_sprites_TT"; }

  public IList<LevelRec> levelRecsTT = new List<LevelRec>() 
  {
    new /*LEVEL_REC*/,
  };
  
  public int getTinyToonVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getTinyToonVideoChunk(int videoPageId)
  {
    return Utils.readVideoBankFromFile("/*VIDEO_NAME*/", videoPageId);
  }
  
  public byte[] getPal(int palId)
  {
    var pallete = new /*PALETTE*/;
    return pallete;
  }
  
  public bool isBigBlockEditorEnabled() { return true;  }
  public bool isBlockEditorEnabled()    { return true;  }
  public bool isEnemyEditorEnabled()    { return true; }
  //--------------------------------------------------------------------------------------------
}