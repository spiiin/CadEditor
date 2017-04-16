using CadEditor;
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
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x147F8,  1  , 0x4000);  }//153
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x14660 , 1  , 0x440);   }//102
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x14A5C , 7  , 64);      }
  public int getBigBlocksCount()        { return 153; }
  public int getBlocksCount()           { return 102; }
  public int getScreenWidth()           { return 8; }
  public int getScreenHeight()          { return 8; }
  
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
    return Utils.readVideoBankFromFile("videoBack_3E_1.bin", videoPageId);
  }
  
  public ObjRec[] getBlocks(int tileId)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    var objects = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(tileId), 2, 2, ConfigScript.getBlocksCount(), false, false);
    for (int i = 0; i < objects.Length; i++)
    {
        objects[i].palBytes[0] = Globals.romdata[0x14C1C + i];
    }
    return objects;
  }
  
  public void setBlocks(int tileId, ObjRec[] blocks)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    Utils.writeBlocksLinear(blocks, Globals.romdata, ConfigScript.getTilesAddr(tileId), ConfigScript.getBlocksCount(), false, false);
    for (int i = 0; i < getBlocksCount(); i++)
    {
        Globals.romdata[0x14C1C + i] = (byte)blocks[i].palBytes[0];
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
      0x0f, 0x16, 0x27, 0x37, 0x0f, 0x1c, 0x10, 0x22,
      0x0f, 0x18, 0x1a, 0x20, 0x0f, 0x0f, 0x27, 0x30
    }; 
    return pallete;
  }
  
  public bool isBigBlockEditorEnabled() { return true;  }
  public bool isBlockEditorEnabled()    { return true;  }
  public bool isEnemyEditorEnabled()    { return false; }
  //--------------------------------------------------------------------------------------------
}