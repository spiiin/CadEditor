using CadEditor;
using System.Collections.Generic;

public class CapcomBase
{
  //Using types
  //OffsetRec
  //public delegate int GetVideoPageAddrFunc(int videoPageId);
  //public delegate byte[] GetVideoChunkFunc(int videoPageId);
  //public delegate void SetVideoChunkFunc(int videoPageId, byte[] videoChunk);
  //public delegate byte[] GetBigBlocksFunc(int bigBlockId);
  //public delegate void   SetBigBlocksFunc(int bigTileIndex, byte[] bigBlockIndexes);
  //public delegate byte[] GetPalFunc(int palId);
  //public delegate void   SetPalFunc(int palId, byte[] pallete);
  /*public struct LevelRec
  {
      public LevelRec(int objectsBeginAddr, int objCount, int width = 0, int height = 0, int layoutAddr = 0)
      {
          this.objCount = objCount;
          this.objectsBeginAddr = objectsBeginAddr;
          this.width = width;
          this.height = height;
          this.layoutAddr = layoutAddr;
      }
      public int objCount;
      public int objectsBeginAddr;
      public int width;
      public int height;
      public int layoutAddr;
  }*/
  public virtual GameType getGameType()  { return GameType.Generic; }
  public virtual int getBigBlocksCount() { return 256; }
  public virtual int getScreenWidth()    { return 8; }
  public virtual int getScreenHeight()   { return 8; }
  
  public virtual GetVideoPageAddrFunc getVideoPageAddrFunc() { return Utils.getChrAddress; }
  public virtual GetVideoChunkFunc    getVideoChunkFunc()    { return Utils.getVideoChunk; }
  public virtual SetVideoChunkFunc    setVideoChunkFunc()    { return Utils.setVideoChunk; }
  public virtual GetBigBlocksFunc     getBigBlocksFunc()     { return Utils.getBigBlocksCapcomDefault;}
  public virtual SetBigBlocksFunc     setBigBlocksFunc()     { return Utils.setBigBlocksCapcomDefault;}
  public virtual GetPalFunc           getPalFunc()           { return Utils.getPalleteLinear;}
  public virtual SetPalFunc           setPalFunc()           { return Utils.setPalleteLinear;}
}