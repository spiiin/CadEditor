using CadEditor;
using System;
using System.Collections.Generic;
//css_include settings_jackal/JackalUtils.cs;

public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(0x10cf8, 1, 16*96);  }
  public int getScreenWidth()    { return 16; }
  public int getScreenHeight()   { return 96; }
  
  public GetBigTileNoFromScreenFunc getBigTileNoFromScreenFunc() { return JackalUtils.getBigTileNoFromScreen; }
  public SetBigTileToScreenFunc     setBigTileToScreenFunc()     { return JackalUtils.setBigTileToScreen; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x111e8  , 2  , 0x1000);  }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x111e8  , 2  , 0x1000);  }
  public int getBlocksCount()           { return 128; }
  public int getBigBlocksCount()        { return 128; }
  public int getPalBytesAddr()          { return 0x11988; }
  public GetBlocksFunc        getBlocksFunc() { return getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  //----------------------------------------------------------------------------
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr2.bin", videoPageId);
  }
  
  public byte[] getPallete(int palId)
  {
    return Utils.readBinFile("pal2.bin");
  }
  
  public ObjRec[] getBlocksFromTiles16Pal1(int blockIndex)
  {
      int tileAddr = (blockIndex == 0) ? ConfigScript.getTilesAddr(0) : 0x10625; //two different block sets
      var bb = Utils.readBlocksLinearTiles16Pal1(Globals.romdata, tileAddr, ConfigScript.getPalBytesAddr(), ConfigScript.getBlocksCount());
      for (int i = 0; i < bb.Length; i++)
      {
        bb[i] = JackalUtils.vertMirror(bb[i]);
      }
      return bb;
  }

  public void setBlocksFromTiles16Pal1(int blockIndex, ObjRec[] blocksData)
  {
    int tileAddr = (blockIndex == 0) ? 0x111e8 : 0x10625; //two different block sets
    for (int i = 0; i < blocksData.Length; i++)
    {
      blocksData[i] = JackalUtils.vertMirror(blocksData[i]); //TODO: remove inplace changes
    }
    Utils.writeBlocksLinearTiles16Pal1(blocksData, Globals.romdata,tileAddr, ConfigScript.getPalBytesAddr(), ConfigScript.getBlocksCount());
  }
}