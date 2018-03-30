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
        formScript.writeLog("Script for find unused blocks at all screens");
        
        var formMain = formScript.getFormMain();
        //int scrNo = formMain.ScreenNo;
        //formScript.writeLog(String.Format("Current active screen: 0x{0:X}", scrNo));
        
        int totalBlocksCount = formMain.BigBlocks.Length;
        
        formScript.writeLog(String.Format("Total big blocks count: {0}", totalBlocksCount));
        int[] usedBlocks = new int[totalBlocksCount];
        
        var layers = formMain.Layers;
        //count all used blocks
        for (int layerNo = 0; layerNo < layers.Length; layerNo++)
        {
            var layer = layers[layerNo];
            var screens = layer.screens;
            if (screens == null)
            {
                continue;
            }
            for (int scrNo = 0; scrNo < screens.Length; scrNo++)
            {
                var activeScreen = screens[scrNo];
                for (int i = 0; i < activeScreen.Length; i++)
                {
                    int blockNo = activeScreen[i];
                    usedBlocks[blockNo]++;
                }
            }
        }
        
        //print info about unused blocks
        formScript.writeLog("Unused blocks:", newLine:false);
        int unusedBlocksCount = 0;
        for (int i = 0; i < usedBlocks.Length; i++)
        {
            if (usedBlocks[i] == 0)
            {
                formScript.writeLog(String.Format(" 0x{0:X}", i), newLine:false);
                unusedBlocksCount++;
            }
        }
        if (unusedBlocksCount == 0)
        {
            formScript.writeLog(" not found", newLine:false);
        }
        formScript.writeLog();
        
        formScript.writeLog(String.Format("Total unused blocks: {0}", unusedBlocksCount));
    }
}