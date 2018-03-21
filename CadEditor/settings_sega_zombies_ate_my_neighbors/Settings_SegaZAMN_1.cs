using CadEditor;
using System;
using System.Collections.Generic;
//css_include settings_sega_zombies_ate_my_neighbors/Settings_ZAMN-Utils.cs;

public class Data 
{ 
  public bool showDumpFileField()  { return true;  }
  public OffsetRec getScreensOffset()  { return new OffsetRec(0x0, 1 , 22*13);   }
  public int getScreenWidth()          { return 22; }
  public int getScreenHeight()         { return 13; }
  public string getBlocksFilename()    { return "zamn_1.png"; }
  public int getWordLen()              { return 2;} 
  
  public GetLevelRecsFunc getLevelRecsFunc() { return ()=> {return levelRecs;}; }
  public GetObjectsFunc getObjectsFunc()   { return ZamnUtils.getObjects;/*getEnemiesFromFile*/ }
  public SetObjectsFunc setObjectsFunc()   { return ZamnUtils.setObjects;/*setEnemiesToFile;*/ }
  
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x2A972, 14, 1, 1, 0x0),
  };
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return true; }
}