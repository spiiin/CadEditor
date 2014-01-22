using CadEditor;
using System.Collections.Generic;
public class Data
{ 
  public GameType getGameType()           { return GameType.TT; }
  public OffsetRec getScreensOffset()     { return new OffsetRec(0xD832, 1 , 8*96);   }
  public int getScreenWidth()             { return 8; }
  public int getScreenHeight()            { return 96; }
  public bool getScreenVertical()         { return true; }
  public string getBlocksFilename()       { return "flintstones2_1.png"; }
  public int    getPictureBlocksWidth()   { return 16; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return true; }
  public bool isVideoEditorEnabled()    { return false; }
  public IList<LevelRec> getLevelRecs() { return levelRecsJB; }
   
  public IList<LevelRec> levelRecsJB = new List<LevelRec>() 
  {
    new LevelRec(0x0, 0, 1, 1, 0x0),
  };
}