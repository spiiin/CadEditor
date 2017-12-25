using CadEditor;
using System;
using System.Linq;
using System.Collections.Generic;

public class Data
{
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x8020, 1 , 256);   }
  public int getScreenWidth()          { return 132; }
  public int getScreenHeight()         { return 1; }
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public OffsetRec getVideoOffset()                  { return new OffsetRec(0x24010, 1 , 0x1000); }
  
  public GetPalFunc           getPalFunc() { return Utils.getPalleteLinear;}
  public SetPalFunc           setPalFunc() { return Utils.setPalleteLinear;}
  public OffsetRec getPalOffset()          { return new OffsetRec(0x1fe37,  1, 16   ); }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x8165 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 48; }
  public int getBigBlocksCount()        { return 48; }
  
  public GetBlocksFunc        getBlocksFunc() { return getBlocksAbadox;}
  public SetBlocksFunc        setBlocksFunc() { return setBlocksAbadox;}
  
  //-------------------------------------------------------------------------------------------------------------------
  public static ObjRec[] getBlocksAbadox(int blockIndex)
  {
      var addr = ConfigScript.getTilesAddr(blockIndex);
      var count = ConfigScript.getBlocksCount();
      var objects = new ObjRec[count];
      int w = 4;
      int h = 30;
      int blockSize = w * h;
      for (int i = 0; i < count; i++)
      {
          var indexes = new int[blockSize];
          var palBytes = new int[32];
          var attrAdd = i * 8;
          int blockAddr = addr + i * blockSize + attrAdd;
          Array.Copy(Globals.romdata, blockAddr , indexes, 0, blockSize);
          indexes = Utils.transpose(indexes, w, h);
          //
          int palBytesAddr = blockAddr + blockSize;
          for (int j = 0; j < 8; j++)
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
  
  public static void setBlocksAbadox(int blockIndex, ObjRec[] blocksData)
  {
      var addr = ConfigScript.getTilesAddr(blockIndex);
      int w = 4;
      int h = 30;
      int blockSize = w*h;
      for (int i = 0; i < blocksData.Length; i++)
      {
          var block = blocksData[i];
          var attrAdd = i * 8;
          int blockAddr = addr + i * blockSize + attrAdd;
          var indexes = Utils.transpose(block.indexes, h, w);
          var indexesBytes = indexes.Select(x => (byte)x).ToArray();
          Array.Copy(indexesBytes, 0, Globals.romdata, blockAddr, blockSize);
          //
          int palBytesAddr = blockAddr + blockSize;
          var palBytes = block.palBytes;
          for (int j = 0; j < 8; j++)
          {
              int palByte = palBytes[j*4+0] << 0 | palBytes[j*4+1] << 2 | palBytes[j*4+2] << 4 | palBytes[j*4+3] << 6;
              Globals.romdata[palBytesAddr + j] = (byte)palByte;
          }
          //
      }
  }
}