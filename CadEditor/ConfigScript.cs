using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using CSScriptLibrary;

namespace CadEditor
{
    public delegate int   GetVideoPageAddrFunc(int videoPageId);
    public delegate byte[] GetVideoChunkFunc(int videoPageId);
    public delegate void   SetVideoChunkFunc(int videoPageId, byte[] videoChunk);
    public delegate ObjRec[] GetBlocksFunc(int blockId);
    public delegate void   SetBlocksFunc(int blockIndex, ObjRec[] blocksData);
    public delegate byte[] GetBigBlocksFunc(int bigBlockId);
    public delegate void   SetBigBlocksFunc(int bigTileIndex, byte[] bigBlockIndexes);
    public delegate byte[] GetPalFunc(int palId);
    public delegate void   SetPalFunc(int palId, byte[] pallete);
    public delegate void   RenderToMainScreenFunc(Graphics g, int curScale);
    public delegate List<ObjectRec> GetObjectsFunc(int levelNo);
    public delegate bool            SetObjectsFunc(int levelNo, List<ObjectRec> objects); 
    public delegate void            SortObjectsFunc(int levelNo, List<ObjectRec> objects);
    public delegate LevelLayerData  GetLayoutFunc(int levelNo);
    public delegate Dictionary<String, int> GetObjectDictionaryFunc(int objNo);
    public delegate int ConvertScreenTileFunc(int val);

    public class ConfigScript
    {
        public static void LoadGlobalsFromFile(string fileName)
        {
            try
            {
                var asm = new AsmHelper(CSScript.Load(fileName));
                object data = asm.CreateObject("Config");
                romName = callFromScript(asm, data, "*.getFileName", "");
                cfgName = callFromScript(asm, data, "*.getConfigName", "");
                dumpName = callFromScript(asm, data, "*.getDumpName", "");
                showDumpFileField = callFromScript(asm, data, "*.showDumpFileField", false);
                nesColors = callFromScript<Color[]>(asm, data, "*.getNesColors", null);
            }
            catch (Exception)
            {
            }
        }

        public static void LoadFromFile(string fileName)
        {
            var asm = new AsmHelper(CSScript.Load(fileName));
            object data;
            try
            {
                data = asm.CreateObject("Data");
            }
            catch (Exception)
            {
                return;
            }
            Directory.SetCurrentDirectory(Path.GetDirectoryName(fileName));
            Globals.gameType = (GameType)asm.InvokeInst(data,"*.getGameType");

            levelsCount = callFromScript(asm, data, "*.getLevelsCount", 1);
            screensOffset = new OffsetRec[levelsCount];

            palOffset = callFromScript(asm, data,"*.getPalOffset", new OffsetRec(0,1,0));
            videoOffset = callFromScript(asm, data, "*.getVideoOffset", new OffsetRec(0, 1, 0));
            videoObjOffset = callFromScript(asm, data, "*.getVideoObjOffset", new OffsetRec(0, 1, 0));
            bigBlocksOffset = callFromScript(asm, data, "*.getBigBlocksOffset", new OffsetRec(0, 1, 0));
            blocksOffset = callFromScript(asm, data, "*.getBlocksOffset", new OffsetRec(0, 1, 0));
            screensOffset[0]        = callFromScript(asm, data, "*.getScreensOffset", new OffsetRec(0, 1, 0));
            screensOffset[0].width  = callFromScript(asm, data, "*.getScreenWidth", 8);
            screensOffset[0].height = callFromScript(asm, data, "*.getScreenHeight", 8);
            if ((screensOffset[0].beginAddr == 0) && (screensOffset[0].recSize == 0))
            {
                screensOffset = callFromScript(asm, data, "*.getScreensOffsetsForLevels", new OffsetRec[1]);
            }
            screensOffset2 = callFromScript(asm, data, "*.getScreensOffset2", new OffsetRec(0, 1, 0));
            screenVertical = callFromScript(asm, data, "*.getScreenVertical", false);
            screenDataStride = callFromScript(asm, data, "*.getScreenDataStride", 1);
            wordLen = callFromScript(asm, data, "*.getWordLen", 1);
            littleEndian = callFromScript(asm, data, "*.isLittleEndian", false);
            useSegaGraphics = callFromScript(asm, data, "*.isUseSegaGraphics", false);
            buildScreenFromSmallBlocks = callFromScript(asm, data, "isBuildScreenFromSmallBlocks", false);
            layersCount = callFromScript(asm, data, "*.getLayersCount", 1);
            levelRecs = callFromScript(asm, data,"*.getLevelRecs", new List<LevelRec>());

            minObjCoordX = callFromScript(asm, data, "*.getMinObjCoordX", 0);
            minObjCoordY = callFromScript(asm, data, "*.getMinObjCoordY", 0);
            minObjType   = callFromScript(asm, data, "*.getMinObjType"  , 0);
            maxObjCoordX = callFromScript(asm, data, "*.getMaxObjCoordX", -1); //ConfigScript.getScreenWidth() * 32
            maxObjCoordY = callFromScript(asm, data, "*.getMaxObjCoordY", -1); //ConfigScript.getScreenHeight() * 32;
            maxObjType   = callFromScript(asm, data, "*.getMaxObjType"  , -1); //256

            getVideoPageAddrFunc = callFromScript <GetVideoPageAddrFunc>(asm, data, "*.getVideoPageAddrFunc");
            getVideoChunkFunc = callFromScript<GetVideoChunkFunc>(asm, data, "*.getVideoChunkFunc");
            setVideoChunkFunc = callFromScript<SetVideoChunkFunc>(asm, data, "*.setVideoChunkFunc");
            getBigBlocksFunc = callFromScript<GetBigBlocksFunc>(asm, data, "*.getBigBlocksFunc");
            setBigBlocksFunc = callFromScript<SetBigBlocksFunc>(asm, data, "*.setBigBlocksFunc");
            getBlocksFunc = callFromScript<GetBlocksFunc>(asm,data,"*.getBlocksFunc");
            setBlocksFunc = callFromScript<SetBlocksFunc>(asm, data, "*.setBlocksFunc");
            getPalFunc = callFromScript<GetPalFunc>(asm, data, "*.getPalFunc");
            setPalFunc = callFromScript<SetPalFunc>(asm, data, "*.setPalFunc");
            getObjectsFunc = callFromScript<GetObjectsFunc>(asm, data, "*.getObjectsFunc");
            setObjectsFunc = callFromScript<SetObjectsFunc>(asm, data, "*.setObjectsFunc");
            sortObjectsFunc = callFromScript<SortObjectsFunc>(asm, data, "*.sortObjectsFunc");
            getLayoutFunc = callFromScript<GetLayoutFunc>(asm, data, "*.getLayoutFunc");
            convertScreenTileFunc = callFromScript<ConvertScreenTileFunc>(asm, data, "*.getConvertScreenTileFunc");
            backConvertScreenTileFunc = callFromScript<ConvertScreenTileFunc>(asm, data, "*.getBackConvertScreenTileFunc");
            getObjectDictionaryFunc = callFromScript<GetObjectDictionaryFunc>(asm, data, "*.getObjectDictionaryFunc");

            renderToMainScreenFunc = callFromScript<RenderToMainScreenFunc>(asm, data, "*.getRenderToMainScreenFunc");

            isBigBlockEditorEnabled = callFromScript(asm, data, "*.isBigBlockEditorEnabled", true);
            isBlockEditorEnabled = callFromScript(asm, data, "*.isBlockEditorEnabled", true);
            isLayoutEditorEnabled = callFromScript(asm, data, "*.isLayoutEditorEnabled", true);
            isEnemyEditorEnabled = callFromScript(asm, data, "*.isEnemyEditorEnabled", true);
            isVideoEditorEnabled = callFromScript(asm, data, "*.isVideoEditorEnabled", true);
            isMapEditorEnabled = callFromScript(asm, data, "*.isMapEditorEnabled", false); //specific for dwd
            objTypesPicturesDir = callFromScript(asm, data, "*.getObjTypesPicturesDir", "obj_sprites");

            showScrollsInLayout = callFromScript(asm, data, "*.isShowScrollsInLayout", true);
            scrollsOffsetFromLayout = callFromScript(asm, data, "*.getScrollsOffsetFromLayout", 0);

            bigBlocksCount = callFromScript(asm, data, "*.getBigBlocksCount", 256);
            blocksCount    = callFromScript(asm, data, "*.getBlocksCount"   , 256);

            blocksPicturesFilename  = callFromScript(asm, data, "getBlocksFilename", "");
            if (blocksPicturesFilename != "" && !File.Exists(blocksPicturesFilename))
                throw new Exception("File does not exists: " + blocksPicturesFilename);
            blocksPicturesFilenames = callFromScript<string[]>(asm, data, "getBlocksFilenames", null);
            if (blocksPicturesFilenames!=null)
              for (int i = 0; i < blocksPicturesFilenames.Length; i++)
                if (!File.Exists(blocksPicturesFilenames[i]))
                    throw new Exception("File does not exists: " + blocksPicturesFilenames[i]);
            blocksPicturesWidth = callFromScript(asm, data, "getPictureBlocksWidth", 32); 
            usePicturesInstedBlocks = blocksPicturesFilename != "" || blocksPicturesFilenames != null;

            blockTypeNames = callFromScript(asm, data, "getBlockTypeNames", defaultBlockTypeNames);

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
        }

        //0x90 - background memory
        //0x91 - objects    memory
        public static int getVideoPageAddr(int id)
        {
            return getVideoPageAddrFunc(id);
        }

        public static byte[] getVideoChunk(int videoPageId)
        {
            return (getVideoChunkFunc ?? (_=>null))(videoPageId);
        }

        public static void setVideoChunk(int videoPageId, byte[] videoChunk)
        {
           setVideoChunkFunc(videoPageId, videoChunk);
        }

        public static byte[] getBigBlocks(int bigBlockId)
        {
            return (getBigBlocksFunc ?? (_ => null))(bigBlockId);
        }

        public static void setBigBlocks(int bigTileIndex, byte[] bigBlockIndexes)
        {
            setBigBlocksFunc(bigTileIndex, bigBlockIndexes);
        }

        public static ObjRec[] getBlocks(int bigBlockId)
        {
            return (getBlocksFunc ?? (_ => null))(bigBlockId);
        }

        public static void setBlocks(int bIndex, ObjRec[] blocks)
        {
            setBlocksFunc(bIndex, blocks);
        }

        public static byte[] getPal(int palId)
        {
            return (getPalFunc ?? (_ => null))(palId);
        }


        public static void setPal(int palId, byte[] pallete)
        {
            setPalFunc(palId, pallete);
        }

        public static List<ObjectRec> getObjects(int levelNo)
        {
            return (getObjectsFunc ?? (_ => null))(levelNo);
        }

        public static void setObjects(int levelNo, List<ObjectRec> objects)
        {
            setObjectsFunc(levelNo, objects);
        }
        
        public static void sortObjects(int levelNo, List<ObjectRec> objects)
        {
            sortObjectsFunc(levelNo, objects);
        }

         public static int convertScreenTile(int tile)
         {
             return (convertScreenTileFunc ?? (v=>v))(tile);
         }

         public static int backConvertScreenTile(int tile)
         {
             return (backConvertScreenTileFunc ?? (v => v))(tile);
         }

        public static LevelLayerData getLayout(int levelNo)
        {
            return getLayoutFunc(levelNo);
        }

        public static Dictionary<String, int> getObjectDictionary(int objType)
        {
            return (getObjectDictionaryFunc ?? (_ => null))(objType);
        }

        public static void renderToMainScreen(Graphics g, int scale)
        {
            if (renderToMainScreenFunc!=null)
                renderToMainScreenFunc(g, scale);
        }

        public static int getBigBlocksCount()
        {
            return bigBlocksCount;
        }

        public static int getBlocksCount()
        {
            return blocksCount;
        }

        public static LevelRec getLevelRec(int i)
        {
            return levelRecs[i];
        }

        public static int getScreenWidth(int levelNo)
        {
            return screensOffset[levelNo].width;
        }

        public static int getScreenHeight(int levelNo)
        {
            return screensOffset[levelNo].height;
        }

        public static int getMaxObjCoordX()
        {
            return maxObjCoordX;
        }

        public static int getMaxObjCoordY()
        {
            return maxObjCoordY;
        }

        public static int getMaxObjType()
        {
            return maxObjType;
        }

        public static int getMinObjCoordX()
        {
            return minObjCoordX;
        }

        public static int getMinObjCoordY()
        {
            return minObjCoordY;
        }

        public static int getMinObjType()
        {
            return minObjType;
        }

        public static string getObjTypesPicturesDir()
        {
            return objTypesPicturesDir;
        }

        public static string[] getBlockTypeNames()
        {
            return blockTypeNames;
        }

        public static bool getScreenVertical()
        {
            return screenVertical;
        }

        public static int getScreenDataStride()
        {
            return screenDataStride;
        }

        public static int getWordLen()
        {
            return wordLen;
        }

        public static bool isLittleEndian()
        {
            return littleEndian;
        }

        public static bool isBuildScreenFromSmallBlocks()
        {
            return buildScreenFromSmallBlocks;
        }

        public static int getBlocksPicturesWidth()
        {
            return blocksPicturesWidth;
        }

        public static int getLayersCount()
        {
            return layersCount;
        }

        public static bool isShowScrollsInLayout()
        {
            return showScrollsInLayout;
        }

        public static int getScrollsOffsetFromLayout()
        {
            return scrollsOffsetFromLayout;
        }

        public static int getLevelsCount()
        {
            return levelsCount;
        }

        public static bool isUseSegaGraphics()
        {
            return useSegaGraphics;
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
        public static OffsetRec[] screensOffset;
        public static OffsetRec screensOffset2;
        public static OffsetRec boxesBackOffset;
        public static int levelsCount;
        public static int bigBlocksCount;
        public static int blocksCount;
        public static bool screenVertical;
        public static int screenDataStride;
        public static int layersCount;
        public static int wordLen;
        public static bool littleEndian;
        public static bool buildScreenFromSmallBlocks;

        public static bool useSegaGraphics;

        public static int minObjCoordX;
        public static int minObjCoordY;
        public static int minObjType;
        public static int maxObjCoordX;
        public static int maxObjCoordY;
        public static int maxObjType;

        public static IList<LevelRec> levelRecs;

        public static GetVideoPageAddrFunc getVideoPageAddrFunc;
        public static GetVideoChunkFunc getVideoChunkFunc;
        public static SetVideoChunkFunc setVideoChunkFunc;
        public static GetBigBlocksFunc getBigBlocksFunc;
        public static SetBigBlocksFunc setBigBlocksFunc;
        public static GetBlocksFunc getBlocksFunc;
        public static SetBlocksFunc setBlocksFunc;
        public static GetPalFunc getPalFunc;
        public static SetPalFunc setPalFunc;
        public static GetObjectsFunc getObjectsFunc;
        public static SetObjectsFunc setObjectsFunc;
        public static SortObjectsFunc sortObjectsFunc;
        public static GetLayoutFunc getLayoutFunc;
        public static GetObjectDictionaryFunc getObjectDictionaryFunc;
        public static RenderToMainScreenFunc renderToMainScreenFunc;
        public static ConvertScreenTileFunc convertScreenTileFunc;
        public static ConvertScreenTileFunc backConvertScreenTileFunc;

        public static bool isBigBlockEditorEnabled;
        public static bool isBlockEditorEnabled;
        public static bool isLayoutEditorEnabled;
        public static bool isEnemyEditorEnabled;
        public static bool isVideoEditorEnabled;
        public static bool isMapEditorEnabled;

        public static bool showScrollsInLayout;
        public static int scrollsOffsetFromLayout;

        public static bool usePicturesInstedBlocks;
        public static string blocksPicturesFilename;
        public static string[] blocksPicturesFilenames;
        public static int blocksPicturesWidth;
        public static string objTypesPicturesDir;

        public static string[] blockTypeNames;
        public static string[] defaultBlockTypeNames = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };

        //chip and dale specific
        public static int LevelRecBaseOffset;
        public static int LevelRecDirOffset;
        public static int LayoutPtrAdd;
        public static int ScrollPtrAdd;
        public static int DirPtrAdd;
        public static int DoorRecBaseOffset;

        //global editor settings
        public static string  romName;
        public static string dumpName;
        public static string  cfgName;
        public static Color[] nesColors;
        public static bool showDumpFileField;
    }
}
