using System;
using System.Collections.Generic;
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

namespace iut.GestionCaisseInterBDE.Wpf.Views
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class CaisseScreen : Page
    {
        MainWindow window;
        

        public CaisseScreen()
        {
            window = Application.Current.MainWindow as MainWindow;

            InitializeComponent();
            var dialog = (BaseMetroDialog)this.Resources["CustomCloseDialogTest"];
            var dp = ((StackPanel)dialog.Content);
            var itemC = ((ItemsControl)dp.Children[0]);
            itemC.ItemsSource = Singleton<Collection<BDE>>.GetInstance();
            var dc = new CaisseViewModel(DialogCoordinator.Instance);
            this.DataContext = dc;

        }
        
        private async void EncaisserBtn_Click(object sender, RoutedEventArgs e)
        {
            var price = totalPrice.Content.ToString();
            var dialog = (BaseMetroDialog)this.Resources["CustomCloseDialogTest"];

            await this.window.ShowMetroDialogAsync(dialog);

        }


        private void BdeChoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = (BaseMetroDialog)this.Resources["CustomCloseDialogTest"];
            var bdeChosen = (BDE)((Tile)sender).Tag;
            ((CaisseViewModel)DataContext).AddBasketToDB(bdeChosen,dialog);            
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = (BaseMetroDialog)this.Resources["CustomCloseDialogTest"];

            this.window.HideMetroDialogAsync(dialog);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AutoUpdater.Start("http://rbsoft.org/updates/AutoUpdaterTest.xml");

        }

        private async  void HistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            new HistorySales().ShowDialog();
        }
    }
}
