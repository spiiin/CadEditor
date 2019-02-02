using CadEditor;
using System;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x00a78, 1 , 30*20, 30, 20);   }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }

  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk(new[] {"chr1.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }

  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xe70, 1, 0x1000); }
  public int getBlocksCount()           { return 125; }
  
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0xcd0, 1, 0x1000); }
  public int getBigBlocksCount()        { return 104; }
  
  public static int getPalBytesAddr()          { return 0x1063; }

  public GetBlocksFunc        getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocks;}
  
  public GetBigBlocksFunc     getBigBlocksFunc()     { return Utils.getBigBlocksCapcomDefault;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return Utils.setBigBlocksCapcomDefault;}
  
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal1.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
  
  public static ObjRec[] getBlocks(int tileId)
  {
      int addr = ConfigScript.getTilesAddr(tileId);
      int count = ConfigScript.getBlocksCount(tileId);
      var blocks = Utils.readBlocksLinear(Globals.romdata, addr, 2, 2, count, false);
      for (int i = 0; i < blocks.Length; i++)
      {
        blocks[i].palBytes[0] = (Globals.romdata[getPalBytesAddr()+i] >> 4) & 0x3;
      }
      return blocks;
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocksData)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount(tileId);
    Utils.writeBlocksLinear(blocksData, Globals.romdata, addr, count, false);
    for (int i = 0; i < blocksData.Length; i++)
    {
      var oldValue = Globals.romdata[getPalBytesAddr()+i];
      //set only 2 last bits.
      //really, game uses first nibble as pallette numbers for blocks on odd positions, 
      // and second nibbles  for blocks on even positions, so there are two variants of blocks exists
      Globals.romdata[getPalBytesAddr()+i] = (byte)((oldValue & 0xCF) | (blocksData[i].palBytes[0] << 4));
    }
  }
}