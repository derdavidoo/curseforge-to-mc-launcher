using CurseForgeToMcLauncher.Models;
using Microsoft.UI.Xaml.Controls;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace CurseForgeToMcLauncher.Services
{
    public static class ModpackTransferService
    {
        public static async Task<(int ok, int failed)> TransferModpacks(List<CurseForgeInstance> instances)
        {
            int successCount = 0;
            int failedCount = 0;

            await SyncLibraries();

            foreach (CurseForgeInstance instance in instances)
            {
                try
                {
                  Log.Debug("Starting transfer for instance: {InstanceName}", instance.Name);
                  await SyncVersionFolder(instance);
                  CreateLauncherProfileJsonEntry(instance);
                  successCount++;
                  Log.Information("Successfully transferred instance: {InstanceName}", instance.Name);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error transferring instance: {InstanceName}", instance.Name);
                    failedCount ++;
                }
            }
            Log.Information("Modpack Transfer Complete. Successful: {SuccessCount}, Failed: {FailedCount}", successCount, failedCount);

            return (successCount, failedCount);
        }

        public static async Task SyncLibraries()
        {
            string minecraftLibDir = System.IO.Path.Combine(App.Settings.MinecraftPath, "libraries");
            string curseForgeLibDir = System.IO.Path.Combine(App.Settings.CurseForgePath, "Install", "libraries");

            await FileCopyService.CopyDirectoryMergeAsync(curseForgeLibDir  , minecraftLibDir);

            Log.Debug("Lib Sync Complete");
        }

        public static async Task SyncVersionFolder(CurseForgeInstance instance)
        {
            string minecraftVersionDir = System.IO.Path.Combine(App.Settings.MinecraftPath, "versions");
            string curseforgeVersionDir = System.IO.Path.Combine(App.Settings.CurseForgePath, "Install", "versions");
            string instanceCfVersionDir = Path.Combine(curseforgeVersionDir, instance.LoaderVersion); // TODO: handle if version does not exist
            string instanceMcVersionDir = Path.Combine(minecraftVersionDir, instance.LoaderVersion);

            DirectoryInfo minecraftInstanceVersionDir = Directory.CreateDirectory(Path.Combine(minecraftVersionDir, instance.LoaderVersion));

            await FileCopyService.CopyDirectoryMergeAsync(instanceCfVersionDir, instanceMcVersionDir);

            Log.Debug("Version Folder Sync Complete: {VersionFolder}", instanceMcVersionDir);
        }

        private static void CreateLauncherProfileJsonEntry(CurseForgeInstance instance)
        {
            string launcherProfileJson = Path.Combine(App.Settings.MinecraftPath, "launcher_profiles.json");
            string json = File.ReadAllText(launcherProfileJson); // TODO: handler falls nicht da

            var root = JsonNode.Parse(json);
            var profiles = root["profiles"] as JsonObject;
            if (profiles == null)
            {
                profiles = new JsonObject();
                root["profiles"] = profiles;
            }

            // Check existing profiles and delete them if matched
            var profilesToRemove = new List<string>();

            foreach (var profile in profiles)
            {
                var profileObj = profile.Value as JsonObject;
                if (profileObj != null)
                {
                    if (profileObj["gameDir"]?.GetValue<string>() == instance.InstanceDir)
                    {
                        profilesToRemove.Add(profile.Key);
                        Log.Debug("Removed existing Json Entry for instance: {InstanceName}", instance.Name);
                    }
                }
            }

            foreach (var currentProfileId in profilesToRemove)
            {
                profiles.Remove(currentProfileId);
            }

            var newProfile = new JsonObject
            {
                ["name"] = instance.Name,
                ["icon"] = CurseForgeInstance.LauncherProfileIcon,
                ["lastVersionId"] = instance.LoaderVersion,
                ["type"] = "custom",
                ["gameDir"] = instance.InstanceDir
            };

            string profileId = Guid.NewGuid().ToString();
            profiles[profileId] = newProfile;

            File.WriteAllText(launcherProfileJson, root.ToJsonString(new System.Text.Json.JsonSerializerOptions { WriteIndented = true}));

            Log.Debug("Added Json Entry");
        }
    }
}
