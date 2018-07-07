using CadEditor;
using System;
//css_include shared_settings/BlockUtils.cs;

public class Data 
{ 
  //public OffsetRec getScreensOffset()  { return new OffsetRec(0x722a, 1 , 16*14);   }
  public int getScreenWidth()          { return 16; }
  public int getScreenHeight()         { return 14; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x74e3, 1  , 0x1000);  }
  public int getBlocksCount()           { return 64; }
  public int getBigBlocksCount()        { return 64; }
  public int getPalBytesAddr()          { return 0x7343; }
  
  public GetBlocksFunc        getBlocksFunc() { return BlockUtils.getBlocksLinear2x2Masked;}
  public SetBlocksFunc        setBlocksFunc() { return BlockUtils.setBlocksLinear2x2Masked;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}

  public int getLevelsCount()           { return 38; }
  public LoadScreensFunc loadScreensFunc()                  { return Utils.loadScreensDiffSize; }

  public OffsetRec[] getScreensOffsetsForLevels() {
    var ans = new OffsetRec[] {
      new OffsetRec(0x722a  , 1  , 16*14, 16, 14),
      new OffsetRec(0x4756  , 1  , 16*14, 16, 14),
      new OffsetRec(0x564e  , 1  , 16*14, 16, 14),
      new OffsetRec(0x4e2d  , 1  , 16*14, 16, 14),
      new OffsetRec(0x54f5  , 1  , 16*14, 16, 14),
      new OffsetRec(0x433e  , 1  , 16*14, 16, 14),
      new OffsetRec(0x49fe  , 1  , 16*14, 16, 14),
      new OffsetRec(0x4ccf  , 1  , 16*14, 16, 14),
      new OffsetRec(0x48a8  , 1  , 16*14, 16, 14),
      new OffsetRec(0x50f0  , 1  , 16*14, 16, 14),

      new OffsetRec(0x44a1  , 1  , 16*14, 16, 14),
      new OffsetRec(0x68db  , 1  , 16*14, 16, 14),
      new OffsetRec(0x6075  , 1  , 16*14, 16, 14),
      new OffsetRec(0x41e4  , 1  , 16*14, 16, 14),
      new OffsetRec(0x6e51  , 1  , 16*14, 16, 14),
      new OffsetRec(0x6b92  , 1  , 16*14, 16, 14),
      new OffsetRec(0x5d78  , 1  , 16*14, 16, 14),
      new OffsetRec(0x6cf5  , 1  , 16*14, 16, 14),
      new OffsetRec(0x45fe  , 1  , 16*14, 16, 14),
      new OffsetRec(0x5ee3  , 1  , 16*14, 16, 14),

      new OffsetRec(0x4b6a  , 1  , 16*14, 16, 14),
      new OffsetRec(0x4f9c  , 1  , 16*14, 16, 14),
      new OffsetRec(0x53a4  , 1  , 16*14, 16, 14),
      new OffsetRec(0x57a5  , 1  , 16*14, 16, 14),
      new OffsetRec(0x70da  , 1  , 16*14, 16, 14),
      new OffsetRec(0x6f94  , 1  , 16*14, 16, 14),
      new OffsetRec(0x7398  , 1  , 16*14, 16, 14),
      new OffsetRec(0x61ce  , 1  , 16*14, 16, 14),
      new OffsetRec(0x64a3  , 1  , 16*14, 16, 14),
      new OffsetRec(0x524d  , 1  , 16*14, 16, 14),

      new OffsetRec(0x6617  , 1  , 16*14, 16, 14),
      new OffsetRec(0x406b  , 1  , 16*14, 16, 14),
      new OffsetRec(0x6a30  , 1  , 16*14, 16, 14),
      new OffsetRec(0x6781  , 1  , 16*14, 16, 14),
      new OffsetRec(0x5a75  , 1  , 16*14, 16, 14),
      new OffsetRec(0x5c00  , 1  , 16*14, 16, 14),
      new OffsetRec(0x5902  , 1  , 16*14, 16, 14),
      new OffsetRec(0x6341  , 1  , 16*14, 16, 14),
    };
    return ans;  
  }

  //----------------------------------------------------------------------------
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal.bin");
  }
  
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr.bin", videoPageId);
  }
}