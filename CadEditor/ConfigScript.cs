using System;
using System.Collections.Generic;
using System.Text;
using CSScriptLibrary;

namespace CadEditor
{
    public delegate int   GetVideoPageAddrFunc(int videoPageId);
    public delegate byte[] GetVideoChunkFunc(int videoPageId);
    public delegate void   SetVideoChunkFunc(int videoPageId, byte[] videoChunk);
    public delegate byte[] GetBigBlocksFunc(int bigBlockId);
    public delegate void   SetBigBlocksFunc(int bigTileIndex, byte[] bigBlockIndexes);
    public delegate byte[] GetPalFunc(int palId);
    public delegate void   SetPalFunc(int palId, byte[] pallete);

    class ConfigScript
    {
        public static void LoadFromFile(string fileName)
        {
            var asm = new AsmHelper(CSScript.Load(fileName));
            var data = asm.CreateObject("Data");

            Globals.gameType = (GameType)asm.InvokeInst(data,"*.getGameType");
            palOffset = (OffsetRec)asm.InvokeInst(data,"*.getPalOffset");
            videoOffset = (OffsetRec)asm.InvokeInst(data, "*.getVideoOffset");
            videoObjOffset = (OffsetRec)asm.InvokeInst(data, "*.getVideoObjOffset");
            bigBlocksOffset = (OffsetRec)asm.InvokeInst(data, "*.getBigBlocksOffset");
            blocksOffset = (OffsetRec)asm.InvokeInst(data, "*.getBlocksOffset");
            screensOffset = (OffsetRec)asm.InvokeInst(data, "*.getScreensOffset");
            bigBlocksCount = (int)asm.InvokeInst(data,"*.getBigBlocksCount");
            screenWidth = (int)asm.InvokeInst(data, "*.getScreenWidth");
            screenHeight = (int)asm.InvokeInst(data, "*.getScreenHeight");
            levelRecs = (IList<LevelRec>)asm.InvokeInst(data,"*.getLevelRecs");

            getVideoPageAddrFunc = (GetVideoPageAddrFunc)asm.InvokeInst(data, "*.getVideoPageAddrFunc");
            getVideoChunkFunc = (GetVideoChunkFunc)asm.InvokeInst(data, "*.getVideoChunkFunc");
            setVideoChunkFunc = (SetVideoChunkFunc)asm.InvokeInst(data, "*.setVideoChunkFunc");
            getBigBlocksFunc = (GetBigBlocksFunc)asm.InvokeInst(data, "*.getBigBlocksFunc");
            setBigBlocksFunc = (SetBigBlocksFunc)asm.InvokeInst(data, "*.setBigBlocksFunc");
            getPalFunc = (GetPalFunc)asm.InvokeInst(data, "*.getPalFunc");
            setPalFunc = (SetPalFunc)asm.InvokeInst(data, "*.setPalFunc");

            isBigBlockEditorEnabled = callFromScript(asm, data, "*.isBigBlockEditorEnabled", true);
            isBlockEditorEnabled = callFromScript(asm, data, "*.isBlockEditorEnabled", true);
            isLayoutEditorEnabled = callFromScript(asm, data, "*.isLayoutEditorEnabled", true);
            isEnemyEditorEnabled = callFromScript(asm, data, "*.isEnemyEditorEnabled", true);
            isVideoEditorEnabled = callFromScript(asm, data, "*.isVideoEditorEnabled", true);
            objTypesPicturesDir = callFromScript(asm, data, "*.getObjTypesPicturesDir", "obj_sprites");

            blocksPicturesFilename = callFromScript(asm, data, "getBlocksFilename", "");
            usePicturesInstedBlocks = blocksPicturesFilename != "";

            if (Globals.gameType == GameType.CAD)
            {
                boxesBackOffset = (OffsetRec)asm.InvokeInst(data, "*.getBoxesBackOffset");
                LevelRecBaseOffset = (int)asm.InvokeInst(data, "*.getLevelRecBaseOffset");
                LevelRecDirOffset = (int)asm.InvokeInst(data, "*.getLevelRecDirOffset");
                LayoutPtrAdd = (int)asm.InvokeInst(data, "*.getLayoutPtrAdd");
                ScrollPtrAdd = (int)asm.InvokeInst(data, "*.getScrollPtrAdd");
                DirPtrAdd = (int)asm.InvokeInst(data, "*.getDirPtrAdd");
                DoorRecBaseOffset = (int)asm.InvokeInst(data, "*.getDoorRecBaseOffset");
            }

            //temp hack
            if (Globals.gameType == GameType.Generic)
            {
                try
                {
                    dwdAdvanceLastLevel = (bool)asm.InvokeInst(data,"*.isDwdAdvanceLastLevel");
                }
                catch (Exception)
                {
                }
            }
        }

        //0x90 - background memory
        //0x91 - objects    memory
        public static int getVideoPageAddr(int id)
        {
            return getVideoPageAddrFunc(id);
        }

        public static byte[] getVideoChunk(int videoPageId)
        {
            return getVideoChunkFunc(videoPageId);
        }

        public static void setVideoChunk(int videoPageId, byte[] videoChunk)
        {
           setVideoChunkFunc(videoPageId, videoChunk);
        }

        public static byte[] getBigBlocks(int bigBlockId)
        {
            return getBigBlocksFunc(bigBlockId);
        }

        public static void setBigBlocks(int bigTileIndex, byte[] bigBlockIndexes)
        {
            setBigBlocksFunc(bigTileIndex, bigBlockIndexes);
        }

        public static byte[] getPal(int palId)
        {
            return getPalFunc(palId);
        }


        public static void setPal(int palId, byte[] pallete)
        {
            setPalFunc(palId, pallete);
        }

        public static int getBigBlocksCount()
        {
            return bigBlocksCount;
        }

        public static LevelRec getLevelRec(int i)
        {
            return levelRecs[i];
        }

        //
        public static bool isDwdAdvanceLastLevel()
        {
            return dwdAdvanceLastLevel;
        }

        public static int getScreenWidth()
        {
            return screenWidth;
        }

        public static int getScreenHeight()
        {
            return screenHeight;
        }

        public static string getObjTypesPicturesDir()
        {
            return objTypesPicturesDir;
        }
       
        public static T callFromScript<T>(AsmHelper script, object data, string funcName, T defaultValue = default(T), params object[] funcParams)
        {
            try
            {
                return (T)script.InvokeInst(data, funcName, funcParams);
            }
            catch (Exception) //all exception catch...
            {
                return defaultValue;
            }
        }

        //public static GameType gameType;

        public static OffsetRec palOffset;
        public static OffsetRec videoOffset;
        public static OffsetRec videoObjOffset;
        public static OffsetRec bigBlocksOffset;
        public static OffsetRec blocksOffset;
        public static OffsetRec screensOffset;
        public static OffsetRec boxesBackOffset;
        public static int bigBlocksCount;
        public static int screenWidth;
        public static int screenHeight;

        //temp hack
        public static bool dwdAdvanceLastLevel = false;

        public static IList<LevelRec> levelRecs;

        public static GetVideoPageAddrFunc getVideoPageAddrFunc;
        public static GetVideoChunkFunc getVideoChunkFunc;
        public static SetVideoChunkFunc setVideoChunkFunc;
        public static GetBigBlocksFunc getBigBlocksFunc;
        public static SetBigBlocksFunc setBigBlocksFunc;
        public static GetPalFunc getPalFunc;
        public static SetPalFunc setPalFunc;

        public static bool isBigBlockEditorEnabled;
        public static bool isBlockEditorEnabled;
        public static bool isLayoutEditorEnabled;
        public static bool isEnemyEditorEnabled;
        public static bool isVideoEditorEnabled;

        public static bool usePicturesInstedBlocks;
        public static string blocksPicturesFilename;
        public static string objTypesPicturesDir;

        //chip and dale specific
        public static int LevelRecBaseOffset;
        public static int LevelRecDirOffset;
        public static int LayoutPtrAdd;
        public static int ScrollPtrAdd;
        public static int DirPtrAdd;
        public static int DoorRecBaseOffset;
    }
}
