using CadEditor;
using System.Collections.Generic;
using System.IO;
using System;
using System.Windows.Forms;
//css_include Settings_CapcomBase.cs;
public class Data:CapcomBase
{
  public override GameType getGameType()  { return GameType.DT2; }
  
  public OffsetRec getPalOffset()       { return new OffsetRec(0x3E2F, 12   , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x4D10 , 5   , 0xD00);  }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x4D10 , 5   , 0xD00);  }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x7310 , 3   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x1008A , 5  , 0x440);  }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x11d5a, 300 , 0x40);   }
  public IList<LevelRec> getLevelRecs() { return levelRecsDt2; }
  
  public override GetVideoPageAddrFunc getVideoPageAddrFunc() { return getDuckTalesVideoAddress; }
  public override GetVideoChunkFunc    getVideoChunkFunc()    { return getDuckTalesVideoChunk;   }
  public override SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  public override GetBigBlocksFunc     getBigBlocksFunc()     { return getBigBlocksDt2;}
  public override SetBigBlocksFunc     setBigBlocksFunc()     { return null;}
  
  public IList<LevelRec> levelRecsDt2 = new List<LevelRec>() 
  {
    new LevelRec(0x19488, 0xFF, 8, 6, 0x11C3A),
    new LevelRec(0x195A7, 0xFF, 8, 6, 0x11C6A),
    new LevelRec(0x196E7, 0xFF, 8, 6, 0x11C9A),
    new LevelRec(0x19830, 0xFF, 8, 6, 0x11CCA),
    new LevelRec(0x19970, 0xFF, 8, 6, 0x11CFA),
    new LevelRec(0x19A87, 0xFF, 8, 6, 0x11D2A),
    new LevelRec(0x19B9E, 0xFF, 8, 6, 0x11C6A),
  };
  
  //--------------------------------------------------------------------------------------------
  //duck tales specific
  public int getDuckTalesVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getDuckTalesVideoChunk(int videoPageId)
  {
    try
    {
        using (FileStream f = File.OpenRead("videoBack_DT2.bin"))
        {
            byte[] videodata = new byte[0x5000];
            f.Read(videodata, 0, 0x5000);
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
  
  public byte[] getBigBlocksDt2(int bigTileIndex)
  {
    byte[] bigBlockIndexes = new byte[getBigBlocksCount() * 4];
    if (bigTileIndex == 0)
    {
      for (int i = 0; i < getBigBlocksCount(); i++)
      {
          bigBlockIndexes[i * 4 + 0] = Globals.romdata[0x10D4A + i];
          bigBlockIndexes[i * 4 + 1] = Globals.romdata[0x10E19 + i];
          bigBlockIndexes[i * 4 + 2] = Globals.romdata[0x10EE8 + i];
          bigBlockIndexes[i * 4 + 3] = Globals.romdata[0x10FB7 + i];
      }
    }
    else if (bigTileIndex == 1)
    {
      for (int i = 0; i < getBigBlocksCount(); i++)
      {
          bigBlockIndexes[i * 4 + 0] = Globals.romdata[0x11086 + i];
          bigBlockIndexes[i * 4 + 1] = Globals.romdata[0x11149 + i];
          bigBlockIndexes[i * 4 + 2] = Globals.romdata[0x1120C + i];
          bigBlockIndexes[i * 4 + 3] = Globals.romdata[0x112CF + i];
      }
    }
    else if (bigTileIndex == 2)
    {
      for (int i = 0; i < getBigBlocksCount(); i++)
      {
          bigBlockIndexes[i * 4 + 0] = Globals.romdata[0x11392 + i];
          bigBlockIndexes[i * 4 + 1] = Globals.romdata[0x1143B + i];
          bigBlockIndexes[i * 4 + 2] = Globals.romdata[0x114E4 + i];
          bigBlockIndexes[i * 4 + 3] = Globals.romdata[0x1158D + i];
      }
    }
    else if (bigTileIndex == 3)
    {
      for (int i = 0; i < getBigBlocksCount(); i++)
      {
          bigBlockIndexes[i * 4 + 0] = Globals.romdata[0x11636 + i];
          bigBlockIndexes[i * 4 + 1] = Globals.romdata[0x116E6 + i];
          bigBlockIndexes[i * 4 + 2] = Globals.romdata[0x11796 + i];
          bigBlockIndexes[i * 4 + 3] = Globals.romdata[0x11846 + i];
      }
    }
    else if (bigTileIndex == 4)
    {
      for (int i = 0; i < getBigBlocksCount(); i++)
      {
          bigBlockIndexes[i * 4 + 0] = Globals.romdata[0x118F6 + i];
          bigBlockIndexes[i * 4 + 1] = Globals.romdata[0x119C7 + i];
          bigBlockIndexes[i * 4 + 2] = Globals.romdata[0x11A98 + i];
          bigBlockIndexes[i * 4 + 3] = Globals.romdata[0x11B69 + i];
      }
    }
    return bigBlockIndexes;
  }
  //--------------------------------------------------------------------------------------------
}