using CadEditor;
using System;
using System.Drawing;
using PluginMapEditor;

public class Data 
{ 
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginMapEditor.dll",
    };
  }
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x8376, 24 , 16*15); }
  public int getScreenWidth()          { return 16; }
  public int getScreenHeight()         { return 15; }
  
  public int getWordLen()              { return 2;}
  public bool isLittleEndian()          { return true;}
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()   { return new OffsetRec(0x55010, 1, 0x1000); }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x4AA3, 1  , 0x1000);  }
  public int getBlocksCount()           { return 1024; }
  public int getBigBlocksCount()        { return 1024; }
  public int getPalBytesAddr()          { return 0x0; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()         { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()            { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()            { return Utils.setVideoChunk; }
  public GetBlocksFunc getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc setBlocksFunc() { return setBlocks;}
  
  public GetPalFunc getPalFunc()  { return Utils.getPalleteLinear;}
  public SetPalFunc setPalFunc()  { return Utils.setPalleteLinear;}
  public OffsetRec getPalOffset() { return new OffsetRec(0x29721 , 1  , 16  ); }
  
  //-------------------------------------------------------------------------------------------------------------------
  public MapInfo[] getMapsInfo() { return mapsInfo; }
  public LoadMapFunc getLoadMapFunc() { return MapUtils.loadMapContraSpirits; }
  public SaveMapFunc getSaveMapFunc() { return MapUtils.saveMapContraSpirits; }
  public bool isMapReadOnly()         { return true; }

  MapInfo[] mapsInfo = new MapInfo[]
  { 
      new MapInfo(){ dataAddr = 0x8376, palAddr = 0x29721, videoNo = 0, attribsAddr = 0xB296 },
  };
  
  //-------------------------------------------------------------------------------------------------------------------
  public ObjRec[] getBlocks(int blockIndex)
  {
      var romdata = Globals.romdata;
      int addr = ConfigScript.getTilesAddr(blockIndex);
      int count = ConfigScript.getBlocksCount();
      var objects = new ObjRec[count];
      for (int i = 0; i < count; i++)
      {
          byte c1, c2, c3, c4;
          c1 = romdata[addr + i * 4 + 0];
          c2 = romdata[addr + i * 4 + 1];
          c3 = romdata[addr + i * 4 + 2];
          c4 = romdata[addr + i * 4 + 3];
          objects[i] = new ObjRec(c1, c2, c3, c4, 0);
      }
      return objects;
  }
  
  public void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
      var romdata = Globals.romdata;
      int addr = ConfigScript.getTilesAddr(blockIndex);
      int count = ConfigScript.getBlocksCount();
      for (int i = 0; i < count; i++)
      {
          var obj = blocksData[i];
          romdata[addr + i * 4 + 0] = (byte)obj.c1;
          romdata[addr + i * 4 + 1] = (byte)obj.c2;
          romdata[addr + i * 4 + 2] = (byte)obj.c3;
          romdata[addr + i * 4 + 3] = (byte)obj.c4;
      }
  }
}