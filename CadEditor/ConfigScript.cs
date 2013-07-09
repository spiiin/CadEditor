using System;
using System.Collections.Generic;
using System.Text;
using CSScriptLibrary;

namespace CadEditor
{
    public delegate int GetVideoPageAddrFunc(int videoPageId);
    public delegate byte[] GetVideoChunkFunc(int videoPageId);

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

            getVideoPageAddrFunc = (GetVideoPageAddrFunc)asm.Invoke("*.getVideoPageAddrFunc");
            getVideoChunkFunc = (GetVideoChunkFunc)asm.Invoke("*.getVideoChunkFunc");

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

        public static int getBigBlocksCount()
        {
            return bigBlocksCount;
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

        public static GetVideoPageAddrFunc getVideoPageAddrFunc;
        public static GetVideoChunkFunc getVideoChunkFunc;

        //chip and dale specific
        public static int LevelRecBaseOffset;
        public static int LevelRecDirOffset;
        public static int LayoutPtrAdd;
        public static int ScrollPtrAdd;
        public static int DirPtrAdd;
        public static int DoorRecBaseOffset;
    }
}
