using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using CadEditor;

namespace PluginLevelParamsCad
{
    public class PluginLevelParams : IPlugin
    {
        public string getName()
        {
            return "Chip and Dale Level Parameters Editor";
        }
        public void addSubeditorButton(FormMain formMain)
        {
            this.formMain = formMain;
            //var rm = new ResourceManager("PluginMapEditor.Icon", this.GetType().Assembly);
            //var iconMap = (System.Drawing.Bitmap)rm.GetObject("icon_map");
            var item = new ToolStripButton("Level params", null, btLevelParams_Click);
            item.DisplayStyle = ToolStripItemDisplayStyle.Image;
            formMain.addSubeditorButton(item);
        }

        private void btLevelParams_Click(object sender, EventArgs e)
        {
            var f = new EditLevelData();
            formMain.subeditorOpen(f, (ToolStripButton)sender, true);
        }

        FormMain formMain;
    }
}
