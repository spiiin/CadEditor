using CadEditor;
using System;
//css_include jetsons_cogswells_caper/JetsonsUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0xf89b, 1 , 8*110, 8, 110);   }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xf105 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  
  public OffsetRec getBigBlocksOffset()    { return new OffsetRec(0xf3c5 , 1  , 0x1000);  }
  public int getBigBlocksCount()           { return 256; }
  
  public int getPalBytesAddr()             { return 0xf785; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public GetBlocksFunc        getBlocksFunc()        { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc()        { return setBlocks;}
  
  public GetBigBlocksFunc     getBigBlocksFunc()     { return JetsonsUtils.getBigBlocksTT;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return JetsonsUtils.setBigBlocksTT;}
  
  //----------------------------------------------------------------------------
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr10.bin", videoPageId);
  }
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal10.bin");
  }
  
  public ObjRec[] getBlocks(int blockIndex)
  {
    return Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), 2, 2, ConfigScript.getBlocksCount(), false, false);
  }
  
  public void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
    Utils.writeBlocksLinear(blocksData, Globals.romdata, ConfigScript.getTilesAddr(blockIndex), ConfigScript.getBlocksCount(), false, false);
  }
}