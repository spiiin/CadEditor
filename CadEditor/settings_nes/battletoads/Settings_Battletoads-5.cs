using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public OffsetRec getScreensOffset()     { return new OffsetRec(0x2f65e   , 1 , 88*8, 88, 8);  }
  public int getBigBlocksCount() { return 60; }
  public int getBlocksCount() { return 60; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x3013b , 1  , 0x1000);  }
  public int getPalBytesAddr()          { return 0x304fb; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return getPallete;}
  public SetPalFunc           setPalFunc()           { return null;}
  
  public LoadPhysicsLayer loadPhysicsLayerFunc() { return loadPhysicsLayer; }
  public SavePhysicsLayer savePhysicsLayerFunc() { return savePhysicsLayer; }
  public int getPhysicsBlocksCount() { return 16; }
  
  //----------------------------------------------------------------------------
  public int getVideoAddress(int id)
  {
    return -1;
  }
  
  public byte[] getVideoChunk(int videoPageId)
  {
     return Utils.readVideoBankFromFile("ppu_dump5.bin", videoPageId);
  }
  
  public byte[] getPallete(int palId)
  {
    var pallete = new byte[] { 
      0x0f, 0x07, 0x17, 0x27, 0x0f, 0x02, 0x1c, 0x2c,
      0x0f, 0x07, 0x05, 0x14, 0x0f, 0x07, 0x16, 0x27
    }; 
    return pallete;
  }
  
  int[] loadPhysicsLayer(int scrNo)
  {
    int width = getScreensOffset().width;
    int height = getScreensOffset().height;
    int size = width*height;
    int physicsAddr = 0x2c14d;
    int lineLen = 4;
    
    var ans = new int[size];
    for (int line = 0; line < width; line++)
    {
       var lineBytes = new int[lineLen];
       for(int lb = 0; lb < lineLen; lb++)
       {
         lineBytes[lb] = Globals.romdata[physicsAddr + line * lineLen + lb];
       }
       ans[0 * width + line]  = lineBytes[3] & 0x0F;
       ans[1 * width + line]  = lineBytes[3] >> 4;
       ans[2 * width + line]  = lineBytes[2] & 0x0F;
       ans[3 * width + line]  = lineBytes[2] >> 4;
       ans[4 * width + line]  = lineBytes[1] & 0x0F;
       ans[5 * width + line]  = lineBytes[1] >> 4;
       ans[6 * width + line]  = lineBytes[0] & 0x0F;
       ans[7 * width + line]  = lineBytes[0] >> 4; 
    }
    return ans;
  }
  
  void savePhysicsLayer(int scrNo, int[] data)
  {
    int width = getScreensOffset().width;
    int height = getScreensOffset().height;
    int size = width*height;
    int physicsAddr = 0x2c14d;
    int lineLen = 4;
    
    for (int line = 0; line < width; line++)
    {
       var lineBytes = new int[lineLen];
       lineBytes[0] = data[7 * width + line] << 4 | data[6 * width + line];
       lineBytes[1] = data[5 * width + line] << 4 | data[4 * width + line];
       lineBytes[2] = data[3 * width + line] << 4 | data[2 * width + line];
       lineBytes[3] = data[1 * width + line] << 4 | data[0 * width + line];
       for(int lb = 0; lb < lineLen; lb++)
       {
          Globals.romdata[physicsAddr + line * lineLen + lb] = (byte)lineBytes[lb];
       }
    }
  }
}