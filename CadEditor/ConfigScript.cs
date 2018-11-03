using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CSScriptLibrary;

namespace CadEditor
{
    public delegate int   GetVideoPageAddrFunc(int videoPageId);
    public delegate byte[] GetVideoChunkFunc(int videoPageId);
    public delegate void   SetVideoChunkFunc(int videoPageId, byte[] videoChunk);

    public delegate ObjRec[] GetBlocksFunc(int blockId);
    public delegate void   SetBlocksFunc(int blockIndex, ObjRec[] blocksData);
    public delegate int GetBlocksAddrFunc(int blockId);
    public delegate int GetBlocksCountFunc(int blockId);

    public delegate BigBlock[] GetBigBlocksFunc(int bigBlockId);
    public delegate void   SetBigBlocksFunc(int bigTileIndex, BigBlock[] bigBlocks);
    public delegate int GetBigBlocksAddrFunc(int bigBlockId);
    public delegate int GetBigBlocksCountFunc(int hierLevel, int bigBlockId);

    public delegate byte[] GetSegaMappingFunc(int bigBlockId);
    public delegate void   SetSegaMappingFunc(int bigTileIndex, byte[] bigBlocks);

    public delegate byte[] GetPalFunc(int palId);
    public delegate void   SetPalFunc(int palId, byte[] pallete);

    public delegate GroupRec[] GetGroupsFunc();
    public delegate IList<LevelRec> GetLevelRecsFunc();

    public delegate void   RenderToMainScreenFunc(Graphics g, int curScale, int scrNo);

    public delegate List<ObjectList> GetObjectsFunc(int levelNo);
    public delegate bool            SetObjectsFunc(int levelNo, List<ObjectList> objects); 
    public delegate void            SortObjectsFunc(int levelNo, int listNo, List<ObjectRec> objects);

    public delegate LevelLayerData  GetLayoutFunc(int levelNo);
    public delegate bool SetLayoutFunc(LevelLayerData date, int levelNo);

    public delegate Dictionary<String, int> GetObjectDictionaryFunc(int listNo, int objNo);

    public delegate int  ConvertScreenTileFunc(int val);
    public delegate int  GetBigTileNoFromScreenFunc(int[] screenData, int index);
    public delegate void SetBigTileToScreenFunc(int[] screenData, int index, int value);

    public delegate byte[] LoadSegaBackFunc();
    public delegate void SaveSegaBackFunc(byte[] data);

    public delegate void DrawObjectFunc(Graphics g, ObjectRec curObject, int listNo, bool selected, float curScale, ImageList objectSprites, bool inactive, int leftMargin, int topMargin);
    public delegate void DrawObjectBigFunc(Graphics g, ObjectRec curObject, int listNo, bool selected, float curScale, Image[] objectSprites, bool inactive, int leftMargin, int topMargin);

    public delegate Screen[] LoadScreensFunc();
    public delegate void SaveScreensFunc(Screen[] screens);

    public delegate int GetPalBytesAddrFunc(int blockId);

    public class ConfigScript
    {
        static ConfigScript()
        {
            //add pathes for including scripts
            var globalSettings = CSScript.GlobalSettings;
            globalSettings.AddSearchDir("./settings_nes");
            globalSettings.AddSearchDir("./settings_smd");
            globalSettings.AddSearchDir("./settings_gba");
        }
        public static void LoadGlobalsFromFile(string fileName)
        {
            try
            {
                var asm = new AsmHelper(CSScript.Load(fileName));
                object data = asm.CreateObject("Config");
                romName = callFromScript(asm, data, "*.getFileName", "");
                cfgName = callFromScript(asm, data, "*.getConfigName", "");
                dumpName = callFromScript(asm, data, "*.getDumpName", "");
                nesColors = callFromScript<Color[]>(asm, data, "*.getNesColors", null);
            }
            catch (Exception)
            {
            }
        }

        public static void LoadStringsFromFile(string fileName)
        {
            /*try
            {
                var asm = new AsmHelper(CSScript.Load(fileName));
                object data = asm.CreateObject("Strings");
                Strings.FormMainName = callFromScript(asm, data, "*.getStrings", "");
            }
            catch (Exception)
            {
            }*/
        }

        private static void addPlugin(string pluginName)
        {
            var plugin = PluginLoader.loadPlugin<IPlugin>(pluginName);
            if (plugin != null)
                plugins.Add(plugin);
        }

        public static void loadPluginWithSilentCatch(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
            }
        }

        public static bool PreloadShowDumpField(string fileName)
        {
            try
            {
                var asm = new AsmHelper(CSScript.LoadCode(File.ReadAllText(fileName)));
                var data = asm.CreateObject("Data");
                bool showDump = callFromScript(asm, data, "*.showDumpFileField", false);
                return showDump;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void LoadFromFile(string fileName)
        {
            programStartDirectory = AppDomain.CurrentDomain.BaseDirectory + "/";
            configDirectory = Path.GetDirectoryName(fileName) + "/";

            var asm = new AsmHelper(CSScript.LoadCode(File.ReadAllText(fileName)));
            object data = null;
            bool metaDataExists = true;
            try
            {
                object metaData = null;
                try
                {
                    metaData = asm.CreateObject("MetaData");
                }
                catch (Exception)
                {
                    metaDataExists = false;
                }
                if (metaDataExists)
                {
                    var scriptText = callFromScript(asm, metaData, "*.makeConfig", "");
                    var patchDict = callFromScript(asm, metaData, "*.getPatchDictionary", new Dictionary<string, object>());
                    scriptText = Utils.patchConfigTemplate(scriptText, patchDict);
                    asm = new AsmHelper(CSScript.LoadCode(scriptText));
                    data = asm.CreateObject("Data");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            if (!metaDataExists)
            {
                try
                {
                    data = asm.CreateObject("Data");
                }
                catch (Exception)
                {
                    return;
                }
            }

            screensOffset = new OffsetRec[1];

            palOffset = callFromScript(asm, data, "*.getPalOffset", new OffsetRec(0, 1, 0));
            videoOffset = callFromScript(asm, data, "*.getVideoOffset", new OffsetRec(0, 1, 0));
            videoObjOffset = callFromScript(asm, data, "*.getVideoObjOffset", new OffsetRec(0, 1, 0));
            blocksOffset = callFromScript(asm, data, "*.getBlocksOffset", new OffsetRec(0, 1, 0));
            screensOffset[0] = callFromScript(asm, data, "*.getScreensOffset", new OffsetRec(0, 1, 0, -1, -1));
            if ((screensOffset[0].beginAddr == 0) && (screensOffset[0].recSize == 0))
            {
                screensOffset = callFromScript(asm, data, "*.getScreensOffsetsForLevels", new OffsetRec[1]);
            }
            if ((screensOffset[0].width <= 0) || (screensOffset[0].height <= 0))
            {
                throw new Exception("Screen width and height must be defined and be positive numbers");
            }
            screenVertical = callFromScript(asm, data, "*.getScreenVertical", false);
            screenDataStride = callFromScript(asm, data, "*.getScreenDataStride", 1);
            wordLen = callFromScript(asm, data, "*.getWordLen", 1);
            littleEndian = callFromScript(asm, data, "*.isLittleEndian", false);
            useSegaGraphics = callFromScript(asm, data, "*.isUseSegaGraphics", false);
            useGbGraphics = callFromScript(asm, data, "*.isUseGbGraphics", false);
            blockSize4x4 = callFromScript(asm, data, "*.isBlockSize4x4", false);
            buildScreenFromSmallBlocks = callFromScript(asm, data, "isBuildScreenFromSmallBlocks", false);
            getLevelRecsFunc = callFromScript<GetLevelRecsFunc>(asm, data, "*.getLevelRecsFunc", ConfigScript.getLevelRecsFuncDefault());

            //todo: remove or change to many lists interface
            minObjCoordX = callFromScript(asm, data, "*.getMinObjCoordX", 0);
            minObjCoordY = callFromScript(asm, data, "*.getMinObjCoordY", 0);
            minObjType   = callFromScript(asm, data, "*.getMinObjType"  , 0);
            maxObjCoordX = callFromScript(asm, data, "*.getMaxObjCoordX", -1);
            maxObjCoordY = callFromScript(asm, data, "*.getMaxObjCoordY", -1);
            maxObjType   = callFromScript(asm, data, "*.getMaxObjType"  , -1); //256

            bigBlocksHierarchyCount = callFromScript<int>(asm, data, "*.getBigBlocksHierarchyCount", 1);

            bigBlocksCounts = new int[bigBlocksHierarchyCount];
            for (int hierLevel = 0; hierLevel < bigBlocksHierarchyCount; hierLevel++)
            {
                bigBlocksCounts[hierLevel] = callFromScript(asm, data, "*.getBigBlocksCountHierarchy", 256, hierLevel);
            }
            bigBlocksCounts[0] = callFromScript(asm, data, "*.getBigBlocksCount", bigBlocksCounts[0]);
            getBigBlocksCountFunc = callFromScript<GetBigBlocksCountFunc>(asm, data, "*.getBigBlocksCountFunc");

            bigBlocksOffsets = new OffsetRec[bigBlocksHierarchyCount];
            for (int hierLevel = 0; hierLevel < bigBlocksHierarchyCount; hierLevel++)
            {
                bigBlocksOffsets[hierLevel] = callFromScript(asm, data, "*.getBigBlocksOffsetHierarchy", new OffsetRec(0, 1, 0), hierLevel);
            }
            bigBlocksOffsets[0] = callFromScript(asm, data, "*.getBigBlocksOffset", bigBlocksOffsets[0]);

            getVideoPageAddrFunc = callFromScript <GetVideoPageAddrFunc>(asm, data, "*.getVideoPageAddrFunc");
            getVideoChunkFunc = callFromScript<GetVideoChunkFunc>(asm, data, "*.getVideoChunkFunc");
            setVideoChunkFunc = callFromScript<SetVideoChunkFunc>(asm, data, "*.setVideoChunkFunc");

            getBigBlocksFuncs = new GetBigBlocksFunc[bigBlocksHierarchyCount];
            setBigBlocksFuncs = new SetBigBlocksFunc[bigBlocksHierarchyCount];
            getBigBlocksAddrFuncs = new GetBigBlocksAddrFunc[bigBlocksHierarchyCount];

            getBigBlocksFuncs = callFromScript<GetBigBlocksFunc[]>(asm, data, "*.getBigBlocksFuncs", new GetBigBlocksFunc[1]);
            setBigBlocksFuncs = callFromScript<SetBigBlocksFunc[]>(asm, data, "*.setBigBlocksFuncs", new SetBigBlocksFunc[1]);
            getBigBlocksAddrFuncs = callFromScript<GetBigBlocksAddrFunc[]>(asm, data, "*.getBigBlocksAddrFuncs", new GetBigBlocksAddrFunc[1]);
            if (!buildScreenFromSmallBlocks)
            {
                getBigBlocksFuncs[0] = callFromScript<GetBigBlocksFunc>(asm, data, "*.getBigBlocksFunc", getBigBlocksFuncs[0]);
                setBigBlocksFuncs[0] = callFromScript<SetBigBlocksFunc>(asm, data, "*.setBigBlocksFunc", setBigBlocksFuncs[0]);
                getBigBlocksAddrFuncs[0] = callFromScript<GetBigBlocksAddrFunc>(asm, data, "*.getBigBlocksAddrFunc", getBigBlocksAddrFuncs[0]);
            }

            getSegaMappingFunc = callFromScript<GetSegaMappingFunc>(asm, data, "*.getSegaMappingFunc", (int index) => Utils.readLinearBigBlockData(0, index));
            setSegaMappingFunc = callFromScript<SetSegaMappingFunc>(asm, data, "*.setSegaMappingFunc", (int index, byte[] bb) => { Utils.writeLinearBigBlockData(0, index, bb); });
            getBlocksFunc = callFromScript<GetBlocksFunc>(asm,data,"*.getBlocksFunc");
            setBlocksFunc = callFromScript<SetBlocksFunc>(asm, data, "*.setBlocksFunc");
            getBlocksAddrFunc = callFromScript<GetBlocksAddrFunc> (asm, data, "*.getBlocksAddrFunc");
            getPalFunc = callFromScript<GetPalFunc>(asm, data, "*.getPalFunc");
            setPalFunc = callFromScript<SetPalFunc>(asm, data, "*.setPalFunc");
            getObjectsFunc = callFromScript<GetObjectsFunc>(asm, data, "*.getObjectsFunc");
            setObjectsFunc = callFromScript<SetObjectsFunc>(asm, data, "*.setObjectsFunc");
            sortObjectsFunc = callFromScript<SortObjectsFunc>(asm, data, "*.sortObjectsFunc");
            getLayoutFunc = callFromScript<GetLayoutFunc>(asm, data, "*.getLayoutFunc", Utils.getDefaultLayoutFunc());
            setLayoutFunc = callFromScript<SetLayoutFunc>(asm, data, "*.setLayoutFunc", null);
            convertScreenTileFunc = callFromScript<ConvertScreenTileFunc>(asm, data, "*.getConvertScreenTileFunc");
            backConvertScreenTileFunc = callFromScript<ConvertScreenTileFunc>(asm, data, "*.getBackConvertScreenTileFunc");
            getBigTileNoFromScreenFunc = callFromScript<GetBigTileNoFromScreenFunc>(asm, data, "*.getBigTileNoFromScreenFunc", Utils.getBigTileNoFromScreen);
            setBigTileToScreenFunc = callFromScript<SetBigTileToScreenFunc>(asm, data, "*.setBigTileToScreenFunc", Utils.setBigTileToScreen);
            getObjectDictionaryFunc = callFromScript<GetObjectDictionaryFunc>(asm, data, "*.getObjectDictionaryFunc");
            loadSegaBackFunc = callFromScript<LoadSegaBackFunc>(asm, data, "*.loadSegaBackFunc");
            saveSegaBackFunc = callFromScript<SaveSegaBackFunc>(asm, data, "*.saveSegaBackFunc");
            segaBackWidth  = callFromScript(asm, data, "*.getSegaBackWidth", 64);
            segaBackHeight = callFromScript(asm, data, "*.getSegaBackHeight", 32);

            drawObjectFunc = callFromScript<DrawObjectFunc>(asm, data, "*.getDrawObjectFunc");
            drawObjectBigFunc = callFromScript<DrawObjectBigFunc>(asm, data, "*.getDrawObjectBigFunc");

            renderToMainScreenFunc = callFromScript<RenderToMainScreenFunc>(asm, data, "*.getRenderToMainScreenFunc");

            isBigBlockEditorEnabled = callFromScript(asm, data, "*.isBigBlockEditorEnabled", true);
            isBlockEditorEnabled = callFromScript(asm, data, "*.isBlockEditorEnabled", true);
            isEnemyEditorEnabled = callFromScript(asm, data, "*.isEnemyEditorEnabled", true);
            objTypesPicturesDir = callFromScript(asm, data, "*.getObjTypesPicturesDir", "obj_sprites");

            showScrollsInLayout = callFromScript(asm, data, "*.isShowScrollsInLayout", true);
            scrollsOffsetFromLayout = callFromScript(asm, data, "*.getScrollsOffsetFromLayout", 0);
            scrollByteArray = callFromScript(asm, data, "*.getScrollByteArray", new byte[0]);

            blocksCount = callFromScript(asm, data, "*.getBlocksCount"   , 256);
            getBlocksCountFunc = callFromScript<GetBlocksCountFunc>(asm, data, "*.getBlocksCountFunc");

            blocksPicturesFilename  = callFromScript(asm, data, "getBlocksFilename", "");

            loadScreensFunc = callFromScript<LoadScreensFunc>(asm, data, "*.loadScreensFunc");
            saveScreensFunc = callFromScript<SaveScreensFunc>(asm, data, "*.saveScreensFunc");
            if (blocksPicturesFilename != "")
            {
                if (!File.Exists(ConfigScript.getBlocksPicturesFilename()))
                {
                    throw new Exception("File does not exists: " + ConfigScript.getBlocksPicturesFilename());
                }
            }
            blocksPicturesWidth = callFromScript(asm, data, "getPictureBlocksWidth", 32); 
            usePicturesInstedBlocks = blocksPicturesFilename != "";

            blockTypeNames = callFromScript(asm, data, "getBlockTypeNames", defaultBlockTypeNames);

            getGroupsFunc = callFromScript<GetGroupsFunc>(asm, data, "*.getGroupsFunc", () => { return new GroupRec[0]; });

            palBytesAddr = callFromScript(asm, data, "*.getPalBytesAddr", -1);
            physicsBytesAddr = callFromScript(asm, data, "*.getPhysicsBytesAddr", -1);
            getPalBytesAddrFunc = callFromScript<GetPalBytesAddrFunc>(asm, data, "*.getPalBytesAddrFunc");

            defaultScale = callFromScript(asm, data, "*.getDefaultScale", -1.0f);

            loadAllPlugins(asm, data);

            ConfigScript.videoNes.updateColorsFromConfig();
        }

        private static GetLevelRecsFunc getLevelRecsFuncDefault()
        {
            return () => { return new List<LevelRec>() { new LevelRec(0, 0, 1, 1, 0) }; };
        }

        private static void loadAllPlugins(AsmHelper asm, object data)
        {
            cleanPlugins();
            loadGlobalPlugins();
            loadPluginsFromCurrentConfig(asm, data);
        }

        private static void cleanPlugins()
        {
            plugins.Clear();
            videoNes = null;
            videoSega = null;
            videoGb = null;
        }

        private static void loadGlobalPlugins()
        {
            //auto load plugins
            loadPluginWithSilentCatch(() => addPlugin("PluginExportScreens.dll"));
            loadPluginWithSilentCatch(() => addPlugin("PluginHexEditor.dll"));

            //auto load video plugins

            loadPluginWithSilentCatch(() => videoNes = PluginLoader.loadPlugin<IVideoPluginNes>("PluginVideoNes.dll"));
            loadPluginWithSilentCatch(() => videoSega = PluginLoader.loadPlugin<IVideoPluginSega>("PluginVideoSega.dll"));
            loadPluginWithSilentCatch(() => videoGb = PluginLoader.loadPlugin<IVideoPluginGb>("PluginVideoGb.dll"));
        }

        private static void loadPluginsFromCurrentConfig(AsmHelper asm, object data)
        {
            string[] pluginNames = callFromScript(asm, data, "getPluginNames", new string[0]);
            foreach (var pluginName in pluginNames)
            {
                var p = PluginLoader.loadPlugin<IPlugin>(pluginName);
                if (p != null)
                {
                    p.loadFromConfig(asm, data);
                    plugins.Add(p);
                }
            }
            plugins.Reverse();
        }

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

        /*public static BigBlock[] getBigBlocks(int bigBlockId)
        {
            return (getBigBlocksFuncs[0] ?? (_ => null))(bigBlockId);
        }*/

        public static BigBlock[] getBigBlocksRecursive(int hierarchyLevel, int bigBlockId)
        {
            return (getBigBlocksFuncs[hierarchyLevel] ?? (_ => null))(bigBlockId);
        }

        /*public static void setBigBlocks(int bigTileIndex, BigBlock[] bigBlockIndexes)
        {
            setBigBlocksFuncs[0](bigTileIndex, bigBlockIndexes);
        }*/

        public static void setBigBlocksHierarchy(int hierarchyLevel, int bigTileIndex, BigBlock[] bigBlockIndexes)
        {
            setBigBlocksFuncs[hierarchyLevel](bigTileIndex, bigBlockIndexes);
        }

        public static byte[] getSegaMapping(int mappingIndex)
        {
            return (getSegaMappingFunc ?? (_ => null))(mappingIndex);
        }

        public static void setSegaMapping(int mappingIndex, byte[] mapping)
        {
            setSegaMappingFunc(mappingIndex, mapping);
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

        public static List<ObjectList> getObjects(int levelNo)
        {
            return (getObjectsFunc ?? (_ => null))(levelNo);
        }

        public static void setObjects(int levelNo, List<ObjectList> objects)
        {
            setObjectsFunc(levelNo, objects);
        }
        
        public static void sortObjects(int levelNo, int listNo, List<ObjectRec> objects)
        {
            sortObjectsFunc(levelNo, listNo, objects);
        }

         public static int convertScreenTile(int tile)
         {
             return (convertScreenTileFunc ?? (v=>v))(tile);
         }

         public static int backConvertScreenTile(int tile)
         {
             return (backConvertScreenTileFunc ?? (v => v))(tile);
         }

         public static int getBigTileNoFromScreen(int[] screenData, int index)
         {
             return getBigTileNoFromScreenFunc(screenData, index);
         }

         public static void setBigTileToScreen(int[] screenData, int index, int value)
         {
             setBigTileToScreenFunc(screenData, index, value);
         }

         public static byte[] loadSegaBack()
         {
             return loadSegaBackFunc();
         }
         public static void saveSegaBack(byte[] data)
         {
             saveSegaBackFunc(data);
         }

         public static int getSegaBackWidth()
         {
             return segaBackWidth;
         }

         public static int getSegaBackHeight()
         {
             return segaBackHeight;
         }

        public static void drawObject(Graphics g, ObjectRec curObject, int listNo, bool selected, float curScale, ImageList objectSprites, bool inactive, int leftMargin, int topMargin)
         {
             if (drawObjectFunc != null)
                 drawObjectFunc(g, curObject, listNo, selected, curScale, objectSprites, inactive, leftMargin, topMargin);
             else
                 Utils.defaultDrawObject(g, curObject, listNo, selected, curScale, objectSprites, inactive, leftMargin, topMargin);
         }

         public static void drawObjectBig(Graphics g, ObjectRec curObject, int listNo, bool selected, float curScale, Image[] objectSprites, bool inactive, int leftMargin, int topMargin)
         {
             if (drawObjectBigFunc != null)
                 drawObjectBigFunc(g, curObject, listNo, selected, curScale, objectSprites, inactive, leftMargin, topMargin);
             else
                 Utils.defaultDrawObjectBig(g, curObject, listNo, selected, curScale, objectSprites, inactive, leftMargin, topMargin);   
         }

        public static Screen[] loadScreens()
        {
            if (loadScreensFunc != null)
            {
                return loadScreensFunc();
            }
            else
            {
                return Utils.loadScreensDiffSize();
            }
        }

        public static void saveScreens(Screen[] screens)
        {
            if (saveScreensFunc != null)
            {
                saveScreensFunc(screens);
            }
            else
            {
                Utils.saveScreensDiffSize(screens);
            }
        }

        public static LevelLayerData getLayout(int levelNo)
        {
            return getLayoutFunc(levelNo);
        }

        public static bool setLayout(LevelLayerData layerData, int levelNo)
        {
            return (setLayoutFunc ?? ((_,__) => true))(layerData, levelNo);
        }

        public static Dictionary<String, int> getObjectDictionary(int listNo, int objType)
        {
            return (getObjectDictionaryFunc ?? ((_,__)=> null))(listNo, objType);
        }

        public static void renderToMainScreen(Graphics g, int scale, int scrNo)
        {
            renderToMainScreenFunc?.Invoke(g, scale, scrNo);
        }

        public static int getBigBlocksCount(int hierarchyLevel, int bigBlockId)
        {
            return getBigBlocksCountFunc?.Invoke(hierarchyLevel, bigBlockId) ?? bigBlocksCounts[hierarchyLevel];
        }

        public static int getBlocksCount(int blockId)
        {
            return getBlocksCountFunc?.Invoke(blockId)?? blocksCount;
        }

        public static IList<LevelRec> getLevelRecs()
        {
            return getLevelRecsFunc();
        }

        public static LevelRec getLevelRec(int i)
        {
            return getLevelRecsFunc()[i];
        }

        public static int getMaxObjType()
        {
            return maxObjType;
        }

        public static int getMinObjType()
        {
            return minObjType;
        }

        public static string getObjTypesPicturesDir()
        {
            return ConfigScript.ConfigDirectory + objTypesPicturesDir;
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

        public static string getBlocksPicturesFilename()
        {
            return configDirectory + blocksPicturesFilename;
        }

        public static bool isShowScrollsInLayout()
        {
            return showScrollsInLayout;
        }

        public static int getScrollsOffsetFromLayout()
        {
            return scrollsOffsetFromLayout;
        }

        public static bool isUseSegaGraphics()
        {
            return useSegaGraphics;
        }

        public static bool isUseGbGraphics()
        {
            return useGbGraphics;
        }

        public static bool isBlockSize4x4()
        {
            return blockSize4x4;
        }

        public static GroupRec[] getGroups()
        {
            return getGroupsFunc();
        }

        public static GroupRec getGroup(int i)
        {
            return getGroups()[i];
        }

        public static int getPalBytesAddr(int blockId)
        {
            return getPalBytesAddrFunc?.Invoke(blockId) ?? palBytesAddr;
        }

        public static int getPhysicsBytesAddr()
        {
            return physicsBytesAddr;
        }

        public static float getDefaultScale()
        {
            return defaultScale;
        }

        //------------------------------------------------------------

        public static int getLayoutAddr(int index)
        {
            return ConfigScript.getLevelRec(index).layoutAddr;
        }

        public static int getScrollAddr(int index)
        {
            return getLayoutAddr(index) + ConfigScript.getScrollsOffsetFromLayout();
        }

        public static int getTilesAddr(int id)
        {
            var getAddrFunc = ConfigScript.getBlocksAddrFunc;
            return getAddrFunc?.Invoke(id) ?? getTilesAddrDefault(id);

        }

        private static int getTilesAddrDefault(int id)
        {
            return ConfigScript.blocksOffset.beginAddr + ConfigScript.blocksOffset.recSize * id;
        }

        public static int getBigTilesAddr(int hierarchyLevel, int id)
        {
            GetBigBlocksAddrFunc getAddrFunc = null;
            if (hierarchyLevel < ConfigScript.getBigBlocksAddrFuncs.Length)
            {
                getAddrFunc = ConfigScript.getBigBlocksAddrFuncs[hierarchyLevel];
            }
            return getAddrFunc?.Invoke(id) ?? getBigTilesAddrDefault(hierarchyLevel, id);
        }

        private static int getBigTilesAddrDefault(int hierarchyLevel, int id)
        {
            var bigBlocksOffset = ConfigScript.bigBlocksOffsets[hierarchyLevel];
            return bigBlocksOffset.beginAddr + bigBlocksOffset.recSize * id;
        }

        public static int getLevelWidth(int levelNo)
        {
            return ConfigScript.getLevelRec(levelNo).width;
        }

        public static int getLevelHeight(int levelNo)
        {
            return ConfigScript.getLevelRec(levelNo).height;
        }

        public static int getbigBlocksHierarchyCount()
        {
            return bigBlocksHierarchyCount;
        }

        public static byte[] getScrollByteArray()
        {
            return scrollByteArray;
        }
        //------------------------------------------------------------

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

        private static string programStartDirectory;
        private static string configDirectory;

        public static string ProgramDirectory { get { return programStartDirectory; } }
        public static string ConfigDirectory  { get { return configDirectory; } }

        public static OffsetRec palOffset;
        public static OffsetRec videoOffset;
        public static OffsetRec videoObjOffset;
        public static OffsetRec[] bigBlocksOffsets;
        public static OffsetRec blocksOffset;
        public static OffsetRec[] screensOffset;
        //public static OffsetRec boxesBackOffset;
        public static int levelsCount;
        public static bool screenVertical;
        public static int screenDataStride;
        public static int wordLen;
        public static bool littleEndian;
        public static bool buildScreenFromSmallBlocks;

        public static bool useSegaGraphics;
        public static bool useGbGraphics;
        public static bool blockSize4x4;

        public static int minObjCoordX;
        public static int minObjCoordY;
        public static int minObjType;
        public static int maxObjCoordX;
        public static int maxObjCoordY;
        public static int maxObjType;

        public static int segaBackWidth;
        public static int segaBackHeight;

        //public static IList<LevelRec> levelRecs;
        public static GetLevelRecsFunc getLevelRecsFunc;

        public static GetVideoPageAddrFunc getVideoPageAddrFunc;
        public static GetVideoChunkFunc getVideoChunkFunc;
        public static SetVideoChunkFunc setVideoChunkFunc;

        public static int bigBlocksHierarchyCount;
        public static int[] bigBlocksCounts;
        public static GetBigBlocksFunc[] getBigBlocksFuncs;
        public static SetBigBlocksFunc[] setBigBlocksFuncs;
        public static GetBigBlocksAddrFunc[] getBigBlocksAddrFuncs;
        public static GetBigBlocksCountFunc getBigBlocksCountFunc;

        public static GetSegaMappingFunc getSegaMappingFunc;
        public static SetSegaMappingFunc setSegaMappingFunc;

        public static int blocksCount;
        public static GetBlocksFunc getBlocksFunc;
        public static SetBlocksFunc setBlocksFunc;
        public static GetBlocksAddrFunc getBlocksAddrFunc;
        public static GetBlocksCountFunc getBlocksCountFunc;

        public static GetPalFunc getPalFunc;
        public static SetPalFunc setPalFunc;

        public static GetObjectsFunc getObjectsFunc;
        public static SetObjectsFunc setObjectsFunc;
        public static SortObjectsFunc sortObjectsFunc;

        public static GetLayoutFunc getLayoutFunc;
        public static SetLayoutFunc setLayoutFunc;
        public static GetObjectDictionaryFunc getObjectDictionaryFunc;
        public static RenderToMainScreenFunc renderToMainScreenFunc;

        public static ConvertScreenTileFunc convertScreenTileFunc;
        public static ConvertScreenTileFunc backConvertScreenTileFunc;

        public static GetBigTileNoFromScreenFunc getBigTileNoFromScreenFunc;
        public static SetBigTileToScreenFunc setBigTileToScreenFunc;

        public static LoadSegaBackFunc loadSegaBackFunc;
        public static SaveSegaBackFunc saveSegaBackFunc;

        public static DrawObjectFunc drawObjectFunc;
        public static DrawObjectBigFunc drawObjectBigFunc;

        public static LoadScreensFunc loadScreensFunc;
        public static SaveScreensFunc saveScreensFunc;

        public static float defaultScale;

        public static bool isBigBlockEditorEnabled;
        public static bool isBlockEditorEnabled;
        public static bool isEnemyEditorEnabled;

        public static bool showScrollsInLayout;
        public static int scrollsOffsetFromLayout;
        public static byte[] scrollByteArray;

        public static bool usePicturesInstedBlocks;
        public static int blocksPicturesWidth;
        public static string objTypesPicturesDir;
        private static string blocksPicturesFilename;

        public static GetGroupsFunc getGroupsFunc;

        public static int palBytesAddr;
        public static int physicsBytesAddr;
        public static GetPalBytesAddrFunc getPalBytesAddrFunc;

        public static string[] blockTypeNames;
        public static string[] defaultBlockTypeNames = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };

        //global editor settings
        public static string  romName;
        public static string dumpName;
        public static string  cfgName;
        public static Color[] nesColors;

        public static List<IPlugin> plugins = new List<IPlugin>();
        public static IVideoPluginNes videoNes;
        public static IVideoPluginSega videoSega;
        public static IVideoPluginGb videoGb;
    }
}
