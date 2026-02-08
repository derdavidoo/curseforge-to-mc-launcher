using CurseForgeToMcLauncher.Models;
using CurseForgeToMcLauncher.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CurseForgeToMcLauncher
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public static UserSettings currentSettings = new();
        public SettingsPage()
        {
            InitializeComponent();
        }
        // TODO: Default button to reset directory to default value
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (currentSettings.SaveSettings()) Frame.GoBack();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            currentSettings = UserSettings.LoadSettings();
            MinecraftDirectoryTextbox.Text = UserSettings.LoadSettings().MinecraftPath;
            CurseforgeDirectoryTextbox.Text = UserSettings.LoadSettings().CurseForgePath;
        }

        private async void BrowseMinecraftDir_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder directory = await FolderPickerService.PickFolderAsync(App.MainWindow);
            if (directory != null)
            {
                MinecraftDirectoryTextbox.Text = directory.Path;
                currentSettings.MinecraftPath = directory.Path;
            }

        }

        private async void BrowseCurseforgeDir_Click(object sender, RoutedEventArgs e)
        {
            StorageFolder directory = await FolderPickerService.PickFolderAsync(App.MainWindow);
            if (directory != null)
            {
                CurseforgeDirectoryTextbox.Text = directory.Path;
                currentSettings.CurseForgePath = directory.Path;
            }
        }

        private void MinecraftDirectoryTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            currentSettings.MinecraftPath = MinecraftDirectoryTextbox.Text;
        }

        private void CurseforgeDirectoryTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            currentSettings.CurseForgePath = CurseforgeDirectoryTextbox.Text;
        }
    }
}
