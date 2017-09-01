using CadEditor;
using System.Collections.Generic;

public class Data
{ 
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginChrView.dll",
    };
  }
  public OffsetRec getPalOffset()       { return new OffsetRec(0, 1  , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0, 2  , 0x1000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x4E44 , 34 , 8*5);   }
  public int getScreenWidth()    { return 8; }
  public int getScreenHeight()   { return 5; }
  public IList<LevelRec> getLevelRecs() { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x40FC , 1  , 0x1000);  }
  public int getBlocksCount()           { return 209; }
  public int getBigBlocksCount()        { return 209; }
  public int getPalBytesAddr()          { return 0x402b; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  //----------------------------------------------------------------------------
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     if (videoPageId == 0x90)
     {
        return Utils.readVideoBankFromFile("ppu_dump1-1.bin", 0);
     }
     else if (videoPageId == 0x91)
     {
       return Utils.readVideoBankFromFile("ppu_dump1-2.bin", 0);
     }
     return new byte[0];
  }
  
  public byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x27, 0x16, 0x38, 0x0f, 0x17, 0x06, 0x38,
      0x0f, 0x11, 0x01, 0x23, 0x0f, 0x1a, 0x08, 0x39
    }; 
    return pallete;
  }
}