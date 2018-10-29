using CadEditor;
using System.Collections.Generic;

public class Data
{ 
  public OffsetRec getVideoOffset()     { return new OffsetRec(0, 2  , 0x1000); }
  public OffsetRec getPalOffset()       { return new OffsetRec(0, 2  , 16);     }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0, 1  , 0x1000); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0, 1  , 0x4000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x8735, 16 , 16*15, 16, 15);   }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x8215, 1  , 0x1000);  }
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
      return Utils.readBlocksFromAlignedArrays(Globals.romdata, ConfigScript.getTilesAddr(tileId), ConfigScript.getBlocksCount(tileId), false);
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocksData)
  {
      Utils.writeBlocksToAlignedArrays(blocksData, Globals.romdata, ConfigScript.getTilesAddr(tileId), ConfigScript.getBlocksCount(tileId), true, false);
  }
  
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
     else if (videoPageId == 0x1)
     {
        return Utils.readVideoBankFromFile("chr2-2.bin", 0);
     }
     else
     {
       return null;
     }
  }
 
}