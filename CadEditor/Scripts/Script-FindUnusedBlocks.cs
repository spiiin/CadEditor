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
        var unusedBlocks = usedBlocks.Select((c,index)=> new {Count=c, Index=index}).Where(c=>c.Count==0).Select(c=>c.Index).ToArray();
        formScript.writeLog("Unused blocks:", newLine:false);
        for (int i = 0; i < unusedBlocks.Length; i++)
        {
            formScript.writeLog(String.Format(" 0x{0:X}", unusedBlocks[i]), newLine:false);
        }
        int unusedBlocksCount = unusedBlocks.Length;
        if (unusedBlocksCount == 0)
        {
            formScript.writeLog(" not found", newLine:false);
        }
        formScript.writeLog();
        
        formScript.writeLog(String.Format("Total unused blocks: {0}", unusedBlocksCount));
        
        if (unusedBlocksCount > 0)
        {
            var unusedPictures = formMain.BigBlocks.Where((im, index)=> unusedBlocks.Contains(index)).ToArray();
            var glueImage = UtilsGDI.GlueImages(unusedPictures, unusedPictures.Length, 1);
            var fname = ConfigScript.ConfigDirectory + String.Format("unusedBlocks.png");
            glueImage.Save(fname);
            formScript.writeLog(String.Format("Unused blocks image saved to file: {0}", fname));
        }
    }
}