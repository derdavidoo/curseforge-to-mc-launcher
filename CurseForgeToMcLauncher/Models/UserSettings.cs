using Microsoft.UI.Xaml.Controls;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CurseForgeToMcLauncher.Models
{
    public class UserSettings
    {
        public string MinecraftPath { get; set; }
        public string CurseForgePath { get; set; }

        public UserSettings()
        {
            string roamingFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string userFolder = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            MinecraftPath = Path.Combine(roamingFolder,".minecraft");
            CurseForgePath = Path.Combine(userFolder,"curseforge","minecraft");
        }

        public bool SaveSettings()  
        {
            App.Settings = this;

            if (!Directory.Exists(this.MinecraftPath))
            {
                _ = new ContentDialog
                {
                    Title = "Missing Folder",
                    Content = "The provided folder for 'Minecraft Folder Path' does not exist. Choose a folder by clicking 'Browse' ",
                    CloseButtonText = "OK",
                    XamlRoot = App.MainWindow.Content.XamlRoot
                }.ShowAsync();
                Log.Information("User provided invalid Minecraft path: {MinecraftPath}", this.MinecraftPath);
                return false;
            }
            if (!Directory.Exists(this.CurseForgePath))
            {
                _ = new ContentDialog
                {
                    Title = "Missing Folder",
                    Content = "The provided folder for 'CurseForge Folder Path' does not exist. Choose a folder by clicking 'Browse' ",
                    CloseButtonText = "OK",
                    XamlRoot = App.MainWindow.Content.XamlRoot
                }.ShowAsync();
                Log.Information("User provided invalid CurseForge path: {CurseForgePath}", this.CurseForgePath);
                return false;
            }

            string json = JsonSerializer.Serialize(this);
            File.WriteAllText(App.AppData.ConfigFile.FullName, json);
            Log.Information("Settings written successfully: {Path}", App.AppData.ConfigFile);
            return true;
        }

        public static UserSettings LoadSettings()
        {
            if (App.AppData.ConfigFile.Exists)
            {
                string json = File.ReadAllText(App.AppData.ConfigFile.FullName);
                try 
                {
                    UserSettings deserializedJson = JsonSerializer.Deserialize<UserSettings>(json);
                    Log.Information("Settings were successfully loaded from config");
                    return deserializedJson;
                }
                catch 
                {
                    Log.Information("Json could not be deserialized");
                }
            }
            Log.Information("Could not read config. Using default values...");
            return new UserSettings();
        }
    }
}
