using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x1D20B, 9, 8*6, 8, 6); }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public OffsetRec getVideoOffset()                          { return new OffsetRec(0x5b010, 3  , 0x1000); }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x1c69b , 1  , 0x1000);  }
  public int getBlocksCount()           { return 183; }
  public int getBigBlocksCount()        { return 183; }
  public int getPalBytesAddr()          { return 0x1c69b - 183; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  public OffsetRec getPalOffset()       { return new OffsetRec(0, 2, 16);  }
  
  //----------------------------------------------------------------------------
  
  public byte[] getPallete(int palId)
  {
      if (palId == 0)
          return Utils.readBinFile("pal8.bin");
      else
          return Utils.readBinFile("pal8-2.bin");
  }
}