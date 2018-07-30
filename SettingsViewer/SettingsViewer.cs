using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CSScriptLibrary;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SettingsViewer
{
    public partial class SettingsViewer : Form
    {
        public SettingsViewer()
        {
            InitializeComponent();
        }

        private void tbConfigName_Click(object sender, EventArgs e)
        {
            if (ofConfig.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    tbConfigName.Text = ofConfig.FileName;
                    pgConfig.SelectedObject = new ConfigGrid(tbConfigName.Text);
                    ConfigViewer.Properties.Settings.Default.ConfigName = tbConfigName.Text;
                    ConfigViewer.Properties.Settings.Default.Save();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Invalid CadEditor config file. Error message: {0}", ex.Message), "Open error");
                    pgConfig.SelectedObject = null;
                }
            }
        }

        private void btReload_Click(object sender, EventArgs e)
        {
            try
            {
                pgConfig.SelectedObject = new ConfigGrid(tbConfigName.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Invalid CadEditor config file. Error message: {0}", ex.Message), "Open error");
                pgConfig.SelectedObject = null;
            }
        }

        private void btExecute_Click(object sender, EventArgs e)
        {
            try
            {
                var item = pgConfig.SelectedGridItem;
                var methodDelegate = (MethodDelegate)item.Value;
                if (methodDelegate == null)
                {
                    MessageBox.Show("null");
                }
                else
                {
                    var result = methodDelegate.Invoke();
                    MessageBox.Show((result ?? "null").ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Can't execute selected method. Error message: {0}", ex.Message), "Execute error");
            }
        }

        private void ConfigViewer_Load(object sender, EventArgs e)
        {
            tbConfigName.Text = ConfigViewer.Properties.Settings.Default.ConfigName;
        }
    }

    class ConfigGrid
    {
        public ConfigGrid(string filename)
        {
            asm = new AsmHelper(CSScript.Load(filename));
            data = asm.CreateObject("Data");
        }
        AsmHelper asm;
        object data;

        public MethodDelegate getMethod(string funcName /*, params object[] funcParams*/)
        {
            try
            {
                return asm.GetMethod(data, String.Format("*.{0}", funcName));
            }
            catch (Exception) //all exception catch...
            {
                return null;
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string getCurrentMethodNameWithoutGetter(int stacktraceLevel)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(stacktraceLevel); 

            var fullName = sf.GetMethod().Name; //return "get_METHODNAME"
            return fullName.Replace("get_", "");
        }

        public MethodDelegate get() { return getMethod(getCurrentMethodNameWithoutGetter(2));} //just shortcut

        [Category("Show dump")]
        public MethodDelegate showDumpFileField => get();
        [Category("Levels count")]
        public MethodDelegate getLevelsCount => get();
        [Category("Offsets")]
        public MethodDelegate getPalOffset => get();
        [Category("Offsets")]
        public MethodDelegate getVideoOffset => get();
        [Category("Offsets")]
        public MethodDelegate getVideoObjOffset => get();
        [Category("Offsets")]
        public MethodDelegate getBlocksOffset => get();
        [Category("Offsets")]
        public MethodDelegate getScreensOffset => get();
        [Category("Screen params")]
        public MethodDelegate loadScreensFunc => get();
        [Category("Screen params")]
        public MethodDelegate saveScreensFunc => get();
        [Category("Screen params")]
        public MethodDelegate getScreensOffsetsForLevels => get();
        [Category("Screen params")]
        public MethodDelegate getScreenVertical => get();
        [Category("Screen params")]
        public MethodDelegate getScreenDataStride => get();
        [Category("Screen params")]
        public MethodDelegate getWordLen => get();
        [Category("Screen params")]
        public MethodDelegate isLittleEndian => get();
        [Category("GameBoy graphics")]
        public MethodDelegate isUsGbGraphics => get();
        [Category("Sega graphics")]
        public MethodDelegate isUseSegaGraphics => get();
        [Category("Sega graphics")]
        public MethodDelegate isBlockSize4x4 => get();
        [Category("Screen params")]
        public MethodDelegate isBuildScreenFromSmallBlocks => get();
        [Category("Level recs")]
        public MethodDelegate getLevelRecsFunc => get();
        [Category("Min/max object params")]
        public MethodDelegate getMinObjCoordX => get();
        [Category("Min/max object params")]
        public MethodDelegate getMinObjCoordY => get();
        [Category("Min/max object params")]
        public MethodDelegate getMinObjType => get();
        [Category("Min/max object params")]
        public MethodDelegate getMaxObjCoordX => get();
        [Category("Min/max object params")]
        public MethodDelegate getMaxObjCoordY => get();
        [Category("Min/max object params")]
        public MethodDelegate getMaxObjType => get();
        [Category("Blocks params")]
        public MethodDelegate getBigBlocksHierarchyCount => get();
        [Category("Blocks params")]
        public MethodDelegate getBigBlocksCountHierarchy => get();
        [Category("Blocks params")]
        public MethodDelegate getBigBlocksCount => get();
        [Category("Blocks params")]
        public MethodDelegate getBigBlocksOffsetHierarchy => get();
        [Category("Offsets")]
        public MethodDelegate getBigBlocksOffset => get();
        [Category("NES CHR funcs")]
        public MethodDelegate getVideoPageAddrFunc => get();
        [Category("NES CHR funcs")]
        public MethodDelegate getVideoChunkFunc => get();
        [Category("NES CHR funcs")]
        public MethodDelegate setVideoChunkFunc => get();
        [Category("BigBlocks funcs")]
        public MethodDelegate getBigBlocksFuncs => get();
        [Category("BigBlocks funcs")]
        public MethodDelegate setBigBlocksFuncs => get();
        [Category("BigBlocks funcs")]
        public MethodDelegate getBigBlocksFunc => get();
        [Category("BigBlocks funcs")]
        public MethodDelegate setBigBlocksFunc => get();
        [Category("Sega blocks funcs")]
        public MethodDelegate getSegaMappingFunc => get();
        [Category("Sega blocks funcs")]
        public MethodDelegate setSegaMappingFunc => get();
        [Category("Blocks funcs")]
        public MethodDelegate getBlocksFunc => get();
        [Category("Blocks funcs")]
        public MethodDelegate setBlocksFunc => get();
        [Category("Pal funcs")]
        public MethodDelegate getPalFunc => get();
        [Category("Pal funcs")]
        public MethodDelegate setPalFunc => get();
        [Category("Objects funcs")]
        public MethodDelegate getObjectsFunc => get();
        [Category("Objects funcs")]
        public MethodDelegate setObjectsFunc => get();
        [Category("Objects funcs")]
        public MethodDelegate sortObjectsFunc => get();
        [Category("Layout funcs")]
        public MethodDelegate getLayoutFunc => get();
        [Category("Layout funcs")]
        public MethodDelegate setLayoutFunc => get();
        [Category("Converst screen to tile funcs")]
        public MethodDelegate getConvertScreenTileFunc => get();
        [Category("Converst screen to tile funcs")]
        public MethodDelegate getBackConvertScreenTileFunc => get();
        [Category("Converst screen to tile funcs")]
        public MethodDelegate getBigTileNoFromScreenFunc => get();
        [Category("Converst screen to tile funcs")]
        public MethodDelegate setBigTileToScreenFunc => get();
        [Category("Objects funcs")]
        public MethodDelegate getObjectDictionaryFunc => get();
        [Category("Sega back funcs")]
        public MethodDelegate loadSegaBackFunc => get();
        [Category("Sega back funcs")]
        public MethodDelegate saveSegaBackFunc => get();
        [Category("Sega back params")]
        public MethodDelegate getSegaBackWidth => get();
        [Category("Sega back params")]
        public MethodDelegate getSegaBackHeight => get();
        [Category("Render objects params")]
        public MethodDelegate getDrawObjectFunc => get();
        [Category("Render objects params")]
        public MethodDelegate getDrawObjectBigFunc => get();
        [Category("Render objects params")]
        public MethodDelegate getRenderToMainScreenFunc => get();
        [Category("Editors enable params")]
        public MethodDelegate isBigBlockEditorEnabled => get();
        [Category("Editors enable params")]
        public MethodDelegate isBlockEditorEnabled => get();
        [Category("Editors enable params")]
        public MethodDelegate isEnemyEditorEnabled => get();
        [Category("Icons hints")]
        public MethodDelegate getObjTypesPicturesDir => get();
        [Category("Scrolls params")]
        public MethodDelegate isShowScrollsInLayout => get();
        [Category("Scrolls params")]
        public MethodDelegate getScrollsOffsetFromLayout => get();
        [Category("Scrolls params")]
        public MethodDelegate getScrollByteArray => get();
        [Category("Blocks params")]
        public MethodDelegate getBlocksCount => get();
        [Category("Blocks pictures params")]
        public MethodDelegate getBlocksFilename => get();
        [Category("Blocks pictures params")]
        public MethodDelegate getPictureBlocksWidth => get();
        [Category("Blocks params")]
        public MethodDelegate getBlockTypeNames => get();
        [Category("View params")]
        public MethodDelegate getGroupsFunc => get();
        [Category("View params")]
        public MethodDelegate getDefaultScale => get();
        [Category("Blocks params")]
        public MethodDelegate getPalBytesAddr => get();
        [Category("Blocks params")]
        public MethodDelegate getPhysicsBytesAddr => get();
    }
}
