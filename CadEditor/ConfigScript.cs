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

    class ConfigScript
    {
        public static void LoadFromFile(string fileName)
        {
            var asm = new AsmHelper(CSScript.Load(fileName));

            Globals.gameType = (GameType)asm.Invoke("*.getGameType");
            palOffset = (OffsetRec)asm.Invoke("*.getPalOffset");
            videoOffset = (OffsetRec)asm.Invoke("*.getVideoOffset");
            videoObjOffset = (OffsetRec)asm.Invoke("*.getVideoObjOffset");
            bigBlocksOffset = (OffsetRec)asm.Invoke("*.getBigBlocksOffset");
            blocksOffset = (OffsetRec)asm.Invoke("*.getBlocksOffset");
            screensOffset = (OffsetRec)asm.Invoke("*.getScreensOffset");
            bigBlocksCount = (int)asm.Invoke("*.getBigBlocksCount");
            levelRecs = (IList<LevelRec>)asm.Invoke("*.getLevelRecs");

            getVideoPageAddrFunc = (GetVideoPageAddrFunc)asm.Invoke("*.getVideoPageAddrFunc");
            getVideoChunkFunc = (GetVideoChunkFunc)asm.Invoke("*.getVideoChunkFunc");
            setVideoChunkFunc = (SetVideoChunkFunc)asm.Invoke("*.setVideoChunkFunc");
            getBigBlocksFunc = (GetBigBlocksFunc)asm.Invoke("*.getBigBlocksFunc");
            setBigBlocksFunc = (SetBigBlocksFunc)asm.Invoke("*.setBigBlocksFunc");

            isBigBlockEditorEnabled = callFromScript(asm, "*.isBigBlockEditorEnabled", true);
            isBlockEditorEnabled = callFromScript(asm, "*.isBlockEditorEnable", true);
            isLayoutEditorEnabled = callFromScript(asm, "*.isLayoutEditorEnabled", true);
            isEnemyEditorEnabled = callFromScript(asm, "*.isEnemyEditorEnabled", true);
            isVideoEditorEnabled = callFromScript(asm, "*.isVideoEditorEnabled", true);

            if (Globals.gameType == GameType.CAD)
            {
                boxesBackOffset = (OffsetRec)asm.Invoke("*.getBoxesBackOffset");
                LevelRecBaseOffset = (int)asm.Invoke("*.getLevelRecBaseOffset");
                LevelRecDirOffset = (int)asm.Invoke("*.getLevelRecDirOffset");
                LayoutPtrAdd = (int)asm.Invoke("*.getLayoutPtrAdd");
                ScrollPtrAdd = (int)asm.Invoke("*.getScrollPtrAdd");
                DirPtrAdd = (int)asm.Invoke("*.getDirPtrAdd");
                DoorRecBaseOffset = (int)asm.Invoke("*.getDoorRecBaseOffset");
            }

            //temp hack
            if (Globals.gameType == GameType.Generic)
            {
                try
                {
                    dwdAdvanceLastLevel = (bool)asm.Invoke("*.isDwdAdvanceLastLevel");
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
       
        public static T callFromScript<T>(AsmHelper script, string funcName, T defaultValue = default(T), params object[] funcParams)
        {
            try
            {
                return (T)script.Invoke(funcName, funcParams);
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

        //temp hack
        public static bool dwdAdvanceLastLevel = false;

        public static IList<LevelRec> levelRecs;

        public static GetVideoPageAddrFunc getVideoPageAddrFunc;
        public static GetVideoChunkFunc getVideoChunkFunc;
        public static SetVideoChunkFunc setVideoChunkFunc;
        public static GetBigBlocksFunc getBigBlocksFunc;
        public static SetBigBlocksFunc setBigBlocksFunc;

        public static bool isBigBlockEditorEnabled;
        public static bool isBlockEditorEnabled;
        public static bool isLayoutEditorEnabled;
        public static bool isEnemyEditorEnabled;
        public static bool isVideoEditorEnabled;

        //chip and dale specific
        public static int LevelRecBaseOffset;
        public static int LevelRecDirOffset;
        public static int LayoutPtrAdd;
        public static int ScrollPtrAdd;
        public static int DirPtrAdd;
        public static int DoorRecBaseOffset;
    }
}
