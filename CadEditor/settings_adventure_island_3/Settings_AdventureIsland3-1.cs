using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(0xFA8, 27, 64, 8, 8);   }

  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()   { return new OffsetRec(0x21010, 1, 0x1000); }
  public OffsetRec getPalOffset()     { return new OffsetRec(0, 16, 16   ); }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xc2, 1  , 0x1000);  }
  public int getBlocksCount()           { return 94; }
  public int getBigBlocksCount()        { return 94; }
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
      0x0f, 0x31, 0x30, 0x02, 0x0f, 0x31, 0x37, 0x18,
      0x0f, 0x31, 0x2a, 0x2b, 0x0f, 0x31, 0x10, 0x00
    }; 
    return pallete;
  }
}