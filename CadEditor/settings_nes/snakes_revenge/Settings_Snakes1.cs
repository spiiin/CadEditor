using CadEditor;
using System;
using System.Windows.Forms;
//css_include shared_settings/SharedUtils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset() { return new OffsetRec(0x803B, 20, 48, 8, 6); }
  
  public bool isBuildScreenFromSmallBlocks() { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0 , 1   , 0x1000);  }
  public OffsetRec getPalOffset  ()     { return new OffsetRec(0x0 , 2   , 16); }
  public GetVideoPageAddrFunc getVideoPageAddrFunc() { return SharedUtils.fakeVideoAddr(); }
  public GetVideoChunkFunc    getVideoChunkFunc()    { return SharedUtils.getVideoChunk(new[] {"chr1.bin"}); }
  public SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x8410, 1  , 0x1000);  }
  public int getBlocksCount()           { return 77; }
  public int getBigBlocksCount()        { return 77; }
  public int getPalBytesAddr()          { return 0x88e0; }
  public GetBlocksFunc        getBlocksFunc() { return Utils.getBlocksFromTiles16Pal1;}
  public SetBlocksFunc        setBlocksFunc() { return Utils.setBlocksFromTiles16Pal1;}
  
  public GetPalFunc           getPalFunc()           { return SharedUtils.readPalFromBin(new[] {"pal1.bin"}); }
  public SetPalFunc           setPalFunc()           { return null;}
  
  public GetBigTileNoFromScreenFunc getBigTileNoFromScreenFunc() { return getBigTileNoFromScreen; }
  public SetBigTileToScreenFunc     setBigTileToScreenFunc()     { return setBigTileToScreen; }
  
  public static int getBigTileNoFromScreen(int[] screenData, int index)
  {
    //hack for unpacking RLE on the fly
    try
    {
        int curScreenIndex = 0;
        do
        {
          if ((screenData[curScreenIndex] & 0x80) == 0x80)
          {
            index -= ((screenData[curScreenIndex] & 0x7F)-2);
          }
          curScreenIndex++;
        } while (curScreenIndex < index);
        if ((screenData[curScreenIndex] & 0x80) == 0x80)
        {
          curScreenIndex++;
        }
        return screenData[curScreenIndex];
    }
    catch (IndexOutOfRangeException e)
    {
        return 0xFF;
    }
  }

  public static void setBigTileToScreen(int[] screenData, int index, int value)
  {
    //will brake rle packing
    screenData[index] = value;
  }
}