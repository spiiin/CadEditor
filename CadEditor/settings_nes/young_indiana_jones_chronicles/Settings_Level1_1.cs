using CadEditor;
using System;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec( 0xc7b0, 1 , 6*216, 6, 216);   }
  public bool getScreenVertical()      { return true; }
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xc010 , 1  , 0x1000);  }
  public int getPalBytesAddr()          { return 0xCCC0; }
  public GetBlocksFunc        getBlocksFunc() { return getBlocksFromTiles16Pal1V;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocksFromTiles16Pal1V;}
  public int getBigBlocksCount() { return 122; }
  public int getBlocksCount()    { return 122; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk("chr1.bin");    }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin("pal1.bin"); }
  public SetPalFunc           setPalFunc()           { return null;}
  
  public static ObjRec[] getBlocksFromTiles16Pal1V(int blockIndex)
  {
      return readBlocksLinearTiles16Pal1V(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), ConfigScript.getPalBytesAddr(blockIndex), ConfigScript.getBlocksCount(blockIndex));
  }

  public static void setBlocksFromTiles16Pal1V(int blockIndex, ObjRec[] blocksData)
  {
      writeBlocksLinearTiles16Pal1V(blocksData, Globals.romdata, ConfigScript.getTilesAddr(blockIndex), ConfigScript.getPalBytesAddr(blockIndex), ConfigScript.getBlocksCount(blockIndex));
  }
  
  public static ObjRec[] readBlocksLinearTiles16Pal1V(byte[] romdata, int addr, int palBytesAddr, int count)
  {
      int BLOCK_W = 4;
      int BLOCK_H = 4;
      var objects = Utils.readBlocksLinear(romdata, addr, BLOCK_W, BLOCK_H, count, false, true);
      for (int i = 0; i < count; i++)
      {
          int palByte = romdata[palBytesAddr + i];
          var palBytes = new[] { (palByte >> 0) & 3, (palByte >> 2) & 3, (palByte >> 4) & 3, (palByte >> 6) & 3 };
          objects[i].palBytes = palBytes;
      }
      return objects;
  }

  public static void writeBlocksLinearTiles16Pal1V(ObjRec[] objects, byte[] romdata, int addr, int palBytesAddr, int count)
  {
      Utils.writeBlocksLinear(objects, romdata, addr, count, false, true);
      for (int i = 0; i < count; i++)
      {
          var objPalBytes = objects[i].palBytes;
          int palByte = objPalBytes[0] | objPalBytes[1] << 2 | objPalBytes[2] << 4 | objPalBytes[3] << 6;
          romdata[palBytesAddr + i] = (byte)palByte;
      }
  }
}