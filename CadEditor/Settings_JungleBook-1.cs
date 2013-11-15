using CadEditor;
using System;
using System.Drawing;

public class Data 
{ 
  public GameType getGameType()                             { return GameType.Generic; }
  public OffsetRec getScreensOffset()                       { return new OffsetRec(90441   , 1 , 16*96);   }
  public int getScreenWidth()                               { return 96; }
  public int getScreenHeight()                              { return 16; }
  public string getBlocksFilename()                         { return "jungle_book_1.png"; }
  public RenderToMainScreenFunc getRenderToMainScreenFunc() { return renderObjects; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
  
  public void renderObjects(Graphics g, int curScale)
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
  }
}