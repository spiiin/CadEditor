using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using NDesk.Options;

namespace CadEditor
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                var globalConfigName = "Config.cs";

                var optionSet = new OptionSet() {
                    { "romName=",      v => OpenFile.fileName = v },
                    { "configName=",  v => OpenFile.configName = v },
                    { "config=",   v => globalConfigName = v },
                };
                var cmdOptions = optionSet.Parse(args);

                ConfigScript.LoadGlobalsFromFile(globalConfigName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
