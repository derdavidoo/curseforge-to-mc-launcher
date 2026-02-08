using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace CurseForgeToMcLauncher.Services
{
    public class AppDataService
    {
        public DirectoryInfo AppDataDirectory { get; }
        public FileInfo ConfigFile { get;}
        public string LogsFolderPath => Path.Combine(AppDataDirectory.FullName, "logs");
        public AppDataService() 
        {
            AppDataDirectory = new DirectoryInfo(ApplicationData.Current.RoamingFolder.Path);
            AppDataDirectory.Create();

            ConfigFile = new FileInfo(Path.Combine(AppDataDirectory.FullName, "config.json"));
        }
    }
}
