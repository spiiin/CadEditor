using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x10, 16, 8*8, 8, 8); }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0, 1 , 0x1000);  }
  public int getBlocksCount()           { return 64; }
  
  public GetBigBlocksFunc     getBigBlocksFunc() { return getBigBlocks;}
  public SetBigBlocksFunc     setBigBlocksFunc() { return setBigBlocks;}
  public GetBlocksFunc        getBlocksFunc() { return getBlocksConsts;}
  public SetBlocksFunc        setBlocksFunc() { return null;}
  public GetPalFunc           getPalFunc()    { return getPallete;}
  public SetPalFunc           setPalFunc()    { return null;}
  
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x5810, 1 , 0x4000); }
  public int getPalBytesAddr()          { return 0xA610; }
  public int getBigBlocksCount() { return 229; }
  //----------------------------------------------------------------------------
  public ObjRec[] getBlocksConsts(int blockIndex)
  {
        var objects = new ObjRec[getBlocksCount()];
        for (int i = 0; i < objects.Length; i++)
        {
            var indexes  = new int[4];
            var palBytes = new int[1];
            int bi = (i/8)*32 + i%8 * 2;
            indexes[0] = bi;
            indexes[1] = bi + 1;
            indexes[2] = bi + 16;
            indexes[3] = bi + 17;
            objects[i] = new ObjRec(2, 2, 0, indexes, palBytes);
        }
        return objects;
  }
  
  public BigBlock[] getBigBlocks(int bigTileIndex)
  {
    var data = Utils.readLinearBigBlockData(0, bigTileIndex);
    var bb = Utils.unlinearizeBigBlocks<BigBlockWithPal>(data, 2, 2);
    for (int i = 0; i < bb.Length; i++)
    {
      int palByte = Globals.romdata[getPalBytesAddr() + i];
      bb[i].palBytes[0] = palByte >> 0 & 0x3;
      bb[i].palBytes[1] = palByte >> 2 & 0x3;
      bb[i].palBytes[2] = palByte >> 4 & 0x3;
      bb[i].palBytes[3] = palByte >> 6 & 0x3;
      
      for (int ind = 0; ind < bb[i].indexes.Length; ind++)
      {
          bb[i].indexes[ind] = bb[i].indexes[ind] & 0x3F; //read only 6 bits
      }
    }
    return bb;
  }
  
  public void setBigBlocks(int bigTileIndex, BigBlock[] bigBlockIndexes)
  {
    var bigBlocksAddr = ConfigScript.getBigTilesAddr(0, bigTileIndex);
    var data = Utils.linearizeBigBlocks(bigBlockIndexes);
    
    //write only first 6 bits of block info (first 2 are physics info)
    int size = data.Length;
    int addr = ConfigScript.getBigTilesAddr(0, bigTileIndex);
    for (int i = 0; i < size; i++)
    {
        int mask = Globals.romdata[addr + i] & 0xC0;
        Globals.romdata[addr + i] =  (byte)(mask | data[i]);
    }
    //save pal bytes
    for (int i = 0; i < bigBlockIndexes.Length; i++)
    {
      var bb = bigBlockIndexes[i] as BigBlockWithPal;
      int palByte = bb.palBytes[0] | bb.palBytes[1] << 2 | bb.palBytes[2]<<4 | bb.palBytes[3]<< 6;
      Globals.romdata[getPalBytesAddr() + i] = (byte)palByte;
    }
  }
  
  public RenderToMainScreenFunc getRenderToMainScreenFunc() { return renderObjects; }
  
  public void renderObjects(Graphics g, int curScale, int scrNo)
  {
    int w = getScreensOffset().width;
    int h = getScreensOffset().height;
    int physicsAddr = 0xC010 + scrNo * w * h;
    for (int x = 0; x < w; x++)
    {
      for (int y = 0; y < h; y++)
      {
          byte physics  = Globals.romdata[physicsAddr + y*w + x];
          var rect = new Rectangle(32*curScale*(x+1), 32*curScale*y, 32*curScale, 32*curScale);
          g.DrawRectangle(new Pen(Color.Red, 2.0f), rect);
          g.DrawString(String.Format("{0:X2}", physics), new Font("Arial", 8), Brushes.Red, rect.X + 8, rect.Y);
      }
    }
  }
  
  public byte[] getPallete(int palId)
  {
      return Utils.readBinFile("pal1.bin");
  }
  
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("chr1.bin", videoPageId);
  }
}