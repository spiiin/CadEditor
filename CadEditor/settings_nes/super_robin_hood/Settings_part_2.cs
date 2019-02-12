using CadEditor;
using System;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0x03c1e, 1 , 14*72, 14, 72);   }
  public bool getScreenVertical()      { return true; }
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk(new[] {"chr1.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x10, 1  , 0x1000);  }
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 256; }
  public int getPalBytesAddr()          { return 0x410; }
  
  public GetBlocksFunc        getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocks;}
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal1.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
  
  public static ObjRec[] getBlocks(int tileId)
  {
    int count = ConfigScript.getBlocksCount(tileId);
    var bb = Utils.readBlocksFromAlignedArrays(Globals.romdata, ConfigScript.getTilesAddr(tileId), count, false);
    var palAddr = ConfigScript.getPalBytesAddr(tileId);
    for (int i = 0; i < count; i++)
    {
        bb[i].palBytes[0] = (Globals.romdata[palAddr + i]>>2) & 0x3;
    }
    return bb;
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocks)
  {
    int count = ConfigScript.getBlocksCount(tileId);
    Utils.writeBlocksToAlignedArrays(blocks, Globals.romdata, ConfigScript.getTilesAddr(tileId), count, false, false);
    var palAddr = ConfigScript.getPalBytesAddr(tileId);
    for (int i = 0; i < count; i++)
    {
        Globals.romdata[palAddr + i] = (byte)(blocks[i].palBytes[0]<<2);
    }
  }
}