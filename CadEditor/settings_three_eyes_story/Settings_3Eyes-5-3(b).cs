using CadEditor;
using System.Collections.Generic;
//css_include settings_three_eyes_story/ThreeUtils.cs;

public class Data
{ 
    public OffsetRec getScreensOffset()   { return new OffsetRec(0x15f04 , 1  , 64, 8, 8);      }
    
    public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x158e0,  1  , 0x4000);  }
    public int getBigBlocksCount()        { return 256; }
    public GetBigBlocksFunc     getBigBlocksFunc()     { return ThreeUtils.getBigBlocks; }
    public SetBigBlocksFunc     setBigBlocksFunc()     { return ThreeUtils.setBigBlocks;}
    
    public OffsetRec getBlocksOffset()    { return new OffsetRec(0x15708 , 1  , 0x1000);   }
    public int getBlocksCount()           { return 256; }
    public int getPalBytesAddr()          { return 0x162c4; }
    public GetBlocksFunc        getBlocksFunc()        { return ThreeUtils.getBlocks;}
    public SetBlocksFunc        setBlocksFunc()        { return ThreeUtils.setBlocks;}
    
    public OffsetRec getVideoOffset()                  { return new OffsetRec(0x0 , 1   , 0x1000);  }
    public GetVideoPageAddrFunc getVideoPageAddrFunc() { return ThreeUtils.fakeVideoAddr(); }
    public GetVideoChunkFunc    getVideoChunkFunc()    { return ThreeUtils.getVideoChunk(new[] {"chr5-3(b).bin"}); }
    public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
    
    public OffsetRec getPalOffset() { return new OffsetRec(0x0 , 1   , 16); }
    public GetPalFunc getPalFunc()  { return ThreeUtils.readPalFromBin(new[] {"pal5-3(b).bin"}); }
    public SetPalFunc setPalFunc()  { return null;}
    
    public bool isBigBlockEditorEnabled() { return true;  }
    public bool isBlockEditorEnabled()    { return true;  }
    public bool isEnemyEditorEnabled()    { return false; }
}