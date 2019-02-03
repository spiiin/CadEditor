using CadEditor;
using System;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x00010, 1 , 16*960, 16, 960);   }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x3C10, 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 256; }
  public int getPalBytesAddr()          { return 0; }
  
  public GetBlocksFunc        getBlocksFunc() { return getBlocksLinear4x2withoutAttribT;}
  public SetBlocksFunc        setBlocksFunc() { return null;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  //blocks are intersects.
  public static ObjRec[] getBlocksLinear4x2withoutAttribT(int blockIndex)
  {
    var singleBlocks = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), 2, 2, ConfigScript.getBlocksCount(blockIndex)+1, false, true);
    var newBlocks = new ObjRec[ConfigScript.getBlocksCount(blockIndex)];
    for (int i = 0; i < newBlocks.Length; i++)
    {
      var indexes = new int[8];
      var palBytes = new int[2];
      indexes[0] = singleBlocks[i].indexes[0];
      indexes[1] = singleBlocks[i].indexes[1];
      indexes[2] = singleBlocks[i+1].indexes[0];
      indexes[3] = singleBlocks[i+1].indexes[1];
      indexes[4] = singleBlocks[i].indexes[2];
      indexes[5] = singleBlocks[i].indexes[3];
      indexes[6] = singleBlocks[i+1].indexes[2];
      indexes[7] = singleBlocks[i+1].indexes[3];
      newBlocks[i] = new ObjRec(4, 2, 0, indexes, palBytes);
    }
    return newBlocks;
  }
    
  public byte[] getPallete(int palId)
  {
    return Utils.readBinFile("pal1.bin");
  }
  
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr1.bin", videoPageId);
  }
}