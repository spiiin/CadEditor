using CadEditor;
using System.Collections.Generic;
//css_include settings_three_eyes_story/ThreeUtils.cs;

public class Data
{ 
    public OffsetRec getScreensOffset()   { return new OffsetRec(0x11efc , 4  , 64);      }
    public int getScreenWidth()           { return 8; }
    public int getScreenHeight()          { return 8; }
    
    public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x11bf0,  1  , 0x4000);  }
    public int getBigBlocksCount()        { return 153; }
    public GetBigBlocksFunc     getBigBlocksFunc()     { return ThreeUtils.getBigBlocks; }
    public SetBigBlocksFunc     setBigBlocksFunc()     { return ThreeUtils.setBigBlocks;}
    
    public OffsetRec getBlocksOffset()    { return new OffsetRec(0x11ac8 , 1  , 0x440);   }
    public int getBlocksCount()           { return 102; }
    public int getPalBytesAddr()          { return 0x125fc; }
    public GetBlocksFunc        getBlocksFunc()        { return ThreeUtils.getBlocks;}
    public SetBlocksFunc        setBlocksFunc()        { return ThreeUtils.setBlocks;}
    
    public OffsetRec getVideoOffset()                  { return new OffsetRec(0x0 , 1   , 0x1000);  }
    public GetVideoPageAddrFunc getVideoPageAddrFunc() { return ThreeUtils.fakeVideoAddr(); }
    public GetVideoChunkFunc    getVideoChunkFunc()    { return ThreeUtils.getVideoChunk(new[] {"chr3-1.bin"}); }
    public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
    
    public OffsetRec getPalOffset() { return new OffsetRec(0x0 , 1   , 16); }
    public GetPalFunc getPalFunc()  { return ThreeUtils.readPalFromBin(new[] {"pal3-1.bin"}); }
    public SetPalFunc setPalFunc()  { return null;}
    
    public bool isBigBlockEditorEnabled() { return true;  }
    public bool isBlockEditorEnabled()    { return true;  }
    public bool isEnemyEditorEnabled()    { return false; }
}