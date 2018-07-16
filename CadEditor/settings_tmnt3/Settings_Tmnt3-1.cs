using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x300B1, 15, 64, 8, 8); }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public OffsetRec getVideoOffset()                          { return new OffsetRec(0x6d010, 2  , 0x1000); }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x30471 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 220; }
  public int getBigBlocksCount()        { return 220; }
  public int getPalBytesAddr()          { return 0x31231; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  public OffsetRec getPalOffset()       { return new OffsetRec(0, 2, 16);  }
  
  //----------------------------------------------------------------------------
  
  
  public byte[] getPallete(int palId)
  {
      if (palId == 0)
          return Utils.readBinFile("pal1.bin");
      else
          return Utils.readBinFile("pal1-2.bin");
  }
}