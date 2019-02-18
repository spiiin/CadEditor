using CadEditor;
using System;

public class TT2Utils 
{

  public static ObjRec[] getBlocks(int tileId)
  {
      int count = ConfigScript.getBlocksCount(tileId);
      var bb = Utils.readBlocksLinear(Globals.romdata, ConfigScript.getTilesAddr(tileId), 2, 2, count, false, false);
      
      var palAddr = ConfigScript.getPalBytesAddr(tileId);
      int palInfoCount = ConfigScript.getBlocksCount(tileId)/4;
      var palInfo = new byte[palInfoCount];
      for (int i = 0; i < palInfoCount; i++)
      {
          palInfo[i] = Globals.romdata[palAddr + i];
      }
      
      var xchgTable = new int[]{3,2,1,0};
      for (int i = 0; i < bb.Length; i++)
      {
          var palInfoByte = palInfo[i/4];
          int palBytesNo = i % 4;
          palBytesNo = xchgTable[palBytesNo];
          bb[i].palBytes[0] = (byte)((palInfoByte >> palBytesNo*2) & 3);
      }
      return bb;
  }
  
  public static void setBlocks(int tileId, ObjRec[] blocksData)
  {
    int addr = ConfigScript.getTilesAddr(tileId);
    int count = ConfigScript.getBlocksCount(tileId);
    var palAddr = ConfigScript.getPalBytesAddr(tileId);
    Utils.writeBlocksLinear(blocksData, Globals.romdata, addr, count, false, false);
    
    int palInfoCount = ConfigScript.getBlocksCount(tileId)/4;
    for (int i = 0; i < palInfoCount; i++)
    {
        var palInfoByte = 
          (blocksData[i*4+3].palBytes[0]<<0) | 
          (blocksData[i*4+2].palBytes[0]<<2) |
          (blocksData[i*4+1].palBytes[0]<<4) | 
          (blocksData[i*4+0].palBytes[0]<<6);
          
        Globals.romdata[palAddr + i] = (byte)palInfoByte;
    }
    
  }
}