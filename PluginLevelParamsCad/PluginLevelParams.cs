using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using CadEditor;
using CSScriptLibrary;

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

        public void loadFromConfig(object asmObj, object data)
        {
            AsmHelper asm = (AsmHelper)asmObj;
            GlobalsCad.boxesBackOffset = (OffsetRec)asm.InvokeInst(data, "*.getBoxesBackOffset");
            GlobalsCad.LevelRecBaseOffset = (int)asm.InvokeInst(data, "*.getLevelRecBaseOffset");
            GlobalsCad.LevelRecDirOffset = (int)asm.InvokeInst(data, "*.getLevelRecDirOffset");
            GlobalsCad.LayoutPtrAdd = (int)asm.InvokeInst(data, "*.getLayoutPtrAdd");
            GlobalsCad.ScrollPtrAdd = (int)asm.InvokeInst(data, "*.getScrollPtrAdd");
            GlobalsCad.DirPtrAdd = (int)asm.InvokeInst(data, "*.getDirPtrAdd");
            GlobalsCad.DoorRecBaseOffset = (int)asm.InvokeInst(data, "*.getDoorRecBaseOffset");
        }

        private void btLevelParams_Click(object sender, EventArgs e)
        {
            var f = new EditLevelData();
            formMain.subeditorOpen(f, (ToolStripButton)sender, true);
        }

        FormMain formMain;
    }
}
