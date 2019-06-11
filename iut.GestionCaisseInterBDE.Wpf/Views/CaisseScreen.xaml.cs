using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iut.GestionCaisseInterBDE.Utilities;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.Controls;
using iut.GestionCaisseInterBDE.Models;
using System.Globalization;
using System.Collections.ObjectModel;
using GestionCaisseInterBDE.Windows;
using iut.GestionCaisseInterBDE.Wpf.ViewModel;
using MahApps.Metro;
using GestionCaisseInterBDE.Views.Windows;
using AutoUpdaterDotNET;
using iut.GestionCaisseInterBDE.Persistance;

namespace iut.GestionCaisseInterBDE.Wpf.Views
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class CaisseScreen : UserControl
    {
        MainWindow window;
        

        public CaisseScreen()
        {
            window = Application.Current.MainWindow as MainWindow;

            InitializeComponent();
            var dc = new CaisseViewModel(DialogCoordinator.Instance);
            this.DataContext = dc;

        }
       

        private void ProductListBtn_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow().ShowDialog();
        }


        private void SearchBar_GotFocus(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox == null) return;
            if (textbox.Text == "Rechercher...") textbox.Text = "";
        }

        private void SearchBar_LostFocus(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;
            if (textbox == null) return;
            if (textbox.Text == "") textbox.Text = "Rechercher...";
        }


        private void HistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            new HistorySales().ShowDialog();
        }
    }
}
