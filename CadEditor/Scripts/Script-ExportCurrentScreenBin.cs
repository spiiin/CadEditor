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
        formScript.writeLog("Script for export current screen to binary array (one byte per index)");
        
        var formMain = formScript.getFormMain();
        formScript.writeLog();
        
        int currentScreenNo = formMain.screenNo;
        formScript.writeLog(String.Format("Current active screen: {0}", currentScreenNo));
        var activeScreen = formMain.screens[currentScreenNo].layers[0].data;
        var data = activeScreen.Select(x=>(byte)x).ToArray();
        var fname = ConfigScript.ConfigDirectory + String.Format("screen{0}.bin", currentScreenNo);
        using (FileStream f = File.OpenWrite(fname))
        {
           f.Write(data, 0, data.Length);
        }
        formScript.writeLog(String.Format("Screen exported to file: {0}", fname));
    }
}