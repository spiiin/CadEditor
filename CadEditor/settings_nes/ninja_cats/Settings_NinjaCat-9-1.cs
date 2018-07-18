using CadEditor;
using System;
using System.Collections.Generic;
//css_include ninja_cats/NinjaCatUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(8055, 32 , 8*5, 5, 8);   }
  public bool getScreenVertical()      { return true; }
  
  public string getObjTypesPicturesDir() { return "obj_sprites_ninjacats"; }
  public DrawObjectBigFunc getDrawObjectBigFunc() { return NinjaCatUtils.drawObjectBig; }
  
  public int getPalBytesAddr()          { return 0x4C1d; }
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x440e , 8   , 0x4000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x572A  , 8   , 0x440); }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc()          { return NinjaCatUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()             { return NinjaCatUtils.getVideoChunk("chr9.bin");   }
  public SetVideoChunkFunc    setVideoChunkFunc()             { return null; }
  public GetBlocksFunc        getBlocksFunc()                 { return NinjaCatUtils.getBlocks;}
  public SetBlocksFunc        setBlocksFunc()                 { return NinjaCatUtils.setBlocks;}
  public GetBigBlocksFunc     getBigBlocksFunc()              { return NinjaCatUtils.getBigBlocks;}
  public SetBigBlocksFunc     setBigBlocksFunc()              { return NinjaCatUtils.setBigBlocks;}
  public GetPalFunc           getPalFunc()                    { return NinjaCatUtils.readPalFromBin("pal9.bin"); }
  public SetPalFunc           setPalFunc()                    { return null;}
  public GetObjectsFunc getObjectsFunc()                      { return NinjaCatUtils.getObjects;  }
  public SetObjectsFunc setObjectsFunc()                      { return NinjaCatUtils.setObjects;  }
}