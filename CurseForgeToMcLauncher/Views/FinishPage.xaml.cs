using ABI.System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI.Xaml.Shapes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CurseForgeToMcLauncher.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FinishPage : Page
    {
        public FinishPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var tuple = (ValueTuple<int, int>)e.Parameter;

            successfulText.Text = "CurseForgeToMcLauncher successfully transfered " + tuple.Item1 + " Instances. \uD83C\uDF89";
            failedText.Text = tuple.Item2 + " Instances could not be transfered.";
        }

        private void viewLogs_Click(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(App.AppData.LogsFolderPath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = App.AppData.LogsFolderPath,
                    UseShellExecute = true
                });
            }
        }
        

        private void reportBugs_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/derdavidoo/Forge-to-Minecraft-Launcher/issues",
                UseShellExecute = true
            });
        }

        private void github_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/derdavidoo/",
                UseShellExecute = true
            });
        }

        private void quit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
