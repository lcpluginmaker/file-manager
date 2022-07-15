using ILeoConsole.Core;
using ILeoConsole.Plugin;
using ILeoConsole;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace LeoConsole_FileManager
{
  public class FileManager : ICommand
  {
    public string Name { get { return "fm"; } }
    public string Description { get { return "TUI file manager"; } }
    public Action CommandFunction { get { return () => Command(); } }
    public Action HelpFunction { get { return () => Console.WriteLine("not available"); } }
    private string[] _Arguments;
    public string[] Arguments { get { return _Arguments; } set { _Arguments = value; } }
    public IData data = new ConsoleData();

    public void Command()
    {
      RunProcess(GetLfPath(), $"-config {data.SavePath}/share/file-manager/lfrc", data.SavePath);
    }

    // get lf binary path
    private string GetLfPath()
    {
      string basePath = Path.Join(data.SavePath, "share", "scripts");
      if (GetRunningOS() == "lnx64") {
        string path = Path.Join(basePath, "lf");
        if (File.Exists(path)) {
          return path;
        }
        throw new Exception("builder binary not found");
      }
      if (GetRunningOS() == "win64") {
        string path = Path.Join(basePath, "lf.exe");
        if (File.Exists(path)) {
          return path;
        }
        throw new Exception("builder binary not found");
      }
      throw new Exception("unknown OS");
    }

    // get OS
    private string GetRunningOS() {
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
        return "win64";
      }
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
        return "lnx64";
      }
      return "other";
    }

    // run a process with parameters and wait for it to finish
    private void RunProcess(string name, string args, string pwd)
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
