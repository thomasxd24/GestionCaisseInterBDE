using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutoUpdaterDotNET;
using iut.GestionCaisseInterBDE.Models;
using iut.GestionCaisseInterBDE.Persistence;
using iut.GestionCaisseInterBDE.Persistence.Services;
using iut.GestionCaisseInterBDE.Wpf.Views.UserControls;
using iut.GestionCaisseInterBDE.Utilities;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace iut.GestionCaisseInterBDE.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public readonly Collection<string> Themes = new Collection<string>()
        {
            "BaseDark",
            "BaseLight"
        };


        public readonly Collection<string> Colors = new Collection<string>()
        {
            "Red",
            "Green",
            "Blue",
            "Purple",
            "Orange",
            "Lime",
            "Emerald",
            "Teal",
            "Cyan",
            "Cobalt",
            "Indigo",
            "Violet",
            "Pink",
            "Magenta",
            "Crimson",
            "Amber",
            "Yellow",
            "Brown",
            "Olive",
            "Steel",
            "Mauve",
            "Taupe",
            "Sienna"

        };

        public MainWindow()
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("fr-FR");
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("fr-FR");
            InitializeComponent();
            var db = new SQLiteDatabase($"Data Source={System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}/bde.db");
            Singleton<IDatabase>.SetInstance(db);
            Singleton<Collection<BDE>>.SetInstance(BDEManager.GetBDEList());
            Singleton<Collection<Product>>.SetInstance(ProductManager.GetProductList());


            comboColors.ItemsSource = Colors;
            comboThemes.ItemsSource = Themes;
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
           
           


        }

        private async void Login()
        {
            var customDialog = new CustomDialog() { Title = "Identifiez-vous" };

           
            customDialog.Content = new Views.UserControls.LoginDialog();

            await this.ShowMetroDialogAsync(customDialog);
        }



        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            var flyout = this.Flyouts.Items[0] as Flyout;
            flyout.IsOpen = !flyout.IsOpen;
        }

        private void ComboThemes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (sender as ComboBox).SelectedItem.ToString();
            ThemeManager.ChangeAppTheme(Application.Current, text);
        }

        private void ComboColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (sender as ComboBox).SelectedItem.ToString();
            var currentTheme = ThemeManager.DetectAppStyle();
            var theme = currentTheme.Item1;
            ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent(text), theme);
        }

        private void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args != null)
            {
                if (args.IsUpdateAvailable)
                {
                    MessageBoxResult dialogResult;
                    if (args.Mandatory)
                    {
                        dialogResult =
                            MessageBox.Show(
                                $@"There is new version {args.CurrentVersion} available. You are using version {args.InstalledVersion}. This is required update. Press Ok to begin updating the application.", @"Update Available",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
                    }
                    else
                    {
                        dialogResult =
                            MessageBox.Show(
                                $@"There is new version {args.CurrentVersion} available. You are using version {
                                        args.InstalledVersion
                                    }. Do you want to update the application now?", @"Update Available",
                                MessageBoxButton.YesNo,
                                MessageBoxImage.Information);
                    }

                    // Uncomment the following line if you want to show standard update dialog instead.

                    if (dialogResult.Equals(MessageBoxResult.Yes))
                    {
                        try
                        {
                            if (AutoUpdater.DownloadUpdate())
                            {
                                Application.Current.Shutdown();
                            }
                        }
                        catch (Exception exception)
                        {
                            MessageBox.Show(exception.Message, exception.GetType().ToString(), MessageBoxButton.OK,
                                MessageBoxImage.Error);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(@"There is no update available please try again later.", @"No update available",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show(
                        @"There is a problem reaching update server please check your internet connection and try again later.",
                        @"Update check failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Login();
        }
    }
}
