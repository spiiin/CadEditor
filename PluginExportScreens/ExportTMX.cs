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

        private void tbFilename_Click(object sender, EventArgs e)
        {
            if (sfSave.ShowDialog() == DialogResult.OK)
            { 
                tbFilename.Text = sfSave.FileName;
            }
        }

        private void btExport_Click(object sender, EventArgs e)
        {
            try
            {
                var options = new Dictionary<string, object>();
                options["Frames"] = true;
                options["FullFrames"] = false;

                var engine = Python.CreateEngine(options);
                engine.SetSearchPaths(new[] { "IronPythonLib", "exportTmx", "exportTmx/pytmxlib" });
                var scope = engine.ExecuteFile("exportTmx/exportTmx.py");
                dynamic export = scope.GetVariable("export");
                int layoutNo = cbLayout.SelectedIndex;
                bool result = export(formMain, layoutNo);
                if (result)
                {
                    MessageBox.Show("Export done!");
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
            foreach (var lr in ConfigScript.levelRecs)
                cbLayout.Items.Add(String.Format("Layout {0}:0x{1:X} ({2}x{3})", lr.name, lr.layoutAddr, lr.width, lr.height));
            cbLayout.SelectedIndex = 0;
        }
    }
}
