using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Windows.Forms;
using System.Drawing;
using CadEditor;
using CSScriptLibrary;


namespace BtRaceEditor
{
    class PluginBtRaceEditor : IPlugin
    {
        public string getName()
        {
            return "Battletoads Race Editor";
        }

        public void addSubeditorButton(FormMain formMain)
        {
            this.formMain = formMain;
            var rm = new ResourceManager("BtRaceEditor.Icon", this.GetType().Assembly);
            var iconAnim = (Bitmap)rm.GetObject("icon_btrace");
            var item = new ToolStripButton("Battletoads Race Editor", iconAnim, btAnim_Click)
            {
                DisplayStyle = ToolStripItemDisplayStyle.Image
            };
            formMain.addSubeditorButton(item);
        }

        public void addToolButton(FormMain formMain)
        {
        }

        public void loadFromConfig(object asmObj, object data)
        {
            AsmHelper asm = (AsmHelper)asmObj;
            BtConfig.objCount = (int)asm.InvokeInst(data, "*.getRaceObjectsCount");
            BtConfig.startAddress = (int)asm.InvokeInst(data, "*.getRaceObjectAddr");
        }

        private void btAnim_Click(object sender, EventArgs e)
        {
            var f = new FormHexTableEditor();
            formMain.subeditorOpen(f, (ToolStripButton)sender, false);
        }

        FormMain formMain;
    }

    public static class BtConfig
    {
        public static int startAddress;
        public static int objCount;
    }
}
