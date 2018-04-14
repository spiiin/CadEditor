using CadEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Script
{
    public void Execute(FormScript formScript)
    {
        formScript.writeLog();
        formScript.writeLog("Script for export all CHRs to png files");
        var romdata = Globals.romdata;
        
        //information about video
        int chrDumpsCount = ConfigScript.videoOffset.recCount;
        formScript.writeLog();
        formScript.writeLog(String.Format("Video chunks count: {0}", chrDumpsCount));
        formScript.writeLog("--------------------------------------------------------------------");
        
        var formMain = formScript.getFormMain();
        int palNo = formMain.CurActivePalleteNo;
        
        var pal = ConfigScript.getPal(palNo);
        for (int i = 0; i < chrDumpsCount; i++)
        {
            var chr = ConfigScript.getVideoChunk(i);
            var nesVideo = ConfigScript.videoNes;
            var png = nesVideo.makeImageRectangle(chr, pal, 0);
            var fname = ConfigScript.ConfigDirectory + String.Format("chr_{0:000}.png", i);
            png.Save(fname);
            formScript.writeLog(String.Format("Video chunk {0} saved to file: {1}", i, fname));
        }
        formScript.writeLog("--------------------------------------------------------------------");
        formScript.writeLog();
    }
}