using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace CadEditor
{

  public static class PluginLoader
  {
    public static IPlugin loadPlugin(string path)
    {


      Assembly currentAssembly = Assembly.LoadFile(Path.Combine(Directory.GetCurrentDirectory(),path));
      foreach (Type type in currentAssembly.GetTypes())
      {
        if (type.GetInterfaces().Contains(typeof(IPlugin)))
          return (IPlugin)Activator.CreateInstance(type); 
      }
      return null;
    }
  }
  public interface IPlugin
  {
    void addSubeditorButton(FormMain formMain);
  }
}
