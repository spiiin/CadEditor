using CadEditor;
using System.Collections.Generic;
using System.IO;
using System;
using System.Windows.Forms;
//css_include Settings_TinyToon-Utils.cs;

public class Data
{
  public GameType getGameType()  { return GameType.TT; }
  
  public OffsetRec getPalOffset()       { return new OffsetRec(0xB1F0, 16, 16        ) ;}
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x4D10 , 1   , 0xD00  ) ;}
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x4D10 , 1   , 0xD00  ) ;}
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x9B2D , 1   , 0x4000 ) ;}
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x1F1CB, 1   , 0x440  ) ;}
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x9A2D , 1   , 8*30 ) ;}
  public int getBigBlocksCount()        { return 20; }
  public int getScreenWidth()           { return 8; }
  public int getScreenHeight()          { return 30; }
  public IList<LevelRec> getLevelRecs() { return levelRecsTT; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getTinyToonVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getTinyToonVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  public GetBigBlocksFunc     getBigBlocksFunc()     { return getBigBlocksTT;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return setBigBlocksTT;}
  public GetBlocksFunc        getBlocksFunc()        { return TinyToonUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()        { return TinyToonUtils.setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPalleteLevel_5_1;}
  public SetPalFunc           setPalFunc()           { return null;}
  public GetObjectsFunc getObjectsFunc() { return TinyToonUtils.getObjectsTT; }
  public SetObjectsFunc setObjectsFunc() { return TinyToonUtils.setObjectsTT; }
  public GetLayoutFunc  getLayoutFunc()  { return TinyToonUtils.getLayoutLinearTT;   }
  
  public IList<LevelRec> levelRecsTT = new List<LevelRec>() 
  {
    new LevelRec(0x1471B, 4, 1, 1, 0x0),
  };
  
  public int getTinyToonVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getTinyToonVideoChunk(int videoPageId)
  {
    try
    {
        using (FileStream f = File.OpenRead("videoBack_TT_63.bin"))
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
    byte[] bigBlockIndexes = new byte[getBigBlocksCount() * 4];
    var bigBlocksAddr = Globals.getBigTilesAddr(bigTileIndex);
    return Utils.readDataFromAlignedArrays(Globals.romdata, bigBlocksAddr, getBigBlocksCount());
  }
  public void setBigBlocksTT(int bigTileIndex, byte[] bigBlockIndexes)
  {
    var bigBlocksAddr = Globals.getBigTilesAddr(bigTileIndex);
    Utils.writeDataToAlignedArrays(bigBlockIndexes, Globals.romdata, bigBlocksAddr, getBigBlocksCount());
  }
  
  public byte[] getPalleteLevel_5_1(int palId)
  {
    var pallete = new byte[] { 
      0x0C, 0x0F, 0x00, 0x00, 0x0C, 0x0f, 0x37, 0x14,
      0x0C, 0x0F, 0x20, 0x10, 0x0C, 0x0f, 0x20, 0x38
    }; 
    return pallete;
  }
  
  public bool isBigBlockEditorEnabled() { return true;  }
  public bool isBlockEditorEnabled()    { return true;  }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return true; }
  public bool isVideoEditorEnabled()    { return true; }
  //--------------------------------------------------------------------------------------------
}