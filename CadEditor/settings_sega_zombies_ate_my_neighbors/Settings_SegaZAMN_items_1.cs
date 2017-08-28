using CadEditor;
using System;
using System.Collections.Generic;
//css_include Settings_ZAMN-Utils.cs;

public class Data 
{ 
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x0, 1 , 22*13);   }
  public int getScreenWidth()          { return 22; }
  public int getScreenHeight()         { return 13; }
  public string getBlocksFilename()    { return "zamn_1.png"; }
  public int getWordLen()              { return 2;} 
  
  public IList<LevelRec> getLevelRecs()    { return levelRecs; }
  public GetObjectsFunc getObjectsFunc()   { return ZamnUtils.getItemsFromRom;/*getItemsFromRom*/ }
  public SetObjectsFunc setObjectsFunc()   { return ZamnUtils.setItemsToRom;/*setItemsToFile*/ }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x2AA96, 10, 1, 1, 0x0),
  };
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return true; }
}