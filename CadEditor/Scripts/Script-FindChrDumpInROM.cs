using CadEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

static class ArrayExtensions
{
    public static IEnumerable<int> StartingIndex(this byte[] x, byte[] y)
    {
        IEnumerable<int> index = Enumerable.Range(0, x.Length-y.Length+1);
        for (int i = 0; i < y.Length; i++)
        {
            index = index.Where(n => x[n + i] == y[i]).ToArray();
        }
        return index;
    }
}

public class Script
{
    public void Execute(FormScript formScript)
    {
        formScript.writeLog();
        formScript.writeLog("Script for search addresses of video dumps in ROM file");
        var romdata = Globals.romdata;
        
        //information about video
        int chrDumpsCount = ConfigScript.videoOffset.recCount;
        formScript.writeLog();
        formScript.writeLog(String.Format("Video chunks count: {0}", chrDumpsCount));
        formScript.writeLog("--------------------------------------------------------------------");
        for (int i = 0; i < chrDumpsCount; i++)
        {
            var chr = ConfigScript.getVideoChunk(i);
            int[] foundIndexes  = romdata.StartingIndex(chr).ToArray();
            if (foundIndexes.Length > 0)
            {
                formScript.writeLog(String.Format("Video chunk {0}. Found at address: ", i), newLine:false);
                foreach (int index in foundIndexes)
                {
                    formScript.writeLog(String.Format("0x{0:X} ", index), newLine:false);
                }
                formScript.writeLog();
            }
            else
            {
              formScript.writeLog(String.Format("Video chunk {0}. Not found in ROM", i));
            }
        }
        formScript.writeLog("--------------------------------------------------------------------");
        formScript.writeLog();
        
        //information about pallettes
        int palsCount = ConfigScript.palOffset.recCount;
        formScript.writeLog();
        formScript.writeLog(String.Format("Pallettes count: {0}", palsCount));
        formScript.writeLog("--------------------------------------------------------------------");
        for (int i = 0; i < palsCount; i++)
        {
            var pal = ConfigScript.getPal(i);
            int[] foundIndexes  = romdata.StartingIndex(pal).ToArray();
            if (foundIndexes.Length > 0)
            {
                formScript.writeLog(String.Format("Pallette {0}. Found at address: ", i), newLine:false);
                foreach (int index in foundIndexes)
                {
                    formScript.writeLog(String.Format("0x{0:X} ", index), newLine:false);
                }
                formScript.writeLog();
            }
            else
            {
              formScript.writeLog(String.Format("Pallete {0}. Not found in ROM", i));
            }
        }
        formScript.writeLog("--------------------------------------------------------------------");
        formScript.writeLog();
    }
}