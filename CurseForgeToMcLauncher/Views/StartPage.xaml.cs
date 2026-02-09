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
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CurseForgeToMcLauncher
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StartPage : Page
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            if (!Path.Exists(App.Settings.MinecraftPath))
            {
                _ = new ContentDialog
                {
                    Title = "Minecraft Directory Not Found",
                    Content = "The Minecraft directory specified in the settings could not be found. Please check your settings and try again. Check the guide on how to configure these Folders correctly.",
                    CloseButtonText = "OK",
                    XamlRoot = App.MainWindow.Content.XamlRoot
                }.ShowAsync();
                return;
            }

            if (!Path.Exists(Path.Combine(App.Settings.CurseForgePath, "Instances")))
            {
                _ = new ContentDialog
                {
                    Title = "CurseForge Directory Not Found",
                    Content = "The CurseForge directory specified in the settings could not be found. Please check your settings and try again. Check the guide on how to configure these Folders correctly",
                    CloseButtonText = "OK",
                    XamlRoot = App.MainWindow.Content.XamlRoot
                }.ShowAsync();
                return;
            }
            Frame.Navigate(typeof(PackSelector));
        }
            
        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingsPage));
        }
    }
}
