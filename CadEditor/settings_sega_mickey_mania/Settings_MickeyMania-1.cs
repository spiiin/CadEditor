using CadEditor;
using System.Collections.Generic;
using System.IO;
using System;
using System.Windows.Forms;

public class Data
{
  public GameType getGameType()              { return GameType.Generic; }
  public bool isUseSegaGraphics()            { return true; }
  public bool isBlockSize4x4()               { return true; } //valid only for sega games
  //public bool isBuildScreenFromSmallBlocks() { return true; }
  public int getWordLen()                    { return 1;}
  
  public OffsetRec getPalOffset()       { return new OffsetRec(0, 1, 64    );   }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0, 1, 0     );   }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0, 1, 0     );   }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0, 1, 256);      }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0, 1, 0     );   }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0, 1, 128*70);      }
  public int getBigBlocksCount()        { return 256; }
  public int getScreenWidth()           { return 128; }
  public int getScreenHeight()          { return 70; }
  public IList<LevelRec> getLevelRecs() { return null; }
  
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChuck;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  public GetBigBlocksFunc     getBigBlocksFunc()     { return Utils.getBigBlocksCapcomDefault;}
  public SetBigBlocksFunc     setBigBlocksFunc()     { return Utils.setBigBlocksCapcomDefault;}
  public GetPalFunc           getPalFunc()           { return readPal;}
  public SetPalFunc           setPalFunc()           { return null;}
  public GetObjectsFunc getObjectsFunc() { return null; }
  public SetObjectsFunc setObjectsFunc() { return null; }
  
  public byte[] getVideoChuck(int videoPageId)
  {
    try
    {
        using (FileStream f = File.OpenRead("vram1.bin"))
        {
            byte[] d = new byte[0x10000];
            f.Read(d, 0, 0x10000);
            return d;
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message);
    }
    return null;
  }
  
  public byte[] readPal(int palNo)
  {
    try
    {
        using (FileStream f = File.OpenRead("pal1.bin"))
        {
            byte[] d = new byte[128];
            f.Read(d, 0, 128);
            return d;
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show(ex.Message);
    }
    return null;
  }
  
  public bool isBigBlockEditorEnabled() { return false;  }
  public bool isBlockEditorEnabled()    { return true;  }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
  //--------------------------------------------------------------------------------------------
}