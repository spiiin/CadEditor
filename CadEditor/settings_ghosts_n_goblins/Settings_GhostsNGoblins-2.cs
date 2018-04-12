using CadEditor;
using System;
//css_include settings_ghosts_n_goblins/GnGUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x637a, 22 , 16*15);   }
  public int getScreenWidth()          { return 16; }
  public int getScreenHeight()         { return 15; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x790a, 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 256; }
  public int getPalBytesAddr()          { return 0x7d0a; }
  
  public GetBlocksFunc        getBlocksFunc() { return GnGUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return GnGUtils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  public OffsetRec            getPalOffset()         { return new OffsetRec(0x0, 3, 16); }
  //----------------------------------------------------------------------------
  
  public byte[] getPallete(int palId)
  {
      if (palId == 0)
      {
          return Utils.readBinFile("pal2(a).bin");
      }
      else if (palId == 1)
      {
          return Utils.readBinFile("pal2(b).bin");
      }
      else
      {
          return Utils.readBinFile("pal2(c).bin");
      }
  }
  
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr2.bin", videoPageId);
  }
}