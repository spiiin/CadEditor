using CadEditor;
using System.IO;
using System;
using System.Windows.Forms;

public class Data
{
  public static GameType getGameType()  { return GameType.DT2; }
  
  public static OffsetRec getPalOffset()       { return new OffsetRec(0x3E2F, 12   , 16);     }
  public static OffsetRec getVideoOffset()     { return new OffsetRec(0x4D10 , 5   , 0xD00);  }
  public static OffsetRec getVideoObjOffset()  { return new OffsetRec(0x4D10 , 5   , 0xD00);  }
  public static OffsetRec getBigBlocksOffset() { return new OffsetRec(0x7310 , 3   , 0x4000); }
  public static OffsetRec getBlocksOffset()    { return new OffsetRec(0x1008A , 5  , 0x440);  }
  public static OffsetRec getScreensOffset()   { return new OffsetRec(0x11d5a, 300 , 0x40);   }
  public static int getBigBlocksCount()        { return 256; }
  
  public static GetVideoPageAddrFunc getVideoPageAddrFunc() { return getDuckTalesVideoAddress; }
  public static GetVideoChunkFunc    getVideoChunkFunc()    { return getDuckTalesVideoChunk;   }
  
  //--------------------------------------------------------------------------------------------
  //duck tales specific
  
  public static int getDuckTalesVideoAddress(int id)
  {
    return -1;
  }
  
  public static byte[] getDuckTalesVideoChunk(int videoPageId)
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
  //--------------------------------------------------------------------------------------------
}