using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CadEditor;
using CSScriptLibrary;

namespace SettingsEditor
{
    class Program
    {
        private static int totalVerified, totalFailed;

        private static IList<string> checkFiles(IList<string> filesToCheck)
        {
            totalVerified = totalFailed = 0;
            var notVerified = new List<string>();
            var counter = 0;
            Console.WriteLine("Checking settings files...");
            string rootDirName = Path.GetFullPath(".");
            var dirNames = new List<string>();
            dirNames.AddRange(Directory.GetDirectories(rootDirName + "\\settings_nes", "*", SearchOption.AllDirectories));
            dirNames.AddRange(Directory.GetDirectories(rootDirName + "\\settings_smd", "*", SearchOption.AllDirectories));
            dirNames.AddRange(Directory.GetDirectories(rootDirName + "\\settings_gb", "*", SearchOption.AllDirectories));
            dirNames.AddRange(Directory.GetDirectories(rootDirName + "\\settings_gba", "*", SearchOption.AllDirectories));

            foreach (var dirName in dirNames)
            {
                string[] fileNames = Directory.GetFiles(dirName, "Settings_*.cs");
                foreach (var f in fileNames)
                {
                    if ((filesToCheck == null) || filesToCheck.Contains(f))
                    {
                        counter++;
                        //if (counter > 500) //change to verify only some part of configs
                        {
                            if (!checkAndPrint(f))
                            {
                                notVerified.Add(f);
                            }
                        }
                    }
                }
            }
            Console.ResetColor();
            Console.WriteLine("Total verified files: {0}", totalVerified);
            Console.WriteLine("Total failed files  : {0}", totalFailed);
            Console.WriteLine("Done!");
            return notVerified;
        }

        static void Main(string[] args)
        {
            CSScript.CacheEnabled = false; //not need cache for testing
            CSScript.GlobalSettings.InMemoryAssembly = true;

            var notVerified = checkFiles(null);
            while (notVerified.Count > 0)
            {
                notVerified = checkFiles(notVerified);
                if (notVerified.Count > 0)
                {
                    Console.WriteLine("Press any key to repeat check for all falied files");
                }
                else
                {
                    Console.WriteLine("All files verified!");
                }
                Console.ReadLine();
            }
        }

        static bool checkAndPrint(string filename)
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
            return result;
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
