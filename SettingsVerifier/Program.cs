using System;
using System.Collections.Generic;
using System.Text;
using CadEditor;

namespace SettingsEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Checking settings files...");
            string rootDirName = System.IO.Path.GetFullPath(".");
            string[] dirNames = System.IO.Directory.GetDirectories(rootDirName, "settings_*");
            foreach (var dirName in dirNames)
            {
                string[] fileNames = System.IO.Directory.GetFiles(dirName, "Settings_*.cs");
                foreach (var f in fileNames)
                    checkAndPrint(f);
            }
            Console.ResetColor();
            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        static void checkAndPrint(string filename)
        {
            bool result = checkFile(filename);
            Console.ForegroundColor = result ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine(result ? "File verified: {0}" : "File not verified: {0}", filename);
        }

        static bool checkFile(string filename)
        {
            try
            {
                ConfigScript.LoadFromFile(filename);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
