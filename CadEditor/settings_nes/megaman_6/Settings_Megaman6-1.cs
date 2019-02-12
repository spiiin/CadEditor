using CadEditor;
using System.Collections.Generic;
//css_include shared_settings/CapcomBase.cs;
public class Data:CapcomBase
{
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginChrView.dll"
    };
  }
  
  public OffsetRec getPalOffset()       { return new OffsetRec(0x0, 1 , 16);     }
  public OffsetRec getVideoOffset()     { return new OffsetRec(0x0, 1 , 0x1000); }
  public OffsetRec getVideoObjOffset()  { return new OffsetRec(0x0, 1 , 0x1000); } //
  public OffsetRec getBigBlocksOffset() { return new OffsetRec(0x58E10, 1 , 0x2000); }
  public OffsetRec getBlocksOffset()    { return new OffsetRec(0x58910, 1 , 0x2000); }
  public OffsetRec getScreensOffset()   { return new OffsetRec(0x66810, 1 , 256*8, 256, 8); }
  
  public override GetVideoPageAddrFunc getVideoPageAddrFunc() { return getVideoAddress; }
  public override GetVideoChunkFunc    getVideoChunkFunc()    { return getVideoChunk;   }
  public override SetVideoChunkFunc    setVideoChunkFunc()    { return null; }
  public override GetPalFunc           getPalFunc()    { return getPallete;}
  public override SetPalFunc           setPalFunc()    { return null;}
  
  public bool isBigBlockEditorEnabled() { return true;  }
  public bool isBlockEditorEnabled()    { return true;  }
  public bool isEnemyEditorEnabled()    { return false; }
  
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