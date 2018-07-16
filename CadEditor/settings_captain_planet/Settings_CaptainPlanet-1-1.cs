using CadEditor;
using System;
using System.Linq;
using System.Collections.Generic;

public class Data 
{
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginChrView.dll"
    };
  }
  
  public OffsetRec getScreensOffset()  { return new OffsetRec(0xC0, 1, 255*4, 255, 4); }

  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }

  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}

  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x4bc, 1, 0x1000); }
  public int getBlocksCount()           { return 256; }

  public GetBlocksFunc        getBlocksFunc() { return getBlocks4x8;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocks4x8;}
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
      return Utils.readVideoBankFromFile("chr1-1.bin", 0);
  }
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal1-1.bin");
  }
  
    //-------------------------------------------------------------------------------------------------------------------
  public static ObjRec[] getBlocks4x8(int blockIndex)
  {
      var addr = ConfigScript.getTilesAddr(blockIndex);
      var count = ConfigScript.getBlocksCount();
      var objects = new ObjRec[count];
      int w = 4;
      int h = 8;
      int blockSize = w * h;
      for (int i = 0; i < count; i++)
      {
          var indexes = new int[blockSize];
          var palBytes = new int[w*h/4];
          var attrAdd = i * 2;
          int blockAddr = addr + i * blockSize + attrAdd + 2;
          Array.Copy(Globals.romdata, blockAddr , indexes, 0, blockSize);

          //
          int palBytesAddr = blockAddr - 2;
          for (int j = 0; j < 2; j++)
          {
              int palByte = Globals.romdata[palBytesAddr + j];
              palBytes[j*4+0] = (palByte >> 0) & 3;
              palBytes[j*4+1] = (palByte >> 2) & 3;
              palBytes[j*4+2] = (palByte >> 4) & 3;
              palBytes[j*4+3] = (palByte >> 6) & 3;
          }
          //
          objects[i] = new ObjRec(w, h, 0, indexes, palBytes);
      }
      return objects;
  }
  
  public static void setBlocks4x8(int blockIndex, ObjRec[] blocksData)
  {
      var addr = ConfigScript.getTilesAddr(blockIndex);
      int w = 4;
      int h = 8;
      int blockSize = w*h;
      for (int i = 0; i < blocksData.Length; i++)
      {
          var block = blocksData[i];
          var attrAdd = i * 2;
          int blockAddr = addr + i * blockSize + attrAdd + 2;
          var indexesBytes = block.indexes.Select(x => (byte)x).ToArray();
          Array.Copy(indexesBytes, 0, Globals.romdata, blockAddr, blockSize);
          //
          int palBytesAddr = blockAddr - 2;
          var palBytes = block.palBytes;
          for (int j = 0; j < 2; j++)
          {
              int palByte = palBytes[j*4+0] << 0 | palBytes[j*4+1] << 2 | palBytes[j*4+2] << 4 | palBytes[j*4+3] << 6;
              Globals.romdata[palBytesAddr + j] = (byte)palByte;
          }
          //
      }
  }
}
