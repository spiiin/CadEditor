using CadEditor;
using System;
using System.Drawing;
using System.Collections.Generic;

public class Data 
{

  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginChrView.dll"
    };
  }
 
  public OffsetRec getVideoOffset()   { return new OffsetRec(0x22010, 1, 0x1000); }

  public OffsetRec getScreensOffset()  { return new OffsetRec(0x10406, 5 , 16*12);   }
  public int getScreenWidth()          { return 12; }
  public int getScreenHeight()         { return 16; }
  public bool getScreenVertical()      { return true; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isEnemyEditorEnabled()    { return false; }
  
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x10091 , 1 , 0x400); }

  public bool isBuildScreenFromSmallBlocks() { return true; }

  public int getBigBlocksCount()        { return 177; }
  public int getBlocksCount()           { return 177; }

  public int getPalBytesAddr()          { return 0x10355; }
  public string[] getBlockTypeNames()   { return objTypesCad;  }
  
 
  public GetVideoPageAddrFunc getVideoPageAddrFunc()         { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()            { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()            { return Utils.setVideoChunk; }

  public GetBlocksFunc        getBlocksFunc()        { return getBlocks;}
  public SetBlocksFunc        setBlocksFunc()        { return setBlocks;}
  public GetPalFunc           getPalFunc()           { return getPalleteLevel_2_1;}
  public SetPalFunc           setPalFunc()           { return null;}
  
//  public byte[] getVideoChunk(int videoPageId)
//  {
//    return Utils.readVideoBankFromFile("chr2.bin", videoPageId);
//  }

  public static ObjRec[] getBlocks(int tileId)
  {
     var objects = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(tileId), 2, 2, ConfigScript.getBlocksCount(), false, false);
    int palAddr = ConfigScript.getPalBytesAddr();
    for (int i = 0; i < ConfigScript.getBlocksCount(); i++)
    {
        objects[i].palBytes[0] =  Globals.romdata[palAddr + i] & 0x3 >> 1;
        objects[i].type = Globals.romdata[palAddr + i] >> 2 & 0xf;
    }
    return objects; 
    
    
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocks)
  {

    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount();
    int palAddr = ConfigScript.getPalBytesAddr();
    
    Utils.writeBlocksLinear(blocks, Globals.romdata, addr, count, false, false);
    
    for (int i = 0; i < count; i++)
    {
        int mask = Globals.romdata[palAddr + i] & 0x3;
        Globals.romdata[palAddr + i] = (byte)(mask | (blocks[i].palBytes[0] >> 6));
    }
    
  }
   
  public byte[] getPalleteLevel_2_1(int palId)
  {
    //for level 2-1
    var pallete = new byte[] { 
      0x0f, 0x10, 0x18, 0x08, 0x0f, 0x28, 0x16, 0x07,
      0x0f, 0x10, 0x1b, 0x0b, 0x0f, 0x30, 0x27, 0x0f
    }; 
    return pallete;
  }
  
string[] objTypesCad =
    new[]  {
        "0 (background)",
        "1 (solid)",
        "2 ()",
        "3 ()",
        "4 ()",
        "5 ()",
        "6 ()",
        "7 ()",
        "8 (ledge)",
        "9 ()",
        "A ()",
        "B ()",
        "C (bo-bo)",
        "D ()",
        "E ()",
        "F ()"
    };

  

}