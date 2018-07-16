using CadEditor;
using System.Collections.Generic;

public class Data
{
  public bool isUseSegaGraphics()            { return true; }
  //public bool isBuildScreenFromSmallBlocks() { return true; }
  public int getWordLen()              { return 2;}
  
  public OffsetRec getPalOffset()       { return new OffsetRec(0, 1, 64    );   }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0, 1, 0     );   }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0, 1, 0     );   }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0xC000, 1, 450*8);  }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0, 1, 0     );   }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0, 25, 16*16 , 16, 16);   }
  public int getBigBlocksCount()        { return 450; }
  
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChuck;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  public GetPalFunc           getPalFunc()           { return readPal;}
  public SetPalFunc           setPalFunc()           { return null;}
  public GetObjectsFunc getObjectsFunc() { return null; }
  public SetObjectsFunc setObjectsFunc() { return null; }
  
  public ConvertScreenTileFunc getConvertScreenTileFunc() { return (v=>v&0x3FF);}
  public ConvertScreenTileFunc getBackConvertScreenTileFunc() { return (v=>v);}
  
  public byte[] getVideoChuck(int videoPageId)
  {
    return Utils.readBinFile("vram.bin");
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