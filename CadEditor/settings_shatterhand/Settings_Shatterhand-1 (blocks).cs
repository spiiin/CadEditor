using CadEditor;
//using System;
using System.Collections.Generic;
//using System.Drawing;

public class Data 
{

  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginChrView.dll"
    };
  }
 
  public OffsetRec getScreensOffset()  { return new OffsetRec(33295, 19 , 8*8);   }
  public int getScreenWidth()          { return 8; }
  public int getScreenHeight()         { return 8; }

  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x86cf , 1 , 0x400); }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x8a77,  1  , 0x4000);  }

  public int getBigBlocksCount()        { return 255; }
  public int getBlocksCount()           { return 156; }

  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  public GetBigBlocksFunc     getBigBlocksFunc()     { return getBigBlocks; }
  public SetBigBlocksFunc     setBigBlocksFunc()     { return setBigBlocks;}
  public GetBlocksFunc        getBlocksFunc()        { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc()        { return setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPalleteLevel_1_1;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public byte[] getVideoChunk(int videoPageId)
  {
    return Utils.readVideoBankFromFile("Shatterhand (U) [!].bin", videoPageId);
  }

  public ObjRec[] getBlocks(int tileId)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    var objects = new ObjRec[getBlocksCount()];
    for (int i = 0; i < getBlocksCount(); i++)
    {
        byte c1 = Globals.romdata[addr + 4 * i + 0];
        byte c2 = Globals.romdata[addr + 4 * i + 2];
        byte c3 = Globals.romdata[addr + 4 * i + 1];
        byte c4 = Globals.romdata[addr + 4 * i + 3];
        byte typeColor = Globals.romdata[0x893f + i];

        objects[i] = new ObjRec(c1, c2, c3, c4, typeColor);
    }
    return objects;
  }
  
  public void setBlocks(int tileId, ObjRec[] blocks)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    for (int i = 0; i < getBlocksCount(); i++)
    {
        var obj = blocks[i];
        
        Globals.romdata[addr + 4 * i + 0] = (byte)obj.c1;
        Globals.romdata[addr + 4 * i + 2] = (byte)obj.c2;
        Globals.romdata[addr + 4 * i + 1] = (byte)obj.c3;
        Globals.romdata[addr + 4 * i + 3] = (byte)obj.c4;
        Globals.romdata[0x893f + i] = (byte)obj.typeColor;
    }
  }
  
  public BigBlock[] getBigBlocks(int bigTileIndex)
  {
      var bblocks = Utils.getBigBlocksCapcomDefault(bigTileIndex);
      for (int v = 0; v < bblocks.Length; v++)
      {
         var b = bblocks[v];
         int temp = b.indexes[1];
         b.indexes[1] = b.indexes[2];
         b.indexes[2] = temp;
      }
      return bblocks;
  }
  
  public void setBigBlocks(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
      for (int v = 0; v < bigBlockIndexes.Length; v++)
      {
         var b = bigBlockIndexes[v];
         int temp = b.indexes[1];
         b.indexes[1] = b.indexes[2];
         b.indexes[2] = temp;
      }
      Utils.setBigBlocksCapcomDefault(bigTileIndex, bigBlockIndexes);
  }
  
  public byte[] getPalleteLevel_1_1(int palId)
  {
    //for test level 1-1
    var pallete = new byte[] { 
      0x0f, 0x0c, 0x17, 0x26, 0x0f, 0x0b, 0x1c, 0x10,
      0x00, 0x0f, 0x29, 0x36, 0x00, 0x0f, 0x21, 0x30
    }; 
    return pallete;
  }

  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
}