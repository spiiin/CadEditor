using CadEditor;
using System;
using System.Drawing;
using System.Collections.Generic;

public class Data 
{ 
  public GameType getGameType()                             { return GameType.TT; }
  public OffsetRec getScreensOffset()                       { return new OffsetRec(90441 - 96   , 1 , 17*96);   }
  public int getScreenWidth()                               { return 96; }
  public int getScreenHeight()                              { return 17; }
  public string getBlocksFilename()                         { return "settings_jungle_book/jungle_book_1.png"; }
  //public RenderToMainScreenFunc getRenderToMainScreenFunc() { return renderObjects; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return true; }
  public bool isVideoEditorEnabled()    { return false; }
  public IList<LevelRec> getLevelRecs() { return levelRecsJB; }
  
  public GetObjectsFunc getObjectsFunc()   { return getObjectsJungleBook;  }
  public SetObjectsFunc setObjectsFunc()   { return setObjectsJungleBook;  }
  public SortObjectsFunc sortObjectsFunc() { return sortObjectsJungleBook; }
  public GetLayoutFunc getLayoutFunc()     { return getLayoutJungleBook;   } 
  public int getMinObjCoordX()           { return 16; }
  public int getMinObjCoordY()           { return 16; }
  public int getMinObjType()             { return 0;          }
  public int getMaxObjCoordX()           { return (96)*32+16; }
  public int getMaxObjCoordY()           { return (17)*32+16; }
  public int getMaxObjType()             { return 256;          }
  
  public IList<LevelRec> levelRecsJB = new List<LevelRec>() 
  {
    new LevelRec(0x0, 47, 1, 1, 0x0),
  };
  
  /*public void renderObjects(Graphics g, int curScale)
  {
    for (int i = 0; i < 48; i++)
    {
        byte x  = Globals.romdata[0x16775 + i];
        byte y  = Globals.romdata[0x167A5 + i];
        byte b1 = Globals.romdata[0x167D5 + i];
        byte b2 = Globals.romdata[0x16805 + i];
        var rect = new Rectangle((x+1) * 32*curScale+16, y * 32*curScale - 32, 16*curScale, 16*curScale);
        g.DrawRectangle(new Pen(Color.Red, 4.0f), rect);
        g.DrawString(String.Format("{0:X}", b1), new Font("Arial", 8), Brushes.Red, rect);
        g.DrawString(String.Format("{0:X}", b2), new Font("Arial", 8), Brushes.Red, rect.X, rect.Y+16);
    }
  }*/
  
   //addrs saved in ram at 7B-7D-7F-81
  public List<ObjectRec> getObjectsJungleBook(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
        byte x    = Globals.romdata[0x16775 + i];
        byte y    = Globals.romdata[0x167A5 + i];
        int realx = x* 32 + 16;
        int realy = y* 32 + 16;
        byte v    = Globals.romdata[0x167D5 + i];
        byte data = Globals.romdata[0x16805 + i];
        var dataDict = new Dictionary<string,int>();
        dataDict["data"] = data;
        var obj = new ObjectRec(v, 0, 0, realx, realy, dataDict);
        objects.Add(obj);
    }
    return objects;
  }

  public bool setObjectsJungleBook(int levelNo, List<ObjectRec> objects)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    for (int i = 0; i < objects.Count; i++)
    {
        var obj = objects[i];
        byte x = (byte)((obj.x - 16) /32);
        byte y = (byte)((obj.y - 16) /32);
        Globals.romdata[0x167D5 + i] = (byte)obj.type;
        Globals.romdata[0x16805 + i] = (byte)obj.additionalData["data"];
        Globals.romdata[0x16775 + i] = x;
        Globals.romdata[0x167A5 + i] = y;
    }
    for (int i = objects.Count; i < objCount; i++)
    {
        Globals.romdata[0x167D5 + i] = 0xFF;
        Globals.romdata[0x16805 + i] = 0xFF;
        Globals.romdata[0x16775 + i] = 0xFF;
        Globals.romdata[0x167A5 + i] = 0xFF;
    }
    return true;
  }
  
  public void sortObjectsJungleBook(int levelNo, List<ObjectRec> objects)
  {
    objects.Sort((o1, o2) => { return o1.x > o2.x ? 1 : o1.x < o2.x ? -1 : o1.y < o2.y ? -1 : o1.y > o2.y ? 1 : 0; });
  }
  
  LevelLayerData getLayoutJungleBook(int levelNo)
  {
    byte[] layer = new byte[1];
    layer[0] = 0;
    return new LevelLayerData(1, 1, layer);
  }
}