using CadEditor;
using System.Collections.Generic;
//css_include Settings_Flintstones-Utils.cs;
public class Data
{ 
  public GameType getGameType()           { return GameType.TT; }
  public OffsetRec getScreensOffset()     { return new OffsetRec(0x1519, 1 , 8*78);   }
  public int getScreenWidth()             { return 8; }
  public int getScreenHeight()            { return 78; }
  public bool getScreenVertical()         { return true; }
  public string getBlocksFilename()       { return "flintstones_2.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return true; }
  public bool isVideoEditorEnabled()    { return false; }
  
  public GetObjectsFunc getObjectsFunc()   { return FliUtils.getObjects;  }
  public SetObjectsFunc setObjectsFunc()   { return FliUtils.setObjects;  }
  public GetLayoutFunc getLayoutFunc()     { return FliUtils.getLayout;   } 
  
  public IList<LevelRec> getLevelRecs() { return levelRecs; }
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x118CB, 6, 1, 1, 0x0),
  };
}