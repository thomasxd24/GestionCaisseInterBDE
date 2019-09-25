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
using iut.GestionCaisseInterBDE.Persistance;
using iut.GestionCaisseInterBDE.Wpf.Views.UserControls;
using iut.GestionCaisseInterBDE.Utilities;
using iut.GestionCaisseInterBDE.Wpf.Views;
using iut.GestionCaisseInterBDE.Wpf.Views.Windows;
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


        public Collection<string> Themes  => new Collection<string>()
        {
            "BaseDark",
            "BaseLight"
        };


        public Collection<string> Colors => new Collection<string>()
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
            var db = new SQLPersistance(new SQLiteDatabase($"Data Source={System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)}/bde.db"));
            Singleton<IPersistance>.SetInstance(db);
            Singleton<Event>.SetInstance(new Event());
            comboColors.ItemsSource = Colors;
            comboThemes.ItemsSource = Themes;
            AutoUpdater.CheckForUpdateEvent += AutoUpdaterOnCheckForUpdateEvent;
           
           


        }

        private async void Login()
        {
            var status = "8=====D - - - --- -  (O)   - - - --- - O=======8";
            while (true)
            {
                var persistance = Singleton<IPersistance>.GetInstance();    
                LoginDialogData result = await this.ShowLoginAsync("Authentication",status , new LoginDialogSettings { ColorScheme = this.MetroDialogOptions.ColorScheme, ShouldHideUsername = false, EnablePasswordPreview = true , NegativeButtonVisibility= Visibility.Visible});
                if (result == null)
                {
                    Application.Current.Shutdown();
                    break;
                }

                    
                User user = persistance.GetUserfromCredentials(result.Username, result.Password);
                if (user != null)
                {
                    username.Content = user.Name;
                    ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent(user.Accent),ThemeManager.GetAppTheme(user.Theme));
                    Singleton<User>.SetInstance(user);
                    Singleton<Event>.GetInstance().InvolveUpdateProduct();
                    Singleton<Event>.GetInstance().InvolveChangeUser();
                    break;
                }

                status = "Nom d'utilisateur ou mot de passe non reconnu";


            }
            


        }



        public void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            var flyout = this.Flyouts.Items[0] as Flyout;
            flyout.IsOpen = !flyout.IsOpen;
        }

        private void ComboThemes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (sender as ComboBox).SelectedItem.ToString();
            ThemeManager.ChangeAppTheme(Application.Current, text);
            var currentUser = Singleton<User>.GetInstance();
            currentUser.Theme = text;
            Singleton<IPersistance>.GetInstance().ChangeStyle(currentUser,text, currentUser.Accent);
        }

        private void ComboColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (sender as ComboBox).SelectedItem.ToString();
            var currentTheme = ThemeManager.DetectAppStyle();
            var theme = currentTheme.Item1;
            var currentUser = Singleton<User>.GetInstance();
            currentUser.Accent = text;
            ThemeManager.ChangeAppStyle(Application.Current, ThemeManager.GetAccent(text), theme);
            Singleton<IPersistance>.GetInstance().ChangeStyle(currentUser,currentUser.Theme, text);


        }

        private async void AutoUpdaterOnCheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args != null)
            {
                if (args.IsUpdateAvailable)
                {
                    MessageDialogResult dialogResult;
                    if (args.Mandatory)
                    {
                        dialogResult = await this.ShowMessageAsync(@"Mise a jour disponible",
                            $@"La version {args.CurrentVersion} est maintenant disponible. Vous utilisez la version {args.InstalledVersion}. Appuyer sur OK pour commencer la mise a jour.",
                            MessageDialogStyle.Affirmative);
                    }
                    else
                    {
                        dialogResult = await this.ShowMessageAsync(@"Mise a jour disponible",
                            $@"La version {args.CurrentVersion} est maintenant disponible. Vous utilisez la version {args.InstalledVersion}. Voulez-vous faire la mise a jour?.",
                            MessageDialogStyle.AffirmativeAndNegative);
                    }

                    // Uncomment the following line if you want to show standard update dialog instead.

                    if (dialogResult.Equals(MessageDialogResult.Affirmative))
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

                    await this.ShowMessageAsync(@"Mise a jour indisponible",
                        @"Pas de mise a jour disponible. Veuillez reessayez plus tard.",
                        MessageDialogStyle.Affirmative);
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

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 50; i++)
            {
                Singleton<Event>.GetInstance().InvolveUpdateProduct();
                await Task.Delay(50);
            }
            
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            AutoUpdater.Start("http://thomasxd24.com/update.xml");
        }

        private void Disco_OnClick(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void AccountBtn_OnClick(object sender, RoutedEventArgs e)
        {
            new AccountScreen().ShowDialog();
        }
    }
}
