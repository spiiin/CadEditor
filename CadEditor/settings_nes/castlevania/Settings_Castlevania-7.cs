using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x12914, 5 , 8*6, 6, 8); }	
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x12a05, 1  , 0x1000);  } 
  
  public int getBlocksCount()           { return 256; }
  public int getBigBlocksCount()        { return 256; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public GetBlocksFunc        getBlocksFunc() { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
   return Utils.readVideoBankFromFile("chr7.bin", 0);		//e318
  }
  
  public byte[] getPallete(int palId)
  {
		return Utils.readBinFile("pal7.bin");       // 1ceac 
  }
  
  public static ObjRec[] getBlocks(int blockIndex)
  {
      return readBlocksLinearTilesPal17(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), ConfigScript.getBlocksCount(blockIndex));
  }

  public static void setBlocks(int blockIndex, ObjRec[] blocksData)
  {
      writeBlocksLinearTilesPal17(blocksData, Globals.romdata, ConfigScript.getTilesAddr(blockIndex), ConfigScript.getBlocksCount(blockIndex));
  }
  
  public static ObjRec[] readBlocksLinearTilesPal17(byte[] romdata, int addr, int count)
  {
      int BLOCK_W = 4;
      int BLOCK_H = 4;
      int BLOCK_S = BLOCK_H * BLOCK_H;
      var objects = Utils.readBlocksLinear(romdata, addr, BLOCK_W, BLOCK_H, count, false, false, 1);
      for (int i = 0; i < count; i++)
      {
          int palBytesAddr = addr - 1 + i * 17; //every 17th byte
          int palByte = romdata[palBytesAddr];
          var palBytes = new int[] { (palByte >> 0) & 3, (palByte >> 2) & 3, (palByte >> 4) & 3, (palByte >> 6) & 3 };
          objects[i].palBytes = palBytes;
      }
      return objects;
  }

  public static void writeBlocksLinearTilesPal17(ObjRec[] objects, byte[] romdata, int addr, int count)
  {
      Utils.writeBlocksLinear(objects, romdata, addr, count, false, false, 1);
      for (int i = 0; i < count; i++)
      {
          int palBytesAddr = addr - 1 + i * 17; //every 17th byte
          var objPalBytes = objects[i].palBytes;
          int palByte = objPalBytes[0] | objPalBytes[1] << 2 | objPalBytes[2] << 4 | objPalBytes[3] << 6;
          romdata[palBytesAddr + i] = (byte)palByte;
      }
  }

}