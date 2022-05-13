using System.Diagnostics;
using System.IO;
using ILeoConsole;
using ILeoConsole.Plugin;
using ILeoConsole.Core;

namespace LeoConsole_FileManager
{
  public class FileManager : ICommand
  {
    public string Name { get { return "fm"; } }
    public string Description { get { return "TUI file manager"; } }
    public Action CommandFunktion { get { return () => Command(); } }
    private string[] _InputProperties;
    public string[] InputProperties { get { return _InputProperties; } set { _InputProperties = value; } }
    public IData data = new ConsoleData();

    public void Command()
    {
      RunProcess(
          Path.Join(data.SavePath, "share", "scripts", "lf"),
          "",
          data.SavePath
          );
    }

    // run a process with parameters and wait for it to finish
    public void RunProcess(string name, string args, string pwd)
    {
      try
      {
        Process p = new Process();
        p.StartInfo.FileName = name;
        p.StartInfo.Arguments = args;
        p.StartInfo.WorkingDirectory = pwd;
        p.Start();
        p.WaitForExit();
      }
      catch (Exception e)
      {
        Console.WriteLine("error running binary " + e.Message);
        return;
      }
    }
  }
}

// vim: tabstop=2 softtabstop=2 shiftwidth=2 expandtab
