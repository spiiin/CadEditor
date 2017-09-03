using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x1903a, 26 , 8*8); }
  public int getScreenWidth()          { return 8; }
  public int getScreenHeight()         { return 8; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x196ba, 1  , 0x1000);  }
  public int getBlocksCount()           { return 255; }
  public int getBigBlocksCount()        { return 255; }
  public int getPalBytesAddr()          { return 0x196ba + 255*16; }
  
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0, 5, 1); }
  public OffsetRec getPalOffset()       { return new OffsetRec(0, 2, 1); }
  
  //----------------------------------------------------------------------------
  
  public byte[] getPallete(int palId)
  {
      if (palId == 0)
      {
          return Utils.readBinFile("pal2.bin");
      }
      else
      {
          return Utils.readBinFile("pal2-2.bin");
      }
  }
  
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
          if (videoPageId == 0)
     {
        return Utils.readVideoBankFromFile("chr2.bin", 0);
     }
     else if (videoPageId == 1)
     {
        return Utils.readVideoBankFromFile("chr2-2.bin", 0);
     }
     else if (videoPageId == 2)
     {
         return Utils.readVideoBankFromFile("chr2-3.bin", 0);
     }
     else if (videoPageId == 3)
     {
         return Utils.readVideoBankFromFile("chr2-4.bin", 0);
     }
     else if (videoPageId == 4)
     {
         return Utils.readVideoBankFromFile("chr2-5.bin", 0);
     }
     else
     {
         return null;
     }
  }
}