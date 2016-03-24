using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

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
                if (args.Length == 1)
                    globalConfigName = args[0];
                ConfigScript.LoadGlobalsFromFile(globalConfigName);
                ConfigScript.videoNes.updateColorsFromConfig();
            }
            catch (Exception)
            {
                //pass
            }
            if (args.Length >=2)
            {
                OpenFile.FileName   = args[0];
                OpenFile.ConfigName = args[1];             
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
