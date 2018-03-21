using CadEditor;
using System.Collections.Generic;

public class Data
{
  public bool showDumpFileField()  { return true;  }
  
  public bool isUseSegaGraphics()            { return true; }
  //public bool isBuildScreenFromSmallBlocks() { return true; }
  public int getWordLen()              { return 2;}
  
  public OffsetRec getPalOffset()       { return new OffsetRec(0, 1, 64    );   }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0, 2, 0     );   }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0, 2, 0     );   }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x137000, 1, 0x500);  }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0, 1, 0     );   }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0, 20, 16*16);   }
  public int getBigBlocksCount()        { return 0x500; }
  public int getScreenWidth()           { return 16; }
  public int getScreenHeight()          { return 16; }
  
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChuck;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  public GetPalFunc           getPalFunc()           { return readPal;}
  public SetPalFunc           setPalFunc()           { return null;}
  public GetObjectsFunc getObjectsFunc() { return null; }
  public SetObjectsFunc setObjectsFunc() { return null; }
  public ConvertScreenTileFunc getConvertScreenTileFunc() { return (v=>v/8);}
  public ConvertScreenTileFunc getBackConvertScreenTileFunc() { return (v=>v*8);}
  
  public byte[] getVideoChuck(int videoPageId)
  {
    return Utils.readBinFile(videoPageId == 0 ? "vram.bin":"vram2.bin");
  }
  
  public byte[] readPal(int palNo)
  {
    return Utils.readBinFile("pal.bin");
  }
  
  public bool isBigBlockEditorEnabled() { return false;  }
  public bool isBlockEditorEnabled()    { return true;  }
  public bool isEnemyEditorEnabled()    { return false; }
  //--------------------------------------------------------------------------------------------
}