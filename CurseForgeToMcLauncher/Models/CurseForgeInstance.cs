using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CurseForgeToMcLauncher.Models
{
    public class CurseForgeInstance
    {
        public string Name { get; set; }
        public string LoaderVersion { get; set; }
        public ImageSource Icon { get; set; }
        public string InstanceDir { get; set; }
        public string InstanceJsonPath { get; set; }
        public bool IsValid { get; set; }

        public static string LauncherProfileIcon = "data:image/png;base64,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";
        public static List<CurseForgeInstance> LoadInstances()
        {
            string instancesFolder = Path.Combine(App.Settings.CurseForgePath, "Instances");
            if (Path.Exists(instancesFolder) == false)
            {
                Log.Error($"Instances folder not found at {instancesFolder}");
            }
            string[] instances = Directory.GetDirectories(instancesFolder);

            List<CurseForgeInstance> curseForgeInstances = new List<CurseForgeInstance>();

            foreach (string instance in instances)
            {
                try
                {

                    ImageSource defaultIcon = new BitmapImage(new Uri("ms-appx:///Assets/Image/default_pack_icon.png"));
                    string versionName = "Unknown";
                    ImageSource instanceIcon = defaultIcon;

                    string instanceDir = Path.Combine(instancesFolder, instance);
                    string instanceJsonPath = Path.Combine(instanceDir, "minecraftinstance.json");

                    if (!Path.Exists(instanceDir) || !File.Exists(instanceJsonPath)) continue;

                    // Load and parse the instance JSON to extract loader version and icon information

                    string instanceJson = File.ReadAllText(instanceJsonPath);
                    JsonDocument Json = JsonDocument.Parse(instanceJson);

                    if (
                        Json.RootElement.TryGetProperty("baseModLoader", out JsonElement baseModLoader) &&
                        baseModLoader.TryGetProperty("versionJson", out JsonElement versionJsonElement)
                       )
                    {
                        try 
                        { 
                            JsonDocument versionJson = JsonDocument.Parse(versionJsonElement.GetString());
                            versionJson.RootElement.TryGetProperty("id", out JsonElement id);
                            versionName = id.GetString();
                        }
                        catch
                        {
                            Log.Error($"Failed to parse versionJson for instance at {instance}");
                            continue;
                        }
                       

                    }
                    else continue;

                    // Check for custom profile image first, then fallback to modpack thumbnail if available

                    if (Json.RootElement.TryGetProperty("profileImagePath", out JsonElement profileImagePath) && File.Exists(profileImagePath.GetString()))
                    {
                        instanceIcon = new BitmapImage(new Uri(profileImagePath.GetString()));
                    }
                    else
                    if (Json.RootElement.TryGetProperty("installedModpack", out JsonElement jsonElement) && jsonElement.ValueKind != JsonValueKind.Null)
                        if (jsonElement.TryGetProperty("thumbnailUrl", out JsonElement iconUrl))
                        {
                            try
                            {
                                instanceIcon = new BitmapImage(new Uri(iconUrl.GetString()));
                            }
                            catch { }

                        }
                    int timePLayed = Json.RootElement.TryGetProperty("timePlayed", out JsonElement playtime) ? playtime.GetInt32() : 0;
                    bool alreadyExecuted = timePLayed > 0;

                    curseForgeInstances.Add(new CurseForgeInstance
                    {
                        Name = Path.GetFileName(instance),
                        LoaderVersion = versionName,
                        Icon = instanceIcon,
                        InstanceDir = instanceDir,
                        InstanceJsonPath = instanceJsonPath,
                        IsValid = alreadyExecuted
                    });
                }
                catch
                {
                    Log.Error($"Failed to load instance at {instance}");
                }
            }

            return curseForgeInstances;
        }
    }
}
