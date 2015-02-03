using CadEditor;
using System.Collections.Generic;
//css_include Settings_TinyToon-Utils.cs;

public class Data
{
  public GameType getGameType()  { return GameType.TT; }
  
  public OffsetRec getPalOffset()       { return new OffsetRec(0xB1F0, 16, 16        ) ;}
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x4D10 , 1   , 0xD00  ) ;}
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x4D10 , 1   , 0xD00  ) ;}
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x986B , 1   , 0x4000 ) ;}
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x1F1CB, 1   , 0x440  ) ;}
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x97AB , 4   , 48 ) ;}
  public int getBigBlocksCount()        { return 20; }
  public int getScreenWidth()           { return 8; }
  public int getScreenHeight()          { return 6; }
  public IList<LevelRec> getLevelRecs() { return levelRecsTT; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getTinyToonVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getTinyToonVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  public GetBigBlocksFunc     getBigBlocksFunc()     { return getBigBlocksTT;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return setBigBlocksTT;}
  public GetBlocksFunc        getBlocksFunc()        { return TinyToonUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()        { return TinyToonUtils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPalleteLevel_5_1;}
  public SetPalFunc           setPalFunc()           { return null;}
  public GetObjectsFunc getObjectsFunc() { return TinyToonUtils.getObjectsTT; }
  public SetObjectsFunc setObjectsFunc() { return TinyToonUtils.setObjectsTT; }
  public string getObjTypesPicturesDir() { return "obj_sprites_TT"; }
  public GetLayoutFunc  getLayoutFunc()  { return TinyToonUtils.getLayoutLinearTT;   }
  
  public IList<LevelRec> levelRecsTT = new List<LevelRec>() 
  {
    new LevelRec(0x14621, 10, 4, 1, 0x0),
  };
  
  public int getTinyToonVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getTinyToonVideoChunk(int videoPageId)
  {
    return Utils.readVideoBankFromFile("videoBack_TT_61.bin", videoPageId);
  }
  
  public byte[] getBigBlocksTT(int bigTileIndex)
  {
    byte[] bigBlockIndexes = new byte[getBigBlocksCount() * 4];
    var bigBlocksAddr = Globals.getBigTilesAddr(bigTileIndex);
    return Utils.readDataFromAlignedArrays(Globals.romdata, bigBlocksAddr, getBigBlocksCount());
  }
  public void setBigBlocksTT(int bigTileIndex, byte[] bigBlockIndexes)
  {
    var bigBlocksAddr = Globals.getBigTilesAddr(bigTileIndex);
    Utils.writeDataToAlignedArrays(bigBlockIndexes, Globals.romdata, bigBlocksAddr, getBigBlocksCount());
  }
  
  public byte[] getPalleteLevel_5_1(int palId)
  {
    var pallete = new byte[] { 
      0x00, 0x0F, 0x00, 0x00, 0x00, 0x0f, 0x20, 0x24,
      0x00, 0x0F, 0x27, 0x17, 0x00, 0x0f, 0x20, 0x38
    }; 
    return pallete;
  }
  
  public bool isBigBlockEditorEnabled() { return true;  }
  public bool isBlockEditorEnabled()    { return true;  }
  public bool isEnemyEditorEnabled()    { return true; }
  //--------------------------------------------------------------------------------------------
}