using CadEditor;
using System;
using System.Drawing;
using System.Collections.Generic;

public class Data 
{ 
  public GameType getGameType()                             { return GameType.LM; } //for Video.makeCurScreen function
  public OffsetRec getPalOffset()                           { return new OffsetRec(0x1CC19, 32  , 16);     }
  public OffsetRec getVideoOffset()                         { return new OffsetRec(0x30010, 16  , 0x1000); }
  public OffsetRec getVideoObjOffset()                      { return new OffsetRec(0x20010, 16  , 0x1000); }
  public OffsetRec getScreensOffset()                       { return new OffsetRec(90441 - 96   , 1 , 17*96);   }
  public int getScreenWidth()                               { return 96; }
  public int getScreenHeight()                              { return 17; }
  //public string getBlocksFilename()                         { return "jungle_book_1.png"; }
  //public RenderToMainScreenFunc getRenderToMainScreenFunc() { return renderObjects; }
  
  public bool isBigBlockEditorEnabled() { return true; }
  public bool isBlockEditorEnabled()    { return true; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return true; }
  public bool isVideoEditorEnabled()    { return true; }
  public IList<LevelRec> getLevelRecs() { return levelRecsJB; }
  
  public SetBlocksFunc setBlocksFunc()     { return setBlocksJB;}
  public GetBlocksFunc getBlocksFunc()     { return getBlocksJB;}
  public GetBigBlocksFunc getBigBlocksFunc()  { return getBigBlocksJB;}
  public SetBigBlocksFunc setBigBlocksFunc()  { return setBigBlocksJB;}
  public GetObjectsFunc getObjectsFunc()   { return getObjectsJungleBook;  }
  public SetObjectsFunc setObjectsFunc()   { return setObjectsJungleBook;  }
  public SortObjectsFunc sortObjectsFunc() { return sortObjectsJungleBook; }
  public GetLayoutFunc getLayoutFunc()     { return getLayoutJungleBook;   }
  public GetPalFunc           getPalFunc() { return Utils.getPalleteLinear;}
  public SetPalFunc           setPalFunc() { return Utils.setPalleteLinear;}
  public GetVideoPageAddrFunc getVideoPageAddrFunc()         { return Utils.getChrAddress; }
  public GetVideoChunkFunc    getVideoChunkFunc()            { return Utils.getVideoChunk; }
  public SetVideoChunkFunc    setVideoChunkFunc()            { return Utils.setVideoChunk; }
  public GetObjectDictionaryFunc getObjectDictionaryFunc()   { return getObjectDictionary; }
  
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
        if ((b1 == 0x40) && (b2 == 0xF)) //draw only crystals
        {
          var rect = new Rectangle((x+1) * 32*curScale+16, y * 32*curScale - 32, 16*curScale, 16*curScale);
          g.DrawRectangle(new Pen(Color.Red, 4.0f), rect);
          g.DrawString(String.Format("{0:X}", b1), new Font("Arial", 8), Brushes.Red, rect);
          g.DrawString(String.Format("{0:X}", b2), new Font("Arial", 8), Brushes.Red, rect.X, rect.Y+16);
        }
    }
  }*/
  
  //addrs saved in ram at 77-79-7E-81
  public List<ObjectRec> getObjectsJungleBook(int levelNo)
  {
    LevelRec lr = ConfigScript.getLevelRec(levelNo);
    int objCount = lr.objCount;
    var objects = new List<ObjectRec>();
    for (int i = 0; i < objCount; i++)
    {
        byte y    = Globals.romdata[0x167A5 + i];
        byte x    = Globals.romdata[0x16775 + i];
        byte data = Globals.romdata[0x16805 + i];
        byte v    = Globals.romdata[0x167D5 + i];
        int realx = x* 32 + 16;
        int realy = y* 32 + 16;
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
  
  public Dictionary<String,int> getObjectDictionary(int type)
  {
    return new Dictionary<String, int> { {"data", 0} };
  }
  
   public void setBlocksJB(int blockIndex, ObjRec[] objects)
  {
    return;
  }
  
  public static ObjRec[] getBlocksJB(int blockIndex)
  {
    var part1 = Utils.readBlocksFromAlignedArrays(Globals.romdata, 0x1D984, 0x80);
    var part2 = Utils.readBlocksFromAlignedArrays(Globals.romdata, 0x16859, 0x80);
    return Utils.mergeArrays(part1, part2);
  }
  
  public byte[] getBigBlocksJB(int bigTileIndex)
  {
    byte[] ans = new byte[256 * 4];
    int bigBlocksAddr1 = 0x1DC04;
    int bigBlocksCount1 = 101;
    int bigBlocksAddr2 = 0x16AD0 +9;//0x16A59;
    int bigBlocksCount2 = 119;
    var bb1 = Utils.readDataFromAlignedArrays(Globals.romdata, bigBlocksAddr1, bigBlocksCount1);
    var bb2 = Utils.readDataFromAlignedArrays(Globals.romdata, bigBlocksAddr2, bigBlocksCount2);
    bb1.CopyTo(ans, 0);
    bb2.CopyTo(ans, 128*4);
    return ans;
  }
  
  public void setBigBlocksJB(int bigTileIndex, byte[] bigBlockIndexes)
  {
    /*var bigBlocksAddr = Globals.getBigTilesAddr(bigTileIndex);
    Utils.writeDataToAlignedArrays(bigBlockIndexes, Globals.romdata, bigBlocksAddr, getBigBlocksCount());*/
  }
}