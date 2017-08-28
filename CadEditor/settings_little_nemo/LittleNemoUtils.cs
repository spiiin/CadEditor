using CadEditor;
using System;
using System.Collections.Generic;

using System.Windows.Forms;

public class LittleNemoUtils 
{ 
  public static List<ObjectList> getObjectsNemo(int levelNo)
  {
      LevelRec lr = ConfigScript.getLevelRec(levelNo);
      int objCount = lr.objCount, addr = lr.objectsBeginAddr;
      var objects = new List<ObjectRec>();

      var objScreenAddr = ConfigScript.getPalBytesAddr(); //not palBytes, but object sx/sy decode
      int screenIndex = 0;
      for (int i = 0; i < objCount; i++)
      {
          //read, how many objects at current screen
          while (true)
          {
              int curIndex = Globals.romdata[objScreenAddr];
              int nextIndex = Globals.romdata[objScreenAddr + 1];
              if (i < nextIndex)
              {
                  break;
              }
              objScreenAddr++;
              screenIndex++;
          }
          int sx = screenIndex % lr.width;
          int sy = screenIndex / lr.width;
          //
          int v = Globals.romdata[addr + i * 2 + 0];
          int xy = Globals.romdata[addr + i * 2 + 1];
          int x = (xy >> 4) * 16;
          int y = (xy & 0x0F) * 16;
          var obj = new ObjectRec(v, sx, sy, x, y);
          objects.Add(obj);
          //hack for .NET optimizer(?). It fails to forever loop, if no this string
          var a = i.ToString();
      }
      return new List<ObjectList> { new ObjectList { objects = objects, name = "Objects" } };
  }
    
  public static bool setObjectsNemo(int levelNo, List<ObjectList> objLists)
  {
      LevelRec lr = ConfigScript.getLevelRec(levelNo);
      int addrBase = lr.objectsBeginAddr;
      int objCount = lr.objCount;
      var objects = objLists[0].objects;
      
      var screenIndexValues = new int[lr.width*lr.height];
      for (int i = 0; i < objCount; i++)
      {
          var obj = objects[i];
          Globals.romdata[addrBase + i * 2 + 0] = (byte)obj.type;
          int xy = ((obj.x / 16) << 4) | ((obj.y / 16) & 0x0F);
          Globals.romdata[addrBase + i * 2 + 1] = (byte)xy;
          
          int screenIndex = obj.sy * lr.width + obj.sx;
          screenIndexValues[screenIndex] += 1;
      }
      
      //write how many objects at every screen
      var objScreenAddr = ConfigScript.getPalBytesAddr(); //read screen sx/sy address
      int totalSx = 0;
      for (int screenIndex = 0; screenIndex < screenIndexValues.Length-1; screenIndex++)
      {
          totalSx += screenIndexValues[screenIndex];
          Globals.romdata[objScreenAddr + screenIndex + 1] = (byte)totalSx;
      }
      return true;
  }
  
  public static LevelLayerData getLayoutLinearPlusOne(int curActiveLayout)
  {
      int layoutAddr = ConfigScript.getLayoutAddr(curActiveLayout);
      int width =  ConfigScript.getLevelWidth(curActiveLayout);
      int height = ConfigScript.getLevelHeight(curActiveLayout);
      int[] layer = new int[width * height];
      for (int i = 0; i < width * height; i++)
          layer[i] = Globals.romdata[layoutAddr + i] + 1;
      return new LevelLayerData(width, height, layer, null, null);
  }
}