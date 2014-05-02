using CadEditor;
using System;
using System.Collections.Generic;
using System.Drawing;

public class Data
{ 
  public GameType getGameType()  { return GameType.Generic; }
  public OffsetRec getScreensOffset()     { return new OffsetRec(166366   , 1 , 11*64);  }
  public int getScreenWidth()    { return 64; }
  public int getScreenHeight()   { return 11; }
  public int getBigBlocksCount() { return 44; }
  public string getBlocksFilename()       { return "battletoads_1.png"; }
  
  public bool isBigBlockEditorEnabled() { return false; }
  public bool isBlockEditorEnabled()    { return false; }
  public bool isLayoutEditorEnabled()   { return false; }
  public bool isEnemyEditorEnabled()    { return false; }
  public bool isVideoEditorEnabled()    { return false; }
  
  public RenderToMainScreenFunc getRenderToMainScreenFunc() { return renderObjects; }
  
  public void renderObjects(Graphics g, int curScale)
  {
    for (int i = 0; i < 64; i++)
    {
        byte b1  = Globals.romdata[0x2BE4D + i*6];
        byte b2  = Globals.romdata[0x2BE4D + i*6 + 1];
        byte b3  = Globals.romdata[0x2BE4D + i*6 + 2];
        byte b4  = Globals.romdata[0x2BE4D + i*6 + 3];
        byte b5  = Globals.romdata[0x2BE4D + i*6 + 4];
        byte b6  = Globals.romdata[0x2BE4D + i*6 + 5];
        var rect = new Rectangle(32*curScale*(i+1), 8*32*curScale, 32*curScale*(i+2), (curScale*32*8)+80);
        g.DrawRectangle(new Pen(Color.Red, 4.0f), rect);
        g.DrawString(String.Format("{0:X2}", b1), new Font("Arial", 8), Brushes.Red, rect);
        g.DrawString(String.Format("{0:X2}", b2), new Font("Arial", 8), Brushes.Red, rect.X + 16, rect.Y);
        g.DrawString(String.Format("{0:X2}", b3), new Font("Arial", 8), Brushes.Red, rect.X + 32, rect.Y);
        g.DrawString(String.Format("{0:X2}", b4), new Font("Arial", 8), Brushes.Red, rect.X + 0 , rect.Y+16);
        g.DrawString(String.Format("{0:X2}", b5), new Font("Arial", 8), Brushes.Red, rect.X + 16, rect.Y+16);
        g.DrawString(String.Format("{0:X2}", b6), new Font("Arial", 8), Brushes.Red, rect.X + 32, rect.Y+16);
    }
  }
}