using CadEditor;
using System;
using System.Collections.Generic;
//css_include settings_little_nemo/LittleNemoUtils.cs;

public class Data 
{ 
  public string[] getPluginNames() 
  {
    return new string[] 
    {
      "PluginEditLayout.dll",
    };
  }
  public OffsetRec getScreensOffset()  { return new OffsetRec(67856, 22 , 8*8);   }
  public int getScreenWidth()          { return 8; }
  public int getScreenHeight()         { return 8; }
  public string getBlocksFilename()    { return "little_nemo_8-2.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isEnemyEditorEnabled()    { return true; }
  
  public GetObjectsFunc getObjectsFunc()   { return LittleNemoUtils.getObjectsNemo; }
  public SetObjectsFunc setObjectsFunc()   { return LittleNemoUtils.setObjectsNemo; }
  public SortObjectsFunc sortObjectsFunc() { return LittleNemoUtils.sortObjectsNemo; }
  
  public GetLayoutFunc getLayoutFunc() { return LittleNemoUtils.getLayoutLinearPlusOne; }
  public SetLayoutFunc setLayoutFunc() { return LittleNemoUtils.setLayoutLinearPlusOne; }
  public int getPalBytesAddr()         { return 0x10F50; }
  
  public IList<LevelRec> getLevelRecs() { return levelRecs; }
  public IList<LevelRec> levelRecs = new List<LevelRec>() 
  {
    new LevelRec(0x10fa1, 38, 16, 5, 0x10e90), 
  };
}