using CadEditor;
using System.Collections.Generic;
using System.IO;
using System;
using System.Windows.Forms;
using System.Drawing;

public static class CHCUtils
{
    static Dictionary<int,int> commandDict = new Dictionary<int,int> {
        { 0xFFFF , 1},
        { 0xFFFE , 5},
        { 0xFFFD , 8},
        { 0xFFFC , 8},
        { 0xFFFB , 8},
        { 0xFFFA , 5},
        { 0xFFF9 , 11},
        { 0xFFF8 , 4},
        { 0xFFF7 , 7},
        { 0xFFF6 , 1},
        { 0xFFF5 , 1},
        { 0xFFF4 , 2},
        { 0xFFF3 , 2},
        { 0xFFF2 , 1},
        { 0xFFF1 , 1},
        { 0xFFF0 , 4},
        { 0xFFEF , 6},
        { 0xFFEE , 6},
        { 0xFFED , 6},
      };
  
  static int OBJ_SIZE = 14;  
      
  public static List<ObjectList> getObjects(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount, addr = lr.objectsBeginAddr;
    var objects = new List<ObjectRec>();
    int curAddr = addr;
    while (objects.Count != objCount)
    {
        int baseAddr = curAddr;
        int v = Utils.readWordUnsigned(Globals.romdata, baseAddr);
        var dataDict = new Dictionary<string,int>();
        if (v < 0xFF00)
        {
          int sx, sy, x, y, fromFloor, addX, addY;
          sx = 0;
          sy = 0;
          addX = 0;
          addY = 0;
          x = Utils.readWord(Globals.romdata, baseAddr + 8);
          y = Utils.readWord(Globals.romdata, baseAddr + 10);
          fromFloor = Utils.readWord(Globals.romdata, baseAddr + 6);
          if (fromFloor == 0)
          {
            x = Utils.readWord(Globals.romdata, baseAddr + 2);
            //x += Utils.readWord(Globals.romdata, baseAddr + 8)/2;
            y = Utils.readWord(Globals.romdata, baseAddr + 4);
            //y += Utils.readWord(Globals.romdata, baseAddr + 10)/2;
            
            addX = Utils.readWord(Globals.romdata, baseAddr + 8);
            addY = Utils.readWord(Globals.romdata, baseAddr + 10);
          }
          int data1 = Globals.romdata[baseAddr + 12];
          int data2 = Globals.romdata[baseAddr + 13];
         
          //read flags field
          dataDict["data1"] = data1;
          dataDict["data2"] = data2;
          
          //We don't read it as BYTE, must be WORD, but for now CadEditor can't show WORD in object's data comboBox
          //read addX field
          dataDict["data3"] = Globals.romdata[baseAddr + 2];
          dataDict["data4"] = Globals.romdata[baseAddr + 3];
          //read addY field
          dataDict["data5"] = Globals.romdata[baseAddr + 4];
          dataDict["data6"] = Globals.romdata[baseAddr + 5];
          
          //read real pos Fields as WORDs
          dataDict["fromFloor"] = fromFloor;
          dataDict["addX"] = addX;
          dataDict["addY"] = addY; 
          var obj = new ObjectRec(v, sx, sy, x, y, dataDict);
          objects.Add(obj);
          curAddr += OBJ_SIZE;
        }
        else
        {
          objects.Add(makeCommandObject(v, curAddr));
          curAddr += commandDict[v]*2;
        }
    }
    return new List<ObjectList> { new ObjectList { objects = objects, name = "Objects" } };
  }
  
  private static ObjectRec makeCommandObject(int v, int baseAddr)
  {
    var dataDict = new Dictionary<string,int>();
    for (int i = 0; i < commandDict[v]*2 - 2; i++)
       dataDict[String.Format("data{0}", i)] = Globals.romdata[baseAddr+2+i];
    return new ObjectRec(v, 0, 0, 0, 0, dataDict);
  }
  
  private static void saveCommandObject(ObjectRec obj, int baseAddr)
  {
    int v = obj.type;
    var dataDict = obj.additionalData;
    Utils.writeWord(Globals.romdata, baseAddr, v);
    for (int i = 0; i < commandDict[v]*2 - 2; i++)
       Globals.romdata[baseAddr+2+i] = (byte)dataDict[String.Format("data{0}", i)];
  }
    
  public static bool setObjects(int levelNo, List<ObjectList> objLists)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount, addr = lr.objectsBeginAddr;
    int curAddr = addr;
    var objects = objLists[0].objects;
    for (int i = 0; i < objects.Count; i++)
    {
      var obj = objects[i];
      int v = obj.type;
      int baseAddr = curAddr;
      Utils.writeWord(Globals.romdata, baseAddr, v);
      if (v < 0xFF00)
      {
        int fromFloor = obj.additionalData["fromFloor"];
        //Utils.writeWord(Globals.romdata, baseAddr + 2 , 0);
        //Utils.writeWord(Globals.romdata, baseAddr + 4 , 0);
        Utils.writeWord(Globals.romdata, baseAddr + 6 , fromFloor);
        Globals.romdata[baseAddr + 12] = (byte)obj.additionalData["data1"];
        Globals.romdata[baseAddr + 13] = (byte)obj.additionalData["data2"];
        /*Globals.romdata[baseAddr + 2] = (byte)obj.additionalData["data3"];
        Globals.romdata[baseAddr + 3] = (byte)obj.additionalData["data4"];
        Globals.romdata[baseAddr + 4] = (byte)obj.additionalData["data5"];
        Globals.romdata[baseAddr + 5] = (byte)obj.additionalData["data6"];*/
        
        if (fromFloor != 0)
        {
          Utils.writeWord(Globals.romdata, baseAddr + 2 , obj.x);
          Utils.writeWord(Globals.romdata, baseAddr + 4 , obj.y);
          Utils.writeWord(Globals.romdata, baseAddr + 8 , obj.x);
          Utils.writeWord(Globals.romdata, baseAddr + 10, obj.y);
        }
        else
        {
          Utils.writeWord(Globals.romdata, baseAddr + 2 , obj.x);
          Utils.writeWord(Globals.romdata, baseAddr + 4 , obj.y);
          Utils.writeWord(Globals.romdata, baseAddr + 8 , obj.additionalData["addX"]);
          Utils.writeWord(Globals.romdata, baseAddr + 10, obj.additionalData["addY"]);
        }
        curAddr += OBJ_SIZE;
      }
      else
      {
        saveCommandObject(objects[i], baseAddr);
        curAddr += commandDict[v]*2;
      }
    }
    return true;
  }
  
  public static void drawObject(Graphics g, ObjectRec curObject, int listNo, bool isSelected, float curScale, ImageList objectSprites, bool inactive, int leftMargin, int topMargin)
  {
    //don't render commands
    if (curObject.type >= 0xFF00)
      return;
    
    var dict = curObject.additionalData;
    var myFont = new Font(FontFamily.GenericSansSerif, 6.0f);
    int x = curObject.x, y = curObject.y;
    int addX = dict["addX"];
    int addY = dict["addY"];
    int fromFloor = dict["fromFloor"];
    
    /*if (curObject.type < objectSprites.Images.Count)
    {
        var p1 = new Point((int)(x * curScale) - 8, (int)(y * curScale) - 8);
        var p2 = new Point(p1.X + (int)(addX/2 * curScale) - 8,  p1.Y + (int)(addY/2 * curScale) - 8);
        g.DrawImage(objectSprites.Images[curObject.type], p1);
        if (fromFloor == 0)
        {
          g.DrawImage(objectSprites.Images[curObject.type],p2);
          g.DrawLine(new Pen(Brushes.Red), p1, p2);
        }
    }
    else*/
    {
        var p1 = new Point((int)(x * curScale) - 8, (int)(y * curScale) - 8);
        g.FillRectangle(Brushes.Black, new Rectangle(p1, new Size(16, 16)));
        g.DrawString(curObject.type.ToString("X3"), myFont, Brushes.White, p1);
        if (fromFloor == 0)
        {
          var p2 = new Point(p1.X + (int)(addX/2 * curScale) - 8, p1.Y + (int)(addY/2 * curScale) - 8);
          g.FillRectangle(Brushes.Green, new Rectangle(p2, new Size(16, 16)));
          g.DrawString(curObject.type.ToString("X3"), myFont, Brushes.White, p2);
          g.DrawLine(new Pen(Brushes.Red), p1, p2);
        }
    }
    if (isSelected)
        g.DrawRectangle(new Pen(Brushes.Red, 2.0f), new Rectangle((int)(x * curScale) - 8, (int)(y * curScale) - 8, 16, 16));
  }
  
  public static Dictionary<String,int> getObjectDictionary(int listNo, int type)
  {
    return new Dictionary<String, int> { 
        {"data1", 0},
        {"data2", 0},
        {"data3", 0},
        {"data4", 0},
        {"data5", 0},
        {"data6", 0},
        {"fromFloor", 0},
        {"addX", 0},
        {"addY", 0},
      };
  }
}