﻿using System;
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
            window = (MainWindow)Application.Current.MainWindow;

            InitializeComponent();
            var dialog = (BaseMetroDialog)this.Resources["CustomCloseDialogTest"];
            var dp = ((StackPanel)dialog.Content);
            var itemC = ((ItemsControl)dp.Children[0]);
            itemC.ItemsSource = Singleton<Collection<BDE>>.GetInstance();
            var dc = new CaisseViewModel();
            this.DataContext = dc;
            productList.ItemsSource = dc.Products;

        }
        




        


       

        private async void EncaisserBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = (BaseMetroDialog)this.Resources["CustomCloseDialogTest"];

            await this.window.ShowMetroDialogAsync(dialog);

        }


        private async void BdeChoiceBtn_Click(object sender, RoutedEventArgs e)
        {
            var dialog = (BaseMetroDialog)this.Resources["CustomCloseDialogTest"];
            var bdeChosen = (BDE)((Tile)sender).Tag;
            string ticketID = ((CaisseViewModel)DataContext).AddBasketToDB(bdeChosen);
            var totalPrice = ((CaisseViewModel)DataContext).TotalPrice;
            await this.window.HideMetroDialogAsync(dialog);
            await this.window.ShowMessageAsync("Encaissement Réussi", $"Un montant de {totalPrice.ToString("C2")} a été encaissé au {bdeChosen.Name} avec le ticket {ticketID}");
            
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

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            var product = (Product)((Tile)sender).Tag;
            ((CaisseViewModel)DataContext).AddProductToBasket(product);
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
    }
}
