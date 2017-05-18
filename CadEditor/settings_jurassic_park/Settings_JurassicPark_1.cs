using CadEditor;
using System;
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
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x60CC - 0x6000, 1 , 48*48); }
  public int getScreenWidth()          { return 48; }
  public int getScreenHeight()         { return 48; }
  
  public int getWordLen()              { return 2;}
  public bool isLittleEndian()          { return true;}
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x76CC - 0x6000, 1  , 0x1000);  }
  public int getBlocksCount()           { return 352; }
  public int getBigBlocksCount()        { return 352; }
  
  public GetBlocksFunc getBlocksFunc() { return Utils.getBlocksLinear2x2withoutAttrib;}
  public SetBlocksFunc setBlocksFunc() { return Utils.setBlocksLinearWithoutAttrib;}
  public int getPalBytesAddr()          { return 0x72cc /*0x72e4*/ - 0x6000; }
  
  public OffsetRec getPalOffset() { return new OffsetRec(0 , 1  , 16  ); }
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public ConvertScreenTileFunc getConvertScreenTileFunc() { return (v=>v&0x1FF);}
  public ConvertScreenTileFunc getBackConvertScreenTileFunc() { return (v=>v);}
  
  public OffsetRec getVideoOffset()   { return new OffsetRec(0x0, 2, 0x1000); }
  
  //----------------------------------------------------------------------------
  
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
     if (videoPageId == 0x90)
     {
        return Utils.readVideoBankFromFile("chr1.bin", 0);
     }
     else if (videoPageId == 0x91)
     {
        return Utils.readVideoBankFromFile("chr1-2.bin", 0);
     }
     else
     {
         return null;
     }
  }
  //-------------------------------------------------------------------------------------------------------------------
  public MapInfo[] getMapsInfo() { return getMaps(); }
  public LoadMapFunc getLoadMapFunc() { return loadMap; }
  public SaveMapFunc getSaveMapFunc() { return MapUtils.saveAttribs; }
  public bool isMapReadOnly()         { return true; }
  public bool mapEditorSharePallete() { return true; }
  
  public MapData loadMap(int mapNo)
  {
      return MapUtils.loadMapFromBlocks(mapNo, 48*47*4, 576, 48*2, false, MapUtils.fillAttribs);
  }

  MapInfo[] getMaps()
  {
    return new MapInfo[]
    { 
        new MapInfo(){ dataAddr = getScreensOffset().beginAddr, palAddr = getPalOffset().beginAddr, videoNo = 0, attribsAddr = getPalBytesAddr() },
    };
  }
}