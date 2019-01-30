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
        
        int currentScreenNo = formMain.screenNo;
        formScript.writeLog(String.Format("Current active screen: {0}", currentScreenNo));
        
        var activeScreen = formMain.screens[currentScreenNo];
        int w = activeScreen.width;
        int h = activeScreen.height;
        var im = MapEditor.screenToImage(formMain.screens, currentScreenNo, new MapEditor.RenderParams
        {
            bigBlocks = formMain.bigBlocks,
            curScale = formMain.curScale,
            width = w,
            height = h,
        });
        var fname = ConfigScript.ConfigDirectory + String.Format("screen{0}.png", currentScreenNo);
        im.Save(fname);
        formScript.writeLog(String.Format("Screen exported to file: {0}", fname));
    }
}