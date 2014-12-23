using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using CadEditor;

namespace PluginMapEditor
{
    public class PluginMapEditor : IPlugin
    {
        public void addSubeditorButton(FormMain formMain)
        {
            this.formMain = formMain;
            var rm = new ResourceManager("PluginMapEditor.Icon", this.GetType().Assembly);
            var iconMap = (System.Drawing.Bitmap)rm.GetObject("icon_map");
            var item = new ToolStripButton("Map Editor", iconMap, btMap_Click);
            item.DisplayStyle = ToolStripItemDisplayStyle.Image;
            formMain.addSubeditorButton(item);
        }

        private void btMap_Click(object sender, EventArgs e)
        {
            var f = new EditMap();
            formMain.subeditorOpen(f, (ToolStripButton)sender, true);
        }

        FormMain formMain;
    }
}
