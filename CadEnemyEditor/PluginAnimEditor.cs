using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Windows.Forms;
using System.Drawing;
using CadEditor;
using CadEnemyEditor;

namespace PluginAnimEditor
{
    class PluginAnimEditor : IPlugin
    {
        public string getName()
        {
            return "Anim Editor (Chip & Dale / Darkwing Duck)";
        }
        public void addSubeditorButton(FormMain formMain)
        {
            this.formMain = formMain;
            /*var rm = new ResourceManager("PluginHexEditor.Icon", this.GetType().Assembly);
            var iconHex = (System.Drawing.Bitmap)rm.GetObject("icon_hex");*/
            var item = new ToolStripButton("Anim Editor", null, btAnim_Click);
            item.DisplayStyle = ToolStripItemDisplayStyle.Image;
            formMain.addSubeditorButton(item);
        }

        public void addToolButton(FormMain formMain)
        {
        }

        public void loadFromConfig(object asm, object data)
        {
        }

        private void btAnim_Click(object sender, EventArgs e)
        {
            var f = new AnimEditor();
            formMain.subeditorOpen(f, (ToolStripButton)sender, true);
        }

        FormMain formMain;
    }
}
