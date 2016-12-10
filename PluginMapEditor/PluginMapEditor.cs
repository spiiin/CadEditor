using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using CadEditor;
using CSScriptLibrary;

namespace PluginMapEditor
{
    public delegate int SaveMapFunc(byte[] mapData, out byte[] packedData);
    public delegate byte[] LoadMapFunc(int romAddr);

    public class PluginMapEditor : IPlugin
    {
        public string getName()
        {
            return "Map Editor (Darkwing Duck version)";
        }
        public void addSubeditorButton(FormMain formMain)
        {
            this.formMain = formMain;
            var rm = new ResourceManager("PluginMapEditor.Icon", this.GetType().Assembly);
            var iconMap = (System.Drawing.Bitmap)rm.GetObject("icon_map");
            var item = new ToolStripButton("Map Editor", iconMap, btMap_Click);
            item.DisplayStyle = ToolStripItemDisplayStyle.Image;
            formMain.addSubeditorButton(item);
        }

        public void addToolButton(FormMain formMain)
        {
        }

        private void btMap_Click(object sender, EventArgs e)
        {
            var f = new EditMap();
            formMain.subeditorOpen(f, (ToolStripButton)sender);
        }

        public void loadFromConfig(object asmObj, object data)
        {
            AsmHelper asm = (AsmHelper)asmObj;
            MapConfig.mapsInfo = (MapInfo[])asm.InvokeInst(data, "*.getMapsInfo");
            MapConfig.loadMapFunc = (LoadMapFunc)asm.InvokeInst(data, "*.getLoadMapFunc");
            MapConfig.saveMapFunc = (SaveMapFunc)asm.InvokeInst(data, "*.getSaveMapFunc");
        }

        FormMain formMain;
    }

    public struct MapInfo
    {
        public int dataAddr;
        public int palAddr;
        public int videoNo;
    }

    public static class MapConfig
    {
        public static MapInfo[] mapsInfo;
        public static LoadMapFunc loadMapFunc;
        public static SaveMapFunc saveMapFunc;

        public static byte[] loadMap(int romAddr)
        {
            return loadMapFunc(romAddr);
        }

        public static int saveMap(byte[] mapData, out byte[] packedData)
        {
            return saveMapFunc(mapData, out packedData);
        }
    }
}
