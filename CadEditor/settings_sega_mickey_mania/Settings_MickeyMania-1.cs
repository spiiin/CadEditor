using CadEditor;
using System.Collections.Generic;

public class Data
{
  public bool showDumpFileField()  { return true;  }
  
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
  public GetPalFunc           getPalFunc()           { return readPal;}
  public SetPalFunc           setPalFunc()           { return null;}
  public GetObjectsFunc getObjectsFunc() { return null; }
  public SetObjectsFunc setObjectsFunc() { return null; }
  
  public byte[] getVideoChuck(int videoPageId)
  {
    return Utils.readBinFile("vram1.bin");
  }
  
  public byte[] readPal(int palNo)
  {
    return Utils.readBinFile("pal1.bin");
  }
  
  public bool isBigBlockEditorEnabled() { return false;  }
  public bool isBlockEditorEnabled()    { return true;  }
  public bool isEnemyEditorEnabled()    { return false; }
  //--------------------------------------------------------------------------------------------
}