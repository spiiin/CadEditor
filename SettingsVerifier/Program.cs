using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CadEditor;

namespace SettingsEditor
{
    class Program
    {
        private static int totalVerified, totalFailed;

        static void Main(string[] args)
        {
            var counter = 0;
            Console.WriteLine("Checking settings files...");
            string rootDirName = System.IO.Path.GetFullPath(".");
            string[] dirNames = System.IO.Directory.GetDirectories(rootDirName, "settings_*");
            foreach (var dirName in dirNames)
            {
                string[] fileNames = System.IO.Directory.GetFiles(dirName, "Settings_*.cs");
                foreach (var f in fileNames)
                {
                    counter++;
                    if (counter > 0) //change to verify only some part of configs
                    {
                        checkAndPrint(f);
                    }
                }
            }
            Console.ResetColor();
            Console.WriteLine("Total verified files: {0}", totalVerified);
            Console.WriteLine("Total failed files  : {0}", totalFailed);
            Console.WriteLine("Done!");
            Console.ReadLine();
        }

        static void checkAndPrint(string filename)
        {
            bool result = checkFile(filename);
            if (result)
            {
                totalVerified++;
            }
            else
            {
                totalFailed++;
            }
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
