using CadEditor;
using System.Collections.Generic;

public static class BlockUtils
{
  //-----------------------------------------------------------------------------------------------
  public class Dt2ObjRec : ObjRec
  {
    public Dt2ObjRec(ObjRec b, int _index)
        :base(b)
    {
        index = _index;
    }
    
    public override int getType()
    {
        return index < 0xA0 ? 0 : 5;
    }
    
    int index;
  }
  
  public static ObjRec[] getBlocksDt2(int blockIndex)
  {
    ObjRec[] blocks = Utils.readBlocksFromAlignedArraysWithoutCropPal(Globals.romdata, ConfigScript.getTilesAddr(blockIndex), ConfigScript.getBlocksCount());
    //decode palByte
    int palInfoCount = ConfigScript.getBlocksCount()/4;
    var palInfo = new byte[palInfoCount];
    for (int i = 0; i < palInfoCount; i++)
    {
        palInfo[i] = (byte)blocks[i].palBytes[0];
    }
    for (int i = 0; i < blocks.Length; i++)
    {
        var palInfoByte = palInfo[i/4];
        int parByteNo = i % 4;
        blocks[i].palBytes[0] = (byte)((palInfoByte >> parByteNo*2) & 3);
    }
    //
    //rebuild blocks to dt2 blocks
    for (int i = 0; i < blocks.Length; i++)
    {
        blocks[i] = new Dt2ObjRec(blocks[i], i);
    }
    //
    return blocks;
  }
  
  public static void setBlocksDt2(int blockIndex, ObjRec[] objects)
  {
    int addr = ConfigScript.getTilesAddr(blockIndex);
    int count = ConfigScript.getBlocksCount();
    for (int i = 0; i < count; i++)
    {
        var obj = objects[i];
        Globals.romdata[addr + count * 0 + i] = (byte)obj.indexes[0];
        Globals.romdata[addr + count * 1 + i] = (byte)obj.indexes[1];
        Globals.romdata[addr + count * 2 + i] = (byte)obj.indexes[2];
        Globals.romdata[addr + count * 3 + i] = (byte)obj.indexes[3];
    }
    
    int palInfoCount = ConfigScript.getBlocksCount()/4;
    for (int i = 0; i < palInfoCount; i++)
    {
        var palInfoByte = 
          (objects[i*4+0].palBytes[0]<<0) | 
          (objects[i*4+1].palBytes[0]<<2) |
          (objects[i*4+2].palBytes[0]<<4) | 
          (objects[i*4+3].palBytes[0]<<6);
          
        Globals.romdata[addr + count * 4 + i] = (byte)palInfoByte;
    }
  }
}