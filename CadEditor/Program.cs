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
                ConfigScript.LoadGlobalsFromFile("Config.cs");
            }
            catch (Exception)
            {
                //pass
            }
            if (args.Length == 2)
            {
                OpenFile.FileName   = args[0];
                OpenFile.ConfigName = args[1];
                OpenFile.FileSize = (int)new FileInfo(OpenFile.FileName).Length;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
