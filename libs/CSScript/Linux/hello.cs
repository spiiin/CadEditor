//#!<cscs.exe path> -nl 
//css_ref System.Windows.Forms;
using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

class Script
{
    static void Main()
    {
        Console.WriteLine("Hello Linux!");
        Console.WriteLine(Path.GetFullPath(Path.Combine(Path.GetTempPath(), "CSSCRIPT")));
		
		foreach(Process p in Process.GetProcesses())
			Console.WriteLine(p.Id + ":" + p.ProcessName );
		
		MessageBox.Show("test");
    }
}

