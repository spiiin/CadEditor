using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;

using CadEditor;

namespace PluginHexEditor
{
    public class PluginHexEditor : IPlugin
    {
        public string getName()
        {
            return "Hex Editor";
        }
      public void addSubeditorButton(FormMain formMain)
      {
        this.formMain = formMain;
        var rm = new ResourceManager("PluginHexEditor.Icon", this.GetType().Assembly);
        var iconHex = (System.Drawing.Bitmap)rm.GetObject("icon_hex");
        var item = new ToolStripButton("Hex Editor", iconHex, btHex_Click);
        item.DisplayStyle = ToolStripItemDisplayStyle.Image;
        formMain.addSubeditorButton(item);
      }

      public void loadFromConfig(object asm, object data)
      {
      }

      private void btHex_Click(object sender, EventArgs e)
      {
          var f = new EditHexEditor();
          var so = ConfigScript.screensOffset[formMain.LevelNoForScreens];
          f.setHighlightZone(so.beginAddr + so.recSize * formMain.ScreenNo, so.recSize);
          formMain.subeditorOpen(f, (ToolStripButton)sender, true);
      }

      FormMain formMain;
    }
}
