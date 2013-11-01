using CadEditor;
using System.Collections.Generic;
using System.IO;
using System;
using System.Windows.Forms;

public class Data
{
  public GameType getGameType()  { return GameType._3E; }
  
  public OffsetRec getPalOffset()       { return new OffsetRec(0xB1F0, 16, 16);        }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x4D10 , 1   , 0xD00);  }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x4D10 , 1   , 0xD00);  }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x147F8, 1   , 0x4000); }//153
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x14660 , 1  , 0x440);  }//102
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x14A5C , 7  , 64);     }
  public int getBigBlocksCount()        { return 256; }
  public int getBlocksCount()           { return 256; }
  public int getScreenWidth()           { return 8; }
  public int getScreenHeight()          { return 8; }
  public IList<LevelRec> getLevelRecs() { return levelRecsTT; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getTinyToonVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getTinyToonVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  public GetBigBlocksFunc     getBigBlocksFunc()     { return Utils.getBigBlocksCapcomDefault;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return Utils.setBigBlocksCapcomDefault;}
  public GetBlocksFunc        getBlocksFunc()        { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc()        { return setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPalleteLevel_1_1;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public IList<LevelRec> levelRecsTT = new List<LevelRec>() 
  {
    new LevelRec(0x0, 1, 8, 6, 0x0),
  };
  
  public int getTinyToonVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getTinyToonVideoChunk(int videoPageId)
  {
    try
    {
        using (FileStream f = File.OpenRead("videoBack_3E_1.bin"))
        {
            byte[] videodata = new byte[0x1000];
            f.Read(videodata, 0, 0x1000);
            byte[] ans = new byte[0x1000];
            int offset = (videoPageId - 0x90)*0x1000;
            for (int i = 0; i < ans.Length; i++)
                ans[i] = videodata[offset + i];
            return ans;
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message);
    }
    return null;
  }
  
  public byte[] getBigBlocksTT(int bigTileIndex)
  {
    var bigBlocksAddr = Globals.getBigTilesAddr(bigTileIndex);
    return Utils.readDataFromAlignedArrays(Globals.romdata, bigBlocksAddr, getBigBlocksCount());
  }
  
  public void setBigBlocksTT(int bigTileIndex, byte[] bigBlockIndexes)
  {
    var bigBlocksAddr = Globals.getBigTilesAddr(bigTileIndex);
    Utils.writeDataToAlignedArrays(bigBlockIndexes, Globals.romdata, bigBlocksAddr, getBigBlocksCount());
  }
  
  public ObjRec[] getBlocks(int tileId)
  {
    int addr = Globals.getTilesAddr(tileId);
    var objects = new ObjRec[getBlocksCount()];
    for (int i = 0; i < getBlocksCount(); i++)
    {
        byte c1 = Globals.romdata[addr + 4 * i + 0];
        byte c2 = Globals.romdata[addr + 4 * i + 1];
        byte c3 = Globals.romdata[addr + 4 * i + 2];
        byte c4 = Globals.romdata[addr + 4 * i + 3];
        byte typeColor = Globals.romdata[0x14C1C + i];
        objects[i] = new ObjRec(c1, c2, c3, c4, typeColor);
    }
    return objects;
  }
  
  public void setBlocks(int tileId, ObjRec[] blocks)
  {
    int addr = Globals.getTilesAddr(tileId);
    for (int i = 0; i < getBlocksCount(); i++)
    {
        var obj = blocks[i];
        Globals.romdata[addr + 4 * i + 0] = obj.c1;
        Globals.romdata[addr + 4 * i + 1] = obj.c2;
        Globals.romdata[addr + 4 * i + 2] = obj.c3;
        Globals.romdata[addr + 4 * i + 3] = obj.c4;
        Globals.romdata[0x14C1C + i] = obj.typeColor;
    }
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
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return true; }
  //--------------------------------------------------------------------------------------------
}