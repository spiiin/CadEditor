using CadEditor;
using System.Collections.Generic;

public class Data
{ 
  public OffsetRec getVideoOffset()     { return new OffsetRec(0, 2  , 0x1000); }
  public OffsetRec getPalOffset()       { return new OffsetRec(0, 2  , 16);     }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0, 1  , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0, 1  , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x1480a, 32 , 16*15);   } //after last screen something like screens too
  public int getScreenWidth()    { return 16; }
  public int getScreenHeight()   { return 15; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x142EA, 1  , 0x1000);  }
  public int getBlocksCount()           { return 240; }
  public int getBigBlocksCount()        { return 240; }
  
  public GetObjectsFunc getObjectsFunc()   { return null;  }
  public SetObjectsFunc setObjectsFunc()   { return null;  }
  
  public GetBlocksFunc        getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public static ObjRec[] getBlocks(int tileId)
  {
      return Utils.readBlocksFromAlignedArrays(Globals.romdata, ConfigScript.getTilesAddr(tileId), ConfigScript.getBlocksCount());
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocksData)
  {
      Utils.writeBlocksToAlignedArrays(blocksData, Globals.romdata, ConfigScript.getTilesAddr(tileId), ConfigScript.getBlocksCount());
  }
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal5.bin");
  }
  
  public int getVideoAddress(int id)
  {
      return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
      return Utils.readVideoBankFromFile("chr5.bin", 0);
  }
 
}