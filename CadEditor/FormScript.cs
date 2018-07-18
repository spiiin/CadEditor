using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CSScriptLibrary;

namespace CadEditor
{
    public partial class FormScript : Form
    {
        public FormScript()
        {
            InitializeComponent();
        }

        FormMain formMain;

        public void setFormMain(FormMain f)
        {
            formMain = f;
        }

        public FormMain getFormMain()
        {
            return formMain;
        }

        public void writeLog(string str = "", bool newLine = true)
        {
            tbLog.AppendText(str + (newLine ? Environment.NewLine : ""));
        }

        private void tbScriptFile_Click(object sender, EventArgs e)
        {
            ofScript.RestoreDirectory = true;
            //InitialDirectory property is accepting only \\ slashes without repeats.
            ofScript.InitialDirectory = ConfigScript.ProgramDirectory.Replace("/", "\\").Replace("\\\\", "\\") + "Scripts";
            if (ofScript.ShowDialog() == DialogResult.OK)
            {
                tbScriptFile.Text = ofScript.FileName;
            }
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            try
            {
                var scriptName = tbScriptFile.Text;
                Properties.Settings.Default["LastScript"] = scriptName;
                Properties.Settings.Default.Save();
                tbLog.Clear();
                tbLog.AppendText(String.Format("Running script: {0}\n", scriptName));
                var asm = new AsmHelper(CSScript.LoadCode(File.ReadAllText(scriptName)));
                var script = asm.CreateObject("Script");
                asm.InvokeInst(script, "Execute", this);
                tbLog.AppendText("Script finished\n");
            }
            catch (Exception ex)
            {
                tbLog.AppendText("Script stopped");
                MessageBox.Show(ex.Message, "Error while running script", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormScript_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["LastScript"].ToString() != "")
            {
                tbScriptFile.Text = Properties.Settings.Default["LastScript"].ToString();
            }
        }
    }
}
