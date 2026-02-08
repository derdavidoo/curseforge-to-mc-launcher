using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using CurseForgeToMcLauncher.Models;
using CurseForgeToMcLauncher.Services;
using System.Threading.Tasks;
using CurseForgeToMcLauncher.Views;
using Serilog;
using Windows.UI.WebUI;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CurseForgeToMcLauncher
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PackSelector : Page
    {

        public ObservableCollection<CurseForgeInstance> Packs { get; } = new();
        public PackSelector()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Packs.Clear();
            foreach (CurseForgeInstance instance in CurseForgeInstance.LoadInstances())
            {
                Packs.Add(instance);
            }
        }

        public List<CurseForgeInstance> GetSelectedPacks()
        {
            return PacksGrid.SelectedItems.Cast<CurseForgeInstance>().ToList();
        }

        private async void Start_Click(object sender, RoutedEventArgs e)
        {
            var (ok, failed) = await ModpackTransferService.TransferModpacks(GetSelectedPacks());

            if (ok == 0) Frame.Navigate(typeof(FailedPage));
            else Frame.Navigate(typeof(FinishPage), (ok,failed));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void PacksGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             Start.IsEnabled = PacksGrid.SelectedItems.Count > 0;

            foreach (CurseForgeInstance instance in e.AddedItems)
            {
                if (!instance.IsValid)
                {
                    Log.Warning("User selected a modpack that wasn't executed: {ModpackName}", instance.Name);
                    var result = await new ContentDialog
                    {
                        Title = "Warning",
                        Content = $"The selected modpack \"{instance.Name}\" was not executed through CurseForge before. It is recommended to run all instances you want to transfer at least once beforehand.\n\nDo you want to keep this Modpack selected?",
                        CloseButtonText = "No",
                        PrimaryButtonText = "Yes",
                        XamlRoot = App.MainWindow.Content.XamlRoot
                    }.ShowAsync();

                    if (result == ContentDialogResult.Primary)
                    {
                        Log.Information("User decided to keep the invalid modpack {ModpackName} selected", instance.Name);
                    }
                    else
                    {
                        Log.Information("User decided to remove the invalid modpack {ModpackName} from selection", instance.Name);
                        PacksGrid.SelectedItems.Remove(instance);
                    }
                }
            }
        }
    }
}
