using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using IronPython.Hosting;
using Microsoft.Scripting;

using CadEditor;

namespace PluginExportScreens
{
    public partial class ExportTMX : Form
    {
        public ExportTMX()
        {
            InitializeComponent();
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (sfSave.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                var options = new Dictionary<string, object>();
                options["Frames"] = true;
                options["FullFrames"] = false;

                var engine = Python.CreateEngine(options);
                engine.SetSearchPaths(new[] { "IronPythonLib", "exportTmx", "exportTmx/pytmxlib" });
                var scope = engine.ExecuteFile("exportTmx/exportTmx.py");
                dynamic export = scope.GetVariable("export");
                int layoutNo = cbLayout.SelectedIndex;
                bool result = export(sfSave.FileName, formMain, layoutNo);
                if (result)
                {
                    MessageBox.Show("Export done!");
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private FormMain formMain;

        public void setFormMain(FormMain f)
        {
            formMain = f;
        }

        private void ExportTMX_Load(object sender, EventArgs e)
        {
            cbLayout.Items.Clear();
            foreach (var lr in ConfigScript.getLevelRecs())
                cbLayout.Items.Add(String.Format("Layout {0}:0x{1:X} ({2}x{3})", lr.name, lr.layoutAddr, lr.width, lr.height));
            cbLayout.SelectedIndex = 0;
        }

        private void ExportTMX_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btExport_Click(btExport, new EventArgs());
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
