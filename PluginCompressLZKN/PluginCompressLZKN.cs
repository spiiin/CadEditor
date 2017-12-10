using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Resources;
using CadEditor;
using CSScriptLibrary;

namespace PluginCompressLZKN
{
    class PluginCompressLZKN : IPlugin
    {
        public string getName()
        {
            return "Compress manager for KONAMI games uses LZKN compression";
        }
        public void addSubeditorButton(FormMain formMain)
        {
            this.formMain = formMain;
            var rm = new ResourceManager("PluginCompressLZKN.Icon", this.GetType().Assembly);
            var iconAnim = (System.Drawing.Bitmap)rm.GetObject("icon_compress");
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
            CompressConfig.compressParams = (CompressParams[]) asm.InvokeInst(data, "*.getCompressParams");
        }

        private void btAnim_Click(object sender, EventArgs e)
        {
            var f = new CompressManager();
            formMain.subeditorOpen(f, (ToolStripButton)sender, true);
        }

        FormMain formMain;
    }

    public class CompressParams
    {
        public int address;
        public int maxSize;
    }

    public static class CompressConfig
    {
        public static CompressParams[] compressParams;
    }
}
