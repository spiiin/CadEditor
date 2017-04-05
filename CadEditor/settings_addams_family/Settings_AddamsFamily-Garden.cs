using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0xC0A8, 1 , 256);   }
  public int getScreenWidth()          { return 256; }
  public int getScreenHeight()         { return 1; }
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public OffsetRec getVideoOffset()                  { return new OffsetRec(0x21010, 1 , 0x1000); }
  
  public GetPalFunc           getPalFunc() { return Utils.getPalleteLinear;}
  public SetPalFunc           setPalFunc() { return Utils.setPalleteLinear;}
  public OffsetRec getPalOffset()          { return new OffsetRec(0x2e0d,  1, 16   ); }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0xC282 , 1  , 0x1000);  }
  public int getBlocksCount()           { return 128; }
  public int getBigBlocksCount()        { return 128; }
  
  public GetBlocksFunc        getBlocksFunc() { return getBlocksLinear1x20withoutAttrib;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksLinearWithoutAttrib;}
  public int getPalBytesAddr()          { return 0xCCCC; }
  
  //-------------------------------------------------------------------------------------------------------------------
  public static ObjRec[] getBlocksLinear1x20withoutAttrib(int blockIndex)
  {
      var bb = Utils.readBlocksLinearWithoutAttribs(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), 1, 20, ConfigScript.getBlocksCount());
      foreach (var b in bb)
      {
        b.palBytes = new int[10];
      }
      return bb;
  }
}