using CadEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class Script
{
    public void Execute(FormScript formScript)
    {
        formScript.writeLog();
        formScript.writeLog("Script for export current screen to png image");
        
        var formMain = formScript.getFormMain();
        formScript.writeLog();
        
        int currentScreenNo = formMain.ScreenNo;
        formScript.writeLog(String.Format("Current active screen: {0}", currentScreenNo));
        
        var activeScreen = formMain.Layers[0].screens[currentScreenNo];
        int w = ConfigScript.getScreenWidth(formMain.LevelNoForScreens);
        int h = ConfigScript.getScreenHeight(formMain.LevelNoForScreens);
        var im = MapEditor.ScreenToImage(formMain.BigBlocks, formMain.Layers, currentScreenNo, formMain.CurScale, false, 0, 0, w, h);
        var fname = ConfigScript.ConfigDirectory + String.Format("screen{0}.png", currentScreenNo);
        im.Save(fname);
        formScript.writeLog(String.Format("Screen exported to file: {0}", fname));
    }
}