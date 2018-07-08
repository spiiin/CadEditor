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
        formScript.writeLog("Script to export 2x2 blocks and blocks attributes to five separate arrays (SoA - Structure of Arrays)");
        
        var formMain = formScript.getFormMain();
        formScript.writeLog();
        
        int curActiveBlockset = formMain.curActiveBigBlockNo;
        var blocks = ConfigScript.getBlocks(curActiveBlockset);
        int blocksCount = blocks.Length;
        formScript.writeLog(String.Format("Current selected block set: {0}. Blocks count: {1}", curActiveBlockset, blocksCount));
        
        var data = new byte[blocksCount*5];
        //cad editor function to write 2x2 blocks (optional with palBytes and types) to data array in SoA form
        Utils.writeBlocksToAlignedArrays(blocks, data, 0, blocksCount, withPal:true, writeType:false);
        
        for (int part = 0; part < 5; part++)
        {
            //prepare data part
            var dataPart = new byte[blocksCount];
            Array.Copy(data, blocksCount*part, dataPart, 0, blocksCount);
            //and write it to file
            var fname = ConfigScript.ConfigDirectory + String.Format("blocks{0}_part{1}.bin", curActiveBlockset, part);
            using (FileStream f = File.OpenWrite(fname))
            {
               f.Write(dataPart, 0, dataPart.Length);
            }
            formScript.writeLog(String.Format("  Part {0} saved to file {1}", part, fname));
        }
    }
}