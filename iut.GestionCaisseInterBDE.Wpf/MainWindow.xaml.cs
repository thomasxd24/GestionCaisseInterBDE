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
using iut.GestionCaisseInterBDE.Models;
using MahApps.Metro;
using MahApps.Metro.Controls;
namespace iut.GestionCaisseInterBDE.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public Collection<string> Themes = new Collection<string>()
        {
            "BaseDark",
            "BaseLight"
        };


        public Collection<string> Colors = new Collection<string>()
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
            Singleton<Collection<BDE>>.SetInstance(BDEManager.GetBDEList());
            Singleton<Collection<Product>>.SetInstance(ProductManager.GetProductList());
            comboColors.ItemsSource = Colors;
            comboThemes.ItemsSource = Themes;


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
    }
}
