using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Windows.Forms;
using System.Drawing;
using CadEditor;
using CadEnemyEditor;
using CSScriptLibrary;

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
            var rm = new ResourceManager("PluginAnimEditor.Icon", this.GetType().Assembly);
            var iconAnim = (System.Drawing.Bitmap)rm.GetObject("icon_anim");
            var item = new ToolStripButton("Anim Editor", iconAnim, btAnim_Click);
            item.DisplayStyle = ToolStripItemDisplayStyle.Image;
            formMain.addSubeditorButton(item);
        }

        public void addToolButton(FormMain formMain)
        {
        }

        public void loadFromConfig(object asmObj, object data)
        {
            AsmHelper asm = (AsmHelper)asmObj;
            AnimConfig.ANIM_COUNT = (int)asm.InvokeInst(data, "*.getAnimCount");
            AnimConfig.animAddrHi = (int)asm.InvokeInst(data, "*.getAnimAddrHi");
            AnimConfig.animAddrLo = (int)asm.InvokeInst(data, "*.getAnimAddrLo");
            AnimConfig.FRAME_COUNT = (int)asm.InvokeInst(data, "*.getFrameCount");
            AnimConfig.frameAddrHi = (int)asm.InvokeInst(data, "*.getFrameAddrHi");
            AnimConfig.frameAddrLo = (int)asm.InvokeInst(data, "*.getFrameAddrLo");
            AnimConfig.COORD_COUNT = (int)asm.InvokeInst(data, "*.getCoordCount");
            AnimConfig.coordAddrHi = (int)asm.InvokeInst(data, "*.getCoordAddrHi");
            AnimConfig.coordAddrLo = (int)asm.InvokeInst(data, "*.getCoordAddrLo");
            AnimConfig.pal = (byte[])asm.InvokeInst(data, "*.getAnimPal");
            AnimConfig.animBankNo = (int)asm.InvokeInst(data, "*.getAnimBankNo");
        }

        private void btAnim_Click(object sender, EventArgs e)
        {
            var f = new AnimEditor();
            formMain.subeditorOpen(f, (ToolStripButton)sender, true);
        }

        FormMain formMain;
    }

    public static class AnimConfig
    {
        public static int ANIM_COUNT;
        public static int animAddrHi;
        public static int animAddrLo;

        public static int FRAME_COUNT;
        public static int frameAddrHi;
        public static int frameAddrLo;

        public static int COORD_COUNT;
        public static int coordAddrHi;
        public static int coordAddrLo;

        public static byte[] pal;

        public static int animBankNo;
    }
}
