using CadEditor;
using System.Collections.Generic;
//css_include Settings_Flintstones-Utils.cs;
public class Data
{
  public OffsetRec getScreensOffset()     { return new OffsetRec(0x28B1, 1 , 8*80);   }
  public int getScreenWidth()             { return 8; }
  public int getScreenHeight()            { return 80; }
  public bool getScreenVertical()         { return true; }
  public string getBlocksFilename()       { return "flintstones_4.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return false; } //broken get/set objects functions
  
  public GetObjectsFunc getObjectsFunc()   { return FliUtils.getObjects;  }
  public SetObjectsFunc setObjectsFunc()   { return FliUtils.setObjects;  }
  public GetLayoutFunc getLayoutFunc()     { return FliUtils.getLayout;   }
  public GetObjectDictionaryFunc getObjectDictionaryFunc() { return FliUtils.getObjectDictionary; }
  
  public IList<LevelRec> getLevelRecs() { return levelRecs; }
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x116C3, 8, 1, 1, 0x0),
  };
}