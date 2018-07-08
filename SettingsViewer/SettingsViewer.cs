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
        public string GetCurrentMethodNameWithoutGetter(int stacktraceLevel)
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(stacktraceLevel); 

            var fullName = sf.GetMethod().Name; //return "get_METHODNAME"
            return fullName.Replace("get_", "");
        }

        public MethodDelegate get() { return getMethod(GetCurrentMethodNameWithoutGetter(2));} //just shortcut

        [Category("Show dump")]
        public MethodDelegate showDumpFileField { get { return get(); } }
        [Category("Levels count")]
        public MethodDelegate getLevelsCount { get { return get(); } }
        [Category("Offsets")]
        public MethodDelegate getPalOffset { get { return get(); } }
        [Category("Offsets")]
        public MethodDelegate getVideoOffset { get { return get(); } }
        [Category("Offsets")]
        public MethodDelegate getVideoObjOffset { get { return get(); } }
        [Category("Offsets")]
        public MethodDelegate getBlocksOffset { get { return get(); } }
        [Category("Offsets")]
        public MethodDelegate getScreensOffset { get { return get(); } }
        [Category("Screen params")]
        public MethodDelegate loadScreensFunc { get { return get(); } }
        [Category("Screen params")]
        public MethodDelegate saveScreensFunc { get { return get(); } }
        [Category("Screen params")]
        public MethodDelegate getScreenWidth { get { return get(); } }
        [Category("Screen params")]
        public MethodDelegate getScreenHeight { get { return get(); } }
        [Category("Screen params")]
        public MethodDelegate getScreensOffsetsForLevels { get { return get(); } }
        [Category("Screen params")]
        public MethodDelegate getScreenVertical { get { return get(); } }
        [Category("Screen params")]
        public MethodDelegate getScreenDataStride { get { return get(); } }
        [Category("Screen params")]
        public MethodDelegate getWordLen { get { return get(); } }
        [Category("Screen params")]
        public MethodDelegate isLittleEndian { get { return get(); } }
        [Category("Sega graphics")]
        public MethodDelegate isUseSegaGraphics { get { return get(); } }
        [Category("Sega graphics")]
        public MethodDelegate isBlockSize4x4 { get { return get(); } }
        [Category("Screen params")]
        public MethodDelegate isBuildScreenFromSmallBlocks { get { return get(); } }
        [Category("Level recs")]
        public MethodDelegate getLevelRecsFunc { get { return get(); } }
        [Category("Min/max object params")]
        public MethodDelegate getMinObjCoordX { get { return get(); } }
        [Category("Min/max object params")]
        public MethodDelegate getMinObjCoordY { get { return get(); } }
        [Category("Min/max object params")]
        public MethodDelegate getMinObjType { get { return get(); } }
        [Category("Min/max object params")]
        public MethodDelegate getMaxObjCoordX { get { return get(); } }
        [Category("Min/max object params")]
        public MethodDelegate getMaxObjCoordY { get { return get(); } }
        [Category("Min/max object params")]
        public MethodDelegate getMaxObjType { get { return get(); } }
        [Category("Blocks params")]
        public MethodDelegate getBigBlocksHierarchyCount { get { return get(); } }
        [Category("Blocks params")]
        public MethodDelegate getBigBlocksCountHierarchy { get { return get(); } }
        [Category("Blocks params")]
        public MethodDelegate getBigBlocksCount { get { return get(); } }
        [Category("Blocks params")]
        public MethodDelegate getBigBlocksOffsetHierarchy { get { return get(); } }
        [Category("Offsets")]
        public MethodDelegate getBigBlocksOffset { get { return get(); } }
        [Category("NES CHR funcs")]
        public MethodDelegate getVideoPageAddrFunc { get { return get(); } }
        [Category("NES CHR funcs")]
        public MethodDelegate getVideoChunkFunc { get { return get(); } }
        [Category("NES CHR funcs")]
        public MethodDelegate setVideoChunkFunc { get { return get(); } }
        [Category("BigBlocks funcs")]
        public MethodDelegate getBigBlocksFuncs { get { return get(); } }
        [Category("BigBlocks funcs")]
        public MethodDelegate setBigBlocksFuncs { get { return get(); } }
        [Category("BigBlocks funcs")]
        public MethodDelegate getBigBlocksFunc { get { return get(); } }
        [Category("BigBlocks funcs")]
        public MethodDelegate setBigBlocksFunc { get { return get(); } }
        [Category("Sega blocks funcs")]
        public MethodDelegate getSegaMappingFunc { get { return get(); } }
        [Category("Sega blocks funcs")]
        public MethodDelegate setSegaMappingFunc { get { return get(); } }
        [Category("Blocks funcs")]
        public MethodDelegate getBlocksFunc { get { return get(); } }
        [Category("Blocks funcs")]
        public MethodDelegate setBlocksFunc { get { return get(); } }
        [Category("Pal funcs")]
        public MethodDelegate getPalFunc { get { return get(); } }
        [Category("Pal funcs")]
        public MethodDelegate setPalFunc { get { return get(); } }
        [Category("Objects funcs")]
        public MethodDelegate getObjectsFunc { get { return get(); } }
        [Category("Objects funcs")]
        public MethodDelegate setObjectsFunc { get { return get(); } }
        [Category("Objects funcs")]
        public MethodDelegate sortObjectsFunc { get { return get(); } }
        [Category("Layout funcs")]
        public MethodDelegate getLayoutFunc { get { return get(); } }
        [Category("Layout funcs")]
        public MethodDelegate setLayoutFunc { get { return get(); } }
        [Category("Converst screen to tile funcs")]
        public MethodDelegate getConvertScreenTileFunc { get { return get(); } }
        [Category("Converst screen to tile funcs")]
        public MethodDelegate getBackConvertScreenTileFunc { get { return get(); } }
        [Category("Converst screen to tile funcs")]
        public MethodDelegate getBigTileNoFromScreenFunc { get { return get(); } }
        [Category("Converst screen to tile funcs")]
        public MethodDelegate setBigTileToScreenFunc { get { return get(); } }
        [Category("Objects funcs")]
        public MethodDelegate getObjectDictionaryFunc { get { return get(); } }
        [Category("Sega back funcs")]
        public MethodDelegate loadSegaBackFunc { get { return get(); } }
        [Category("Sega back funcs")]
        public MethodDelegate saveSegaBackFunc { get { return get(); } }
        [Category("Sega back params")]
        public MethodDelegate getSegaBackWidth { get { return get(); } }
        [Category("Sega back params")]
        public MethodDelegate getSegaBackHeight { get { return get(); } }
        [Category("Render objects params")]
        public MethodDelegate getDrawObjectFunc { get { return get(); } }
        [Category("Render objects params")]
        public MethodDelegate getDrawObjectBigFunc { get { return get(); } }
        [Category("Render objects params")]
        public MethodDelegate getRenderToMainScreenFunc { get { return get(); } }
        [Category("Editors enable params")]
        public MethodDelegate isBigBlockEditorEnabled { get { return get(); } }
        [Category("Editors enable params")]
        public MethodDelegate isBlockEditorEnabled { get { return get(); } }
        [Category("Editors enable params")]
        public MethodDelegate isEnemyEditorEnabled { get { return get(); } }
        [Category("Icons hints")]
        public MethodDelegate getObjTypesPicturesDir { get { return get(); } }
        [Category("Scrolls params")]
        public MethodDelegate isShowScrollsInLayout { get { return get(); } }
        [Category("Scrolls params")]
        public MethodDelegate getScrollsOffsetFromLayout { get { return get(); } }
        [Category("Scrolls params")]
        public MethodDelegate getScrollByteArray { get { return get(); } }
        [Category("Blocks params")]
        public MethodDelegate getBlocksCount { get { return get(); } }
        [Category("Blocks pictures params")]
        public MethodDelegate getBlocksFilename { get { return get(); } }
        [Category("Blocks pictures params")]
        public MethodDelegate getPictureBlocksWidth { get { return get(); } }
        [Category("Blocks params")]
        public MethodDelegate getBlockTypeNames { get { return get(); } }
        [Category("View params")]
        public MethodDelegate getGroupsFunc { get { return get(); } }
        [Category("View params")]
        public MethodDelegate getDefaultScale { get { return get(); } }
        [Category("Blocks params")]
        public MethodDelegate getPalBytesAddr { get { return get(); } }
        [Category("Blocks params")]
        public MethodDelegate getPhysicsBytesAddr { get { return get(); } }
    }
}
