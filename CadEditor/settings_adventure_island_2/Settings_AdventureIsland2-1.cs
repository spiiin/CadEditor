using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(0xE98, 35, 64, 8, 8);   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()   { return new OffsetRec(0x21010, 1, 0x1000); }
  public OffsetRec getPalOffset()     { return new OffsetRec(0, 16, 16   ); }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xbf, 1  , 0x1000);  }
  public int getBlocksCount()           { return 91; }
  public int getBigBlocksCount()        { return 91; }
  public int getPalBytesAddr()          { return 0x64; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()         { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()            { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()            { return Utils.setVideoChunk; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  //-------------------------------------------------------------------------------
  
  
  public byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x12, 0x20, 0x21, 0x0f, 0x05, 0x17, 0x21,
      0x0f, 0x16, 0x27, 0x21, 0x0f, 0x1a, 0x27, 0x21
    }; 
    return pallete;
  }
}