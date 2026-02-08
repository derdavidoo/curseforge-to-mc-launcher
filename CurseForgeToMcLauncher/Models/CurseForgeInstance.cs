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
